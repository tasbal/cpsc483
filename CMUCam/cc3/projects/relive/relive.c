#include <math.h>
#include <stdbool.h>
#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <cc3.h>
#include <cc3_ilp.h>
#include <cc3_conv.h>
#include <cc3_img_writer.h>
#include <string.h>
#include "parser.h"
#include "relive.h"

void initialize()
{	
	parse_init();
	
	cc3_led_set_state (1, true);
	cc3_led_set_state (2, true);
	
	// configure uarts
	cc3_uart_init (0, CC3_UART_RATE_115200, CC3_UART_MODE_8N1,
		CC3_UART_BINMODE_TEXT);
	cc3_uart_init (1, CC3_UART_RATE_4800, CC3_UART_MODE_8N1,
		CC3_UART_BINMODE_BINARY);
	// Make it so that stdout and stdin are not buffered
	uint32_t val = setvbuf (stdout, NULL, _IONBF, 0);
	val = setvbuf (stdin, NULL, _IONBF, 0);

	cc3_camera_init ();
	cc3_filesystem_init();

	// read config file from MMC
	printf ("Reading config file\r\n");
	memory = fopen ("c:/config.txt", "r");
	if (memory == NULL) {
		perror ("fopen failed\r\n");
		return;
	}
	// get config file
	char* config_buff = (char*)malloc(sizeof(char)*100);
	fscanf(memory, "%s", config_buff);
	if (fclose (memory) == EOF) {
		perror ("fclose failed\r\n");
		while(1);
	}
	
	// parse config file
	parse_Config(config_buff);
	if(config->good)
	{		
		printf("Delay - %d\tMin Dist - %.2lf",(int)config->delay,config->min_dist);
		if(config->halo)
		{
			printf("\tHalo - true\r\n");
			printf("\tHalo %s: Lat - %.2lf\tLon - %.2lf\tRange - %.2lf\n\r\n",config->halo_info->name,config->halo_info->lat,config->halo_info->lon,config->halo_info->range);
		}
		else
			printf("\tHalo - false\r\n");
	}
	else
		printf("config.txt INVALID\r\n");
	
	//configure camera
	cc3_camera_set_colorspace (CC3_COLORSPACE_RGB);
	cc3_camera_set_resolution (CC3_CAMERA_RESOLUTION_HIGH);
	cc3_camera_set_auto_white_balance (true);
	cc3_camera_set_auto_exposure (true);
	
	gps_com = cc3_uart_fopen(1,"r+");
	
	// init pixbuf with width and height
	cc3_pixbuf_load();
	
	// init jpeg
	init_jpeg();
	printf("\r\n");

	cc3_timer_wait_ms(1000);	
	cc3_led_set_state (1, false);
	cc3_led_set_state (2, false);
	free(config_buff);
	printf("\r\n");
}

/************************************************************************/

int takePict(int picNum)
{
	char filename[16];
	
	do
	{
		snprintf(filename, 16, "c:/img%.5d.jpg", picNum);
		memory = fopen(filename, "r");
		if ( memory != NULL )
		{
			printf( "%s already exists...\r\n",filename); 
			picNum++; 
			fclose(memory);
		}
	}while( memory!=NULL );
	
	// print file that you are going to write to stderr
	fprintf(stderr,"%s\r\n", filename);
	memory = fopen(filename, "w");
	
	if(memory==NULL || picNum>200 )
	{
		cc3_led_set_state(3, true);
		printf( "Error: Can't open file\r\n" );
		while(1);
	}
	printf("Taking Picture\r\n");
	capture_current_jpeg(memory);
	fclose(memory);
	
	picNum++;
	return picNum;
}

/************************************************************************/

bool check_triggers( int deltaTime )
{
	bool takePic = false;
	int distance = 0;
	
//	if( gps->good )
	{
		// it has halo so must check if within halo
/*		if ( config->halo )
		{
			// if distance is greater than range then return with false
			// else check rest of triggers
			distance = calcDist( config->halo_info->lon, config->halo_info->lat, gps->lat, gps->lon );
			if ( distance >= config->halo_info->range)
				return takePic;
				
		}
		
		// see if covered min distance
		distance = calcDist( prev_gps->lat, prev_gps->lon, gps->lat, gps->lon );
		if ( distance >= config->min_dist)
			takePic = true;
*/		
		// timer went off
		if( deltaTime >= config->delay )
			takePic = true;
	}

	return takePic;
}

/************************************************************************/

void write_to_memory(char* data, int opt)
{
	if(opt == 0)
		memory = fopen ("c:/gpsData.txt", "a");
	else
		memory = fopen ("c:/metadata.txt", "a");
	
	if (memory == NULL) {
	perror ("fopen failed");
	return;
	}
	
	if ( opt == 0 )
	{
		fprintf(memory, "%lf, %lf, %2d:%2d:%2d", gps->lat, gps->lon, gps->hour, gps->minute, gps->second);
		
		if(config->halo)
			fprintf(memory, ", %s", config->halo_info->name);
		fprintf(memory, "\r\n");
	}	
	else if ( opt == 1 )
	{
		if ( data != NULL)
			fprintf(memory, "%s", data);
	}
	else if ( opt == 2 )
		fprintf(memory, "\r\n");

	if ( fclose (memory) == EOF) {
	perror ("fclose failed");
	}
}

/************************************************************************/

void get_gps_data()
{
	char* gps_buff = (char*)malloc(sizeof(char)*100);

	printf("\r\nGetting GPS Data\r\n");
	fscanf(gps_com,"%s",gps_buff);
	printf("%s\r\n",gps_buff);
	
	write_to_memory(gps_buff,1);
	write_to_memory(NULL,2);
	
	parse_GPS(gps_buff);
	if(gps->good)
		printf("Lat - %.2lf\tLon - %.2lf\tDate - %d\\%d\\%d\tTime - %02d:%02d:%02d\r\n",gps->lat,gps->lon,gps->month,gps->day,gps->year,gps->hour,gps->minute,gps->second);
	else
		printf("INVALID\r\n");
	
	free(gps_buff);
}

/************************************************************************/