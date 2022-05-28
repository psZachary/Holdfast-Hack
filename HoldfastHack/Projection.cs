using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace HoldfastHack
{
    class Projection
    {
        public static bool WorldToScreen(Camera camera, Vector3 position, out Vector2 screenPosition)
        {

            Vector3 w2s = camera.WorldToScreenPoint(position);
            Vector2 w2sScreenPosition = new Vector2(w2s.x, Math.Abs(Screen.height - w2s.y));

            if (w2sScreenPosition.x > 0 && w2sScreenPosition.y > 0)
            {
                if (w2sScreenPosition.x < Screen.width && w2sScreenPosition.y < Screen.height && w2s.z > 0)
                {
                    screenPosition.x = w2sScreenPosition.x;
                    screenPosition.y = w2sScreenPosition.y;
                    return true;
                }
            }

            screenPosition.x = 0;
            screenPosition.y = 0;

            return false;
        }
    }
}
