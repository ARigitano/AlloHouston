﻿using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience.UI
{
    internal class UIExperienceDisplay : MonoBehaviour
    {
        /// <summary>
        /// Experience status prefab.
        /// </summary>
        [SerializeField]
        [Tooltip("Experience status prefab.")]
        private UIExperienceStatus _experienceStatusPrefab = null;
        /// <summary>
        /// Experience status content transform.
        /// </summary>
        [SerializeField]
        [Tooltip("Experience status content transform.")]
        private Transform _experienceStatusContentTransform = null;

        public void Init(XPManager[] xpSynchronizers)
        {
            foreach (var xpSynchronizer in xpSynchronizers)
            {
                UIExperienceStatus go = GameObject.Instantiate(_experienceStatusPrefab, _experienceStatusContentTransform);
                go.Init(GameManager.instance, xpSynchronizer);
            }
        }
    }
}
