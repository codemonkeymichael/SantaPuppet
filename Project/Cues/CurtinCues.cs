namespace SantaPuppet.Cues;

public class CurtinCues
{
    private static GpioController _piGPIOController;
    public static GpioController _mcp20GPIOController;
    private static int[] _curtinMotor;
    private const int maxSteps = 1000;
    private static int maxStepCounter { get; set; }

    public CurtinCues(GpioController piGPIOController, GpioController mcp20GPIOController)
    {
        //Console.WriteLine("Curtin Cues Constructors");
        _curtinMotor = Motors.curtinMotor;
        _piGPIOController = piGPIOController;
        _mcp20GPIOController = mcp20GPIOController;
    }

    /// <summary>
    /// Open and close the curtin.
    /// </summary>
    /// <param name="open"></param>
    /// <param name="speed"></param>
    public void OpenClose(bool open = true, int speed = 2)
    {
        //Console.WriteLine("CurtinCues OpenClose open = " + open);

        if (open) Array.Reverse(_curtinMotor);

        //foreach (var c in _curtinMotor)
        //{
        //    Console.WriteLine(c);
        //}

        maxStepCounter = 0;
        int step = 0; //Four steps 0 1 2 3

        //Console.WriteLine("Curtin maxStepCounter=" + maxStepCounter + " maxSteps=" + maxSteps + " speed=" + speed);

        while (true)
        {

            if (maxStepCounter < maxSteps)
            {
                if (open && _mcp20GPIOController.Read(Inputs.CurtinStageLeftStopOpen) == PinValue.Low
                    ||
                    !open && _mcp20GPIOController.Read(Inputs.CurtinStageRightStopClosed) == PinValue.Low)

                {
                    //Console.WriteLine("Curtin Break maxStepCounter=" + maxStepCounter + " _curtinMotor[step]=" + _curtinMotor[step] + " speed=" + speed);

                    int motorSteps = step;
                    _piGPIOController.Write(_curtinMotor[motorSteps], PinValue.High);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    _piGPIOController.Write(_curtinMotor[motorSteps], PinValue.High);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    _piGPIOController.Write(_curtinMotor[motorSteps], PinValue.Low);
                    motorSteps++;
                    if (motorSteps > 3) motorSteps = 0;
                    _piGPIOController.Write(_curtinMotor[motorSteps], PinValue.Low);
                    step++;
                    if (step > 3) step = 0;
                    Thread.Sleep(speed);
                }
                else
                {
                    Console.WriteLine("Curtin hit the stop. Open = " + open);
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
            //Console.WriteLine("Turn off all curtin motors.");
            _piGPIOController.Write(motor, PinValue.Low);
        }
    }
}