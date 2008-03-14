#ifndef _PARSER_h_
#define _PAESER_h_

#define MINTODEG .01667
#define DEGTORAD 0.017453293
#define _CRT_SECURE_NO_WARNINGS

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

GPSData* parse_GPS(char* gps_data);
GPSData* parse_GPS_tsip(char* gps_data, int dLen);
ConfigInfo* parse_Config(char* config_string);
GPSData* convert(char* time,char* lat,char* lon,char* date);
double toDeg(char* data,int lat_or_lon);
double toRad(double degrees);
double calcDist(GPSData* gps1, GPSData *gps2);

#endif
