using System;
using UnityEngine;

namespace CRI.HelloHouston.GameElements
{
    [RequireComponent(typeof(Animator))]
    public class ArmAnimator : MonoBehaviour
    {
        public event Action onAnimationEnd;
        public event Action onAnimationStart;
        [SerializeField]
        [Tooltip("Point on which the instantiated tubed is to be attached.")]
        private Transform _attachPoint = null;
        [SerializeField]
        [Tooltip("Starting transforms of the tubex.")]
        private Transform[] _tubexAttachPoints = null;
        /// <summary>
        /// If true, the ArmAnimator is currently performing an animation.
        /// </summary>
        public bool busy { get; set; }

        private GameObject _tubex;
        private Animator _animator;
        private Transform _plierTransform;
        private Action _installAction;
        private Action _uninstallAction;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetTubex(GameObject tubex, int animationIndex, Transform plierTransform, bool install)
        {
            _tubex = tubex;
            if (install)
            {
                if (animationIndex < _tubexAttachPoints.Length)
                    _tubex.transform.SetParent(_tubexAttachPoints[animationIndex]);
                _plierTransform = plierTransform;
                _tubex.transform.localPosition = Vector3.zero;
                _tubex.transform.localRotation = Quaternion.identity;
            }
        }

        /// <summary>
        /// Launches the install tubex animation.
        /// </summary>
        /// <param name="index">The index of the animation.</param>
        /// <param name="onInstall">Action to be triggered at the exact animation frame the tubex gets installed.</param>
        public void InstallTubex(int index, int animationIndex, Action onInstall = null)
        {
            _animator.SetInteger("Index", index);
            _animator.SetBool("Install", true);
            _animator.SetTrigger("StartAnimation");
            _animator.SetInteger("AnimIndex", animationIndex);
            _installAction = onInstall;
        }

        /// <summary>
        /// Launches the uninstall tubex animation.
        /// </summary>
        /// <param name="index">The index of the animation.</param>
        /// <param name="onUninstall">Action to be triggered at the exact animation frame the tubex gets uninstalled.</param>
        public void UninstallTubex(int index, int animationIndex, Action onUninstall = null)
        {
            _animator.SetInteger("Index", index);
            _animator.SetBool("Install", false);
            _animator.SetTrigger("StartAnimation");
            _animator.SetInteger("AnimIndex", animationIndex);
            _uninstallAction = onUninstall;
        }

        /// <summary>
        /// Instantiates the tubex if we're playing the tubex installation animation,
        /// Destroys the tubex if we're playing the tubex uninstallation animation.
        /// </summary>
        public void CreateDestroyTubex()
        {
            if (!_animator.GetBool("Install"))
                Destroy(_tubex);
        }

        /// <summary>
        /// Called by the Animator at the exact frame when the arm picks up the tubex.
        /// Attachs the tubex to the arm's attach point.
        /// </summary>
        public void OnTubexPickup()
        {
            _tubex.transform.SetParent(_attachPoint);
        }

        /// <summary>
        /// Called by the Animator at the exact frame when the arm deposits the tubex.
        /// Destroys the tubex.
        /// </summary>
        public void OnTubexDeposit()
        {
            Destroy(_tubex);
        }

        /// <summary>
        /// Called by the Animator at the exact frame when the arm installs the tubex.
        /// Attach the tubex to the plier's attach point.
        /// </summary>
        public void OnTubexInstallation()
        {
            _tubex.transform.SetParent(_plierTransform);
            if (_installAction != null)
                _installAction.Invoke();
        }

        /// <summary>
        /// Called by the Animator at the exact frame when the arm picks up the tubex from the plier.
        /// Attachs the tubex to the arm's attach point.
        /// </summary>
        public void OnTubexUninstallation()
        {
            _tubex.transform.SetParent(_attachPoint);
            if (_installAction != null)
                _uninstallAction.Invoke();
        }

        /// <summary>
        /// Called by the Animator when the animation starts.
        /// </summary>
        public void OnAnimationStart()
        {
            busy = true;
            if (onAnimationStart != null)
                onAnimationStart();
        }

        /// <summary>
        /// Called by the Animator when the animation ends.
        /// </summary>
        public void OnAnimationEnd()
        {
            busy = false;
            if (!_animator.GetBool("Install"))
                Destroy(_tubex);
            if (onAnimationEnd != null)
                onAnimationEnd();
        }
    }
}