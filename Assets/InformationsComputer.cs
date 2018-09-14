using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationsComputer : MonoBehaviour {

    [SerializeField] public Sprite[] _diagramms;
    public int _imageIndex = 0;
    [SerializeField] private Image _screen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _screen.overrideSprite = _diagramms[_imageIndex];
	}
}
