using LibVLCSharp.Shared;

namespace SantaPuppet;

class Program
{
    public static bool songPlaying = false;
    public static GpioController _piGPIOController = new GpioController();
    private static I2cConnectionSettings connectionSettings = new I2cConnectionSettings(1, 0x20);
    private static I2cDevice device = I2cDevice.Create(connectionSettings);
    private static Mcp23017 mcp23017 = new Mcp23017(device);
    public static GpioController _mcp20GPIOController = new GpioController(PinNumberingScheme.Logical, mcp23017);


    public Program()
    {
        Console.WriteLine("Program constructor.");
    }


    public static void Main(string[] args)
    {
        Console.WriteLine("Santa Puppet is Running");

        Inputs.OpenPins(_mcp20GPIOController);
        Lights.OpenPins(_piGPIOController, _mcp20GPIOController);
        Motors.OpenPins(_piGPIOController);


        Songs.ItsTheMostWonderfulTimeOfTheYear song01 = new Songs.ItsTheMostWonderfulTimeOfTheYear(_piGPIOController, _mcp20GPIOController);

        Audio.CueSong(song01.cueStack());

        //Blink the play btn
        LightCues lc = new LightCues(_piGPIOController);
        Thread blinkPlayBtn = new Thread(() => lc.PlayBtnBlink());
        blinkPlayBtn.Start();

        while (true)
        {

            if (_piGPIOController.Read(Inputs.PlayButton) == PinValue.High)
            {
                if (songPlaying)
                {
                    songPlaying = false;
                    //lc.PlayBtnGreen();
                    Audio.StopSong();
                    //Wait for playbutton to be released
                    while (_piGPIOController.Read(Inputs.PlayButton) == PinValue.High)
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
                    while (_piGPIOController.Read(Inputs.PlayButton) == PinValue.High)
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
