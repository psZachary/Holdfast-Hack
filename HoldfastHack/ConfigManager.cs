using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoldfastHack
{
    class ConfigManager
    {
        public float RunSpeedScale { get; set; }
        public bool NoSpreadRecoil { get; set; }
        public bool HealthEsp { get; set; }
        public bool MaxMag { get; set; }
        public bool TeleKill { get; set; }
        public bool NameEsp { get; set; }
        public bool NameChanger { get; set; }
        public bool Aimbot { get; set; }
        public float AimSensitivty { get; set; }

        public ConfigManager()
        {
            AimSensitivty = 5f;
        }
    }
}
