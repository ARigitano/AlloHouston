using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace VRCalibrationTool
{
    /// <summary>
    /// Mananages all the experiments selected for the session.
    /// </summary>
    public class RoomManager : MonoBehaviour 
	{
		public GameObject _room; 								//The room chosen for the session
		public GameObject[] _placeholdersRoom;                  //The placeholders offered by the room
        public GameObject[] _placeholdersRoomBottom;
        public GameObject[] _decorationRoom;                    //The decoration placeholders offered by the room
        public GameObject[] _decorationRoomBottom;
        public int _placeholdersRoomNeeded;                     //Number of placeholders needed in the room for this session
        public int _placeholdersRoomNeededBottom;
        public GameObject _table;                               //The table chosen for the session
        public AIScreen _aiScreen;
        public int _placeholdersTableNeeded;					//Number of placeholders needed at the table for this session
		public Experiment[] _experiments;						//Experiments available
		public Text _numberPlaceholdersText;                    //Shows the number of placeholders still available
        public Text _numberPlaceholdersBottomText;
        public int _wallCounter;                                //Counter for number of wall placeholders available
        public int _wallBottomCounter;
        public int _experimentsCounter;							//Counter for number of  experiments added
		public List<int> _possibleWallTop;						//List of numbers to organize wall blocs randomwly
		[SerializeField]private List<int> _finishedListWall;    //Ordered list of numbers to organize wall blocs randomwly
        public int _counterPosition = 0;                        //Counter for the experiments positionning
        [SerializeField] private GameObject[] _wallFurniture;   //Decorations that will be place on unused placeholders;

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
        /// Randomly positions the experiments of the room after their number and type have been selected.
        /// </summary>
		public void CalibrateExperiments() 
		{
			//Checking if there are enough placeholders to meet the demand
			bool isRoomOk = false;  //Are there enough placeholders in the room?
            bool isBottomOk = false;

			if (_placeholdersRoom.Length >= _placeholdersRoomNeeded)
				isRoomOk = true;

            if (_placeholdersRoomBottom.Length >= _placeholdersRoomNeededBottom)
                isBottomOk = true;

            //There are enough placeholders in the room
            if (isRoomOk && isBottomOk) 
			{
                Debug.Log("Experiments initialization succeeded.");
                Debug.Log ("Number of placeholders wall offered: "+_placeholdersRoom.Length+". Number of placeholders wall asked: "+_placeholdersRoomNeeded);
                Debug.Log("Number of placeholders bottom offered: " + _placeholdersRoomBottom.Length + ". Number of placeholders bottom asked: " + _placeholdersRoomNeededBottom);

                _finishedListWall = GenerateRandomList (_placeholdersRoom.Length/*+ _placeholdersRoomBottom.Length*/);
				ViveControllerManager ViveControllerManager = GameObject.Find ("ViveManager").GetComponent<ViveControllerManager> ();

				//Instantiating and placing expperiments
				foreach (Experiment experiment in _experiments) 
				{
                    GameObject instantiatedExperimentGameObject = (GameObject) Instantiate (experiment.prefab);
                    Experimentation instantiatedExperiment = (Experimentation)instantiatedExperimentGameObject.GetComponent<Experimentation>();

                    instantiatedExperiment._expNumber.text = "A" + _finishedListWall[_counterPosition];

                    if (instantiatedExperiment.tag == "Wall top")
                    {
                        Debug.Log(_finishedListWall[_counterPosition]);
                        if(_placeholdersRoom[_finishedListWall[_counterPosition]] != null)
                        instantiatedExperiment.transform.parent = _placeholdersRoom[_finishedListWall[_counterPosition]].transform;
                        
                        instantiatedExperiment.transform.position = _placeholdersRoom[_finishedListWall[_counterPosition]].transform.position;
                        instantiatedExperiment.transform.localScale = _room.transform.localScale;
                        instantiatedExperiment.transform.localRotation = Quaternion.identity;
                        _counterPosition++;
                    } else if (instantiatedExperiment.tag == "Wall bottom")
                        {
                            instantiatedExperiment.transform.parent = _placeholdersRoomBottom[_finishedListWall[_counterPosition]].transform;
                            instantiatedExperiment.transform.position = _placeholdersRoomBottom[_finishedListWall[_counterPosition]].transform.position;
                            instantiatedExperiment.transform.localScale = _room.transform.localScale;
                            instantiatedExperiment.transform.localRotation = Quaternion.identity;
                            _counterPosition++;
                        }
                }

                //Instantiating and placing decoration
                foreach(GameObject decoration in _decorationRoom)
                {
                    int randomNuber = Random.Range(0, 2);
                    GameObject instantiatedDecorationGameObject = (GameObject)Instantiate(_wallFurniture[randomNuber]);
                    instantiatedDecorationGameObject.transform.parent = decoration.transform;
                    instantiatedDecorationGameObject.transform.position = decoration.transform.position;
                    instantiatedDecorationGameObject.transform.localScale = _room.transform.localScale;
                    instantiatedDecorationGameObject.transform.localRotation = Quaternion.identity;

                }
			} 
			else 
			{
				//There are not enough placeholders
				Debug.Log ("Experiments initialization failed.");
				Debug.Log ("Number of placeholders wall offered: "+_placeholdersRoom.Length+". Number of placeholders wall asked: "+_placeholdersRoomNeeded);
			}
		}
			
		// Update is called once per frame
		void Update () 
		{
            if (_room != null) 
			{
				_numberPlaceholdersText.text = "Wall Top free: " + _wallCounter;
                _numberPlaceholdersBottomText.text = "Wall Bottom free: " + _wallBottomCounter;
            }
		}
	}
}
