using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace VRCalibrationTool
{
    /// <summary>
    /// Stores or loads instantiable objects' coordinates for the auto calibration phase
    /// </summary>
    public class XMLManager : MonoBehaviour {

	    public static XMLManager ins;
        public ItemDatabase itemDB;

        void Awake()
        {
		    ins = this;
		    LoadItems();
	    }

        /// <summary>
        /// Save items' coordinates in the XML file when they are instantiated
        /// </summary>
	    public void SaveItems()
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaa");
		    XmlSerializer serializer = new XmlSerializer (typeof(ItemDatabase));
		    FileStream stream = new FileStream (Application.dataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Create);

	        Debug.Log (stream.Length);

		    serializer.Serialize (stream, itemDB);
		    stream.Close();
	}
    
        //Loads the informations stored in the XML file
	    public void LoadItems() {
		    XmlSerializer serializer = new XmlSerializer (typeof(ItemDatabase));
		    FileStream stream = new FileStream (Application.dataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Open);
		    itemDB = serializer.Deserialize (stream) as ItemDatabase;
		    stream.Close();
	    }

}

    /// <summary>
    /// Serializabe object
    /// </summary>
    [System.Serializable]
    public class ItemEntry
    {
	    [XmlAttribute("type")]
	    public string type;
	    public SerializableVector3 point1, point2, point3;
    }

    /// <summary>
    /// Serializable XML entry
    /// </summary>
    [System.Serializable]
    public class ItemDatabase
    {
	    [XmlArray("CalibratedItems")]
	    public List<ItemEntry> list = new List<ItemEntry>();
    }
}


