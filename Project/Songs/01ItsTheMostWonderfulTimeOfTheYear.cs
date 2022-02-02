using SantaPuppet.Cues;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet.Songs;

public class ItsTheMostWonderfulTimeOfTheYear : Songs
{

    public ItsTheMostWonderfulTimeOfTheYear()
    {
        cueStackLights();
        cueStackCurtin();
    }
    public SongModel cueStack()
    {
        //Build the Song
        SongModel song = new SongModel();
        song.Title = "Its The Most Wonderful Time Of The Year";
        song.SongPath = "01ItsTheMostWonderfulTimeOfTheYear.wav";
        song.CuesLite = _CuesLite;
        song.CuesCurtin = _CuesCurtin;

        return song;
    }

    private List<CueModel> cueStackCurtin()
    {
        CurtinCues cur = new CurtinCues();
        AddCurtinCue(6000, () => cur.OpenClose(true, 5), "Open Curtin", 0);
        AddCurtinCue(25435, () => cur.OpenClose(false, 5), "Close Curtin",2);
        return _CuesCurtin;

  
    }

    private List<CueModel> cueStackLights()
    {
        //Build the lighting cues
        LightCues lites = new LightCues(); 

        AddLiteCue(50, () => lites.DownStage(200, false, false, 0.05), "Fade down foot lights");
        AddLiteCue(1000, () => lites.Back_Color_Green(), "Backlights Green Color");
        AddLiteCue(2000, () => lites.Back_Color_Blue(), "Backlights Blue Color");
        AddLiteCue(6107, () => lites.DownStage(75, true, true, 0.25), "Fade up Key Lights");
        AddLiteCue(6127, () => lites.DownStage(85, true, false, 0.25), "Fade up foot lights");
        AddLiteCue(10866, () => lites.Back_Color_Red_Dur(350), "Ding 1");
        AddLiteCue(11646, () => lites.Back_Color_Yellow_Dur(350), "Dong");       
        AddLiteCue(12541, () => lites.Back_Color_Red_Dur(350), "Ding");
        AddLiteCue(13439, () => lites.Back_Color_Yellow_Dur(350), "Dong");
        AddLiteCue(19180, () => lites.Back_Color_Red(), "Ahhh");
        AddLiteCue(19184, () => lites.Back_Color_Yellow(), "Ahhh");
        AddLiteCue(24830, () => lites.Back_Color_Red_Dur(15), "Ding 2");
        AddLiteCue(25750, () => lites.Back_Color_Yellow_Dur(15), "Dong");
        AddLiteCue(26394, () => lites.Back_Color_Red_Dur(350), "Ding");
        AddLiteCue(27558, () => lites.Back_Color_Yellow_Dur(350), "Dong");
        AddLiteCue(32041, () => lites.Back_Color_Red_Dur(350), "Ding 3");
        AddLiteCue(32831, () => lites.Back_Color_Yellow_Dur(350), "Dong");
        AddLiteCue(33512, () => lites.Back_Color_Red_Dur(350), "Ding");
        AddLiteCue(34369, () => lites.Back_Color_Yellow_Dur(350), "Dong");
        AddLiteCue(40242, () => lites.Back_Color_Red(), "Ahhh");
        AddLiteCue(40246, () => lites.Back_Color_Yellow(), "Ahhh");
        AddLiteCue(46177, () => lites.Back_Color_Red_Dur(15), "Ding");
        AddLiteCue(46872, () => lites.Back_Color_Yellow_Dur(15), "Dong");
        AddLiteCue(47924, () => lites.Back_Color_Red_Dur(350), "Ding");
        AddLiteCue(48641, () => lites.Back_Color_Yellow_Dur(350), "Dong");
        AddLiteCue(19386, () => lites.Back_Color_Red(), "Its the most",1);
        AddLiteCue(19391, () => lites.Back_Color_Yellow(), "Its the most", 1);
        AddLiteCue(26149, () => lites.Back_Color_Black(), "Black", 1);
        AddLiteCue(27000, () => lites.Back_OnOf_Slow_Bounce_NoSplit(3), "Back Lights Chase", 1);//1960 Per Loop
        AddLiteCue(27015, () => lites.DownStage(100,false,true,0.05), "Horns Down Stage Key Off", 1);
        AddLiteCue(27030, () => lites.DownStage(100, false, false, 0.03), "Horns Down Stage Foot Off", 1);
        AddLiteCue(33708, () => lites.Back_Color_Red(), "Girls", 1);
        AddLiteCue(35812, () => lites.Back_Color_Yellow(), "Boys", 1);
        AddLiteCue(37424, () => lites.Back_Color_Black(), "Both - Red Yellow Off", 1); /////////////////////////////////////////////////////////////////
        AddLiteCue(37440, () => lites.Back_Color_Green(), "Both - Green", 1);
        AddLiteCue(37455, () => lites.Back_Color_Green(), "Both - Blue", 1);
        AddLiteCue(40700, () => lites.DownStage(40, true, true, 0.25), "All - Down Stage Key Up", 1);
        AddLiteCue(40750, () => lites.DownStage(40, true, true, 0.25), "All - Down Stage Foot Up", 1);
        AddLiteCue(53375, () => lites.Back_Color_Red(), "All", 1);
        AddLiteCue(53382, () => lites.Back_Color_Yellow(), "All", 1);
        AddLiteCue(575, () => lites.Back_Color_Black(), "Back to just him - Back lites out", 2);
        AddLiteCue(590, () => lites.Back_Color_Blue(), "Back to just him - Blue", 2);
        AddLiteCue(605, () => lites.Back_Color_Green(), "Back to just him - Green", 2);
        AddLiteCue(11049, () => lites.Back_Color_Green(), "Set-up Chase - Clear Back Lites", 2);
        AddLiteCue(11065, () => lites.Back_StrobeRandom_Slow_NoSplit(12), "Chaser Random No Split Slow", 2);
        AddLiteCue(15260, () => lites.Back_StrobeRandom_Slow_Split(12), "Chaser Random Split Slow", 2);
        AddLiteCue(20065, () => lites.Back_StrobeRandom_Fast_NoSplit(21), "Chaser Random No Split Fast", 2);
        AddLiteCue(23070, () => lites.Back_StrobeRandom_Fast_Split(12), "Chaser Random Split Fast", 2);
        AddLiteCue(26736, () => lites.Back_StrobeAll_Fast(), "Strobe 1 Back All Fast", 2);
        AddLiteCue(27063, () => lites.Back_StrobeAll_Fast(), "Strobe 2 Back All Fast", 2);
        AddLiteCue(27200, () => lites.DownStage(0, true, true, 1.0), "Strobe 3 Down Stage Key", 2);
        AddLiteCue(27215, () => lites.DownStage(0, true, false, 1.0), "Strobe 3 Down Stage Foot", 2);
        AddLiteCue(27550, () => lites.Back_StrobeAll_Fast(), "Strobe 3 Back All", 2);
        AddLiteCue(27700, () => lites.DownStage(10, false, true, 0.001), "Strobe 3 Down Stage Key Out", 2);
        AddLiteCue(27715, () => lites.DownStage(10, false, false, 0.001), "Strobe 3 Down Stage Foot Out", 2);

        return _CuesLite;
    }
}
