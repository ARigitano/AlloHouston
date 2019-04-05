using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.WindowTemplate
{
    [ExecuteInEditMode]
    public class SliderValue : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("A slider.")]
        private Slider _slider;
        [SerializeField]
        [Tooltip("The text on which the value of the slider is displayed.")]
        private Text _text;

        private void Reset()
        {
            _slider = GetComponentInParent<Slider>();
            _text = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _text.text = ((int)value).ToString();
        }
    }
}
