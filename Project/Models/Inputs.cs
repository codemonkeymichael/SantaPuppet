namespace SantaPuppet.Models.Inputs;

public class Inputs
{
    /// <summary>
    /// MCP-GPIO-B0-9. Left Arm Stops (red wire)
    /// </summary>
    public static int SantaLeftArmStop { get; } = 9;

    /// <summary>
    /// MCP-GPIO-B1-10. Right Arm Stops (green wire)
    /// </summary>
    public static int SantaRightArmStop { get; } = 10;

    /// <summary>
    /// MCP-GPIO-B2-11. Feet Stops (blue wire)
    /// </summary>
    public static int SantaFeetStop { get; } = 11;

    /// <summary>
    /// MCP-GPIO-B3-12. Twist Stops (black wire)
    /// </summary>
    public static int SantaTwistStop { get; } = 12;


    /// <summary>
    /// MCP-GPIO-B4-13. Curtin Stage Left Stop (Open) (yellow wire)
    /// </summary>
    public static int CurtinStageLeftStopOpen { get; } = 13;

    /// <summary>
    /// MCP-GPIO-B5-14. Curtin Stage Right Stop (Close) (green wire) 
    /// </summary>
    public static int CurtinStageRightStopClosed { get; } = 14;


    /// <summary>
    /// MCP-GPIO-B6-15. Play button (blue wire)
    /// </summary>
    public static int PlayButton { get; } = 15;

    /// <summary>
    /// Open all the input pins
    /// </summary>
    /// <param name="mcp20GPIOController"></param>
    public static void OpenPins(GpioController mcp20GPIOController)
    {
        mcp20GPIOController.OpenPin(SantaLeftArmStop, PinMode.Input);
        mcp20GPIOController.OpenPin(SantaRightArmStop, PinMode.Input);
        mcp20GPIOController.OpenPin(SantaFeetStop, PinMode.Input);
        mcp20GPIOController.OpenPin(SantaTwistStop, PinMode.Input);
        mcp20GPIOController.OpenPin(PlayButton, PinMode.Input);
        mcp20GPIOController.OpenPin(CurtinStageLeftStopOpen, PinMode.Input);
        mcp20GPIOController.OpenPin(CurtinStageRightStopClosed, PinMode.Input);
    }
}
