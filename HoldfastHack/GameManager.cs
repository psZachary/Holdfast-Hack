using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoldfastGame;
using UnityEngine;

namespace HoldfastHack
{
    
    class GameManager : MonoBehaviour
    {
        public List<PlayerBase> Players { get; set; }
        public List<CommonGlobalVariables> CommonGlobals { get; set; }
        public ClientPlayerBase LocalPlayer { get; set; }
        public List<ClientWeaponHolder> ClientWeaponHolders { get; set; }
        public List<Weapon> Weapons { get; set; }
        public List<FirearmWeaponProperties> WeaponProperties { get; set; }
        public ClientGameManager ClientGameManager { get; set; }
        public GameplayOptionsCollection GameOptionsCollection { get; set; }
        public GameManager()
        {
            this.UpdateAll();
        }

        public void UpdateAll()
        {
            this.UpdatePlayerObjects();
            this.UpdateCommonGlobals();
            this.UpdateWeaponHolders();
            this.UpdateWeapons();
            this.UpdateWeaponProperties();
            this.UpdateClientGameManager();
            this.UpdateGameOptionsCollection();
        }
        public void UpdateGameOptionsCollection()
        {
            GameOptionsCollection = GameOptionsCollectionManager.Instance.GameOptions.GameplayOptions;
        }
        public void UpdateClientGameManager()
        {
            ClientGameManager = FindObjectOfType<ClientGameManager>();
        }
        public void UpdatePlayerObjects()
        {
            Players = FindObjectsOfType<PlayerBase>().ToList();
            LocalPlayer = FindObjectOfType<ClientPlayerBase>();
        }
        public void UpdateCommonGlobals()
        {
            CommonGlobals = FindObjectsOfType<CommonGlobalVariables>().ToList();
        }
        public void UpdateWeaponHolders()
        {
            ClientWeaponHolders = FindObjectsOfType<ClientWeaponHolder>().ToList();
        }



        public void UpdateWeapons()
        {
            Weapons = FindObjectsOfType<Weapon>().ToList();
        }
        public void UpdateWeaponProperties()
        {
            WeaponProperties = FindObjectsOfType<FirearmWeaponProperties>().ToList();
        }
        public PlayerBase GetClosestPlayerToScreenCenter(Camera camera) {
            PlayerBase closestPlayer = null;
            float closestDistance = float.MaxValue;
            foreach (PlayerBase player in Players)
            {
                Vector2 screenPosition = Vector2.zero;
                if (Projection.WorldToScreen(camera, new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z), out screenPosition)) { 
                    float distance = Vector2.Distance(new Vector2(screenPosition.x, screenPosition.y), new Vector2(Screen.width / 2, Screen.height / 2));
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPlayer = player;
                    }
                }
            }
    
            return closestPlayer;

        }
        public PlayerBase GetClosestPlayer()
        {
     
            float closestDistance = 9999999f;
            PlayerBase closestPlayer = null;

            foreach(var player in Players)
            {
                var transform = player.transform;
                var localTransform = LocalPlayer.transform;
                if (transform != null && localTransform != null)
                {
                    var distance = (transform.position - localTransform.position).magnitude;

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPlayer = player;
                    }
                }
            }

            return closestPlayer;
        }


    }
}
