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
        private static List<Action> lightingCuesCues;
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
            Console.WriteLine(s.Title);
        }

        private static void OnPlaybackStart()
        {
            Console.WriteLine("Playback Started");
            aTimer = new System.Timers.Timer(song.Cues[currentCue].CueTime);
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
            int cueTime = song.Cues[currentCue].CueTime + (song.Cues[currentCue].CueTimeMin * 60000);           
            Console.WriteLine("Fire currentCue=" + currentCue.ToString() +
                " time=" + cueTime +
                " Cues.Count=" + song.Cues.Count.ToString() +
                " CueName=" + song.Cues[currentCue].CueName);
 
            Thread t = new Thread(() => song.Cues[currentCue].CueAction.Invoke());
            t.Name = song.Cues[currentCue].CueName;
            t.Start(); 

            if (currentCue < song.Cues.Count)
            {
                int nextCueTime = song.Cues[currentCue + 1].CueTime + (song.Cues[currentCue + 1].CueTimeMin * 60000);
                //Console.WriteLine("nextCueTime=" + nextCueTime);
                //Console.WriteLine("cueTime=" + cueTime);
                int newInterval = nextCueTime - cueTime;
                //Console.WriteLine("newInterval=" + newInterval);
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
