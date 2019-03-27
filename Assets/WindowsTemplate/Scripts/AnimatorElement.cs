using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CRI.HelloHouston.WindowTemplate
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorElement : AnimationElement
    {
        private Animator _animator;

        protected override void StartShowAnimation()
        { 
            GetComponent<Animator>().SetBool("Show", true);
        }

        protected override void StartHideAnimation()
        {
            GetComponent<Animator>().SetBool("Show", false);
        }
    }
}
