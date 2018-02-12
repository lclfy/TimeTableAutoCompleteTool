using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableAutoCompleteTool
{
    class CommandModel
    {//车次号，停运状态
        public string trainNumber { get; set; }
        public Boolean streamStatus { get; set; }  
    }
}
