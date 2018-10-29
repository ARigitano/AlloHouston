// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Calibration
{
    public class CalibrationButton : VRUIItem
    {
        private ViveControllerManager _viveManager;
        private int _buttonID;
        /// <summary>
        /// Text of the button.
        /// </summary>
        [SerializeField]
        [Tooltip("Text of the button.")]
        private Text _buttonText;

        public void Init(ViveControllerManager viveManager, int buttonId, string text)
        {
            _viveManager = viveManager;
            _buttonID = buttonId;
            _buttonText.text = text;
        }

        public void AssignCollectionNumber()
        {
            _viveManager._virtualObjectPrefabIndex = _buttonID;
        }
    }
}
