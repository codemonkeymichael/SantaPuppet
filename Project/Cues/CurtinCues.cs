using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SantaPuppet.Models.Outputs;

namespace SantaPuppet.Cues
{
    public class CurtinCues
    {
        private static GpioController _controller;
        private static int[] _curtinMotor;
        private const int maxSteps = 20000;
        private static int maxStepCounter { get; set; }

        public CurtinCues(GpioController controller)
        {
            //Console.WriteLine("Curtin Cues Constructors");
            _curtinMotor = Motors.curtinMotor;
            _controller = controller;
        }


        public void OpenClose(bool open = true, int speed = 2)
        {
            var positionStatus = _controller.Read(8);
            Console.WriteLine("A pos Status:" + positionStatus);

            if (open) Array.Reverse(_curtinMotor);
            maxStepCounter = 0;
            int step = 0; //Four steps 0 1 2 3
            //Console.WriteLine("1 Curtin maxStepCounter=" + maxStepCounter + " maxSteps=" + maxSteps + " speed=" + speed);
            while (true)
            {
             
                if (maxStepCounter < maxSteps )
                {
                    if (_controller.Read(8) == PinValue.Low) {
                        //Console.WriteLine("2 Curtin Break maxStepCounter=" + maxStepCounter + " positionStatus=" + positionStatus + " speed=" + speed);

                        int motorSteps = step;
                        _controller.Write(_curtinMotor[motorSteps], PinValue.High);
                        Console.WriteLine("motorSteps=" + motorSteps + " curtinMotor[motorSteps]=" + _curtinMotor[motorSteps]);
                        motorSteps++;
                        if (motorSteps > 3) motorSteps = 0;
                        _controller.Write(_curtinMotor[motorSteps], PinValue.High);
                        motorSteps++;
                        if (motorSteps > 3) motorSteps = 0;
                        _controller.Write(_curtinMotor[motorSteps], PinValue.Low);
                        motorSteps++;
                        if (motorSteps > 3) motorSteps = 0;
                        _controller.Write(_curtinMotor[motorSteps], PinValue.Low);
                        step++;
                        if (step > 3) step = 0;
                        Thread.Sleep(speed);
                    }
                    else
                    {
                        Console.WriteLine("Curtin hit the stop.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Curtin over ran the max number of steps. Something must be wrong with the stop input GPIO.");
                    break;
                }
                maxStepCounter++;

            }
            if (open) Array.Reverse(_curtinMotor);
            foreach (int motor in _curtinMotor)
            {
                Console.WriteLine("Curtin Low.");
                _controller.Write(motor, PinValue.Low);
            }
        }
    }
}
