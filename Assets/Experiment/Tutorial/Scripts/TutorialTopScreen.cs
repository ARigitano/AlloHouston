using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class TutorialTopScreen : XPElement
    {
        [SerializeField]
        private GameObject _maintenancePanel, _secondMaintenancePanel;

        public void StartMaintenance()
        {

        }

        public void ContinueMaintenance()
        {
            _maintenancePanel.SetActive(false);
            _secondMaintenancePanel.SetActive(true);
        }
    }
}
