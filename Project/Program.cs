using LibVLCSharp.Shared;

namespace SantaPuppet;

class Program
{
    public static bool songPlaying = false;
    public static GpioController _piGPIOController = new GpioController();
    private static I2cConnectionSettings connectionSettings = new I2cConnectionSettings(1, 0x20);
    private static I2cDevice device = I2cDevice.Create(connectionSettings);
    private static Mcp23017 mcp23017 = new Mcp23017(device);
    private static GpioController _mcp20GPIOController = new GpioController(PinNumberingScheme.Logical, mcp23017);
    public static Queue<QueueModel> queueList;



    public Program()
    {
        Console.WriteLine("Program constructor.");
        queueList = new Queue<QueueModel>();
    }


    public static void Main(string[] args)
    {
        Console.WriteLine("Santa Puppet is Running");

        //Inputs.OpenPins(_mcp20GPIOController);
        Lights.OpenPins(_piGPIOController, _mcp20GPIOController);
        //Motors.OpenPins(_piGPIOController);

        LightCues lites = new LightCues(_piGPIOController);
        lites.Back_Color(LightCues.Color.Random, true, 0);

        //Songs.ItsTheMostWonderfulTimeOfTheYear song01 = new Songs.ItsTheMostWonderfulTimeOfTheYear(_piGPIOController, _mcp20GPIOController);

        //Audio.CueSong(song01.cueStack());

        ////Blink the play btn
        //LightCues lc = new LightCues(_piGPIOController, _mcp20GPIOController);
        //Thread blinkPlayBtn = new Thread(() => lc.PlayBtnBlink());
        //blinkPlayBtn.Start();

        //while (true)
        //{
        //    //Console.WriteLine("Play Button Status " + _mcp20GPIOController.Read(Inputs.PlayButton));
        //    //Thread.Sleep(1000);

        //    if (_mcp20GPIOController.Read(Inputs.PlayButton) == PinValue.High)
        //    {
        //        if (songPlaying)
        //        {
        //            //Stop a song
        //            songPlaying = false;
        //            //lc.PlayBtnGreen();
        //            Audio.StopSong();
        //            //Wait for playbutton to be released
        //            while (_mcp20GPIOController.Read(Inputs.PlayButton) == PinValue.High)
        //            {
        //                Thread.Sleep(25);
        //            }
        //            //Don't respond to a button push for a couple seconds
        //            Thread.Sleep(2500);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Button pushed play song");
        //            songPlaying = true;

        //            Audio.PlaySong();

        //            //Wait for playbutton to be released
        //            while (_mcp20GPIOController.Read(Inputs.PlayButton) == PinValue.High)
        //            {
        //                Thread.Sleep(25);
        //            }
        //            //Don't respond to a button push for a couple seconds
        //            Thread.Sleep(2500);

        //        }
        //    }


        //}

    }

    static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {
        //TODO: Make this work
        //Close a pins and stop playing music
        Console.WriteLine("I quit!");
    }

    public static void RunI2CJobs()
    {
        //Console.WriteLine(_queueList.Count);
        while (queueList.Count > 0)
        {        
            var job = queueList.Dequeue();
            _mcp20GPIOController.Write(job.Pin, job.PinValue);
        }
    }


}
