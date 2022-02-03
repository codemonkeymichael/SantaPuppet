This project is a Christmas gift for my brother in law Tim. He is a super cool guy. Also I'm having a lot of fun working on this.     
      
It’s my first real raspberry pi project. I started it in October 2021 and clearly the scope was too large to finish by that December so it will now be his 2022 gift.    
     
The project will be a Santa Clause marionet that will sing and dance to classic Christmas songs. It will also include a light show. I’m hand building every part of it.    
    
(The plan is… not all the code has been written yet 2/3/2022)    
The following code controls 6 stepper motors and 16 channels of lights. It will host a blazor web site to control some of the feature. Features like a Santa Cuckoo Clock that will go off every hour counting down the days until Christmas and playing a song. I’m starting with 10 songs and will add more as time permits. 
    
The code is C# .NET6.  
    

Notes:  

GPIOs   
https://cdn.sparkfun.com/assets/learn_tutorials/4/2/4/header_pinout.jpg    
   

GPIO-14. Play Btn Feedback LED Green     
     
GPIO-15. Talking (black wire) ???????

GPIO-18. LED1 Key Lights (PWM Dimmable Channel 0) (blue wire)  

GPIO-23. Motor Right Arm (red wire)   
GPIO-24. Motor Right Arm (green wire)    
GPIO-25. Motor Right Arm (blue wire)    
GPIO-8. Motor Right Arm (yellow wire)   
   
GPIO-7. Motor Shoulders (red wire)   
GPIO-12. Motor Shoulders (green wire)   
GPIO-16. Motor Shoulders (blue wire)        
GPIO-20. Motor Shoulders (yellow wire)     
    
GPIO-21. Motor Curtin (red wire)       
GPIO-26. Motor Curtin (green wire) 

GPIO-19. LED2 Foot Lights (PWM Dimmable Channel 1) (yellow wire) 

GPIO-13. Motor Curtin (blue wire)   
GPIO-6. Motor Curtin (yellow wire)   
    
GPIO-5. Motor Feet (red wire)   
GPIO-11. Motor Feet (green wire)    
GPIO-9. Motor Feet (blue wire)     
GPIO-10. Motor Feet (yellow wire)    
    
GPIO-22. Motor2 Left Arm (red wire)   
GPIO-27. Motor2 Left Arm (green wire)    
GPIO-17. Motor2 Left Arm (blue wire)      
GPIO-4. Motor2 Left Arm (yellow wire)   

GPIO-1. -----
GPIO-0. -----
     
   
MCP23017 Port Expander 0x20    
https://raspi.tv/wp-content/uploads/2013/07/MCP23017.jpg    
https://github.com/dotnet/iot/tree/main/src/devices/Mcp23xxx    
    
GPIO-A0. Back Left Red LED    
GPIO-A1. Back Left Green LED      
GPIO-A2. Back Left Blue LED     
GPIO-A3. Back Left Yellow LED      
      
GPIO-A4. Back Right Red LED     
GPIO-A5. Back Right Green LED    
GPIO-A6. Back Right Blue LED    
GPIO-A7. Back Right Yellow LED      
     
Inputs:
https://www.cl.cam.ac.uk/projects/raspberrypi/tutorials/robot/buttons_and_switches/  
  
GPIO-B0. Left Arm Stops (red wire)       
GPIO-B1. Right Arm Stops (white wire)     
GPIO-B2. Feet Stops (blue Red wire)     
GPIO-B3. Twist Stops (black wire)       
      
GPIO-B4. Curtin Stage Left Stop (Open) (yellow wire)        
GPIO-B5. Curtin Stage Right Stop (Close) (green wire)      
GPIO-B6. Play button (blue wire)    
    
GPIO-B7. Play Btn Feedback LED Red    
   
    
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
https://code.videolan.org/videolan/LibVLCSharp
https://code.videolan.org/videolan/LibVLCSharp/-/blob/3.x/docs/linux-setup.md
