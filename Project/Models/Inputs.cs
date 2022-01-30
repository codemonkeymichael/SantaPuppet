namespace SantaPuppet.Models.Inputs;

public class Inputs
{
    /// <summary>
    /// MCP-GPIO-B0. Left Arm Stops (red wire)
    /// </summary>
    public static int SantaLeftArmStop { get; } = 8;

    /// <summary>
    /// MCP-GPIO-B1. Right Arm Stops (green wire)
    /// </summary>
    public static int SantaRightArmStop { get; } = 9;

    /// <summary>
    /// MCP-GPIO-B2. Feet Stops (blue wire)
    /// </summary>
    public static int SantaFeetStop { get; } = 10;

    /// <summary>
    /// MCP-GPIO-B3. Twist Stops (black wire)
    /// </summary>
    public static int SantaTwistStop { get; } = 11;


    /// <summary>
    /// MCP-GPIO-B4. Curtin Stage Left Stop (Open) (yellow wire)
    /// </summary>
    public static int CurtinStageLeftStopOpen { get; } = 12;

    /// <summary>
    /// MCP-GPIO-B5-14. Curtin Stage Right Stop (Close) (green wire) 
    /// </summary>
    public static int CurtinStageRightStopClosed { get; } = 13;


    /// <summary>
    /// MCP-GPIO-B6. Play button (blue wire)
    /// </summary>
    public static int PlayButton { get; } = 14;

    /// <summary>
    /// Open all the input pins
    /// </summary>   
    public static void OpenPins()
    {
        Program.mcp20GPIOController.OpenPin(SantaLeftArmStop, PinMode.Input);
        Thread.Sleep(5);
        Program.mcp20GPIOController.OpenPin(SantaRightArmStop, PinMode.Input);
        Thread.Sleep(5);
        Program.mcp20GPIOController.OpenPin(SantaFeetStop, PinMode.Input);
        Thread.Sleep(5);
        Program.mcp20GPIOController.OpenPin(SantaTwistStop, PinMode.Input);
        Thread.Sleep(5);
        Program.mcp20GPIOController.OpenPin(PlayButton, PinMode.Input);
        Thread.Sleep(5);
        Program.mcp20GPIOController.OpenPin(CurtinStageLeftStopOpen, PinMode.Input);
        Thread.Sleep(5);
        Program.mcp20GPIOController.OpenPin(CurtinStageRightStopClosed, PinMode.Input);
        Thread.Sleep(5);


    }
}
