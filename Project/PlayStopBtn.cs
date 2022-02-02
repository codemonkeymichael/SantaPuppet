using LibVLCSharp.Shared;

namespace SantaPuppet;

public static class PlayStopBtn
{

  

    public static void PlayStop()
    {

        Console.WriteLine("Play Stop Btn");


        Songs.ItsTheMostWonderfulTimeOfTheYear song01 = new Songs.ItsTheMostWonderfulTimeOfTheYear();

        Audio.CueSong(song01.cueStack());


        //Blink the play btn
        LightCues lc = new LightCues();
        Thread blinkPlayBtn = new Thread(() => lc.PlayBtnBlink());
        blinkPlayBtn.Start();

        //Turn On Footlights
        lc.DownStage(200, true, false, 1.0);

        //int workingPos = 0;
        //string[] working = { "|", "/", "-", @"\" };
        int loopDelay = 15;

        while (true)
        {

            if (I2CJobQueue.EnqueueInputCheck(Inputs.PlayButton) == PinValue.High)
            {
                if (Audio.player.IsPlaying)
                {
                    ////Console.SetCursorPosition(1, 3);
                    ////Console.ForegroundColor = ConsoleColor.Red;
                    ////Console.WriteLine("Button pushed stop song        ");
                    ////Stop a song
        

                    //Audio.StopSong();
                    ////Wait for playbutton to be released
                    //while (I2CJobQueue.EnqueueInputCheck(Inputs.PlayButton) == PinValue.High)
                    //{
                    //    Thread.Sleep(25);
                    //}
                    ////Don't respond to a button push for a couple seconds
                    //Thread.Sleep(2500);
                    //loopDelay = 100;
                }
                else
                {
                    //Console.SetCursorPosition(1, 3);
                    //Console.ForegroundColor = ConsoleColor.Green;           
                    //Console.WriteLine("Button pushed play song        ");

                    Audio.PlaySong();

                    ////Wait for playbutton to be released
                    //while (I2CJobQueue.EnqueueInputCheck(Inputs.PlayButton) == PinValue.High)
                    //{
                    //    Thread.Sleep(25);
                    //}
                    ////Don't respond to a button push for a couple seconds
                    //Thread.Sleep(2500);
                    //loopDelay = 500;
                    ////Console.SetCursorPosition(2, 4);
                    ////Console.ForegroundColor = ConsoleColor.Green;
                    ////Console.Write(new string(' ', Console.WindowWidth));

                    break;

                }
            }
            else
            {
                //Console.SetCursorPosition(1, 3);
                //Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.WriteLine("Button Monitor           " + working[workingPos]);
                //workingPos++; if (workingPos > 3) workingPos = 0;
            }

            Thread.Sleep(loopDelay);

        }
        Console.WriteLine("Out of Loop");
        Console.Read();

    }



    static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {
        //TODO: Make this work
        //Close a pins and stop playing music
        Console.WriteLine("I quit!");
    }
}