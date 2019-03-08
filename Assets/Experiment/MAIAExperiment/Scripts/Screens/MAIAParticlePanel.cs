using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAParticlePanel : MonoBehaviour
    {
        /// <summary>
        /// The particles entered by the player.
        /// </summary>
        private List<Particle> _enteredParticles = new List<Particle>();
        [SerializeField]
        [Tooltip("The main tablet script.")]
        private MAIATabletScreen _tablet = null;
        private ParticlesIdentification _piScreen;
        private MAIAManager _manager;

        private void OnDisable()
        {
            ClearParticles();
        }

        /// <summary>
        /// Clears the particles combination entered.
        /// </summary>
        public void ClearParticles()
        {
            _enteredParticles.Clear();
            _piScreen.UpdateParticles(_enteredParticles);
        }

        /// <summary>
        /// Sends an error message to the top screen.
        /// </summary>
        public void ParticleSendErrorMessage(ParticlesIdentification.ErrorType particleErrorType)
        {
            _piScreen.DisplayErrorMessage(particleErrorType);
        }

        /// <summary>
        /// Deletes the last entered particle.
        /// </summary>
        public void DeleteParticle()
        {
            if (_enteredParticles.Count > 0)
                _enteredParticles.RemoveAt(_enteredParticles.Count - 1);
            _piScreen.UpdateParticles(_enteredParticles);
        }

        /// Adds a particle to the combination.
        /// </summary>
        /// <param name="particleButton">The particle to add.</param>
        public void AddParticle(Particle particle)
        {
            if (_enteredParticles.Count < _manager.generatedParticles.Count)
            {
                _manager.logController.AddLogInput("Particle input: " + particle.particleName, _manager.xpContext);
                _enteredParticles.Add(particle);
                _piScreen.UpdateParticles(_enteredParticles);
            }
        }

        /// <summary>
        /// Submits the particles combination entered.
        /// </summary>
        public void SubmitParticles()
        {
            //Checks if the combination entered has the right number of particles.
            if (_enteredParticles.Count == _manager.generatedParticles.Count)
            {
                var l1 = _enteredParticles.OrderBy(particle => particle.symbol).ThenBy(particle => particle.negative).ToArray();
                var l2 = _manager.generatedParticles.OrderBy(particle => particle.symbol).ThenBy(particle => particle.negative).ToArray();
                bool symbols = true;
                bool charges = true;
                for (int i = 0; i < l1.Count(); i++)
                {
                    if (l1[i].symbol != l2[i].symbol)
                    {
                        symbols = false;
                        Debug.Log(l1[i].symbol + " !=" + l2[i].symbol);
                        break;
                    }
                    if (l1[i].negative != l2[i].negative && !l1[i].particleName.ToLower().Contains("neutrino"))
                        charges = false;
                }
                // Checks if the right symbols have been entered.
                if (symbols)
                {
                    //Check if the right symbols + charges have been entered. We ignore the neutrino particle in this process.
                    if (charges)
                    {
                        //The right combination of particles have been entered.
                        _tablet.OnRightParticleCombination();
                    }
                    else
                    {
                        //A wrong combination of charges have been entered.
                        ParticleSendErrorMessage(ParticlesIdentification.ErrorType.WrongNumberCharges);
                    }
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
        }
    }
}
