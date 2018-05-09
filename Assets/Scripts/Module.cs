using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour {

	[SerializeField] private GameObject _led;
	[SerializeField] private GameObject _gameManager;
	[SerializeField] private Material _ledMaterial;
	private bool _isFixed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "ViveController" && !_isFixed) {
			_led.GetComponent<MeshRenderer> ().material = _ledMaterial;
			_gameManager.GetComponent<GameManager> ().incidentsFixed++;
			_gameManager.GetComponent<GameManager> ().EndGame ();
			Debug.Log ("Incident resolved");
			_isFixed = true;
		}
	}
}
