using Iot.Device.Mcp23xxx;
using System.Collections.Generic;
using System.Device.I2c;
using System.Device.Pwm;

namespace SantaPuppet.Cues;

public class LightCues
{
    private static GpioController _mcp20GPIOController;
    private static GpioController _piGPIOController;
    private static Queue<QueueModel> _queueList;

    public enum Color
    {
        Red,
        Green,
        Blue,
        Yellow,
        Black,
        Random
    }


    public LightCues(GpioController piGPIOController, GpioController mcp20GPIOController)
    {
        _piGPIOController = piGPIOController;
        _mcp20GPIOController = mcp20GPIOController;
        _queueList = new Queue<QueueModel>();
    }



    private void RunJobs()
    {
        //Console.WriteLine(_queueList.Count);
       while (_queueList.Count > 0)
        {
           var job = _queueList.Dequeue();
            _mcp20GPIOController.Write(job.Pin, job.PinValue);
        }
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
            _mcp20GPIOController.Write(light, PinValue.High);
        }

        Thread.Sleep(duration);

        foreach (int light in Lights.Backlights)
        {
            _mcp20GPIOController.Write(light, PinValue.Low);
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
                _mcp20GPIOController.Write(Lights.Backlights[left], PinValue.High);
                addUpSleepTime += duration / 2;
                Thread.Sleep(duration / 2);
                _mcp20GPIOController.Write(Lights.Backlights[right], PinValue.High);
                addUpSleepTime += duration / 2;
                Thread.Sleep(duration / 2);
                _mcp20GPIOController.Write(Lights.Backlights[left], PinValue.Low);
                addUpSleepTime += duration / 2;
                Thread.Sleep(duration / 2);
                _mcp20GPIOController.Write(Lights.Backlights[right], PinValue.Low);
            }
            else
            {
                var light = rand.Next(0, 8);
                _mcp20GPIOController.Write(Lights.Backlights[light], PinValue.High);
                addUpSleepTime += duration;
                Thread.Sleep(duration);
                _mcp20GPIOController.Write(Lights.Backlights[light], PinValue.Low);
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
            _mcp20GPIOController.Write(la, PinValue.High);
        }

        for (int n = 0; n < repeat; n++)
        {
            for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
            {
                _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.Low);
                if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.Low);
                Thread.Sleep(speed);
                _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.High);
                if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.High);
                if (split && i == 3) break;
            }
            if (bounce)
            {
                Array.Reverse(Lights.Backlights);
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.Low);
                    if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.Low);
                    Thread.Sleep(speed);
                    _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.High);
                    if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.High);
                    if (split && i == 3) break;
                }
                Array.Reverse(Lights.Backlights);
            }
        }

        //Turn them all Off
        foreach (int la in Lights.Backlights)
        {
            _mcp20GPIOController.Write(la, PinValue.Low);
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
                _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.High);
                //Console.WriteLine("Count Left=" + i.ToString() + "  Count Right=" + (i + 4).ToString());
                if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.High);
                Thread.Sleep(speed);
                _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.Low);
                if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.Low);
                if (split && i == 3) break;
            }
            if (bounce)
            {
                Array.Reverse(Lights.Backlights);
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.High);
                    //Console.WriteLine("Count Left=" + i.ToString() + "  Count Right=" + (i + 4).ToString());
                    if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.High);
                    Thread.Sleep(speed);
                    _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.Low);
                    if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.Low);
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
                _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.High);
                if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.High);
                addUpSleepTime += speed;
                Thread.Sleep(speed);
                if (split && i == 3) break;
            }
            for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
            {
                _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.Low);
                if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.Low);
                addUpSleepTime += speed;
                Thread.Sleep(speed);
                if (split && i == 3) break;
            }
            if (bounce)
            {
                Array.Reverse(Lights.Backlights);
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.High);
                    if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.High);
                    addUpSleepTime += speed;
                    Thread.Sleep(speed);
                    if (split && i == 3) break;
                }
                for (int i = 0; i < Lights.Backlights.Count() - 1; i++)
                {
                    _mcp20GPIOController.Write(Lights.Backlights[i], PinValue.Low);
                    if (split) _mcp20GPIOController.Write(Lights.Backlights[i + 4], PinValue.Low);
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
                var job0 = new QueueModel();
                job0.Pin = light;
                job0.PinValue = PinValue.Low;
                _queueList.Enqueue(job0);
                RunJobs();

            }
        }

        Color randColor = Color.Black;
        if (color == Color.Random)
        {        
            Array values = Enum.GetValues(typeof(Color));
            Random random = new Random();
            while (randColor == Color.Random || randColor == Color.Black)
            {
                randColor = (Color)values.GetValue(random.Next(values.Length));
            }  
        }

        int pin1 = 0;
        int pin2 = 0;

        switch (randColor)
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
        var job1 = new QueueModel();
        job1.Pin = pin1;
        job1.PinValue = PinValue.High;
        _queueList.Enqueue(job1);
        RunJobs();
        var job2 = new QueueModel();
        job2.Pin = pin2;
        job2.PinValue = PinValue.High;
        _queueList.Enqueue(job2);
        RunJobs();
        if (duration > 0)
        {
            Thread.Sleep(duration);
            var job3 = new QueueModel();
            job3.Pin = pin1;
            job3.PinValue = PinValue.High;
            _queueList.Enqueue(job3);
            RunJobs();
            var job4 = new QueueModel();
            job4.Pin = pin2;
            job4.PinValue = PinValue.High;
            _queueList.Enqueue(job4);
            RunJobs();
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
        _mcp20GPIOController.Write(Lights.PlayBtnRed, PinValue.Low);
        _piGPIOController.Write(Lights.PlayBtnGreen, PinValue.Low);
        while (true)
        {
            onStatus = !onStatus;
            if (Program.songPlaying)
            {
                _mcp20GPIOController.Write(Lights.PlayBtnRed, PinValue.High);
                break;
            }
            else
            {
                if (onStatus)
                {
                    //Console.WriteLine("Play Btn blink green off");
                    _piGPIOController.Write(Lights.PlayBtnGreen, PinValue.Low);
                }
                else
                {
                    //Console.WriteLine("Play Btn blink green on Is Pin Open ="  + _mcp20GPIOController.IsPinOpen(Lights.PlayBtnGreen));
                    _piGPIOController.Write(Lights.PlayBtnGreen, PinValue.High);
                }
            }
            Thread.Sleep(500);
        }
    }

    public void PlayBtnGreen()
    {
        _mcp20GPIOController.Write(Lights.PlayBtnGreen, PinValue.High);
        _mcp20GPIOController.Write(Lights.PlayBtnRed, PinValue.Low);
    }
    public void PlayBtnRed()
    {
        _mcp20GPIOController.Write(Lights.PlayBtnGreen, PinValue.Low);
        _mcp20GPIOController.Write(Lights.PlayBtnRed, PinValue.High);

    }
}

