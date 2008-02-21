#ifndef _parser_h_
#define _parser_h_

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MINTODEG .01667

typedef struct 
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

GPSData* parse(char* gps_data);
void readFile();
GPSData* convert(char* time,char* lat,char* lon,char* date);
double toDeg(char* data,int lat_or_lon);


#endif