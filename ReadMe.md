After you publish run this to push it to the raspberry pi

Use publish to build project for the pi

Use FileZilla to copy files into the PI
Host: sftp://192.168.1.15
Username: pi
Password: Sydney9-25
Port: 22

TODO: Put the FTP in a post publish script

To remote into the pi go to a cmd prompt:
ssh pi@192.168.1.15
password: Sydney9-25

to run this on the pi 
dotnet /home/pi/SantaPuppet/SantaPuppet.dll

Kill The App
List Running Apps to get PID
ps -ef
To Kill It
kill -9 <PID>
