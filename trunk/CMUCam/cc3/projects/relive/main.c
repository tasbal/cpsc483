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
	bool power_save = false;
	
printf("\r\nHello, Camera initialized\r\n");

	bool on = true;
	int picNum = 0;
	while (1)
	{
		// wait until valid gps
		while ( !gps->good )
		{
			get_gps_data();
		}
		
		if ( power_save )
		{
			// update change in parameters
			deltaTime += cc3_timer_get_current_ms() - prevTime;
			
			// update previous state
			prevTime =  cc3_timer_get_current_ms();
			
			// for debugin outputs state every second
			if(deltaTime > second*1000)
			{
				printf("\r\ndeltaTime: %d s\n\r",second);
				second++;
			}
			
			//first check if need to get out of power save
			power_save = (config->delay - deltaTime >= 3000);
			
			if ( !power_save )
			{
				//turn on gps and camera
				cc3_gpio_set_value (0, 1);
				cc3_camera_set_power_state (true);
				gps->good = false;
			}
		}
		else
		{
			// first check if need to get into power save, if yes go to next iteration
			power_save = (config->delay - deltaTime >= 3000);
			
			if ( power_save )
			{
				//turn off gps and camera
				cc3_gpio_set_value (0, 0);
				cc3_camera_set_power_state (false);
				continue;
			}
			
			get_gps_data();
			
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
	}
	
	destroy_jpeg();
	return 0;
}

/************************************************************************/
