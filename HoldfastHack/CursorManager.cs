using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HoldfastHack
{
    class CursorManager
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        
        public static void RelativeMove(int relx, int rely)
		{
            mouse_event(0x0001, relx, rely, 0, 0);
		}
    }
}
