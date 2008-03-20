#include <math.h>
#include <stdbool.h>
#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <cc3.h>
#include <cc3_ilp.h>
#include <cc3_conv.h>
#include <cc3_img_writer.h>
#include <string.h>
#include "parser.h"
#include "relive.h"

void initialize()
{	
	parse_init();

	state = START;
	face = false;
	
	cc3_led_set_state (1, true);
	cc3_led_set_state (2, true);
	
	// configure uarts
	cc3_uart_init (0, CC3_UART_RATE_115200, CC3_UART_MODE_8N1,
		CC3_UART_BINMODE_TEXT);
//	cc3_uart_init (1, CC3_UART_RATE_4800, CC3_UART_MODE_8N1,
//		CC3_UART_BINMODE_BINARY);
	cc3_uart_init (1, CC3_UART_RATE_38400, CC3_UART_MODE_8N1,
		CC3_UART_BINMODE_BINARY);
	// Make it so that stdout and stdin are not buffered
	uint32_t val = setvbuf (stdout, NULL, _IONBF, 0);
	val = setvbuf (stdin, NULL, _IONBF, 0);

	cc3_camera_init ();
	cc3_filesystem_init();

	// read config file from MMC
	printf ("Reading config file\r\n");
	memory = fopen ("c:/config.txt", "r");
	if (memory == NULL) {
		perror ("fopen failed\r\n");
		return;
	}
	// get config file
	char* config_buff = (char*)malloc(sizeof(char)*100);
	fscanf(memory, "%s", config_buff);
	if (fclose (memory) == EOF) {
		perror ("fclose failed\r\n");
		while(1);
	}
	
	// parse config file
	parse_Config(config_buff);
	if(config->good)
	{		
		printf("Delay - %.2lf\tMin Dist - %.2lf",config->delay,config->min_dist);
		if(config->face_detect)
			printf("\tFace - true");
		else
			printf("\tFace - false");
		if(config->halo)
		{
			printf("\tHalo - true\tNumber of Halos - %d\r\n", config->numHalo);
			HaloInfo* tmpHalo = config->halo_info;
			for( int i = 0; i < config->numHalo; i++ )
			{
				printf("\tHalo %s: Lat - %.2lf\tLon - %.2lf\tRange - %.2lf\n\r\n",tmpHalo->name,tmpHalo->lat,tmpHalo->lon,tmpHalo->range);
				tmpHalo = tmpHalo->next;
			}
		}
		else
			printf("\tHalo - false\r\n");
	}
	else
		printf("config.txt INVALID\r\n");
	
	//configure camera
	cc3_camera_set_colorspace (CC3_COLORSPACE_RGB);
	cc3_camera_set_resolution (CC3_CAMERA_RESOLUTION_HIGH);
	cc3_camera_set_auto_white_balance (true);
	cc3_camera_set_auto_exposure (true);
	
	gps_com = cc3_uart_fopen(1,"r+");
	
	// init pixbuf with width and height
	cc3_pixbuf_load();
	// init jpeg
	init_jpeg();
	printf("\r\n");
	
//	setup_copernicus();

	cc3_timer_wait_ms(1000);	
	cc3_led_set_state (1, false);
	cc3_led_set_state (2, false);
	free(config_buff);
	printf("\r\n");
}

/************************************************************************/

int takePict(int picNum)
{
	char filename[16];
	
	do
	{
		snprintf(filename, 16, "c:/img%.5d.jpg", picNum);
		memory = fopen(filename, "r");
		if ( memory != NULL )
		{
			printf( "%s already exists...\r\n",filename); 
			picNum++; 
			fclose(memory);
		}
	}while( memory!=NULL );
	
	// print file that you are going to write to stderr
	fprintf(stderr,"%s\r\n", filename);
	memory = fopen(filename, "w");
	
	if(memory==NULL || picNum>200 )
	{
		cc3_led_set_state(3, true);
		printf( "Error: Can't open file\r\n" );
		while(1);
	}
	printf("Taking Picture\r\n");
	capture_current_jpeg(memory);
	fclose(memory);
	
	picNum++;
	return picNum;
}

/************************************************************************/

bool check_triggers( int deltaTime )
{
	bool takePic = false;
	
	// timer whent off
	if( deltaTime >= config->delay )
	{
		if( gps->good )
			takePic = true;
	}
	
	return takePic;
}

/************************************************************************/

void write_metadata()
{
	memory = fopen ("c:/metadata.txt", "a");
	if (memory == NULL) {
	perror ("fopen failed");
	}
	
	fprintf(memory, "%lf, %lf, %2d:%2d:%2d", gps->lat, gps->lon, gps->hour, gps->minute, gps->second);

	if( face )
		fprintf(memory, ", face");
	if(config->halo)
	{
		HaloInfo* tmpHalo = config->halo_info;
		for(int i = 0; i < which_halo; i++)
		{
			tmpHalo = tmpHalo->next;
		}
		fprintf(memory, ", %s", tmpHalo->name);
	}
	fprintf(memory, "\r\n");

	if ( fclose (memory) == EOF) {
	perror ("fclose failed");
	}
}

/************************************************************************

void setup_copernicus()
{
	char* gps_buff = (char*)malloc(sizeof(char)*100);
	printf("Cofinguring GPS\r\n\n");
	
	int msgLen = 0;
	int chsum = 0;
	
	char msg[100];
	msg[msgLen++] = '$';
	msg[msgLen++] = 'P';
	msg[msgLen++] = 'T';
	msg[msgLen++] = 'N';
	msg[msgLen++] = 'L';
	bool correct = false;
	while(!correct)
	{
		msgLen = 5;
		msg[msgLen++] = 'S';
		msg[msgLen++] = 'N';
		msg[msgLen++] = 'M';
		msg[msgLen++] = ',';
		msg[msgLen++] = '0';
		msg[msgLen++] = '1'; 
		msg[msgLen++] = '0';
		msg[msgLen++] = '0';
		msg[msgLen++] = ','; 
		msg[msgLen++] = '0';
		msg[msgLen++] = '1'; 
		msg[msgLen++] = '*';
		chsum = compute_checksum(msg, msgLen);
		msg[msgLen++] = digit_to_char_hex(chsum>>4);
		msg[msgLen++] = digit_to_char_hex(chsum);
		msg[msgLen++] = 0x0D;	// cr
		msg[msgLen++] = 0x0A;		// lf
		
		for(int i = 0; i < msgLen; i++)
		{
			putchar(fputc(msg[i], gps_com));
		}
		
		printf("\r\nChecking Configuration\r\n\n");
		
		msgLen = 5;
		msg[msgLen++] = 'Q';
		msg[msgLen++] = 'N';
		msg[msgLen++] = 'M';
		msg[msgLen++] = '*';
		chsum = compute_checksum(msg, msgLen);
		msg[msgLen++] = digit_to_char_hex(chsum>>4);
		msg[msgLen++] = digit_to_char_hex(chsum);
		msg[msgLen++] = 0x0D;	// cr
		msg[msgLen++] = 0x0A;		// lf
		
		for(int i = 0; i < msgLen; i++)
		{
			putchar(fputc(msg[i], gps_com));
		}
		
		fscanf(gps_com,"%s",gps_buff);
		printf("%s\r\n",gps_buff);
		
		if(strcmp(strtok(gps_buff,'*'),"$PTNLaNM,0100,01*")==0)
			correct = true;
		else
		{
			printf("\r\nNot Correct\r\n\n");
			cc3_timer_wait_ms(1000);
		}
	}
	
	free(gps_buff);
	printf("\r\nCofingured GPS\r\n");
}

/************************************************************************

int compute_checksum(char* msg, int len)
{
	int chsum = 0;
	int i;
	
	for( i = 1; i < len; i++)
	{
		if(msg[i] == '*')
			break;
		chsum ^= msg[i];
	}
	
	if(i < len && msg[i] =='*')
		return chsum;
	else
		return -1;
}

/************************************************************************

char digit_to_char_hex(int digit)
{
	digit &= 0x0f;
	if(digit < 10)
		return digit + '0';
	else
		return digit + 'A' - 10;
}

/************************************************************************

void get_gps_data()
{
	char* gps_buff = (char*)malloc(sizeof(char)*100);
	
	if (!cc3_uart_has_data (1))
	{
		printf("Getting GPS Data\r\n");
		fscanf(gps_com,"%s",gps_buff);
		printf("%s\r\n",gps_buff);
		gps = parse_GPS(gps_buff);
		if(gps!=NULL)
			printf("Lat - %.2lf\tLon - %.2lf\tDate - %d\\%d\\%d\tTime - %02d:%02d:%02d\r\n",gps->lat,gps->lon,gps->month,gps->day,gps->year,gps->hour,gps->minute,gps->second);
		else
			printf("INVALID\r\n");
	}
	
	free(gps_buff);
}

/************************************************************************/

void get_gps_data()
{
	char *data= (char*)malloc(sizeof(char)*300);
	int dLen = 0;
	
	printf("\r\n\nGetting GPS Data\r\n");
	
	while( state != DONE && state != ERROR)
	{
//printf("Current State: %d\r\n", state);
//printf("Current data length: %d\r\n\n", dLen);
		dLen = receive_byte( fgetc(gps_com), data, dLen );
	}
	
	if(state == DONE) //everything went ok
	{
		printf("\r\nComplete TSIP Packet\r\n");
		for(int i = 0; i < dLen; i++)
		{
			printf(" %x",data[i]);
			write_gpsdata(data[i], 0);
		}
		write_gpsdata(0, 1);
		write_gpsdata(0, 1);
		printf("\r\n");
		
		parse_GPS_tsip(data, dLen);
		if(gps->good)
			printf("Lat - %.2lf\tLon - %.2lf\tDate - %d\\%d\\%d\tTime - %02d:%02d:%02d\r\n",gps->lat,gps->lon,gps->month,gps->day,gps->year,gps->hour,gps->minute,gps->second);
		else
			printf("INVALID\r\n");
		
	}
	else
		printf("Error in getting GPS Data\r\n");
	
	state = START;
	free(data);
}

/************************************************************************/

int receive_byte( char byte, char* data, int dLen )
{
//printf("Recieving byte %x\r\n", byte);
	
	switch ( state )
	{
	case START:
//printf("In START\r\n");
		if( byte == DLE )
		{
//printf("Its a DLE\r\n");
			state = STATE_DLE;
			data[dLen++] = byte;
		}
		break;
	case STATE_DLE:
//printf("In STATE_DLE\r\n");
		if( byte == ETX )
		{
//printf("Its an ETX\r\n");
			// dLen:	   0         1                     >1
			// pkt:	<DLE><id><string><DLE><ETX>
			
			if( dLen > 1 )
			{
//printf("data Length = %d and > 1\r\n", dLen);
				state = DONE;
				data[dLen++] = DLE;
				data[dLen++] = ETX;
			}
			else
			{
printf("In STATE_DLE, byte == ETX, and dLen < 1");
				state = ERROR;
			}
		}
		else
		{
			if( dLen == 1 && byte == DLE )
			{
				state = ERROR;
printf("DLE in id byte!");
			}
			else
			{
//printf("Its not an ETX and its not the id byte\r\n");
				state = IN_PROGRESS;
				data[dLen++] = byte;
			}
		}
		break;
	case IN_PROGRESS:
//printf("In IN_PROGRESS\r\n");
		// Dont add because may be stuffing DLE
		if( byte == DLE )
		{
//printf("Its a DLE\r\n");
			state = STATE_DLE;
		}
		else
		{
//printf("Its not a DLE\r\n");
			data[dLen++] = byte;
		}
		break;
	default:
printf("Should not have gotten here. State = %d\r\n", state);
		state = ERROR;
		break;
	}
	
	// we got too many bytes
	if( dLen >= 300 )
	{
printf("dLen >= 300");
		state = ERROR;
	}
	
	return dLen;
}

/************************************************************************/

void write_gpsdata(char data, int opt)
{
	memory = fopen ("c:/gpsData.txt", "a");
	if (memory == NULL) {
	perror ("fopen failed");
	return;
	}
	
	if ( opt == 0 )
		fprintf(memory, "%x", data);
	else
		fprintf(memory, "\r\n");

	if ( fclose (memory) == EOF) {
	perror ("fclose failed");
	while(1);
	}
}

/************************************************************************/
