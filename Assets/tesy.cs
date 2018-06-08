using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesy : MonoBehaviour {

	public GameObject room;
	public GameObject bloc;
	public GameObject mur1, mur2, mur3;
	public GameObject lego1, lego2;
	public GameObject inst1, inst2;


	// Use this for initialization
	void Start () {

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
	
	// Update is called once per frame
	void Update () {
		
	}
}
