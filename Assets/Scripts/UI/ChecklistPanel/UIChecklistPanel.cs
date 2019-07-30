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

        private void CheckInteractable()
        {
            UIChecklistEntry[] checklistEntries = GetComponentsInChildren<UIChecklistEntry>();
            _nextButton.interactable = checklistEntries.All(x => x.doneToggle.isOn);
        }

        public override void Init(object obj)
        {
            Init((RoomSettings)obj);
        }

        /// <summary>
        /// Initialization of the calibration panel.
        /// </summary>
        /// <param name="vroom"></param>
        /// <param name="calibrationManager"></param>
        public void Init(RoomSettings rxpp)
        {
            string[] checklist = rxpp.vroom.checklist.Concat(rxpp.xpContexts.Where(x => x.xpSettings != null).SelectMany(x => x.xpSettings.checklist)).ToArray();
            foreach (string check in checklist)
            {
                UIChecklistEntry roomCalEntry = Instantiate(_checklistEntryPrefab, _panelTransform);
                roomCalEntry.Init(check);
                roomCalEntry.doneToggle.onValueChanged.AddListener((bool value) => CheckInteractable());
            }
            _nextObject = rxpp;
            _nextButton.onClick.AddListener(Next);
            CheckInteractable();
        }
    }
}
