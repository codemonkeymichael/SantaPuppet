namespace SantaPuppet;

public class SongModel
{
    public string Title { get; set; }
    public string SongPath { get; set; }
    public List<CueModel> CuesLite { get; set; }
    public List<CueModel> CuesCurtin { get; set; }
    public List<CueModel> CuesAnim { get; set; }
    public List<CueModel> CuesTalk { get; set; }

}

