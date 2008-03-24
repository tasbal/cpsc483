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
	config->halo_info = NULL;
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
}

/************************************************************************/

void parse_GPS_tsip(char* gps_data, int dLen)
{
	switch(gps_data[1])
	{
	// GPS TIME:
	//
	case 0x41:
		printf("gps time\r\n");
		break;
	case 0x42:
		printf("pos\r\n");
		break;
	case 0x43:
		printf("velocity\r\n");
		break;
	case 0x45:
		printf("software Ver\r\n");
		break;
	case 0x46:
		printf("health\r\n");
		break;
	case 0x4A:
		printf("lla pos\r\n");
		break;
	case 0x4B:
		printf("status\r\n");
		break;
	case 0x55:
		printf("options\r\n");
		break;
	case 0x56:
		printf("velocity\r\n");
		break;
	case 0x6D:
		printf("satelites\r\n");
		break;
	case 0x82:
		printf("sbas\r\n");
		break;
	case 0x83:
		printf("opsition and blas\r\n");
		break;
	case 0x84:
		printf("lla pos and blas\r\n");
		break;
	case 0x8F:
		printf("last fix\r\n");
		break;
	}
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
		if(str1 == NULL)	// couldn't get all necessary just return
			return;
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
				//face detection
				if(strcmp(str1,"True")==0)
					config->face_detect = true;
				else
					config->face_detect = false;
			}
			break;
		case 3:
			{
				//halo enabled
				if(strcmp(str1,"True")==0)
					config->halo = true;
				else
					config->halo = false;
				
				cont = false;
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
	// end of while loop to get basic stuff with success
	config->good = true;
	
printf("config good\r\n");
		
	// if no halos then we are done with configuration from sd card
	if( !config->halo )
		return;
		
printf("It has halo info\r\n");
	
	if(str1 == NULL)
	{
printf("NULL\r\n");
		return;
	}
	config->numHalo = atoi(str1);
	if(config->numHalo < 1)
	{
printf("%s: %d<1\r\n",str1,config->numHalo);
		config->halo = false;
		config->numHalo = 0;
		return;
	}
	
printf("%d halos", config->numHalo);
	
	config->halo_info =  (HaloInfo*)malloc(sizeof(HaloInfo));
	HaloInfo* tmpHalo = config->halo_info;
	tmpHalo->prev = NULL;
	
	
	str1 = strtok(NULL,",");
	for(int i = 0; i < config->numHalo; i ++)
	{
		cont = true;
		numComma = 0;
		
		while(cont)
		{
printf("Halo num %d\n\r",i);
			
			if(str1 == NULL)	// we cant get all info so disregard this halo and rest of info
			{
				if( tmpHalo->prev == NULL)	// then its head
				{
					free(config->halo_info);
					config->halo_info  = NULL;
					config->halo = false;
					config->numHalo = 0;
				}
				else
				{
					tmpHalo = tmpHalo->prev;
					free(tmpHalo->next);
					tmpHalo->next = NULL;
					config->numHalo = i;
				}
				return;
			}
			
			switch(numComma)
			{
			case 0:
				{
					strcpy(tmpHalo->name, str1);
printf("Name: %s\r\n", tmpHalo->name);
				}
				break;
			case 1:
				{
					tmpHalo->lat = atof(str1);
printf("Latitude: %f\r\n", tmpHalo->lat);
				}
				break;
			case 2:
				{
					tmpHalo->lon = atof(str1);
printf("Longitude: %f\r\n", tmpHalo->lon);
				}
				break;
			case 3:
				{
					tmpHalo->range = atof(str1);
printf("Range: %f\r\n", tmpHalo->range);
					cont = false;
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
		// end of while that will get all the info of a single halo
		// if at any point it does not get all the info it will disregard
		// that halo and return because cant trust rest of info
		
		tmpHalo->next = (HaloInfo*)malloc(sizeof(HaloInfo));
		HaloInfo* t = tmpHalo;
		tmpHalo = tmpHalo->next;
		tmpHalo->prev = t;
		
printf("\n\r");
	}
	// end of getting all the halos successfully but we had allocated
	// space for one more so we need to free it
	
	tmpHalo = tmpHalo->prev;
	free(tmpHalo->next);
	tmpHalo->next = NULL;
	
printf("\n\r");
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

double calcDist( GPSData* gps1, GPSData* gps2 )
{
	double nLat1, nLon1, nLat2, nLon2;
	double nDLat, nDLon, nA, nC, nD;
	double nRadius = 6378140; // Earths radius in Kilometers

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
