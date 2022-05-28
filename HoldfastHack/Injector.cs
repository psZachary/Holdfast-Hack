using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HoldfastHack
{
    public class Injector
    {
        internal static GameObject LoadObject { get; set; }
        public static void Inject()
        {
            LoadObject = new GameObject();
            LoadObject.AddComponent<HackClient>();
            GameObject.DontDestroyOnLoad(LoadObject);
         
        }
        public static void Unload()
        {
            _Unload();
        }
        private static void _Unload()
        {
            GameObject.Destroy(LoadObject);
        }
        
    }
}
