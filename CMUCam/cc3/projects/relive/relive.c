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
	char* gps_buff = (char*)malloc(sizeof(char)*100);
	printf("Cofinguring GPS\r\n");
	
	
	cc3_timer_wait_ms(100);
	// set gps to send RMC data every second
	fprintf(gps, "$PTNLSNM,0100,01*56%c%c",13,10);
	
	cc3_timer_wait_ms(1000);
	fprintf(gps, "$PTNLSNM,0100,01*56%c%c",13,10);
	
	fprintf(gps, "$PTNLQNM*54%c%c",13,10);
	fscanf(gps,"%s",gps_buff);
	printf("%s\n\n\r\n",gps_buff);
	
	
	printf("Cofingured GPS\r\n");
}

/************************************************************************/
