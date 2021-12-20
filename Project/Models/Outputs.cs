using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet.Models.Outputs
{
    public class Lights
    {



        //GPIO-27. Play Btn Feedback LED Green
        public static int PlayBtGreen { get; } = 27;
        //GPIO-15. Play Btn Feedback LED Red
        public static int PlayBtnRed { get; } = 15;


        //GPIO-18. Key Lights(PWM Dimmable Channel 0)
        private static int KeyLights { get; } = 18;
        //GPIO-19. Foot Lights(PWM Dimmable Channel 1)
        private static int FootLights { get; } = 19;


        //Backlight Truss:
        //GPIO-5. Back Left Red(QA Pass)
        public static int BackStageRightRed { get; } = 5;
        //GPIO-6. Back Left Green(QA Pass)
        public static int BackStageRightGreen { get; } = 6;
        //GPIO-7. Back Left Blue(QA Pass)
        public static int BackStageRightBlue { get; } = 7;
        //GPIO-12. Back Left Yellow(QA Pass)
        public static int BackStageRightYellow { get; } = 12;

        //GPIO-13. Back Right Red(QA Pass)
        public static int BackStageLeftRed { get; } = 13;
        //GPIO-16. Back Right Green(QA Pass)
        public static int BackStageLeftGreen { get; } = 16;
        //GPIO-20. Back Right Blue(QA Pass)
        public static int BackStageLeftBlue { get; } = 20;
        //GPIO-21. Back Right Yellow(QA Pass)
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

        public Lights(GpioController controller)
        {
            controller.OpenPin(PlayBtnRed, PinMode.Output);
            controller.OpenPin(PlayBtGreen, PinMode.Output);
            controller.OpenPin(BackStageRightRed, PinMode.Output);
            controller.OpenPin(BackStageRightGreen, PinMode.Output);
            controller.OpenPin(BackStageRightBlue, PinMode.Output);
            controller.OpenPin(BackStageRightYellow, PinMode.Output);
            controller.OpenPin(BackStageLeftRed, PinMode.Output);
            controller.OpenPin(BackStageLeftGreen, PinMode.Output);
            controller.OpenPin(BackStageLeftBlue, PinMode.Output);
            controller.OpenPin(BackStageLeftYellow, PinMode.Output);
        }
    }

    public class Motors
    {

        //GPIO-14. Talking

        //GPIO-22. Motor Curtin(QA Pass)
        //GPIO-23. Motor Curtin(QA Pass)
        //GPIO-24. Motor Curtin(QA Pass)
        //GPIO-25. Motor Curtin(QA Pass)

        //MCP23017 Port Expander 0x20 
        //https://raspi.tv/wp-content/uploads/2013/07/MCP23017.jpg  

        //GPIO-A0.Motor1 Feet
        //GPIO-A1.Motor1 Feet
        //GPIO-A2.Motor1 Feet
        //GPIO-A3.Motor1 Feet


        //GPIO-A4.Motor2 Left Arm
        //GPIO-A5.Motor2 Left Arm
        //GPIO-A6.Motor2 Left Arm
        //GPIO-A7.Motor2 Left Arm



        //GPIO-B0.Motor3 Right Arm
        //GPIO-B1.Motor3 Right Arm
        //GPIO-B2.Motor3 Right Arm
        //GPIO-B3.Motor3 Right Arm


        //GPIO-B4.Motor4 Shoulders
        //GPIO-B5.Motor4 Shoulders
        //GPIO-B6.Motor4 Shoulders
        //GPIO-B7.Motor4 Shoulders

        public Motors(GpioController controller)
        {

        }
    }
}
