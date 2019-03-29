using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class TutorialHologram : XPHologramElement
    {
        //private List<GameObject> _irregularities = new List<GameObject>();
        private int _nbIrregukarities;
        [SerializeField]
        private TextMesh _uiNbIrregularities, _uiTimer;
        [SerializeField]
        private float _timer;

        // Start is called before the first frame update
        void Start()
        {
            foreach(GameObject irregularity in GameObject.FindGameObjectsWithTag("Irregularity"))
            {
                _nbIrregukarities++;
                //_irregularities.Add(irregularity);
            }

            //_nbIrregukarities = _irregularities.Count;
            _uiNbIrregularities.text = _nbIrregukarities.ToString();

            StartCoroutine("CountDown");
        }

        IEnumerator CountDown()
        {
            while(_timer > 0f)
            {
                yield return new WaitForSeconds(1f);
                _timer--;
                _uiTimer.text = _timer.ToString();
            }

            _uiNbIrregularities.text = "Fail";
        }

        public void UpdateNbIrregularities()
        {
            _nbIrregukarities--;
            _uiNbIrregularities.text = _nbIrregukarities.ToString();

            if(_nbIrregukarities == 0)
            {
                _uiNbIrregularities.text = "winrar";
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
