using LibVLCSharp.Shared;

namespace SantaPuppet;

public static class I2CJobQueue
{

    private static Queue<QueueModel> _queueList = new Queue<QueueModel>();

    public static PinValue? RunJobs()
    {
        while (_queueList.Count > 0)
        {
            var job = _queueList.Dequeue();
            if (job.PinMo == PinMode.Output)
            {
                Program.mcp20GPIOController.Write(job.Pin, job.PinVal);
                return job.PinVal;          
            }
            else
            {
               return Program.mcp20GPIOController.Read(job.Pin);
            }
        }
        return null;

    }

    public static void EnqueueLightJob(int PinNum, PinValue PinVal)
    {
        var job = new QueueModel();
        job.Pin = PinNum;
        job.PinVal = PinVal;
        job.PinMo = PinMode.Output;
        _queueList.Enqueue(job);
        RunJobs();
    }

    public static PinValue? EnqueueInputCheck(int PinNum)
    {
        var job = new QueueModel();
        job.Pin = PinNum;
        job.PinMo = PinMode.Input;     
        _queueList.Enqueue(job);
        return RunJobs();    
    }
}


