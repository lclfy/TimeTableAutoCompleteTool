using System;
using System.Collections.Generic;
using System.Text;

namespace DisplaySystem.Model
{
    [Serializable()]
    class ModelData
    {
        public string title { get; set; }
        public int startTrackNum { get; set; }
        public int stopTrackNum { get; set; }
        public List<TrackLine> tLine { get; set; }
        public List<TrackPoint> tPoint { get; set; }
        public List<PowerSupplyModel> psModel { get; set; }
        public List<Signal> signal { get; set; }
    }
}
