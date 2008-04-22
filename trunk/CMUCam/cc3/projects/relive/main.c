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
	
	int picNum;
	// try to open picNum.txt if exist that will be the 
	// picture number we will start with if not start at 0
	printf ("\n\rReading picNum file\r\n");
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
		while(1);
	}
	printf("Starting picture numbering at: %d\r\n",picNum);
	
	// if delay is greater than 20 min than wake up 1 min before
	// having to take a pic
	if(config->delay >= 20*60000)
		gps_start_delay = 60000;
	else
		gps_start_delay = 15000;
	
	printf("\r\nHello, Camera initialized\r\n");
	
	// start timing at this point
	prevTime =  cc3_timer_get_current_ms();
	// starts out awake
	cc3_led_set_state (2, true);
	bool on = true;
	while (1)
	{
		// we have not gotten a fix on a sattelite
		// when first turn on and after waking up GPS unit
		if ( !gps->good )
		{
			uint32_t saved_deltaTime = deltaTime;
			int saved_second = second;
			
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
		}
		
		// Now it is either in power saving mode or not
		if ( power_save )
		{
			//first check if need to get out of power save
			power_save = (config->delay - deltaTime >= gps_start_delay);
			if ( !power_save )
			{
				//turn on led2 to know its going to wake up
				cc3_led_set_state(2,true);
				on = true;
				
				//turn on gps and camera
				cc3_camera_set_power_state (true);
				cc3_gpio_set_value (0, 1);
				
				//if delay is greater than 20 min than the gps will
				//probably have a cold start
				if(config->delay >=20*60000)
					gps->good = false;
			}
		}
		else
		{
			// first check if need to get into power save, if yes go to next iteration
			power_save = (config->delay - deltaTime > gps_start_delay);
			if ( power_save )
			{
				//turn off led2 to now its going to sleep
				cc3_led_set_state (2, false);
				on = false;
				
				//turn off gps and camera
				cc3_camera_set_power_state (false);
				cc3_gpio_set_value (0, 0);
				cc3_led_set_state (1, false);
				continue;
			}
			
			get_gps_data();
			
			if ( deltaDist >= config->min_dist && deltaTime >= config->delay  )
			{
				if( check_triggers() )
				{
					picNum=takePict(picNum);
					write_metadata();
				}
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
