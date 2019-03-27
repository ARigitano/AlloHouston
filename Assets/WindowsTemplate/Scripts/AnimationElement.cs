using UnityEditor;
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
        [HideInInspector]
        public bool visible = false;

        protected abstract void StartShowAnimation();
        protected abstract void StartHideAnimation();

        public void Show()
        {
            visible = true;
            StartShowAnimation();
        }

        public void Hide()
        {
            visible = false;
            StartHideAnimation();
        }
    }

    [CustomEditor(typeof(AnimationElement), true)]
    public class AnimationElementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var animationElement = (AnimationElement)target;
            if (!animationElement.visible && GUILayout.Button("Show"))
                animationElement.Show();
            if (animationElement.visible && GUILayout.Button("Hide"))
                animationElement.Hide();
        }
    }
}
