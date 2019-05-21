using CRI.HelloHouston.GameElement;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;
using CRI.HelloHouston.Experience;
using System.Linq;
using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.WindowTemplate;

namespace CRI.HelloHouston.GameElements
{
    /// <summary>
    /// Hologram that changes the experiment tubes.
    /// </summary>
    [RequireComponent(typeof(AnimatorElement))]
    public class ChangeTubeHologram : MonoBehaviour, IHologram
    {
        /// <summary>
        /// Is the experiment currently loading?
        /// </summary>
        private bool isLoading = false;
        /// <summary>
        /// List of all the available experiments for this game
        /// </summary>
        private XPTube[] _tubes = null;
        [SerializeField]
        [Tooltip("The tube slots used to load the experiments.")]
        private TubeSlot[] _tubeSlots = null;
        [SerializeField]
        [Tooltip("Transform of the empty spaces to place the tubes on.")]
        private Transform[] _tubeTransforms = null;
        [SerializeField]
        [Tooltip("Prefab of the xp tube")]
        private XPTube _xpTubePrefab = null;
        [SerializeField]
        [Tooltip("Prefab of the empty tube.")]
        private GameObject _emptyTubePrefab = null;
        [SerializeField]
        [Tooltip("Animator element.")]
        private AnimatorElement _animatorElement;

        public bool visible { get; set; }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3.0f);
            LoadingFinished();
        }

        private void Reset()
        {
            _animatorElement = GetComponent<AnimatorElement>();
        }

        public void Init(GameManager gameManager, XPManager[] managers, VirtualWallTopZone[] topZones)
        {
            Transform[] shuffledTransforms = _tubeTransforms.Shuffle().ToArray();
            _tubes = new XPTube[managers.Length];
            for (int i = 0; i < shuffledTransforms.Length; i++)
            {
                if (i < managers.Length)
                {
                    var go = Instantiate(_xpTubePrefab, shuffledTransforms[i]);
                    go.manager = managers[i];
                    _tubes[i] = go;
                }
                else
                {
                    var go = Instantiate(_emptyTubePrefab, shuffledTransforms[i]);
                }
            }
            for (int i = 0; i < _tubeSlots.Length; i++)
            {
                _tubeSlots[i].Init(gameManager, topZones[i]);
            }
        }

        /// <summary>
        /// Called when a tube is loading.
        /// </summary>
        /// <param name="experience">The experiment beig loaded</param>
        /// <param name="topZone">Index of the wall top zone on which the experiment is loading.</param>
        public void LoadingTube(XPManager experience, int wallTopZoneIndex)
        {
            foreach(XPTube tube in _tubes)
            {
                tube.SetUnavailable();
            }
            StartCoroutine(Wait());
        }

        /// <summary>
        /// Called when a tube has finished loading.
        /// </summary>
        private void LoadingFinished()
        {
            foreach (XPTube tube in _tubes)
            {
                tube.SetAvailable();
            }
        }

        public void ShowHologram()
        {
            visible = true;
            _animatorElement.Show();
        }

        public void HideHologram()
        {
            visible = false;
            _animatorElement.Hide();
        }
    }
}
