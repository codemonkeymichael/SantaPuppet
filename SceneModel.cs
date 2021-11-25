using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaPuppet
{
    public class SceneModel
    {
        public int CueTime { get; set; }
        public Action LightScene { get; set; }
        public string SceneName { get; set; }    
    }
}
