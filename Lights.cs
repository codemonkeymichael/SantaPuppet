using System;
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
        private static int[] allLights = new int[] { 5, 6, 7, 12, 13, 16, 20, 21 };
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
            foreach (int light in allLights)
            {
                controller.OpenPin(light, PinMode.Output);
            }
        }


        public void BackStrobeAll(object duration)
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

        public void BLackOut()
        {
            foreach (int light in allLights)
            {
                controller.Write(light, PinValue.Low);
            }

            var pwm0 = PwmChannel.Create(0, 0, 400, 0.0);
            pwm0.Start();

            var pwm1 = PwmChannel.Create(0, 1, 400, 0.0);
            pwm1.Start();
        }

        public void StrobeAll(object duration)
        {
            int d = Convert.ToInt32(duration);
            foreach (int light in allLights)
            {
                controller.Write(light, PinValue.High);
            }

            Thread.Sleep(d);

            foreach (int light in allLights)
            {
                controller.Write(light, PinValue.Low);
            }
        }

        public void BackChaseOneOff(object speed, object repeat, object bounce, object split)
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

        public void BackChaseOneOn(object speed, object repeat, object bounce, object split)
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
                        if (h && i == 3)  break;
                    }
                    Array.Reverse(backLights);
                }
            }
        }

        public void BackChaseOnChaseOff(object speed, object repeat, object bounce, object split)
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
        /// Down stage lights are controlled by PWM and as such can be dimmed.
        /// </summary>
        /// <param name="speed">int 1 is fast, 15 is slow</param>
        /// <param name="up">bool true = fade up brighter, false = fade down dimmer</param>
        /// <param name="keyLights">bool true = lights at the top, false = foot lamps</param>
        /// <param name="max">double 1.0 = full on when fading up, 0.5 is half </param>
        /// <param name="min">double 0.0 = is off, 0.5 is half </param>
        /// <param name="start">double 0.0 = is off, 0.5 is half </param>
        public void DownStageLights(object speed, object up, object keyLights, object max, object min, object start)
        {
            int s = Convert.ToInt32(speed);
            bool u = Convert.ToBoolean(up);
            bool k = Convert.ToBoolean(keyLights);
            double mx = Convert.ToDouble(max);
            double mn = Convert.ToDouble(min);
            double st = Convert.ToDouble(start);

            int keyLites = 1;
            if (k) keyLites = 0;
            Console.WriteLine("DownStageLights speed=" + s.ToString() + 
                " up=" + u.ToString() + 
                " key=" + keyLites.ToString() +
                " mx=" + mx +
                " mn=" + mn + 
                " st=" + st);

            var pwm = PwmChannel.Create(0, keyLites, 400, st);
            pwm.Start();

            double fadeStatus = 0.0;

            while (true)
            {
                if (u)
                {
                    fadeStatus = fadeStatus + 0.001;
                }
                else
                {
                    fadeStatus = fadeStatus - 0.001;
                }

                int sleepTime0 = s; //How fast to fade 10 is slow 1 is fast

                if (fadeStatus > mx)
                {
                    Console.WriteLine("fadeStatus = " + fadeStatus + " mx=" + mx);
                    break;
                }

                pwm.DutyCycle = fadeStatus;
                Thread.Sleep(s);
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
