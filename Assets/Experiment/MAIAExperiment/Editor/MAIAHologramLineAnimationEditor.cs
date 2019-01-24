using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    [CustomEditor(typeof(MAIAHologramLineAnimation))]
    public class MAIAHologramLineAnimationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var line = (MAIAHologramLineAnimation)target;
            if (GUILayout.Button("Start Animation"))
            {
                line.StartAnimation();
            }
        }
    }
}