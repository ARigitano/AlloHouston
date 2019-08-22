using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    public struct ParticleEventArgs
    {
        public float value;
        public Particle particle;
    }

    public delegate void ParticleSliderEventHandler(object sender, ParticleEventArgs e);

    [RequireComponent(typeof(Slider))]
    public class MAIAParticleSlider : MonoBehaviour
    {
        public ParticleSliderEventHandler onValueChanged;
        [SerializeField]
        [Tooltip("The particle associated with this slider.")]
        private Particle _particle = null;

        public Particle particle
        {
            get
            {
                return _particle;
            }
        }

        public Slider slider { get; private set; }

        public int currentValue
        {
            get
            {
                return (int)slider.value;
            }
        }

        public void OnValueChanged(float value)
        {
            if (onValueChanged != null)
                onValueChanged(this, new ParticleEventArgs() { particle = this.particle, value = value });
        }

        public void Init(int maxSliderValue)
        {
            if (maxSliderValue == 0)
            {
                gameObject.SetActive(false);
                return;
            }
            slider = GetComponent<Slider>();
            slider.minValue = 0;
            slider.maxValue = maxSliderValue + 0.9f;
            slider.value = 0;
            slider.onValueChanged.AddListener(OnValueChanged);
        }
    }
}