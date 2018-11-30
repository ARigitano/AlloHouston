using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experiences.UI
{
    internal class UIHintDisplay : MonoBehaviour
    {
        /// <summary>
        /// A dropdown where the hint choices will be displayed on.
        /// </summary>
        [SerializeField]
        [Tooltip("The dropdown where the hint choices will be displayed on.")]
        private Dropdown _dropdown;
        /// <summary>
        /// An input field where the user can enter custom hints.
        /// </summary>
        [SerializeField]
        [Tooltip("An input field where the user can enter custom hints.")]
        private InputField _inputField;
        /// <summary>
        /// The enter button.
        /// </summary>
        [SerializeField]
        [Tooltip("The enter button.")]
        private Button _enterButton;

        private GameManager _gameManager;
        private bool _wasFocused;
        private GameHint[] _currentHints;

        private void OnEnable()
        {
            GameManager.onExperienceChange += RefreshDropdown;
        }

        private void OnDisable()
        {
            GameManager.onExperienceChange -= RefreshDropdown;
        }

        private void Reset()
        {
            _inputField = GetComponentInChildren<InputField>();
            _dropdown = GetComponentInChildren<Dropdown>();
            _enterButton = GetComponentInChildren<Button>();
        }

        private void Start()
        {
            _gameManager = GameManager.instance;
            _enterButton.onClick.AddListener(Validate);
            _dropdown.onValueChanged.AddListener((value) =>
            {
                _inputField.text = _currentHints[value].hint;
            });
            RefreshDropdown();
        }

        private void RefreshDropdown()
        {
            GameHint[] hints = _gameManager.GetAllCurrentHints();
            _dropdown.ClearOptions();
            _currentHints = hints;
            foreach (GameHint hint in hints)
            {
                _dropdown.options.Add(new Dropdown.OptionData(hint.ToString()));
            }
        }

        private void Validate()
        {
            if (!string.IsNullOrEmpty(_inputField.text))
            {
                _gameManager.SendHintToPlayers(_inputField.text);
                _inputField.text = "";
            }
        }

        private void Update()
        {
            _enterButton.interactable = !string.IsNullOrEmpty(_inputField.text);
            if (_wasFocused && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
                Validate();
            _wasFocused = _inputField.isFocused;
        }
    }
}