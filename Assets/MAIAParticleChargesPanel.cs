using System.Linq;
using UnityEngine;
using CRI.HelloHouston.WindowTemplate;
using System.Collections.Generic;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAParticleChargesPanel : Window
    {
        private struct ParticleCount
        {
            public Particle particle;
            public int count;
        }
        [Header("Particle Panel Attributes")]
        [SerializeField]
        [Tooltip("The main tablet script.")]
        private MAIATabletScreen _tablet = null;
        [SerializeField]
        [Tooltip("An array of particle sliders used to represent the number of particles.")]
        private MAIAParticleSlider[] _particleSliders = null;
        private ParticlesIdentification _piScreen;
        private MAIAManager _manager;
        private Dictionary<string, int> _particleSymbolCount;

        private void OnDisable()
        {
            foreach (var particleSlider in _particleSliders)
            {
                particleSlider.slider.value = 0;
            }
        }

        private void OnParticleValueChanged(object sender, ParticleEventArgs e)
        {
            var sliders = _particleSliders.Where(slider => slider.particle.symbol == e.particle.symbol).ToArray();
            int sum = sliders.Sum(slider => slider.currentValue);
            // We have more combined charges than possible
            if (sum > _particleSymbolCount[e.particle.symbol])
            {
                // We take the other slider and substract the difference
                sliders.First(slider => (object)slider != sender).slider.value -= (sum - _particleSymbolCount[e.particle.symbol]);
            }
            _piScreen.UpdateParticleCharges(e);
        }

        /// <summary>
        /// Sends an error message to the top screen.
        /// </summary>
        public void ParticleSendErrorMessage(ParticlesIdentification.ErrorType particleErrorType)
        {
            _piScreen.DisplayErrorMessage(particleErrorType);
        }

        /// <summary>
        /// Submits the particles combination entered.
        /// </summary>
        public void SubmitParticles()
        {
            int count = _particleSliders.Sum(x => x.currentValue);
            //Checks if the combination entered has the right number of particles.
            if (count == _manager.generatedParticles.Count)
            {
                var l1 = _particleSliders.Select(slider => new ParticleCount() { particle = slider.particle, count = (int)slider.currentValue }).ToArray();
                var l2 = _manager.generatedParticles.GroupBy(particle => particle).Select(group => new ParticleCount() { particle = group.Key, count = group.Count() }).ToArray();
                bool charges = true;
                for (int i = 0; i < l1.Count(); i++)
                {
                    if (l2.First(x => x.particle == l1[i].particle).count != l1[i].count)
                    {
                        charges = false;
                        break;
                    }
                }
                // Checks if the right symbols have been entered.
                if (charges)
                {
                    _tablet.OnRightParticleCombination();
                }
                else
                {
                    //A wrong combination of symbols have been entered.
                    ParticleSendErrorMessage(ParticlesIdentification.ErrorType.WrongParticles);
                }
            }
            else
            {
                //A combination of particles with a wrong length has been entered.
                ParticleSendErrorMessage(ParticlesIdentification.ErrorType.WrongNumberParticles);
            }
        }

        public void Init(MAIAManager manager, ParticlesIdentification piScreen)
        {
            _manager = manager;
            _piScreen = piScreen;
            _particleSymbolCount = new Dictionary<string, int>();
            foreach (var group in _manager.generatedParticles.GroupBy(particle => particle.symbol))
            {
                _particleSymbolCount.Add(group.Key, group.Count());
            }
            foreach (var particleSlider in _particleSliders)
            {
                particleSlider.Init(_particleSymbolCount[particleSlider.particle.symbol]);
                particleSlider.onValueChanged += OnParticleValueChanged;
            }
        }
    }
}

