﻿using DisplaySystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisplaySystem
{
    [Serializable()]
    public class PowerSupplyModel
    {
        public int powerSupplyID { get; set; }
        public string powerSupplyName { get; set; }
        public bool function { get; set; }
        //轨道
        public List<TrackLine> containedTrackLine { get; set; }
        //道岔
        public List<TrackPoint> containedTrackPoint { get; set; }
        //信号机
        public List<Signal> functionalTrackPoint { get; set; }
        //供电范围
        public string powerSupplyRange { get; set; }
    }
}
