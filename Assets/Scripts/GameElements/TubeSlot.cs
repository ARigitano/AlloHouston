using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRI.HelloHouston.Experience;
using CRI.HelloHouston.Calibration;
using System;

namespace CRI.HelloHouston.GameElements
{
    public class TubeSlot : MonoBehaviour
    {
        private VirtualWallTopZone _topZone;
        private GameManager _gameManager;
        public XPTube currentTube;

        public void Init(GameManager gameManager, VirtualWallTopZone topZone)
        {
            _gameManager = gameManager;
            _topZone = topZone;
        }

        public bool LoadExperiment(XPTube tube, XPManager manager, Action onXPLoaded)
        {
            bool res = _gameManager.LoadXP(manager, _topZone, onXPLoaded);
            if (res)
                currentTube = tube;
            return res;
        }

        public bool UnloadExperiment(Action onXPUnloaded)
        {
            bool res = _gameManager.UnloadXP(_topZone, onXPUnloaded);
            if (res)
                currentTube = null;
            return res;
        }
    }
}