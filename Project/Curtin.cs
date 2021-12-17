﻿using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet
{
    public class Curtin
    {
        private static GpioController controller = new GpioController();
        private static int[] curtinMotor = new int[4] { 22, 23, 24, 25 };
        private const int maxSteps = 20000;
        private static int maxStepCounter { get; set; }

        public Curtin()
        {
            Console.WriteLine("Lights Controller");
            foreach (int motor in curtinMotor)
            {
                controller.OpenPin(motor, PinMode.Output);
            }
            controller.OpenPin(9, PinMode.Input);
        }


        public void OpenClose(bool open = true, int speed = 2)
        {


            if (open) Array.Reverse(curtinMotor);
            maxStepCounter = 0;
            int step = 0; //Four steps 0 1 2 3
            //Console.WriteLine("1 Curtin maxStepCounter=" + maxStepCounter + " maxSteps=" + maxSteps + " speed=" + speed);
            while (true)
            {
                var positionStatus = controller.Read(9);                
                if (maxStepCounter < maxSteps && positionStatus == PinValue.Low)
                {
                            Console.WriteLine("2 Curtin Break maxStepCounter=" + maxStepCounter + " positionStatus=" + positionStatus + " speed=" + speed);
                    //        break;
                    //    }
                    //    else
                    //    {
                    int motorSteps = step;
                    controller.Write(curtinMotor[motorSteps], PinValue.High);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    controller.Write(curtinMotor[motorSteps], PinValue.High);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    controller.Write(curtinMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    controller.Write(curtinMotor[motorSteps], PinValue.Low);
                    step++;
                    if (step > 3) step = 0;
                    Thread.Sleep(speed);
                }
                else
                {                   
                    Console.WriteLine("Curtin over ran the max number of steps. Something must be wrong with the stop input GPIO.");

                    break;
                }
                maxStepCounter++;
        
            }
            if (open) Array.Reverse(curtinMotor);
            foreach (int motor in curtinMotor)
            {
                Console.WriteLine("Curtin Low.");
                controller.Write(motor, PinValue.Low);
            }
        }
    }
}
