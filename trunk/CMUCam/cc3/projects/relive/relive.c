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
#include <string.h>
#include "parser.h"
#include "relive.h"

void initialize()
{
	cc3_led_set_state (1, true);
	cc3_led_set_state (2, true);
	
	// configure uarts
	cc3_uart_init (0, CC3_UART_RATE_115200, CC3_UART_MODE_8N1,
		CC3_UART_BINMODE_BINARY);
	cc3_uart_init (1, CC3_UART_RATE_38400, CC3_UART_MODE_8N1,
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
		while(1);
	}
	
	// get config file
	char* config_buff = (char*)malloc(sizeof(char)*100);
	fscanf(memory, "%s", config_buff);
	// parse config file
	config = parse_Config(config_buff);

	
	//configure camera
	cc3_camera_set_colorspace (CC3_COLORSPACE_RGB);
	cc3_camera_set_resolution (CC3_CAMERA_RESOLUTION_HIGH);
	cc3_camera_set_auto_white_balance (true);
	cc3_camera_set_auto_exposure (true);

	int result = fclose (memory);
	if (result == EOF) {
		perror ("fclose failed\r\n");
		while(1);
	}
	
	gps_com = cc3_uart_fopen(1,"r+");
	
	// init pixbuf with width and height
	cc3_pixbuf_load();
	// init jpeg
	init_jpeg();
	printf("\r\n");
	
//	setup_copernicus();

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

void setup_copernicus()
{
	char* gps_buff = (char*)malloc(sizeof(char)*100);
	printf("Cofinguring GPS\r\n");
	
	bool correct = false;
	while(!correct)
	{
		cc3_timer_wait_ms(100);
		// set gps to send RMC data every second
		fprintf(gps_com, "$PTNLSNM,0101,01*FF%c%c",13,10);
		cc3_timer_wait_ms(1000);
		fprintf(gps_com, "$PTNLSNM,0101,01*FF%c%c",13,10);
		
		cc3_timer_wait_ms(1000);
		// query and see if it got changes
		fprintf(gps_com, "$PTNLQNM*FF%c%c",13,10);
		cc3_timer_wait_ms(1000);
		fprintf(gps_com, "$PTNLQNM*FF%c%c",13,10);
		
		fscanf(gps_com,"%s",gps_buff);
		printf("%s\r\n",gps_buff);
		
		if(strcmp(strtok(gps_buff,'*'),"$PTNLaNM,0100,01*")==0)
			correct = true;
		else
			cc3_timer_wait_ms(1000);
	}
	
	free(gps_buff);
	printf("Cofingured GPS\r\n");
}

/************************************************************************/

void get_gps_data()
{
	char* gps_buff = (char*)malloc(sizeof(char)*100);
	
	if (!cc3_uart_has_data (1))
	{
		printf("Getting GPS Data\r\n");
		fscanf(gps_com,"%s",gps_buff);
		printf("%s\r\n",gps_buff);
//		gps = parse_GPS(gps_buff);
//		if(gps!=NULL)
//			printf("Lat - %.2lf\tLon - %.2lf\tDate - %d\\%d\\%d\tTime - %02d:%02d:%02d\r\n",gps->lat,gps->lon,gps->month,gps->day,gps->year,gps->hour,gps->minute,gps->second);
//		else
//			printf("INVALID\r\n");
	}
	
	free(gps_buff);
}

/************************************************************************/
