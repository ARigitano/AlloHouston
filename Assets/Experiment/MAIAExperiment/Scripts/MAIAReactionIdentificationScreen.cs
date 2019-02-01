using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAReactionIdentificationScreen : MonoBehaviour
    {
        /// <summary>
        /// The top screen.
        /// </summary>
        [SerializeField]
        private MAIATopScreen _topScreen = null;
        /// <summary>
        /// Particle grid cell prefab.
        /// </summary>
        [SerializeField]
        private ParticleGridCell _particleGridCellPrefab = null;
        /// <summary>
        /// Particle grid cell dictionary.
        /// </summary>
        private Dictionary<Particle, ParticleGridCell> _particleGridCellDictionary;
        /// <summary>
        /// Particle grid transform.
        /// </summary>
        [SerializeField]
        private Transform _particleGridTransform = null;
        /// <summary>
        /// Popup displayed when selecting the wrong Feynman diagram.
        /// </summary>
        [SerializeField]
        private GameObject _popupLose = null;
        /// <summary>
        /// Popups displayed when the right or wrong Feynman diagram.
        /// </summary>
        [SerializeField]
        private GameObject _popupWin = null;
        /// <summary>
        /// Analysis popup.
        /// </summary>
        [SerializeField]
        private GameObject _analysisPopup = null;
        /// <summary>
        /// Analysis duration.
        /// </summary>
        [SerializeField]
        private float _analysisDuration = 3.0f;
        /// <summary>
        /// Array of text to be displayed if the reaction selected is the wrong one.
        /// </summary>
        [SerializeField]
        [Tooltip("Array of text to be displayed if the reaction selected is the wrong one.")]
        private string[] _loseText = null;

        private int _strike = 0;

        /// <summary>
        /// Effect if the correct Feynman diagram is selected, or a wrong one.
        /// </summary>
        /// <param name="realReaction">The real reaction.</param>
        /// <param name="reactionSelected">The reaction selected by the player.</param>
        public void ReactionSelected(bool correctDiagram)
        {
            StartCoroutine(ReactionSelectedAnimation(correctDiagram));
        }

        private IEnumerator ReactionSelectedAnimation(bool correctDiagram)
        {
            _analysisPopup.SetActive(true);
            yield return new WaitForSeconds(_analysisDuration);
            _analysisPopup.SetActive(false);
            if (correctDiagram)
            {
                _popupWin.SetActive(true);
            }
            else
            {
                _popupLose.GetComponentInChildren<Text>().text = _strike < _loseText.Length ? _loseText[_strike] : _loseText[_loseText.Length - 1];
                _strike++;
                _popupLose.SetActive(true);
                StartCoroutine(_topScreen.WaitGeneric(2f, () =>
                {
                    _popupLose.SetActive(false);
                }));
            }
        }

        public void StartReactionIdentification()
        {
            var dictionary = new Dictionary<Particle, int>();
            _particleGridCellDictionary = new Dictionary<Particle, ParticleGridCell>();
            foreach (var particleGroup in _topScreen.manager.settings.allParticles.OrderBy(particle => particle.symbol).ThenBy(particle => !particle.negative).GroupBy(particle => particle))
            {
                dictionary.Add(particleGroup.Key, 0);
                var particleGridCell = Instantiate(_particleGridCellPrefab, _particleGridTransform);
                particleGridCell.Init(particleGroup.Key);
                _particleGridCellDictionary.Add(particleGroup.Key, particleGridCell);
            }
            foreach (var particleGroup in _topScreen.manager.selectedReaction.exit.particles.GroupBy(particle => particle))
                dictionary[particleGroup.Key] += particleGroup.Count();
            DisplayParticles(dictionary);
        }

        private void DisplayParticles(Dictionary<Particle, int> dictionary)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                var group = dictionary.ElementAt(i);
                _particleGridCellDictionary[group.Key].SetText(group.Value.ToString());
            }
        }
    }
}
