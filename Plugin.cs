using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IllusionPlugin;
using UnityEngine;
using System.Reflection;

namespace GraphicsPluginTest
{
    public class Plugin : IllusionPlugin.IPlugin
    {
        public string Name => "PressPToPause";
        public string Version => "Zero pt. One";

        private bool init = false;

        public static void Log(string data)
        {
            Console.WriteLine("[PressPToPause] " + data);
            File.AppendAllText(@"MultiLog.txt", "[LookingGlass Mod] " + data + Environment.NewLine);
        }

        public void OnApplicationQuit()
        {
        }

        //private Assembly Resolve(object sender, ResolveEventArgs args)
        //{
        //    Plugin.Log($"Resolving: {args.Name}");
        //    return editor;
        //}

        public static void Dump(object str, string filename)
        {
            System.IO.File.WriteAllText(filename, str.ToString());
        }

        public void OnApplicationStart()
        {
        }

        public void OnFixedUpdate()
        {
        }

        public void OnLevelWasInitialized(int level)
        {
            if (init) return;
            init = true;
            Console.WriteLine("Loaded plugin");
            GameObject go = new GameObject("PressPToPause");
            go.AddComponent<PressPToPause>();
        }

        public void OnLevelWasLoaded(int level)
        {
        }

        public void OnUpdate()
        {
        }
    }
}
