using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.Calibration.XML;
using CRI.HelloHouston.Translation;
using UnityEngine;
using UnityEngine.UI;
using VRCalibrationTool;

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
        private Image _calibrationSuccessfulImage = null;
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

        private VirtualItem _virtualItem;

        private void OnEnable()
        {
            if (_virtualItem != null)
            {
                _virtualItem.onDateChange += OnDateChange;
                _virtualItem.onCalibratedChange += OnCalibratedChange;
            }
            CalibrationManager.onCalibrationEnd += OnCalibrationEnd;
        }

        private void OnDisable()
        {
            if (_virtualItem != null)
            {
                _virtualItem.onDateChange -= OnDateChange;
                _virtualItem.onCalibratedChange -= OnCalibratedChange;
            }
            CalibrationManager.onCalibrationEnd -= OnCalibrationEnd;
        }

        public void Init(VirtualRoom virtualRoom,
            CalibrationManager calibrationManager)
        {
            _virtualItem = virtualRoom;
            _nameText.text = string.Format("Room {0}", virtualRoom.index);
            _dateText.text = virtualRoom.lastUpdate.ToString();
            _calibrationButton.GetComponentInChildren<TranslatedText>().InitTranslatedText(_calibrationText);
            _calibrationSuccessfulImage.color = virtualRoom.calibrated ? Color.green : Color.red;
            virtualRoom.onDateChange += OnDateChange;
            virtualRoom.onCalibratedChange += OnCalibratedChange;
            _calibrationButton.onClick.AddListener(() =>
            {
                calibrationManager.StartCalibration(virtualRoom);
                _calibrationButton.interactable = false;
                _calibrationButton.GetComponentInChildren<TranslatedText>().InitTranslatedText(_ongoingCalibrationText);
            }
            );
        }

        public void Init(VirtualBlock virtualBlock,
            CalibrationManager calibrationManager)
        {
            _virtualItem = virtualBlock;
            _nameText.text = string.Format("∟ Block {0} {1}", virtualBlock.block.type, virtualBlock.block.index);
            _dateText.text = virtualBlock.lastUpdate.ToString();
            _calibrationButton.GetComponentInChildren<TranslatedText>().InitTranslatedText(_calibrationText);
            virtualBlock.onDateChange += OnDateChange;
            virtualBlock.onCalibratedChange += OnCalibratedChange;
            _calibrationButton.onClick.AddListener(() =>
            {
                calibrationManager.StartCalibration(virtualBlock);
                _calibrationButton.interactable = false;
                _calibrationButton.GetComponentInChildren<TranslatedText>().InitTranslatedText(_ongoingCalibrationText);
            });
        }

        private void OnDateChange(System.DateTime date)
        {
            _dateText.text = date.ToString();
        }

        private void OnCalibratedChange(bool b)
        {
            _calibrationSuccessfulImage.color = b ? Color.green : Color.red;
        }

        private void OnCalibrationEnd()
        {
            _calibrationButton.interactable = true;
            _calibrationButton.GetComponentInChildren<TranslatedText>().InitTranslatedText(_calibrationText);
        }
    }
}