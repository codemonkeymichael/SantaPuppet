﻿using System.Timers;
using LibVLCSharp.Shared;

namespace SantaPuppet;
internal class Audio
{
    private static System.Timers.Timer _timerLite;
    private static int _currentLiteCue = 0;
    private static System.Timers.Timer _timerCurtin;
    private static int _currentCurtinCue = 0;

    private static SongModel _song;
    public static MediaPlayer _player;


    /// <summary>
    /// Do this well before you need to play. This gives the player some time to buffer the audio.
    /// </summary>
    /// <param name="s"></param>
    public static void CueSong(SongModel s)
    {
        _song = s;
        //order lite cues
        var liteCueListOrdered = from cl in _song.CuesLite
                     orderby cl.CueTimeMin ascending, cl.CueTime ascending
                     select cl;
        _song.CuesLite = liteCueListOrdered.ToList();


        var songPath = AppDomain.CurrentDomain.BaseDirectory + "audio/" + _song.SongPath;

        //VLC Player Init
        Core.Initialize();
        var libVLC = new LibVLC();
        var media = new Media(libVLC, songPath, FromType.FromPath);
        _player = new MediaPlayer(media);
        _player.Playing += OnPlaybackStart;
        _player.EndReached += OnPlaybackFinished;
        _player.TimeChanged += Player_TimeChanged; //Keep cues in sync with the audio    
        //Reset for a new song
        _currentLiteCue = 0;
        _currentCurtinCue = 0;
   
    }

    public static void PlaySong()
    {
        _player.Position = 0.0F;
        _player.Play();
        
        //Console.WriteLine("PlaySong()");
    }

    private static void Player_TimeChanged(object? sender, MediaPlayerTimeChangedEventArgs e)
    {
        //This corrects the timer interval as the audio plays
        if (_currentLiteCue < _song.CuesLite.Count)
        {
            int cueTime = _song.CuesLite[_currentLiteCue].CueTime + (_song.CuesLite[_currentLiteCue].CueTimeMin * 60000);
            double newInterval = cueTime - e.Time; //Compair the cue time with where the song is  
            //Console.WriteLine("Player_TimeChanged()" +
            //" _currentCue=" + _currentCue +
            //" _song.Cues.Count=" + _song.Cues.Count +
            //" cue time=" + cueTime +
            //" new timer interval=" + newInterval);
            if (newInterval > 0) _timerLite.Interval = newInterval;
        }
    
        if (_currentCurtinCue < _song.CuesCurtin.Count)
        {          
            int cueTime = _song.CuesCurtin[_currentCurtinCue].CueTime + (_song.CuesCurtin[_currentCurtinCue].CueTimeMin * 60000);      
            double newInterval = cueTime - e.Time; //Compair the cue time with where the song is  
            //Console.WriteLine("_currentCurtinCue " + _currentCurtinCue + "  _song.CuesCurtin.Count " + _song.CuesCurtin.Count + "  cueTime " + cueTime + "  newInterval " + newInterval) ;
            if (newInterval > 0) _timerCurtin.Interval = newInterval;
        }
    }

    public static void StopSong()
    {
        Program.songPlaying = false;
        _player.Stop();
        _timerLite.Stop();
        _timerLite.Dispose();
        _timerCurtin.Stop();
        _timerCurtin.Dispose();
    }

    private static void OnPlaybackStart(object? sender, EventArgs e)
    {
        //Lights
        _timerLite = new System.Timers.Timer(_song.CuesLite[_currentLiteCue].CueTime); 
        _timerLite.Elapsed += OnTimedLiteEvent;
        _timerLite.AutoReset = false;
        _timerLite.Enabled = true;
        _timerLite.Start();
        //Curtin 
        _timerCurtin = new System.Timers.Timer(_song.CuesCurtin[_currentCurtinCue].CueTime);
        _timerCurtin.Elapsed += OnTimedCurtinEvent;
        _timerCurtin.AutoReset = false;
        _timerCurtin.Enabled = true;
        _timerCurtin.Start();
    }
    private static void OnTimedLiteEvent(Object source, ElapsedEventArgs e)
    {
        int cueTime = _song.CuesLite[_currentLiteCue].CueTime + (_song.CuesLite[_currentLiteCue].CueTimeMin * 60000);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Cue " + _currentLiteCue +
            " of " + _song.CuesLite.Count +
            "  Time " + cueTime +
            "  CueName Light-" + _song.CuesLite[_currentLiteCue].CueName);
   
        int num = _currentLiteCue; //I don't know why I have to do this???? It works.
        Thread t = new Thread(() => _song.CuesLite[num].CueAction.Invoke());
        t.Name = _song.CuesLite[_currentLiteCue].CueName;
        t.Start();

        _currentLiteCue++;
        if (_currentLiteCue < _song.CuesLite.Count)
        {
            int nextCueTime = _song.CuesLite[_currentLiteCue].CueTime + (_song.CuesLite[_currentLiteCue].CueTimeMin * 60000);
            int newInterval = nextCueTime - cueTime;
            _timerLite.Interval = newInterval;
        }
    }

    private static void OnTimedCurtinEvent(Object source, ElapsedEventArgs e)
    {
        int cueTime = _song.CuesCurtin[_currentCurtinCue].CueTime + (_song.CuesCurtin[_currentCurtinCue].CueTimeMin * 60000);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Cue " + _currentCurtinCue +
            " of " + _song.CuesCurtin.Count +
            "  Time " + cueTime +
            "  CueName Curtin-" + _song.CuesCurtin[_currentCurtinCue].CueName);

        int num = _currentCurtinCue; //I don't know why I have to do this???? It works.
        Thread t = new Thread(() => _song.CuesCurtin[num].CueAction.Invoke());
        t.Name = _song.CuesCurtin[_currentCurtinCue].CueName;
        t.Start();

        _currentCurtinCue++;
        if (_currentCurtinCue < _song.CuesCurtin.Count)
        {
            int nextCueTime = _song.CuesCurtin[_currentCurtinCue].CueTime + (_song.CuesCurtin[_currentCurtinCue].CueTimeMin * 60000);
            int newInterval = nextCueTime - cueTime;
            _timerLite.Interval = newInterval;
        }
    }

    private static void OnPlaybackFinished(object? sender, EventArgs e)
    {
        Console.WriteLine("Playback Finished");
        Program.songPlaying = false;
        _timerLite.Stop();
        _timerLite.Dispose();
        _timerCurtin.Stop();
        _timerCurtin.Dispose();
    }
}

