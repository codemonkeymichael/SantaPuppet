using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet
{
    public class Cuttin
    {
        static void AroundStrong(int[] motor, GpioController controller)
        {

            int[] step1 = new int[4] { 21, 22, 23, 24 };
            int[] step2 = new int[4] { 22, 23, 24, 21 };
            int[] step3 = new int[4] { 23, 24, 21, 22 };
            int[] step4 = new int[4] { 24, 21, 22, 23 };
            int[][] steps = new int[][] { step1, step2, step3, step4 };


            while (true)
            {
                //Set the values
                foreach (var step in steps)
                {
                    //Console.WriteLine($" High {step[0]} {step[1]} Low {step[2]} {step[3]}");

                    controller.Write(step[0], PinValue.High);
                    controller.Write(step[1], PinValue.High);
                    controller.Write(step[2], PinValue.Low);
                    controller.Write(step[3], PinValue.Low);

                    Thread.Sleep(2);

                }
            }
        }

    }
}
