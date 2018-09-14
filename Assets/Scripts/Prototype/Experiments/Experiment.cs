using System.Collections;
using UnityEngine;

/// <summary>
/// Constructor for Experiment scriptable object.
/// </summary>
[CreateAssetMenu(fileName= "New Experiment", menuName = "Experiment/New Experiment", order = 1)]
public class Experiment : ScriptableObject 
{
	public string type;                 //What kind of gameplay
	public string audience;             //For scientists or for all
	public string difficulty;           //Level of difficulty
	public string subject;              //What kind of content
	public int placeholdersWallTop;     //Number of placeholders required on the top part of wall
	public int placeholdersWallTablet;    //Number of placeholders required on the tablet part of wall
    public int placeholdersWallBottom;    //Number of placeholders required on the bottom part of wall
    public GameObject prefab;           //Prefab that will be placed on the placeholders

}
