#include <math.h>
#include <stdbool.h>
#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <ctype.h>
#include <cc3.h>
#include <cc3_ilp.h>
#include <cc3_img_writer.h>
#include <string.h>
#include "jpeglib.h"
#include "parser.h"
#include "relive.h"

static void capture_current_jpeg(FILE *f);
static void init_jpeg(void);
static void destroy_jpeg(void);
static struct jpeg_compress_struct cinfo;
static struct jpeg_error_mgr jerr;
uint8_t *row;

int main (void)
{
	int result;
	int i = 0;
	uint32_t val;
	FILE *memory;
	FILE *serial_2;
	char* gps_buff = (char*)malloc(sizeof(char)*100);
	char* config_buff = (char*)malloc(sizeof(char)*100);
	GPSData* gps;
	ConfigInfo* config;

	cc3_filesystem_init();

	// configure uarts
	cc3_uart_init (0, CC3_UART_RATE_115200, CC3_UART_MODE_8N1,
		CC3_UART_BINMODE_BINARY);
	cc3_uart_init (1, CC3_UART_RATE_4800, CC3_UART_MODE_8N1,
		CC3_UART_BINMODE_TEXT);
	// Make it so that stdout and stdin are not buffered
	val = setvbuf (stdout, NULL, _IONBF, 0);
	val = setvbuf (stdin, NULL, _IONBF, 0);

	cc3_camera_init ();
	cc3_filesystem_init();

#ifndef VIRTUAL_CAM
	// read config file from MMC
	printf ("\r\n\n\nReading config file\r\n");
	memory = fopen ("c:/config.txt", "r");
	if (memory == NULL) {
		perror ("fopen failed\r\n");
		while(1);
	}
	
	// get config file
	fscanf(memory, "%s", config_buff);
	// parse config file
	config = parse_Config(config_buff);
	
	if(config!=NULL)
	{		
		printf("Delay - %.2lf\tMin Dist - %.2lf\tFace - %d\tHalo - %d\r\n",config->delay,config->min_dist,config->face_detect,config->halo);
		if(config->halo == true)
		{
			printf("\tLat - %.2lf\tLon - %.2lf\tRange - %.2lf\n\r\n",config->halo_info->lat,config->halo_info->lon,config->halo_info->range);
		}
	}
	else
		printf("config.txt INVALID\r\n");
	
	//configure camera
	cc3_camera_set_colorspace (CC3_COLORSPACE_RGB);
	cc3_camera_set_resolution (CC3_CAMERA_RESOLUTION_HIGH);
	cc3_camera_set_auto_white_balance (true);
	cc3_camera_set_auto_exposure (true);

	result = fclose (memory);
	if (result == EOF) {
		perror ("fclose failed\r\n");
		while(1);
	}
	printf("\r\n");
#else
	cc3_camera_set_colorspace (CC3_COLORSPACE_RGB);
	cc3_camera_set_resolution (CC3_CAMERA_RESOLUTION_LOW);
	cc3_camera_set_auto_white_balance (true);
	cc3_camera_set_auto_exposure (true);
#endif
	
	serial_2 = cc3_uart_fopen(1,"r+");
	//setup_copernicus(serial_2);

	printf("\r\nHello, Camera initialized\r\n");
	// init pixbuf with width and height
	cc3_pixbuf_load();
	// init jpeg
	init_jpeg();
	cc3_timer_wait_ms(1000);
	
	cc3_led_set_state (0,true);
	cc3_led_set_state (1, true);
	cc3_led_set_state (2, true);

	char c;

	bool on = true;
	while (1)
	{
/*		char filename[16];
		cc3_led_set_state(0, false);
		cc3_led_set_state(0, true);
		
		do
		{
#ifdef VIRTUAL_CAM
			snprintf(filename, 16, "img%.5d.jpg", i);
#else
			snprintf(filename, 16, "c:/img%.5d.jpg", i);
#endif
			
			memory = fopen(filename, "r");
			if ( memory != NULL )
			{
				printf( "%s already exists...\r\n",filename ); 
				i++; 
				fclose(memory);
			}
		}while( memory!=NULL );
		
		// print file that you are going to write to stderr
		fprintf(stderr,"%s\r\n", filename);
		memory = fopen(filename, "w");
		
		if(memory==NULL || i>200 )
		{
			cc3_led_set_state(3, true);
			printf( "Error: Can't open file\r\n" );
			while(1);
		}
		printf("Taking Picture\r\n");
		capture_current_jpeg(memory);
		fclose(memory);
		i++;
*/		
		if(on)
		{
			cc3_led_set_state (1, false);
			cc3_led_set_state (2, false);
			on = false;
		}
		else
		{
			cc3_led_set_state (1, true);
			cc3_led_set_state (2, true);
			on = true;
		}
		
		//if (!cc3_uart_has_data (1))
		//{
			printf("Getting GPS Data\r\n\n");
			//c = fgetc(serial_2);
			//printf("%c",c);
		        fscanf(serial_2,"%s",gps_buff);
			printf("%s",gps_buff);
			//gps = parse_GPS(gps_buff);
			//if(gps!=NULL)
			//	printf("Lat - %.2lf\tLon - %.2lf\tDate - %d\\%d\\%d\tTime - %02d:%02d:%02d\r\n",gps->lat,gps->lon,gps->month,gps->day,gps->year,gps->hour,gps->minute,gps->second);
			//else
			//	printf("INVALID\n");
		//}
	}
	
	destroy_jpeg();
	return 0;
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
