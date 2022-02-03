using LibVLCSharp.Shared;

namespace SantaPuppet;

public static class I2CJobQueue
{

    private static Queue<QueueModel> _queueList = new Queue<QueueModel>();
    private static PinValue? _inputVal = null;

    public static void RunJobs()
    {
        while (_queueList.Count > 0)
        {
            //Console.WriteLine("RunJob Queue Count " + _queueList.Count );
            var job = _queueList.Dequeue();

            if (job.PinMo == PinMode.Output)
            {
                Program.mcp20GPIOController.Write(job.Pin, job.PinVal);
            }
            else
            {
                _inputVal = Program.mcp20GPIOController.Read(job.Pin);
            }
            //Console.WriteLine("RunJob Queue Count " + _queueList.Count + "  Pin " + job.Pin + "  Mode " + job.PinMo);
            //Thread.Sleep(10);
        }
    }

    public static void EnqueueLightJob(int PinNum, PinValue PinVal)
    {
        //Console.WriteLine("EnqueueLightJob PinNum=" + PinNum + " PinVal=" + PinVal);
        var job = new QueueModel();
        job.Pin = PinNum;
        job.PinVal = PinVal;
        job.PinMo = PinMode.Output;
        _queueList.Enqueue(job);
        RunJobs();
    }

    public static void PlayButtonCheck()
    {
        //Loop to check play button status
        while (true)
        {
            _inputVal = null;
            var job = new QueueModel();
            job.Pin = Inputs.PlayButton;
            job.PinMo = PinMode.Input;
            _queueList.Enqueue(job);
            RunJobs();
            if (_inputVal == PinValue.High)
            {
                Inputs.PlayButtonTrigger = true;
                //Console.WriteLine("PlayButtonCheck TRUE");
                break;
            }
            else
            {
                Inputs.PlayButtonTrigger = false;
                //Console.WriteLine("PlayButtonCheck FALSE");
            }
            Thread.Sleep(5);
        }
    }

    public static void CurtinOpenCheck()
    {
        _inputVal = null;
        var job = new QueueModel();
        job.Pin = Inputs.CurtinStageLeftStopOpen;
        job.PinMo = PinMode.Input;
        _queueList.Enqueue(job);
        RunJobs();
        if (_inputVal == PinValue.High)
        {
            Inputs.CurtinStageLeftStopOpenTrigger = true;
            //Console.WriteLine("CurtinOpenCheck TRUE");
        }
        else
        {
            Inputs.CurtinStageLeftStopOpenTrigger = false;
            //Console.WriteLine("CurtinOpenCheck FALSE");
        }
    }

    public static void CurtinCloseCheck()
    {
        _inputVal = null;
        var job = new QueueModel();
        job.Pin = Inputs.CurtinStageRightStopClosed;
        job.PinMo = PinMode.Input;
        _queueList.Enqueue(job);
        RunJobs();
        if (_inputVal == PinValue.High)
        {
            Inputs.CurtinStageRightStopClosedTrigger = true;
            //Console.WriteLine("CurtinOpenCheck TRUE");
        }
        else
        {
            Inputs.CurtinStageRightStopClosedTrigger = false;
            //Console.WriteLine("CurtinOpenCheck FALSE");
        }
    }

    public static PinValue? EnqueueInputCheck(int PinNum)
    {
        _inputVal = null;
        var job = new QueueModel();
        job.Pin = PinNum;
        job.PinMo = PinMode.Input;
        _queueList.Enqueue(job);
        RunJobs();
        return _inputVal;
    }
}


