
using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Device.Pwm;
using System.Threading;
using SantaPuppet;
using SantaPuppet.Models.Inputs;

namespace SantaPuppet
{
    class Program
    {
        public static bool songPlaying = false;
        public static GpioController gpioController = new GpioController();
        public static Inputs ins = new Inputs(gpioController);

        public Program()
        {
            //TODO: Open all the GPIO Pin Here not in classes
            //gpioController.OpenPin(26, PinMode.Input); //GPIO-26. Play button - Input

        }



        static void Main(string[] args)
        {
            Console.WriteLine("Santa Puppet is Running");
            Songs.ItsTheMostWonderfulTimeOfTheYear song01 = new Songs.ItsTheMostWonderfulTimeOfTheYear();

            //Thread songThread;

            while (true)
            {

                if (gpioController.Read(ins.PlayButton) == PinValue.High)
                {
                    if (songPlaying)
                    {
                        Audio.StopSong(song01.stop());
                        songPlaying = false;
                    }
                    else
                    {
                        songPlaying = true;
                        
                        //songThread = new(() => Audio.PlaySong(song.play()));
                        //songThread.Start();

                        Audio.PlaySong(song01.play());
                   
                    }
                }
            }
        }

        //TODO: Push to start and feedback


        //TODO: Push to stop and feedback

        //TODO: Play song and store it what has played




        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            //TODO: Make this work
            //Close a pins and stop playing music
            Console.WriteLine("I quit!");
        }



    }
}