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

        public static void BackChaseOneOffLights(object speed)
        {
            //int red1 = 5;
            //int green1 = 6;
            //int blue1 = 7;
            //int yellow1 = 12;
            int s = Convert.ToInt32(speed);
            int[] lightArray = new int[] { 5, 6, 7, 12 };

            var controller = new GpioController();
            foreach (int la in lightArray)
            {
                controller.OpenPin(la, PinMode.Output);
            }

            int status = 0;

            while (true)
            {
                for (int i = 0; i < lightArray.Length; i++)
                {
                    //Turn them all On
                    foreach (int la in lightArray)
                    {
                        controller.Write(la, PinValue.High);
                    }
                    //Turn One Off
                    if (i == status)
                    {
                        controller.Write(lightArray[i], PinValue.Low);
                    }
                    status++;
                    if (status > lightArray.Length) status = 0;
                    Thread.Sleep(s);
                }
            }
        }

        public static void BackChaseOneOnLights(object speed)
        {
            //int red1 = 5;
            //int green1 = 6;
            //int blue1 = 7;
            //int yellow1 = 12;
            int s = Convert.ToInt32(speed);
            int[] lightArray = new int[] { 5, 6, 7, 12 };

            var controller = new GpioController();
            foreach (int la in lightArray)
            {
                controller.OpenPin(la, PinMode.Output);
            }

            int status = 0;

            while (true)
            {
                for (int i = 0; i < lightArray.Length; i++)
                {
                    //Turn them all off
                    foreach (int la in lightArray)
                    {
                        controller.Write(la, PinValue.Low);
                    }
                    //Turn One On
                    if (i == status)
                    {
                        controller.Write(lightArray[i], PinValue.High);
                    }
                    status++;
                    if (status > lightArray.Length) status = 0;
                    Thread.Sleep(s);
                }
            }
        }

        public static void BackChaseOnChaseOffLights(object speed)
        {
            //int red1 = 5;
            //int green1 = 6;
            //int blue1 = 7;
            //int yellow1 = 12;
            int s = Convert.ToInt32(speed);

            int[] lightArray = new int[] { 5, 6, 7, 12 };

            var controller = new GpioController();
            foreach (int la in lightArray)
            {
                controller.OpenPin(la, PinMode.Output);
            }

            int status = 0;

            while (true)
            {
                for (int i = 0; i < lightArray.Length; i++)
                {
                    if (i == status)
                    {
                        controller.Write(lightArray[i], PinValue.High);
                    }
                    else
                    {
                        controller.Write(lightArray[i], PinValue.Low);
                    }
                    status++;
                    if (status > lightArray.Length) status = 0;
                    Thread.Sleep(s);
                }
            }
        }

        public static void FadeKeyLights()
        {

            var pwm0 = PwmChannel.Create(0, 1, 1000, 0.0);
            pwm0.Start();

            bool statusUp0 = true;
            double fadeStatus0 = 0.0;
            while (true)
            {
                if (statusUp0)
                {
                    fadeStatus0 = fadeStatus0 + 0.001;
                }
                else
                {
                    fadeStatus0 = fadeStatus0 - 0.001;
                }
                int sleepTime0 = 1; //How fast to fade 10 is slow 1 is fast
                if (fadeStatus0 > 0.99)
                {
                    statusUp0 = false;
                    sleepTime0 = 800;
                }
                if (fadeStatus0 < 0.001)
                {
                    fadeStatus0 = 0.0;
                    statusUp0 = true;
                    sleepTime0 = 800;
                }

                pwm0.DutyCycle = fadeStatus0;

                Thread.Sleep(sleepTime0);
            }
        }

        public static void FadeFootLights()
        {

            var pwm1 = PwmChannel.Create(0, 0, 1000, 0.0);
            pwm1.Start();

            bool statusUp1 = true;
            double fadeStatus1 = 0.0;
            while (true)
            {
                if (statusUp1)
                {
                    fadeStatus1 = fadeStatus1 + 0.001;
                }
                else
                {
                    fadeStatus1 = fadeStatus1 - 0.001;
                }
                int sleepTime1 = 8; //How fast to fade 10 is slow 1 is fast
                if (fadeStatus1 > 0.999)
                {
                    statusUp1 = false;
                    sleepTime1 = 1500;
                }
                if (fadeStatus1 < 0.001)
                {
                    fadeStatus1 = 0.0;
                    statusUp1 = true;
                    sleepTime1 = 1500;
                }

                pwm1.DutyCycle = fadeStatus1;

                Thread.Sleep(sleepTime1);
            }
        }
    }
}
