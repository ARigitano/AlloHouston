using CRI.HelloHouston.Calibration.XML;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Calibration.UI
{
    public class UICalibrationPanel : MonoBehaviour
    {
        /// <summary>
        /// Prefab of a calibration entry.
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab of a CalibrationEntry")]
        private UICalibrationEntry _calibrationEntryPrefab = null;
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
        /// Reset button.
        /// </summary>
        [SerializeField]
        [Tooltip("Reset button.")]
        private Button _resetButton = null;
        /// <summary>
        /// List of calibration entries.
        /// </summary>
        private List<UICalibrationEntry> _calibrationEntryList = new List<UICalibrationEntry>();

        private void OnEnable()
        {
            CalibrationManager.onCalibrationEnd += OnCalibrationEnd;
        }

        private void OnDisable()
        {
            CalibrationManager.onCalibrationEnd -= OnCalibrationEnd;
        }

        private void OnCalibrationEnd()
        {
            CheckInteractable();
        }

        private void CheckInteractable()
        {
            bool interactable = true;
            foreach (var calibrationEntry in _calibrationEntryList)
            {
                interactable &= calibrationEntry.virtualItem.calibrated;
            }
            _nextButton.interactable = interactable;
        }

        private void Reset()
        {
            _panelTransform = transform;
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
            UICalibrationEntry roomCalEntry = Instantiate(_calibrationEntryPrefab, _panelTransform);
            _resetButton.onClick.AddListener(() => calibrationManager.ResetVirtualItems());
            roomCalEntry.Init(vroom, calibrationManager);
            _calibrationEntryList.Add(roomCalEntry);
            foreach (VirtualBlock vblock in vroom.blocks)
            {
                UICalibrationEntry blockCalEntry = Instantiate(_calibrationEntryPrefab, _panelTransform);
                blockCalEntry.Init(vblock, calibrationManager);
                _calibrationEntryList.Add(blockCalEntry);
            }
        }
    }
}