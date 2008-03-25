#define _CRT_SECURE_NO_WARNINGS
//disable warnings about strcpy and strtok (depreciated)
#pragma warning(disable : 4996)

#include "parser.h"

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

GPSData* parse_GPS(char* gps_string)
{
	if(gps_string == NULL || gps_string[0] != '$' || gps_string[1] != 'G' || gps_string[2] != 'P')
		return NULL;
	if(gps_string[3] == 'R' && gps_string[4] == 'M' && gps_string[5] == 'C')
	{
		return parse_GPRMC(gps_string);
	}
	if(gps_string[3] == 'G' && gps_string[4] == 'G' && gps_string[5] == 'A')
	{
		return parse_GPGGA(gps_string);
	}
	if(gps_string[3] == 'V' && gps_string[4] == 'T' && gps_string[5] == 'G')
	{
		return parse_GPVTG(gps_string);
	}

	return NULL;
}
GPSData* parse_GPRMC(char* gps_string)
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
		case 0:  //gprmc
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
					free(time);
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

GPSData* parse_GPVTG(char* gps_string)
{
	//nothing useful out of GPVTG right now, maybe later
	return NULL;
}


GPSData* parse_GPGGA(char* gps_string)
{
	char* str1;
	int num_comma;	
	char* time;
	char* lat;
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
				if(strcmp(str1,"$GPGGA") != 0 )
					return NULL;
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
				if(strcmp(str1,"0")==0 || strcmp(str1,"") ==0)
				{
					free(time);
					free(lat);
					free(lon);
					return NULL;
				}
				else
				{
					g = convert(time,lat,lon,NULL);
					free(time);
					free(lat);
					free(lon);
					return g;
				}
			}
			break;
		}
		str1 = strtok(NULL,",");
		num_comma++;
	}
	return NULL;
}


GPSData* convert(char* time,char* lat,char* lon,char* date)
{
	GPSData* g;

	char* tmp;

	tmp = (char*)malloc(sizeof(char)*3);

	g = (GPSData*)malloc(sizeof(GPSData));
	if(lat!=NULL)
	{
		g->lat = toDeg(lat,0);
	}
	else
	{
		g->lat = -9999;
	}
	
	if(lon!=NULL)
	{
		g->lon = toDeg(lon,1)*-1;
	}
	else
	{
		g->lon = -9999;
	}
	
	if(date!=NULL)
	{
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
	}
	else
	{
		g->day = -9999;
		g->month = -9999;
		g->year = -9999;
	}

	if(time!=NULL)
	{
		tmp[0] = time[0];
		tmp[1] = time[1];

		g->hour = atoi(tmp);

		tmp[0] = time[2];
		tmp[1] = time[3];
		g->minute = atoi(tmp);
	
		tmp[0] = time[4];
		tmp[1] = time[5];
		g->second = atoi(tmp);
	}
	else
	{
		g->hour = -9999;
		g->minute = -9999;
		g->second = -9999;
	}
	free(tmp);

	return g;
}


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



double toRad(double degrees)
{
	return degrees*DEGTORAD;
}

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



void readFile_GPS()
{
	FILE* fptr;
	char* gps;
	GPSData* g;
	GPSData* prev;
	double totDist;
	int i;
	i = 0;
	totDist = 0;
	prev = NULL;
	
	gps = (char*)malloc(sizeof(char)*100);

	if( fopen_s(&fptr,"GPS.txt","r") != 0 )
	{
		printf("Cannot open file\n");
		return;
	}

	while(fgets(gps,100,fptr)!= NULL)
	{
		g = parse_GPS(gps);
		
		if(g!=NULL)
		{		
			printf("%02d\tLat - %.2lf\tLon - %.2lf\tDate - %d\\%d\\%d\tTime - %02d:%02d:%02d\n",i,g->lat,g->lon,g->month,g->day,g->year,g->hour,g->minute,g->second);
			if( prev != NULL )
			{
				totDist += calcDist(g,prev);
			}
			prev = g;
		}
		else
			printf("%02d\tINVALID\n",i);
		i++;
	}
	printf("\n\nTotal Distance Travelled - %lf meters\n",totDist);

	free(gps);
}

void readFile_Config()
{
	FILE* fptr;
	char* config_line;
	ConfigInfo* c;
	int i;
	i = 0;
	
	config_line = (char*)malloc(sizeof(char)*100);

	if( fopen_s(&fptr,"Config.txt","r") != 0 )
	{
		printf("Cannot open file\n");
		return;
	}

	while(fscanf_s(fptr,"%s",config_line,100) != EOF)
	{
		c = parse_Config(config_line);
		
		if(c!=NULL)
		{		
			printf("Delay - %.2lf\tMin Dist - %.2lf\tFace - %d\tHalo - %d\n",c->delay,c->min_dist,c->face_detect,c->halo);
			if(c->halo == true)
			{
				printf("\tLat - %.2lf\tLon - %.2lf\tRange - %.2lf\n\n",c->halo_info->lat,c->halo_info->lon,c->halo_info->range);
			}
		}
		else
			printf("%02d\tINVALID\n",i);
	}
	free(config_line);
}

double sin(double x)
{
	double numerator = x;
	double denominator = 1.0;
	double sign = 1.0;
	double sin = 0;
	int i;
	// terms below define the number of terms you want
	int terms = 10; 
	for (i = 1 ; i <= terms ; i++ )
	{
		sin += numerator / denominator * sign;
		numerator *= x * x;
		denominator *= i*2 * (i*2+1);
		sign *= -1;
	}
	return sin;
}

double cos(double x)
{
	//taylor series implementation
	double numerator = 1.0;
	double denominator = 1.0;
	double sign = 1.0;
	double cos = 0;
	int i;
	int terms = 10;
	for ( i = 1; i<= terms; i++)
	{
		cos += numerator/denominator * sign;
		numerator *= x * x;
		denominator *= i*2 * (i*2-1);
		sign *= -1;
	}
	return cos;
}

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

double pow(double base,double pow)
{
	double toRet = 1;
	int i;
	for( i = 0; i < pow; i++)
	{
		toRet*=base;
	}
	return toRet;
}

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
