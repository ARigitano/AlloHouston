// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VRCalibrationTool
{
    public class VivePointer : MonoBehaviour
    {
        [SerializeField]
        private ViveControllerManager _viveManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PositionTag")
            {
                _viveManager.touchingPoint = true;
                _viveManager.incorrectPoint = other.gameObject;
            }
            else if (other.tag == "ViveTracker")
            {
                _viveManager.touchingTracker = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "PositionTag")
            {
                _viveManager.touchingPoint = false;
                _viveManager.incorrectPoint = null;
            }
        }
    }
}
