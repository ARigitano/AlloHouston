using System.Xml;
using System.Xml.Serialization;

namespace VRCalibrationTool
{
public class CalibratedObject {

	[XmlAttribute("type")]
	public string type;
	public SerializableVector3 point1, point2, point3;

}
}
