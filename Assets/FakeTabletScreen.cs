using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience
{ 
    public class FakeTabletScreen : XPElement
    {
        [SerializeField]
        private FakeSynchronizer _synchronizer;
        [SerializeField]
        private GameObject _panel, _b1C2, _b1C4, _b1C4Left, _b1C5Left, _b1C6Left;
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private float _speed = 0.2f;
        [SerializeField]
        private string _realPassword;
        public string enteredPassword;
        public string[] realParticles;
        public string[] _enteredParticles;
        [SerializeField]
        private Button[] _digitButtons, _particleButtons;
        public string[] result;
        public Text partic, partic2;
        
        private void GenerateParticles()
        {
            string[] particleTypes = new string[] { "e", "μ", "q", "γ", "e-", "μ-", "q-", "v" };
            string type = "";

            for(int i = 0; i < realParticles.Length; i++)
            {
                realParticles[i] = particleTypes[Random.Range(0, particleTypes.Length)];
                type += realParticles[i];
            }

            partic2.text = type;
        }

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

        public void ClearParticles()
        {
            for(int i = 0; i < _enteredParticles.Length; i++)
            {
                _enteredParticles[i] = "";
            }
            _synchronizer.SynchronizeScreens("EnteringParticle");
        }

        public void SubmitParticles()
        {
            Debug.Log("fired");

            string particles = "";
            string realPart = "";

            for (int i = 0; i < _enteredParticles.Length; i++) 
            {
                particles+=_enteredParticles[i];
            }

            for (int i = 0; i < realParticles.Length; i++) {
                realPart += realParticles[i];
            }

            if (particles == realPart)
            {
                //_synchronizer.SynchronizeScreens("ParticleCorrect");
                //meme longueur
                partic.text = "meme longueur";
                Debug.Log("meme longueur");
            }
            else if (particles != realPart)
            {
                bool isSign = false;

                if (particles.Length != realPart.Length)
                {
                    //pas meme longueur
                    Debug.Log("pas meme longueur");
                    partic.text = "pas meme longueur";
                }
                else
                {
                    for (int i = 0; i < realParticles.Length; i++)
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
                        for (int i = 0; i < realParticles.Length; i++)
                        {
                            string particle = _enteredParticles[i];
                            char sign = particle[1];

                            string realParticle = realParticles[i];
                            char realSign = realParticle[1];

                            if (sign != null && realSign != null && sign != realSign)
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

        public void EnteringParticle(string particle)
        {
            for(int i = 0; i < _enteredParticles.Length; i++)
            {
                if(_enteredParticles[i] == "")
                {
                    _enteredParticles[i] = particle;
                    _synchronizer.SynchronizeScreens("EnteringParticle");
                    Debug.Log(particle);
                    break;
                }
            }
        }

        public void EnteringDigit(int number)
        {
            if(enteredPassword.Length < _realPassword.Length)
            {
                enteredPassword += number.ToString();
                _synchronizer.SynchronizeScreens("EnteringDigit");

                if (enteredPassword.Length == _realPassword.Length && enteredPassword == _realPassword)
                {
                    _synchronizer.SynchronizeScreens("PasswordCorrect");
                }
                else if(enteredPassword.Length == _realPassword.Length && enteredPassword != _realPassword)
                {
                    _synchronizer.SynchronizeScreens("PasswordInCorrect");
                    enteredPassword = "";
                }
            }
        }

        public void AccessGranted()
        {
            _b1C5Left.SetActive(false);
            _b1C6Left.SetActive(true);
        }

        public void WaitingConfirmation()
        {
            _b1C2.SetActive(true);
            _panel.SetActive(false);
        }

        public void OverrideButtonClicked()
        {
            _synchronizer.SynchronizeScreens("OverrideButtonClicked");
            _b1C5Left.SetActive(true);
            _b1C4Left.SetActive(false);
        }

        public void StartButtonClicked()
        {
            _synchronizer.SynchronizeScreens("StartButtonClicked");
            _b1C4.SetActive(true);
            _b1C2.SetActive(false);
            StartCoroutine("FakeLoading");
        }

        public override void OnResolved()
        {
            Debug.Log(name + "Resolved");
        }

        public override void OnFailed()
        {
            Debug.Log(name + "Failed");
        }

        public override void OnActivated()
        {
            Debug.Log(name + "Activated");
        }

        public override void OnPause()
        {
            Debug.Log(name + "Paused");
        }

        public override void OnUnpause()
        {
            Debug.Log(name + "Unpaused");
        }

        // Use this for initialization
        void Start()
        {
            GenerateParticles();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
