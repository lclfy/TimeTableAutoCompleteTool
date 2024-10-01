using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TimeTableAutoCompleteTool
{
    [Serializable()]
    public class Signal : ICloneable
    {
        public string signalID { get; set; }
        public Point signalPoint { get; set; }
        public int signalType { get; set; }
        public int signalDir { get; set; }
        public string tip { get; set; }

        public Signal()
        {
            signalID = "";
            signalPoint = new Point();
        }

        public object Clone()
        {
            Signal _sg = new Signal();
            _sg.signalID = this.signalID;
            _sg.signalPoint = this.signalPoint;
            _sg.signalType = this.signalType;
            //0为朝左，1为朝右
            _sg.signalDir = this.signalDir;
            _sg.tip = this.tip;
        return _sg as object;//深复制
        }
    }
}
