using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableAutoCompleteTool
{
    class CommandModel
    {//车次号，停运状态(0停开，1开行，2次日)，高峰临客
        public string trainNumber { get; set; }
        public int streamStatus { get; set; }
        //0为普通-1为高峰-2为临客-3为周末-有的再加
        public int trainType { get; set; }
        //车型
        public string trainModel { get; set; }
        //短-长-8+8（0,1,2）
        public int trainConnectType { get; set; }
    }
}
