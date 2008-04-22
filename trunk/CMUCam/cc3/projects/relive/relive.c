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
	prevTime = 0;
	deltaTime = 0;
	second = 0;
	deltaDist = 0;
	power_save = false;
	first_time_fix = true;
	
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
	printf ("\n\rReading config file\r\n");
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
	
	// for debugin outputs the info from config file
	if(config->good)
	{
		printf("\r\nConfig File:\n\rDelay(ms) - %d\tMin Dist(mm) - %d",(int)config->delay,(int)(config->min_dist*1000));
		if(config->halo)
		{
			printf("\tHalo - true\r\n");
			printf("\tHalo %s:\t Lat*10000 - %d\tLon*10000 - %d\tRange(mm) - %d\r\n",
				config->halo_info->name,
				(int)(config->halo_info->lat*1000000),
				(int)(config->halo_info->lon*1000000),
				(int)(config->halo_info->range*1000) );
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

	//turn on gps
	cc3_gpio_set_mode (0, CC3_GPIO_MODE_OUTPUT);
	cc3_gpio_set_value (0, 1);
	
	gps_com = cc3_uart_fopen(1,"r+");
	
	// init pixbuf with width and height
	cc3_pixbuf_load();
	
	// init jpeg
	printf("\r\nInitialize JPEG:\r\n");
	init_jpeg();

	cc3_timer_wait_ms(1000);	
	cc3_led_set_state (1, false);
	cc3_led_set_state (2, false);
	//turn off camera until ready to take picture
	cc3_camera_set_power_state (false);
	free(config_buff);
}

/************************************************************************/

int takePict(int picNum)
{
	printf("\r\nTaking Picture:\n\r");
	
	// Take picture
	char filename[24];
	snprintf(filename, 24, "c:/%d/img%.5d.jpg", gps->hour, picNum);
	
	// print file that you are going to write to stderr
	fprintf(stderr,"%s\r\n", filename);
	memory = fopen(filename, "w");
	
	if(memory==NULL || picNum>200 )
	{
		cc3_led_set_state(3, true);
		printf( "Error: Can't open file\r\n" );
		while(1);
	}
	capture_current_jpeg(memory);
	fclose(memory);
	
	picNum++;
	
	// save the picture number just in case the camera is turned
	// off it will start naming with that number
	memory = fopen("c:/picNum.txt", "w");
	fprintf(memory, "%d", picNum);
	if ( fclose (memory) == EOF) {
		perror ("fclose failed");
	}
	
	return picNum;
}

/************************************************************************/

bool check_triggers()
{
	// it has halo so must check if within halo
	if ( config->halo )
	{
		// if distance is greater than range then return with false
		// else check rest of triggers
		double distance = calcDist( config->halo_info->lat, config->halo_info->lon, gps->lat, gps->lon );
		printf("\r\ndistance from halo - %d (mm)\r\n", (int)(distance*1000));
		if ( distance >= config->halo_info->range)
			return false;
	}

	// after start time
	if ( gps->hour  >= config->start_hour && gps->minute >= config->start_min )
		// before stop time
		if ( gps->hour  <= config->stop_hour && gps->minute <= config->stop_min )
				return true;

	return false;
}

/************************************************************************/

void write_metadata()
{	
	memory = fopen ("c:/metadata.txt", "a");\
	if (memory == NULL) {
		perror ("fopen failed");
		return;
	}
	
	fprintf(memory, "%d,%d,%2d:%2d:%2d,",
		(int)(gps->lat*1000000),
		(int)(gps->lon*1000000),
		gps->hour, gps->minute, gps->second);
	
	if(config->halo)
		fprintf(memory, "%s", config->halo_info->name);
	
	fprintf(memory, "\r\n");

	if ( fclose (memory) == EOF) {
		perror ("fclose failed");
	}
}

/************************************************************************/

void get_gps_data()
{
	char* gps_buff = (char*)malloc(sizeof(char)*100);
	
	fflush(gps_com);
	fscanf(gps_com,"%s",gps_buff);
	printf("\r\nGetting GPS Data: %s\r\n",gps_buff);
	
	if(parse_GPS(gps_buff))
		printf("Lat - %d\tLon - %d\tDate - %d\\%d\\%d\tTime - %02d:%02d:%02d\r\n",
			(int)(gps->lat*1000000),
			(int)(gps->lon*1000000),
			gps->month,gps->day,gps->year,
			gps->hour,gps->minute,gps->second);
	else
		printf("INVALID\r\n");
	
	free(gps_buff);
}

/************************************************************************/

void update_time()
{
	deltaTime += cc3_timer_get_current_ms() - prevTime;
	prevTime =  cc3_timer_get_current_ms();
}

/************************************************************************/
