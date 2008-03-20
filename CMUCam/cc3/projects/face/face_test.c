#include "face.h"

int main (void)
{
	if(face_detect() == 1)
	{
		printf("Face Found\r\n");
	}
	else
	{
		printf("Face Not Found\r\n");
	}
	printf("Done\r\n");
	while(1);
	return 0;
}
