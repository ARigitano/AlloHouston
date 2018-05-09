using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int incidentsFixed = 0;
	[SerializeField] private TextMesh tableText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*public void StartCalibration() {

	}*/

	/*public void EndCalibration() {

	}*/

	/*public void StartGame() {

	}*/

	public void EndGame() {
		if (incidentsFixed == 3) {
			tableText.text = "Victory";
			Debug.Log ("End of game");
		}
	}
}
