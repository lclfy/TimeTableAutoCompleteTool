using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableAutoCompleteTool
{
    class CommandModel
    {//车次号，停运状态，高峰临客
        public string trainNumber { get; set; }
        public Boolean streamStatus { get; set; }  
        //0为普通-1为高峰-2为临客-3为周末-有的再加
        public int trainType { get; set; }
    }
}
