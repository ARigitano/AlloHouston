using System.Collections;
using UnityEngine;

/// <summary>
/// Constructor for XpGroup scriptable object.
/// </summary>
[CreateAssetMenu(fileName = "New XpGroup", menuName = "Experience/New XpGroup", order = 1)]
public class XpGroup : ScriptableObject
{
    public string _name;                //Name of the experiment
    public string _description;         //A description of the experiment
    public string _type;                //What kind of gameplay
    public string _subject;             //What kind of content

    public string audience;             //For scientists or for all
    public string difficulty;           //Level of difficulty

    public int placeholdersWallTop;     //Number of placeholders required on the top part of wall
    public int placeholdersWallTablet;    //Number of placeholders required on the tablet part of wall
    public int placeholdersWallBottom;    //Number of placeholders required on the bottom part of wall
    public GameObject prefab;           //Prefab that will be placed on the placeholders
}