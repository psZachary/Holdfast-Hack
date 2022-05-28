using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HoldfastGame;

namespace HoldfastHack
{
    class AimbotManager
    {
        
        public static void DoAimbot(GameManager gameManager, Camera camera, ConfigManager config, KeyCode activation)
        {

            if (Input.GetKey(activation))
            {
                if (config.Aimbot)
                {
                    var closest = gameManager.GetClosestPlayerToScreenCenter(camera);
                    if (closest != null && closest.transform != null && gameManager.LocalPlayer != null)
                    {
          
                        
                        Vector2 enemyPosition = new Vector2(0, 0);
                        if (Projection.WorldToScreen(camera, new Vector3(closest.transform.position.x, closest.transform.position.y + 1.5f, closest.transform.position.z), out enemyPosition) && closest.Health > 0)
                        {
                            Vector2 relativePosition;
                            if (closest.roundPlayerInstance != null && closest.roundPlayerInstance.PlayerStartData != null && closest.roundPlayerInstance.PlayerStartData.Faction != gameManager.LocalPlayer.roundPlayerInstance.PlayerStartData.Faction)
                            {
                                relativePosition.x = enemyPosition.x - Screen.width / 2;
                                relativePosition.y = enemyPosition.y - Screen.height / 2;
                                CursorManager.RelativeMove((int)(relativePosition.x / config.AimSensitivty), (int)(relativePosition.y / config.AimSensitivty));

                            }
                        }
                        
                    }
                }
            }
        }
    }
}