using System.Xml.Serialization;

namespace CRI.HelloHouston.Calibration.Data
{
    public enum BlockType
    {
        Table,
        Experiment,
        Door,
        Corner,
        FloorAndRoof,
        Generic,
        Unknown,
    }
}