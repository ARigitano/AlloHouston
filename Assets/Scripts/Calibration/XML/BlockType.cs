using System.Xml.Serialization;

namespace VRCalibrationTool
{
    public enum BlockType
    {
        [XmlEnum("table")]
        Table,
        [XmlEnum("experiment")]
        Experiment,
        [XmlEnum("door")]
        Door,
        [XmlEnum("corner")]
        Corner,
        [XmlEnum("unknown")]
        Unknown,
    }
}