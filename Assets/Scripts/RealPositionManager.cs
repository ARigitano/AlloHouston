using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RealPositionManager : MonoBehaviour {
	public static RealPositionManager instance;

	private List<PositionTag> _realPositionTagList = new List<PositionTag>();

	public PositionTag[] controllerPositionTags {
		get {
			return _realPositionTagList.OrderBy (x => x.positionTagIndex).ToArray();
		}
	}

	void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);
	}

	void Start()
	{
		_realPositionTagList = GetComponentsInChildren<PositionTag> ().ToList ();
	}

	public void ResetPositionTags()
	{
		foreach (var realPositionTag in _realPositionTagList) {
			realPositionTag.Reset ();
		}
	}
}
