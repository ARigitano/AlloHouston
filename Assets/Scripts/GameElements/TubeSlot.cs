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

        public void LoadExperiment(XPManager manager, Action onXPLoaded)
        {
            _gameManager.LoadXP(manager, _topZone, onXPLoaded);
        }

        public void UnloadExperiment(Action onXPUnloaded)
        {
            _gameManager.UnloadXP(_topZone, onXPUnloaded);
        }
    }
}