#include <stdbool.h>
#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <cc3.h>
#include <cc3_ilp.h>
#include "parser.h"
#include "relive.h"

int main (void)
{
	initialize();
	// if we could not get a good config file then quit
	if( !config->good )
	{
		destroy_jpeg();
		return 0;
	}
	
	// set time to equal delay so it takes picture right away
	uint32_t prevTime = 0;
	uint32_t deltaTime = config->delay;
	
	printf("\r\nHello, Camera initialized\r\n");

	bool on = true;
	int picNum = 0;
	while (1)
	{
		if (!cc3_uart_has_data (1))
			get_gps_data();
		
		// if its been delay millisecons take picture
		if ( check_triggers(deltaTime)  )
		{
			picNum=takePict(picNum);
//			face = face_detect();
			write_metadata();
			deltaTime = 0;
		}
		// else update change in time by subtracting previous time off current time
		// then updating previous time to current time
		else
		{
			deltaTime += cc3_timer_get_current_ms() - prevTime;
			prevTime =  cc3_timer_get_current_ms();
		}
	
		// blinking LED to make sure camera is working
		if(on)
		{
			cc3_led_set_state (2, false);
			on = false;
		}
		else
		{
			cc3_led_set_state (2, true);
			on = true;
		}
	}
	
	destroy_jpeg();
	return 0;
}
