﻿Merry Christmas Tim  
I wrote code for you for Christmas 2021. Nice right, just what you wanted.   
  
After you publish run this to push it to the raspberry pi  
  
Use publish to build project for the pi  
  
Use FileZilla to copy files into the PI  
Host: sftp://192.168.1.15  
Username: pi  
Password: Sydney9-25  
Port: 22  
   
To remote into the pi go to a cmd prompt:  
ssh pi@192.168.1.15  
password: Sydney9-25   
  
to run this on the pi   
dotnet /home/pi/SantaPuppet/SantaPuppet.dll  
   
Kill The App  
List Running Apps to get PID   
ps -ef  
To Kill It  
kill -9 PID   
  
  
GPIOs   
https://cdn.sparkfun.com/assets/learn_tutorials/4/2/4/header_pinout.jpg    
   
Inputs:
https://www.cl.cam.ac.uk/projects/raspberrypi/tutorials/robot/buttons_and_switches/
GPIO-4. Left Arm Stops (Red)  
GPIO-10. Right Arm Stops (Green)  
GPIO-11. Feet Stops (Blue)  
GPIO-17. Twist Stops (Black)   
GPIO-8. Curtin Stage Left Stop (Open) (Yellow) Need 10K  
GPIO-9. Curtin Stage Right Stop (Close) (Green) Need 10K  
GPIO-26. Play button - Input  
     
GPIO-27. Play Btn Feedback LED Green   
GPIO-15. Play Btn Feedback LED Red  
  
GPIO-14. Talking  
     
GPIO-22. Motor Curtin (QA Pass)  
GPIO-23. Motor Curtin (QA Pass)     
GPIO-24. Motor Curtin (QA Pass)   
GPIO-25. Motor Curtin (QA Pass)    
   
GPIO-18. LED1 Key Lights (PWM Dimmable Channel 0)  
GPIO-19. LED2 Foot Lights (PWM Dimmable Channel 1)  
   
Backlight Truss:  
GPIO-5. LED6 Back Left Red  (QA Pass)   
GPIO-6. LED7 Back Left Green  (QA Pass)   
GPIO-7. LED8 Back Left Blue  (QA Pass)   
GPIO-12. LED9 Back Left Yellow  (QA Pass)   
GPIO-13. LED10 Back Right Red  (QA Pass)   
GPIO-16. LED11 Back Right Green  (QA Pass)    
GPIO-20. LED12 Back Right Blue   (QA Pass)    
GPIO-21. LED13 Back Right Yellow  (QA Pass)   
  

MCP23017 Port Expander 0x20 
https://raspi.tv/wp-content/uploads/2013/07/MCP23017.jpg  

GPIO-A0. Motor1 Feet   
GPIO-A1. Motor1 Feet   
GPIO-A2. Motor1 Feet   
GPIO-A3. Motor1 Feet    
   
GPIO-A4. Motor2 Left Arm   
GPIO-A5. Motor2 Left Arm  
GPIO-A6. Motor2 Left Arm   
GPIO-A7. Motor2 Left Arm   
  
  
GPIO-B0. Motor3 Right Arm  
GPIO-B1. Motor3 Right Arm  
GPIO-B2. Motor3 Right Arm  
GPIO-B3. Motor3 Right Arm   
   
GPIO-B4. Motor4 Shoulders   
GPIO-B5. Motor4 Shoulders  
GPIO-B6. Motor4 Shoulders  
GPIO-B7. Motor4 Shoulders 


MCP23017 Port Expander 0x21   
https://raspi.tv/wp-content/uploads/2013/07/MCP23017.jpg   
  
GPIO-A0.  
GPIO-A1.   
GPIO-A2.   
GPIO-A3.   
   
GPIO-A4.   
GPIO-A5. 
GPIO-A6.   
GPIO-A7.   

  
GPIO-B0.  
GPIO-B1.   
GPIO-B2.   
GPIO-B3.   
   
GPIO-B4.    
GPIO-B5.  
GPIO-B6. 
GPIO-B7. 


Audio playback ref
https://github.com/mobiletechtracker/NetCoreAudio
https://code.videolan.org/videolan/LibVLCSharp
https://code.videolan.org/videolan/LibVLCSharp/-/blob/3.x/docs/linux-setup.md