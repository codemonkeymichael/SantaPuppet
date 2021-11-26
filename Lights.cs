﻿using System;
using System.Collections.Generic;
using System.Device.Gpio;
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


        public void BLackOut()
        {
            foreach (int light in backLights)
            {
                controller.Write(light, PinValue.Low);
            }

            var pwm0 = PwmChannel.Create(0, 0, 400, 0.0);
            pwm0.Start();

            var pwm1 = PwmChannel.Create(0, 1, 400, 0.0);
            pwm1.Start();
        }

        public void Back_StrobeAll_Fast()
        {
            Back_StrobeAll(300);
        }
        public void Back_StrobeAll_Slow()
        {
            Back_StrobeAll(600);
        }
        public void Back_StrobeAll(object duration)
        {
            int d = Convert.ToInt32(duration);
            foreach (int light in backLights)
            {
                controller.Write(light, PinValue.High);
            }

            Thread.Sleep(d);

            foreach (int light in backLights)
            {
                controller.Write(light, PinValue.Low);
            }
        }

        public void Back_StrobeRandom_Fast_NoSplit(object repeat)
        {
            Back_StrobeRandom(125, repeat, false);
        }
        public void Back_StrobeRandom_Slow_NoSplit(object repeat)
        {
            Back_StrobeRandom(350, repeat, false);
        }
        public void Back_StrobeRandom_Fast_Split(object repeat)
        {
            Back_StrobeRandom(150, repeat, true);
        }
        public void Back_StrobeRandom_Slow_Split(object repeat)
        {
            Back_StrobeRandom(400, repeat, true);
        }

        public void Back_StrobeRandom(object duration, object repeat, object split)
        {
            var rand = new Random();

            int d = Convert.ToInt32(duration);
            int r = Convert.ToInt32(repeat);
            bool h = Convert.ToBoolean(split);

            for (int n = 0; n < r; n++)
            {
                if (h) {
                    //Left Half
                    var left = rand.Next(0,4);            
                    var right = rand.Next(5,8);
                    controller.Write(backLights[left], PinValue.High);
                    Thread.Sleep(d / 2);
                    controller.Write(backLights[right], PinValue.High);
                    Thread.Sleep(d / 2);
                    controller.Write(backLights[left], PinValue.Low);
                    Thread.Sleep(d / 2);
                    controller.Write(backLights[right], PinValue.Low);
                }
                else
                {
                    var light = rand.Next(0,8);
                    controller.Write(backLights[light], PinValue.High);
                    Thread.Sleep(d);
                    controller.Write(backLights[light], PinValue.Low);
                }
            }
        }


        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Fast_NoBoun_NoSplit(object repeat)
        {
            Back_1Off(85, repeat, false, false);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Slow_NoBoun_NoSplit(object repeat)
        {
            Back_1Off(110, repeat, false, false);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Fast_Bounce_NoSplit(object repeat)
        {
            Back_1Off(85, repeat, true, false);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Slow_Bounce_NoSplit(object repeat)
        {
            Back_1Off(110, repeat, true, false);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Fast_NoBounce_Split(object repeat)
        {
            Back_1Off(85, repeat, false, true);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off__Slow_NoBoun_Split(object repeat)
        {
            Back_1Off(110, repeat, false, true);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Fast_Boun_Split(object repeat)
        {
            Back_1Off(85, repeat, true, true);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off_Slow_Bounce_Split(object repeat)
        {
            Back_1Off(110, repeat, true, true);
        }
        /// <summary>
        /// One Off
        /// </summary>
        public void Back_1Off(object speed, object repeat, object bounce, object split)
        {
            int s = Convert.ToInt32(speed);
            int r = Convert.ToInt32(repeat);
            bool b = Convert.ToBoolean(bounce);
            bool h = Convert.ToBoolean(split);

            //Turn them all On
            foreach (int la in backLights)
            {
                controller.Write(la, PinValue.High);
            }

            for (int n = 0; n < r; n++)
            {
                for (int i = 0; i < backLights.Count() - 1; i++)
                {
                    controller.Write(backLights[i], PinValue.Low);
                    if (h) controller.Write(backLights[i + 4], PinValue.Low);
                    Thread.Sleep(s);
                    controller.Write(backLights[i], PinValue.High);
                    if (h) controller.Write(backLights[i + 4], PinValue.High);
                    if (h && i == 3) break;
                }
                if (b)
                {
                    Array.Reverse(backLights);
                    for (int i = 0; i < backLights.Count() - 1; i++)
                    {
                        controller.Write(backLights[i], PinValue.Low);
                        if (h) controller.Write(backLights[i + 4], PinValue.Low);
                        Thread.Sleep(s);
                        controller.Write(backLights[i], PinValue.High);
                        if (h) controller.Write(backLights[i + 4], PinValue.High);
                        if (h && i == 3) break;
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
        public void Back_1LOn_Fast_NoBoun_NoSplit(object repeat)
        {
            Back_1LOn(75, repeat, false, false);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Slow_NoBoun_NoSplit(object repeat)
        {
            Back_1LOn(110, repeat, false, false);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Fast_Bounce_NoSplit(object repeat)
        {
            Back_1LOn(75, repeat, true, false);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Slow_Bounce_NoSplit(object repeat)
        {
            Back_1LOn(110, repeat, true, false);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Fast_NoBoun_Split(object repeat)
        {
            Back_1LOn(75, repeat, false, true);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Slow_NoBoun_Split(object repeat)
        {
            Back_1LOn(110, repeat, false, true);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Fast_Bounce_Split(object repeat)
        {
            Back_1LOn(75, repeat, true, true);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn_Slow_Bounce_Split(object repeat)
        {
            Back_1LOn(110, repeat, true, true);
        }
        /// <summary>
        /// One On
        /// </summary>
        public void Back_1LOn(object speed, object repeat, object bounce, object split)
        {
            int s = Convert.ToInt32(speed);
            int r = Convert.ToInt32(repeat);
            bool b = Convert.ToBoolean(bounce);
            bool h = Convert.ToBoolean(split);

            for (int n = 0; n < r; n++)
            {
                for (int i = 0; i < backLights.Count() - 1; i++)
                {
                    controller.Write(backLights[i], PinValue.High);
                    //Console.WriteLine("Count Left=" + i.ToString() + "  Count Right=" + (i + 4).ToString());
                    if (h) controller.Write(backLights[i + 4], PinValue.High);
                    Thread.Sleep(s);
                    controller.Write(backLights[i], PinValue.Low);
                    if (h) controller.Write(backLights[i + 4], PinValue.Low);
                    if (h && i == 3) break;
                }
                if (b)
                {
                    Array.Reverse(backLights);
                    for (int i = 0; i < backLights.Count() - 1; i++)
                    {
                        controller.Write(backLights[i], PinValue.High);
                        //Console.WriteLine("Count Left=" + i.ToString() + "  Count Right=" + (i + 4).ToString());
                        if (h) controller.Write(backLights[i + 4], PinValue.High);
                        Thread.Sleep(s);
                        controller.Write(backLights[i], PinValue.Low);
                        if (h) controller.Write(backLights[i + 4], PinValue.Low);
                        if (h && i == 3) break;
                    }
                    Array.Reverse(backLights);
                }
            }
        }


        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Fast_NoBoun_NoSplit(object repeat)
        {
            Back_OnOf(70, repeat, false, false);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Slow_NoBoun_NoSplit(object repeat)
        {
            Back_OnOf(105, repeat, false, false);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Fast_Bounce_NoSplit(object repeat)
        {
            Back_OnOf(70, repeat, true, false);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Slow_Bounce_NoSplit(object repeat)
        {
            Back_OnOf(70, repeat, true, false);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Fast_NoBoun_Split(object repeat)
        {
            Back_OnOf(70, repeat, false, true);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Slow_NoBoun_Split(object repeat)
        {
            Back_OnOf(70, repeat, false, true);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Fast_Bounce_Split(object repeat)
        {
            Back_OnOf(70, repeat, true, true);
        }
        /// <summary>
        /// On Off
        /// </summary>
        public void Back_OnOf_Slow_Bounce_Split(object repeat)
        {
            Back_OnOf(70, repeat, true, true);
        }

        /// <summary>
        /// Backligts all turn on one by one then turn off one by one
        /// </summary>
        /// <param name="speed">int The time in millaseconds between each light coming one and going off 20 is kinda fast</param>
        /// <param name="repeat">int How many times should it run</param>
        /// <param name="bounce">bool true = is left then right, false = just left</param>
        /// <param name="split">bool true = left 4 and the right 4 chase seprately, false = all 8 light chace together</param>
        public void Back_OnOf(object speed, object repeat, object bounce, object split)
        {
            int s = Convert.ToInt32(speed);
            int r = Convert.ToInt32(repeat);
            bool b = Convert.ToBoolean(bounce);
            bool h = Convert.ToBoolean(split);

            for (int n = 0; n < r; n++)
            {
                for (int i = 0; i < backLights.Count() - 1; i++)
                {
                    controller.Write(backLights[i], PinValue.High);
                    if (h) controller.Write(backLights[i + 4], PinValue.High);
                    Thread.Sleep(s);
                    if (h && i == 3) break;
                }
                for (int i = 0; i < backLights.Count() - 1; i++)
                {
                    controller.Write(backLights[i], PinValue.Low);
                    if (h) controller.Write(backLights[i + 4], PinValue.Low);
                    Thread.Sleep(s);
                    if (h && i == 3) break;
                }
                if (b)
                {
                    Array.Reverse(backLights);
                    for (int i = 0; i < backLights.Count() - 1; i++)
                    {
                        controller.Write(backLights[i], PinValue.High);
                        if (h) controller.Write(backLights[i + 4], PinValue.High);
                        Thread.Sleep(s);
                        if (h && i == 3) break;
                    }
                    for (int i = 0; i < backLights.Count() - 1; i++)
                    {
                        controller.Write(backLights[i], PinValue.Low);
                        if (h) controller.Write(backLights[i + 4], PinValue.Low);
                        Thread.Sleep(s);
                        if (h && i == 3) break;
                    }
                    Array.Reverse(backLights);

                }
            }

        }

        /// <summary>
        /// Backligts turn on one color.
        /// </summary>
        /// <param name="color">Red, Green, Blue, Yellow, Random</param>
        /// <param name="clear">true = Turn off all backligts before turning on the color. </param>
        public void Back_Color(object color, object clear)
        {
            string c = Convert.ToString(color);
            bool clr = Convert.ToBoolean(clear);
            if (clr)
            {
                foreach (var light in backLights)
                {
                    controller.Write(light, PinValue.Low);
                }
            }
            if(c == "Random")
            {
                var random = new Random();
                var list = new List<string> { "Red", "Green", "Blue", "Yellow" };
                int index = random.Next(list.Count);
                c = list[index];
            }
            switch (c)
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
        public void DownStage(object speed, object up, object keyLights, object max, object min, object start)
        {
            int s = Convert.ToInt32(speed);
            bool u = Convert.ToBoolean(up);
            bool k = Convert.ToBoolean(keyLights);
            double mx = Convert.ToDouble(max);
            double mn = Convert.ToDouble(min);
            double st = Convert.ToDouble(start);

            int keyLites = 1;
            if (k) keyLites = 0;
            //Console.WriteLine("DownStageLights speed=" + s.ToString() + 
            //    " up=" + u.ToString() + 
            //    " key=" + keyLites.ToString() +
            //    " mx=" + mx +
            //    " mn=" + mn + 
            //    " st=" + st);

            var pwm = PwmChannel.Create(0, keyLites, 400, st);
            pwm.Start();

            if (s > 0)
            {
                double fadeStatus = st;
                while (true)
                {
                    if (u)
                    {
                        fadeStatus = fadeStatus + 0.001;
                        //Console.WriteLine("Up fadeStatus=" + fadeStatus);
                    }
                    else
                    {
                        fadeStatus = fadeStatus - 0.001;
                        //Console.WriteLine("Down fadeStatus=" + fadeStatus);
                    }
                    int sleepTime0 = s;
                    if (fadeStatus > mx && u) break;
                    if (fadeStatus < mn && !u) break;

                    pwm.DutyCycle = fadeStatus;
                    Thread.Sleep(s);
                }
            }
        }

        //public static void FadeKeyLights()
        //{

        //    var pwm0 = PwmChannel.Create(0, 1, 1000, 0.0);
        //    pwm0.Start();

        //    bool statusUp0 = true;
        //    double fadeStatus0 = 0.0;
        //    while (true)
        //    {
        //        if (statusUp0)
        //        {
        //            fadeStatus0 = fadeStatus0 + 0.001;
        //        }
        //        else
        //        {
        //            fadeStatus0 = fadeStatus0 - 0.001;
        //        }
        //        int sleepTime0 = 1; //How fast to fade 10 is slow 1 is fast
        //        if (fadeStatus0 > 0.99)
        //        {
        //            statusUp0 = false;
        //            sleepTime0 = 800;
        //        }
        //        if (fadeStatus0 < 0.001)
        //        {
        //            fadeStatus0 = 0.0;
        //            statusUp0 = true;
        //            sleepTime0 = 800;
        //        }

        //        pwm0.DutyCycle = fadeStatus0;

        //        Thread.Sleep(sleepTime0);
        //    }
        //}

    }
}
