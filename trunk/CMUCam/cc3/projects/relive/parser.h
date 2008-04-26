#ifndef _PARSER_h_
#define _PARSER_h_

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
	bool schedule;
	int start_hour;
	int start_min;
	int stop_hour;
	int stop_min;
	HaloInfo* halo_info;
	bool good;
}ConfigInfo;


GPSData *gps;
GPSData *prev_gps;
ConfigInfo *config;

void parse_init(void);
bool parse_GPS(char* gps_data);
bool parse_GPGGA(char* gps_string);
void parse_Config(char* config_string);
void convert(char* time,char* lat,char* lon);
double toRad(double degrees);
double toDeg(char* data,int lat_or_lon);
double calcDist( double nLat1, double nLon1, double nLat2, double nLon2 );
void copy_gps(void);

double sin(double x);
double cos(double x);
double pow(double base,double pow2);

#endif
