
using System;
using System.Device.Gpio;
using System.Device.Pwm;
using System.Threading;
using SantaPuppet;

//Dimming an LED
//https://toscode.gitee.com/inlazy/iot/blob/master/Documentation/raspi-pwm.md
//nano /boot/config.txt Add this dtoverlay=pwm-2chan
//GPIO 18 defaults to PWM chan0
//GPIO 19 defaults to PWM chan1

namespace SantaPuppet
{
    class Program
    {
     

        static void Main(string[] args)
        {
            Console.WriteLine("Santa Puppet is Running");
            Thread threadKeyLights =  new(Lights.FadeKeyLights);
            threadKeyLights.Start();
            Thread threadFootLights = new(Lights.FadeFootLights);
            threadFootLights.Start();

            var lightsThread5 = new Thread(() => Lights.BackChaseOnChaseOffLights(250));
            lightsThread5.Start();

            //Thread talk = new(Talk);
            //talk.Start();


        }



        static void Talk()
        {
            int pin = 21;

            var controller = new GpioController();
            controller.OpenPin(pin, PinMode.Output);
            bool status = true;

            while (true)
            {
                if (status)
                {
                    controller.Write(pin, PinValue.High);
                    status = false;
                    Thread.Sleep(100);
                }
                else
                {
                    controller.Write(pin, PinValue.Low);
                    status = true;
                    Thread.Sleep(1000);
                }
        
            }
        }
    }
}