using System;
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

        private void Awake()
        {
            //_animator = GetComponent<Animator>();
        }

        protected override void StartShowAnimation()
        { 
            //_animator.SetBool("Show", true);
        }

        protected override void StartHideAnimation()
        {
            //_animator.SetBool("Show", false);
        }
    }
}
