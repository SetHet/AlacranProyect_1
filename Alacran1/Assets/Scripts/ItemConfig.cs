using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GamePlay
{
    public static class ItemConfig
    {
        public static string joke_mensaje = "Kane";
        
        public enum Ammo
        {
            Pistol,
            Shotgun,
            Ligera
        }
    }

    #if UNITY_EDITOR
    public class ItemConfig_Editor : EditorWindow
    {
        [MenuItem ("Window/Gameplay/Item")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ItemConfig_Editor));
        }

        private void OnGUI()
        {
            ItemConfig.joke_mensaje = GUI.TextArea(new Rect(0, 0, 100, 20), ItemConfig.joke_mensaje);
        }
        
    }
    #endif
}