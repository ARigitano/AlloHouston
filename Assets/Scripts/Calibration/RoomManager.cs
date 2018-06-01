/// <summary>
/// Mananages all the experiments selected for the session.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace VRCalibrationTool
{
	public class RoomManager : MonoBehaviour {

		public GameObject room; 								//The room chosen for the session
		public GameObject[] placeholdersRoom;					//The placeholders offered by the room
		public int placeholdersRoomNeeded;						//Number of placeholders needed in the room for this session
		public GameObject table; 								//The table chosen for the session
		public GameObject[] placeholdersTable;					//The placeholders offered by the table
		public int placeholdersTableNeeded;						//Number of placeholders needed at the table for this session
		public string[] module;									//The experiments chosen for the session
		public Experiment[] experiments;						//Experiments available
		[SerializeField] private GameObject _placeholderMenu;	
		public GameObject buttonPrefab;							//Prefab for an experiement selection button
		public GameObject panelToAttachButtonsTo;				//Panel to attach experiment selection button to
		public Text numberPlaceholdersText;						//Shows the number of placeholders still available
		public int j;											//Counter for number of placeholders available
		public int i;											//Counter for number of  experiments added


		/// <summary>
		/// Creates an experiment selection button
		/// </summary>
		/// <param name="name">Name.</param>
		public void CreateButton(string name) {
			GameObject button = (GameObject)Instantiate(buttonPrefab);
			button.transform.SetParent(panelToAttachButtonsTo.transform);
			//button.GetComponent<Button>().onClick.AddListener(OnClick);
			button.transform.GetChild(0).GetComponent<Text>().text = name;
			GameObject buttonModule = button.transform.GetChild (1).gameObject;
			if (buttonModule.GetComponent<ModuleButton> () != null) {
				buttonModule.GetComponent<ModuleButton> ()._placeholder = GameObject.Find (name);
			}
		}

		public void calibrateExperiments() {
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

			if (placeholdersRoom.Length <= placeholdersRoomNeeded)
				isRoomOk = true;

			bool isTableOK = false;

			if (placeholdersTable.Length <= placeholdersTableNeeded)
				isTableOK = true;

			if (isRoomOk && isTableOK) {

				//Instantiating and placing expperiments
				foreach (Experiment experiment in experiments) {
					GameObject instantiatedExperiment = (GameObject) Instantiate (experiment.prefab);
					int wallCounter = 0;
					int tableCounter = 0;

					foreach(Transform child in instantiatedExperiment.transform) {
						if (child.tag == "Wall top") {
							List<int> possibleWallTop = null;
				
							for (int i = 0; i < placeholdersRoom.Length; i++) {
								possibleWallTop.Add(i);
							}

							int randomWallTop = Random.Range (0, possibleWallTop.Count);
							int randUse = possibleWallTop [randomWallTop];
							possibleWallTop.RemoveAt (randomWallTop);

							child.position = placeholdersRoom [randUse].transform.position;
							wallCounter++;
						} else if (child.tag == "Table") {
							List<int> possibleTable = null;

							for (int i = 0; i < placeholdersTable.Length; i++) {
								possibleTable.Add(i);
							}

							int randomTable = Random.Range (0, possibleTable.Count);
							int randUsed = possibleTable [randomTable];
							possibleTable.RemoveAt (randomTable);

							child.position = placeholdersTable [randUsed].transform.position;
							tableCounter++;
						}
					}
				}

			} else {
				//There are not enough placeholders
				Debug.Log ("Experiments initialization failed.");
				Debug.Log ("Number of placeholders wall offered: "+placeholdersRoom+". Number of placeholders wall asked: "+placeholdersRoomNeeded);
				Debug.Log ("Number of placeholders table offered: "+placeholdersTable+". Number of placeholders table asked: "+placeholdersTableNeeded);
			}



		}
			
		// Update is called once per frame
		void Update () {
			if (room != null) {
				//j = placeholders.Length;
				numberPlaceholdersText.text = "Placeholders free: " + j;
			}
		}
	}
}
