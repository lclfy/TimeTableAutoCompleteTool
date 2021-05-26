using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TimeTableAutoCompleteTool
{
    [Serializable()]
    public class TrackLine : IComparable<TrackLine>, ICloneable
    {
        public int trackLineID { get; set; }
        public string trackText { get; set; }
        //左右坐标
        public Point selfLeftPoint { get; set; }
        public Point selfRightPoint { get; set; }
        //左右节点
        public TrackPoint leftTrackPoint { get; set; }
        public TrackPoint rightTrackPoint { get; set; }
        //左右方向
        public string leftWayTo { get; set; }
        public string rightWayTo { get; set; }
        //该轨道在无电区内包含；0=全部，1=左半部，2=右半部
        public int containsInPS { get; set; }
        public int CompareTo(TrackLine other)
        {
            if (null == other)
            {
                return 1;//空值比较大，返回1
            }
            return this.trackLineID.CompareTo(other.trackLineID);//升序
            //return other.trackLineID.CompareTo(this.trackLineID);//降序
        }

        public TrackLine()
        {
            trackText = "";
            //左右坐标
            selfLeftPoint = new Point();
            selfRightPoint = new Point();
            leftTrackPoint = new TrackPoint();
            rightTrackPoint = new TrackPoint();
            leftWayTo = "";
            rightWayTo = "";
    }

        public object Clone()
        {
            TrackLine _tl = new TrackLine();
            _tl.trackLineID = this.trackLineID;
            _tl.trackText = this.trackText;
            _tl.selfLeftPoint = this.selfLeftPoint;
            _tl.selfRightPoint = this.selfRightPoint;
            _tl.leftTrackPoint = this.leftTrackPoint;
            _tl.rightTrackPoint = this.rightTrackPoint;
            _tl.leftWayTo = this.leftWayTo;
            _tl.rightWayTo = this.rightWayTo;
            _tl.containsInPS = this.containsInPS;

            return _tl  as object;//深复制
        }
    }
}
