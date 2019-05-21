using CRI.HelloHouston.GameElement;
using UnityEngine;
using CRI.HelloHouston.WindowTemplate;

namespace CRI.HelloHouston.GameElements
{
    [RequireComponent(typeof(AnimatorElement))]
    public class AnimatorHologram : MonoBehaviour, IHologram
    {
        public bool visible { get; set; }
        [SerializeField]
        [Tooltip("Animator element.")]
        private AnimatorElement _animatorElement;

        private void Reset()
        {
            _animatorElement = GetComponent<AnimatorElement>();
        }

        public void HideHologram()
        {
            visible = false;
            _animatorElement.Hide();
        }

        public void ShowHologram()
        {
            visible = true;
            _animatorElement.Show();
        }
    }
}