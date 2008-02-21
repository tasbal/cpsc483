#!usr/bin/perl
#
#  mapper.pl
#
#  03.23.06
#  Tim Vidas
#  tvidas(ta)nucia(tod)unomaha(tod)edu
#
#  This program takes a directory as input and creates a single xml file and one jpg thumbnail for each jpg in the input directory.  The output xml 
#  (when combined with the php file distributed with this) will greate a Google Map from the the GPS data embedded in the jpgs.  Each marker will 
#  have it's own numbered GMap icon and when clicked will display the thumbnail created by this script.  It is assumed that the images (and thumbnails)
#  are placed in a ./images subdirectory.  The associated php file should be fairly straighforward and easy to modify.
#
#Pre-reqs
#  requires perl (duh) 
#    -uses Image::ExifTool
#    -uses File::Basename::Object (for windows CPAN compatibility)
#  the input images must have GPS data (Nikon D200, Dx1, Dx2 and Ricoh Pro G3 are the only ones i know of at this time)
#  you must sign up for a GMaps Key - it's free    http://www.google.com/apis/maps/signup.html
#    -and you must edit the php file to reflect your key
#    -if you want to use html instead of php just remove everything between the <?php and ?> tags - then it's html
#
#Disclaimer
#  Use are your own risk.  No llamas were injured in the making of this script.
#
#  I've run this script on windows using activeperl, and only on jpgs created by a Nikon 200 Camera, YMMV - but please let me know if you have success
#  with other cameras / OSes etc
#
#To DO
#    Works in North America Only - need to etract more GPS data to flips signs right - not planning on fixing this soon since gmaps only does NA right now
#    Extract a bit more info to put into the XML - mainly just to give the thumbnail views a little more to go on - date / title / etc
#    Make input directory, output files more user friendly :-)
#    Argumentify the script, -k for kml, -m for maps, -i input directory, etc.
#
#Distribution
#    GPL.  use it, share it, pass it along, just keep my name in it somewhere.  thanks.
#
#2008.02.21 Modifications made by Mario Raushel raushel(at)gmail(dot)com
#		   v4 added time, date and title to tooltip
#		   icons now checked at ../icon/ rather than direct linking (removed hotlinks to Tim Vidas's site)
use Image::ExifTool;
use File::Basename::Object;
use strict;
my $GPScount = 0;
my $NonGPScount = 0;
my $ImageCount = 0;
my $GeoOutput = "";
my $MarkerOutput = "<markers>\n";
my $KMLOutput = '<?xml version="1.0" encoding="UTF-8"?> <kml xmlns="http://earth.google.com/kml/2.0"> <Folder>';
my $title;
my $domain = "localhost";
my $rootdir;
my $putdir;
my $desc;

print "enter full path to root directory of images (ie: \"C:\Documents and Settings\\user\\My Pictures\")\n";
$rootdir = <>;
chomp $rootdir;
if ($rootdir eq '' or $rootdir eq ""){
	$rootdir = "C:\\Documents and Settings\\user\\My Pictures";
}


print "enter full path to put XML output file ($rootdir)\n";
$putdir = <>;
chomp $putdir;
if ($putdir eq '' or $putdir eq ""){
	$putdir = $rootdir;
}
print "Title of this Set? (eg 'UNO')\n";
$title = <>;
chomp $title;
if ($title eq '' or $title eq ""){
	$title = $desc = "Untitled";
}else{
	print "description of the set? (eg 'My Campus')\n";
	$desc = <>;
	chomp $desc;
	if ($desc eq '' or $desc eq ""){
		$desc = "no description given";
	}
}
	
$KMLOutput .= "<name>$title</name>\n<open>1</open>\n<description>$desc</description>\n";

do_dir($rootdir);
#do_dir('C:\Documents and Settings\username\Desktop\pics');


print "Images     : $ImageCount\n";
print "  With GPS : $GPScount\n";
print "  W/O GPS  : $NonGPScount\n";

$MarkerOutput .= "</markers>\n";

# i used to store all the html data here for the GMap.htm (now php) file, but in the end i just decided it was dumb to put in the
# perl script and i'd just distribut the php file with this script

open MARKERS, ">$putdir\\GMapNew.xml" or die "cannot create file: $!";
#open MARKERS, ">GMapNew.xml" or die "cannot create file: $!";
print MARKERS $MarkerOutput;
close MARKERS;

$KMLOutput .= "</Folder>\n</kml>\n";
open KML, ">$putdir\\GMapNew.kml" or die "cannot create file: $!";
print KML $KMLOutput;
close KML;

exit;

#subroutine to get the GPS data from the jpg exif
#also since it's hitting every jpg anyway, calls sub that pulls the binary thumbnail from the file
sub get_exif_gps {
	my $deg;
	my $temp;
	my $min;
	my $sec;
	my $date;
	my $time;
	my $return = 0;
	my $length = 0;
	my $offset = 0;
	my $lat;
	my $lng;
	my $iconNum;
	my $filename = shift;
	my $filebase = File::Basename::Object->new($filename);
	my $desc2 = File::Basename::Object->new($filebase->dirname);
	   $desc2 = File::Basename::Object->new($desc2->dirname);
	   $desc2 = $desc2->basename;
	my $exifTool = new Image::ExifTool;
	   $exifTool->Options(Binary => 1, IgnoreMinorErrors => 1);
	my $info = $exifTool->ImageInfo($filename,'GPSAltitude','GPSLongitude','GPSLatitude','GPSDateTime','GPSAltitude','GPSDateStamp','GPSTimeStamp','GPSSatellites','ThumbnailLength','ThumbnailOffset');

	if(%$info == ''){
		print "\tNO GPS DATA!\n";
		$return = 1;
		$NonGPScount++;
	}else{
		#print "Has GPS Data";
		$return = 0;
		$GPScount++;

		#$MarkerOutput .= "<marker html=\"&lt;a href=\'$filename\'&gt;" . $filebase->basename . "&lt;/a&gt;&lt;img src='./images/" . $filebase->basename . "-thumb.jpg'&gt;\" ";
		$MarkerOutput .= "<marker html=\"&lt;a href='./images/". $filebase->basename ."'&gt;&lt;img src='./images/" . $filebase->basename . "-thumb.jpg'&gt;&lt;/a&gt;\" name=\"" . $filebase->basename . "\" ";
		foreach (keys %$info) {
			my $val = $$info{$_};
			if (ref $val eq 'ARRAY'){
				$val = join (', ', @$val);
			}elsif (ref $val eq 'SCALAR') {
				$val = '(## binary Data ##)';
			}
			printf("\t%-30s : %s.\n", $_, $val);
			if($_ eq "GPSLongitude"){
				($deg,$temp,$min,$sec)=split(/ /,$val);
				$val = -1 * ($deg + $min/60 + $sec/3600); #the -1 is a North America thing
				 print  "D $deg, T $temp, M $min, S $sec = $val\n";
				$MarkerOutput .= "lng=\"$val\" ";
				$lng = $val;
			}
			if($_ eq "GPSLatitude"){
				($deg,$temp,$min,$sec)=split(/ /,$val);
				$val = $deg + $min/60 + $sec/3600;
				print "D $deg, T $temp, M $min, S $sec = $val\n";
				$MarkerOutput .= "lat=\"$val\" ";
				$lat = $val;
			}
			#adding gps date data
			if($_ eq "GPSDateStamp"){
				($date)=split(/ /,$val);
				$val = $date;
				print "Date $date = $val\n";
				$MarkerOutput .= "date=\"$val\" ";
				$date = $val;
			}
			#adding gps time data
			if($_ eq "GPSTimeStamp"){
				($time)=split(/ /,$val);
				$val = $time;
				print "Time $time = $val\n";
				$MarkerOutput .= "time=\"$val\" ";
				$time = $val;
			}
			if($_ eq "ThumbnailLength"){
				$length = $val;
			}
			if($_ eq "ThumbnailOffset"){
				$offset = $val;
			}
			#print "$_ => $$info{$_}\n";	
		}
		#determine marker number
		if($GPScount < 100){
				$iconNum = $GPScount;
				if($iconNum < 10){
				$iconNum = "0" . $iconNum;
				}
				if($iconNum < 100){
				$iconNum = "0" . $iconNum;
				}
		}else{
			$iconNum = "000";
		}
				
		$MarkerOutput .= " label=\"$GPScount\"/>\n";
		$KMLOutput .= "<Placemark>\n<name>" . $filebase->basename ."</name><description>\n";
		$KMLOutput .= "<![CDATA[\n<a href=\"http://$domain/mapper/$desc2/images/" . $filebase->basename . "\">";
		$KMLOutput .= "<img src=\"http://$domain/mapper/$desc2/images/". $filebase->basename ."-thumb.jpg\"></a>\n]]>\n</description>\n";
		$KMLOutput .= "<LookAt id =\"tmv$GPScount\">\n";
		$KMLOutput .= "<longitude>$lng</longitude>\n";
		$KMLOutput .= "<latitude>$lat</latitude>\n";
		$KMLOutput .= "<tilt>0</tilt>\n<heading>90</heading>\n</LookAt>\n";
		$KMLOutput .= "<visibility>1</visibility>\n<Style>\n<IconStyle>\n<Icon>\n";
		$KMLOutput .= "<href>http://$domain/mapper/icon/b" . $iconNum . ".png</href>\n";
		$KMLOutput .= "</Icon>\n</IconStyle>\n</Style>\n";
		$KMLOutput .= "<Point>\n<coordinates>$lng,$lat</coordinates>\n</Point>\n";
		$KMLOutput .= "</Placemark>\n";
		extract_thumb($filename,$offset,$length);
	}
	print "\n";
	return $return;
}

#subroutine that uses binmode to put the thumbnail from the jpg header
sub extract_thumb{
	my $filename = shift; 
	my $offset = shift;
	my $length = shift;
	my $buff;

	print "extracting from $filename,\n \tat $offset for $length";

	open(BINREAD, $filename)         or die "can't open $filename: $!";
	open(BINWRITE, ">$filename-thumb.jpg") or die "can't create new file $!";

	#both filehandles need to operate in binary mode
	binmode(BINREAD);              
	binmode(BINWRITE);            
	#while (read(GIF, $buff, 8 * 2**10)) {
	seek(BINREAD, $offset, 0);
		#read(GIF, $buff, 40, 29084);
	read(BINREAD, $buff, $length);
	    print BINWRITE $buff;
	
	close(BINREAD);
	close(BINWRITE);
}

#just a directory wrapper helper function
sub do_dir {
	my $dir = shift;
	opendir(D, $dir);
	my @f = readdir(D);
	closedir(D);
	foreach my $file (@f) {
		my $filename = $dir . '/' . $file;
		if ($file eq '.' || $file eq '..') {
		} elsif (-d $filename) {
			do_dir($filename); 
		} else {
			print "$filename\n";
			$ImageCount++;
			get_exif_gps($filename);
		} 
	}
}

