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
        

        //mcp23017.WriteByte(Register.IODIR, 0x00, Port.PortA);
        //mcp23017.WriteByte(Register.IODIR, 0x00, Port.PortB);

    }


    public static void Main(string[] args)
    {
        Console.WriteLine("Santa Puppet is Running");

        Inputs.OpenPins(_piGPIOController);
        Lights.OpenPins(_piGPIOController);
        Motors.OpenPins(_piGPIOController, _mcp20GPIOController);
      

   

      

        //AnimationCues thing = new AnimationCues();
        //thing.TestI2c();

        //CurtinCues cur = new CurtinCues(_controller);
        //cur.OpenClose(false, 2);

        Songs.ItsTheMostWonderfulTimeOfTheYear song01 = new Songs.ItsTheMostWonderfulTimeOfTheYear(_piGPIOController, _mcp20GPIOController);
 

        Audio.CueSong(song01.cueStack());

        //Blink the play btn
        LightCues lc = new LightCues(_piGPIOController);
        Thread blinkPlayBtn = new Thread(() => lc.PlayBtnBlink());
        blinkPlayBtn.Start();

        //var stats8 = PinValue.Low;
        //var stats9 = PinValue.Low;


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

            //if (_controller.Read(Inputs.CurtinStageLeftStopOpen) != stats8)
            //{
            //    stats8 = _controller.Read(Inputs.CurtinStageLeftStopOpen);
            //    Console.WriteLine("Input Pin 8 Pushed = " + stats8);
            //    Thread.Sleep(2500);
            //}
            //if (_controller.Read(Inputs.CurtinStageRightStopClosed) != stats9)
            //{
            //    stats9 = _controller.Read(Inputs.CurtinStageRightStopClosed);
            //    Console.WriteLine("Input Pin 9 Pushed = " + stats9);
            //    Thread.Sleep(2500);
            //}


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
