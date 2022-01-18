namespace SantaPuppet.Cues;

public class AnimationCues
{
    private static GpioController _mcp20GPIOController;
    private static GpioController _piGPIOController;
    private static int[] _shouldersMotor;
    private static int[] _feetMotor;
    private static int[] _leftArmMotor;
    private static int[] _rightArmMotor;


    public AnimationCues(GpioController piGPIOController, GpioController mcp20GPIOController)
    {
        //Console.WriteLine("Animation Cues Constructors");
        _mcp20GPIOController = mcp20GPIOController;
        _piGPIOController = piGPIOController;
        _shouldersMotor = Motors.shouldersMotor;
        _feetMotor = Motors.feetMotor;
        _leftArmMotor = Motors.leftArmMotor;
        _rightArmMotor = Motors.rightArmMotor;

    }

    public void TwistCenter()
    {
        //Go all utill we hit the stop
        int step = 0; //Four steps 0 1 2 3
        int max = 380; //362 is the whole range of motion
        int maxCounter = 0;
        int stepsToCenter = 250;

        while (true)
        {
            if (maxCounter < max)
            {

                if (_piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
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
                    Thread.Sleep(3);
                    maxCounter++;
                }
                else
                {
                    Console.WriteLine("Santa Twist Stop Hit");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Santa Twist Max Counter Stop. Something went wrong with the stop input.");
                break;
            }
        }

        //Then back to center
        Array.Reverse(_shouldersMotor);
        for (int i = 0; i < stepsToCenter; i++)
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
            Thread.Sleep(3);
            maxCounter++;
        }
        Array.Reverse(_shouldersMotor);
        foreach (var motor in _shouldersMotor)
        {
            _mcp20GPIOController.Write(motor, PinValue.Low);
        }
    }

    public void TwistBackForth(int repeat = 6, int steps = 200, int speed = 2)
    {
        bool direction = false;

        for (int i = 0; i < repeat; i++)
        {
            Console.WriteLine("Twist Back and Forth direction=" + direction);
            Twist(direction, steps, speed);
            direction = !direction;
            Thread.Sleep(steps * speed + 2);
        }
    }

    public void Twist(bool right = true, int steps = 380, int speed = 10)
    {
        Console.WriteLine("Twist");

        int step = 0; //Four steps 0 1 2 3
        int max = 380; //362 is the whole range of motion
        int maxCounter = 0;

        if (right) Array.Reverse(_shouldersMotor);

        while (true)
        {
            if (maxCounter < max && maxCounter < steps)
            {

                if (_piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
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

    public void Feet(bool right = true, int steps = 380, int speed = 10)
    {
        Console.WriteLine("Feet");

        int step = 0; //Four steps 0 1 2 3
        int max = 200; //362 is the whole range of motion
        int maxCounter = 0;

        if (right) Array.Reverse(_feetMotor);

        while (true)
        {
            if (maxCounter < max && maxCounter < steps)
            {

                //if (_piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
                //{
                int motorSteps = step;
                    _mcp20GPIOController.Write(_feetMotor[motorSteps], PinValue.High);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    _mcp20GPIOController.Write(_feetMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    _mcp20GPIOController.Write(_feetMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    _mcp20GPIOController.Write(_feetMotor[motorSteps], PinValue.Low);
                    step++;
                    if (step > 3) step = 0;
                    Thread.Sleep(speed);
                    maxCounter++;
                //}
                //else
                //{
                //    Console.WriteLine("Santa Twist Stop Hit");
                //    break;
                //}
            }
            else
            {
                Console.WriteLine("Santa Feet Max Counter Stop. Something went wrong with the stop input.");
                break;
            }
        }


        foreach (var motor in _feetMotor)
        {
            _mcp20GPIOController.Write(motor, PinValue.Low);
        }
        if (right) Array.Reverse(_feetMotor);


    }

    public void LeftArm(bool right = true, int steps = 380, int speed = 10)
    {
        Console.WriteLine("LeftArm");

        int step = 0; //Four steps 0 1 2 3
        int max = 200; //362 is the whole range of motion
        int maxCounter = 0;

        if (right) Array.Reverse(_leftArmMotor);

        while (true)
        {
            if (maxCounter < max && maxCounter < steps)
            {

                //if (_piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
                //{
                int motorSteps = step;
                _mcp20GPIOController.Write(_leftArmMotor[motorSteps], PinValue.High);
                motorSteps++;
                if (motorSteps > 3) motorSteps = 0;
                _mcp20GPIOController.Write(_leftArmMotor[motorSteps], PinValue.Low);
                motorSteps++;
                if (motorSteps > 3) motorSteps = 0;
                _mcp20GPIOController.Write(_leftArmMotor[motorSteps], PinValue.Low);
                motorSteps++;
                if (motorSteps > 3) motorSteps = 0;
                _mcp20GPIOController.Write(_leftArmMotor[motorSteps], PinValue.Low);
                step++;
                if (step > 3) step = 0;
                Thread.Sleep(speed);
                maxCounter++;
                //}
                //else
                //{
                //    Console.WriteLine("Santa Twist Stop Hit");
                //    break;
                //}
            }
            else
            {
                Console.WriteLine("Santa Left Arm Max Counter Stop. Something went wrong with the stop input.");
                break;
            }
        }


        foreach (var motor in _leftArmMotor)
        {
            _mcp20GPIOController.Write(motor, PinValue.Low);
        }
        if (right) Array.Reverse(_leftArmMotor);


    }

    public void RightArm(bool right = true, int steps = 380, int speed = 10)
    {
        Console.WriteLine("RightArm");

        int step = 0; //Four steps 0 1 2 3
        int max = 200; //362 is the whole range of motion
        int maxCounter = 0;

        if (right) Array.Reverse(_rightArmMotor);

        while (true)
        {
            if (maxCounter < max && maxCounter < steps)
            {

                //if (_piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
                //{
                int motorSteps = step;
                _mcp20GPIOController.Write(_rightArmMotor[motorSteps], PinValue.High);
                motorSteps++;
                if (motorSteps > 3) motorSteps = 0;
                _mcp20GPIOController.Write(_rightArmMotor[motorSteps], PinValue.Low);
                motorSteps++;
                if (motorSteps > 3) motorSteps = 0;
                _mcp20GPIOController.Write(_rightArmMotor[motorSteps], PinValue.Low);
                motorSteps++;
                if (motorSteps > 3) motorSteps = 0;
                _mcp20GPIOController.Write(_rightArmMotor[motorSteps], PinValue.Low);
                step++;
                if (step > 3) step = 0;
                Thread.Sleep(speed);
                maxCounter++;
                //}
                //else
                //{
                //    Console.WriteLine("Santa Twist Stop Hit");
                //    break;
                //}
            }
            else
            {
                Console.WriteLine("Santa Right Arm Max Counter Stop. Something went wrong with the stop input.");
                break;
            }
        }


        foreach (var motor in _rightArmMotor)
        {
            _mcp20GPIOController.Write(motor, PinValue.Low);
        }
        if (right) Array.Reverse(_rightArmMotor);


    }

}

