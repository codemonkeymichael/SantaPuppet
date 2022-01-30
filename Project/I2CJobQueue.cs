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
            var job = _queueList.Dequeue();
            if (job.PinMo == PinMode.Output)
            {
                Program.mcp20GPIOController.Write(job.Pin, job.PinVal);
            }
            else
            {
                _inputVal = Program.mcp20GPIOController.Read(job.Pin); 
            }           
        }     
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
        _inputVal = null;
        var job = new QueueModel();
        job.Pin = PinNum;
        job.PinMo = PinMode.Input;     
        _queueList.Enqueue(job);
        RunJobs();
        return _inputVal;
    }
}


