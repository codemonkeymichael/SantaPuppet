using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet.Songs
{
    public class TSO
    {
        public SongModel songData()
        {
            //Build the Song
            SongModel song = new SongModel();
            song.Title = "TSO";
            song.SongPath = "/home/pi/audio/TSO.wav";
            song.Scenes = new List<SceneModel>();

            Lights lites = new Lights();



            SceneModel scene = new SceneModel();
            scene.CueTime = 5;
            scene.LightScene = () => lites.BLackOut();
            scene.SceneName = "Black Out";
            song.Scenes.Add(scene);

            //SceneModel sceneTest = new SceneModel();
            //sceneTest.CueTime = 1000;
            //sceneTest.LightScene = () => lites.DownStageLights(40,true,true,0.05,0.0,0.02);
            //sceneTest.SceneName = "BackChaseOneOn";
            //song.Scenes.Add(sceneTest);

            //SceneModel sceneTest2 = new SceneModel();
            //sceneTest2.CueTime = 5000;
            //sceneTest2.LightScene = () => lites.BackChaseOnChaseOff(250, 2, true);
            //sceneTest2.SceneName = "BackChaseOnChaseOff";
            //song.Scenes.Add(sceneTest2);


            SceneModel scene0 = new SceneModel();
            scene0.CueTime = 6172;
            scene0.LightScene = () => lites.DownStageLights(20, true, true, 0.05, 0.0, 0.00);
            scene0.SceneName = "Fade up Key Lights";
            song.Scenes.Add(scene0);

            SceneModel scene1 = new SceneModel();
            scene1.CueTime = 6174;
            scene1.LightScene = () => lites.DownStageLights(20, true, false, 0.15, 0.0, 0.0);
            scene1.SceneName = "Fade up foot lights";
            song.Scenes.Add(scene1);

            SceneModel scene2 = new SceneModel();
            scene2.CueTime = 32720;
            scene2.LightScene = () => lites.BackStrobeAll(500);
            scene2.SceneName = "Strobe All the backlights";
            song.Scenes.Add(scene2);

            SceneModel scene5 = new SceneModel();
            scene5.CueTime = 32730;
            scene5.LightScene = () => lites.DownStageLights(0, true, true, 0.999, 0.0, 0.999);
            scene5.SceneName = "Strobe Up Down Stage";
            song.Scenes.Add(scene5);

            SceneModel scene10 = new SceneModel();
            scene10.CueTime = 32740;
            scene10.LightScene = () => lites.DownStageLights(0, true, false, 0.999, 0.0, 0.999);
            scene10.SceneName = "Strobe Up Down Stage";
            song.Scenes.Add(scene10);

            SceneModel scene12 = new SceneModel();
            scene12.CueTime = 32780;
            scene12.LightScene = () => lites.DownStageLights(4, false, true, 0.9, 0.18, 0.999);
            scene12.SceneName = "Strobe Down Down Stage";
            song.Scenes.Add(scene12);

            SceneModel scene14 = new SceneModel();
            scene14.CueTime = 32790;
            scene14.LightScene = () => lites.DownStageLights(4, false, false, 0.9, 0.10, 0.999);
            scene14.SceneName = "Strobe Down Down Stage";
            song.Scenes.Add(scene14);


            SceneModel scene15 = new SceneModel();
            scene15.CueTime = 33717;
            scene15.LightScene = () => lites.BackStrobeAll(500);
            scene15.SceneName = "Strobe";
            song.Scenes.Add(scene15);
            SceneModel scene16 = new SceneModel();
            scene16.CueTime = 34696;
            scene16.LightScene = () => lites.BackStrobeAll(500);
            scene16.SceneName = "Strobe";
            song.Scenes.Add(scene16);
            SceneModel scene17 = new SceneModel();
            scene17.CueTime = 35730;
            scene17.LightScene = () => lites.BackStrobeAll(500);
            scene17.SceneName = "Strobe";
            song.Scenes.Add(scene17);
            SceneModel scene18 = new SceneModel();
            scene18.CueTime = 36653;
            scene18.LightScene = () => lites.BackStrobeAll(500);
            scene18.SceneName = "Strobe";
            song.Scenes.Add(scene18);
            SceneModel scene19 = new SceneModel();
            scene19.CueTime = 37604;
            scene19.LightScene = () => lites.BackStrobeAll(500);
            scene19.SceneName = "Strobe";
            song.Scenes.Add(scene19);
            SceneModel scenea = new SceneModel();
            scenea.CueTime = 38527;
            scenea.LightScene = () => lites.BackStrobeAll(500);
            scenea.SceneName = "Strobe";
            song.Scenes.Add(scenea);
            SceneModel sceneb = new SceneModel();
            sceneb.CueTime = 39534;
            sceneb.LightScene = () => lites.BackStrobeAll(500);
            sceneb.SceneName = "Strobe";
            song.Scenes.Add(sceneb);

            SceneModel scene20 = new SceneModel();
            scene20.CueTime = 40205;
            scene20.LightScene = () => lites.BackChaseOnChaseOff(75, 3, true, true);
            scene20.SceneName = "Chase 1";
            song.Scenes.Add(scene20);

            SceneModel scene30 = new SceneModel();
            scene30.CueTime = 44372;
            scene30.LightScene = () => lites.BackStrobeAll(500);
            scene30.SceneName = "Strobe";
            song.Scenes.Add(scene30);
            SceneModel scene31 = new SceneModel();
            scene31.CueTime = 45353;
            scene31.LightScene = () => lites.BackStrobeAll(500);
            scene31.SceneName = "Strobe";
            song.Scenes.Add(scene31);
            SceneModel scene32 = new SceneModel();
            scene32.CueTime = 46274;
            scene32.LightScene = () => lites.BackStrobeAll(500);
            scene32.SceneName = "Strobe";
            song.Scenes.Add(scene32);
            SceneModel scene33 = new SceneModel();
            scene33.CueTime = 47224;
            scene33.LightScene = () => lites.BackStrobeAll(500);
            scene33.SceneName = "Strobe";
            song.Scenes.Add(scene33);

            SceneModel scene50 = new SceneModel();
            scene50.CueTime = 47896;
            scene50.LightScene = () => lites.BackChaseOneOff(80, 5, true, true);
            scene50.SceneName = "Chase 2";
            song.Scenes.Add(scene50);

            SceneModel scene34 = new SceneModel();
            scene34.CueTime = 52090;
            scene34.LightScene = () => lites.BackStrobeAll(500);
            scene34.SceneName = "Strobe";
            song.Scenes.Add(scene34);
            SceneModel scene35 = new SceneModel();
            scene35.CueTime = 53069;
            scene35.LightScene = () => lites.BackStrobeAll(500);
            scene35.SceneName = "Strobe";
            song.Scenes.Add(scene35);
            SceneModel scene36 = new SceneModel();
            scene36.CueTime = 53992;
            scene36.LightScene = () => lites.BackStrobeAll(500);
            scene36.SceneName = "Strobe";
            song.Scenes.Add(scene36);


            ///Here



            SceneModel scene4 = new SceneModel();
            scene4.CueTime = 191951;
            scene4.LightScene = () => lites.BLackOut();
            scene4.SceneName = "Black Out";
            song.Scenes.Add(scene4);

            return song;
        }
    }
}
//lightingCuesTimes.Add(6172); //1 Strings start
//lightingCuesTimes.Add(6174); //2 Strings start
//lightingCuesTimes.Add(32726); //3
//lightingCuesTimes.Add(192146); //4 End
//lightingCuesTimes.Add(192148); //5 End

//lightingCuesScenes.Add(() => lites.DownStageLights(15, true, true)); //1
//lightingCuesScenes.Add(() => lites.DownStageLights(15, true, false));
//lightingCuesScenes.Add(() => lites.BackStrobeAll(500));
////lightingCuesScenes.Add(() => lites.StrobeAll(500));
//lightingCuesScenes.Add(() => lites.BackStrobeAll(500));
//lightingCuesScenes.Add(() => lites.BLackOut());
//lightingCuesScenes.Add(() => Lights.DownStageLights(12, false, true));
//lightingCuesScenes.Add(() => Lights.DownStageLights(12, false, false));
