using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using NetCoreAudio;
using System.Timers;



namespace SantaPuppet
{
    internal class Audio
    {
        private static System.Timers.Timer _timer;
        private static int currentCue = 0;
        private static bool firstCue = true;
        private static SongModel song;
        private static Player player;

        public static void PlaySong(SongModel s)
        {
            song = s;
            player = new Player();
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

        public static void StopSong()
        {
            player.Stop();
        }

        private static void OnPlaybackStart()
        {
            Console.WriteLine("Playback Started");
            _timer = new System.Timers.Timer(song.Cues[currentCue].CueTime);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = false;
            _timer.Enabled = true;
            _timer.Start();
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
                _timer.Interval = newInterval;
                _timer.Start();
            }
        }
        private static void OnPlaybackFinished(object sender, EventArgs e)
        {
            Console.WriteLine("Playback finished");
        }
    }
}
