using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;

public class ColorButon : MonoBehaviour {

	[SerializeField] private ColorXP _colorXP;
    public int index;
    private bool _fixed = false;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other) {
		if (other.tag == "ViveController" && !_fixed) {
            
            _colorXP.Resolved(index);
            _gameManager.incidentsFixed++;
            _gameManager.EndGame();
            _fixed = true;
			
		}
	}
}
