namespace SantaPuppet.Models.Outputs;

public class Lights
{

    /// <summary>
    /// GPIO-27. Play Btn Feedback LED Green
    /// </summary>
    public static int PlayBtnGreen { get; } = 27;

    /// <summary>
    /// GPIO-15. Play Btn Feedback LED Red
    /// </summary>
    public static int PlayBtnRed { get; } = 15;

    /// <summary>
    /// GPIO-18. Key Lights(PWM Dimmable Channel 0)
    /// </summary>
    private static int KeyLights { get; } = 18;

    /// <summary>
    /// GPIO-19. Foot Lights(PWM Dimmable Channel 1)
    /// </summary>
    private static int FootLights { get; } = 19;


    /// <summary>
    /// Backlight Truss
    /// GPIO-5. Back Left Red(QA Pass)
    /// </summary>
    public static int BackStageRightRed { get; } = 5;

    /// <summary>
    /// GPIO-6. Back Left Green(QA Pass)
    /// </summary>
    public static int BackStageRightGreen { get; } = 6;

    /// <summary>
    /// GPIO-7. Back Left Blue(QA Pass)
    /// </summary>
    public static int BackStageRightBlue { get; } = 7;

    /// <summary>
    /// GPIO-12. Back Left Yellow(QA Pass)
    /// </summary> 
    public static int BackStageRightYellow { get; } = 12;

    /// <summary>
    /// GPIO-13. Back Right Red(QA Pass)
    /// </summary>
    public static int BackStageLeftRed { get; } = 13;

    /// <summary>
    /// GPIO-16. Back Right Green(QA Pass)
    /// </summary>
    public static int BackStageLeftGreen { get; } = 16;

    /// <summary>
    /// GPIO-20. Back Right Blue(QA Pass)
    /// </summary>
    public static int BackStageLeftBlue { get; } = 20;

    /// <summary>
    /// GPIO-21. Back Right Yellow(QA Pass)
    /// </summary>
    public static int BackStageLeftYellow { get; } = 21;

    public static int[] Backlights { get; } = new int[8]
    {
            BackStageRightRed,
            BackStageRightGreen,
            BackStageRightBlue,
            BackStageRightYellow,
            BackStageLeftRed,
            BackStageLeftGreen,
            BackStageLeftBlue,
            BackStageLeftYellow
    };

    public static void OpenPins(GpioController controller)
    {
        //Button Lights
        controller.OpenPin(PlayBtnRed, PinMode.Output);
        controller.OpenPin(PlayBtnGreen, PinMode.Output);
        //Stage Back Lights
        foreach (var pin in Backlights)
        {
            controller.OpenPin(pin, PinMode.Output);
        }

    }
}

public class Motors
{
    /// <summary>
    /// GPIO-14. Talking (Black)
    /// </summary>
    public static int SantaTalk { get; } = 14;


    /// <summary>
    /// Curtin Motor GPIO 22, 23, 24, 25S
    /// GPIO-22. Motor Curtin 1 (Red)
    /// GPIO-23. Motor Curtin 2 (Green)
    /// GPIO-24. Motor Curtin 3 (Blue)
    /// GPIO-25. Motor Curtin 4 (Yellow)
    /// </summary>
    public static int[] curtinMotor { get; } = new int[4] { 22, 23, 24, 25 };

    //MCP23017 Port Expander 0x20 
    //https://raspi.tv/wp-content/uploads/2013/07/MCP23017.jpg  


    /// <summary>
    /// MCP23017 0x20
    /// GPIO-A0.Motor1 Shoulders (Red)
    /// GPIO-A1.Motor1 Shoulders (Green)
    /// GPIO-A2.Motor1 Shoulders (Blue)
    /// GPIO-A3.Motor1 Shoulders (Yellow)
    /// </summary>
    public static int[] shouldersMotor { get; } = new int[4] { 0, 1, 2, 3 };


    //GPIO-A4.Motor2 Left Arm
    //GPIO-A5.Motor2 Left Arm
    //GPIO-A6.Motor2 Left Arm
    //GPIO-A7.Motor2 Left Arm



    //GPIO-B0.Motor3 Right Arm
    //GPIO-B1.Motor3 Right Arm
    //GPIO-B2.Motor3 Right Arm
    //GPIO-B3.Motor3 Right Arm


    //GPIO-B4.Motor4 Feet
    //GPIO-B5.Motor4 Feet
    //GPIO-B6.Motor4 Feet
    //GPIO-B7.Motor4 Feet

    public static void OpenPins(GpioController piGPIOController, GpioController mcp20GPIOController)
    {
        piGPIOController.OpenPin(SantaTalk, PinMode.Output);

        CurtinCues cc = new CurtinCues(piGPIOController);
        foreach (int motor in curtinMotor)
        {
            piGPIOController.OpenPin(motor, PinMode.Output, PinValue.Low);
        }
        cc.OpenClose(false, 2);

        AnimationCues ac = new AnimationCues(piGPIOController, mcp20GPIOController);
        foreach (int motor in shouldersMotor)
        {
            mcp20GPIOController.OpenPin(motor, PinMode.Output, PinValue.Low);           
        }       
        ac.TwistCenter();

    }
}

