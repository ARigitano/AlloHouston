using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CRI.HelloHouston.Experience;
using System.Linq;

namespace CRI.HelloHouston.Calibration.UI
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
        private Button _nextButton = null;
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
                if (!checklistEntry._doneToggle.isOn)
                {
                    interactable = false;
                    break;
                }
                interactable = true;
            }
            _nextButton.interactable = interactable;
        }

        public override void Init(object obj)
        {
            Init((RoomXPPair)obj);
        }

        /// <summary>
        /// Initialization of the calibration panel.
        /// </summary>
        /// <param name="vroom"></param>
        /// <param name="calibrationManager"></param>
        public void Init(RoomXPPair rxpp)
        {
            _virtualRoom = rxpp.vroom;
            string[] checklist = rxpp.vroom.checklist.Concat(rxpp.xpContexts.SelectMany(x => x.xpSettings.checklist)).ToArray();
            foreach (string check in checklist)
            {
                UIChecklistEntry roomCalEntry = Instantiate(_checklistEntryPrefab, _panelTransform);
                roomCalEntry.Init(check);
                roomCalEntry._doneToggle.onValueChanged.AddListener((bool value) => CheckInteractable());
            }
            _nextObject = rxpp;
            _nextButton.onClick.AddListener(Next);
            CheckInteractable();
        }
    }
}
