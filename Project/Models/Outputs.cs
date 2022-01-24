namespace SantaPuppet.Models.Outputs;

public class Lights
{

    /// MCP23017 Port Expander 0x20 
    /// https://raspi.tv/wp-content/uploads/2013/07/MCP23017.jpg 


    /// <summary>
    /// PI-GPIO-14. Play Btn Feedback LED Green
    /// </summary>
    public static int PlayBtnGreen { get; } = 14;


    /// <summary>
    /// PI-GPIO-18. Key Lights(PWM Dimmable Channel 0)
    /// </summary>
    private static int KeyLights { get; } = 18;



    /// <summary>
    /// GPIO-B7-16. Play Btn Feedback LED Red
    /// </summary>
    public static int PlayBtnRed { get; } = 15;



    /// <summary>
    /// PI-GPIO-19. Foot Lights(PWM Dimmable Channel 1)
    /// </summary>
    private static int FootLights { get; } = 19;


    /// <summary>
    /// Backlight Truss
    /// MCP-GPIO-A0-1. Back Left Red (red wire)
    /// </summary>
    public static int BackStageRightRed { get; } = 1;

    /// <summary>
    /// MCP-GPIO-A1-2. Back Left Green (green wire)
    /// </summary>
    public static int BackStageRightGreen { get; } = 2;

    /// <summary>
    /// MCP-GPIO-A2. Back Left Blue (blue wire)
    /// </summary>
    public static int BackStageRightBlue { get; } = 3;

    /// <summary>
    /// MCP-GPIO-A3. Back Left Yellow (yellow Pass)
    /// </summary> 
    public static int BackStageRightYellow { get; } = 4;

    /// <summary>
    /// MCP-GPIO-A4. Back Right Red (red wire)
    /// </summary>
    public static int BackStageLeftRed { get; } = 5;

    /// <summary>
    /// MCP-GPIO-A5. Back Right Green (green wire)
    /// </summary>
    public static int BackStageLeftGreen { get; } = 6;

    /// <summary>
    /// MCP-GPIO-A6. Back Right Blue (blue wire)
    /// </summary>
    public static int BackStageLeftBlue { get; } = 7;

    /// <summary>
    /// MCP-GPIO-A7. Back Right Yellow(yellow wire)
    /// </summary>
    public static int BackStageLeftYellow { get; } = 8;

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

    public static void OpenPins(GpioController piGPIOController, GpioController mcp20GPIOController)
    {
        //Button Lights
        //mcp20GPIOController.OpenPin(PlayBtnRed, PinMode.Output, PinValue.Low);

        piGPIOController.OpenPin(PlayBtnGreen, PinMode.Output, PinValue.Low);

        //Stage Back Lights
        foreach (var pin in Backlights)
        {
            mcp20GPIOController.OpenPin(pin, PinMode.Output, PinValue.Low);
        }

    }
}

public class Motors
{
    /// <summary>
    /// PI-GPIO-15. Talking (Black) 
    /// </summary>
    public static int SantaTalk { get; } = 15;


    /// <summary>
    /// Curtin Motor PI GPIO 21, 26, 13, 6
    /// PI-GPIO-21. Motor Curtin 1 (red wire)
    /// PI-GPIO-26. Motor Curtin 2 (green wire)
    /// PI-GPIO-13. Motor Curtin 3 (blue wire)
    /// PI-GPIO-6.  Motor Curtin 4 (yellow wire)   
    /// </summary>
    public static int[] curtinMotor { get; } = new int[4] { 21, 26, 13, 6 };




    /// <summary>
    /// Shoulders Motor PI GPIO 7, 12, 16, 20
    /// PI-GPIO-7. Motor Shoulders (red wire)
    /// PI-GPIO-12. Motor Shoulders (green wire)
    /// PI-GPIO-16. Motor Shoulders (blue wire)
    /// PI-GPIO-20. Motor Shoulders (yellow wire) 
    /// </summary>
    public static int[] shouldersMotor { get; } = new int[4] { 7, 12, 16, 20 };

    /// <summary>
    /// Feet Motor PI GPIO 5, 11, 9, 10
    /// PI-GPIO-5. Motor Feet (red wire)
    /// PI-GPIO-11. Motor Feet (green wire)
    /// PI-GPIO-9. Motor Feet (blue wire)
    /// PI-GPIO-10. Motor Feet (yellow wire) 
    /// </summary>
    public static int[] feetMotor { get; } = new int[4] { 5, 11, 9, 10 };

    /// <summary>
    /// Lefyt Arm Motor PI GPIO  22, 27, 17, 4
    /// PI-GPIO-22. Motor Left Arm (red wire)
    /// PI-GPIO-27. Motor Left Arm (green wire)
    /// PI-GPIO-17. Motor Left Arm (blue wire)
    /// PI-GPIO-4. Motor Left Arm (yellow wire) 
    /// </summary>
    public static int[] leftArmMotor { get; } = new int[4] { 22, 27, 17, 4 };


    /// <summary>
    /// MCP23017 0x20
    /// PI-GPIO-23. Motor Right Arm (red wire)
    /// PI-GPIO-24. Motor Right Arm (green wire)
    /// PI-GPIO-25. Motor Right Arm (blue wire)
    /// PI-GPIO-8. Motor Right Arm (yellow wire) 
    /// </summary>
    public static int[] rightArmMotor { get; } = new int[4] { 23, 24, 25, 8 };




    public static void OpenPins(GpioController piGPIOController)
    {
        piGPIOController.OpenPin(SantaTalk, PinMode.Output, PinValue.Low);

        //CurtinCues cc = new CurtinCues(piGPIOController);
        foreach (int motor in curtinMotor)
        {
            piGPIOController.OpenPin(motor, PinMode.Output, PinValue.Low);
        }
        //cc.OpenClose(false, 2);

        //AnimationCues ac = new AnimationCues(piGPIOController, mcp20GPIOController);
        foreach (int motor in shouldersMotor)
        {
            piGPIOController.OpenPin(motor, PinMode.Output, PinValue.Low);
        }
        //ac.TwistCenter();

        foreach (int motor in feetMotor)
        {
            piGPIOController.OpenPin(motor, PinMode.Output, PinValue.Low);
        }
        //TODO: Center the feet motor

        foreach (int motor in leftArmMotor)
        {
            piGPIOController.OpenPin(motor, PinMode.Output, PinValue.Low);
        }
        //TODO: Center the left arm motor
        foreach (int motor in rightArmMotor)
        {
            piGPIOController.OpenPin(motor, PinMode.Output, PinValue.Low);
        }

    }
}

