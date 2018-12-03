using CRI.HelloHouston.Experience.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    internal class UIAction : MonoBehaviour
    {
        private GeneralAction _action;
        [SerializeField]
        private Button _button;

        private void Reset()
        {
            _button = GetComponentInChildren<Button>();
        }

        private void Init(GeneralAction action)
        {
            _action = action;
        }
    }
}