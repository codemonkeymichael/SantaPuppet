using Iot.Device.Mcp23xxx;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.I2c;
using System.Device.Pwm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SantaPuppet
{
    public class Lights
    {
        private static GpioController controller = new GpioController();
        private static int keyLights = 18;
        private static int footLights = 19;
        private static int[] backLights = new int[] { 5, 6, 7, 12, 13, 16, 20, 21 };

        //red1 = 5;
        //green1 = 6;
        //blue1 = 7;
        //yellow1 = 12;
        //red2 = 13;
        //green2 = 16;
        //blue2 = 20;
        //yellow2 = 21;
        //
        //Dimming an LED
        //https://github.com/dotnet/iot/blob/main/Documentation/raspi-pwm.md
        //nano /boot/config.txt Add this dtoverlay=pwm-2chan,pin=18,func=2,pin2=19,func2=2
        //GPIO 18 defaults to PWM chan0
        //GPIO 19 defaults to PWM chan1

        public Lights()
        {
            Console.WriteLine("Lights Controller");
            foreach (int light in backLights)
            {
                controller.OpenPin(light, PinMode.Output);
            }
        }

        public void TestI2c()
        {
            Console.WriteLine("Test MCP23017 I2C");

            var connectionSettingsx20 = new I2cConnectionSettings(1, 0x20);
            var i2cDevicex20 = I2cDevice.Create(connectionSettingsx20);          
            var mcp23017x20 = new Mcp23017(i2cDevicex20);

            mcp23017x20.WriteByte(Register.IODIR, 0b0000_0000, Port.PortA);
            mcp23017x20.WriteByte(Register.IODIR, 0b0000_0000, Port.PortB);
 

            var controller = new GpioController(PinNumberingScheme.Logical, mcp23017x20);


            for (int i = 0; i < 15; i++)
            {
                controller.OpenPin(i, PinMode.Output, PinValue.Low);
                controller.SetPinMode(i, PinMode.Output);

            }

            bool status = true;

            while (true)
            {
                if (status)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        controller.Write(i, PinValue.High);          
                        Task.Delay(250).Wait();
                    }
                    Console.WriteLine("On");
                    for (int i = 0; i < 15; i++)
                    {
                        Console.WriteLine("Pin" + i + " " + controller.Read(i) + 
                            " Mode=" + controller.GetPinMode(i) + 
                            " PinCount=" + controller.PinCount +
                            " IsPinModeSupported.Output=" + controller.IsPinModeSupported(i,PinMode.Output));                       
                    }
                    status = false;                 
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        controller.Write(i, PinValue.Low);
                        Task.Delay(250).Wait();
                    }
                    Console.WriteLine("Off");
                    for (int i = 0; i < 15; i++)
                    {
                        Console.WriteLine("Pin" + i + " " + controller.Read(i));
                    }
                    status = true;                   
                }               
            }
        }

        public void BLackOut()
        {
            foreach (int light in backLights)
            {
                controller.Write(light, PinValue.Low);
            }
            DownStage(0, false, true, 0.0, 0.0, 0.0);
            DownStage(0, false, false, 0.0, 0.0, 0.0);
        }

        public void Back_StrobeAll_Fast()
        {
            Back_StrobeAll(300);
        }
        public void Back_StrobeAll_Slow()
        {
            Back_StrobeAll(600);
        }
        public void Back_StrobeAll(int duration)
        {         
            foreach (int light in backLights)
            {
                controller.Write(light, PinValue.High);
            }

            Thread.Sleep(duration);

            foreach (int light in backLights)
            {
                controller.Write(light, PinValue.Low);
            }
        }

        public void Back_StrobeRandom_Fast_NoSplit(int repeat)
        {
            Back_StrobeRandom(125, repeat, false);
        }
        public void Back_StrobeRandom_Slow_NoSplit(int repeat)
        {
            Back_StrobeRandom(350, repeat, false);
        }
        public void Back_StrobeRandom_Fast_Split(int repeat)
        {
            Back_StrobeRandom(150, repeat, true);
        }
        public void Back_StrobeRandom_Slow_Split(int repeat)
        {
            Back_StrobeRandom(400, repeat, true);
        }
        public void Back_StrobeRandom(int duration, int repeat, bool split)
        {
            var rand = new Random();   

            int addUpSleepTime = 0;

            for (int n = 0; n < repeat; n++)
            {
                if (split)
                {
                    //Left Half
                    var left = rand.Next(0, 4);
                    var right = rand.Next(5, 8);
                    controller.Write(backLights[left], PinValue.High);
                    addUpSleepTime += duration / 2;
                    Thread.Sleep(duration / 2);
                    controller.Write(backLights[right], PinValue.High);
                    addUpSleepTime += duration / 2;
                    Thread.Sleep(duration / 2);
                    controller.Write(backLights[left], PinValue.Low);
                    addUpSleepTime += duration / 2;
                    Thread.Sleep(duration / 2);
                    controller.Write(backLights[right], PinValue.Low);
                }
                else
                {
                    var light = rand.Next(0, 8);
                    controller.Write(backLights[light], PinValue.High);
                    addUpSleepTime += duration;
                    Thread.Sleep(duration);
                    controller.Write(backLights[light], PinValue.Low);
                }
                //Console.WriteLine("Total Loop Duration = " + addUpSleepTime);
            }
        }


        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Fast_NoBoun_NoSplit(int repeat)
        {
            Back_1Off(85, repeat, false, false);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Slow_NoBoun_NoSplit(int repeat)
        {
            Back_1Off(110, repeat, false, false);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Fast_Bounce_NoSplit(int repeat)
        {
            Back_1Off(85, repeat, true, false);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Slow_Bounce_NoSplit(int repeat)
        {
            Back_1Off(110, repeat, true, false);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Fast_NoBounce_Split(int repeat)
        {
            Back_1Off(85, repeat, false, true);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off__Slow_NoBoun_Split(int repeat)
        {
            Back_1Off(110, repeat, false, true);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Fast_Boun_Split(int repeat)
        {
            Back_1Off(85, repeat, true, true);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Slow_Bounce_Split(int repeat)
        {
            Back_1Off(110, repeat, true, true);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off(int speed, int repeat, bool bounce, bool split)
        { 
            //Turn them all On
            foreach (int la in backLights)
            {
                controller.Write(la, PinValue.High);
            }

            for (int n = 0; n < repeat; n++)
            {
                for (int i = 0; i < backLights.Count() - 1; i++)
                {
                    controller.Write(backLights[i], PinValue.Low);
                    if (split) controller.Write(backLights[i + 4], PinValue.Low);
                    Thread.Sleep(speed);
                    controller.Write(backLights[i], PinValue.High);
                    if (split) controller.Write(backLights[i + 4], PinValue.High);
                    if (split && i == 3) break;
                }
                if (bounce)
                {
                    Array.Reverse(backLights);
                    for (int i = 0; i < backLights.Count() - 1; i++)
                    {
                        controller.Write(backLights[i], PinValue.Low);
                        if (split) controller.Write(backLights[i + 4], PinValue.Low);
                        Thread.Sleep(speed);
                        controller.Write(backLights[i], PinValue.High);
                        if (split) controller.Write(backLights[i + 4], PinValue.High);
                        if (split && i == 3) break;
                    }
                    Array.Reverse(backLights);
                }
            }

            //Turn them all Off
            foreach (int la in backLights)
            {
                controller.Write(la, PinValue.Low);
            }
        }


        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Fast_NoBoun_NoSplit(int repeat)
        {
            Back_1LOn(75, repeat, false, false);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Slow_NoBoun_NoSplit(int repeat)
        {
            Back_1LOn(110, repeat, false, false);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Fast_Bounce_NoSplit(int repeat)
        {
            Back_1LOn(75, repeat, true, false);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Slow_Bounce_NoSplit(int repeat)
        {
            Back_1LOn(110, repeat, true, false);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Fast_NoBoun_Split(int repeat)
        {
            Back_1LOn(75, repeat, false, true);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Slow_NoBoun_Split(int repeat)
        {
            Back_1LOn(110, repeat, false, true);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Fast_Bounce_Split(int repeat)
        {
            Back_1LOn(75, repeat, true, true);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Slow_Bounce_Split(int repeat)
        {
            Back_1LOn(110, repeat, true, true);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn(int speed, int repeat, bool bounce, bool split)
        {  
            for (int n = 0; n < repeat; n++)
            {
                for (int i = 0; i < backLights.Count() - 1; i++)
                {
                    controller.Write(backLights[i], PinValue.High);
                    //Console.WriteLine("Count Left=" + i.ToString() + "  Count Right=" + (i + 4).ToString());
                    if (split) controller.Write(backLights[i + 4], PinValue.High);
                    Thread.Sleep(speed);
                    controller.Write(backLights[i], PinValue.Low);
                    if (split) controller.Write(backLights[i + 4], PinValue.Low);
                    if (split && i == 3) break;
                }
                if (bounce)
                {
                    Array.Reverse(backLights);
                    for (int i = 0; i < backLights.Count() - 1; i++)
                    {
                        controller.Write(backLights[i], PinValue.High);
                        //Console.WriteLine("Count Left=" + i.ToString() + "  Count Right=" + (i + 4).ToString());
                        if (split) controller.Write(backLights[i + 4], PinValue.High);
                        Thread.Sleep(speed);
                        controller.Write(backLights[i], PinValue.Low);
                        if (split) controller.Write(backLights[i + 4], PinValue.Low);
                        if (split && i == 3) break;
                    }
                    Array.Reverse(backLights);
                }
            }
        }


        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Fast_NoBoun_NoSplit(int repeat)
        {
            Back_OnOf(70, repeat, false, false);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Slow_NoBoun_NoSplit(int repeat)
        {
            Back_OnOf(105, repeat, false, false);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Fast_Bounce_NoSplit(int repeat)
        {
            Back_OnOf(70, repeat, true, false);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Slow_Bounce_NoSplit(int repeat)
        {
            Back_OnOf(70, repeat, true, false);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Fast_NoBoun_Split(int repeat)
        {
            Back_OnOf(70, repeat, false, true);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Slow_NoBoun_Split(int repeat)
        {
            Back_OnOf(70, repeat, false, true);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Fast_Bounce_Split(int repeat)
        {
            Back_OnOf(70, repeat, true, true);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Slow_Bounce_Split(int repeat)
        {
            Back_OnOf(70, repeat, true, true);
        }

        /// <summary>
        /// Backligts all turn on one by one then turn off one by one
        /// </summary>
        /// <param name="speed">The time in millaseconds between each light coming one and going off 20 is kinda fast</param>
        /// <param name="repeat">How many times should it run</param>
        /// <param name="bounce">true = is left then right, false = just left</param>
        /// <param name="split">left 4 and the right 4 chase seprately, false = all 8 light chace together</param>
        public void Back_OnOf(int speed, int repeat, bool bounce, bool split)
        {       
            var addUpSleepTime = 0;

            for (int n = 0; n < repeat; n++)
            {
                for (int i = 0; i < backLights.Count() - 1; i++)
                {
                    controller.Write(backLights[i], PinValue.High);
                    if (split) controller.Write(backLights[i + 4], PinValue.High);
                    addUpSleepTime += speed;
                    Thread.Sleep(speed);
                    if (split && i == 3) break;
                }
                for (int i = 0; i < backLights.Count() - 1; i++)
                {
                    controller.Write(backLights[i], PinValue.Low);
                    if (split) controller.Write(backLights[i + 4], PinValue.Low);
                    addUpSleepTime += speed;
                    Thread.Sleep(speed);
                    if (split && i == 3) break;
                }
                if (bounce)
                {
                    Array.Reverse(backLights);
                    for (int i = 0; i < backLights.Count() - 1; i++)
                    {
                        controller.Write(backLights[i], PinValue.High);
                        if (split) controller.Write(backLights[i + 4], PinValue.High);
                        addUpSleepTime += speed;
                        Thread.Sleep(speed);
                        if (split && i == 3) break;
                    }
                    for (int i = 0; i < backLights.Count() - 1; i++)
                    {
                        controller.Write(backLights[i], PinValue.Low);
                        if (split) controller.Write(backLights[i + 4], PinValue.Low);
                        addUpSleepTime += speed;
                        Thread.Sleep(speed);
                        if (split && i == 3) break;
                    }
                    Array.Reverse(backLights);

                }
                //Console.WriteLine("Total Sleep Per Repeat = " + addUpSleepTime);
            }

        }


        public void Back_Color_Red()
        {
            Back_Color("Red", false, 0);
        }
        public void Back_Color_Green()
        {
            Back_Color("Green", false, 0);
        }
        public void Back_Color_Blue()
        {
            Back_Color("Blue", false, 0);
        }
        public void Back_Color_Yellow()
        {
            Back_Color("Yellow", false, 0);
        }
        public void Back_Color_Randon()
        {
            Back_Color("Random", false, 0);
        }
        public void Back_Color_Black()
        {
            Back_Color("Black", false, 0);
        }
        public void Back_Color_Red_Dur(int duration)
        {
            Back_Color("Red", false, duration);
        }
        public void Back_Color_Green_Dur(int duration)
        {
            Back_Color("Green", false, duration);
        }
        public void Back_Color_Blue_Dur(int duration)
        {
            Back_Color("Blue", false, duration);
        }
        public void Back_Color_Yellow_Dur(int duration)
        {
            Back_Color("Yellow", false, duration);
        }

        /// <summary>
        /// Backligts turn on one color.
        /// </summary>
        /// <param name="color">Red, Green, Blue, Yellow, Black, Random</param>
        /// <param name="clear">true = Turn off all backligts before turning on the color. </param>
        /// <param name="duration">0 = leave them on </param>
        public void Back_Color(string color, bool clear, int duration)
        {       
            //Console.WriteLine("Back_Color - Color = " + c);
            //Console.WriteLine("Back_Color - Clear = " + clr);

            if (color == "Black" || clear)
            {
                foreach (var light in backLights)
                {
                    controller.Write(light, PinValue.Low);
                }
            }

            if (color == "Random")
            {
                var random = new Random();
                var list = new List<string> { "Red", "Green", "Blue", "Yellow" };
                int index = random.Next(list.Count);
                color = list[index];
            }
            switch (color)
            {
                case "Red":
                    controller.Write(backLights[0], PinValue.High);
                    controller.Write(backLights[4], PinValue.High);
                    break;
                case "Green":
                    controller.Write(backLights[1], PinValue.High);
                    controller.Write(backLights[5], PinValue.High);
                    break;
                case "Blue":
                    controller.Write(backLights[2], PinValue.High);
                    controller.Write(backLights[6], PinValue.High);
                    break;
                case "Yellow":
                    controller.Write(backLights[3], PinValue.High);
                    controller.Write(backLights[7], PinValue.High);
                    break;
            }
            if (duration > 0)
            {
                Thread.Sleep(duration);
                switch (color)
                {
                    case "Red":
                        controller.Write(backLights[0], PinValue.Low);
                        controller.Write(backLights[4], PinValue.Low);
                        break;
                    case "Green":
                        controller.Write(backLights[1], PinValue.Low);
                        controller.Write(backLights[5], PinValue.Low);
                        break;
                    case "Blue":
                        controller.Write(backLights[2], PinValue.Low);
                        controller.Write(backLights[6], PinValue.Low);
                        break;
                    case "Yellow":
                        controller.Write(backLights[3], PinValue.Low);
                        controller.Write(backLights[7], PinValue.Low);
                        break;
                }
            }
        }



        /// <summary>
        /// Down stage lights are controlled by PWM and as such can be dimmed.
        /// </summary>
        /// <param name="speed">int 1 is fast, 15 is slow</param>
        /// <param name="up">bool true = fade up brighter, false = fade down dimmer</param>
        /// <param name="keyLights">bool true = lights at the top, false = foot lamps</param>
        /// <param name="max">double 1.0 = full on when fading up, 0.5 is half </param>
        /// <param name="min">double 0.0 = is off, 0.5 is half </param>
        /// <param name="start">double 0.0 = is off, 0.5 is half </param>
        public void DownStage(int speed, bool up, bool keyLights, double max, double min, double start)
        {
            int keyLites = 1;
            if (keyLights) keyLites = 0;
            //Console.WriteLine("DownStageLights speed=" + s.ToString() + 
            //    " up=" + u.ToString() + 
            //    " key=" + keyLites.ToString() +
            //    " mx=" + mx +
            //    " mn=" + mn + 
            //    " st=" + st);

            var pwm = PwmChannel.Create(0, keyLites, 400, start);
            pwm.Start();

            if (speed > 0)
            {
                double fadeStatus = start;
                while (true)
                {
                    if (up)
                    {
                        fadeStatus = fadeStatus + 0.001;
                        //Console.WriteLine("Up fadeStatus=" + fadeStatus);
                    }
                    else
                    {
                        fadeStatus = fadeStatus - 0.001;
                        //Console.WriteLine("Down fadeStatus=" + fadeStatus);
                    }            
                    if (fadeStatus > max && up) break;
                    if (fadeStatus < min && !up) break;

                    pwm.DutyCycle = fadeStatus;
                    Thread.Sleep(speed);
                }
            }
        }

    }
}
