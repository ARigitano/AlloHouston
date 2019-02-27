using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Translation.UI
{
    public class UILangPanel : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The lang dropdown.")]
        private Dropdown _dropdown;

        private void Reset()
        {
            _dropdown = GetComponentInChildren<Dropdown>();
        }

        private void Start()
        {
            InitDropdown();
        }

        private void InitDropdown()
        {

        }
    }
}
