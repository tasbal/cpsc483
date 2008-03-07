#ifndef _parser_h_
#define _parser_h_

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

#define MINTODEG .01667
#define DEGTORAD 0.017453293

typedef enum _bool { false, true } bool;

typedef struct _GPSData
{
	double lat;
	double lon;
	int hour;
	int minute;
	int second;
	int month;
	int day;
	int year;
}GPSData;

typedef struct _HaloInfo
{
	double lat;
	double lon;
	double range;
}HaloInfo;

typedef struct _ConfigInfo
{
	double delay;
	double min_dist;
	bool face_detect;
	bool halo;
	HaloInfo* halo_info;
}ConfigInfo;

GPSData* parse_GPS(char* gps_string);  //call this function with any supported GPS string and it will return parsed
GPSData* parse_GPRMC(char* gps_string);
GPSData* parse_GPGGA(char* gps_string);
GPSData* parse_GPVTG(char* gps_string);
void readFile_GPS();
void readFile_Config();
GPSData* convert(char* time,char* lat,char* lon,char* date);
double toDeg(char* data,int lat_or_lon);
double toRad(double degrees);
double calcDist(GPSData* gps1, GPSData *gps2);

#endif