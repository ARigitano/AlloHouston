// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Xml;
using System.Xml.Serialization;

namespace VRCalibrationTool
{
    public class CalibratedObject
    {
        [XmlAttribute("type")]
        public string type;
        public SerializableVector3 point1, point2, point3;
    }
}
