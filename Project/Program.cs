using LibVLCSharp.Shared;

namespace SantaPuppet;

class Program
{
    public static bool songPlaying = false;
    public static GpioController piGPIOController = new GpioController();
    private static I2cConnectionSettings connectionSettings = new I2cConnectionSettings(1, 0x20);
    private static I2cDevice device = I2cDevice.Create(connectionSettings);
    private static Mcp23017 mcp23017 = new Mcp23017(device);
    public static GpioController mcp20GPIOController = new GpioController(PinNumberingScheme.Logical, mcp23017);

    public Program()
    {
        Console.WriteLine("Program constructor.");
    }


    public static void Main(string[] args)
    {
        Console.WriteLine("Santa Puppet is Running");

        Inputs.OpenPins();
        Lights.OpenPins();
        //Motors.OpenPins(_piGPIOController);

        LightCues lites = new LightCues();
        lites.Back_Color(LightCues.Color.Random, true, 1250);

        Songs.ItsTheMostWonderfulTimeOfTheYear song01 = new Songs.ItsTheMostWonderfulTimeOfTheYear();

        Audio.CueSong(song01.cueStack());

        //Blink the play btn
        LightCues lc = new LightCues();
        Thread blinkPlayBtn = new Thread(() => lc.PlayBtnBlink());
        blinkPlayBtn.Start();

        while (true)
        {
            //    //Console.WriteLine("Play Button Status " + _mcp20GPIOController.Read(Inputs.PlayButton));
            //    //Thread.Sleep(1000);

            if (I2CJobQueue.EnqueueInputCheck(Inputs.PlayButton) == PinValue.High)
            {
                if (songPlaying)
                {
                    //Stop a song
                    songPlaying = false;
                    //lc.PlayBtnGreen();
                    Audio.StopSong();
                    //Wait for playbutton to be released
                    while (I2CJobQueue.EnqueueInputCheck(Inputs.PlayButton) == PinValue.High)
                    {
                        Thread.Sleep(25);
                    }
                    //Don't respond to a button push for a couple seconds
                    Thread.Sleep(2500);
                }
                else
                {
                    Console.WriteLine("Button pushed play song");
                    songPlaying = true;

                    Audio.PlaySong();

                    //Wait for playbutton to be released
                    while (I2CJobQueue.EnqueueInputCheck(Inputs.PlayButton) == PinValue.High)
                    {
                        Thread.Sleep(25);
                    }
                    //Don't respond to a button push for a couple seconds
                    Thread.Sleep(2500);

                }
            }


        }

    }

    static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {
        //TODO: Make this work
        //Close a pins and stop playing music
        Console.WriteLine("I quit!");
    }
}