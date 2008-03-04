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

typedef struct 
{
	int highRes;
}configData;

GPSData* parseGPS(char* gps_data);
configData* parseConfig(char* config_data);
GPSData* convertGPS(char* time,char* lat,char* lon,char* date);
configData* convertConfig(char* res);
double toDeg(char* data,int lat_or_lon);
double toRad(double degrees);
double calcDist(GPSData* gps1, GPSData *gps2);

#endif
