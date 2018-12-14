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
        private Reaction[] _allReactions;
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
        public List<string> _enteredParticles = new List<string>();
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
        private List<Reaction> _chosenReactions = new List<Reaction>();
        /// <summary>
        /// The reaction to idetify.
        /// </summary>
        [SerializeField]
        private Reaction _realReaction;
        /// <summary>
        /// The particles produced by the ongoing reactions.
        /// </summary>
        [SerializeField]
        public List<Particle> reactionExits = new List<Particle>();
        private bool isTouched = false;

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
        private List<Particle> ParticlesCombination()
        {

            try
            {
                _allParticles = Resources.LoadAll(_path, typeof(Particle)).Cast<Particle>().ToArray();

                ReactionsCombination();

                foreach(Reaction reaction in _chosenReactions)
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

                    GenerateParticleString();
                }
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
            for(int i = 0; i < _enteredParticles.Count; i++)
            {
                _enteredParticles[i] = "";
            }
            _synchronizer.SynchronizeScreens("EnteringParticle");
        }

        /// <summary>
        /// Submits the particles combination entered.
        /// </summary>
        public void SubmitParticles()
        {
            Debug.Log("fired");

            string particles = "";
            string realPart = "";

            for (int i = 0; i < _enteredParticles.Count; i++) 
            {
                particles+=_enteredParticles[i];
            }

            for (int i = 0; i < realParticles.Count; i++) {
                realPart += realParticles[i];
            }

            if (particles == realPart)
            {
                //_synchronizer.SynchronizeScreens("ParticleCorrect");
                //meme longueur
                partic.text = "meme string";
                Debug.Log("meme string");
            }
            else if (particles != realPart)
            {
                bool isSign = false;
                int particleCounter = 0;

                for (int i = 0; i < _enteredParticles.Count; i++) {
                    if(_enteredParticles[i] != "")
                        particleCounter++;
                }

                if (particleCounter != realParticles.Count)
                {
                    //pas meme longueur
                    Debug.Log("pas meme longueur");
                    partic.text = "pas meme longueur";
                }
                else 
                {
                    for (int i = 0; i < realParticles.Count; i++)
                    {
                        string particle = _enteredParticles[i];
                        char sign = particle[0];

                        string realParticle = realParticles[i];
                        char realSign = realParticle[0];

                        if (sign != realSign)
                        {
                            //pas meme symbol
                            Debug.Log("pas meme symbol");
                            isSign = true;
                            partic.text = "pas meme symbol";
                            break;
                        }
                    }
                    if (!isSign)
                    {
                        for (int i = 0; i < realParticles.Count; i++)
                        {
                            char sign = '\0';
                            char realSign = '\0';
                            if (_enteredParticles[i].Length == 2)
                            {
                                string particle = _enteredParticles[i];
                                sign = particle[1];
                            }

                            if (realParticles[i].Length == 2)
                            {
                                string realParticle = realParticles[i];
                                realSign = realParticle[1];
                            }

                            if (sign != realSign)
                            {
                                //pas meme charge
                                Debug.Log("pas meme charge");
                                partic.text = "pas meme charge";
                                break;
                            }
                        }
                    }
                }
                  
                _synchronizer.SynchronizeScreens("ParticleInCorrect");
                enteredPassword = "";
            }
        }

        /// <summary>
        /// Adds a particle to the combination.
        /// </summary>
        /// <param name="particle">The particle to add.</param>
        public void EnteringParticle(string particle)
        {
            if (!isTouched)
            {
                isTouched = true;
                if (_enteredParticles.Count < 23)
                {
                _enteredParticles.Add(particle);
                _synchronizer.SynchronizeScreens("EnteringParticle");
            }
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

        // Use this for initialization
        void Start()
        {
            reactionExits = ParticlesCombination();
        }
    }
}
