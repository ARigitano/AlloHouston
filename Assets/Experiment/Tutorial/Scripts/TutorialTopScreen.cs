using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class TutorialTopScreen : XPElement
    {
        private GameObject _maintenancePanel, _secondMaintenancePanel;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

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
