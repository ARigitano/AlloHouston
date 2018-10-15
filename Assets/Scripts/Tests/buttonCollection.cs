// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
    public class ButtonCollection : MonoBehaviour
    {
        [SerializeField]
        private ViveControllerManager _viveManager;
        [SerializeField]
        private int _buttonID;


        public void AssignCollectionNumber()
        {
            _viveManager._virtualObjectPrefabIndex = _buttonID;
        }
    }
}
