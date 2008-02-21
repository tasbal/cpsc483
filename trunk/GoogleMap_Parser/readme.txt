Tim's GMapper

There should be two files:  mapper.pl and GMap.php.

  03.23.06
  Tim Vidas
  tvidas(ta)nucia(tod)unomaha(tod)edu

  This program takes a directory as input and creates a single xml file and one jpg thumbnail for each jpg in the input directory.  The output xml 
  (when combined with the php file distributed with this) will greate a Google Map from the the GPS data embedded in the jpgs.  Each marker will 
  have it's own numbered GMap icon and when clicked will display the thumbnail created by this script.  It is assumed that the images (and thumbnails)
  are placed in a ./images subdirectory.  The associated php file should be fairly straighforward and easy to modify.


Distribution
    GPL.  use it, share it, pass it along, just keep my name in it somewhere.  thanks.

Pre-reqs
  requires perl (duh) 
    -uses Image::ExifTool
    -uses File::Basename::Object (for windows CPAN compatibility)
  the input images must have GPS data (Nikon D200, Dx1, Dx2 and Ricoh Pro G3 are the only ones i know of at this time)
  you must sign up for a GMaps Key - it's free    http://www.google.com/apis/maps/signup.html
    -and you must edit the php file to reflect your key
    -if you want to use html instead of php just remove everything between the <?php and ?> tags - then it's html

Disclaimer
  Use are your own risk.  No llamas were injured in the making of this script.

  I've run this script on windows using activeperl, and only on jpgs created by a Nikon 200 Camera, YMMV - but please let me know if you have success
  with other cameras / OSes etc

To DO
    Works in North America Only - need to etract more GPS data to flips signs right - not planning on fixing this soon since gmaps only does NA right now
    Extract a bit more info to put into the XML - mainly just to give the thumbnail views a little more to go on - date / title / etc
    Argumentify the whole thing




How to make it work:
	Take pictures with a camera that embeds GPS into the Exif.
	Put all the pictures into a directory.
	install PERL
	install the Image::ExifTool and File::Basename::Ojbect  modules (I recommend using CPAN)
	run mapper.pl and specify the directory of images (this creates both thumbnails and a GMapNew.xml file)
	Obtain a Google Maps API key from http://www.google.com/apis/maps/signup.html
	Edit GMap.php to reflect your API key.
	Upload the GMap.php and GMapNew.xml files to the webserver in a directory that works with your API key
	Upload the images and thumbnails to a subdirectory called ./images
	point your browser to the location of GMap.php on your server
	if all works well, you should see your images all mapped out.
	email me so I can check out your map!

