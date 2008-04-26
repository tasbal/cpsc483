#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <math.h>
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
	gps->good = false;
	
	prev_gps = (GPSData*)malloc(sizeof(GPSData));
	copy_gps();
}

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
			}
			break;
		case 1:
			{
				//min distance
				config->min_dist  = atof(str1);
			}
			break;
		case 2:
			{
				if(strcmp(str1,"True")==0)
				{
					config->schedule = true;
				}
				else
					config->schedule = false;
			}
			break;
		case 3:
			{
				if(config->schedule)
				{
					config->start_hour = atoi(str1);
				}
				else
				{
					config->start_hour = 0;
				}
			}
			break;
		case 4:
			{
				if(config->schedule)
				{
					config->start_min = atoi(str1);
				}
				else
				{
					config->start_min = 0;
				}
			}
			break;
		case 5:
			{
				if(config->schedule)
				{
					config->stop_hour = atoi(str1);
				}
				else
				{
					config->stop_hour = 23;
				}
			}
			break;
		case 6:
			{
				if(config->schedule)
				{
					config->stop_min = atoi(str1);
				}
				else
				{
					config->stop_min = 59;
				}
			}
			break;
		case 7:
			{
				//halo enabled
				if(strcmp(str1,"True")==0)
				{
					config->halo = true;
				}
				else
				{
					config->halo = false;
					cont = false;
				}
			}
			break;
		case 8:
			{
				//halo name
				strcpy(config->halo_info->name, str1);
			}
			break;
		case 9:
			{
				//lat
				config->halo_info->lat = atof(str1);
			}
			break;
		case 10:
			{
				//lon
				config->halo_info->lon = atof(str1);
			}
			break;
		case 11:
			{
				//range
				config->halo_info->range = atof(str1);
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
	}
	// end of while loop to get basic stuff with success
	config->good = true;
}

bool parse_GPS(char* gps_string)
{
	if(gps_string == NULL || gps_string[0] != '$' || gps_string[1] != 'G' || gps_string[2] != 'P')
		return false;
	if(gps_string[3] == 'G' && gps_string[4] == 'G' && gps_string[5] == 'A')
	{
		return parse_GPGGA(gps_string);
	}
	return false;
}

bool parse_GPGGA(char* gps_string)
{
	char* str1 = NULL;
	int num_comma;	
	char* time = NULL;
	char* lat = NULL;
	char* lon= NULL;
	if(gps_string == NULL || gps_string[0]!='$')
		return NULL;

	num_comma = 0;

	str1 = strsep(&gps_string, ",");
	while(1)
	{
		if(str1 == NULL)
			break;

		//don't want to try to convert this data until the fix quality has been checked
		switch(num_comma)
		{
		case 0:  //gpgga
			{
				if(strcmp(str1,"$GPGGA") != 0 )
					return false;
			}
			break;
		case 1:  //time
			{
				time = (char*)malloc(15*sizeof(char));
				strcpy(time,str1);
			}
			break;
		case 2:  //lat (in DDMM.MMMM)
			{
				lat = (char*)malloc(20*sizeof(char));
				strcpy(lat,str1);
			}
			break;
		case 4:  //lon (in DDDMM.MMMMM)
			{
				lon = (char*)malloc(20*sizeof(char));
				strcpy(lon,str1);	
			}
			break;
		case 6:  //quality indicator
			{
				//0 no fix
				//1 nondifferential fix
				//2 differential fix
				//7 from GPS memory?
				if(strcmp(str1,"0")==0 || strcmp(str1,"") ==0)  
				{
						free(time);
						free(lat);
						free(lon);
						return false;
				}
				if(strcmp(str1,"7")==0 && !gps->good)
				{
					// if we did not lock onto the gps before
					// then dont accept a seven
						free(time);
						free(lat);
						free(lon);
						return false;
				}
				
				convert(time,lat,lon);
				gps->good = true;			
				free(time);
				free(lat);
				free(lon);
				return true;
			}
			break;
		}
		str1 = strsep(&gps_string,",");
		num_comma++;
	}
	return false;
}

void convert(char* time,char* lat,char* lon)
{
	char* tmp = (char*)malloc(sizeof(char)*3);

	gps->lat = toDeg(lat,0);
	gps->lon = toDeg(lon,1)*-1;

	tmp[0] = time[0];
	tmp[1] = time[1];

	gps->hour = atoi(tmp);
	gps->hour-=5;  //account for UTC to CST
	if(gps->hour<0)
		gps->hour+=24;

	tmp[0] = time[2];
	tmp[1] = time[3];
	gps->minute = atoi(tmp);
	
	tmp[0] = time[4];
	tmp[1] = time[5];
	gps->second = atoi(tmp);
	
	free(tmp);
}

double toDeg(char* DDMM,int latorlon)
{
	//converts from DDDMM.MMMMMM to DDDD.DDDDD
	//use a 0 for lattitude and a 1 for longitude

	char* deg = NULL;
	char* min = NULL;
	char* ptr = NULL;
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

double toRad(double degrees)
{
	return degrees*DEGTORAD;
}

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

void copy_gps(void)
{	
	prev_gps->lat = gps->lat;
	prev_gps->lon = gps->lon;
	prev_gps->hour = gps->hour;
	prev_gps->minute = gps->minute;
	prev_gps->second = gps->second;
	prev_gps->good = gps->good;
}

double sin(double x)
{
	double numerator = x;
	double denominator = 1.0;
	double sign = 1.0;
	double sin2 = 0;
	int i;
	// terms below define the number of terms you want
	int terms = 10; 
	for (i = 1 ; i <= terms ; i++ )
	{
		sin2 += numerator / denominator * sign;
		numerator *= x * x;
		denominator *= i*2 * (i*2+1);
		sign *= -1;
	}
	return sin2;
}

double cos(double x)
{
	//taylor series implementation
	double numerator = 1.0;
	double denominator = 1.0;
	double sign = 1.0;
	double cos2 = 0;
	int i;
	int terms = 10;
	for ( i = 1; i<= terms; i++)
	{
		cos2 += numerator/denominator * sign;
		numerator *= x * x;
		denominator *= i*2 * (i*2-1);
		sign *= -1;
	}
	return cos2;
}

double pow(double base,double pow2)
{
	double toRet = 1;
	int i;
	for( i = 0; i < pow2; i++)
	{
		toRet*=base;
	}
	return toRet;
}


