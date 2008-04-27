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
	// if initialization fails then quit
	initialize();
	if( !initialize() )
	{
		destroy_jpeg();
		return 0;
	}

	// start timing at this point
	prevTime =  cc3_timer_get_current_ms();

	// wait for a fix from gps
	while( !gps->good )
	{
		get_gps_data();
		update_time();
	}

#ifdef LOG
	snprintf(log_str, 100, "\n\r It took %d s to get GPS Signal\n\r", (int)(deltaTime/1000));
	write_log();
#endif

	// It is the first time we got a fix on a sattelite
	// since the unit first turned on so also save the
	// gps info into prev_gps
	copy_gps();

	// reset deltaTime and start timing from this point
	deltaTime = 0;
	prevTime =  cc3_timer_get_current_ms();

	// main loop that has two cases when its in power saving mode
	// and when its not
	while (1)
	{
		//First update time and distance
		update_time();
		deltaDist = calcDist( prev_gps->lat, prev_gps->lon, gps->lat, gps->lon );

		// Now it is either in power saving mode or not
		if ( power_save )
		{
			//first check if need to get out of power save
			power_save = (config->delay - deltaTime >= cam_focus_delay);
			if ( !power_save )
			{
				//turn on led2 to know its going to wake up
				cc3_led_set_state(2,true);

				//turn on camera
				cc3_camera_set_power_state (true);
			}
		}
		else
		{
			// first check if need to get into power save, if yes go to next iteration
			power_save = (config->delay - deltaTime > cam_focus_delay);
			if ( power_save )
			{
				//turn off led2 to know its going to sleep
				cc3_led_set_state (2, false);

				//turn off camera
				cc3_camera_set_power_state (false);

				continue;
			}

			get_gps_data();

			// if we have covered the distance and the time is up then
			// check whether to take a picture or not
			if ( deltaDist >= config->min_dist && deltaTime >= config->delay  )
			{
#ifdef LOG
				snprintf(log_str, 100, "\n\r%d has passed and covered %d mm\n\r", (int)(deltaTime/1000), (int)(deltaDist*1000));
				write_log();
#endif
				if( check_triggers() )
				{
					takePict();
					write_metadata();
				}
				copy_gps();
				deltaDist = 0;
				deltaTime = 0;
			}
		}
	}

	destroy_jpeg();
	return 0;
}

/************************************************************************/
