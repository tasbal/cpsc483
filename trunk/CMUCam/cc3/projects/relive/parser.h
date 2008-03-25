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
	bool good;
}GPSData;

typedef struct _HaloInfo
{
	char* name;
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
	bool good;
}ConfigInfo;

/************************************************************************/

GPSData *gps;
GPSData *prev_gps;
ConfigInfo *config;

/************************************************************************/

void parse_init(void);
void parse_GPS(char* gps_data);
void parse_Config(char* config_string);
GPSData* convert(char* time,char* lat,char* lon,char* date);
double toDeg(char* data,int lat_or_lon);
double calcDist( double nLat1, double nLon1, double nLat2, double nLon2 );
void copy_gps(void);

/************************************************************************/

#endif
