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
#include "parser.h"
#include "relive.h"


int main (void)
{
	uint32_t val;
	FILE *memory;
	FILE *serial_2;
	char* gps_buff = (char*)malloc(sizeof(char)*100);

	cc3_filesystem_init();

	// configure uarts
	cc3_uart_init (0, CC3_UART_RATE_115200, CC3_UART_MODE_8N1,
		CC3_UART_BINMODE_BINARY);
	cc3_uart_init (1, CC3_UART_RATE_115200, CC3_UART_MODE_8N1,
		CC3_UART_BINMODE_BINARY);
	// Make it so that stdout and stdin are not buffered
	val = setvbuf (stdout, NULL, _IONBF, 0);
	val = setvbuf (stdin, NULL, _IONBF, 0);

	cc3_camera_init ();

	cc3_camera_set_colorspace (CC3_COLORSPACE_RGB);
	cc3_camera_set_resolution (CC3_CAMERA_RESOLUTION_LOW);
	cc3_camera_set_auto_white_balance (true);
	cc3_camera_set_auto_exposure (true);

	cc3_led_set_state (0,true);
	cc3_led_set_state (1, true);
	cc3_led_set_state (2, true);

#ifndef VIRTUAL_CAM
	// read config file from MMC
	int result;
	printf ("\nReading config file\n");
	memory = fopen ("c:/config.dat", "r");
	if (memory == NULL) {
		perror ("fopen failed");
	}

	result = fclose (memory);
	if (result == EOF) {
		perror ("fclose failed");
	}
#endif
	
	
//	printf("Done init of CMUCam\n");
//	setup_copernicus();
	
	// get gps data through serial 2
//	fgets(gps_buff, 100, serial_2);
	
	//printf("testing parser\n");
	//printf("%2d\t",1);
	//test(",,,,,,,W*6A");
	//printf("%2d\t",2);
	//test("$GPRMC,,V,,,,,,,,,W*6A");
	//printf("%2d\t",3);
	//test("$GPRMC,040302.663,A,3939.7,N,10506.6,W,0.27,358.86,200804,,*1A");

	bool on = true;
	while (1)
	{
		if(on)
		{
			cc3_led_set_state (0,false);
			cc3_led_set_state (1, false);
			cc3_led_set_state (2, false);
			on = false;
		}
		else
		{
		
			cc3_led_set_state (0,true);
			cc3_led_set_state (1, true);
			cc3_led_set_state (2, true);
			on = true;
		}
		cc3_timer_wait_ms (1000);
		while(strcmp(gps_buff,"END")!=0)
		{
			printf("\n\n");
			scanf("%s",gps_buff);
			printf("\n\n");
			test(gps_buff);
			
		}
		return 0;
	}
	
	return 0;
}


