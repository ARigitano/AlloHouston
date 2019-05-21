using CRI.HelloHouston.Experience;
using UnityEngine;

namespace CRI.HelloHouston.GameElements
{
    [RequireComponent(typeof(Animator))]
    public class PlierAnimator : MonoBehaviour
    {
        public XPManager manager { get; set; }
        public GameObject tubex { get; set; }
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void UninstallTubex()
        {
            _animator.SetBool("Open", true);
            _animator.SetTrigger("Start");
        }

        public void InstallTubex()
        {
            _animator.SetBool("Open", false);
            _animator.SetTrigger("Start");
        }
    }
}