using HoldfastGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HoldfastHack
{
    class Telekill : MonoBehaviour
    {
        public static IEnumerator Teleloop(List<PlayerBase> playerList, ClientPlayerBase localPlayer, ConfigManager config)
        {
            while (true)
            {
                for (int i = 0; i < playerList.Count; i++)
                {
                    PlayerBase player;

                    player = playerList[i];
                    if (player.roundPlayerInstance != null)
                    {
                        var playerTransform = player.roundPlayerInstance.PlayerTransform;

                        if (playerTransform && localPlayer.transform)
                        {
                            if (playerTransform.gameObject == localPlayer.gameObject) continue;
                            if (playerTransform.position == localPlayer.transform.position) continue;
                            if (player.roundPlayerInstance.PlayerStartData != null && player.roundPlayerInstance.PlayerStartData.Faction != localPlayer.roundPlayerInstance.PlayerStartData.Faction)
                            {
                                while (player.Health > 0)
                                {

                                    yield return new WaitForSeconds(0.05f);
                                    if (config.TeleKill)
                                    {
                                        localPlayer.transform.position = new Vector3(playerTransform.position.x + 1, playerTransform.position.y, playerTransform.position.z);
                                    }
                                }
                            }
                        }
                    }
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
