using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;

public class ColorXP : MonoBehaviour {

	[SerializeField] private GameObject _screen;
	public Material[] _screenMaterials;
	public TextMesh _expNumber;
	private RoomManager _roomManager;
    private Room table;
    public GameObject textPrefab;
    public GameObject panelToAttachTextTo;
    public int error;
    private GameObject text;


    // Use this for initialization
    void Start () {
		/*_roomManager = GameObject.Find ("RoomManager").GetComponent<RoomManager> ();
		_expNumber.text = "A" + _roomManager.counterPosition;*/

        table = GameObject.FindGameObjectWithTag("Table").GetComponent<Room>();

        if (table != null && table.canvas)
        {
            panelToAttachTextTo = table.canvas;
            text = (GameObject)Instantiate(textPrefab);
            text.transform.SetParent(panelToAttachTextTo.transform);
            text.transform.localRotation = Quaternion.identity;
            text.transform.position = panelToAttachTextTo.transform.position;

            error = Random.Range(0, 3);

            text.transform.GetComponent<Text>().text = _expNumber.text +": Error "+error;

        }


    }

    public void Resolved(int index)
    {
        if(index == error)
        {
            Debug.Log("Experiment solved");
            text.transform.GetComponent<Text>().text = _expNumber.text + ": Cleared";
        } else
        {
            Debug.Log("Experiment failed");
            text.transform.GetComponent<Text>().text = _expNumber.text + ": Failed";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeScreenColor(int counter) {
		_screen.GetComponent<MeshRenderer> ().material = _screenMaterials [counter];
	}
}
