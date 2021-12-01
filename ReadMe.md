Merry Christmas Tim
I wrote code for you for Christmas.

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

8.Curtin Close Stop - Input
9. Curtin Open Stop - Input

10. Play Btn LED Green

Maybe if there''s time

11. (Backdrop Side Lights)
14. (Backbrop Up Lights)
15. (Side Light Red)
17. (Side Light Green)
27. (Side Light Blue Could Be for Talking)

22. Motor Curtin
23. Motor Curtin
24. Motor Curtin
25. Motor Curtin

18. LED1 Key Lights (PWM Dimmable Channel 0)

19. LED2 Foot Lights (PWM Dimmable Channel 1)

Backlight Truss

5. LED6 Back Left Red
6. LED7 Back Left Green
7. LED8 Back Left Blue
12. LED9 Back Left Yellow
13. LED10 Back Right Red
16. LED11 Back Right Green
20. LED12 Back Right Blue
21. LED13 Back Right Yellow

26. Play button 

MCP23017 Port Expander

1. Motor1 Feet
2. Motor1 Feet
3. Motor1 Feet
4. Motor1 Feet

5. Motor2 Left Arm
6. Motor2 Left Arm
7. Motor2 Left Arm
8. Motor2 Left Arm

9. Motor3 Right Arm
10. Motor3 Right Arm
11. Motor3 Right Arm
12. Motor3 Right Arm

13. Motor4 Shoulders
14. Motor4 Shoulders
15. Motor4 Shoulders
16. Motor4 Shoulders
