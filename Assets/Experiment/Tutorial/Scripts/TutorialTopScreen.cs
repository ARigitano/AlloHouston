using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class TutorialTopScreen : XPElement
    {
        [SerializeField]
        private GameObject _maintenancePanel, _secondMaintenancePanel, _maintenancePopup, _secondMaintenancePopup;

        public void StartMaintenance()
        {
            _maintenancePopup.SetActive(true);
        }

        public void ContinueMaintenance()
        {
            _maintenancePanel.SetActive(false);
            _secondMaintenancePanel.SetActive(true);
        }

        public void AdvancedMaintenance()
        {
            _secondMaintenancePopup.SetActive(true);
        }
    }
}
