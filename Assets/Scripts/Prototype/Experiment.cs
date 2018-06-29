using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName= "ColorXP", menuName = "Experiment/ColorXP", order = 1)]
public class Experiment : ScriptableObject 
{
	public string type = "Color";
	public string audience = "Casual";
	public string difficulty = "Very easy";
	public string subject = "Logic";
	public int placeholdersWallTop = 1;
	public int plaholdersWallTablet = 0;
	public int plaholdersWallBottom = 0;
	public GameObject prefab;

}
