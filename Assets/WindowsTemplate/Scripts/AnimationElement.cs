using UnityEngine;

namespace CRI.HelloHouston.WindowTemplate
{
    public abstract class AnimationElement : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Delay until the next element in the animation sequence should play after the show animation.")]
        protected float _postShowDelay;
        [SerializeField]
        [Tooltip("Delay until the next element in the animation sequence should play after the hide animation.")]
        protected float _postHideDelay;
        /// <summary>
        /// Delay until the next animator in the collection of animators is ready to play.
        /// </summary>
        public float postShowDelay
        {
            get
            {
                return _postShowDelay;
            }
        }

        public float postHideDelay
        {
            get
            {
                return _postHideDelay;
            }
        }
        public abstract void StartShowAnimation();
        public abstract void StartHideAnimation();
    }
}
