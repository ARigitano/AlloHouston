using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceholdersMenu : MonoBehaviour {

	public GameObject buttonPrefab;
	public GameObject panelToAttachButtonsTo;
	[SerializeField] private GameObject[] placeholders;

	// Use this for initialization
	void Start () {

		placeholders = GameObject.FindGameObjectsWithTag ("Placeholder");

		foreach (GameObject placeholder in placeholders) {
			//CreateButton (placeholder.name);
		}
		
	}

	void OnClick()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateButton(string name) {
		GameObject button = (GameObject)Instantiate(buttonPrefab);
		button.transform.SetParent(panelToAttachButtonsTo.transform);
		button.GetComponent<Button>().onClick.AddListener(OnClick);
		button.transform.GetChild(0).GetComponent<Text>().text = name;
		GameObject buttonModule = button.transform.GetChild (1).gameObject;
		if (buttonModule.GetComponent<ModuleButton> () != null) {
			buttonModule.GetComponent<ModuleButton> ()._placeholder = GameObject.Find (name);
		}
	}
}
