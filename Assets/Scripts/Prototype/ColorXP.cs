using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorXP : MonoBehaviour {

	[SerializeField] private GameObject _screen;
	public Material[] _screenMaterials;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeScreenColor(int counter) {
		_screen.GetComponent<MeshRenderer> ().material = _screenMaterials [counter];
	}
}
