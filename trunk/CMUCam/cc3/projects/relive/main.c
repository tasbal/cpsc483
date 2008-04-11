#include <stdbool.h>
#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <cc3.h>
#include <cc3_ilp.h>
#include "parser.h"
#include "relive.h"

#define gps_start_delay 8000

int main (void)
{	
	initialize();
	// if we could not get a good config file then quit
	if( !config->good )
	{
		destroy_jpeg();
		return 0;
	}
	
	printf("\r\nHello, Camera initialized\r\n");

	bool on = true;
	int picNum = 0;
	while (1)
	{
		// we have not gotten a fix on a sattelite
		// when first turn on and after waking up GPS unit
		if ( !gps->good )
		{
			uint32_t saved_prevTime = prevTime;
			uint32_t saved_deltaTime = deltaTime;
			int saved_second = second;
			
			prevTime = 0;
			deltaTime = 0;
			second = 0;
			
			while( !gps->good )
			{
				get_gps_data();
				
				update_time();
				if(deltaTime > second*1000)
				{
					printf("\r\ndeltaTime: %d s\n\r",second);
					second++;
			
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
			
			// It is the first time we got a fix on a sattelite
			// since the unit first turned on so also save the
			// gps info into prev_gps
			if( first_time_fix )
			{
				copy_gps();
				first_time_fix = false;
			}
			
			prevTime = saved_prevTime;
			deltaTime = saved_deltaTime;
			second = saved_second;	
			cc3_led_set_state (1, true);
		}
		
		//Main function First update time and distance
		update_time();
		deltaDist = calcDist( prev_gps->lat, prev_gps->lon, gps->lat, gps->lon );
		if(deltaTime > second*1000)
		{
			printf("\r\ndeltaTime: %d s\n\rdeltaDist: %d mm\n\r",second,(int)(deltaDist*1000));
			second++;
			
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
		
		// Now it is either in power saving mode or not
		if ( power_save )
		{
			//first check if need to get out of power save
			power_save = (config->delay - deltaTime >= gps_start_delay);
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
			power_save = (config->delay - deltaTime > gps_start_delay);
			if ( power_save )
			{
				//turn off gps and camera
				cc3_gpio_set_value (0, 0);
				cc3_camera_set_power_state (false);
				cc3_led_set_state (1, false);
				continue;
			}
			
			get_gps_data();
			
			if ( check_triggers()  )
			{
				picNum=takePict(picNum);
				write_metadata();
				copy_gps();
				deltaDist = 0;
				deltaTime = 0;
				second = 0;
			}
		}
	}

	destroy_jpeg();
	return 0;
}

/************************************************************************/
