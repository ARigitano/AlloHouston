using CRI.HelloHouston.WindowTemplate;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAParticlePanel : Window
    {
        [Header("Particle Panel Attributes")]
        [SerializeField]
        [Tooltip("The main tablet script.")]
        private MAIATabletScreen _tablet = null;
        [SerializeField]
        [Tooltip("An array of particle sliders used to represent the number of particles.")]
        private MAIAParticleSlider[] _particleSliders;
        private ParticlesIdentification _piScreen;
        private MAIAManager _manager;

        private void OnDisable()
        {
            foreach (var particleSlider in _particleSliders)
            {
                particleSlider.slider.value = 0;
            }
        }

        private void OnParticleValueChanged(object sender, ParticleEventArgs e)
        {
            _piScreen.UpdateParticles(e);
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
                var l1 = _particleSliders.Select(x => x.particle).OrderBy(particle => particle.symbol).ThenBy(particle => particle.negative).ToArray();
                var l2 = _manager.generatedParticles.OrderBy(particle => particle.symbol).ThenBy(particle => particle.negative).ToArray();
                bool symbols = true;
                for (int i = 0; i < l1.Count(); i++)
                {
                    if (l1[i].symbol != l2[i].symbol)
                    {
                        symbols = false;
                        Debug.Log(l1[i].symbol + " !=" + l2[i].symbol);
                        break;
                    }
                }
                // Checks if the right symbols have been entered.
                if (symbols)
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
            foreach (var particleSlider in _particleSliders)
            {
                particleSlider.Init(manager.generatedParticles.Count);
                particleSlider.onValueChanged += OnParticleValueChanged;
            }
        }
    }
}
