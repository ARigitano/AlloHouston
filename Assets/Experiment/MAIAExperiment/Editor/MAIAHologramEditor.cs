using UnityEditor;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    [CustomEditor(typeof(MAIAHologramTube))]
    public class MAIAHologramEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var hologram = (MAIAHologramTube)target;
            if (GUILayout.Button("Start Animation"))
            {
                hologram.StartAnimation();
            }
        }
    }
}