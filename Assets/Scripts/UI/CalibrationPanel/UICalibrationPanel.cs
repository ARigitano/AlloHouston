using CRI.HelloHouston.Calibration.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace CRI.HelloHouston.Calibration.UI
{
    public class UICalibrationPanel : UIPanel
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
        /// The calibration manager
        /// </summary>
        [SerializeField]
        [Tooltip("Calibration manager.")]
        private CalibrationManager _calibrationManager = null;
        /// <summary>
        /// List of calibration entries.
        /// </summary>
        private List<UICalibrationEntry> _calibrationEntryList = new List<UICalibrationEntry>();
        /// <summary>
        /// The virtual room.
        /// </summary>
        private VirtualRoom _virtualRoom = null;

        private void OnEnable()
        {
            CalibrationManager.onCalibrationEnd += OnCalibrationEnd;
        }

        private void OnDisable()
        {
            CalibrationManager.onCalibrationEnd -= OnCalibrationEnd;
        }

        private void CheckInteractable()
        {
            bool interactable = true;
            foreach (var calibrationEntry in _calibrationEntryList)
            {
                interactable &= calibrationEntry.virtualItem.calibrated;
            }
            _nextButton.interactable = interactable;
            if (_nextButton.interactable)
            {
                _nextObject = _virtualRoom;
            }
        }

        private void Reset()
        {
            _panelTransform = transform;
        }

        public override void Init(object obj)
        {
            if (_calibrationManager == null)
                _calibrationManager = FindObjectOfType<CalibrationManager>();
            _calibrationManager.StartCalibration();
            VirtualRoom vroom = _calibrationManager.CreateVirtualRoom(DataManager.instance.blockDB.rooms[0]);
            if (_nextButton != null)
                _nextButton.onClick.AddListener(Next);
            Init(vroom, _calibrationManager);
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

        private void OnCalibrationEnd(object sender, CalibrationEventArgs e)
        {
            CheckInteractable();
        }


        public override void Next()
        {
            _calibrationManager.EndCalibration();
            base.Next();
        }
    }
}