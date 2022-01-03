using Iot.Device.Mcp23xxx;
using System.Collections.Generic;
using System.Device.I2c;
using System.Device.Pwm;

namespace SantaPuppet.Cues;

public class LightCues
{
    private static GpioController _controller;


    public LightCues(GpioController controller)
    {
        //Console.WriteLine("Light Cues Construstor");
        _controller = controller;
    }



    public void TestI2c()
    {
        //Console.WriteLine("Test MCP23017 I2C");



        //I2cConnectionSettings connectionSettings = new I2cConnectionSettings(1, 0x20);
        //I2cDevice device = I2cDevice.Create(connectionSettings);
        //Mcp23017 mcp23017 = new Mcp23017(device);

        //mcp23017.WriteByte(Register.IODIR, 0x00, Port.PortA);
        //mcp23017.WriteByte(Register.IODIR, 0x00, Port.PortB);




        //var controller = new GpioController(PinNumberingScheme.Logical, mcp23017);


        //for (int i = 0; i < 16; i++)
        //{
        //    controller.OpenPin(i, PinMode.Output, PinValue.Low);
        //    controller.SetPinMode(i, PinMode.Output);

        //}

        //bool status = true;

        //while (true)
        //{
        //    if (status)
        //    {
        //        for (int i = 0; i < 16; i++)
        //        {
        //            controller.Write(i, PinValue.High);          
        //            Task.Delay(250).Wait();
        //        }
        //        Console.WriteLine("On");
        //        for (int i = 0; i < 16; i++)
        //        {
        //            Console.WriteLine("Pin" + i + " " + controller.Read(i) + 
        //                " Mode=" + controller.GetPinMode(i) + 
        //                " PinCount=" + controller.PinCount +
        //                " IsPinModeSupported.Output=" + controller.IsPinModeSupported(i,PinMode.Output));                       
        //        }
        //        status = false;                 
        //    }
        //    else
        //    {
        //        for (int i = 0; i < 16; i++)
        //        {
        //            controller.Write(i, PinValue.Low);
        //            Task.Delay(250).Wait();
        //        }
        //        Console.WriteLine("Off");
        //        for (int i = 0; i < 16; i++)
        //        {
        //            Console.WriteLine("Pin" + i + " " + controller.Read(i));
        //        }
        //        status = true;                   
        //    }               
        //}
    }

    public void Back_StrobeAll_Fast()
    {
        Back_StrobeAll(300);
    }
    public void Back_StrobeAll_Slow()
    {
        Back_StrobeAll(600);
    }
    public void Back_StrobeAll(int duration)
    {
        foreach (int light in Lights.Backlights)
        {
            _controller.Write(light, PinValue.High);
        }

        Thread.Sleep(duration);

        foreach (int light in Lights.Backlights)
        {
            _controller.Write(light, PinValue.Low);
        }
    }

    public void Back_StrobeRandom_Fast_NoSplit(int repeat)
    {
        Back_StrobeRandom(125, repeat, false);
    }
    public void Back_StrobeRandom_Slow_NoSplit(int repeat)
    {
        Back_StrobeRandom(350, repeat, false);
    }
    public void Back_StrobeRandom_Fast_Split(int repeat)
    {
        Back_StrobeRandom(150, repeat, true);
    }
    public void Back_StrobeRandom_Slow_Split(int repeat)
    {
        Back_StrobeRandom(400, repeat, true);
    }
    public void Back_StrobeRandom(int duration, int repeat, bool split)
    {
        var rand = new Random();

        int addUpSleepTime = 0;

        for (int n = 0; n < repeat; n++)
        {
            if (split)
            {
                //Left Half
                var left = rand.Next(0, 4);
                var right = rand.Next(5, 8);
                _controller.Write(Lights.Backlights[left], PinValue.High);
                addUpSleepTime += duration / 2;
                Thread.Sleep(duration / 2);
                _controller.Write(Lights.Backlights[right], PinValue.High);
                addUpSleepTime += duration / 2;
                Thread.Sleep(duration / 2);
                _controller.Write(Lights.Backlights[left], PinValue.Low);
                addUpSleepTime += duration / 2;
                Thread.Sleep(duration / 2);
                _controller.Write(Lights.Backlights[right], PinValue.Low);
            }
            else
            {
                var light = rand.Next(0, 8);
                _controller.Write(Lights.Backlights[light], PinValue.High);
                addUpSleepTime += duration;
                Thread.Sleep(duration);
                _controller.Write(Lights.Backlights[light], PinValue.Low);
            }
            //Console.WriteLine("Total Loop Duration = " + addUpSleepTime);
        }
    }


    /// <summary>
    /// One Off
    /// </summary>
    public void Back_1Off_Fast_NoBoun_NoSplit(int repeat)
    {
        Back_1Off(85, repeat, false, false);
    }
    /// <summary>
    /// One Off
    /// </summary>
    public void Back_1Off_Slow_NoBoun_NoSplit(int repeat)
    {
        Back_1Off(110, repeat, false, false);
    }
    /// <summary>
    /// One Off
    /// </summary>
    public void Back_1Off_Fast_Bounce_NoSplit(int repeat)
    {
        Back_1Off(85, repeat, true, false);
    }
    /// <summary>
    /// One Off
    /// </summary>
    public void Back_1Off_Slow_Bounce_NoSplit(int repeat)
    {
        Back_1Off(110, repeat, true, false);
    }
    /// <summary>
    /// One Off
    /// </summary>
    public void Back_1Off_Fast_NoBounce_Split(int repeat)
    {
        Back_1Off(85, repeat, false, true);
    }
    /// <summary>
    /// One Off
    /// </summary>
    public void Back_1Off__Slow_NoBoun_Split(int repeat)
    {
        Back_1Off(110, repeat, false, true);
    }
    /// <summary>
    /// One Off
    /// </summary>
    public void Back_1Off_Fast_Boun_Split(int repeat)
    {
        Back_1Off(85, repeat, true, true);
    }
    /// <summary>
    /// One Off
    /// </summary>
    public void Back_1Off_Slow_Bounce_Split(int repeat)
    {
        Back_1Off(110, repeat, true, true);
    }
    /// <summary>
    /// One Off
    /// </summary>
    public void Back_1Off(int speed, int repeat, bool bounce, bool split)
    {
        //Turn them all On
        foreach (int la in Lights.Backlights)
        {
            _controller.Write(la, PinValue.High);
        }

        for (int n = 0; n < repeat; n++)
        {
            for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
            {
                _controller.Write(Lights.Backlights[i], PinValue.Low);
                if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.Low);
                Thread.Sleep(speed);
                _controller.Write(Lights.Backlights[i], PinValue.High);
                if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.High);
                if (split && i == 3) break;
            }
            if (bounce)
            {
                Array.Reverse(Lights.Backlights);
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    _controller.Write(Lights.Backlights[i], PinValue.Low);
                    if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.Low);
                    Thread.Sleep(speed);
                    _controller.Write(Lights.Backlights[i], PinValue.High);
                    if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.High);
                    if (split && i == 3) break;
                }
                Array.Reverse(Lights.Backlights);
            }
        }

        //Turn them all Off
        foreach (int la in Lights.Backlights)
        {
            _controller.Write(la, PinValue.Low);
        }
    }


    /// <summary>
    /// One On
    /// </summary>
    public void Back_1LOn_Fast_NoBoun_NoSplit(int repeat)
    {
        Back_1LOn(75, repeat, false, false);
    }
    /// <summary>
    /// One On
    /// </summary>
    public void Back_1LOn_Slow_NoBoun_NoSplit(int repeat)
    {
        Back_1LOn(110, repeat, false, false);
    }
    /// <summary>
    /// One On
    /// </summary>
    public void Back_1LOn_Fast_Bounce_NoSplit(int repeat)
    {
        Back_1LOn(75, repeat, true, false);
    }
    /// <summary>
    /// One On
    /// </summary>
    public void Back_1LOn_Slow_Bounce_NoSplit(int repeat)
    {
        Back_1LOn(110, repeat, true, false);
    }
    /// <summary>
    /// One On
    /// </summary>
    public void Back_1LOn_Fast_NoBoun_Split(int repeat)
    {
        Back_1LOn(75, repeat, false, true);
    }
    /// <summary>
    /// One On
    /// </summary>
    public void Back_1LOn_Slow_NoBoun_Split(int repeat)
    {
        Back_1LOn(110, repeat, false, true);
    }
    /// <summary>
    /// One On
    /// </summary>
    public void Back_1LOn_Fast_Bounce_Split(int repeat)
    {
        Back_1LOn(75, repeat, true, true);
    }
    /// <summary>
    /// One On
    /// </summary>
    public void Back_1LOn_Slow_Bounce_Split(int repeat)
    {
        Back_1LOn(110, repeat, true, true);
    }
    /// <summary>
    /// One On
    /// </summary>
    public void Back_1LOn(int speed, int repeat, bool bounce, bool split)
    {
        for (int n = 0; n < repeat; n++)
        {
            for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
            {
                _controller.Write(Lights.Backlights[i], PinValue.High);
                //Console.WriteLine("Count Left=" + i.ToString() + "  Count Right=" + (i + 4).ToString());
                if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.High);
                Thread.Sleep(speed);
                _controller.Write(Lights.Backlights[i], PinValue.Low);
                if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.Low);
                if (split && i == 3) break;
            }
            if (bounce)
            {
                Array.Reverse(Lights.Backlights);
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    _controller.Write(Lights.Backlights[i], PinValue.High);
                    //Console.WriteLine("Count Left=" + i.ToString() + "  Count Right=" + (i + 4).ToString());
                    if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.High);
                    Thread.Sleep(speed);
                    _controller.Write(Lights.Backlights[i], PinValue.Low);
                    if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.Low);
                    if (split && i == 3) break;
                }
                Array.Reverse(Lights.Backlights);
            }
        }
    }


    /// <summary>
    /// On Off
    /// </summary>
    public void Back_OnOf_Fast_NoBoun_NoSplit(int repeat)
    {
        Back_OnOf(70, repeat, false, false);
    }
    /// <summary>
    /// On Off
    /// </summary>
    public void Back_OnOf_Slow_NoBoun_NoSplit(int repeat)
    {
        Back_OnOf(105, repeat, false, false);
    }
    /// <summary>
    /// On Off
    /// </summary>
    public void Back_OnOf_Fast_Bounce_NoSplit(int repeat)
    {
        Back_OnOf(70, repeat, true, false);
    }
    /// <summary>
    /// On Off
    /// </summary>
    public void Back_OnOf_Slow_Bounce_NoSplit(int repeat)
    {
        Back_OnOf(70, repeat, true, false);
    }
    /// <summary>
    /// On Off
    /// </summary>
    public void Back_OnOf_Fast_NoBoun_Split(int repeat)
    {
        Back_OnOf(70, repeat, false, true);
    }
    /// <summary>
    /// On Off
    /// </summary>
    public void Back_OnOf_Slow_NoBoun_Split(int repeat)
    {
        Back_OnOf(70, repeat, false, true);
    }
    /// <summary>
    /// On Off
    /// </summary>
    public void Back_OnOf_Fast_Bounce_Split(int repeat)
    {
        Back_OnOf(70, repeat, true, true);
    }
    /// <summary>
    /// On Off
    /// </summary>
    public void Back_OnOf_Slow_Bounce_Split(int repeat)
    {
        Back_OnOf(70, repeat, true, true);
    }

    /// <summary>
    /// Backligts all turn on one by one then turn off one by one
    /// </summary>
    /// <param name="speed">The time in millaseconds between each light coming one and going off 20 is kinda fast</param>
    /// <param name="repeat">How many times should it run</param>
    /// <param name="bounce">true = is left then right, false = just left</param>
    /// <param name="split">left 4 and the right 4 chase seprately, false = all 8 light chace together</param>
    public void Back_OnOf(int speed, int repeat, bool bounce, bool split)
    {
        var addUpSleepTime = 0;

        for (int n = 0; n < repeat; n++)
        {
            for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
            {
                _controller.Write(Lights.Backlights[i], PinValue.High);
                if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.High);
                addUpSleepTime += speed;
                Thread.Sleep(speed);
                if (split && i == 3) break;
            }
            for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
            {
                _controller.Write(Lights.Backlights[i], PinValue.Low);
                if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.Low);
                addUpSleepTime += speed;
                Thread.Sleep(speed);
                if (split && i == 3) break;
            }
            if (bounce)
            {
                Array.Reverse(Lights.Backlights);
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    _controller.Write(Lights.Backlights[i], PinValue.High);
                    if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.High);
                    addUpSleepTime += speed;
                    Thread.Sleep(speed);
                    if (split && i == 3) break;
                }
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    _controller.Write(Lights.Backlights[i], PinValue.Low);
                    if (split) _controller.Write(Lights.Backlights[i + 4], PinValue.Low);
                    addUpSleepTime += speed;
                    Thread.Sleep(speed);
                    if (split && i == 3) break;
                }
                Array.Reverse(Lights.Backlights);

            }
            //Console.WriteLine("Total Sleep Per Repeat = " + addUpSleepTime);
        }

    }


    public void Back_Color_Red()
    {
        Back_Color("Red", false, 0);
    }
    public void Back_Color_Green()
    {
        Back_Color("Green", false, 0);
    }
    public void Back_Color_Blue()
    {
        Back_Color("Blue", false, 0);
    }
    public void Back_Color_Yellow()
    {
        Back_Color("Yellow", false, 0);
    }
    public void Back_Color_Randon()
    {
        Back_Color("Random", false, 0);
    }
    public void Back_Color_Black()
    {
        Back_Color("Black", false, 0);
    }
    public void Back_Color_Red_Dur(int duration)
    {
        Back_Color("Red", false, duration);
    }
    public void Back_Color_Green_Dur(int duration)
    {
        Back_Color("Green", false, duration);
    }
    public void Back_Color_Blue_Dur(int duration)
    {
        Back_Color("Blue", false, duration);
    }
    public void Back_Color_Yellow_Dur(int duration)
    {
        Back_Color("Yellow", false, duration);
    }

    /// <summary>
    /// Backligts turn on one color.
    /// </summary>
    /// <param name="color">Red, Green, Blue, Yellow, Black, Random</param>
    /// <param name="clear">true = Turn off all backligts before turning on the color. </param>
    /// <param name="duration">0 = leave them on </param>
    public void Back_Color(string color, bool clear, int duration)
    {
        //Console.WriteLine("Back_Color - Color = " + c);
        //Console.WriteLine("Back_Color - Clear = " + clr);

        if (color == "Black" || clear)
        {
            foreach (var light in Lights.Backlights)
            {
                _controller.Write(light, PinValue.Low);
            }
        }

        if (color == "Random")
        {
            var random = new Random();
            var list = new List<string> { "Red", "Green", "Blue", "Yellow" };
            int index = random.Next(list.Count);
            color = list[index];
        }
        switch (color)
        {
            case "Red":
                _controller.Write(Lights.Backlights[0], PinValue.High);
                _controller.Write(Lights.Backlights[4], PinValue.High);
                break;
            case "Green":
                _controller.Write(Lights.Backlights[1], PinValue.High);
                _controller.Write(Lights.Backlights[5], PinValue.High);
                break;
            case "Blue":
                _controller.Write(Lights.Backlights[2], PinValue.High);
                _controller.Write(Lights.Backlights[6], PinValue.High);
                break;
            case "Yellow":
                _controller.Write(Lights.Backlights[3], PinValue.High);
                _controller.Write(Lights.Backlights[7], PinValue.High);
                break;
        }
        if (duration > 0)
        {
            Thread.Sleep(duration);
            switch (color)
            {
                case "Red":
                    _controller.Write(Lights.Backlights[0], PinValue.Low);
                    _controller.Write(Lights.Backlights[4], PinValue.Low);
                    break;
                case "Green":
                    _controller.Write(Lights.Backlights[1], PinValue.Low);
                    _controller.Write(Lights.Backlights[5], PinValue.Low);
                    break;
                case "Blue":
                    _controller.Write(Lights.Backlights[2], PinValue.Low);
                    _controller.Write(Lights.Backlights[6], PinValue.Low);
                    break;
                case "Yellow":
                    _controller.Write(Lights.Backlights[3], PinValue.Low);
                    _controller.Write(Lights.Backlights[7], PinValue.Low);
                    break;
            }
        }
    }



    /// <summary>
    /// Down stage lights are controlled by PWM and as such can be dimmed.
    /// </summary>
    /// <param name="speed">int 1 is fast, 15 is slow</param>
    /// <param name="up">bool true = fade up brighter, false = fade down dimmer</param>
    /// <param name="keyLights">bool true = lights at the top, false = foot lamps</param>
    /// <param name="max">double 1.0 = full on when fading up, 0.5 is half </param>
    /// <param name="min">double 0.0 = is off, 0.5 is half </param>
    /// <param name="start">double 0.0 = is off, 0.5 is half </param>
    public void DownStage(int speed, bool up, bool keyLights, double max, double min, double start)
    {
        //Console.WriteLine("DownStage()");
        int keyLites = 1;
        if (keyLights) keyLites = 0;
        //Console.WriteLine("DownStageLights speed=" + s.ToString() + 
        //    " up=" + u.ToString() + 
        //    " key=" + keyLites.ToString() +
        //    " mx=" + mx +
        //    " mn=" + mn + 
        //    " st=" + st);

        var pwm = PwmChannel.Create(0, keyLites, 400, start);
        pwm.Start();

        if (speed > 0)
        {
            double fadeStatus = start;
            while (true)
            {
                if (up)
                {
                    fadeStatus = fadeStatus + 0.001;
                    //Console.WriteLine("Up fadeStatus=" + fadeStatus);
                }
                else
                {
                    fadeStatus = fadeStatus - 0.001;
                    //Console.WriteLine("Down fadeStatus=" + fadeStatus);
                }
                if (fadeStatus > max && up) break;
                if (fadeStatus < min && !up) break;

                pwm.DutyCycle = fadeStatus;
                Thread.Sleep(speed);
            }
        }
    }

    public void PlayBtnBlink()
    {
        var onStatus = false;
        while (true)
        {
            if (Program.songPlaying)
            {
                _controller.Write(Lights.PlayBtnGreen, PinValue.Low);
                if (onStatus)
                {
                    _controller.Write(Lights.PlayBtnRed, PinValue.Low);
                    onStatus = false;
                }
                else
                {
                    _controller.Write(Lights.PlayBtnRed, PinValue.High);
                    onStatus = true;
                }
            }
            else
            {
                _controller.Write(Lights.PlayBtnRed, PinValue.Low);
                if (onStatus)
                {
                    _controller.Write(Lights.PlayBtnGreen, PinValue.Low);
                    onStatus = false;
                }
                else
                {
                    _controller.Write(Lights.PlayBtnGreen, PinValue.High);
                    onStatus = true;
                }
            }
            Thread.Sleep(500);
        }
    }

    public void PlayBtnGreen()
    {
        _controller.Write(Lights.PlayBtnGreen, PinValue.High);
        _controller.Write(Lights.PlayBtnRed, PinValue.Low);
    }
    public void PlayBtnRed()
    {
        _controller.Write(Lights.PlayBtnGreen, PinValue.Low);
        _controller.Write(Lights.PlayBtnRed, PinValue.High);

    }
}

