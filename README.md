GifRec
======
Record your screen into a animated GIF. 

This program is publicly available under the MIT License. See [LICENSE](http://opensource.org/licenses/MIT) for more details.

http://www.gifrec.uni.me/

[Example GIF recorded from a youtube video](http://www.gifrec.uni.me/i/ZWPLW.gif)

About
=====

GifRec is an app, created in C#, which is for your desktop that allows users to record an area of their screen and turns it into an animated GIF. Created GIFs are automatically uploaded to the web but this can be disabled in options.

Features
========

- Record areas of your scren and convert into a GIF
- Auto resizing and low file sizes
- Upload to the web for easy sharing
- Easy to use
 
Currently, to record a GIF for say, 5 seconds, that is configured in the options then records for that amount in time. In future versions, users will be able to START/STOP recording at their own will instead of a fixed duration.

Compiling
=====

When you compile this app, you will need to head over to [Uploaders.cs](https://github.com/ben-shepherd/GifRec/blob/master/src/Uploaders.cs) and edit the FTP Url (where the file will be stored), Web Url (Url to output), Ftp username and Ftp password.

Known bugs
=========

None 
