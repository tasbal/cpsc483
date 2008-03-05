#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <stdbool.h>
#include "parser.h"

#define MINTODEG .01667
#define DEGTORAD 0.017453293
#define _CRT_SECURE_NO_WARNINGS

ConfigInfo* parse_Config(char* config_string)
{
	ConfigInfo* c;
	int numComma;
	char* str1;
	
	numComma = 0;
	if(config_string == NULL)
		return NULL;

	c =  (ConfigInfo*)malloc(sizeof(ConfigInfo));

	str1 = strtok(config_string,",");
	while(1)
	{
		if(str1 == NULL)
			break;
		switch(numComma)
		{
		case 0:
			{
				//delay
				c->delay  = atof(str1);
			}
			break;
		case 1:
			{
				//min distance
				c->min_dist  = atof(str1);
			}
			break;
		case 2:
			{
				//face detection
				if(strcmp(str1,"True")==0)
					c->face_detect = true;
				else
					c->face_detect = false;
			}
			break;
		case 3:
			{
				//halo enabled
				if(strcmp(str1,"True")==0)
					c->halo = true;
				else
					c->halo = false;
			}
			break;
		case 4:
			{
				//lat
				if(c->halo == true)
				{
					c->halo_info = (HaloInfo*)malloc(sizeof(HaloInfo));
					c->halo_info->lat = atof(str1);
				}
				else
				{
					c->halo_info = NULL;
				}
				
			}
			break;
		case 5:
			{
				//lon
				if(c->halo == true)
				{
					c->halo_info->lon = atof(str1);
				}
				else
				{
					c->halo_info = NULL;
				}
			}
			break;
		case 6:
			{
				//range
				if(c->halo == true)
				{
					c->halo_info->range = atof(str1);
				}
				else
				{
					c->halo_info = NULL;
				}
				return c;
			}
			break;
		default:
			{
			}
			break;
		}
		str1 = strtok(NULL,",");
		numComma++;
	}

	return c;
}

/************************************************************************/

GPSData* parse_GPS(char* gps_string)
{
	char* str1;
	int num_comma;	
	char* time;
	char* lat;
	char* date;
	char* lon;
	GPSData* g;
	if(gps_string == NULL || gps_string[0]!='$')
		return NULL;

	num_comma = 0;

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
					return NULL;
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
					return NULL;
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
				g = convert(time,lat,lon,date);
				free(time);
				free(lat);
				free(lon);
				free(date);
				return g;
			}
			break;
		}

		str1 = strtok(NULL,",");
		num_comma++;
	}
	return NULL;
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

double calcDist( GPSData* gps1, GPSData* gps2 )
{
	double nLat1, nLon1, nLat2, nLon2;
	double nDLat, nDLon, nA, nC, nD;
	double nRadius = 6378140; // Earth’s radius in Kilometers

	nLat1 = gps1->lat;
	nLon1 = gps1->lon;
	nLat2 = gps2->lat;
	nLon2 = gps2->lon;

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
