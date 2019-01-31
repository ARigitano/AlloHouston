using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAAnalysisScreen : MonoBehaviour
    {
        /// <summary>
        /// Script for the whole top screen.
        /// </summary>
        [SerializeField]
        private MAIATopScreen _maiaTopScreen = null;
        /// <summary>
        /// Slots where the diagrams of the chosen reactions are displayed.
        /// </summary>
        [SerializeField]
        private MAIACaseDiagram[] _diagrams = null;
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
        /// Error popup.
        /// </summary>
        [SerializeField]
        private GameObject _errorPopup = null;

        [SerializeField]
        private float _analysisAnimationStepDuration = 2.0f;

        public void StartAnalysisAnimation()
        {
            StartCoroutine(AnalysisAnimation());
        }

        private IEnumerator AnalysisAnimation()
        {
            var dictionary = new Dictionary<Particle, int>();
            _particleGridCellDictionary = new Dictionary<Particle, ParticleGridCell>();
            var otherReactions = _maiaTopScreen.manager.ongoingReactions.Where(reaction => reaction != _maiaTopScreen.manager.selectedReaction).ToArray();
            foreach (var particleGroup in _maiaTopScreen.manager.generatedParticles.OrderBy(particle => particle.symbol).ThenBy(particle => !particle.negative).GroupBy(particle => particle))
            {
                dictionary.Add(particleGroup.Key, particleGroup.Count());
                var particleGridCell = Instantiate(_particleGridCellPrefab, _particleGridTransform);
                particleGridCell.Init(particleGroup.Key);
                _particleGridCellDictionary.Add(particleGroup.Key, particleGridCell);
            }
            DisplayParticles(dictionary);
            for (int i = 0; i < otherReactions.Length; i++)
            {
                var reaction = otherReactions[i];
                yield return new WaitForSeconds(_analysisAnimationStepDuration);
                foreach (var particleGroup in reaction.exit.particles.GroupBy(particle => particle))
                    dictionary[particleGroup.Key] -= particleGroup.Count();
                DisplayParticles(dictionary);
                DisplayDiagram(reaction.diagramImage, i);
            }
            yield return new WaitForSeconds(_analysisAnimationStepDuration);
            _diagrams[_diagrams.Length - 1].gameObject.SetActive(true);
            yield return new WaitForSeconds(_analysisAnimationStepDuration);
            _errorPopup.gameObject.SetActive(true);
            yield return new WaitForSeconds(_analysisAnimationStepDuration);
            _maiaTopScreen.manager.StartReactionIdentification();
        }

        private void DisplayParticles(Dictionary<Particle, int> dictionary)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                var group = dictionary.ElementAt(i);
                _particleGridCellDictionary[group.Key].SetText(group.Value.ToString());
            }
        }

        private void DisplayDiagram(Sprite sprite, int i)
        {
            _diagrams[i].gameObject.SetActive(true);
            _diagrams[i].SetSprite(sprite);
        }
    }
}
