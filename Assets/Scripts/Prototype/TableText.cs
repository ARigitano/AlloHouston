using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableText : MonoBehaviour 
{
	private GameObject _gameManager;

	// Use this for initialization
	void Start () 
	{
		_gameManager = GameObject.Find ("GameManager");
		_gameManager.GetComponent<GameManager> ()._tableText = gameObject.GetComponent<TextMesh> ();
	}
}
