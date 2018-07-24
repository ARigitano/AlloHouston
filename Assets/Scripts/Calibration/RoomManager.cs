/// <summary>
/// Mananages all the experiments selected for the session.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace VRCalibrationTool
{
	public class RoomManager : MonoBehaviour 
	{
		public GameObject _room; 								//The room chosen for the session
		public GameObject[] _placeholdersRoom;					//The placeholders offered by the room
		public int _placeholdersRoomNeeded;						//Number of placeholders needed in the room for this session
		public GameObject _table; 								//The table chosen for the session
		public GameObject[] _placeholdersTable;					//The placeholders offered by the table
		public int _placeholdersTableNeeded;					//Number of placeholders needed at the table for this session
		//public string[] _module;								//The experiments chosen for the session
		public Experiment[] _experiments;						//Experiments available
		[SerializeField] private GameObject _placeholderMenu;	
		//public GameObject _buttonPrefab;						//Prefab for an experiement selection button
		//public GameObject _panelToAttachButtonsTo;				//Panel to attach experiment selection button to
		public Text _numberPlaceholdersText;					//Shows the number of placeholders still available
		public Text _numberTablePlaceholdersText;				//Shows the number of placeholders still available
		public int _wallCounter;								//Counter for number of wall placeholders available
		public int _tableCounter;								//Counter for number of table placeholders available
		public int _experimentsCounter;							//Counter for number of  experiments added
		public List<int> _possibleWallTop;						//List of numbers to organize wall blocs randomwly
		public List<int> _possibleTable;						//List of numbers to organize table blocs randomwly
		[SerializeField]private List<int> _finishedListWall;    //Ordered list of numbers to organize wall blocs randomwly
        [SerializeField]private List<int> _finishedListTable;   //Ordered list of numbers to organize table blocs randomwly
        public int counterPosition = 0;

        /// <summary>
        /// Randomly generates a list of unique numbers
        /// </summary>
        /// <param name="maxNumbers">Number of numbers to generate</param>
        /// <returns>The list of randomly generated unique numbers</returns>
		public List<int> GenerateRandomList(int maxNumbers)
        {
			List<int> uniqueNumbers = new List<int> (); //List of unique numbers ranked from smallest to biggest
			List<int> finishedList = new List<int> ();  //List of unique numbers randomly ranked

			for (int i = 0; i < maxNumbers; i++)
            {
				uniqueNumbers.Add (i);
			}

			for (int i = 0; i < maxNumbers; i++)
            {
				int ranNum = uniqueNumbers [Random.Range (0, uniqueNumbers.Count)];
				finishedList.Add (ranNum);
				uniqueNumbers.Remove (ranNum);
			}

			return finishedList;
		}

		/// <summary>
		/// Creates an experiment selection button
		/// </summary>
		/// <param name="name">Name.</param>
		/*public void CreateButton(string name) 
		{
			GameObject button = (GameObject)Instantiate(_buttonPrefab);
			button.transform.SetParent(_panelToAttachButtonsTo.transform);
			button.GetComponent<Button>().onClick.AddListener(OnClick);
			button.transform.GetChild(0).GetComponent<Text>().text = name;
			GameObject buttonModule = button.transform.GetChild (1).gameObject;
			if (buttonModule.GetComponent<ModuleButton> () != null) {
				buttonModule.GetComponent<ModuleButton> ()._placeholder = GameObject.Find (name);
			}
		}*/

		public void calibrateExperiments() 
		{
			//Checking if there are enough placeholders to meet the demand
			bool isRoomOk = false;

			if (_placeholdersRoom.Length >= _placeholdersRoomNeeded)
				isRoomOk = true;

			bool isTableOK = false;

			if (_placeholdersTable.Length >= _placeholdersTableNeeded)
				isTableOK = true;

			Debug.Log ("Room: " + isRoomOk + " Table: " + isTableOK);

			if (isRoomOk) 
			{

				Debug.Log ("Number of placeholders wall offered: "+_placeholdersRoom.Length+". Number of placeholders wall asked: "+_placeholdersRoomNeeded);
				Debug.Log ("Number of placeholders table offered: "+_placeholdersTable.Length+". Number of placeholders table asked: "+_placeholdersTableNeeded);

				_finishedListWall = GenerateRandomList (_placeholdersRoom.Length);
				_finishedListTable = GenerateRandomList (_placeholdersRoom.Length);



				ViveControllerManager ViveControllerManager = GameObject.Find ("ViveManager").GetComponent<ViveControllerManager> ();


				//Instantiating and placing expperiments
				foreach (Experiment experiment in _experiments) 
				{
                    GameObject instantiatedExperimentGameObject = (GameObject) Instantiate (experiment.prefab);
                    Experimentation instantiatedExperiment = (Experimentation)instantiatedExperimentGameObject.GetComponent<Experimentation>();

                    //instantiatedExperiment.GetComponent<ColorXP> ()._expNumber.text = "A" + _finishedListWall [counterPosition];
                    instantiatedExperiment._expNumber.text = "A" + _finishedListWall[counterPosition];


                    if (instantiatedExperiment.tag == "Wall top") 
						{
							instantiatedExperiment.transform.parent = _placeholdersRoom [counterPosition].transform;

							Debug.Log (instantiatedExperiment.name);

							instantiatedExperiment.transform.position = _placeholdersRoom [counterPosition].transform.position;
							instantiatedExperiment.transform.localRotation = Quaternion.identity;
							//instantiatedExperiment.transform.localScale = new Vector3(1f, 1f, 1f);

							//instantiatedExperiment.transform.parent = _placeholdersRoom [counterPosition].transform.parent;
							//Destroy (_placeholdersRoom [counterPosition].gameObject);
							counterPosition++;




							/*foreach(Transform child2 in _placeholdersRoom[counterPosition].transform) {
								
								if (child2.tag == "PositionTag") {
									Debug.Log ("Instantiating Positiong Tag");
									Debug.Log (child2.name);

									child2.gameObject.AddComponent<PositionTag> ();
									child2.GetComponent<PositionTag> ().positionTagIndex = counterPosition;

									Debug.Log ("Index: " + counterPosition);

									PositionTag pos = (PositionTag) ViveControllerManager.CreatePositionTag ();
									pos.transform.position = child2.position;
									ViveControllerManager._PositionTags [counterPosition].transform.position = 
									counterPosition++;
								}



								child.GetComponent<VirtualObject> ().Calibrate (ViveControllerManager._PositionTags);



								ViveControllerManager._PositionTags [0] = null;
								ViveControllerManager._PositionTags [1] = null;
								ViveControllerManager._PositionTags [2] = null;
								Debug.Log ("YOOOOHIII");

							}
							counterPosition = 0;*/
						} 


					



					/*foreach(Transform child in instantiatedExperiment.transform) 
					{
						
						if (child.tag == "Table") 
						{

						}
					}*/

				}
			} 
			else 
			{
				//There are not enough placeholders
				Debug.Log ("Experiments initialization failed.");
				Debug.Log ("Number of placeholders wall offered: "+_placeholdersRoom.Length+". Number of placeholders wall asked: "+_placeholdersRoomNeeded);
				Debug.Log ("Number of placeholders table offered: "+_placeholdersTable.Length+". Number of placeholders table asked: "+_placeholdersTableNeeded);
			}
		}
			
		// Update is called once per frame
		void Update () 
		{
            
            if (_room != null) 
			{
				//j = placeholders.Length;
				_numberPlaceholdersText.text = "Wall Top free: " + _wallCounter;
				_numberTablePlaceholdersText.text = "Table free: " + _tableCounter;
			}
		}
	}
}
