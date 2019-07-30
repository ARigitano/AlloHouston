using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CRI.HelloHouston.WindowTemplate
{
    [RequireComponent(typeof(Button))]
    public class ButtonAnimation : AnimatorElement, IPointerClickHandler
    {
        [Tooltip("The target button of the animation.")]
        [SerializeField]
        private Button _button;

        [Tooltip("Duration of the pressed button animation (in seconds).")]
        [SerializeField]
        private float _pressedTime = 0.1f;

        private Sprite _defaultSprite;

        private void Reset()
        {
            _button = GetComponentInParent<Button>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StopAllCoroutines();
            if (_defaultSprite == null)
                _defaultSprite = _button.image.sprite;
            if (_button.gameObject.GetComponent<UISounds>() != null)
                _button.gameObject.GetComponent<UISounds>().PlayPressed();
            _button.image.sprite = _button.spriteState.pressedSprite;
            StartCoroutine(Unpress());
        }

        private IEnumerator Unpress()
        {
            yield return new WaitForSeconds(_pressedTime);
            if (_defaultSprite != null)
                _button.image.sprite = _defaultSprite;
        }
    }
}
