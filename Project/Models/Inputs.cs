using System;
using System.Collections.Generic;
using System.Device.Gpio;



namespace SantaPuppet.Models.Inputs
{
    public class Inputs
    {        
        /// <summary>
        /// GPIO-4. Left Arm Stops(Red)
        /// </summary>
        public static int SantaLeftArmStop { get; } = 4;
     
        /// <summary>
        /// GPIO-10. Right Arm Stops(Green)
        /// </summary>
        public static int SantaRightArmStop { get; } = 10;

        /// <summary>
        /// GPIO-11. Feet Stops(Blue)
        /// </summary>
        public static int SantaFeetStop { get; } = 11;

        /// <summary>
        /// GPIO-17. Twist Stops(Black)
        /// </summary>
        public static int SantaTwistStop { get; } = 17;

      
        /// <summary>
        /// GPIO-8. Curtin Stage Left Stop(Open) (Yellow)
        /// </summary>
        public static int CurtinStageLeftStopOpen { get; } = 8;

        /// <summary>
        /// GPIO-9. Curtin Stage Right Stop(Close) (Green) 
        /// </summary>
        public static int CurtinStageRightStopClosed { get; } = 9;

 
        /// <summary>
        /// GPIO-26. Play button (Blue)
        /// </summary>
        public static int PlayButton { get; } = 26;

        /// <summary>
        /// Open all the input pins
        /// </summary>
        /// <param name="controller"></param>
        public static void OpenPins(GpioController controller) 
        {
            controller.OpenPin(SantaLeftArmStop, PinMode.Input);
            controller.OpenPin(SantaRightArmStop, PinMode.Input);
            controller.OpenPin(SantaFeetStop, PinMode.Input);
            controller.OpenPin(SantaTwistStop, PinMode.Input);
            controller.OpenPin(PlayButton, PinMode.Input); 
            controller.OpenPin(CurtinStageLeftStopOpen, PinMode.Input);
            controller.OpenPin(CurtinStageRightStopClosed, PinMode.Input);
        } 
    }
}
