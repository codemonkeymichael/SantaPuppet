
using System;
using System.Device.Gpio;
using System.Device.Pwm;
using System.Threading;

//Dimming an LED
//https://toscode.gitee.com/inlazy/iot/blob/master/Documentation/raspi-pwm.md
//nano /boot/config.txt Add this dtoverlay=pwm-2chan
//GPIO 18 defaults to PWM chan0
//GPIO 19 defaults to PWM chan1

namespace DimAnLed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello PWM!");
            Thread threadKeyLights = new(FadeKeyLights);
            threadKeyLights.Start();
            Thread threadFootLights = new(FadeFootLights);
            threadFootLights.Start();
            Thread threadBackChaseOneOnLights = new(BackChaseOnChaseOffLights);
            threadBackChaseOneOnLights.Start();
        }

        static void BackChaseOneOnLights()
        {
            //int red1 = 5;
            //int green1 = 6;
            //int blue1 = 7;
            //int yellow1 = 12;

            int[] lightArray = new int[] { 5, 6, 7, 12 };

            var controller = new GpioController();
            foreach (int la in lightArray)
            {
                controller.OpenPin(la, PinMode.Output);
            }

            int status = 0;

            while (true)
            {
                for (int i = 0; i < lightArray.Length; i++)
                {
                    //Turn them all off
                    foreach (int la in lightArray)
                    {
                        controller.Write(la, PinValue.Low);
                    }
                    //Turn One On
                    if (i == status)
                    {
                        controller.Write(lightArray[i], PinValue.High);
                    }
                    status++;
                    if (status > lightArray.Length) status = 0;
                    Thread.Sleep(200);
                }
            }
        }

        static async void BackChaseOnChaseOffLights()
        {
            //int red1 = 5;
            //int green1 = 6;
            //int blue1 = 7;
            //int yellow1 = 12;

            int[] lightArray = new int[] { 5, 6, 7, 12 };

            var controller = new GpioController();
            foreach(int la in lightArray)
            {
                controller.OpenPin(la, PinMode.Output);
            }

            int status = 0;

            while (true)
            {
                for(int i = 0; i < lightArray.Length; i++)  
                {
                    if (i == status)
                    {
                        controller.Write(lightArray[i], PinValue.High);
                    }
                    else
                    {
                        controller.Write(lightArray[i], PinValue.Low);
                    }
                    status++;
                    if (status > lightArray.Length) status = 0;
                    Thread.Sleep(100);
                }
              
            }
        }

        static async void FadeKeyLights()
        {

            var pwm0 = PwmChannel.Create(0, 1, 1000, 0.0);
            pwm0.Start();

            bool statusUp0 = true;
            double fadeStatus0 = 0.0;
            while (true)
            {
                if (statusUp0)
                {
                    fadeStatus0 = fadeStatus0 + 0.01;
                }
                else
                {
                    fadeStatus0 = fadeStatus0 - 0.01;
                }
                int sleepTime0 = 2; //How fast to fade 10 is slow 1 is fast
                if (fadeStatus0 > 0.99)
                {
                    statusUp0 = false;
                    sleepTime0 = 800;
                }
                if (fadeStatus0 < 0.01)
                {
                    statusUp0 = true;
                    sleepTime0 = 800;
                }

                pwm0.DutyCycle = fadeStatus0;

                Thread.Sleep(sleepTime0);
            }
        }

        static async void FadeFootLights()
        {

            var pwm1 = PwmChannel.Create(0, 0, 1000, 0.0);
            pwm1.Start();

            bool statusUp1 = true;
            double fadeStatus1 = 0.0;
            while (true)
            {
                if (statusUp1)
                {
                    fadeStatus1 = fadeStatus1 + 0.01;
                }
                else
                {
                    fadeStatus1 = fadeStatus1 - 0.01;
                }
                int sleepTime1 = 15; //How fast to fade 10 is slow 1 is fast
                if (fadeStatus1 > 0.99)
                {
                    statusUp1 = false;
                    sleepTime1 = 1500;
                }
                if (fadeStatus1 < 0.01)
                {
                    statusUp1 = true;
                    sleepTime1 = 1500;
                }

                pwm1.DutyCycle = fadeStatus1;

                Thread.Sleep(sleepTime1);
            }
        }
    }
}

//Console.WriteLine("Blinking LED. Press Ctrl+C to end.");
//int pin = 12;
//using var controller = new GpioController();
////controller.OpenPin(pin, PinMode.Output);
////bool status = true;

////System.Device.Pwm.Drivers.SoftwarePwmChannel buzzerPwmS = new System.Device.Pwm.Drivers.SoftwarePwmChannel(16, 400, 0.5, false, controller, false);
//System.Device.Pwm.PwmChannel buzzerPwmH = new System.Device.Pwm.Drivers.SoftwarePwmChannel(12, 400, 0.5, false, controller, false);
//var buzzer = new Iot.Device.Buzzer.Buzzer(buzzerPwmH);
//buzzer.StartPlaying(100);

//while (true)
//{
//    if (status)
//    {
//        controller.Write(pin, PinValue.High);
//        status = false;
//    }
//    else
//    {
//        controller.Write(pin, PinValue.Low);
//        status = true;
//    }

//    Thread.Sleep(500);
//}


//namespace Example.PWMLED
//{
//	class Program
//	{
//		static void Main(string[] args)
//		{
//			new Program().Run(args);
//		}

//		private void Run(string[] args)
//		{
//			Console.WriteLine("Press any key to exit...");

//			using (var driver = new I2cDriver(ProcessorPin.Pin2, ProcessorPin.Pin3))
//			{
//				// random object to add some spice to the output
//				Random rnd = new Random();

//				// device support
//				int i2cAddress = 0x40;
//				var device = new Pca9685Connection(driver.Connect(i2cAddress));
//				var pwmFrequency = Frequency.FromHertz(60);
//				device.SetPwmUpdateRate(pwmFrequency);

//				// task support
//				CancellationTokenSource cancelSource = new CancellationTokenSource();
//				List<Task> tasks = new List<Task>();

//				// create the channel(s) and start task(s)
//				PwmChannel[] pwmChannels = { PwmChannel.C0, }; // add more channels for more additional LEDs
//				foreach (var channel in pwmChannels.Distinct())
//				{
//					// pass random sleep ms to have LEDs at different fade speeds
//					Task task = Task.Run(() => RunLed(device, channel, rnd.Next(20, 41), cancelSource.Token));
//				}

//				// loop until key pressed
//				while (!Console.KeyAvailable)
//				{
//					Thread.Sleep(250);
//				}

//				// cancel task(s) and wait
//				cancelSource.Cancel();
//				Task.WaitAll(tasks.ToArray());
//			}
//		}

//		private void RunLed(Pca9685Connection device, PwmChannel channel, int sleepMs, CancellationToken cancelToken)
//		{
//			// PMW ticks range from 0 to 4095
//			int increment = 100;
//			int startCycleTick = 0;
//			device.SetPwm(channel, 0, 0);
//			while (!cancelToken.IsCancellationRequested)
//			{
//				// up we go
//				for (int endCycleTick = 100; endCycleTick < 4100; endCycleTick += increment)
//				{
//					device.SetPwm(channel, startCycleTick, endCycleTick);
//					Thread.Sleep(sleepMs);
//				}

//				// after we've gone up, show kindness and see if we're done
//				if (cancelToken.IsCancellationRequested) break;

//				// and down we go
//				for (int endCycleTick = 4095; endCycleTick > 0; endCycleTick -= increment)
//				{
//					device.SetPwm(channel, startCycleTick, endCycleTick);
//					Thread.Sleep(sleepMs);
//				}
//			}
//		}
//	}
//}

