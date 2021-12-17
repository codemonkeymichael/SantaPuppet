
using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Device.Pwm;
using System.Threading;
using SantaPuppet;

namespace SantaPuppet
{
    class Program
    {

        //private const string I2C_CONTROLLER_NAME = "I2C1"; //specific to RPI2
        //private const byte PORT_EXPANDER_I2C_ADDRESS = 0x20; // 7-bit I2C address of the port expander
        //private const byte PORT_EXPANDER_IODIR_REGISTER_ADDRESS = 0x00; // IODIR register controls the direction of the GPIO on the port expander
        //private const byte PORT_EXPANDER_GPIO_REGISTER_ADDRESS = 0x09; // GPIO register is used to read the pins input
        //private const byte PORT_EXPANDER_OLAT_REGISTER_ADDRESS = 0x0A; // Output Latch register is used to set the pins output high/low
  



        //private I2cDevice i2cPortExpander;




        static void Main(string[] args)
        {
            Console.WriteLine("Santa Puppet is Running");

            Songs.ItsTheMostWonderfulTimeOfTheYear song = new Songs.ItsTheMostWonderfulTimeOfTheYear();
            Thread t = new(() => Audio.PlaySong(song.songData()));   
            t.Start();

            //var i2cSettings = new I2cConnectionSettings(0,PORT_EXPANDER_I2C_ADDRESS);
            //string deviceSelector = I2cDevice.GetDeviceSelector(I2C_CONTROLLER_NAME);
            //var i2cDeviceControllers = await DeviceInformation.FindAllAsync(deviceSelector);
            //i2cPortExpander = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, i2cSettings);



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


        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("I quit!");
        }



    }
}