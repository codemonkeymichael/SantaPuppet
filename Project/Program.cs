using LibVLCSharp.Shared;

namespace SantaPuppet;

public static class Program
{
    public static bool songPlaying = false;
    public static GpioController piGPIOController = new GpioController();
    private static I2cConnectionSettings connectionSettings = new I2cConnectionSettings(1, 0x20);
    private static I2cDevice device = I2cDevice.Create(connectionSettings);
    private static Mcp23017 mcp23017 = new Mcp23017(device);
    public static GpioController mcp20GPIOController = new GpioController(PinNumberingScheme.Logical, mcp23017);

    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Santa Puppet is Running");

        Inputs.OpenPins();
        Lights.OpenPins();
        Motors.OpenPins();

        Songs.ItsTheMostWonderfulTimeOfTheYear song01 = new Songs.ItsTheMostWonderfulTimeOfTheYear();

        Audio.CueSong(song01.cueStack());

        //Blink the play btn
        LightCues lc = new LightCues();
        Thread blinkPlayBtn = new Thread(() => lc.PlayBtnBlink());
        blinkPlayBtn.Start();

        int workingPos = 0;
        string[] working = { "|", "/", "-", @"\" };
        int loopDelay = 100;

        while (true)
        {
            if (I2CJobQueue.EnqueueInputCheck(Inputs.PlayButton) == PinValue.High)
            { 
                if (songPlaying)
                {
                    //Console.SetCursorPosition(1, 3);
                    //Console.ForegroundColor = ConsoleColor.Red;
                    //Console.WriteLine("Button pushed stop song        ");
                    //Stop a song
                    songPlaying = false;
      
                    Audio.StopSong();
                    //Wait for playbutton to be released
                    while (I2CJobQueue.EnqueueInputCheck(Inputs.PlayButton) == PinValue.High)
                    {
                        Thread.Sleep(25);
                    }
                    //Don't respond to a button push for a couple seconds
                    Thread.Sleep(2500);
                    loopDelay = 100;
                }
                else
                {
                    //Console.SetCursorPosition(1, 3);
                    //Console.ForegroundColor = ConsoleColor.Green;           
                    //Console.WriteLine("Button pushed play song        ");

                    songPlaying = true;

                    Audio.PlaySong();

                    //Wait for playbutton to be released
                    while (I2CJobQueue.EnqueueInputCheck(Inputs.PlayButton) == PinValue.High)
                    {
                        Thread.Sleep(25);
                    }
                    //Don't respond to a button push for a couple seconds
                    Thread.Sleep(2500);
                    loopDelay = 500;
                    //Console.SetCursorPosition(2, 4);
                    //Console.ForegroundColor = ConsoleColor.Green;
                    //Console.Write(new string(' ', Console.WindowWidth));

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

    }

    static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {
        //TODO: Make this work
        //Close a pins and stop playing music
        Console.WriteLine("I quit!");
    }
}