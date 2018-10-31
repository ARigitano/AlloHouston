// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace VRCalibrationTool
{
    public class RealPositionManager : MonoBehaviour
    {
        public static RealPositionManager instance;

        private List<PositionTag> _realPositionTagList = new List<PositionTag>();

        public PositionTag[] controllerPositionTags
        {
            get
            {
                return _realPositionTagList.OrderBy(x => x.index).ToArray();
            }
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            _realPositionTagList = GetComponentsInChildren<PositionTag>().ToList();
        }

        public void ResetPositionTags()
        {
            foreach (var realPositionTag in _realPositionTagList)
            {
                realPositionTag.Reset();
            }
        }
    }
}
