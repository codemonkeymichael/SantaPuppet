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
        AddCurtinCue(2000, () => cur.OpenClose(true, 5), "Open Curtin", 99);
        AddCurtinCue(25435, () => cur.OpenClose(false, 5), "Close Curtin",2);
        return _CuesCurtin;

  
    }

    private List<CueModel> cueStackLights()
    {
        //Build the lighting cues
        LightCues lites = new LightCues();

        AddLiteCue(14, () => lites.DownStage(0, false, true, 0.0, 0.0, 0.0), "Fade down foot lights");

        AddLiteCue(16, () => lites.Back_Color_Green(), "Backlights Green Color");

        AddLiteCue(18, () => lites.Back_Color_Blue(), "Backlights Blue Color");

        AddLiteCue(22, () => lites.DownStage(20, true, false, 0.05, 0.0, 0.0), "Fade up foot lights");

        AddLiteCue(6107, () => lites.DownStage(20, true, true, 0.25, 0.0, 0.00), "Fade up Key Lights");



        ////CueModel scene15 = new CueModel();
        ////scene15.CueTime = 10866;
        ////scene15.CueTimeMin = 0;
        ////scene15.CueAction = () => lites.Back_Color_Red_Dur(350);
        ////scene15.CueName = "Ding";
        ////Cues.Add(scene15);

        ////CueModel scene16 = new CueModel();
        ////scene16.CueTime = 11646;
        ////scene16.CueTimeMin = 0;
        ////scene16.CueAction = () => lites.Back_Color_Yellow_Dur(350);
        ////scene16.CueName = "Dong";
        ////Cues.Add(scene16);

        //CueModel scene17 = new CueModel();
        //scene17.CueTime = 12541;
        //scene17.CueTimeMin = 0;
        //scene17.CueAction = () => lites.Back_Color_Red_Dur(350);
        //scene17.CueName = "Ding";
        //Cues.Add(scene17);

        //CueModel scene18 = new CueModel();
        //scene18.CueTime = 13439;
        //scene18.CueTimeMin = 0;
        //scene18.CueAction = () => lites.Back_Color_Yellow_Dur(350);
        //scene18.CueName = "Dong";
        //Cues.Add(scene18);

        //CueModel scene19 = new CueModel();
        //scene19.CueTime = 19180;
        //scene19.CueTimeMin = 0;
        //scene19.CueAction = () => lites.Back_Color_Red();
        //scene19.CueName = "Ahhh";
        //Cues.Add(scene19);

        //CueModel scene20 = new CueModel();
        //scene20.CueTime = 19184;
        //scene20.CueTimeMin = 0;
        //scene20.CueAction = () => lites.Back_Color_Yellow();
        //scene20.CueName = "Ahhh";
        //Cues.Add(scene20);

        //CueModel scene21 = new CueModel();
        //scene21.CueTime = 24830;
        //scene21.CueTimeMin = 0;
        //scene21.CueAction = () => lites.Back_Color_Red_Dur(15); //Turn it off
        //scene21.CueName = "Ding";
        //Cues.Add(scene21);

        //CueModel scene22 = new CueModel();
        //scene22.CueTime = 25750;
        //scene22.CueTimeMin = 0;
        //scene22.CueAction = () => lites.Back_Color_Yellow_Dur(15); //Turn it off
        //scene22.CueName = "Dong";
        //Cues.Add(scene22);

        //CueModel scene23 = new CueModel();
        //scene23.CueTime = 26394;
        //scene22.CueTimeMin = 0;
        //scene23.CueAction = () => lites.Back_Color_Red_Dur(350);
        //scene23.CueName = "Ding";
        //Cues.Add(scene23);

        //CueModel scene24 = new CueModel();
        //scene24.CueTime = 27558;
        //scene24.CueTimeMin = 0;
        //scene24.CueAction = () => lites.Back_Color_Yellow_Dur(350);
        //scene24.CueName = "Dong";
        //Cues.Add(scene24);

        //CueModel scene25 = new CueModel();
        //scene25.CueTime = 32041;
        //scene25.CueTimeMin = 0;
        //scene25.CueAction = () => lites.Back_Color_Red_Dur(350);
        //scene25.CueName = "Ding";
        //Cues.Add(scene25);

        //CueModel scene26 = new CueModel();
        //scene26.CueTime = 32531;
        //scene26.CueTimeMin = 0;
        //scene26.CueAction = () => lites.Back_Color_Yellow_Dur(350);
        //scene26.CueName = "Dong";
        //Cues.Add(scene26);

        //CueModel scene27 = new CueModel();
        //scene27.CueTime = 33512;
        //scene27.CueTimeMin = 0;
        //scene27.CueAction = () => lites.Back_Color_Red_Dur(350);
        //scene27.CueName = "Ding";
        //Cues.Add(scene27);

        //CueModel scene28 = new CueModel();
        //scene28.CueTime = 34369;
        //scene28.CueTimeMin = 0;
        //scene28.CueAction = () => lites.Back_Color_Yellow_Dur(350);
        //scene28.CueName = "Dong";
        //Cues.Add(scene28);

        //CueModel scene29 = new CueModel();
        //scene29.CueTime = 40242;
        //scene29.CueTimeMin = 0;
        //scene29.CueAction = () => lites.Back_Color_Red();
        //scene29.CueName = "Ahhh";
        //Cues.Add(scene29);

        //CueModel scene30 = new CueModel();
        //scene30.CueTime = 40246;
        //scene30.CueTimeMin = 0;
        //scene30.CueAction = () => lites.Back_Color_Yellow();
        //scene30.CueName = "Ahhh";
        //Cues.Add(scene30);

        //CueModel scene31 = new CueModel();
        //scene31.CueTime = 46177;
        //scene31.CueTimeMin = 0;
        //scene31.CueAction = () => lites.Back_Color_Red_Dur(10); //Off
        //scene31.CueName = "Ding";
        //Cues.Add(scene31);

        //CueModel scene32 = new CueModel();
        //scene32.CueTime = 46872;
        //scene32.CueTimeMin = 0;
        //scene32.CueAction = () => lites.Back_Color_Yellow_Dur(10); //Off
        //scene32.CueName = "Dong";
        //Cues.Add(scene32);

        //CueModel scene33 = new CueModel();
        //scene33.CueTime = 47924;
        //scene33.CueTimeMin = 0;
        //scene33.CueAction = () => lites.Back_Color_Red_Dur(350);
        //scene33.CueName = "Ding";
        //Cues.Add(scene33);

        //CueModel scene34 = new CueModel();
        //scene34.CueTime = 48641;
        //scene34.CueTimeMin = 0;
        //scene34.CueAction = () => lites.Back_Color_Yellow_Dur(350);
        //scene34.CueName = "Dong";
        //Cues.Add(scene34);

        //CueModel scene35 = new CueModel();
        //scene35.CueTime = 19386;
        //scene35.CueTimeMin = 1;
        //scene35.CueAction = () => lites.Back_Color_Red();
        //scene35.CueName = "Its the most";
        //Cues.Add(scene35);

        //CueModel scene36 = new CueModel();
        //scene36.CueTime = 19391;
        //scene36.CueTimeMin = 1;
        //scene36.CueAction = () => lites.Back_Color_Yellow();
        //scene36.CueName = "Its the most";
        //Cues.Add(scene36);

        //CueModel scene37 = new CueModel();
        //scene37.CueTime = 26149;
        //scene37.CueTimeMin = 1;
        //scene37.CueAction = () => lites.Back_Color_Black();
        //scene37.CueName = "Black";
        //Cues.Add(scene37);

        //CueModel scene38 = new CueModel();
        //scene38.CueTime = 27000;
        //scene38.CueTimeMin = 1;
        //scene38.CueAction = () => lites.Back_OnOf_Slow_Bounce_NoSplit(3); //1960 Per Loop 
        //scene38.CueName = "Horns";
        //Cues.Add(scene38);

        //CueModel scene38a = new CueModel();
        //scene38a.CueTime = 27005;
        //scene38a.CueTimeMin = 1;
        //scene38a.CueAction = () => lites.DownStage(20, false, true, 0.0, 0.0, 0.25);
        //scene38a.CueName = "Horns Down Stage Off";
        //Cues.Add(scene38a);

        //CueModel scene38b = new CueModel();
        //scene38b.CueTime = 27010;
        //scene38b.CueTimeMin = 1;
        //scene38b.CueAction = () => lites.DownStage(20, false, false, 0.0, 0.0, 0.20);
        //scene38b.CueName = "Horns Down Stage Off";
        //Cues.Add(scene38b);

        //CueModel scene39 = new CueModel();
        //scene39.CueTime = 33708;
        //scene39.CueTimeMin = 1;
        //scene39.CueAction = () => lites.Back_Color_Red();
        //scene39.CueName = "Girls";
        //Cues.Add(scene39);

        //CueModel scene40 = new CueModel();
        //scene40.CueTime = 35812;
        //scene40.CueTimeMin = 1;
        //scene40.CueAction = () => lites.Back_Color_Yellow();
        //scene40.CueName = "Boys";
        //Cues.Add(scene40);

        //CueModel scene41 = new CueModel();
        //scene41.CueTime = 37424;
        //scene41.CueTimeMin = 1;
        //scene41.CueAction = () => lites.Back_Color_Black();
        //scene41.CueName = "Both";
        //Cues.Add(scene41);

        //CueModel scene42 = new CueModel();
        //scene42.CueTime = 37426;
        //scene42.CueTimeMin = 1;
        //scene42.CueAction = () => lites.Back_Color_Green();
        //scene42.CueName = "Both";
        //Cues.Add(scene42);

        //CueModel scene43 = new CueModel();
        //scene43.CueTime = 37428;
        //scene43.CueTimeMin = 1;
        //scene43.CueAction = () => lites.Back_Color_Blue();
        //scene43.CueName = "Both";
        //Cues.Add(scene43);

        //CueModel scene44 = new CueModel();
        //scene44.CueTime = 40700;
        //scene44.CueTimeMin = 1;
        //scene44.CueAction = () => lites.DownStage(10, true, true, 1.00, 0.0, 0.0);
        //scene44.CueName = "All";
        //Cues.Add(scene44);

        //CueModel scene45 = new CueModel();
        //scene45.CueTime = 40750;
        //scene45.CueTimeMin = 1;
        //scene45.CueAction = () => lites.DownStage(10, true, false, 1.00, 0.0, 0.0);
        //scene45.CueName = "All";
        //Cues.Add(scene45);

        //CueModel scene46 = new CueModel();
        //scene46.CueTime = 53375;
        //scene46.CueTimeMin = 1;
        //scene46.CueAction = () => lites.Back_Color_Red();
        //scene46.CueName = "All";
        //Cues.Add(scene46);

        //CueModel scene47 = new CueModel();
        //scene47.CueTime = 53382;
        //scene47.CueTimeMin = 1;
        //scene47.CueAction = () => lites.Back_Color_Yellow();
        //scene47.CueName = "All";
        //Cues.Add(scene47);

        //CueModel scene48 = new CueModel();
        //scene48.CueTime = 575;
        //scene48.CueTimeMin = 2;
        //scene48.CueAction = () => lites.Back_Color_Black();
        //scene48.CueName = "Back to just him";
        //Cues.Add(scene48);

        //CueModel scene49 = new CueModel();
        //scene49.CueTime = 580;
        //scene49.CueTimeMin = 2;
        //scene49.CueAction = () => lites.Back_Color_Blue();
        //scene49.CueName = "Back to just him";
        //Cues.Add(scene49);

        //CueModel scene50 = new CueModel();
        //scene50.CueTime = 585;
        //scene50.CueTimeMin = 2;
        //scene50.CueAction = () => lites.Back_Color_Green();
        //scene50.CueName = "Back to just him";
        //Cues.Add(scene50);

        //CueModel scene51 = new CueModel();
        //scene51.CueTime = 11049;
        //scene51.CueTimeMin = 2;
        //scene51.CueAction = () => lites.Back_Color_Black();
        //scene51.CueName = "Back to just him";
        //Cues.Add(scene51);

        //CueModel scene52 = new CueModel();
        //scene52.CueTime = 11055;
        //scene52.CueTimeMin = 2;
        //scene52.CueAction = () => lites.Back_StrobeRandom_Slow_NoSplit(12); //26613 End  Slow 350 x 12 = 4200
        //scene52.CueName = "Sparkle Slow";
        //Cues.Add(scene52);

        //CueModel scene53 = new CueModel();
        //scene53.CueTime = 15260;
        //scene53.CueTimeMin = 2;
        //scene53.CueAction = () => lites.Back_StrobeRandom_Slow_Split(12); //Slow 400 x 12 = 4800
        //scene53.CueName = "Sparkle Slow";
        //Cues.Add(scene53);

        //CueModel scene54 = new CueModel();
        //scene54.CueTime = 20065;
        //scene54.CueTimeMin = 2;
        //scene54.CueAction = () => lites.Back_StrobeRandom_Fast_NoSplit(22); //125 x 22 = 
        //scene54.CueName = "Sparkle Fast";
        //Cues.Add(scene54);

        //CueModel scene55 = new CueModel();
        //scene55.CueTime = 23070;
        //scene55.CueTimeMin = 2;
        //scene55.CueAction = () => lites.Back_StrobeRandom_Fast_Split(20); //150
        //scene55.CueName = "Sparkle Fast";
        //Cues.Add(scene55);


        //CueModel scene56 = new CueModel();
        //scene56.CueTime = 26736;
        //scene56.CueTimeMin = 2;
        //scene56.CueAction = () => lites.Back_StrobeAll_Fast();
        //scene56.CueName = "Strobe 1";
        //Cues.Add(scene56);

        //CueModel scene57 = new CueModel();
        //scene57.CueTime = 27063;
        //scene57.CueTimeMin = 2;
        //scene57.CueAction = () => lites.Back_StrobeAll_Fast();
        //scene57.CueName = "Strobe 2";
        //Cues.Add(scene57);

        //CueModel scene58 = new CueModel();
        //scene58.CueTime = 27410;
        //scene58.CueTimeMin = 2;
        //scene58.CueAction = () => lites.DownStage(0, true, true, 0.999, 0.0, 1.0);
        //scene58.CueName = "Strobe 3 Down Stage Key";
        //Cues.Add(scene58);

        //CueModel scene59 = new CueModel();
        //scene59.CueTime = 27415;
        //scene59.CueTimeMin = 2;
        //scene59.CueAction = () => lites.DownStage(0, true, false, 0.999, 0.0, 1.0);
        //scene59.CueName = "Strobe 3 Down Stage Key";
        //Cues.Add(scene59);

        //CueModel scene60 = new CueModel();
        //scene60.CueTime = 27420;
        //scene60.CueTimeMin = 2;
        //scene60.CueAction = () => lites.Back_StrobeAll_Fast();
        //scene60.CueName = "Strobe 3";
        //Cues.Add(scene60);

        //CueModel scene61 = new CueModel();
        //scene61.CueTime = 27425;
        //scene61.CueTimeMin = 2;
        //scene61.CueAction = () => lites.DownStage(2, false, true, 0.0, 0.0, 1.0);
        //scene61.CueName = "Strobe 3 Down Stage Key Out";
        //Cues.Add(scene61);

        //CueModel scene62 = new CueModel();
        //scene62.CueTime = 27430;
        //scene62.CueTimeMin = 2;
        //scene62.CueAction = () => lites.DownStage(2, false, false, 0.0, 0.0, 1.0);
        //scene62.CueName = "Strobe 3 Down Stage Key Out";
        //Cues.Add(scene62);







        ////CueModel scene999 = new CueModel();
        ////scene999.CueTime = 28921;
        ////scene999.CueTimeMin = 2;
        ////scene999.CueAction = () => lites.BLackOut();
        ////scene999.CueName = "Black Out";
        ////Cues.Add(scene999);

        return _CuesLite;
    }
}
