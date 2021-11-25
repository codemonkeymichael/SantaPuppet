using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using NetCoreAudio;
using System.Timers;

//https://github.com/mobiletechtracker/NetCoreAudio
//https://code.videolan.org/videolan/LibVLCSharp
//https://code.videolan.org/videolan/LibVLCSharp/-/blob/3.x/docs/linux-setup.md

namespace SantaPuppet
{
    internal class Audio
    {
        private static System.Timers.Timer aTimer;
        private static List<int> lightingCuesTimes;
        private static List<Action> lightingCuesScenes;
        private static int currentCue = 0;
        private static bool firstCue = true;
        private static SongModel song;
        private static List<Thread> listThreads = new List<Thread>();

        public static void PlaySong(SongModel s)
        {
            song = s;
            var player = new Player();
            player.PlaybackFinished += OnPlaybackFinished;
            player.Play(song.SongPath).Wait();

            currentCue = 0;
            firstCue = true;
            while (true)
            {
                if (player.Playing)
                {
                    OnPlaybackStart();
                    break;
                }
            }
        }

        private static void OnPlaybackStart()
        {
            Console.WriteLine("Playback Started");
            aTimer = new System.Timers.Timer(song.Scenes[currentCue].CueTime);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
            aTimer.Start();
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (firstCue)
            {
                firstCue = false;
            }
            else
            {
                currentCue++;
            }
            Console.WriteLine("Fire currentCue=" + currentCue.ToString() +
                " time=" + song.Scenes[currentCue].CueTime +
                " scenes.Count=" + song.Scenes.Count.ToString() +
                " sceneName=" + song.Scenes[currentCue].SceneName);
 
            Thread t = new Thread(() => song.Scenes[currentCue].LightScene.Invoke());
            t.Name = song.Scenes[currentCue].SceneName;
            t.Start(); 

            if (currentCue < song.Scenes.Count)
            {
                int newInterval = song.Scenes[currentCue + 1].CueTime - song.Scenes[currentCue].CueTime;
                //Console.WriteLine("newInterval=" + newInterval.ToString());
                aTimer.Interval = newInterval;
                aTimer.Start();
            }
        }
        private static void OnPlaybackFinished(object sender, EventArgs e)
        {
            Console.WriteLine("Playback finished");
        }
    }
}
