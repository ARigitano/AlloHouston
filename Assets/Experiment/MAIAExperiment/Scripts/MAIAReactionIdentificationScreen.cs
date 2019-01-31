using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
