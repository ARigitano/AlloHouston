using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.WindowTemplate
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorElement : AnimationElement
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
