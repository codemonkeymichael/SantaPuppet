using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet
{
    public class Animation
    {
        public static void Dance(int[] motor)
        {
            var controller = new GpioController();

            int[] step1 = new int[4] { 21, 22, 23, 24 };
            int[] step2 = new int[4] { 22, 23, 24, 21 };
            int[] step3 = new int[4] { 23, 24, 21, 22 };
            int[] step4 = new int[4] { 24, 21, 22, 23 };
            int[][] steps = new int[][] { step1, step2, step3, step4 };

            int[] directionChange = new int[] { 75, 1, 1, 150, 1, 1, 75, 1, 1, 75, 1, 1, 150, 1, 1, 75 }; //how many steps in a direction
            int[] directionSpeed = new int[] { 2, 250, 250, 2, 250, 250, 2, 250, 250, 6, 250, 250, 6, 250, 250, 6 }; //ms per step direction
            int directionCount = 0;
            int drictionIndex = 0;       

            while (true)
            {
                directionCount++;
                //Set the values
                foreach (var step in steps)
                {
                    Console.WriteLine($" High {step[0]} {step[1]} Low {step[2]} {step[3]}");

                    controller.Write(step[0], PinValue.High);
                    controller.Write(step[1], PinValue.High);
                    controller.Write(step[2], PinValue.Low);
                    controller.Write(step[3], PinValue.Low);

                    Thread.Sleep(directionSpeed[drictionIndex]);

                }


                if (directionCount == directionChange[drictionIndex])
                {
                    Array.Reverse(steps);
                    directionCount = 0;

                    drictionIndex++;
                    if (drictionIndex > (directionChange.Length - 1))
                    {
                        drictionIndex = 0;
                    }
                }
            }
        }

    }
}
