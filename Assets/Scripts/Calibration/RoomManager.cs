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
				//voir combien de placeholders mur
				//voir besoin combien placeholcers mur
				//result mur
				//voir combien de placeholders table
				//voir besoin combien placeholders table
				//result table
				//si result mur et table ok
				//instantier experiences
				//repartir aleatoirement blocs murs
				//repartir aleatoirement blocs table
				//message ok
				//si result mur et table pas ok
				//message status mur
				//message status table


			//Checking if there are enough placeholders to meet the demand
			bool isRoomOk = false;

			if (_placeholdersRoom.Length >= _placeholdersRoomNeeded)
				isRoomOk = true;

			bool isTableOK = false;

			if (_placeholdersTable.Length >= _placeholdersTableNeeded)
				isTableOK = true;

			Debug.Log ("Room: " + isRoomOk + " Table: " + isTableOK);

			if (isRoomOk && isTableOK) 
			{

				Debug.Log ("Number of placeholders wall offered: "+_placeholdersRoom.Length+". Number of placeholders wall asked: "+_placeholdersRoomNeeded);
				Debug.Log ("Number of placeholders table offered: "+_placeholdersTable.Length+". Number of placeholders table asked: "+_placeholdersTableNeeded);

				//List<int> possibleWallTop = new List<int> (3);
				//List<int> possibleTable = new List<int> (placeholdersTableNeeded);
				int possibleWallCounter = 0;
				int possibleTableCounter = 0;

				//Instantiating and placing expperiments
				foreach (Experiment experiment in _experiments) 
				{
					GameObject instantiatedExperiment = (GameObject) Instantiate (experiment.prefab);

					Debug.Log ("wallcounter= " + possibleWallCounter);

					int wallCounter = 0;
					int tableCounter = 0;

					foreach(Transform child in instantiatedExperiment.transform) 
					{
						if (child.tag == "Wall top") 
						{
							//List<int> possibleWallTop = new List<int> (placeholdersRoomNeeded);
				

							_possibleWallTop.Add(possibleWallCounter);
							possibleWallCounter++;
							

							int randomWallTop = Random.Range (0, _possibleWallTop.Count);
							int randUse = _possibleWallTop [randomWallTop];
							//possibleWallTop.RemoveAt (randomWallTop);
							child.position = _placeholdersRoom [randUse].transform.position;
							child.rotation = _placeholdersRoom [randUse].transform.rotation;
							child.localScale = _placeholdersRoom [randUse].transform.localScale;
							Debug.Log(child.position);
							Debug.Log (_placeholdersRoom [randUse].transform.position);
							wallCounter++;
						} 
						else if (child.tag == "Table") 
						{
							//List<int> possibleTable = new List<int> (placeholdersTableNeeded);


							_possibleTable.Add(possibleWallCounter);
							possibleTableCounter++;

							int randomTable = Random.Range (0, _possibleTable.Count);
							int randUsed = _possibleTable [randomTable];
							//possibleTable.RemoveAt (randomTable);

							child.position = _placeholdersTable [randUsed].transform.position;
							tableCounter++;
						}
					}
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
