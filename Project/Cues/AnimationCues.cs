using System.Collections;

namespace SantaPuppet.Cues;

public class AnimationCues
{
    private static int[] _shouldersMotor;
    private static int[] _feetMotor;
    private static int[] _leftArmMotor;
    private static int[] _rightArmMotor;

    public AnimationCues()
    {
        //Console.WriteLine("Animation Cues Constructors");        
        _shouldersMotor = Motors.shouldersMotor;
        _feetMotor = Motors.feetMotor;
        _leftArmMotor = Motors.leftArmMotor;
        _rightArmMotor = Motors.rightArmMotor;       

    }

    private void MotorShutDown(int[] motor)
    {
        foreach (var pin in motor)
        {

            //TODO: Build a job and run it
        
            Program.piGPIOController.Write(pin, PinValue.Low);
         
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

                if (Program.piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
                {
                    int motorSteps = step;
                    Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.High);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
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
            Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.High);
            motorSteps++;
            if (motorSteps > 3) motorSteps = 0;
            Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
            motorSteps++;
            if (motorSteps > 3) motorSteps = 0;
            Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
            motorSteps++;
            if (motorSteps > 3) motorSteps = 0;
            Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
            step++;
            if (step > 3) step = 0;
            Thread.Sleep(3);
            maxCounter++;
        }
        Array.Reverse(_shouldersMotor);
 
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

        int step = 0; //Four steps 0 1 2 3
        int max = 380; //362 is the whole range of motion
        int maxCounter = 0;

        if (right) Array.Reverse(_shouldersMotor);

        while (true)
        {
            if (maxCounter < max && maxCounter < steps)
            {

                //if (Program.piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
                //{
                    int motorSteps = step;
                    Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.High);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    Program.piGPIOController.Write(_shouldersMotor[motorSteps], PinValue.Low);
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
                Console.WriteLine("Santa Twist Max Counter Stop. Something went wrong with the stop input.");
                break;
            }
        }


        if (right) Array.Reverse(_shouldersMotor);


    }

    public void Feet(bool right = true, int steps = 380, int speed = 10)
    {
        //Console.WriteLine("Feet");

       


        //int step = 0; //Four steps 0 1 2 3
        //int max = 200; //362 is the whole range of motion
        //int maxCounter = 0;

        //if (right) Array.Reverse(_feetMotor);

        //while (true)
        //{
        //    if (maxCounter < max && maxCounter < steps)
        //    {

        //        //if (Program.piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
        //        //{
        //        int motorSteps = step;
        //            Program.piGPIOController.Write(_feetMotor[motorSteps], PinValue.High);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            Program.piGPIOController.Write(_feetMotor[motorSteps], PinValue.Low);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            Program.piGPIOController.Write(_feetMotor[motorSteps], PinValue.Low);
        //            motorSteps++;
        //            if (motorSteps > 3) motorSteps = 0;
        //            Program.piGPIOController.Write(_feetMotor[motorSteps], PinValue.Low);
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


       
        //if (right) Array.Reverse(_feetMotor);


    }

    public void LeftArm(bool right = true, int steps = 380, int speed = 10)
    {
        //Console.WriteLine("LeftArm");

       

        //int step = 0; //Four steps 0 1 2 3
        //int max = 200; //362 is the whole range of motion
        //int maxCounter = 0;

        //if (right) Array.Reverse(_leftArmMotor);

        //while (true)
        //{
        //    if (maxCounter < max && maxCounter < steps)
        //    {

        //        //if (Program.piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
        //        //{
        //        int motorSteps = step;
        //        Program.piGPIOController.Write(_leftArmMotor[motorSteps], PinValue.High);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        Program.piGPIOController.Write(_leftArmMotor[motorSteps], PinValue.Low);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        Program.piGPIOController.Write(_leftArmMotor[motorSteps], PinValue.Low);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        Program.piGPIOController.Write(_leftArmMotor[motorSteps], PinValue.Low);
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


        //if (right) Array.Reverse(_leftArmMotor);


    }

    public void RightArm(bool right = true, int steps = 380, int speed = 10)
    {
        //Console.WriteLine("RightArm");

        int max = 500; //362 is the whole range of motion
        if (steps < max) max = steps;
        int maxCounter = 0;

        if (right) Array.Reverse(_rightArmMotor);

       

        //while (true)
        //{
        //    if (maxCounter < max && maxCounter < steps)
        //    {

        //        //if (Program.piGPIOController.Read(Inputs.SantaTwistStop) == PinValue.Low || maxCounter < 2) //max < 2 let it off the input stop 
        //        //{
        //        int motorSteps = step;
        //        Program.piGPIOController.Write(_rightArmMotor[motorSteps], PinValue.High);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        Program.piGPIOController.Write(_rightArmMotor[motorSteps], PinValue.Low);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        Program.piGPIOController.Write(_rightArmMotor[motorSteps], PinValue.Low);
        //        motorSteps++;
        //        if (motorSteps > 3) motorSteps = 0;
        //        Program.piGPIOController.Write(_rightArmMotor[motorSteps], PinValue.Low);
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

        //if (right) Array.Reverse(_rightArmMotor);
    }
}