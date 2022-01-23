using System.Collections;

namespace SantaPuppet.Cues;

public class AnimationCues
{
    private static GpioController _mcp20GPIOController;
    private static GpioController _piGPIOController;
    private static int[] _shouldersMotor;
    private static int[] _feetMotor;
    private static int[] _leftArmMotor;
    private static int[] _rightArmMotor;
    //private static ThreadQueue _queue; 
    private static Queue<QueueModel> _queueList;



    public AnimationCues(GpioController piGPIOController, GpioController mcp20GPIOController)
    {
        Console.WriteLine("Animation Cues Constructors");
        _mcp20GPIOController = mcp20GPIOController;
        _piGPIOController = piGPIOController; //Inputs for the stops
        _shouldersMotor = Motors.shouldersMotor;
        _feetMotor = Motors.feetMotor;
        _leftArmMotor = Motors.leftArmMotor;
        _rightArmMotor = Motors.rightArmMotor;
   
        _queueList = new Queue<QueueModel>();

    }

   private void RunJobs()
    {
        //Console.WriteLine(_queueList.Count);
        while (_queueList.Count > 0)
        {    
            var job = _queueList.Dequeue();
            
            if (job.FullStep)
            {
                int motorSteps = job.CurrentPosition;
                motorSteps--;    
                if (motorSteps < 0) motorSteps = 3;
                _mcp20GPIOController.Write(job.PinArray[motorSteps], PinValue.Low);
            }
            else
            {
                _mcp20GPIOController.Write(job.PinArray[job.CurrentPosition], PinValue.High);
            }
            //Thread.Sleep(1);
        }
    }

    private void MotorShutDown(int[] motor)
    {
        foreach (var pin in motor)
        {

            //TODO: Build a job and run it
        
            _mcp20GPIOController.Write(pin, PinValue.Low);
         
        }
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

        //for (int i = 0; i < 5; i++)
        //{
        //    Console.WriteLine("Twist Back and Forth direction=" + direction);
        //    Twist(direction, steps, speed);
        //    direction = !direction;
        //    Thread.Sleep(steps * speed + 2);
        //}
    }

    public void Twist(bool right = true, int steps = 380, int speed = 10)
    {
        Console.WriteLine("Twist");

        int max = 380; //362 is the whole range of motion
        if (steps < max) max = steps;
        int maxCounter = 0;

        if (right) Array.Reverse(_shouldersMotor);

        var job = new QueueModel();
        job.PinArray = _shouldersMotor;
        job.CurrentPosition = 0;
        job.MotorName = "  Shoulders";
        job.FullStep = true;

        while (true)
        {
            //Console.WriteLine("Method " + job.CurrentPosition);
            _queueList.Enqueue(job);
            RunJobs();
            if (job.FullStep)
            {
                job.CurrentPosition++;
                job.FullStep = false;
            }
            else
            {
                job.FullStep = true;

            }
            if (job.CurrentPosition > 3) job.CurrentPosition = 0;
            maxCounter++;
            if (maxCounter > max) break;
            Thread.Sleep(speed);
        }

        if (right) Array.Reverse(_shouldersMotor);

        //int step = 0; //Four steps 0 1 2 3
        //int max = 380; //362 is the whole range of motion
        //int maxCounter = 0;

        //if (right) Array.Reverse(_shouldersMotor);

        //while (true)
        //{
        //    if (maxCounter < max && maxCounter < steps)
        //    {

        //        if (_piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
        //        {
        //            int motorSteps = step;
        //            _mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.High);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            _mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            _mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            _mcp20GPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
        //            step++;
        //            if (step > 3) step = 0;
        //            Thread.Sleep(speed);
        //            maxCounter++;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Santa Twist Stop Hit");
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Santa Twist Max Counter Stop. Something went wrong with the stop input.");
        //        break;
        //    }
        //}


        //foreach (var motor in _shouldersMotor)
        //{
        //    _mcp20GPIOController.Write(motor, PinValue.Low);
        //}
        //if (right) Array.Reverse(_shouldersMotor);


    }

    public void Feet(bool right = true, int steps = 380, int speed = 10)
    {
        //Console.WriteLine("Feet");

        int max = 500; //362 is the whole range of motion
        if (steps < max) max = steps;
        int maxCounter = 0;

        if (right) Array.Reverse(_feetMotor);

        var job = new QueueModel();
        job.PinArray = _feetMotor;
        job.CurrentPosition = 0;
        job.MotorName = "  Feet";
        job.FullStep = true;
        
        while (true)
        {
            //Console.WriteLine("Method " + job.CurrentPosition);
            _queueList.Enqueue(job);
            RunJobs();
            if (job.FullStep)
            {
                job.CurrentPosition++;
                job.FullStep = false;
            }
            else
            {           
                job.FullStep = true;

            }
            if (job.CurrentPosition > 3) job.CurrentPosition = 0;
            maxCounter++;
            if (maxCounter > max) break;
            Thread.Sleep(speed);
        }


        //Don't shut the motor down unill the end
        //MotorShutDown(Motors.feetMotor);

        //foreach (var motor in _feetMotor)
        //{
        //    _mcp20GPIOController.Write(motor, PinValue.Low);
        //}
        if (right) Array.Reverse(_feetMotor);


        //int step = 0; //Four steps 0 1 2 3
        //int max = 200; //362 is the whole range of motion
        //int maxCounter = 0;

        //if (right) Array.Reverse(_feetMotor);

        //while (true)
        //{
        //    if (maxCounter < max && maxCounter < steps)
        //    {

        //        //if (_piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
        //        //{
        //        int motorSteps = step;
        //            _mcp20GPIOController.Write(_feetMotor[motorSteps], PinValue.High);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            _mcp20GPIOController.Write(_feetMotor[motorSteps], PinValue.Low);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            _mcp20GPIOController.Write(_feetMotor[motorSteps], PinValue.Low);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            _mcp20GPIOController.Write(_feetMotor[motorSteps], PinValue.Low);
        //            step++;
        //            if (step > 3) step = 0;
        //            Thread.Sleep(speed);
        //            maxCounter++;
        //        //}
        //        //else
        //        //{
        //        //    Console.WriteLine("Santa Twist Stop Hit");
        //        //    break;
        //        //}
        //    }
        //    else
        //    {
        //        Console.WriteLine("Santa Feet Max Counter Stop. Something went wrong with the stop input.");
        //        break;
        //    }
        //}


        //foreach (var motor in _feetMotor)
        //{
        //    _mcp20GPIOController.Write(motor, PinValue.Low);
        //}
        //if (right) Array.Reverse(_feetMotor);


    }

    public void LeftArm(bool right = true, int steps = 380, int speed = 10)
    {
        //Console.WriteLine("LeftArm");

        int max = 500; //362 is the whole range of motion
        if (steps < max) max = steps;
        int maxCounter = 0;

        if (right) Array.Reverse(_leftArmMotor);

        var job = new QueueModel();
        job.PinArray = _leftArmMotor;
        job.CurrentPosition = 0;
        job.MotorName = "                         Left Arm";
        job.FullStep = true;

        while (true)
        {
            //Console.WriteLine("Method " + job.CurrentPosition);
            _queueList.Enqueue(job);
            RunJobs();
            if (job.FullStep)
            {
                job.CurrentPosition++;
                job.FullStep = false;
            }
            else
            {
                job.FullStep = true;

            }
            if (job.CurrentPosition > 3) job.CurrentPosition = 0;
            maxCounter++;
            if (maxCounter > max) break;
            Thread.Sleep(speed);
        }

        foreach (var motor in _leftArmMotor)
        {
            _mcp20GPIOController.Write(motor, PinValue.Low);
        }
        if (right) Array.Reverse(_leftArmMotor);

        //int step = 0; //Four steps 0 1 2 3
        //int max = 200; //362 is the whole range of motion
        //int maxCounter = 0;

        //if (right) Array.Reverse(_leftArmMotor);

        //while (true)
        //{
        //    if (maxCounter < max && maxCounter < steps)
        //    {

        //        //if (_piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
        //        //{
        //        int motorSteps = step;
        //        _mcp20GPIOController.Write(_leftArmMotor[motorSteps], PinValue.High);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        _mcp20GPIOController.Write(_leftArmMotor[motorSteps], PinValue.Low);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        _mcp20GPIOController.Write(_leftArmMotor[motorSteps], PinValue.Low);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        _mcp20GPIOController.Write(_leftArmMotor[motorSteps], PinValue.Low);
        //        step++;
        //        if (step > 3) step = 0;
        //        Thread.Sleep(speed);
        //        maxCounter++;
        //        //}
        //        //else
        //        //{
        //        //    Console.WriteLine("Santa Twist Stop Hit");
        //        //    break;
        //        //}
        //    }
        //    else
        //    {
        //        Console.WriteLine("Santa Left Arm Max Counter Stop. Something went wrong with the stop input.");
        //        break;
        //    }
        //}


        //foreach (var motor in _leftArmMotor)
        //{
        //    _mcp20GPIOController.Write(motor, PinValue.Low);
        //}
        //if (right) Array.Reverse(_leftArmMotor);


    }

    public void RightArm(bool right = true, int steps = 380, int speed = 10)
    {
        //Console.WriteLine("RightArm");

        int max = 500; //362 is the whole range of motion
        if (steps < max) max = steps;
        int maxCounter = 0;

        if (right) Array.Reverse(_rightArmMotor);

        var job = new QueueModel();
        job.PinArray = _rightArmMotor;
        job.CurrentPosition = 0;
        job.MotorName = "            Right Arm";
        job.FullStep = true;

        while (true)
        {
            //Console.WriteLine("Method " + job.CurrentPosition);
            _queueList.Enqueue(job);
            RunJobs();
            if (job.FullStep)
            {
                job.CurrentPosition++;
                job.FullStep = false;
            }
            else
            {
                job.FullStep = true;

            }
            if (job.CurrentPosition > 3) job.CurrentPosition = 0;
            maxCounter++;
            if (maxCounter > max) break;
            Thread.Sleep(speed);
        }

        foreach (var motor in _rightArmMotor)
        {
            _mcp20GPIOController.Write(motor, PinValue.Low);
        }
        if (right) Array.Reverse(_rightArmMotor);

        //while (true)
        //{
        //    if (maxCounter < max && maxCounter < steps)
        //    {

        //        //if (_piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
        //        //{
        //        int motorSteps = step;
        //        _mcp20GPIOController.Write(_rightArmMotor[motorSteps], PinValue.High);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        _mcp20GPIOController.Write(_rightArmMotor[motorSteps], PinValue.Low);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        _mcp20GPIOController.Write(_rightArmMotor[motorSteps], PinValue.Low);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        _mcp20GPIOController.Write(_rightArmMotor[motorSteps], PinValue.Low);
        //        step++;
        //        if (step > 3) step = 0;
        //        Thread.Sleep(speed);
        //        maxCounter++;
        //        //}
        //        //else
        //        //{
        //        //    Console.WriteLine("Santa Twist Stop Hit");
        //        //    break;
        //        //}
        //    }
        //    else
        //    {
        //        Console.WriteLine("Santa Right Arm Max Counter Stop. Something went wrong with the stop input.");
        //        break;
        //    }
        //}


        //foreach (var motor in _rightArmMotor)
        //{
        //    _mcp20GPIOController.Write(motor, PinValue.Low);
        //}
        //if (right) Array.Reverse(_rightArmMotor);

    }
}