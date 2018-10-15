using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

namespace VRCalibrationTool
{
    /// <summary>
    /// Takes the coordonnates of the selected object stored in the XML to auto calibrate it.
    /// </summary>
    public class AutoCaliberMenu : MonoBehaviour 
	{
        [SerializeField]
        private GameObject _buttonPrefab = null;            //Prefab for the autocalibrate button	
        [SerializeField]
        private GameObject _panelToAttachButtonsTo = null;	//Panel to attach the autocalibrate button to

        private ViveControllerManager _viveControllerManager = null;

		private void Start()
		{
			for (int i = 0; i < XMLManager.instance.itemDB.list.Count; i++) 
			{
				CreateButton (XMLManager.instance.itemDB.list[i].type);
			}
            _viveControllerManager = GameObject.Find("ViveManager").GetComponent<ViveControllerManager>();
        }

		/// <summary>
		/// If button clicked, autocalibrates the selected object.
		/// </summary>
		private void OnClick()
		{
			string objectCalibrateName = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text;
		    VirtualObject objectCalibrate = (VirtualObject)Resources.Load (objectCalibrateName);
            
            ItemEntry item = XMLManager.instance.itemDB.list.FirstOrDefault(x => x.type == objectCalibrateName);
            if (item != null)
            {
                _viveControllerManager.CreatePositionTag(item.points.Length);
                for (int i = 0; i < item.points.Length; i++)
                {
                    _viveControllerManager._positionTags[i].transform.position = item.points[i].Vector3;
                }
                VirtualObject objectInstantiated = Instantiate(objectCalibrate);
                //objectInstantiated.GetComponent<VirtualObject> ().Calibrate (ViveControllerManager._PositionTags);
                _viveControllerManager.CalibrateVR(objectInstantiated);
                Debug.Log("Object calibrated: " + objectCalibrate.name);
            }
		}

		/// <summary>
		/// Creates an autocalibrate button.
		/// </summary>
		/// <param name="name">Name.</param>
		private void CreateButton(string name) 
		{
			GameObject button = (GameObject)Instantiate(_buttonPrefab);
			button.transform.SetParent(_panelToAttachButtonsTo.transform);
			button.GetComponent<Button>().onClick.AddListener(OnClick);
			button.transform.GetChild(0).GetComponent<Text>().text = name;
		}
	}
}
