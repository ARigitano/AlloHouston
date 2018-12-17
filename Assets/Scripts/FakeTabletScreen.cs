using CRI.HelloHouston.Experience;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

namespace CRI.HelloHouston.ParticlePhysics
{
    /// <summary>
    /// The tablet screen of the experiment block for the particle physics experiment.
    /// </summary>
    public class FakeTabletScreen : XPElement
    {
        /// <summary>
        /// All the particle scriptable objects.
        /// </summary>
        [SerializeField]
        private Particle[] _allParticles;
        /// <summary>
        /// All the reaction scriptable objects.
        /// </summary>
        [SerializeField]
        public Reaction[] _allReactions;
        /// <summary>
        /// Path to the particle scriptable objects folder.
        /// </summary>
        private static string _path = "Particles";
        /// <summary>
        /// Path to the particle scriptable objects folder.
        /// </summary>
        private static string _pathReaction = "reactions";
        /// <summary>
        /// Contains the combination of particles randomly generated.
        /// </summary>
        public Particle[] particleTypes;
        /// <summary>
        /// Synchronizer for this experiment.
        /// </summary>
        [SerializeField]
        private FakeSynchronizer _synchronizer;
        /// <summary>
        /// All the panels for the tablet screen.
        /// </summary>
        [SerializeField]
        private GameObject _panel, _b1C2, _b1C4, _b1C4Left, _b1C5Left, _b1C6Left;
        /// <summary>
        /// Loading bar to display the time remaining.
        /// </summary>
        [SerializeField]
        private Slider _slider;
        /// <summary>
        /// Speed of the time remaining loading bar.
        /// </summary>
        [SerializeField]
        private float _speed = 0.2f;
        /// <summary>
        /// Real password to get access.
        /// </summary>
        [SerializeField]
        private string _realPassword;
        /// <summary>
        /// Password entered by the player.
        /// </summary>
        public string enteredPassword;
        /// <summary>
        /// The combination of particles randomly generated rewritten as a string.
        /// </summary>
        public List<string> realParticles = new List<string>();
        /// <summary>
        /// The particles entered by the player.
        /// </summary>
        public List<Particle> _enteredParticles = new List<Particle>();
        [SerializeField]
        /// <summary>
        /// The buttons to enter digits for the password.
        /// </summary>
        private Button[] _digitButtons,
        /// <summary>
        /// The buttons to enter particles.
        /// </summary>
                         _particleButtons;
        /// <summary>
        /// String displayed depending on the particles combination entered.
        /// </summary>
        public string[] result;
        //TO DO: those should be accessed by the synchronizer
        public Text partic, partic2;
        /// <summary>
        /// Number of ongoing reactions.
        /// </summary>
        [SerializeField]
        private int _numberChosenReaction = 4;
        /// <summary>
        /// The ongoing reactions.
        /// </summary>
        [SerializeField]
        public List<Reaction> _chosenReactions = new List<Reaction>();
        /// <summary>
        /// The reaction to idetify.
        /// </summary>
        [SerializeField]
        public Reaction _realReaction;
        /// <summary>
        /// The particles produced by the ongoing reactions.
        /// </summary>
        [SerializeField]
        public List<Particle> reactionExits = new List<Particle>();
        private bool isTouched = false;
        //TODO: not needed?
        public Particle particleToEnter;
        public int displayedDiagram = 0;
        public string particleErrorString;

        public void NextDiagram()
        {
            if (!isTouched)
            {
                isTouched = true;
                if (displayedDiagram < _allReactions.Length)
                {
                    displayedDiagram++;
                    _synchronizer.SynchronizeScreens("OtherDiagram");
                }
                StartCoroutine("WaitButton");
            }
        }

        public void PreviousDiagram()
        {
            if (!isTouched)
            {
                isTouched = true;
                Debug.Log("CALLED");
                if (displayedDiagram > 0)
                {
                    displayedDiagram--;

                    _synchronizer.SynchronizeScreens("OtherDiagram");
                }
                StartCoroutine("WaitButton");
            }
        }

        public void PreselectExits()
        {
            if (!isTouched)
            {
                isTouched = true;
                
                    _synchronizer.SynchronizeScreens("SelectExit");
                
                StartCoroutine("WaitButton");
            }
        }

        public void PreselectInteraction()
        {
            if (!isTouched)
            {
                isTouched = true;

                _synchronizer.SynchronizeScreens("SelectInteraction");

                StartCoroutine("WaitButton");
            }
        }

        IEnumerator WaitButton()
        {
            yield return new WaitForSeconds(1f);
            isTouched = false;
        }

        /// <summary>
        /// Selects the ongoing particle reactions for this game.
        /// </summary>
        private void ReactionsCombination()
        {
            try
            {
                _allReactions = Resources.LoadAll(_pathReaction, typeof(Reaction)).Cast<Reaction>().ToArray();

                List<Reaction> fundamentals = new List<Reaction>();

                foreach (Reaction reaction in _allReactions)
                {
                    if (reaction.fundamental)
                    {
                        fundamentals.Add(reaction);
                    }
                }

                for (int i = 0; i < _numberChosenReaction; i++)
                {
                    _chosenReactions.Add(fundamentals[UnityEngine.Random.Range(0, fundamentals.Count)]);
                }

                _realReaction = _chosenReactions[UnityEngine.Random.Range(0, _chosenReactions.Count)];

            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        /// <summary>
        /// Lists the particles produced by the ongoing reactions.
        /// </summary>
        /// <returns>The list of produced particles.</returns>
        public List<Particle> ParticlesCombination()
        {

            try
            {
                _allParticles = Resources.LoadAll(_path, typeof(Particle)).Cast<Particle>().ToArray();
                
                ReactionsCombination();
                

                foreach (Reaction reaction in _chosenReactions)
                {
                    string[] particlesStrings = reaction.exits.ToString().Split('_');

                    for(int i = 0; i<particlesStrings.Length; i++)
                    {
                        foreach(Particle particle in _allParticles)
                        {
                            if(particle.symbol == particlesStrings[i])
                            {
                                reactionExits.Add(particle);
                                realParticles.Add(particle.symbol);
                            }
                        }
                    }
                }
                
                GenerateParticleString();
               
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            return reactionExits;
        }

        /// <summary>
        /// Converts the produced particles into a string.
        /// </summary
        private void GenerateParticleString()
        {
                string type = "";
            
            for (int i = 0; i < realParticles.Count; i++)
                {
                    type += realParticles[i];
                }
            
            partic2.text = type;
            
            _synchronizer.SynchronizeScreens("ParticleCorrect");
            
        }

        /// <summary>
        /// Fake delay that can never be attained.
        /// </summary>
        /// <returns>null</returns>
        IEnumerator FakeLoading()
        {
            float i = 0f;
            while (_slider.value <= 0.75f)
            {
                i += Time.deltaTime * _speed;
                _slider.value = Mathf.Sqrt(i);
                yield return null;
            }
        }

        /// <summary>
        /// Clears the particles combination entered.
        /// </summary>
        public void ClearParticles()
        {
           
                _enteredParticles.Clear();
            
            _synchronizer.SynchronizeScreens("ClearParticles");
        }

        /// <summary>
        /// Submits the particles combination entered.
        /// </summary>
        public void SubmitParticles()
        {
            if(_enteredParticles.Count == reactionExits.Count)
            {
                bool symbols = true;

               for(int i = 0; i<reactionExits.Count;i++)
                {
                    if(_enteredParticles[i].symbol != reactionExits[i].symbol)
                    {
                        Debug.Log("pas meme symbol");
                        symbols = false;
                        particleErrorString = "";
                        break;
                    }
                }
                if (symbols)
                {
                    for (int i = 0; i < reactionExits.Count; i++)
                    {
                        if (_enteredParticles[i].negative != reactionExits[i].negative)
                        {
                            Debug.Log("pas meme charge");
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("pas meme longueur");
            }
        }

        /// <summary>
        /// Adds a particle to the combination.
        /// </summary>
        /// <param name="particleButton">The particle to add.</param>
        public void EnteringParticle(string particleButton)
        {
            if (!isTouched && _enteredParticles.Count < reactionExits.Count)
            {
                isTouched = true;

                foreach(Particle particle in reactionExits)
                {
                    if(particle.symbol == particleButton)
                    {
                        _enteredParticles.Add(particle);
                        break;
                    }
                }

                _synchronizer.SynchronizeScreens("EnteringParticle");
                StartCoroutine("WaitButton");
            }
                
        }

        /// <summary>
        /// Adds a number to the password.
        /// </summary>
        /// <param name="number">The number to add.</param>
        public void EnteringDigit(int number)
        {
            if (!isTouched)
            {
                isTouched = true;
                if (enteredPassword.Length < _realPassword.Length)
                {
                    enteredPassword += number.ToString();
                    _synchronizer.SynchronizeScreens("EnteringDigit");

                    if (enteredPassword.Length == _realPassword.Length && enteredPassword == _realPassword)
                    {
                        _synchronizer.SynchronizeScreens("PasswordCorrect");
                    }
                    else if (enteredPassword.Length == _realPassword.Length && enteredPassword != _realPassword)
                    {
                        _synchronizer.SynchronizeScreens("PasswordInCorrect");
                        enteredPassword = "";
                    }
                }
                StartCoroutine("WaitButton");
            }

        }

        /// <summary>
        /// Displays particle selection panel after the correct password have been entered.
        /// </summary>
        public void AccessGranted()
        {
            _b1C5Left.SetActive(false);
            _b1C6Left.SetActive(true);
        }

        /// <summary>
        /// Displays start panel after the splash screen has finished loading.
        /// </summary>
        public void WaitingConfirmation()
        {
            _b1C2.SetActive(true);
            _panel.SetActive(false);
        }

        /// <summary>
        /// Displays password panel adter override button has been clicked.
        /// </summary>
        public void OverrideButtonClicked()
        {
            _synchronizer.SynchronizeScreens("OverrideButtonClicked");
            _b1C5Left.SetActive(true);
            _b1C4Left.SetActive(false);
        }

        /// <summary>
        /// Displays override panel after start button has been clicked.
        /// </summary>
        public void StartButtonClicked()
        {
            _synchronizer.SynchronizeScreens("StartButtonClicked");
            _b1C4.SetActive(true);
            _b1C2.SetActive(false);
            StartCoroutine("FakeLoading");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is correctly resolved.
        /// </summary>
        public override void OnResolved()
        {
            Debug.Log(name + "Resolved");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is failed.
        /// </summary>
        public override void OnFailed()
        {
            Debug.Log(name + "Failed");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        public override void OnActivated()
        {
            Debug.Log(name + "Activated");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is paused.
        /// </summary>
        public override void OnPause()
        {
            Debug.Log(name + "Paused");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is unpaused.
        /// </summary>
        public override void OnUnpause()
        {
            Debug.Log(name + "Unpaused");
        }
    }
}
