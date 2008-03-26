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
	uint32_t deltaTime = 0;
	int second = 0;
	double deltaDist = 0;
	
printf("\r\nHello, Camera initialized\r\n");

	bool on = true;
	int picNum = 0;
	while (1)
	{
		if (!cc3_uart_has_data (1))
			get_gps_data();
		
		// if its been delay millisecons take picture
		if ( check_triggers(deltaTime, deltaDist, second)  )
		{
			picNum=takePict(picNum);
			write_to_memory(NULL, 0);
			deltaTime = 0;
			second = 0;
		}
		// else update change in time by subtracting previous time off current time
		// then updating previous time to current time
		else
		{
			// update change in parameters
			deltaTime += cc3_timer_get_current_ms() - prevTime;
			deltaDist += calcDist( prev_gps->lat, prev_gps->lon, gps->lat, gps->lon );
			
			// update previous state
			prevTime =  cc3_timer_get_current_ms();
			copy_gps();
			
			// for debugin outputs state every second
			if(deltaTime > second*1000)
			{
				printf("\r\ndeltaTime: %d s\n\rdeltaDist: %d mm\n\r",second,(int)(deltaDist*1000));
				second++;
			}
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

/************************************************************************/
