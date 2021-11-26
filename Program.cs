
using System;
using System.Device.Gpio;
using System.Device.Pwm;
using System.Threading;
using SantaPuppet;

namespace SantaPuppet
{
    class Program
    {
     

        static void Main(string[] args)
        {
            Console.WriteLine("Santa Puppet is Running");

            Songs.ItsTheMostWonderfulTimeOfTheYear song = new Songs.ItsTheMostWonderfulTimeOfTheYear();
            Thread t = new(() => Audio.PlaySong(song.songData()));
            t.Name = "Song";
            t.Start();


            //Thread threadKeyLights =  new(Lights.FadeKeyLights);
            //threadKeyLights.Start();
            //Thread threadFootLights = new(Lights.FadeFootLights);
            //threadFootLights.Start();

            //var lightsThread5 = new Thread(() => Lights.BackChaseOnChaseOffLights(250));
            //lightsThread5.Start();      
            //Lights thing = new Lights();
            //thing.DownStageLights(9,true,true);

            //var pwm0 = PwmChannel.Create(0, 1, 10, 0.01); //Channel 1 = GPIO 19
            //pwm0.Start();

            //var pwm1 = PwmChannel.Create(0, 0, 10, 0.5); //Channel 0 = GPIO 18
            //pwm1.Start();

            ////GpioController controller = new GpioController();
            ////controller.ClosePin(18);
            ////controller.Write(18, PinValue.High);


            Console.ReadKey();
        }



       
    }
}