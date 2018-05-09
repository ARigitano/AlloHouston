using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace VRCalibrationTool
{
public class XMLManager : MonoBehaviour {

	public static XMLManager ins;

	void Awake() {
		ins = this;

		SaveItems ();
		//LoadItems();
	}

	public ItemDatabase itemDB;

	public void SaveItems() {

		XmlSerializer serializer = new XmlSerializer (typeof(ItemDatabase));
		FileStream stream = new FileStream (Application.dataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Create);
		serializer.Serialize (stream, itemDB);
		stream.Close();

	}

	public void LoadItems() {
		XmlSerializer serializer = new XmlSerializer (typeof(ItemDatabase));
		FileStream stream = new FileStream (Application.dataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Open);
		itemDB = serializer.Deserialize (stream) as ItemDatabase;
		stream.Close();
	}

}



[System.Serializable]
public class ItemEntry {
	[XmlAttribute("type")]
	public string type;
	public SerializableVector3 point1, point2, point3;
}

[System.Serializable]
public class ItemDatabase {

	[XmlArray("CalibratedItems")]
	public List<ItemEntry> list = new List<ItemEntry>();
}
}


