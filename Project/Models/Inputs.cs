using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet.Models.Inputs
{
    public class Inputs
    {

        //GPIO-4. Left Arm Stops(Red)
        //GPIO-10. Right Arm Stops(Green)
        //GPIO-11. Feet Stops(Blue)
        //GPIO-17. Twist Stops(Black)
        //GPIO-8. Curtin Stage Left Stop(Open) (Yellow)
        //GPIO-9. Curtin Stage Right Stop(Close) (Green) 

 
        /// <summary>
        /// GPIO-26. Play button - Input
        /// </summary>
        public int PlayButton { get; } = 26;

        /// <summary>
        /// Open all the input pins
        /// </summary>
        /// <param name="controller"></param>
        public Inputs(GpioController controller) {
         
            controller.OpenPin(PlayButton, PinMode.Input); 
        }

   



    }
}
