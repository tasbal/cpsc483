#include <stdio.h>
#include <stdlib.h>
#include <string.h>
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
		case 3:
			{
				//halo name
				strcpy(config->halo_info->name, str1);
			}
			break;
		case 4:
			{
				//lat
				config->halo_info->lat = atof(str1);
			}
			break;
		case 5:
			{
				//lon
				config->halo_info->lon = atof(str1);
			}
			break;
		case 6:
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

/************************************************************************/

bool parse_GPS(char* gps_string)
{
	if(gps_string == NULL || gps_string[0] != '$' || gps_string[1] != 'G' || gps_string[2] != 'P')
		return false;
	if(gps_string[3] == 'G' && gps_string[4] == 'G' && gps_string[5] == 'A')
	{
		return parse_GPGGA(gps_string);
	}
	if(gps_string[3] == 'V' && gps_string[4] == 'T' && gps_string[5] == 'G')
	{
		return parse_GPVTG(gps_string);
	}
	return false;
}

/************************************************************************/

bool parse_GPVTG(char* gps_string)
{
	//nothing useful out of GPVTG right now, maybe later
	return false;
}

/************************************************************************/

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
				if(strcmp(str1,"7") && !gps->good)
				{
					// if we did not lock onto the gps before
					// then dont accept a seven
						free(time);
						free(lat);
						free(lon);
						return false;
				}
				
				convert(time,lat,lon,NULL);
				
				// first time to aquire signal
				// will also save data into prev_gps
				// for calculating travel distance
				if (!gps->good)
				{
					copy_gps();
					prev_gps->good = true;
				}
				
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

/************************************************************************/

void convert(char* time,char* lat,char* lon,char* date)
{
	char* tmp = (char*)malloc(sizeof(char)*3);

	gps->lat = toDeg(lat,0);
	gps->lon = toDeg(lon,1)*-1;

	tmp[0] = date[0];
	tmp[1] = date[1];
	tmp[2] = '\0';
	gps->day = atoi(tmp);
	
	tmp[0] = date[2];
	tmp[1] = date[3];
	gps->month = atoi(tmp);

	tmp[0] = date[4];
	tmp[1] = date[5];
	gps->year = atoi(tmp)+2000;

	tmp[0] = time[0];
	tmp[1] = time[1];

	gps->hour = atoi(tmp);

	tmp[0] = time[2];
	tmp[1] = time[3];
	gps->minute = atoi(tmp);
	
	tmp[0] = time[4];
	tmp[1] = time[5];
	gps->second = atoi(tmp);
	
	free(tmp);
}

/************************************************************************/

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


/************************************************************************/

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

/************************************************************************/

double atan2(double num,double den)
{
	double x = num/den;
	double numerator = x;
	double denominator = 1.0;
	double sign = 1.0;
	double arctan = 0;
	int i;
	int terms = 20;
	for ( i = 1; i <= terms; i++)
	{
		arctan += numerator / denominator * sign;
		numerator *= x * x;
		denominator = 2 * i + 1;
		sign *= -1;
	}
	return arctan;
}

/************************************************************************/

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

/************************************************************************/

double sqrt (double y) 
{
	double x, z, tempf;
	unsigned long *tfptr;
	tfptr = ((unsigned long *)&tempf) + 1;
	tempf = y;
	*tfptr = (0xbfcdd90a - *tfptr)>>1; 
	x =  tempf;
	z =  y*0.5;                        
	x = (1.5*x) - (x*x)*(x*z);         
	x = (1.5*x) - (x*x)*(x*z);
	x = (1.5*x) - (x*x)*(x*z);
	x = (1.5*x) - (x*x)*(x*z);
	x = (1.5*x) - (x*x)*(x*z);
	return x*y;
}

/************************************************************************/
