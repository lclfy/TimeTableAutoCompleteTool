using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DisplaySystem
{
    [Serializable()]
    public class TrackPoint : IComparable<TrackPoint> , ICloneable
    {
        public int trackPointID { get; set; }
        //是否为信号机
        public bool function { get; set; }
        //  1定位 2反位
        public int switchDirection { get; set; }
        public Point trackPoint { get; set; }
        //              ==3
        //  1===//==2
        public int firstTrackLine { get; set; }
       public int secondTrackLine { get; set; }
        public int thirdTrackLine { get; set; }

        public int CompareTo(TrackPoint other)
        {
            if (null == other)
            {
                return 1;//空值比较大，返回1
            }
            return this.trackPointID.CompareTo(other.trackPointID);//升序
            //return other.trackPointID.CompareTo(this.trackPointID);//降序
        }

        public object Clone()
        {
            TrackPoint _tp = new TrackPoint();
            _tp.trackPointID = trackPointID;
            _tp.function = function;
            _tp.switchDirection = switchDirection;
            _tp.trackPoint = trackPoint;
            _tp.firstTrackLine = firstTrackLine;
            _tp.secondTrackLine = secondTrackLine;
            _tp.thirdTrackLine = thirdTrackLine;
               return _tp  as object;//深复制
        }
    }
}
