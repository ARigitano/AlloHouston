using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.Calibration.XML;
using System.Collections.Generic;
using UnityEngine;

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
        private Transform _panelTransform;
        /// <summary>
        /// List of calibration entries.
        /// </summary>
        private List<UICalibrationEntry> _calibrationEntryList = new List<UICalibrationEntry>();

        private void Reset()
        {
            _panelTransform = transform;
        }

        private void Start()
        {
            var calibrationManager = FindObjectOfType<CalibrationManager>();
            VirtualRoom vroom = calibrationManager.CreateVirtualRoom(XMLManager.instance.blockDB.rooms[0]);
            Init(vroom, calibrationManager);
        }

        public void Init(VirtualRoom vroom, CalibrationManager calibrationManager)
        {
            UICalibrationEntry roomCalEntry = Instantiate(_calibrationEntryPrefab, _panelTransform);
            roomCalEntry.Init(vroom, calibrationManager);
            UICalibrationEntry tableCalEntry = Instantiate(_calibrationEntryPrefab, _panelTransform);
            tableCalEntry.Init(vroom.table, calibrationManager);
            foreach (VirtualBlock vblock in vroom.blocks)
            {
                UICalibrationEntry blockCalEntry = Instantiate(_calibrationEntryPrefab, _panelTransform);
                blockCalEntry.Init(vblock, calibrationManager);
            }
        }
    }
}