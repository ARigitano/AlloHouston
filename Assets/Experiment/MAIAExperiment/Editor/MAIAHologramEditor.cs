using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    [CustomEditor(typeof(MAIAHologram))]
    public class MAIAHologramEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var hologram = (MAIAHologram)target;
            if (GUILayout.Button("Start Animation"))
            {
                hologram.StartAnimation();
            }
        }
    }
}