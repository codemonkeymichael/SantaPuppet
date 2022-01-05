namespace SantaPuppet.Cues;

public class AnimationCues
{
    private static GpioController _mcp20GPIOController;
    private static GpioController _piGPIOController;
    private static int[] _shouldersMotor;


    public AnimationCues(GpioController piGPIOController, GpioController mcp20GPIOController)
    {
        //Console.WriteLine("Animation Cues Constructors");
        _mcp20GPIOController = mcp20GPIOController;
        _piGPIOController = piGPIOController;
        _shouldersMotor = Motors.shouldersMotor;
    }



    public void Twist(bool right = true, int speed = 10 )
    {
        Console.WriteLine("Test MCP23017 I2C");

        int step = 0; //Four steps 0 1 2 3
        int max = 380;
        int maxCounter = 0;

        if (right) Array.Reverse(_shouldersMotor);

        while (true)
        {
            if (maxCounter < max)
            {
                if (_piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 5) //max < 5 let it off the input stop 
                {
                    int motorSteps = step;
                    _mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.High);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    _mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    _mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    _mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
                    step++;
                    if (step > 3) step = 0;
                    Thread.Sleep(speed);
                    maxCounter++;
                }
                else
                {
                    Console.WriteLine("Santa Twist Stop Hit");
                    //Back it up one step so it's not be shorting the stop input pin.
                    //step--;
                    //if (step < 0) step = 4;
                    //int motorSteps = step;
                    //_mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.High);
                    //motorSteps++;
                    //if (motorSteps > 3) motorSteps = 0;
                    //_mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
                    //motorSteps++;
                    //if (motorSteps > 3) motorSteps = 0;
                    //_mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
                    //motorSteps++;
                    //if (motorSteps > 3) motorSteps = 0;
                    //_mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
                    break;
                }
            }
            else
            {
                Console.WriteLine("Santa Twist Max Counter Stop. Something went wrong with the stop input.");
                break;
            }
        }

       
        foreach (var motor in _shouldersMotor)
        {           
            _mcp20GPIOController.Write(motor, PinValue.Low);
        }
        if (right) Array.Reverse(_shouldersMotor);


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

