using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesy : MonoBehaviour {


	private int maxNumbers = 3;
	[SerializeField]private List<int> uniqueNumbers;
	[SerializeField]private List<int> finishedList;

	public GameObject room;
	public GameObject bloc;
	public GameObject mur1, mur2, mur3;
	public GameObject lego1, lego2;
	public GameObject inst1, inst2;


	public void GenerateRandomList() {
		for (int i = 0; i < maxNumbers; i++) {
			uniqueNumbers.Add (i);
		}

		for (int i = 0; i < maxNumbers; i++) {
			int ranNum = uniqueNumbers [Random.Range (0, uniqueNumbers.Count)];
			finishedList.Add (ranNum);
			uniqueNumbers.Remove (ranNum);
		}
	}


	// Use this for initialization
	void Start () {

		uniqueNumbers = new List<int> ();
		finishedList = new List<int> ();
		GenerateRandomList ();


		inst1 = (GameObject) Instantiate (room);
		mur1 = GameObject.Find("ColorXP (1)");
		mur2 = GameObject.Find("ColorXP (2)");

		inst2 = (GameObject) Instantiate (bloc);
		lego1 = GameObject.Find("ColorXP_Wall");
		lego2 = GameObject.Find("ColorXp_Table");

		//lego1.transform.parent = mur1.transform;
		//lego1.transform.SetParent(mur1.transform);
		lego1.transform.position = mur1.transform.position;
		lego1.transform.rotation = mur1.transform.rotation;
		lego1.transform.localScale = mur1.transform.lossyScale;
		lego2.transform.parent = mur2.transform;
		lego2.transform.position = mur2.transform.position;
		lego2.transform.rotation = mur2.transform.rotation;
		lego2.transform.localScale = mur2.transform.lossyScale;

		
	}

}
