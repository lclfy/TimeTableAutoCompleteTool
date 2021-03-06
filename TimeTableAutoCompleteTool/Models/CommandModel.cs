﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableAutoCompleteTool
{
    public class CommandModel
    {
        //第几行
        public string trainIndex { get; set; }
        //车次号
        public string trainNumber { get; set; }
        public string secondTrainNumber { get; set; }
        //停运状态(0停开，1开行，2次日，4客调不含的停运)
        public int streamStatus { get; set; }
        //0为普通-1为高峰-2为临客-3为周末-4为加开
        public int trainType { get; set; }
        //车型
        public string trainModel { get; set; }
        //车号
        public string trainId { get; set; }
        //短-长-8+8（0,1,2）
        public int trainConnectType { get; set; }
        //上-下行
        public int upOrDown { get; set; }
        //判断是否匹配上时刻表车次
        public bool MatchedWithTimeTable { get; set; }
        //列车类型-1旅客列车-0其他列车
        public bool psngerTrain { get; set; }

        //未匹配的时刻表名称
        public string notMatchedTabelName { get; set; }

        public CommandModel()
        {
            trainIndex = "";
            trainNumber = "";
            secondTrainNumber = "";
            trainModel = "";
            trainId = "";
            upOrDown = -1;
        }

    }
}
