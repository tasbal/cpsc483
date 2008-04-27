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

bool initialize()
{	
	char* config_buff = (char*)malloc(sizeof(char)*100);

	// init some variables
	prevTime = 0;
	deltaTime = 0;
	deltaDist = 0;
	power_save = false;
	cam_focus_delay = 20000;

	// init parser structures for gps and config
	parse_init();

	// turn on leds so we know it started working
	cc3_led_set_state (1, true);
	cc3_led_set_state (2, true);

	// configure uart for gps serial communication
	cc3_uart_init (0, CC3_UART_RATE_4800, CC3_UART_MODE_8N1,
		CC3_UART_BINMODE_BINARY);

	// init the camera and file system
	cc3_camera_init ();
	cc3_filesystem_init();

#ifdef LOG
	snprintf(log_str, 100, "**********\n\rNew Session\n\r");
	write_log();
	snprintf(log_str, 100, "\n\rReading config file\r\n");
	write_log();
#endif

	// read config file from MMC
	memory = fopen ("c:/config.txt", "r");
	if (memory == NULL) {
		perror ("fopen failed\r\n");
		return false;
	}
	// get config file
	fscanf(memory, "%s", config_buff);
	if (fclose (memory) == EOF) {
		perror ("fclose failed\r\n");
		return false;
	}
	// parse config file
	parse_Config(config_buff);

	// if the config is not good then quit
	if(!config->good)
	{
#ifdef LOG
		snprintf(log_str, 100, "\n\rconfig.txt INVALID\r\n");
		write_log();
#endif
		return false;
	}

#ifdef LOG
	snprintf(log_str, 100, "\r\nConfig File:\n\rDelay(ms) - %d\tMin Dist(mm) - %d",(int)config->delay,(int)(config->min_dist*1000));
	write_log();
	if(config->halo)
	{
		snprintf(log_str, 100, "\tHalo - true\r\n");
		write_log();
		snprintf(log_str, 100, "\tHalo %s:\t Lat*10000 - %d\tLon*10000 - %d\tRange(mm) - %d\r\n",
			config->halo_info->name,
			(int)(config->halo_info->lat*1000000),
			(int)(config->halo_info->lon*1000000),
			(int)(config->halo_info->range*1000) );
		write_log();
	}
	else
	{
		snprintf(log_str, 100, "\tHalo - false\r\n");
		write_log();
	}
#endif

	//configure camera
	cc3_camera_set_colorspace (CC3_COLORSPACE_RGB);
	cc3_camera_set_resolution (CC3_CAMERA_RESOLUTION_HIGH);
	cc3_camera_set_auto_white_balance (true);
	cc3_camera_set_auto_exposure (true);

	// init pixbuf with width and height and JPEG compression
	cc3_pixbuf_load();
	init_jpeg();

	// try to open picNum.txt if exist that will be the 
	// picture number we will start with if not start at 0
#ifdef LOG
	snprintf(log_str, 100, "\n\rReading picNum file\r\n");
	write_log();
#endif
	memory = fopen ("c:/picNum.txt", "r");
	if (memory == NULL) {
		picNum = 0;
	}
	else
	{
		char* picNum_buff = (char*)malloc(sizeof(char)*100);
		fscanf(memory, "%s", picNum_buff);
		picNum = atoi(picNum_buff);
	}
	if (fclose (memory) == EOF) {
		perror ("fclose failed\r\n");
		return false;
	}
#ifdef LOG
	snprintf(log_str, 100, "Starting picture numbering at: %d\r\n",picNum);
	write_log();
#endif

	// starts out awake with no gps signal
	cc3_led_set_state (1, false);
	cc3_led_set_state (2, true);

	cc3_timer_wait_ms(1000);
	free(config_buff);
	return true;
}

/************************************************************************/

void takePict()
{
#ifdef LOG
	snprintf(log_str, 100, "\r\nTaking Picture:\n\r");
	write_log();
#endif

	// Take picture
	char filename[24];
	snprintf(filename, 24, "c:/%d/img%.5d.jpg", gps->hour, picNum);

	// print file that you are going to write to stderr
	fprintf(stderr,"%s\r\n", filename);
	memory = fopen(filename, "w");
	if(memory==NULL || picNum>200 )
	{
		cc3_led_set_state(3, true);
#ifdef LOG
		snprintf(log_str, 100, "Error: Can't open file\r\n" );
		write_log();
#endif
		while(1);
	}
	capture_current_jpeg(memory);
	fclose(memory);
	if ( fclose (memory) == EOF) {
		perror ("fclose failed");
		while(1);
	}

	picNum++;
	// save the picture number just in case the camera is turned
	// off it will start naming with that number
	memory = fopen("c:/picNum.txt", "w");
	if (memory == NULL) {
		perror ("fopen failed");
		while(1);
	}
	fprintf(memory, "%d", picNum);
	if ( fclose (memory) == EOF) {
		perror ("fclose failed");
		while(1);
	}
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
#ifdef LOG
		snprintf(log_str, 100, "\r\nDistance from halo - %d (mm)\r\n", (int)(distance*1000));
		write_log();
#endif
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
	memory = fopen ("c:/metadata.txt", "a");
	if (memory == NULL) {
		perror ("fopen failed");
		while(1);
	}

	fprintf(memory, "%d,%d,%02d:%02d:%02d,",
		(int)(gps->lat*1000000),
		(int)(gps->lon*1000000),
		gps->hour, gps->minute, gps->second);

	if(config->halo)
		fprintf(memory, "%s", config->halo_info->name);

	fprintf(memory, "\r\n");

	if ( fclose (memory) == EOF) {
		perror ("fclose failed");
		while(1);
	}
}

/************************************************************************/

void write_log()
{
	memory = fopen ("c:/log.txt", "a");
	if (memory == NULL) {
		perror ("fopen failed");
		while(1);
	}

	fprintf(memory, "%s",log_str);

	if ( fclose (memory) == EOF) {
		perror ("fclose failed");
		while(1);
	}
}

/************************************************************************/

void get_gps_data()
{
	// gps buffer
	char* gps_buff = (char*)malloc(sizeof(char)*100);

	// scan for data from the gps
	scanf("%s",gps_buff);
#ifdef LOG_GPS
	snprintf(log_str, 100, "\r\nGetting GPS Data: %s\r\n",gps_buff);
	write_log();
#endif

	// now try to parst the gps string
	if(parse_GPS(gps_buff))
	{
#ifdef LOG_GPS
		snprintf(log_str, 100, "Lat - %d\tLon - %d\tTime - %02d:%02d:%02d\r\n",
			(int)(gps->lat*1000000),
			(int)(gps->lon*1000000),
			gps->hour,gps->minute,gps->second);
		write_log();
#endif
		// turn on 'Good GPS LED'
		cc3_led_set_state (1, true);
	}
	else
	{
#ifdef LOG_GPS
		snprintf(log_str, 100, "INVALID\r\n");
		write_log();
#endif
		// turn off 'Good GPS LED'
		cc3_led_set_state (1, false);
	}

	free(gps_buff);
}

/************************************************************************/

void update_time()
{
	deltaTime += cc3_timer_get_current_ms() - prevTime;
	prevTime =  cc3_timer_get_current_ms();
}

/************************************************************************/
