#include <math.h>
#include <stdbool.h>
#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <ctype.h>
#include <cc3.h>
#include <cc3_ilp.h>
#include <cc3_conv.h>
#include <cc3_img_writer.h>
#include "jpeglib.h"
#include "relive.h"

static struct jpeg_compress_struct cinfo;
static struct jpeg_error_mgr jerr;
uint8_t *row;

/************************************************************************/

void setup_copernicus(FILE* gps)
{
	printf("Cofinguring GPS");
	
	// set gps to send RMC data every second
	fprintf(gps, "$PTNLSNM,0100,01*FF");
}

/************************************************************************/

void init_jpeg(void)
{
	cinfo.err = jpeg_std_error(&jerr);
	jpeg_create_compress(&cinfo);

	// parameters for jpeg image
	cinfo.image_width = cc3_g_pixbuf_frame.width;
	cinfo.image_height = cc3_g_pixbuf_frame.height;
	printf( "image width=%d image height=%d\n", cinfo.image_width, cinfo.image_height );
	cinfo.input_components = 3;
	// cinfo.in_color_space = JCS_YCbCr;
	cinfo.in_color_space = JCS_RGB;

	// set image quality, etc.
	jpeg_set_defaults(&cinfo);
	jpeg_set_quality(&cinfo, 100, true);

	// allocate memory for 1 row
	row = cc3_malloc_rows(1);
	if(row==NULL)
		printf( "Out of memory!\n" );
}

/************************************************************************/

void capture_current_jpeg(FILE *f)
{
	JSAMPROW row_pointer[1];
	row_pointer[0] = row;

	// output is file
	jpeg_stdio_dest(&cinfo, f);

	// capture a frame to the FIFO
	cc3_pixbuf_load();

	// read and compress
	jpeg_start_compress(&cinfo, TRUE);
	while (cinfo.next_scanline < cinfo.image_height)
	{
		cc3_pixbuf_read_rows(row, 1);
		jpeg_write_scanlines(&cinfo, row_pointer, 1);
	}

	// finish
	jpeg_finish_compress(&cinfo);
}

/************************************************************************/

void destroy_jpeg(void)
{
	jpeg_destroy_compress(&cinfo);
	free(row);
}
