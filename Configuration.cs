using Rocket.API;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedMaxplayers
{
public class Configuration : IRocketPluginConfiguration
    {
        public byte DontGoLessThan { get; set; }
        public byte IncreasePlayersBy { get; set; }
        public bool DecreaseAfterPlayersLeave { get; set; }
        public byte DontGoOverThan { get; set; }
        public void LoadDefaults()
        {
            DontGoLessThan = 24;
            IncreasePlayersBy = 4;
            DecreaseAfterPlayersLeave = true;
            DontGoOverThan = 52;
        }
    }
}
