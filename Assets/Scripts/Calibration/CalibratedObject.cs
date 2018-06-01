/// <summary>
/// Class for a calibrated object. Contains its type and the coordonates for three position tags.
/// </summary>

using System.Xml;
using System.Xml.Serialization;

namespace VRCalibrationTool
{
    public class CalibratedObject
    {
        [XmlAttribute("type")]
        public string type;									//Type of object
        public SerializableVector3 point1, point2, point3;	//Positions of positiontags
    }
}
