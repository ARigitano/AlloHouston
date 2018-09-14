using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Victory or failure text displayed on the table
/// </summary>
public class TableText : MonoBehaviour 
{
	private GameObject _gameManager;

	// Use this for initialization
	void Start () 
	{
		_gameManager = GameObject.Find ("GameManager");
		_gameManager.GetComponent<GameManager> ()._tableText = gameObject.GetComponent<TextMeshPro> ();
	}
}
