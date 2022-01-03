namespace SantaPuppet.Cues;

public class AnimationCues
{
    private static GpioController _controller;
    public static int[] _shouldersMotor;

    public AnimationCues(GpioController controller)
    {
        //Console.WriteLine("Animation Cues Constructors");
        _controller = controller;
    }



    public void TurnLeft()
    {
        Console.WriteLine("Test MCP23017 I2C");

        I2cConnectionSettings connectionSettings = new I2cConnectionSettings(1, 0x20);
        I2cDevice device = I2cDevice.Create(connectionSettings);
        Mcp23017 mcp23017 = new Mcp23017(device);

        mcp23017.WriteByte(Register.IODIR, 0x00, Port.PortA);
        mcp23017.WriteByte(Register.IODIR, 0x00, Port.PortB);

        var controller = new GpioController(PinNumberingScheme.Logical, mcp23017);

        for (int i = 0; i < 16; i++)
        {
            controller.OpenPin(i, PinMode.Output, PinValue.Low);
            //controller.SetPinMode(i, PinMode.Output);
        }

        int step = 0; //Four steps 0 1 2 3
        int max = 50;
        int maxCounter = 0;

        while (true)
        {
            if (maxCounter < max)
            {
                int motorSteps = step;
                controller.Write(Motors.shouldersMotor[motorSteps], PinValue.High);
                motorSteps++;
                if (motorSteps > 3) motorSteps = 0;
                controller.Write(Motors.shouldersMotor[motorSteps], PinValue.Low);
                motorSteps++;
                if (motorSteps > 3) motorSteps = 0;
                controller.Write(Motors.shouldersMotor[motorSteps], PinValue.Low);
                motorSteps++;
                if (motorSteps > 3) motorSteps = 0;
                controller.Write(Motors.shouldersMotor[motorSteps], PinValue.Low);
                step++;
                if (step > 3) step = 0;
                Thread.Sleep(20);
                maxCounter++;
            }
            else
            {
                break;
            }
        }

        //bool status = true;

        //while (true)
        //{
        //    if (status)
        //    {
        //        for (int i = 0; i < 16; i++)
        //        {
        //            controller.Write(i, PinValue.High);
        //            Task.Delay(20).Wait();
        //        }
        //        Console.WriteLine("On");
        //        for (int i = 0; i < 16; i++)
        //        {
        //            Console.WriteLine("Pin" + i + " " + controller.Read(i) +
        //                " Mode=" + controller.GetPinMode(i) +
        //                " PinCount=" + controller.PinCount +
        //                " IsPinModeSupported.Output=" + controller.IsPinModeSupported(i, PinMode.Output));
        //        }
        //        status = false;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < 16; i++)
        //        {
        //            controller.Write(i, PinValue.Low);
        //            Task.Delay(250).Wait();
        //        }
        //        Console.WriteLine("Off");
        //        for (int i = 0; i < 16; i++)
        //        {
        //            Console.WriteLine("Pin" + i + " " + controller.Read(i));
        //        }
        //        status = true;
        //    }
        //}
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

