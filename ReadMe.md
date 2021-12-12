Merry Christmas Tim
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
kill -9 <PID>


GPIOs 
https://cdn.sparkfun.com/assets/learn_tutorials/4/2/4/header_pinout.jpg

GPIO-8. Curtin Stage Left Stop (Open and Close) - Input
GPIO9. Curtin Stage Right Stop (Open and Close) - Input

GPIO-10. Play Btn LED Green
GPIO-11. Play Btn LED Red
GPIO-26. Play button (Input)

Maybe if there''s time
GPIO-4. (Backdrop Up Lights)
GPIO-14. (Backbrop Up Lights)
GPIO-15. (Side Light Red)
GPIO-17. (Side Light Green)
GPIO-27. (Talking)

GPIO-22. Motor Curtin
GPIO-23. Motor Curtin
GPIO-24. Motor Curtin
GPIO-25. Motor Curtin

GPIO-18. LED1 Key Lights (PWM Dimmable Channel 0)
GPIO-19. LED2 Foot Lights (PWM Dimmable Channel 1)

Backlight Truss

GPIO-5. LED6 Back Left Red
GPIO-6. LED7 Back Left Green
GPIO-7. LED8 Back Left Blue
GPIO-12. LED9 Back Left Yellow
GPIO-13. LED10 Back Right Red
GPIO-16. LED11 Back Right Green
GPIO-20. LED12 Back Right Blue
GPIO-21. LED13 Back Right Yellow



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
