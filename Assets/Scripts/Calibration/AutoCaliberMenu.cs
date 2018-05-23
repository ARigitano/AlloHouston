using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace VRCalibrationTool
{
public class AutoCaliberMenu : MonoBehaviour {

		public GameObject buttonPrefab;
		public GameObject panelToAttachButtonsTo;
		public PositionTag[] PositionTags;

		void Start()
		{

			for (int i = 0; i < XMLManager.ins.itemDB.list.Count; i++) {
				CreateButton (XMLManager.ins.itemDB.list[i].type);
			}


		}
		void OnClick()
		{
			

			GameObject objectCalibrate = GameObject.Find (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);

			ViveControllerManager ViveControllerManager = GameObject.Find ("ViveManager").GetComponent<ViveControllerManager> ();

			ViveControllerManager.CreatePositionTag ();
			ViveControllerManager.CreatePositionTag ();
			ViveControllerManager.CreatePositionTag ();

			ViveControllerManager.PositionTags [0].transform.position = XMLManager.ins.itemDB.list[0].point1.Vector3;
			ViveControllerManager.PositionTags [1].transform.position = XMLManager.ins.itemDB.list[0].point2.Vector3;
			ViveControllerManager.PositionTags [2].transform.position = XMLManager.ins.itemDB.list[0].point3.Vector3;

			objectCalibrate.GetComponent<VirtualObject> ().Calibrate (ViveControllerManager.PositionTags);

			Debug.Log("Object calibrated: "+objectCalibrate.name);
		}

		private void CreateButton(string name) {
			GameObject button = (GameObject)Instantiate(buttonPrefab);
			button.transform.SetParent(panelToAttachButtonsTo.transform);
			button.GetComponent<Button>().onClick.AddListener(OnClick);
			button.transform.GetChild(0).GetComponent<Text>().text = name;
		}

	}
}
