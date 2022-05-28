using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HoldfastGame;
using uGameDB;
using System.Reflection;

namespace HoldfastHack
{
    class HackClient : MonoBehaviour
    {
        public bool IsMenuOpen { get; set; }
        
        public GameManager GameManager { get; set; }
        public ConfigManager ConfigManager { get; set; }
        public StringGenerator StringGenerator { get; set; }
        public float ObjectCacheUpdateTimer { get; set; }
        public float PlayerCacheUpdateTimer { get; set; }

        public void Start()
        {
            this.IsMenuOpen = true;
            this.GameManager = new GameManager();
            this.ConfigManager = new ConfigManager();
            this.StringGenerator = new StringGenerator();
            this.ObjectCacheUpdateTimer = 0;
            this.PlayerCacheUpdateTimer = 0;
            this.ConfigManager.RunSpeedScale = FindObjectOfType<CommonGlobalVariables>().CharacterRunSpeedScale;

        }

        public void Update()
        {
            this.ObjectCacheUpdateTimer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Insert))
            {
                IsMenuOpen = !IsMenuOpen;
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Injector.Unload();
            }

        }

        public void FixedUpdate()
        {
        
            if (this.ObjectCacheUpdateTimer <= 0)
            {
                this.GameManager.UpdateAll();
                
                this.ObjectCacheUpdateTimer = 15f;
            }

            if (this.ObjectCacheUpdateTimer <= 0)
            {
                this.GameManager.UpdatePlayerObjects();

                this.PlayerCacheUpdateTimer = 1f;
            }

            this.GameManager.CommonGlobals.ForEach((globalVars) =>
            {
                if (globalVars)
                {
                    globalVars.CharacterRunSpeedScale = this.ConfigManager.RunSpeedScale;
                }
            });
      
            this.GameManager.Weapons.ForEach((weapon) =>
            {
                if (weapon)
                {
                    var firearmProperties = weapon.FirearmWeaponProperties;
                    if (firearmProperties)
                    {
                        weapon.FirearmWeaponProperties.recoilForce = 0;
                        weapon.FirearmWeaponProperties.shotMaximumHorizontalDeviationAngle = 0;
                    }
                }
            });

            
          
        }

        public void OnGUI()
        {
            if (IsMenuOpen)
                GUI.Window(0, new Rect(100, 100, 300, 400), OnWindow, "Holdfast Hack");

            EspManager.DoEsp(Camera.current, GameManager.Players, GameManager.LocalPlayer, this.ConfigManager);
            // Maybe move to Update()? 
            AimbotManager.DoAimbot(GameManager, Camera.current, ConfigManager, KeyCode.Mouse1);

        }
        private void OnWindow(int id)
        {
            GUI.Label(new Rect(20, 20, 100, 20), "Run Speed Scale");
            ConfigManager.RunSpeedScale = GUI.HorizontalSlider(new Rect(20, 40, 200, 40), ConfigManager.RunSpeedScale, 0, 10);
            if (GUI.Button(new Rect(20, 60, 200, 20), "No Spread / Recoil"))
            {
                ConfigManager.NoSpreadRecoil = true;
            }
            ConfigManager.HealthEsp = GUI.Toggle(new Rect(20, 80, 200, 20), ConfigManager.HealthEsp, "Health ESP");
            ConfigManager.NameEsp = GUI.Toggle(new Rect(20, 100, 200, 20), ConfigManager.NameEsp, "Name ESP");
            ConfigManager.TeleKill = GUI.Toggle(new Rect(20, 120, 200, 20), ConfigManager.TeleKill, "TeleKill");

            if (GUI.Button(new Rect(20, 140, 200, 20), "Start TeleKill"))
            {
                StartCoroutine(Telekill.Teleloop(GameManager.Players, GameManager.LocalPlayer, ConfigManager));
            }
            ConfigManager.NameChanger = GUI.Toggle(new Rect(20, 160, 200, 20), ConfigManager.NameChanger, "Name Changer");
            ConfigManager.Aimbot = GUI.Toggle(new Rect(20, 180, 200, 20), ConfigManager.Aimbot, "Aimbot");
            GUI.Label(new Rect(20, 200, 100, 20), "Aim Sensitivity");
            ConfigManager.AimSensitivty = GUI.HorizontalSlider(new Rect(20, 220, 200, 40), ConfigManager.AimSensitivty, 0, 20);
            if (ConfigManager.NameChanger && GameManager.LocalPlayer != null && GameManager.LocalPlayer.CurrentRoundPlayerInformation != null && GameManager.LocalPlayer.CurrentRoundPlayerInformation.InitialDetails != null)
            {

                if (this.GameManager.GameOptionsCollection != null)
                {
                    this.GameManager.GameOptionsCollection.customPlayerName = StringGenerator.GenerateString(10);
                    this.GameManager.GameOptionsCollection.useSteamName = false;
                }

            }

        }
    }


}
        
