using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenControl : MonoBehaviour {

    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private float _speed = 0.2f;
    [SerializeField]
    private Text _percentage;
    [SerializeField]
    private GameObject _nextPanel, _thisPanel, _nexttabletPanel, _thisTabletPanel;

    IEnumerator Loading()
    {
        while(_slider.value <= 1f)
        {
            _slider.value += Time.deltaTime * _speed;
            _percentage.text = Mathf.Round(_slider.value*100) + "%";
            if(_slider.value >= 0.9f)
            {
                _slider.value = 1f;
                _nextPanel.SetActive(true);
                _nexttabletPanel.SetActive(true);
                _thisTabletPanel.SetActive(false);
                _thisPanel.SetActive(false);
            }
            yield return null;
        }
    }

	// Use this for initialization
	void Start () {
        StartCoroutine("Loading");
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
