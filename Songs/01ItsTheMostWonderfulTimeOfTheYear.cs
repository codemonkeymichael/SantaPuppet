using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet.Songs
{
    public class ItsTheMostWonderfulTimeOfTheYear
    {
        public SongModel songData()
        {
            //Build the Song
            SongModel song = new SongModel();
            song.Title = "Its The Most Wonderful Time Of The Year";
            song.SongPath = "/home/pi/SantaPuppet/audio/01ItsTheMostWonderfulTimeOfTheYear.wav";
            song.Scenes = new List<SceneModel>();

            Lights lites = new Lights();

            //SceneModel sceneTest = new SceneModel();
            //sceneTest.CueTime = 1;
            //sceneTest.LightScene = () => lites.Back_StrobeRandom_Fast_NoSplit(1);
            //sceneTest.SceneName = "Test";
            //song.Scenes.Add(sceneTest);


            SceneModel scene0 = new SceneModel();
            scene0.CueTime = 2;
            scene0.CueTimeMin = 0;
            scene0.LightScene = () => lites.BLackOut();
            scene0.SceneName = "Black Out - Its The Most Wonderful Time Of The Year";
            song.Scenes.Add(scene0);

            SceneModel scene1 = new SceneModel();
            scene1.CueTime = 5;
            scene1.CueTimeMin = 0;
            scene1.LightScene = () => lites.Back_Color_Green();
            scene1.SceneName = "Backlights Green Color";
            song.Scenes.Add(scene1);

            SceneModel scene2 = new SceneModel();
            scene2.CueTime = 10;
            scene2.CueTimeMin = 0;
            scene2.LightScene = () => lites.Back_Color_Blue();
            scene2.SceneName = "Backlights Blue Color";
            song.Scenes.Add(scene2);


            SceneModel scene10 = new SceneModel();
            scene10.CueTime = 15;
            scene10.CueTimeMin = 0;
            scene10.LightScene = () => lites.DownStage(80, true, false, 0.20, 0.0, 0.0);
            scene10.SceneName = "Fade up foot lights";
            song.Scenes.Add(scene10);

            SceneModel scene11 = new SceneModel();
            scene11.CueTime = 6107;
            scene11.CueTimeMin = 0;
            scene11.LightScene = () => lites.DownStage(20, true, true, 0.25, 0.0, 0.00);
            scene11.SceneName = "Fade up Key Lights";
            song.Scenes.Add(scene11);


            SceneModel scene15 = new SceneModel();
            scene15.CueTime = 10866;
            scene15.CueTimeMin = 0;
            scene15.LightScene = () => lites.Back_Color_Red_Dur(350);
            scene15.SceneName = "Ding";
            song.Scenes.Add(scene15);

            SceneModel scene16 = new SceneModel();
            scene16.CueTime = 11646;
            scene16.CueTimeMin = 0;
            scene16.LightScene = () => lites.Back_Color_Yellow_Dur(350);
            scene16.SceneName = "Dong";
            song.Scenes.Add(scene16);

            SceneModel scene17 = new SceneModel();
            scene17.CueTime = 12541;
            scene17.CueTimeMin = 0;
            scene17.LightScene = () => lites.Back_Color_Red_Dur(350);
            scene17.SceneName = "Ding";
            song.Scenes.Add(scene17);

            SceneModel scene18 = new SceneModel();
            scene18.CueTime = 13439;
            scene18.CueTimeMin = 0;
            scene18.LightScene = () => lites.Back_Color_Yellow_Dur(350);
            scene18.SceneName = "Dong";
            song.Scenes.Add(scene18);

            SceneModel scene19 = new SceneModel();
            scene19.CueTime = 19180;
            scene19.CueTimeMin = 0;
            scene19.LightScene = () => lites.Back_Color_Red();
            scene19.SceneName = "Ahhh";
            song.Scenes.Add(scene19);

            SceneModel scene20 = new SceneModel();
            scene20.CueTime = 19184;
            scene20.CueTimeMin = 0;
            scene20.LightScene = () => lites.Back_Color_Yellow();
            scene20.SceneName = "Ahhh";
            song.Scenes.Add(scene20);

            SceneModel scene21 = new SceneModel();
            scene21.CueTime = 24830;
            scene21.CueTimeMin = 0;
            scene21.LightScene = () => lites.Back_Color_Red_Dur(15); //Turn it off
            scene21.SceneName = "Ding";
            song.Scenes.Add(scene21);

            SceneModel scene22 = new SceneModel();
            scene22.CueTime = 25750;
            scene22.CueTimeMin = 0;
            scene22.LightScene = () => lites.Back_Color_Yellow_Dur(15); //Turn it off
            scene22.SceneName = "Dong";
            song.Scenes.Add(scene22);

            SceneModel scene23 = new SceneModel();
            scene23.CueTime = 26394;
            scene22.CueTimeMin = 0;
            scene23.LightScene = () => lites.Back_Color_Red_Dur(350);
            scene23.SceneName = "Ding";
            song.Scenes.Add(scene23);

            SceneModel scene24 = new SceneModel();
            scene24.CueTime = 27558;
            scene24.CueTimeMin = 0;
            scene24.LightScene = () => lites.Back_Color_Yellow_Dur(350);
            scene24.SceneName = "Dong";
            song.Scenes.Add(scene24);

            SceneModel scene25 = new SceneModel();
            scene25.CueTime = 32041;
            scene25.CueTimeMin = 0;
            scene25.LightScene = () => lites.Back_Color_Red_Dur(350);
            scene25.SceneName = "Ding";
            song.Scenes.Add(scene25);

            SceneModel scene26 = new SceneModel();
            scene26.CueTime = 32531;
            scene26.CueTimeMin = 0;
            scene26.LightScene = () => lites.Back_Color_Yellow_Dur(350);
            scene26.SceneName = "Dong";
            song.Scenes.Add(scene26);

            SceneModel scene27 = new SceneModel();
            scene27.CueTime = 33512;
            scene27.CueTimeMin = 0;
            scene27.LightScene = () => lites.Back_Color_Red_Dur(350);
            scene27.SceneName = "Ding";
            song.Scenes.Add(scene27);

            SceneModel scene28 = new SceneModel();
            scene28.CueTime = 34369;
            scene28.CueTimeMin = 0;
            scene28.LightScene = () => lites.Back_Color_Yellow_Dur(350);
            scene28.SceneName = "Dong";
            song.Scenes.Add(scene28);

            SceneModel scene29 = new SceneModel();
            scene29.CueTime = 40242;
            scene29.CueTimeMin = 0;
            scene29.LightScene = () => lites.Back_Color_Red();
            scene29.SceneName = "Ahhh";
            song.Scenes.Add(scene29);

            SceneModel scene30 = new SceneModel();
            scene30.CueTime = 40246;
            scene30.CueTimeMin = 0;
            scene30.LightScene = () => lites.Back_Color_Yellow();
            scene30.SceneName = "Ahhh";
            song.Scenes.Add(scene30);

            SceneModel scene31 = new SceneModel();
            scene31.CueTime = 46177;
            scene31.CueTimeMin = 0;
            scene31.LightScene = () => lites.Back_Color_Red_Dur(10); //Off
            scene31.SceneName = "Ding";
            song.Scenes.Add(scene31);

            SceneModel scene32 = new SceneModel();
            scene32.CueTime = 46872;
            scene32.CueTimeMin = 0;
            scene32.LightScene = () => lites.Back_Color_Yellow_Dur(10); //Off
            scene32.SceneName = "Dong";
            song.Scenes.Add(scene32);

            SceneModel scene33 = new SceneModel();
            scene33.CueTime = 47924;
            scene33.CueTimeMin = 0;
            scene33.LightScene = () => lites.Back_Color_Red_Dur(350);
            scene33.SceneName = "Ding";
            song.Scenes.Add(scene33);

            SceneModel scene34 = new SceneModel();
            scene34.CueTime = 48641;
            scene34.CueTimeMin = 0;
            scene34.LightScene = () => lites.Back_Color_Yellow_Dur(350);
            scene34.SceneName = "Dong";
            song.Scenes.Add(scene34);

            SceneModel scene35 = new SceneModel();
            scene35.CueTime = 19386;
            scene35.CueTimeMin = 1;
            scene35.LightScene = () => lites.Back_Color_Red();
            scene35.SceneName = "Its the most";
            song.Scenes.Add(scene35);

            SceneModel scene36 = new SceneModel();
            scene36.CueTime = 19391;
            scene36.CueTimeMin = 1;
            scene36.LightScene = () => lites.Back_Color_Yellow();
            scene36.SceneName = "Its the most";
            song.Scenes.Add(scene36);

            SceneModel scene37 = new SceneModel();
            scene37.CueTime = 26149;
            scene37.CueTimeMin = 1;
            scene37.LightScene = () => lites.Back_Color_Black();
            scene37.SceneName = "Black";
            song.Scenes.Add(scene37);

            SceneModel scene38 = new SceneModel();
            scene38.CueTime = 27000; 
            scene38.CueTimeMin = 1;
            scene38.LightScene = () => lites.Back_OnOf_Slow_Bounce_NoSplit(3); //1960 Per Loop 
            scene38.SceneName = "Horns";
            song.Scenes.Add(scene38);

            SceneModel scene38a = new SceneModel();
            scene38a.CueTime = 27005;
            scene38a.CueTimeMin = 1;
            scene38a.LightScene = () => lites.DownStage(20, false, true, 0.0, 0.0, 0.25);
            scene38a.SceneName = "Horns Down Stage Off";
            song.Scenes.Add(scene38a);

            SceneModel scene38b = new SceneModel();
            scene38b.CueTime = 27010;
            scene38b.CueTimeMin = 1;
            scene38b.LightScene = () => lites.DownStage(20, false, false, 0.0, 0.0, 0.20);
            scene38b.SceneName = "Horns Down Stage Off";
            song.Scenes.Add(scene38b);

            SceneModel scene39 = new SceneModel();
            scene39.CueTime = 33708;
            scene39.CueTimeMin = 1;
            scene39.LightScene = () => lites.Back_Color_Red();
            scene39.SceneName = "Girls";
            song.Scenes.Add(scene39);

            SceneModel scene40 = new SceneModel();
            scene40.CueTime = 35812;
            scene40.CueTimeMin = 1;
            scene40.LightScene = () => lites.Back_Color_Yellow();
            scene40.SceneName = "Boys";
            song.Scenes.Add(scene40);

            SceneModel scene41 = new SceneModel();
            scene41.CueTime = 37424;
            scene41.CueTimeMin = 1;
            scene41.LightScene = () => lites.Back_Color_Black();
            scene41.SceneName = "Both";
            song.Scenes.Add(scene41);

            SceneModel scene42 = new SceneModel();
            scene42.CueTime = 37426;
            scene42.CueTimeMin = 1;
            scene42.LightScene = () => lites.Back_Color_Green();
            scene42.SceneName = "Both";
            song.Scenes.Add(scene42);

            SceneModel scene43 = new SceneModel();
            scene43.CueTime = 37428;
            scene43.CueTimeMin = 1;
            scene43.LightScene = () => lites.Back_Color_Blue();
            scene43.SceneName = "Both";
            song.Scenes.Add(scene43);

            SceneModel scene44 = new SceneModel();
            scene44.CueTime = 40700;
            scene44.CueTimeMin = 1;
            scene44.LightScene = () => lites.DownStage(10, true, true, 0.25, 0.0, 0.0);
            scene44.SceneName = "All";
            song.Scenes.Add(scene44);

            SceneModel scene45 = new SceneModel();
            scene45.CueTime = 40750;
            scene45.CueTimeMin = 1;
            scene45.LightScene = () => lites.DownStage(10, true, false, 0.20, 0.0, 0.0);
            scene45.SceneName = "All";
            song.Scenes.Add(scene45);

            SceneModel scene46 = new SceneModel();
            scene46.CueTime = 53375;
            scene46.CueTimeMin = 1;
            scene46.LightScene = () => lites.Back_Color_Red();
            scene46.SceneName = "All";
            song.Scenes.Add(scene46);

            SceneModel scene47 = new SceneModel();
            scene47.CueTime = 53382;
            scene47.CueTimeMin = 1;
            scene47.LightScene = () => lites.Back_Color_Yellow();
            scene47.SceneName = "All";
            song.Scenes.Add(scene47);

            SceneModel scene48 = new SceneModel();
            scene48.CueTime = 575;
            scene48.CueTimeMin = 2;
            scene48.LightScene = () => lites.Back_Color_Black();
            scene48.SceneName = "Back to just him";
            song.Scenes.Add(scene48);

            SceneModel scene49 = new SceneModel();
            scene49.CueTime = 580;
            scene49.CueTimeMin = 2;
            scene49.LightScene = () => lites.Back_Color_Blue();
            scene49.SceneName = "Back to just him";
            song.Scenes.Add(scene49);

            SceneModel scene50 = new SceneModel();
            scene50.CueTime = 585;
            scene50.CueTimeMin = 2;
            scene50.LightScene = () => lites.Back_Color_Green();
            scene50.SceneName = "Back to just him";
            song.Scenes.Add(scene50);

            SceneModel scene51 = new SceneModel();
            scene51.CueTime = 11049;
            scene51.CueTimeMin = 2;
            scene51.LightScene = () => lites.Back_Color_Black();
            scene51.SceneName = "Back to just him";
            song.Scenes.Add(scene51);

            SceneModel scene52 = new SceneModel();
            scene52.CueTime = 11055;
            scene52.CueTimeMin = 2;
            scene52.LightScene = () => lites.Back_StrobeRandom_Slow_NoSplit(12); //26613 End  Slow 350 x 12 = 4200
            scene52.SceneName = "Sparkle Slow";
            song.Scenes.Add(scene52);

            SceneModel scene53 = new SceneModel();
            scene53.CueTime = 15260;
            scene53.CueTimeMin = 2;
            scene53.LightScene = () => lites.Back_StrobeRandom_Slow_Split(12); //Slow 400 x 12 = 4800
            scene53.SceneName = "Sparkle Slow";
            song.Scenes.Add(scene53);

            SceneModel scene54 = new SceneModel();
            scene54.CueTime = 20065;
            scene54.CueTimeMin = 2;
            scene54.LightScene = () => lites.Back_StrobeRandom_Fast_NoSplit(24); //125 x 24 = 3000
            scene54.SceneName = "Sparkle Fast";
            song.Scenes.Add(scene54);

            SceneModel scene55 = new SceneModel();
            scene55.CueTime = 23070;
            scene55.CueTimeMin = 2;
            scene55.LightScene = () => lites.Back_StrobeRandom_Fast_Split(23); //150 x 22 = 3300 
            scene55.SceneName = "Sparkle Fast";
            song.Scenes.Add(scene55);

            SceneModel scene56 = new SceneModel();
            scene56.CueTime = 26736;
            scene56.CueTimeMin = 2;
            scene56.LightScene = () => lites.Back_StrobeAll_Fast();
            scene56.SceneName = "Strobe 1";
            song.Scenes.Add(scene56);

            SceneModel scene57 = new SceneModel();
            scene57.CueTime = 27063;
            scene57.CueTimeMin = 2;
            scene57.LightScene = () => lites.Back_StrobeAll_Fast();
            scene57.SceneName = "Strobe 2";
            song.Scenes.Add(scene57);

            SceneModel scene58 = new SceneModel();
            scene58.CueTime = 27410;
            scene58.CueTimeMin = 2;
            scene58.LightScene = () => lites.DownStage(0,true,true,0.999,0.0,1.0);
            scene58.SceneName = "Strobe 3 Down Stage Key";
            song.Scenes.Add(scene58);

            SceneModel scene59 = new SceneModel();
            scene59.CueTime = 27415;
            scene59.CueTimeMin = 2;
            scene59.LightScene = () => lites.DownStage(0, true, false, 0.999, 0.0, 1.0);
            scene59.SceneName = "Strobe 3 Down Stage Key";
            song.Scenes.Add(scene59);

            SceneModel scene60 = new SceneModel();
            scene60.CueTime = 27420;
            scene60.CueTimeMin = 2;
            scene60.LightScene = () => lites.Back_StrobeAll_Fast();
            scene60.SceneName = "Strobe 3";
            song.Scenes.Add(scene60);

            SceneModel scene61 = new SceneModel();
            scene61.CueTime = 27500;
            scene61.CueTimeMin = 2;
            scene61.LightScene = () => lites.DownStage(2, false, true, 0.999, 0.0, 1.0);
            scene61.SceneName = "Strobe 3 Down Stage Key Out";
            song.Scenes.Add(scene61);

            SceneModel scene62 = new SceneModel();
            scene62.CueTime = 27505;
            scene62.CueTimeMin = 2;
            scene62.LightScene = () => lites.DownStage(2, false, false, 0.999, 0.0, 1.0);
            scene62.SceneName = "Strobe 3 Down Stage Key Out";
            song.Scenes.Add(scene62);








            SceneModel scene999 = new SceneModel();
            scene999.CueTime = 28921;
            scene999.CueTimeMin = 2;
            scene999.LightScene = () => lites.BLackOut();
            scene999.SceneName = "Black Out";
            song.Scenes.Add(scene999);

            return song;
        }
    }
}