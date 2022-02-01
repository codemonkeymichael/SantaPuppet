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

    public List<CueModel> _CuesLite = new List<CueModel>();
    public List<CueModel> _CuesAnim = new List<CueModel>();
    public List<CueModel> _CuesCurtin = new List<CueModel>();
    public List<CueModel> _CuesTalk = new List<CueModel>();

    public void AddLiteCue(int CueTime, Action CueAction, string CueName, int CueTimeMin = 0)
    {
        Console.WriteLine("AddLiteCue " + CueName);
        CueModel c = new CueModel();
        c.CueTime = CueTime;
        c.CueTimeMin = CueTimeMin;
        c.CueAction = CueAction;
        c.CueName = CueName;
        _CuesLite.Add(c);
    }
    public void AddAnimCue(int CueTime, Action CueAction, string CueName, int CueTimeMin = 0)
    {
        CueModel c = new CueModel();
        c.CueTime = CueTime;
        c.CueTimeMin = CueTimeMin;
        c.CueAction = CueAction;
        c.CueName = CueName;
        _CuesAnim.Add(c);
    }
    public void AddCurtinCue(int CueTime, Action CueAction, string CueName, int CueTimeMin = 0)
    {
        CueModel c = new CueModel();
        c.CueTime = CueTime;
        c.CueTimeMin = CueTimeMin;
        c.CueAction = CueAction;
        c.CueName = CueName;
        _CuesCurtin.Add(c);
    }
    public void AddTalkCue(int CueTime, Action CueAction, string CueName, int CueTimeMin = 0)
    {
        CueModel c = new CueModel();
        c.CueTime = CueTime;
        c.CueTimeMin = CueTimeMin;
        c.CueAction = CueAction;
        c.CueName = CueName;
        _CuesTalk.Add(c);
    }
}
