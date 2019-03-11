using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Window
{
    [RequireComponent(typeof(Animator))]
    public class ButtonAnimator : GenericAnimator
    {
        private Animator _animator;

        public override void StartShowAnimation()
        { 
            GetComponent<Animator>().SetBool("Show", true);
        }

        public override void StartHideAnimation()
        {
            GetComponent<Animator>().SetBool("Show", false);
        }
    }
}
