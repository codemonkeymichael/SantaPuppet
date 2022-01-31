using Iot.Device.Mcp23xxx;
using System.Collections.Generic;
using System.Device.I2c;
using System.Device.Pwm;

namespace SantaPuppet.Cues;

public class LightCues
{
    public enum Color
    {
        Red,
        Green,
        Blue,
        Yellow,
        Black,
        Random
    }

    private double footLiteLevel = 1.0;
    private double keyLiteLevel = 0.0;
    private PwmChannel foot;
    private PwmChannel key;

    public LightCues()
    {
        foot = PwmChannel.Create(0, 1, 400, footLiteLevel);
        foot.Start();
        key = PwmChannel.Create(0, 0, 400, keyLiteLevel);
        key.Start();
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
            I2CJobQueue.EnqueueLightJob(light, PinValue.High);
        }

        Thread.Sleep(duration);

        foreach (int light in Lights.Backlights)
        {
            I2CJobQueue.EnqueueLightJob(light, PinValue.Low);
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

                I2CJobQueue.EnqueueLightJob(Lights.Backlights[left], PinValue.High);

                addUpSleepTime += duration / 2;
                Thread.Sleep(duration / 2);

                I2CJobQueue.EnqueueLightJob(Lights.Backlights[right], PinValue.High);

                addUpSleepTime += duration / 2;
                Thread.Sleep(duration / 2);

                I2CJobQueue.EnqueueLightJob(Lights.Backlights[left], PinValue.Low);

                addUpSleepTime += duration / 2;
                Thread.Sleep(duration / 2);

                I2CJobQueue.EnqueueLightJob(Lights.Backlights[right], PinValue.Low);
            }
            else
            {
                var light = rand.Next(0, 8);

                I2CJobQueue.EnqueueLightJob(Lights.Backlights[light], PinValue.High);

                addUpSleepTime += duration;
                Thread.Sleep(duration);

                I2CJobQueue.EnqueueLightJob(Lights.Backlights[light], PinValue.Low);
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
        foreach (int light in Lights.Backlights)
        {
            I2CJobQueue.EnqueueLightJob(light, PinValue.High);
        }

        for (int n = 0; n < repeat; n++)
        {
            for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
            {
                I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.Low);

                if (split)
                {
                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.Low);
                }
                Thread.Sleep(speed);

                I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.High);

                if (split)
                {
                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.High);
                }
                if (split && i == 3) break;
            }
            if (bounce)
            {
                Array.Reverse(Lights.Backlights);
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.Low);

                    if (split)
                    {
                        I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.Low);
                    }
                    Thread.Sleep(speed);

                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.High);

                    if (split)
                    {
                        I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.High);
                    };
                    if (split && i == 3) break;
                }
                Array.Reverse(Lights.Backlights);
            }
        }

        //Turn them all Off
        foreach (int light in Lights.Backlights)
        {
            I2CJobQueue.EnqueueLightJob(light, PinValue.Low);
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
    /// Speed 75, Bounce true, Split (left and right) true 
    /// </summary>
    /// <param name="repeat">How mant times to bounce</param>
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
                I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.High);

                //Console.WriteLine("Count Left=" + i.ToString() + "  Count Right=" + (i + 4).ToString());
                if (split)
                {
                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.High);
                }
                Thread.Sleep(speed);

                I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.Low);

                if (split)
                {
                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.Low);
                }
                if (split && i == 3) break;
            }
            if (bounce)
            {
                Array.Reverse(Lights.Backlights);
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.High);

                    //Console.WriteLine("Count Left=" + i.ToString() + "  Count Right=" + (i + 4).ToString());
                    if (split)
                    {
                        I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.High);
                    }
                    Thread.Sleep(speed);

                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.Low);

                    if (split)
                    {
                        I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.Low);
                    }
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
                I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.High);

                if (split)
                {
                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.High);
                }
                addUpSleepTime += speed;
                Thread.Sleep(speed);
                if (split && i == 3) break;
            }
            for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
            {
                I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.Low);

                if (split)
                {
                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.Low);
                }
                addUpSleepTime += speed;
                Thread.Sleep(speed);
                if (split && i == 3) break;
            }
            if (bounce)
            {
                Array.Reverse(Lights.Backlights);
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.High);
                    if (split)
                    {
                        I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.High);
                    }
                    addUpSleepTime += speed;
                    Thread.Sleep(speed);
                    if (split && i == 3) break;
                }
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    I2CJobQueue.EnqueueLightJob(Lights.Backlights[i], PinValue.Low);

                    if (split)
                    {
                        I2CJobQueue.EnqueueLightJob(Lights.Backlights[i + 4], PinValue.Low);
                    }
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
        Back_Color(Color.Red, false, 0);
    }
    public void Back_Color_Green()
    {
        Back_Color(Color.Green, false, 0);
    }
    public void Back_Color_Blue()
    {
        Back_Color(Color.Blue, false, 0);
    }
    public void Back_Color_Yellow()
    {
        Back_Color(Color.Yellow, false, 0);
    }
    public void Back_Color_Random()
    {
        Back_Color(Color.Random, false, 0);
    }
    public void Back_Color_Black()
    {
        Back_Color(Color.Black, false, 0);
    }
    public void Back_Color_Red_Dur(int duration)
    {
        Back_Color(Color.Red, false, duration);
    }
    public void Back_Color_Green_Dur(int duration)
    {
        Back_Color(Color.Green, false, duration);
    }
    public void Back_Color_Blue_Dur(int duration)
    {
        Back_Color(Color.Blue, false, duration);
    }
    public void Back_Color_Yellow_Dur(int duration)
    {
        Back_Color(Color.Yellow, false, duration);
    }

    /// <summary>
    /// Backligts turn on one color.
    /// </summary>
    /// <param name="color">Red, Green, Blue, Yellow, Black, Random</param>
    /// <param name="clear">true = Turn off all backligts before turning on the color. </param>
    /// <param name="duration">0 = leave them on </param>
    public void Back_Color(Color color, bool clear, int duration)
    {
        Console.WriteLine("Back_Color - Color = " + color);

        if (color == Color.Black || clear)
        {
            foreach (var light in Lights.Backlights)
            {
                I2CJobQueue.EnqueueLightJob(light, PinValue.Low);
            }
        }


        if (color == Color.Random)
        {
            Array values = Enum.GetValues(typeof(Color));
            Random random = new Random();
            while (color == Color.Random || color == Color.Black)
            {
                color = (Color)values.GetValue(random.Next(values.Length));
            }
        }

        int pin1 = 0;
        int pin2 = 0;

        switch (color)
        {
            case Color.Red:
                pin1 = 0;
                pin2 = 4;
                break;
            case Color.Green:
                pin1 = 1;
                pin2 = 5;
                break;
            case Color.Blue:
                pin1 = 2;
                pin2 = 6;
                break;
            case Color.Yellow:
                pin1 = 3;
                pin2 = 7;
                break;
        }
        I2CJobQueue.EnqueueLightJob(pin1, PinValue.High);
        I2CJobQueue.EnqueueLightJob(pin2, PinValue.High);

        if (duration > 0)
        {
            Thread.Sleep(duration);

            I2CJobQueue.EnqueueLightJob(pin1, PinValue.Low);
            I2CJobQueue.EnqueueLightJob(pin2, PinValue.Low);
        }
    }



    /// <summary>
    /// Down stage lights are controlled by PWM and as such can be dimmed.
    /// </summary>
    /// <param name="speed">int 1 is fast, 15 is slow. In micro seconds between steps.</param>
    /// <param name="up">bool true = fade up brighter, false = fade down dimmer</param>
    /// <param name="keyLights">bool true = lights at the top, false = foot lamps</param>
    /// <param name="level">double 1.0 = full on when fading up, 0.5 is half </param>
    public void DownStage(int speed, bool up, bool keyLights, double level)
    {
       
        double startLevel = footLiteLevel;       
        if (keyLights) startLevel = keyLiteLevel;
        //Console.WriteLine("DownStage() keyLights=" + keyLights + " startLevel=" + startLevel);

        //Console.WriteLine("DownStageLights speed=" + s.ToString() + 
        //    " up=" + u.ToString() + 
        //    " key=" + keyLites.ToString() +
        //    " mx=" + mx +
        //    " mn=" + mn + 
        //    " st=" + st);  

        if (speed > 0)
        {
            double fadeStatus = startLevel;
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
                if (fadeStatus > level && up) break;
                if (fadeStatus < level && !up) break;

                if (keyLights)
                {
                    key.DutyCycle = fadeStatus;
                }
                else
                {
                    foot.DutyCycle = fadeStatus;
                    //Console.WriteLine(fadeStatus);
                }
            }
            Thread.Sleep(speed);
        }
    }


    public void PlayBtnBlink()
    {
        var onStatus = false;
        I2CJobQueue.EnqueueLightJob(Lights.PlayBtnRed, PinValue.Low);
        Program.piGPIOController.Write(Lights.PlayBtnGreen, PinValue.Low);
        //LightCues lc = new LightCues();
        //lc.DownStage(2, true, false, 1.0);



        while (true)
        {
            onStatus = !onStatus;
            if (Audio.player.IsPlaying)
            {
                I2CJobQueue.EnqueueLightJob(Lights.PlayBtnRed, PinValue.High);
                break;
            }
            else
            {
                if (onStatus)
                {
                    Program.piGPIOController.Write(Lights.PlayBtnGreen, PinValue.Low);
                }
                else
                {
                    Program.piGPIOController.Write(Lights.PlayBtnGreen, PinValue.High);
                }
            }
            Thread.Sleep(500);
        }
    }
}


