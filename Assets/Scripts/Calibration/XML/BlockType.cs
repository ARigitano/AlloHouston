using System.Xml.Serialization;

namespace CRI.HelloHouston.Calibration.XML
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