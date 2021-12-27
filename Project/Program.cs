﻿
using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Device.Pwm;
using System.Threading;
using LibVLCSharp.Shared;
using SantaPuppet;
using SantaPuppet.Cues;
using SantaPuppet.Models.Inputs;
using SantaPuppet.Models.Outputs;


namespace SantaPuppet
{
    class Program
    {
        public static bool songPlaying = false;
        public static GpioController _controller = new GpioController();


        public Program()
        {

        }

        //private static void OnPositionChanged(object? sender, MediaPlayerPositionChangedEventArgs e)
        //{
        //    Console.WriteLine(e.Position.ToString());
        //}


        static void Main(string[] args)
        {
            Console.WriteLine("Santa Puppet is Running");

            //Core.Initialize();

            //using (var libVLC = new LibVLC())
            //{
            //    var media = new Media(libVLC, "audio/07JingleBellRock.wav", FromType.FromPath);
            //    //await media.Parse(MediaParseOptions.ParseNetwork);
            //    using (var mp = new MediaPlayer(media))
            //    {
                    
            //        var r = mp.Play();
            //        mp.PositionChanged += OnPositionChanged;
              
            //        Console.ReadKey();
            //    }
            //}




            Lights.OpenPins(_controller);
            Motors.OpenPins(_controller);
            Inputs.OpenPins(_controller);


            Songs.ItsTheMostWonderfulTimeOfTheYear song01 = new Songs.ItsTheMostWonderfulTimeOfTheYear(_controller);

            //Blink the play btn
            LightCues lc = new LightCues(_controller);
            Thread blinkPlayBtn = new Thread(() => lc.PlayBtnBlink());
            blinkPlayBtn.Start();

            var stats8 = PinValue.Low;
            var stats9 = PinValue.Low;


            while (true)
            {

                if (_controller.Read(Inputs.PlayButton) == PinValue.High)
                {
                    if (songPlaying)
                    {                  
                        songPlaying = false;
                        //lc.PlayBtnGreen();
                        Audio.StopSong();
                        //Wait for playbutton to be released
                        while (_controller.Read(Inputs.PlayButton) == PinValue.High)
                        {
                            Thread.Sleep(25);
                        }
                        //Don't respond to a button push for a couple seconds
                        Thread.Sleep(2500);
                    }
                    else
                    {
                        songPlaying = true;
                        //lc.PlayBtnRed();
                        Audio.PlaySong(song01.play());
       
                        //Wait for playbutton to be released
                        while (_controller.Read(Inputs.PlayButton) == PinValue.High)
                        {
                            Thread.Sleep(25);
                        }
                        //Don't respond to a button push for a couple seconds
                        Thread.Sleep(2500);

                    }
                }
               
                if (_controller.Read(Inputs.CurtinStageLeftStopOpen) != stats8)
                {
                    stats8 = _controller.Read(Inputs.CurtinStageLeftStopOpen);
                    Console.WriteLine("Input Pin 8 Pushed = " + stats8);
                    Thread.Sleep(2500);
                }
                if (_controller.Read(Inputs.CurtinStageRightStopClosed) != stats9)
                {
                    stats9 = _controller.Read(Inputs.CurtinStageRightStopClosed);
                    Console.WriteLine("Input Pin 9 Pushed = " + stats9);
                    Thread.Sleep(2500);
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
}