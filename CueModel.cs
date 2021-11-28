using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet
{
    public class CueModel
    {
        public int CueTime { get; set; }
        public int CueTimeMin { get; set; }
        public Action CueAction { get; set; }
        public string CueName { get; set; }    
    }
}
