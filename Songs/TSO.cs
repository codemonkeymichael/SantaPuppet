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

            SceneModel sceneTest = new SceneModel();
            sceneTest.CueTime = 1000;
            sceneTest.LightScene = () => lites.DownStageLights(40,true,true,0.05,0.0,0.02);
            sceneTest.SceneName = "BackChaseOneOn";
            song.Scenes.Add(sceneTest);

            //SceneModel sceneTest2 = new SceneModel();
            //sceneTest2.CueTime = 5000;
            //sceneTest2.LightScene = () => lites.BackChaseOnChaseOff(250, 2, true);
            //sceneTest2.SceneName = "BackChaseOnChaseOff";
            //song.Scenes.Add(sceneTest2);

            //SceneModel scene0 = new SceneModel();
            //scene0.CueTime = 6172;
            //scene0.LightScene = () => lites.DownStageLights(15, true, true);
            //scene0.SceneName = "Fade up Key Lights";
            //song.Scenes.Add(scene0);

            //SceneModel scene1 = new SceneModel();
            //scene1.CueTime = 6174;
            //scene1.LightScene = () => lites.DownStageLights(15, true, false);
            //scene1.SceneName = "Fade up foot lights";
            //song.Scenes.Add(scene1);

            //SceneModel scene2 = new SceneModel();
            //scene2.CueTime = 32726;
            //scene2.LightScene = () => lites.BackStrobeAll(500);
            //scene2.SceneName = "Strobe All the backlights";
            //song.Scenes.Add(scene2);

            //SceneModel scene20 = new SceneModel();
            //scene20.CueTime = 33726;
            //scene20.LightScene = () => lites.BackChaseOnChaseOff(50, 4);
            //scene20.SceneName = "Chase";
            //song.Scenes.Add(scene20);

            //SceneModel scene30 = new SceneModel();
            //scene30.CueTime = 90726;
            //scene30.LightScene = () => lites.BackStrobeAll(500);
            //scene30.SceneName = "Strobe";
            //song.Scenes.Add(scene30);

            //SceneModel scene3 = new SceneModel();
            //scene3.CueTime = 192146;
            //scene3.LightScene = () => lites.BackStrobeAll(500);
            //scene2.SceneName = "Strobe All the backlights";
            //song.Scenes.Add(scene3);

            //SceneModel scene4 = new SceneModel();
            //scene4.CueTime = 192748;
            //scene4.LightScene = () => lites.BLackOut();
            //scene4.SceneName = "Black Out";
            //song.Scenes.Add(scene4);

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
