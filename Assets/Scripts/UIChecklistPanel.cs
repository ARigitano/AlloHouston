using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.Calibration.UI;
using CRI.HelloHouston.Calibration.XML;

namespace CRI.HelloHouston.Checklist.UI
{
    public class UIChecklistPanel : UIPanel
    {
        /// <summary>
        /// Prefab of a checklist entry.
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab of a ChecklistEntry")]
        private UIChecklistEntry _checklistEntryPrefab = null;
        /// <summary>
        /// Transform of the panel.
        /// </summary>
        [SerializeField]
        [Tooltip("Transform of the panel.")]
        private Transform _panelTransform = null;
        /// <summary>
        /// Next button.
        /// </summary>
        [SerializeField]
        [Tooltip("Next button.")]
        private UINextButton _nextButton = null;
        /// <summary>
        /// Reset button.
        /// </summary>
        /// <summary>
        /// List of calibration entries.
        /// </summary>
        private List<UICalibrationEntry> _calibrationEntryList = new List<UICalibrationEntry>();
        /// <summary>
        /// The virtual room.
        /// </summary>
        private VirtualRoom _virtualRoom = null;

       

        private void CheckInteractable()
        {
            bool interactable = false;

            UIChecklistEntry[] checklistEntries = GetComponentsInChildren<UIChecklistEntry>();

            foreach (UIChecklistEntry checklistEntry in checklistEntries)
            {
                if(!checklistEntry._doneToggle.isOn)
                {
                    interactable = false;
                    break;
                }
                interactable = true;
            }

            _nextButton.interactable = interactable;
            if (_nextButton.interactable)
            {
                _nextButton.nextObject = _virtualRoom;
            }
        }

    

        private void Start()
        {
            var calibrationManager = FindObjectOfType<CalibrationManager>();
            VirtualRoom vroom = calibrationManager.CreateVirtualRoom(XMLManager.instance.blockDB.rooms[0]);
            Init(vroom, calibrationManager);
            CheckInteractable();
        }

        /// <summary>
        /// Initialization of the calibration panel.
        /// </summary>
        /// <param name="vroom"></param>
        /// <param name="calibrationManager"></param>
        public void Init(VirtualRoom vroom, CalibrationManager calibrationManager)
        {
            _virtualRoom = vroom;
            

            foreach(string check in vroom.checklist)
            {
                UIChecklistEntry roomCalEntry = Instantiate(_checklistEntryPrefab, _panelTransform);
                roomCalEntry.Init(check);
                roomCalEntry._doneToggle.onValueChanged.AddListener( delegate { CheckInteractable(); });

            }

         


        }
    }
}
