using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    [RequireComponent(typeof(Toggle))]
    internal class UICategoryToggle : MonoBehaviour
    {
        [SerializeField]
        private Toggle _toggle;
        [SerializeField]
        private Toggle[] _toggles;

        private UnityEngine.Events.UnityAction<bool> _callback;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        public void Init(Toggle[] toggles, string categoryName)
        {
            _toggles = toggles;
            _toggle.GetComponentInChildren<Text>().text = categoryName;
            _toggle.name = "Category Toggle " + categoryName;
            _callback = (value) =>
            {
                foreach (var toggle in toggles)
                {
                    toggle.isOn = value;
                }
            };
            _toggle.onValueChanged.AddListener(_callback);
        }

        public void Refresh()
        {
            _toggle.onValueChanged.RemoveListener(_callback);
            _toggle.isOn = _toggles.All(x => x.isOn);
            _toggle.onValueChanged.AddListener(_callback);
        }
    }
}