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
#include "relive.h"

void setup_copernicus(FILE* gps)
{
	printf("Cofinguring GPS\r\n");
	
	
	cc3_timer_wait_ms(100);
	// set gps to send RMC data every second
	fprintf(gps, "$PTNLSNM,0100,01*56\n\r");
	
	cc3_timer_wait_ms(1000);
	fprintf(gps, "$PTNLSNM,0100,01*56\n\r");
	
	printf("Cofingured GPS\r\n");
}

/************************************************************************/
