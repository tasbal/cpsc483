#include <stdbool.h>
#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <ctype.h>
#include <cc3.h>
#include <cc3_ilp.h>
#include "parser.h"
#include "relive.h"

int main (void)
{
	initialize();
		
	if(config!=NULL)
	{		
		printf("Delay - %.2lf\tMin Dist - %.2lf\tFace - %d\tHalo - %d\r\n",config->delay,config->min_dist,config->face_detect,config->halo);
		if(config->halo == true)
		{
			printf("\tLat - %.2lf\tLon - %.2lf\tRange - %.2lf\n\r\n",config->halo_info->lat,config->halo_info->lon,config->halo_info->range);
		}
	}
	else
		printf("config.txt INVALID\r\n");
	
	printf("\r\nHello, Camera initialized\r\n");

	bool on = true;
	int picNum = 0;
	while (1)
	{
		picNum=takePict(picNum);
//		get_gps_data();
	
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
