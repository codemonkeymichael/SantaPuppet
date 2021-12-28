﻿using System.Timers;
using LibVLCSharp.Shared;

namespace SantaPuppet;
internal class Audio
{
    private static System.Timers.Timer _timer;
    private static int _currentCue = 0;
    //private static bool firstCue = true;
    private static SongModel _song;
    private static MediaPlayer _player;
    //private static long _playerLastPosition = 0;

    public static void PlaySong(SongModel s)
    {
        _song = s;
        //VLC Player Init
        Core.Initialize();
        var libVLC = new LibVLC();
        var media = new Media(libVLC, _song.SongPath, FromType.FromPath);
        _player = new MediaPlayer(media);
        _player.Playing += OnPlaybackStart;
        _player.TimeChanged += Player_TimeChanged; //Keep cues in sync with the audio
        _player.Play();
        //Reset for a new song
        _currentCue = 0;
        Console.WriteLine(s.Title);
    }



    private static void Player_TimeChanged(object? sender, MediaPlayerTimeChangedEventArgs e)
    {
        //This corrects the timer interval as the audio plays
        int cueTime = _song.Cues[_currentCue].CueTime + (_song.Cues[_currentCue].CueTimeMin * 60000);
        double newInterval = cueTime - e.Time;
        if (newInterval < 2) newInterval = 2;
        _timer.Interval = newInterval;
    }

    public static void StopSong()
    {
        Program.songPlaying = false;
        _player.Stop();
        _timer.Stop();
        _timer.Dispose();
    }

    private static void OnPlaybackStart(object? sender, EventArgs e)
    {
        //Console.WriteLine("Playback Started Run Cue Timer");
        _timer = new System.Timers.Timer(_song.Cues[_currentCue].CueTime);
        // Hook up the Elapsed event for the timer. 
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = false;
        _timer.Enabled = true;
        _timer.Start();
    }
    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        //Console.WriteLine("OnTimedEvent");
        int cueTime = _song.Cues[_currentCue].CueTime + (_song.Cues[_currentCue].CueTimeMin * 60000);
        Console.WriteLine("Cue " + _currentCue +
            " of " + _song.Cues.Count +
            "  Time " + cueTime +
            "  CueName " + _song.Cues[_currentCue].CueName);
        Thread t = new Thread(() => _song.Cues[_currentCue].CueAction.Invoke());
        t.Name = _song.Cues[_currentCue].CueName;
        t.Start();

        if (_currentCue < _song.Cues.Count)
        {
            int nextCueTime = _song.Cues[_currentCue + 1].CueTime + (_song.Cues[_currentCue + 1].CueTimeMin * 60000);
            int newInterval = nextCueTime - cueTime;
            _timer.Interval = newInterval;
        }
        _currentCue++;
    }
    private static void OnPlaybackFinished(object sender, EventArgs e)
    {
        Console.WriteLine("Playback finished");
        Program.songPlaying = false;
        _timer.Stop();
        _timer.Dispose();
    }
}

