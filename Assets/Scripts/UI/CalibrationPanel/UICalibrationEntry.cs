using System;
using CRI.HelloHouston.Translation;
using UnityEngine;
using UnityEngine.UI;
using CRI.HelloHouston.Experience;

namespace CRI.HelloHouston.Calibration.UI
{
    public class UICalibrationEntry : MonoBehaviour
    {
        /// <summary>
        /// Text field of the entry's name.
        /// </summary>
        [SerializeField]
        [Tooltip("Text field of the entry's name.")]
        private Text _nameText = null;
        /// <summary>
        /// Button to start the calibration of the entry.
        /// </summary>
        [SerializeField]
        [Tooltip("Button to start the calibration of the entry.")]
        private Button _calibrationButton = null;
        /// <summary>
        /// Image to tell if the calibration is successful.
        /// </summary>
        [SerializeField]
        [Tooltip("Image to tell if the calibration is successful")]
        private UICalibrationValidationButton _calibrationValidationButton = null;
        /// <summary>
        /// Text field of the date of the last calibration.
        /// </summary>
        [SerializeField]
        [Tooltip("Text field of the date of the last calibration.")]
        private Text _dateText = null;
        /// <summary>
        /// Calibration text key.
        /// </summary>
        [SerializeField]
        [Tooltip("Calibration text key.")]
        public string _calibrationText;
        /// <summary>
        /// Ongoing calibration text.
        /// </summary>
        [SerializeField]
        [Tooltip("Ongoing calibration text")]
        public string _ongoingCalibrationText;

        public VirtualItem virtualItem {
            get;
            private set;
        }

        private void OnEnable()
        {
            if (virtualItem != null)
            {
                virtualItem.onDateChange += OnDateChange;
                virtualItem.onCalibratedChange += OnCalibratedChange;
            }
            CalibrationManager.onCalibrationEnd += OnCalibrationEnd;
        }

        private void OnDisable()
        {
            if (virtualItem != null)
            {
                virtualItem.onDateChange -= OnDateChange;
                virtualItem.onCalibratedChange -= OnCalibratedChange;
            }
            CalibrationManager.onCalibrationEnd -= OnCalibrationEnd;
        }

        private void Init(VirtualItem virtualItem, CalibrationManager calibrationManager)
        {
            this.virtualItem = virtualItem;
            _dateText.text = virtualItem.lastUpdate.ToString();
            _calibrationButton.GetComponentInChildren<MainTranslatedText>().InitTranslatedText(GameManager.instance, _calibrationText);
            _calibrationValidationButton.SetValidation(virtualItem.calibrated);
            virtualItem.onDateChange += OnDateChange;
            virtualItem.onCalibratedChange += OnCalibratedChange;
            _calibrationButton.onClick.AddListener(() =>
            {
                calibrationManager.StartObjectCalibration(virtualItem);
                _calibrationButton.GetComponentInChildren<Text>().text = calibrationManager.remainingPositionTags.Value.ToString();
                _calibrationButton.interactable = false;
                CalibrationManager.onUpdatePositionTag += OnUpdatePositionTag;
            }
            );
        }

        /// <summary>
        /// Initialization of the calibration entry.
        /// </summary>
        /// <param name="virtualRoom"></param>
        /// <param name="calibrationManager"></param>
        public void Init(VirtualRoom virtualRoom,
            CalibrationManager calibrationManager)
        {
            _nameText.text = string.Format("Room {0}", virtualRoom.index);
            Init((VirtualItem)virtualRoom, calibrationManager);
        }

        /// <summary>
        /// Initialization of the calibration entry.
        /// </summary>
        /// <param name="virtualBlock"></param>
        /// <param name="calibrationManager"></param>
        public void Init(VirtualBlock virtualBlock,
            CalibrationManager calibrationManager)
        {
            _nameText.text = string.Format("∟ {2} - Block {0} {1}", virtualBlock.block.type, virtualBlock.block.index, virtualBlock.indexInRoom + 1);
            Init((VirtualItem)virtualBlock, calibrationManager);
        }

        private void OnDateChange(object sender, VirtualItemEventArgs e)
        {
            _dateText.text = e.updateTime.ToString();
        }

        private void OnCalibratedChange(object sender, VirtualItemEventArgs e)
        {
            _calibrationValidationButton.SetValidation(e.calibrated);
        }

        private void OnUpdatePositionTag(object sender, CalibrationPositionEventArgs e)
        {
            _calibrationButton.GetComponentInChildren<Text>().text = e.remainingPositionTags != null ? e.remainingPositionTags.ToString() : "";
        }

        private void OnCalibrationEnd(object sender, CalibrationEventArgs e)
        {
            _calibrationButton.interactable = true;
            _calibrationButton.GetComponentInChildren<MainTranslatedText>().InitTranslatedText(GameManager.instance, _calibrationText);
            CalibrationManager.onUpdatePositionTag -= OnUpdatePositionTag;
        }
    }
}