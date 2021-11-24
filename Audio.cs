using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using NetCoreAudio;

namespace SantaPuppet
{
    internal class Audio
    {

        public static void PlaySong()
        {
            var player = new Player();
            player.PlaybackFinished += OnPlaybackFinished;
            player.Play("/home/pi/audio/song.wav").Wait();
        }

        private static void OnPlaybackFinished(object sender, EventArgs e)
        {
            Console.WriteLine("Playback finished");
        }
    }
}
