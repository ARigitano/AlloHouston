using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseManager : MonoBehaviour {
	public static MouseManager instance;

	[SerializeField]
	private PositionTag _mousePositionTag;

	public static int positionTagIndex = 0;

	private List<PositionTag> _mousePositionTagList = new List<PositionTag>();

	public PositionTag[] mousePositionTags {
		get {
			return _mousePositionTagList.OrderBy (x => x.positionTagIndex).ToArray();
		}
	}

	void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);
	}

	/// <summary>
	/// Creates a position tag at the given position
	/// </summary>
	/// <param name="position">Position.</param>
	public void CreatePositionTag(Vector3 position)
	{
		Debug.Log (position);
		var mpt = GameObject.Instantiate (_mousePositionTag, position, Quaternion.identity, this.transform);
		mpt.positionTagIndex = positionTagIndex;
		positionTagIndex++;
		_mousePositionTagList.Add (mpt);
	}

	public void ResetPositionTags()
	{
		foreach (var mousePositionTag in _mousePositionTagList) {
			Destroy (mousePositionTag.gameObject);
		}
		_mousePositionTagList.Clear ();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) {
			var mousePosition = Input.mousePosition;
			mousePosition.z = 10.0f;
			CreatePositionTag (Camera.main.ScreenToWorldPoint (mousePosition));
		}
	}
}
