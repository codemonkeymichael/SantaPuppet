using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet
{
    public class SongModel
    {
        public string Title { get; set; }
        public string SongPath { get; set; }
        public List<CueModel> Cues { get; set; }

    }
}
