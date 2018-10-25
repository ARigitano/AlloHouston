using System.Collections;
using UnityEngine;

/// <summary>
/// Constructor for XpDifficulty scriptable object.
/// </summary>
[CreateAssetMenu(fileName = "New XpDifficulty", menuName = "Experience/New XpDifficulty", order = 2)]
public class XpDifficulty : ScriptableObject
{
    public enum AudienceType
    {
      Casual,
      Scientific
    }

    public enum DifficultyType
    {
        Easy,
        Medium,
        Hard
    }

    public string name;  //Name of the experiment

    public AudienceType audienceType;

    public DifficultyType difficultyType;
    [Tooltip("Bonjour")]
    public WallTopZonePrefab wallTopZonePrefab;

    public WallBottomZonePrefab wallBottomZonePrefab;

    public HologramZonePrefab hologramZonePrefab;

    public CornerZonePrefab cornerZonePrefab;

    public DoorZonePrefab doorZonePrefab;





}
