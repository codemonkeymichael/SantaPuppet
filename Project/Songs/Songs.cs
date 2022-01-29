using SantaPuppet.Cues;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet.Songs;


public class Songs
{

    public List<CueModel> _Cues = new List<CueModel>();


    public void AddCue(int CueTime, Action CueAction, string CueName, int CueTimeMin = 0)
    {
        CueModel c = new CueModel();
        c.CueTime = CueTime;
        c.CueTimeMin = CueTimeMin;
        c.CueAction = CueAction;
        c.CueName = CueName;
        _Cues.Add(c);
    }
}
