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
	printf("Cofinguring GPS");
	
	// set gps to send RMC data every second
	fprintf(gps, "$PTNLSNM,0100,01*56");
}

/************************************************************************/
