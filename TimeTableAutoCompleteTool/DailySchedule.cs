using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableAutoCompleteTool
{
    class DailySchedule
    {
        public int id { get; set; }
        public string trainNumber { get; set; }
        public string streamStatus { get; set; }
        public string startStation { get; set; }
        public string stopStation { get; set; }
        public string stopTime { get; set; }
        public string startTime { get; set; }
        //0为普通-1为高峰-2为临客-3为周末-有的再加
        public int trainType { get; set; }
        //1始发 2终到
        public int stopStartStatus { get; set; }
        public string stopToStartTime { get; set; }
        public string trainBelongsTo { get; set; }
        public string trackNum { get; set; }
        //编组
        public string trainConnectType { get; set; }
        //定员
        public string ratedSeats { get; set; }
        public string trainModel { get; set; }
        //备注
        public string extraText { get; set; }
        //新旧交替
        public string tipsText { get; set; }
    }
}
