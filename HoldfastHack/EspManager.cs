using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoldfastGame;
using UnityEngine;

namespace HoldfastHack
{
    class EspManager
    {
        public static void DoEsp(Camera camera, List<PlayerBase> playerList, ClientPlayerBase localPlayer, ConfigManager config)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                var player = playerList[i];
                var playerTransform = player.transform;

                if (playerTransform != null && player != null && localPlayer != null && localPlayer.roundPlayerInstance != null && localPlayer.roundPlayerInstance.PlayerStartData != null)
                {
                    Vector2 position;
                    if (Projection.WorldToScreen(camera, playerTransform.position, out position) && player.Health > 0)
                    {
                        if (player.roundPlayerInstance != null && player.roundPlayerInstance.PlayerStartData != null && player.roundPlayerInstance.PlayerStartData.Faction != localPlayer.roundPlayerInstance.PlayerStartData.Faction)
                        {
                            if (config.HealthEsp)
                            {
                                GUI.Label(new Rect(position, new Vector2(100, 20)), player.Health.ToString());
                            }
                            if (config.NameEsp)
                            {
                                if (player.CurrentRoundPlayerInformation != null && player.CurrentRoundPlayerInformation.InitialDetails != null)
                                    GUI.Label(new Rect(new Vector2(position.x, position.y + 15), new Vector2(100, 20)), player.CurrentRoundPlayerInformation.InitialDetails.Name);
                            }
                        }
                    }
                }
            }
        }

    }
}
