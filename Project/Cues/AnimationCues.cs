namespace SantaPuppet.Cues;

public class AnimationCues
{
    private static GpioController _controller;


    public AnimationCues(GpioController controller)
    {
        //Console.WriteLine("Animation Cues Constructors");
        _controller = controller;
    }


    public void RightArm(int speed = 2)
    {

        //if (open) Array.Reverse(_curtinMotor);
        //maxStepCounter = 0;
        //int step = 0; //Four steps 0 1 2 3
        ////Console.WriteLine("1 Curtin maxStepCounter=" + maxStepCounter + " maxSteps=" + maxSteps + " speed=" + speed);
        //while (true)
        //{

        //    if (maxStepCounter < maxSteps )
        //    {
        //        if (open && _controller.Read(Inputs.CurtinStageLeftStopOpen) == PinValue.Low
        //            || 
        //            !open && _controller.Read(Inputs.CurtinStageRightStopClosed) == PinValue.Low)

        //           {
        //            //Console.WriteLine("2 Curtin Break maxStepCounter=" + maxStepCounter + " positionStatus=" + positionStatus + " speed=" + speed);

        //            int motorSteps = step;
        //            _controller.Write(_curtinMotor[motorSteps], PinValue.High);

        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            _controller.Write(_curtinMotor[motorSteps], PinValue.High);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            _controller.Write(_curtinMotor[motorSteps], PinValue.Low);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            _controller.Write(_curtinMotor[motorSteps], PinValue.Low);
        //            step++;
        //            if (step > 3) step = 0;
        //            Thread.Sleep(speed);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Curtin hit the stop. Open = " + open);
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Curtin over ran the max number of steps. Something must be wrong with the stop input GPIO.");
        //        break;
        //    }
        //    maxStepCounter++;

        //}
        //if (open) Array.Reverse(_curtinMotor);
        //foreach (int motor in _curtinMotor)
        //{
        //    //Console.WriteLine("Curtin Low.");
        //    _controller.Write(motor, PinValue.Low);
        //}
    }
}

