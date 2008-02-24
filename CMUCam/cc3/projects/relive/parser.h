#ifndef _PARSER_h_
#define _PAESER_h_

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
double toRad(double degrees);
double calcDist(GPSData* gps1, GPSData *gps2);
void test(char* gps);

#endif