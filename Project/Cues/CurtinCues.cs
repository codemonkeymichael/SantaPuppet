namespace SantaPuppet.Cues;

public class CurtinCues
{
    private static int[] _curtinMotor = Motors.curtinMotor;
    //private const int maxSteps = 1000;
    private const int maxSteps = 99999999;
    private static int maxStepCounter { get; set; }


    /// <summary>
    /// Open and close the curtin.
    /// </summary>
    /// <param name="open"></param>
    /// <param name="speed"></param>
    public void OpenClose(bool open = true, int speed = 2)
    {
        //Console.WriteLine("CurtinCues OpenClose open = " + open);

        if (open) Array.Reverse(_curtinMotor);

        maxStepCounter = 0;
        int step = 0; //Four steps 0 1 2 3

        //Console.WriteLine("Curtin maxStepCounter=" + maxStepCounter + " maxSteps=" + maxSteps + " speed=" + speed);

        while (true)
        {

            if (maxStepCounter < maxSteps)
            {
                if ((open && !Inputs.CurtinStageLeftStopOpenTrigger)
                    ||
                    (!open && !Inputs.CurtinStageRightStopClosedTrigger))
                {
                    //Console.WriteLine("Curtin Break maxStepCounter=" + maxStepCounter + " _curtinMotor[step]=" + _curtinMotor[step] + " speed=" + speed);

                    int motorSteps = step;
                    Program.piGPIOController.Write(_curtinMotor[motorSteps], PinValue.High);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    Program.piGPIOController.Write(_curtinMotor[motorSteps], PinValue.High);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    Program.piGPIOController.Write(_curtinMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    Program.piGPIOController.Write(_curtinMotor[motorSteps], PinValue.Low);
                    step++;
                    if (step > 3)
                    {
                        step = 0;
                        //Check the stop every 4 steps
                        if (open)
                        {
                            Thread stopCheck = new Thread(() => I2CJobQueue.CurtinOpenCheck());
                            stopCheck.Start();
                        }
                        else
                        {
                            Thread stopCheck = new Thread(() => I2CJobQueue.CurtinCloseCheck());
                            stopCheck.Start();
                        }
                    }
                    Thread.Sleep(speed);
                }
                else
                {
                    Console.WriteLine("Curtin hit the stop. Open = " + open);
                    Inputs.CurtinStageRightStopClosedTrigger = false;
                    Inputs.CurtinStageLeftStopOpenTrigger = false;
                    break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Curtin over ran the max number of steps. Something must be wrong with the stop input GPIO.");
                break;
            }
            maxStepCounter++;

        }
        if (open) Array.Reverse(_curtinMotor);
        foreach (int motor in _curtinMotor)
        {
            //Console.WriteLine("Turn off all curtin motors.");
            Program.piGPIOController.Write(motor, PinValue.Low);
        }
    }
}