using System.Xml.Serialization;

namespace VRCalibrationTool
{
    /// <summary>
    /// An item entry.
    /// </summary>
    [System.Serializable]
    public class ItemEntry
    {
        /// <summary>
        /// Path of the ItemEntry prefab.
        /// </summary>
        public string prefabPath
        {
            get
            {
                return type + index.ToString();
            }
        }
        /// <summary>
        /// Index of ItemEntry.
        /// </summary>
        [XmlAttribute("index")]
        public int index;
        /// <summary>
        /// Type of ItemEntry.
        /// </summary>
        [XmlAttribute("type")]
        public string type;
        /// <summary>
        /// Points of calibration.
        /// </summary>
        [XmlArrayItem(typeof(SerializableVector3), ElementName = "point")]
        [XmlArray("points")]
        public SerializableVector3[] points;

        public ItemEntry()
        { }

        public ItemEntry(int index, string type, PositionTag[] points)
        {
            this.index = index;
            this.type = type;
            this.points = new SerializableVector3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                this.points[i] = new SerializableVector3(points[i].transform.position);
            }
        }
    }
}
