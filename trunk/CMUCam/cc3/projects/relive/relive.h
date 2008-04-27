#ifndef _RELIVE_H_
#define _RELIVE_H_

#include "jpeglib.h"

#define LOG
#define LOG_GPS

FILE *memory;
uint32_t prevTime, deltaTime;
double deltaDist;
bool power_save;
int cam_focus_delay;
int picNum;
char log_str[100];

/************************************************************************/

bool initialize(void);
void takePict(void);
bool check_triggers(void);
void write_metadata(void);
void write_log(void);
void get_gps_data(void);
void update_time(void);

static void capture_current_jpeg(FILE *f);
static void init_jpeg(void);
static void destroy_jpeg(void);

/************************************************************************/

static struct jpeg_compress_struct cinfo;
static struct jpeg_error_mgr jerr;
uint8_t *row;

/************************************************************************/

static void init_jpeg()
{
	cinfo.err = jpeg_std_error(&jerr);
	jpeg_create_compress(&cinfo);

	// parameters for jpeg image
	cinfo.image_width = cc3_g_pixbuf_frame.width;
	cinfo.image_height = cc3_g_pixbuf_frame.height;

#ifdef LOG
	snprintf(log_str, 100, "\r\nInitialize JPEG:\r\nimage width=%d image height=%d\n", cinfo.image_width, cinfo.image_height );
	write_log();
#endif

	cinfo.input_components = 3;
	cinfo.in_color_space = JCS_RGB;

	// set image quality, etc.
	jpeg_set_defaults(&cinfo);
	jpeg_set_quality(&cinfo, 80, true);

	// allocate memory for 1 row
	row = cc3_malloc_rows(1);

#ifdef LOG
	if(row==NULL)
	{
		snprintf(log_str, 100, "Out of memory!\r\n" );
		write_log();
	}
#endif
}

/************************************************************************/

static void capture_current_jpeg(FILE *f)
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

static void destroy_jpeg()
{
	jpeg_destroy_compress(&cinfo);
	free(row);
}

/************************************************************************/

#endif
