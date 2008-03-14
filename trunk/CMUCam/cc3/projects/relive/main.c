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
	
	printf("\r\nHello, Camera initialized\r\n");

	bool on = true;
	int picNum = 0;
	while (1)
	{
		if (!cc3_uart_has_data (1))
			get_gps_data();
		
//		picNum=takePict(picNum);
	
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
