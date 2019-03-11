using UnityEngine;

namespace CRI.HelloHouston.Window
{
    public abstract class GenericAnimator : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Delay until the next animator in the collection of animator is ready to play.")]
        protected float _delay;
        /// <summary>
        /// Delay until the next animator in the collection of animators is ready to play.
        /// </summary>
        public float delay
        {
            get
            {
                return _delay;
            }
        }
        public abstract void StartShowAnimation();
        public abstract void StartHideAnimation();
    }
}
