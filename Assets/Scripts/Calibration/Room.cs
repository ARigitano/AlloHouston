using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
    /// <summary>
    /// Class for a room.
    /// </summary>
    public class Room : MonoBehaviour
    {
        public GameObject[] _placeholders;  //The placeholders offered by this room.
        public GameObject[] _plaholdersBottom;
        public GameObject _canvas;           //Canvas on which information linked to this room will be displayed.
    }
}
