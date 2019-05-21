using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRI.HelloHouston.Experience;
using CRI.HelloHouston.Calibration;

namespace CRI.HelloHouston.GameElements
{
    public class TubeSlot : MonoBehaviour
    {
        private VirtualWallTopZone _topZone;
        private GameManager _gameManager;

        public void Init(GameManager gameManager, VirtualWallTopZone topZone)
        {
            _gameManager = gameManager;
            _topZone = topZone;
        }

        public void LoadExperiment(XPManager manager)
        {
            _gameManager.LoadXP(manager, _topZone);
        }

        public void UnloadExperiment()
        {
            _gameManager.UnloadXP(_topZone);
        }
    }
}