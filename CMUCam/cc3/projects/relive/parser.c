#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <stdbool.h>

#include "parser.h"

void parse_init()
{
	config =  (ConfigInfo*)malloc(sizeof(ConfigInfo));
	config->delay = 0;
	config->min_dist = 0;
	config->face_detect = 0;
	config->halo = 0;
	config->halo_info = (HaloInfo*)malloc(sizeof(HaloInfo));
	config->halo_info->name =(char*)malloc(sizeof(char)*50);
	config->halo_info->lat = 0;
	config->halo_info->lon = 0;
	config->halo_info->range = 0;
	config->good = false;
	
	gps = (GPSData*)malloc(sizeof(GPSData));
	gps->lat = 0;
	gps->lon = 0;
	gps->hour = 0;
	gps->minute = 0;
	gps->second = 0;
	gps->month = 0;
	gps->day = 0;
	gps->year = 0;
	gps->good = false;
	
	prev_gps = (GPSData*)malloc(sizeof(GPSData));
	copy_gps();
}

/************************************************************************/

void parse_Config(char* config_string)
{
	int numComma;
	char* str1;
	bool cont = true;
	
	numComma = 0;
	if(config_string == NULL)
		return;

	str1 = strtok(config_string,",");
	while(cont)
	{
		if(str1 == NULL)
		{
			// an error ocurred return without making config good
			return;
		}
		switch(numComma)
		{
		case 0:
			{
				//delay
				config->delay  = atof(str1);
				config->delay *= 60000;
					// times 60000 because we need millisec
					// but config file is in minutes
printf("Delay - %d\n\r", (int)config->delay);
			}
			break;
		case 1:
			{
				//min distance
				config->min_dist  = atof(str1);
printf("Min Dist - %d\n\r", (int)config->min_dist);
			}
			break;
		case 2:
			{
				//halo enabled
				if(strcmp(str1,"True")==0)
				{
					config->halo = true;
printf("Halo - true\n\r");
				}
				else
				{
					config->halo = false;
					cont = false;
printf("Halo - false\n\r");
				}
			}
			break;
		case 3:
			{
				//halo name
				strcpy(config->halo_info->name, str1);
printf("Halo Name - %s\n\r", config->halo_info->name);
			}
			break;
		case 4:
			{
				//lat
				config->halo_info->lat = atof(str1);
printf("Halo Lat - %d\n\r", (int)config->halo_info->lat);
			}
			break;
		case 5:
			{
				//lon
				config->halo_info->lon = atof(str1);
printf("Halo Lon - %d\n\r", (int)config->halo_info->lon);
			}
			break;
		case 6:
			{
				//range
				config->halo_info->range = atof(str1);
printf("Halo Range - %d\n\r", (int)config->halo_info->range);
				cont = false;
			}
			break;
		default:
			{
				//an error occured return dont set config as good
				return;
			}
			break;
		}
		str1 = strtok(NULL,",");
		numComma++;
printf("%d - %s\r\n", numComma++, str1);
	}
	// end of while loop to get basic stuff with success
printf("Config good\n\r");
	config->good = true;
}

/************************************************************************/

void parse_GPS(char* gps_string)
{
	if(gps_string == NULL || gps_string[0]!='$')
		return;
	
	char* str1 = NULL;
	int num_comma = 0;	
	char* time = NULL;
	char* lat = NULL;
	char* date = NULL;
	char* lon = NULL;

	str1 = strtok(gps_string, ",");
	while(1)
	{
		if(str1 == NULL)
			break;

		//don't want to try to convert this data until the fix quality has been checked
		switch(num_comma)
		{
		case 0:  //gpgga
			{
				if(strcmp(str1,"$GPRMC") != 0 )
					return;
			}
			break;
		case 1:  //time
			{
				time = (char*)malloc(15*sizeof(char));
				strcpy(time,str1);
			}
			break;
		case 2:  //active or void
			{
				if(strcmp(str1,"A") != 0 )
				{
					return;
				}
			}
			break;
		case 3:  //lat (in DDDMM.MMMMM)
			{
				lat = (char*)malloc(20*sizeof(char));
				strcpy(lat,str1);
			}
			break;
		case 5:  //lon (in DDDMM.MMMMM)
			{
				lon = (char*)malloc(20*sizeof(char));
				strcpy(lon,str1);
			}
			break;
		case 9:
			{
				date = (char*)malloc(10*sizeof(char));
				strcpy(date,str1);	
				
				gps = convert(time,lat,lon,date);
				
				// first time to aquire signal
				// will also save data into prev_gps
				// for calculating travel distance
				if (!gps->good)
				{
					gps->good = true;
					copy_gps();
				}
				else
					gps->good = true;
				
				free(time);
				free(lat);
				free(lon);
				free(date);
				return;
			}
			break;
		}
		str1 = strtok(NULL,",");
		num_comma++;
	}
}

/************************************************************************/

GPSData* convert(char* time,char* lat,char* lon,char* date)
{
	GPSData* g;

	char* tmp;

	tmp = (char*)malloc(sizeof(char)*3);

	g = (GPSData*)malloc(sizeof(GPSData));

	g->lat = toDeg(lat,0);
	g->lon = toDeg(lon,1)*-1;

	tmp[0] = date[0];
	tmp[1] = date[1];
	tmp[2] = '\0';
	g->day = atoi(tmp);
	
	tmp[0] = date[2];
	tmp[1] = date[3];
	g->month = atoi(tmp);

	tmp[0] = date[4];
	tmp[1] = date[5];
	g->year = atoi(tmp)+2000;

	tmp[0] = time[0];
	tmp[1] = time[1];

	g->hour = atoi(tmp);

	tmp[0] = time[2];
	tmp[1] = time[3];
	g->minute = atoi(tmp);
	
	tmp[0] = time[4];
	tmp[1] = time[5];
	g->second = atoi(tmp);
	
	free(tmp);

	return g;
}

/************************************************************************/

double toDeg(char* DDMM,int latorlon)
{
	//converts from DDDMM.MMMMMM to DDDD.DDDDD
	//use a 0 for lattitude and a 1 for longitude

	char* deg;
	char* min;
	char* ptr;
	double deg_d;
	double min_d;
	int i;
	int DDMM_len;

	if( latorlon != 1 && latorlon != 0 )
		return -99999;

	DDMM_len = (int)strlen(DDMM);
    
	deg = (char*)malloc(sizeof(char)*4);
	min = (char*)malloc(sizeof(char)*10);

	if(latorlon==1)
	{
		//store the degrees
		for(i=0;i<3;i++)
			deg[i] = DDMM[i];
		deg[3] = '\0';

		//store the minutes
		for(i=3;i<DDMM_len;i++)
			min[i-3] = DDMM[i];
		min[i-3] = '\0';
	}
	else
	{
				//store the degrees
		for(i=0;i<2;i++)
			deg[i] = DDMM[i];
		deg[2] = '\0';

		//store the minutes
		for(i=2;i<DDMM_len;i++)
			min[i-2] = DDMM[i];
		min[i-2] = '\0';
	}
	deg_d = strtod(deg,&ptr);
	free(deg);
	
	min_d = strtod(min,&ptr);
	free(min);

	return deg_d + (min_d*MINTODEG);
}

/************************************************************************/

double toRad(double degrees)
{
	return degrees*DEGTORAD;
}

/************************************************************************/

double calcDist( double nLat1, double nLon1, double nLat2, double nLon2 )
{
	double nDLat, nDLon, nA, nC, nD;
	double nRadius = 6378140; // Earths radius in Kilometers

	// Get the difference between our two points 

	// then convert the difference into radians

	nDLat = toRad(nLat2 - nLat1);  

	nDLon = toRad(nLon2 - nLon1); 

	// Here is the new line

	nLat1 =  toRad(nLat1);

	nLat2 =  toRad(nLat2);

	nA = pow ( sin(nDLat/2), 2 ) +

	cos(nLat1) * cos(nLat2) * 

	pow ( sin(nDLon/2), 2 );

	nC = 2 * atan2( sqrt(nA), sqrt( 1 - nA ));

	nD = nRadius * nC;
	return nD; // Return our calculated distance
}

/************************************************************************/

void copy_gps(void)
{	
	prev_gps->lat = gps->lat;
	prev_gps->lon = gps->lon;
	prev_gps->hour = gps->hour;
	prev_gps->minute = gps->minute;
	prev_gps->second = gps->second;
	prev_gps->month = gps->month;
	prev_gps->day = gps->day;
	prev_gps->year = gps->year;
	prev_gps->good = gps->good;
}

/************************************************************************/