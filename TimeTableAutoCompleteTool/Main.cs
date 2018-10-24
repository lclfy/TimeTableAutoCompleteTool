using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NPOI.SS.UserModel;
//2003
using NPOI.HSSF.UserModel;
//2007以后
using NPOI.XSSF.UserModel;
using System.IO;
using System.Text.RegularExpressions;
using NPOI.SS.Util;
using CCWin;
using System.Configuration;
using System.Globalization;

namespace TimeTableAutoCompleteTool
{
    public partial class Main : Skin_Mac
    {
        private Boolean hasText = false;
        private Boolean hasFilePath = false;
        private List<CommandModel> commandModel;
        private List<CommandModel> yesterdayCommandModel;
        private List<CommandModel> detectedCModel;
        private List<CaculatorModel> caculatorModel;
        private List<DailySchedule> allDailyScheduleModel;
        private List<EMUCheckModel> allEmuCheckModel;
        List<string> ExcelFile = new List<string>();
        private string startPath = "";
        private string wrongTrain = "";
        private string commandText = "";
        private int lastCommandLength = 0;
        private string yesterdayCommandText = "";
        private int lastYesterdayCommandLength = 0;
        int fontSize = 12;
        string filePath = "";
        string addedTrainText = "";
        //行车1，综控2，动车所3；
        public int modeSelect;
        string upStations = "京广-（新乡东 安阳东 鹤壁东 邯郸东 石家庄 保定东 定州东 正定机场 邢台东 高碑店东 涿州东 北京西）石地区-（太原南 定州东 阳泉北 石家庄东 藁城南 辛集南 衡水北 景州 德州东 平原东 禹城东 齐河）京沪北-（北京南 廊坊 天津西 天津 天津南 沧州西 德州东 泰安 曲阜东 滕州东 枣庄）徐兰-（ 开封北 兰考南 商丘 永城北 砀山南 萧县北 徐州东）京沪南-（ 宿州东 蚌埠南 定远 滁州 南京南 南京 镇江南 丹阳北 常州北 无锡东 苏州 苏州北 昆山南 上海 上海虹桥）胶济-（济南西 威海 荣成 胶州北 高密 潍坊 昌乐 青州市 淄博 周村东 章丘 济南东 烟台 青岛北 青岛） 城际-（宋城路）  京东北-（ 辽阳 铁岭西 开原西 昌图西 四平东 公主岭南 长春西 德惠西 扶余北 双城北 哈尔滨西 秦皇岛 沈阳北 沈阳 承德南 承德 怀柔南 朝阳 大连北 长春 哈尔滨西 ） 郑东南-（ 合肥南 肥东 巢北 黄庵 全椒 江浦 黄山北 金华南 宁波 杭州东 温州南 义乌 松江南 金山北 嘉善南 嘉兴南 桐乡 海宁西 余杭 ） ";
        string downStations = "郑州 郑州西 京广-（ 许昌东 漯河西 驻马店西 信阳东 明港东 孝感北 武汉 汉口 咸宁北 赤壁北 岳阳东 汨罗东 长沙南 株洲西 衡山西 衡阳东 耒阳西 郴州西 韶关 英德西 清远 广州北 深圳北 福田 深圳北 广州南 庆盛 虎门 光明城 西九龙 珠海）城际-（ 新郑机场 焦作）徐兰-（ 巩义南 洛阳龙门 三门峡西 灵宝西 华山北 渭南北 临潼东 西安北 汉中 宝鸡南 天水南 秦安 通渭 定西北 榆中 兰州西）西南-（ 成都东 重庆西 重庆北 贵阳北 昆明南 南宁东 怀化南 湘潭北 韶山南 芷江 新晃西 娄底南 桂林 玉溪 宜昌东 恩施 襄阳北 汉川 天门南 仙桃西 潜江 荆州 枝江北 湛江西）东南-（ 黄冈东 萍乡北 新余北 宜春东 鹰潭北 南昌西 九江  赣州西 厦门北 潮汕 漳州 惠州南）郑万-（长葛北 禹州东 郏县 平顶山西 方城 邓州东 南阳卧龙 襄阳东津 南漳 保康县 神农架 兴山 巴东北 巫山 奉节 云阳 万州北） 郑合-（许昌北 鄢陵南 扶沟南 西华 周口东 淮阳 沈丘北 界首南 临泉 阜阳西）";
        string build = "build 45 - v181024";
        string readMe = "build45更新内容:\n" +
            "修复了动车所当出现共用车底的两个车次时，位于下方的车次无法显示的问题。";

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "客调命令辅助工具";
            buildLBL.Text = build;
            start_Btn.Enabled = false;
            compare_btn.Enabled = false;
            //TrainEarlyCaculator_Btn.Enabled = false;
            load();
            checkedChanged();
            contentOfDeveloper.IsBalloon = true;
            updateReadMe.IsBalloon = true;
            updateReadMe.SetToolTip(this.buildLBL, readMe);
            FontSize_tb.Text = fontSize.ToString();
        }

        private void load()
        {
            int _modeSelect = 0;
            int.TryParse(ConfigurationManager.AppSettings["modeSelect"], out _modeSelect);
            int _fontSize = 0;
            int.TryParse(ConfigurationManager.AppSettings["fontSize"], out _fontSize);
            if (_modeSelect == 0)
            {
                Welcome form = new Welcome(this);
                form.ShowDialog();
            }
            else
            {
                modeSelect = _modeSelect;
                ModeSelect();
            }
            if (_fontSize != 0)
            {
                fontSize = _fontSize;
            }
        }

        public void ModeSelect()
        {
            if (modeSelect == 0 || modeSelect == 1)
            {
                radioButton1.Select();
            }
            else if (modeSelect == 2)
            {
                radioButton2.Select();
            }
            else if (modeSelect == 3)
            {
                radioButton3.Select();
            }
        }

        private void save()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.Save();
            if (config.AppSettings.Settings["modeSelect"] == null)
            {
                KeyValueConfigurationElement _k = new KeyValueConfigurationElement("modeSelect", modeSelect.ToString());
                config.AppSettings.Settings.Add(_k);
            }
            else
            {
                config.AppSettings.Settings["modeSelect"].Value = modeSelect.ToString();
            }
            if (config.AppSettings.Settings["fontSize"] == null)
            {
                KeyValueConfigurationElement _k = new KeyValueConfigurationElement("fontSize", fontSize.ToString());
                config.AppSettings.Settings.Add(_k);
            }
            else
            {
                config.AppSettings.Settings["fontSize"].Value = fontSize.ToString();
            }
            config.Save();
            ConfigurationManager.RefreshSection("fontSize");
            ConfigurationManager.RefreshSection("modeSelect");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            save();
            base.OnClosing(e);
        }

        private void command_rTb_TextChanged(object sender, EventArgs e)
        {
            commandText = command_rTb.Text;
            if (command_rTb.Text.Length != 0)
            {
                hasText = true;
                startBtnCheck();
                if (command_rTb.Text.Length != lastCommandLength)
                    analyseCommand();
            }
            else
            {
                hasText = false;
                startBtnCheck();
            }
            lastCommandLength = command_rTb.Text.Length;
        }

        private void importTimeTable_Btn_Click(object sender, EventArgs e)
        {
            SelectPath();
        }

        private void startBtnCheck()
        {
            if (hasFilePath && hasText)
            {
                start_Btn.Enabled = true;
                //TrainEarlyCaculator_Btn.Enabled = true;
            }
            else
            {
                start_Btn.Enabled = false;
                //TrainEarlyCaculator_Btn.Enabled = false;
            }
            if (yesterdayCommandModel != null && yesterdayCommandModel.Count != 0 && hasFilePath && hasText && ExcelFile.Count > 0)
            {
                compare_btn.Enabled = true;
            }
            else
            {
                compare_btn.Enabled = false;
            }
        }

        private void start_Btn_Click(object sender, EventArgs e)
        {
            if (commandModel.Count != 0 && radioButton1.Checked)
            {
                if (FontSize_tb.Text.Length == 0)
                {
                    FontSize_tb.Text = "12";
                }
                updateTimeTable();
            }
            else if (commandModel.Count != 0 && radioButton2.Checked)
            {
                if (radioButton4.Checked)
                {
                    readBasicTrainTable();
                }else if (radioButton5.Checked)
                {
                    readEMUCTable();
                }

            }
            else if (commandModel.Count != 0 && radioButton3.Checked)
            {
                trainTypeAutoComplete();
            }
            else
            {
                MessageBox.Show("未检测到任何车次信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void analyseCommand(bool isYesterday = false, string detectedTrainRow = "")
        {   //分析客调命令
            //删除不需要的标点符号-字符
            int addedTrainCount = 0;
            try
            {
                string wrongNumber = "";
                List<string> _commands = removeUnuseableWord(isYesterday);
                String[] AllCommand;
                if (detectedTrainRow.Length == 0)
                {//不是抽样调查
                    AllCommand = _commands[0].Split('。');
                }
                else
                {
                    //string[] mf3={"c","c++","c#"};
                    AllCommand = new string[1];
                    AllCommand[0] = detectedTrainRow;
                }
                List<CommandModel> AllModels = new List<CommandModel>();
                addedTrainText = "";
                for (int i = 0; i < AllCommand.Length; i++)
                {
                    if (AllCommand[i].Contains("站") &&
                        AllCommand[i].Contains("开") && (
                        AllCommand[i].Contains("001") ||
                        AllCommand[i].Contains("002") ||
                        AllCommand[i].Contains("003") ||
                        AllCommand[i].Contains("004") ||
                        AllCommand[i].Contains("005") ||
                        AllCommand[i].Contains("006") ||
                        AllCommand[i].Contains("007") ||
                        AllCommand[i].Contains("008") ||
                        AllCommand[i].Contains("009")))
                    {//加开车次，单独存储
                        string addedCommand = AllCommand[i];
                        if (addedCommand.Contains("月") && addedCommand.Contains("日"))
                        {
                            addedTrainCount++;
                            addedTrainText = addedTrainText + addedTrainCount + "、" + addedCommand.Split('：')[addedCommand.Split('：').Length - 1].Remove(0, 3) + "\n";
                        }
                    }
                    //取行号，便于查找
                    string index = AllCommand[i].Split('、')[0].Trim().Replace("\n", "");
                    String[] command;
                    String[] AllTrainNumberInOneRaw;
                    string trainModel = "null";
                    int streamStatus = 1;
                    //用于某些情况下标记不正常车次避免重复添加
                    Boolean isNormal = true;
                    int trainType = 0;
                    command = AllCommand[i].Split('：');
                    if (command.Length > 1)
                    {//非常规情况找车次
                        if (!command[1].Contains("G") &&
                        !command[1].Contains("D") &&
                        !command[1].Contains("C") &&
                        !command[1].Contains("J"))
                        {                //特殊数据
                                         //304、2018年02月11日，null-G4326/7：18：50分出库11日当天请令：临客线-G4326/7。
                                         //305、2018年02月11日，null - G4328 / 5：18：50分出库11日当天请令：临客线-G4328/5。
                            for (int r = 0; r < command.Length; r++)
                            {//从后往前开始找车次
                                if (command[command.Length - r - 1].Contains("G") ||
                                    command[command.Length - r - 1].Contains("D") ||
                                    command[command.Length - r - 1].Contains("C") ||
                                    command[command.Length - r - 1].Contains("J"))
                                {//找到了就用该项作为车次
                                    command[1] = command[command.Length - r - 1];
                                    break;
                                }
                            }
                        }
                        if (command[1].Contains("，"))
                        {//有逗号-逗号换横杠
                            command[1] = command[1].Replace('，', '-');
                        }
                        if (command[1].Contains("高峰"))
                        {
                            trainType = 1;
                        }
                        else if (command[1].Contains("临客"))
                        {
                            trainType = 2;
                        }
                        else if (command[1].Contains("周末"))
                        {
                            trainType = 3;
                        }

                        for (int timeCount = 0; timeCount < command.Length; timeCount++)
                        {
                            if (command[timeCount].Contains("CR"))
                            {
                                for (int word = 0; word < command[timeCount].Split('，').Length; word++)
                                {
                                    if (command[timeCount].Split('，')[word].Contains("CR") ||
                                        command[timeCount].Split('，')[word].Contains("cr"))
                                    {
                                        trainModel = command[timeCount].Split('，')[word];
                                    }
                                }

                            }
                        }


                        //找停运标记-特殊标记则直接加入模型
                        for (int n = 0; n < command.Length; n++)
                        {//从后往前开始找停运状态
                            if ((command[command.Length - n - 1].Contains("停运") &&
                                !command[command.Length - n - 1].Contains("G") &&
                                !command[command.Length - n - 1].Contains("D") &&
                                !command[command.Length - n - 1].Contains("C") &&
                                !command[command.Length - n - 1].Contains("J") &&
                                !command[command.Length - n - 1].Contains("00")) ||
                                 (command.Length > 2 && command[command.Length - n - 1].Contains("停运）")))
                            {//如果有-则继续判断是否全部停运
                             //特殊情况-部分停运，但停运部分使用括号标记
                             //76、2018年02月15日，CRH380AL-2590：DJ5732-G2001-(G662-G669：停运)。
                             //221、2018年02月22日，CRH380AL-2600：【0J5901-DJ5902-G6718(石家庄～北京西):停运】，0G4909-G4910-G801/4-G6611-G1559/8-G807-0G808。
                                if (command[command.Length - n - 1].Contains("停运）"))
                                {
                                    if (command[command.Length - n - 1].Contains("G") ||
                                        command[command.Length - n - 1].Contains("D") ||
                                        command[command.Length - n - 1].Contains("C") ||
                                        command[command.Length - n - 1].Contains("J") ||
                                        command[command.Length - n - 1].Contains("0"))
                                    {//如果停运标记后面还有车的话
                                        List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(command[command.Length - n - 1], @"[\u4e00-\u9fa5]", "").Replace('）', ' ').Replace('，', ' ').Split('-'), 1, trainType, trainModel, index);
                                        foreach (CommandModel model in tempModels)
                                        {
                                            if (!model.trainNumber.Contains("未识别"))
                                            {
                                                AllModels.Add(model);
                                            }
                                            else
                                            {
                                                wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.trainNumber + "\r\n";
                                            }
                                        }
                                    }
                                    isNormal = false;
                                    AllTrainNumberInOneRaw = command[1].Split('-');
                                    //寻找车次中的括号左半部分
                                    //从前往后找，找到标记后的车次为停开
                                    bool stopped = false;
                                    for (int m = 0; m < AllTrainNumberInOneRaw.Length; m++)
                                    {
                                        if (AllTrainNumberInOneRaw[m].Contains("（G") ||
                                            AllTrainNumberInOneRaw[m].Contains("（D") ||
                                            AllTrainNumberInOneRaw[m].Contains("（C") ||
                                            AllTrainNumberInOneRaw[m].Contains("（J") ||
                                            AllTrainNumberInOneRaw[m].Contains("（0"))
                                        {//找到标记
                                            stopped = true;
                                        }
                                        //停开与开行分开进行建模
                                        if (stopped == true)
                                        {//不开
                                            List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[m], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 0, trainType, trainModel, index);
                                            foreach (CommandModel model in tempModels)
                                            {
                                                if (!model.trainNumber.Contains("未识别"))
                                                {
                                                    AllModels.Add(model);
                                                }
                                                else
                                                {
                                                    wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.trainNumber + "\r\n";
                                                }
                                            }
                                        }
                                        else if (stopped == false)
                                        {//开
                                            List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[m], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 1, trainType, trainModel, index);
                                            foreach (CommandModel model in tempModels)
                                            {
                                                if (!model.trainNumber.Contains("未识别"))
                                                {
                                                    AllModels.Add(model);
                                                }
                                                else
                                                {
                                                    wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.trainNumber + "\r\n";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //正常情况-则默认所有车次停开
                                    streamStatus = 0;
                                }
                            }
                            break;
                        }
                        //判断某车底中仅停运一部分，且停运标记在车次中的特殊停运车次
                        //示例：236、2018年02月12日，CRH380AL-2607：0D5699(停运)-D5700(停运)-0G75-G75(郑州东始发)。
                        if (command[1].Contains("停"))
                        {
                            AllTrainNumberInOneRaw = command[1].Split('-');
                            //如果部分停开-则停开与开行分开进行建模
                            for (int h = 0; h < AllTrainNumberInOneRaw.Length; h++)
                            {
                                if (AllTrainNumberInOneRaw[h].Contains("停"))
                                {//去中文添加-由于部分情况下无法辨认小括号-因此必须在此处去除小括号
                                    List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 0, trainType, trainModel, index);
                                    foreach (CommandModel model in tempModels)
                                    {
                                        if (!model.trainNumber.Contains("未识别"))
                                        {
                                            AllModels.Add(model);
                                        }
                                        else
                                        {
                                            wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.trainNumber + "\r\n";
                                        }
                                    }
                                }
                                else
                                {
                                    List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 1, trainType, trainModel, index);
                                    foreach (CommandModel model in tempModels)
                                    {
                                        if (!model.trainNumber.Contains("未识别"))
                                        {
                                            AllModels.Add(model);
                                        }
                                        else
                                        {
                                            wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.trainNumber + "\r\n";
                                        }
                                    }
                                }
                            }
                        }
                        else if (command[1].Contains("次日"))
                        {

                            AllTrainNumberInOneRaw = command[1].Split('-');
                            //同理-部分次日-则次日与当日分开进行建模
                            for (int h = 0; h < AllTrainNumberInOneRaw.Length; h++)
                            {
                                if (AllTrainNumberInOneRaw[h].Contains("次日"))
                                {//去中文添加-由于部分情况下无法辨认小括号-因此必须在此处去除小括号
                                    List<CommandModel> tempModels;
                                    if (streamStatus != 0)
                                    {
                                        tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 2, trainType, trainModel, index);
                                    }
                                    else
                                    {
                                        tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), streamStatus, trainType, trainModel, index);
                                    }
                                    foreach (CommandModel model in tempModels)
                                    {
                                        if (!model.trainNumber.Contains("未识别"))
                                        {
                                            AllModels.Add(model);
                                        }
                                        else
                                        {
                                            wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.trainNumber + "\r\n";
                                        }
                                    }
                                }
                                else
                                {
                                    List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), streamStatus, trainType, trainModel, index);
                                    foreach (CommandModel model in tempModels)
                                    {
                                        if (!model.trainNumber.Contains("未识别"))
                                        {
                                            AllModels.Add(model);
                                        }
                                        else
                                        {
                                            wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.trainNumber + "\r\n";
                                        }
                                    }
                                }
                            }
                        }
                        else if (command[1].Contains("站") ||
                            (command[1].Contains("道") ||
                            command[1].Contains("到") ||
                            command[1].Contains("开")))
                        {//221、2018年03月20日，CRH380AL-2619：0J5901-DJ5902-G6718(石家庄～北京西)-G801/4（商丘站变更为26道）-0093(商丘站14:25开，郑州东徐兰场15:20到)-0094(郑州东徐兰场16:05开，郑州东动车所16.25到)。
                         //101、2018年03月20日，CRH380B-3763+3758：G1922/19（商丘站变更为27道）。
                         //把车次单独分离-去中文-去横杠-去括号内数字-在此处去除小括号
                         //去括号内数字方法-把括号前半部分换成空格，会变成G801/4 26，G1922/19 27
                         //识别时取空格前数字即可
                            AllTrainNumberInOneRaw = Regex.Replace(command[1], @"[\u4e00-\u9fa5]", "").Replace("（", " ").Replace("）", "").Split('-');
                            //把车次添加模型
                            List<CommandModel> tempModels = trainModelAddFunc(AllTrainNumberInOneRaw, streamStatus, trainType, trainModel, index);
                            foreach (CommandModel model in tempModels)
                            {
                                if (!model.trainNumber.Contains("未识别"))
                                {
                                    AllModels.Add(model);
                                }
                                else
                                {
                                    wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.trainNumber + "\r\n";
                                }
                            }
                        }
                        else if (isNormal)
                        {//如果一切正常 则
                         //把车次单独分离-去中文-去横杠-由于部分情况下无法辨认小括号-因此必须在此处去除小括号
                            AllTrainNumberInOneRaw = Regex.Replace(command[1], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-');
                            //把车次添加模型
                            List<CommandModel> tempModels = trainModelAddFunc(AllTrainNumberInOneRaw, streamStatus, trainType, trainModel, index);
                            foreach (CommandModel model in tempModels)
                            {
                                if (!model.trainNumber.Contains("未识别"))
                                {
                                    AllModels.Add(model);
                                }
                                else
                                {
                                    wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.trainNumber + "\r\n";
                                }
                            }
                        }
                    }
                }
                //右方显示框内容
                String commands = "";
                foreach (CommandModel model in AllModels)
                {
                    String streamStatus = "";
                    String trainType = "";
                    if (model.streamStatus == 1)
                    {
                        streamStatus = "开行";
                    }
                    else
                    {
                        streamStatus = "停运";
                    }
                    switch (model.trainType)
                    {
                        case 0:
                            trainType = "普通";
                            break;
                        case 1:
                            trainType = "高峰";
                            break;
                        case 2:
                            trainType = "临客";
                            break;
                        case 3:
                            trainType = "周末";
                            break;
                    }
                    if (model.secondTrainNumber.Equals("null"))
                    {
                        commands = commands + "第" + model.trainIndex.Trim() + "行-" + model.trainNumber + "-" + streamStatus + "-" + trainType + "\r\n";
                    }
                    else
                    {
                        commands = commands + "第" + model.trainIndex.Trim() + "行-" + model.trainNumber + "-" + model.secondTrainNumber + "-" + streamStatus + "-" + trainType + "\r\n";
                    }
                }
                wrongTrain = wrongNumber;
                if (wrongTrain != null)
                {
                    if (wrongTrain.Length != 0)
                    {
                        searchResult_tb.Text = "识别错误车辆：" + "\r\n" + wrongTrain;
                    }
                }
                outputTB.Text = "共" + AllModels.Count.ToString() + "趟" + "\r\n" + commands;
                if(detectedTrainRow.Length == 0)
                {
                    if (!isYesterday)
                    {
                        commandModel = AllModels;
                    }
                    else
                    {
                        yesterdayCommandModel = AllModels;
                    }
                }
                else
                {
                    detectedCModel = AllModels;
                    //analyseCommand();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("出现错误：" + e.ToString().Split('。')[0], "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        public bool IsTrainNumber(string input)
        {//判断是否是符合规范的车次 若不符合 则给予识别错误提示
            bool _isTrainNumber = false;
            if (input.Contains("CR"))
            {
                return false;
            }
            Regex regexOnlyNumAndAlphabeta = new Regex(@"^[A-Za-z0-9]+$");
            Regex regexOnlyAlphabeta = new Regex(@"^[A-Za-z]+$");
            if (regexOnlyNumAndAlphabeta.IsMatch(input) &&
                !regexOnlyAlphabeta.IsMatch(input) &&
                input.Length < 8 &&
                input.Length > 1)
            {
                _isTrainNumber = true;
            }
            return _isTrainNumber;
        }

        private List<string> removeUnuseableWord(bool isYesterday = false,string detectedCommand = "")
        {//字符转换
            String standardCommand = "";
            if(detectedCommand.Length  == 0)
            {
                if (!isYesterday)
                {
                    standardCommand = command_rTb.Text.ToString();
                }
                else
                {
                    standardCommand = yesterdayCommand_rtb.Text.ToString();
                }
            }
            else
            {
                standardCommand = detectedCommand;
            }
            List<string> commands = new List<string>();
            standardCommand = removing(standardCommand.Trim());
            commands.Add(standardCommand.Trim());
            return commands;
        }

        private string removing(string standardCommand)
        {
            if (standardCommand.Contains(":"))
            { standardCommand = standardCommand.Replace(":", "："); }
            //删除客调命令中的时间
            //standardCommand = Regex.Replace(standardCommand, @"\d+：\d", "");
            standardCommand = Regex.Replace(standardCommand,@"[0-9]{2}(：)[0-9]{2}","");
            standardCommand = Regex.Replace(standardCommand, @"[0-9]{1}(：)[0-9]{2}", "");

            if (standardCommand.Contains("~"))
                standardCommand = standardCommand.Replace("~", "～");
            if (standardCommand.Contains("~"))
                standardCommand = standardCommand.Replace("~", "～");
            if (standardCommand.Contains("签发："))
                standardCommand = standardCommand.Replace("签发：", "");
            if (standardCommand.Contains("会签："))
                standardCommand = standardCommand.Replace("会签：", "");
            if (standardCommand.Contains("("))
                standardCommand = standardCommand.Replace("(", "（");
            if (standardCommand.Contains(")"))
                standardCommand = standardCommand.Replace(")", "）");
            if (standardCommand.Contains("d"))
                standardCommand = standardCommand.Replace("d", "D");
            if (standardCommand.Contains("g"))
                standardCommand = standardCommand.Replace("g", "G");
            if (standardCommand.Contains("c"))
                standardCommand = standardCommand.Replace("c", "C");
            if (standardCommand.Contains("j"))
                standardCommand = standardCommand.Replace("j", "J");
            //if (standardCommand.Contains("CRH"))
            // standardCommand = standardCommand.Replace("CRH", "");
            //if (standardCommand.Contains("CR"))
            // standardCommand = standardCommand.Replace("CR", "");
            if (standardCommand.Contains("；"))
                standardCommand = standardCommand.Replace("；", "");
            //特殊情况添加 221、2018年02月22日，CRH380AL-2600：【0J5901-DJ5902-G6718(石家庄～北京西):停运】，0G4909-G4910-G801/4-G6611-G1559/8-G807-0G808。
            //中括号/大括号转小括号 减少后期识别代码数量
            if (standardCommand.Contains("["))
                standardCommand = standardCommand.Replace("[", "（");
            if (standardCommand.Contains("—"))
                standardCommand = standardCommand.Replace("—", "-");
            if (standardCommand.Contains("]"))
                standardCommand = standardCommand.Replace("]", "）");
            if (standardCommand.Contains("【"))
                standardCommand = standardCommand.Replace("【", "（");
            if (standardCommand.Contains("】"))
                standardCommand = standardCommand.Replace("】", "）");
            if (standardCommand.Contains("{"))
                standardCommand = standardCommand.Replace("{", "）");
            if (standardCommand.Contains("}"))
                standardCommand = standardCommand.Replace("}", "）");
            if (standardCommand.Contains(" "))
                standardCommand = standardCommand.Replace(" ", "");
            return standardCommand;
        }

        private List<CommandModel> trainModelAddFunc(String[] AllTrainNumberInOneRaw, int streamStatus, int trainType, string trainModel, string index)
        {//建立车次模型-通用方法
            //处理单程双车次车辆
            int trainConnectType = -1;
            string trainId = "";
            List<CommandModel> AllModels = new List<CommandModel>();
            if (!trainModel.Equals("null"))
            {//0短编 1长编 2重联
                if (trainModel.Contains("L") ||
                    trainModel.Contains("2B") ||
                    trainModel.Contains("2E") ||
                    trainModel.Contains("1E") ||
                    trainModel.Contains("AF-A") ||
                    trainModel.Contains("BF-A"))
                {
                    trainConnectType = 1;
                }
                else if (trainModel.Contains("+"))
                {
                    trainConnectType = 2;
                }
                else
                {
                    trainConnectType = 0;
                }
            }
            if (trainConnectType == 2)
            {//重联，考虑不同型号重联情况
                Regex _regexOnlyNum = new Regex(@"^[0-9]+$");
                string[] trainIds = trainModel.Split('+');
                for (int i = 0; i < trainIds.Length; i++)
                {
                    for (int j = 0; j < trainIds[i].Split('-').Length; j++)
                    {
                        if (_regexOnlyNum.IsMatch(trainIds[i].Split('-')[j]))
                        {
                            if (!trainId.Contains("/"))
                            {
                                trainId = trainIds[i].Split('-')[j] + "/";
                            }
                            else
                            {
                                trainId = trainId + trainIds[i].Split('-')[j];
                            }
                        }
                    }
                }
            }
            else if (trainConnectType == 1)
            {//长编
                if (trainModel.Split('-').Length == 2)
                {
                    trainId = trainModel.Split('-')[1] + "L";
                }
                else if (trainModel.Split('-').Length == 3)
                {
                    trainId = trainModel.Split('-')[2] + "L";
                }
            }
            else
            {
                if (trainModel.Split('-').Length == 2)
                {
                    trainId = trainModel.Split('-')[1];
                }
                else if (trainModel.Split('-').Length == 3)
                {
                    trainId = trainModel.Split('-')[2];
                }
            }
            if (!trainModel.Contains("+"))
            {
                if (!trainModel.Contains("-A"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim();
                }
                else
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-A";
                }
            }
            else
            {
                if (!trainModel.Contains("-A"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "+";
                }
                else
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-A+";
                }

            }
            //判断index是否为纯数字
            Regex regexOnlyNum = new Regex(@"^[0-9]+$");
            if (!regexOnlyNum.IsMatch(index))
            {
                char[] _indexChar = index.ToCharArray();
                string _tempIndexString = "";
                for (int i = 0; i < _indexChar.Length; i++)
                {
                    if (regexOnlyNum.IsMatch(_indexChar[i].ToString()))
                    {
                        _tempIndexString = _tempIndexString + _indexChar[i];
                    }
                    else
                    {
                        if (i == 0)
                        {//如果第一个字符就不是数字
                            index = "?";
                        }
                        else
                        {
                            index = _tempIndexString;
                            break;
                        }
                    }
                }
            }
            for (int k = 0; k < AllTrainNumberInOneRaw.Length; k++)
            {
                if (AllTrainNumberInOneRaw[k].Contains("G") ||
                   AllTrainNumberInOneRaw[k].Contains("D") ||
                   AllTrainNumberInOneRaw[k].Contains("C") ||
                   AllTrainNumberInOneRaw[k].Contains("J") ||
                   AllTrainNumberInOneRaw[k].Contains("00"))
                {
                    if (AllTrainNumberInOneRaw[k].Contains("/"))
                    {
                        string _trainNumber = "";
                        if (AllTrainNumberInOneRaw[k].Contains(" "))
                        {
                            _trainNumber = AllTrainNumberInOneRaw[k].Split(' ')[0];
                        }
                        else
                        {
                            _trainNumber = AllTrainNumberInOneRaw[k];
                        }
                        String[] trainWithDoubleNumber = _trainNumber.Split('/');
                        //先添加第一个车次
                        CommandModel m1 = new CommandModel();
                        m1.trainNumber = trainWithDoubleNumber[0].Trim();
                        m1.streamStatus = streamStatus;
                        m1.trainType = trainType;
                        m1.trainModel = trainModel;
                        m1.trainConnectType = trainConnectType;
                        m1.trainIndex = index;
                        m1.trainId = trainId;
                        if (!IsTrainNumber(m1.trainNumber))
                        {
                            m1.trainNumber = "未识别-(" + m1.trainNumber + ")";
                        }
                        Char[] firstTrainWord = trainWithDoubleNumber[0].ToCharArray();
                        String secondTrainWord = "";
                        for (int q = 0; q < firstTrainWord.Length; q++)
                        {
                            if (q != firstTrainWord.Length - trainWithDoubleNumber[1].Length)
                            {
                                secondTrainWord = secondTrainWord + firstTrainWord[q];
                            }
                            else
                            {
                                secondTrainWord = secondTrainWord + trainWithDoubleNumber[1];
                                //添加第二个车次
                                m1.secondTrainNumber = secondTrainWord.Trim();
                                m1.upOrDown = -1;
                                AllModels.Add(m1);
                                break;
                            }
                        }
                    }
                    else if (AllTrainNumberInOneRaw[k].Length != 0)
                    {
                        string _trainNumber = "";
                        if (AllTrainNumberInOneRaw[k].Contains(" "))
                        {
                            _trainNumber = AllTrainNumberInOneRaw[k].Split(' ')[0];
                        }
                        else
                        {
                            _trainNumber = AllTrainNumberInOneRaw[k];
                        }
                        CommandModel model = new CommandModel();
                        model.trainNumber = _trainNumber;
                        if (!IsTrainNumber(model.trainNumber))
                        {
                            model.trainNumber = "未识别-(" + model.trainNumber + ")";
                        }
                        else
                        {
                            int outNum = 0;
                            int.TryParse(model.trainNumber.ToCharArray()[model.trainNumber.ToCharArray().Length - 1].ToString(), out outNum);
                            if (outNum % 2 == 0)
                            {//上行
                                model.upOrDown = 0;
                            }
                            else
                            {//下行
                                model.upOrDown = 1;
                            }
                        }
                        model.secondTrainNumber = "null";
                        model.streamStatus = streamStatus;
                        model.trainType = trainType;
                        model.trainModel = trainModel;
                        model.trainConnectType = trainConnectType;
                        model.trainIndex = index;
                        model.trainId = trainId;

                        AllModels.Add(model);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return AllModels;
        }


        //使用NPOI进行Excel操作
        private void SelectPath()
        {
            ExcelFile = new List<string>();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();   //显示选择文件对话框 
            openFileDialog1.Filter = "Excel 文件 |*.xlsx;*.xls";
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\" + startPath + "\\";
            //openFileDialog1.Filter = "Excel 2003 文件 (*.xls)|*.xls";
            if (radioButton1.Checked)
            {
                openFileDialog1.Multiselect = true;
            }
            else
            {
                openFileDialog1.Multiselect = false;
            }
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int fileCount = 0;
                String fileNames = "已选择：";
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    fileCount++;
                    ExcelFile.Add(fileName);
                }

                if (radioButton1.Checked)
                {
                    this.filePathLBL.Text = "已选择：" + fileCount + "个文件";
                }
                else
                {
                    this.filePathLBL.Text = "已选择：" + openFileDialog1.FileName;     //显示文件路径 
                }
                hasFilePath = true;
                startBtnCheck();
            }
        }

        private void updateTimeTable()
        {
            foreach (string fileName in ExcelFile)
            {
                IWorkbook workbook = null;  //新建IWorkbook对象 
                                            //车次统计
                int allTrainsCount = 0;
                int allPsngerTrainsCount = 0;
                int stoppedTrainsCount = 0;
                int allTrainsInTimeTable = 0;
                string checkedText = "";
                try
                {
                    FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                    {
                        try
                        {
                            workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                        }
                        catch (Exception e)
                        {
                            if (File.Exists(Application.StartupPath + "\\时刻表\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1]))
                            {
                                MessageBox.Show("时刻表文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                string pLocalFilePath = Application.StartupPath + "\\时刻表\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//要复制的文件路径
                                string pSaveFilePath = fileName;//指定存储的路径
                                File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                                workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
                            }
                            else
                            {
                                MessageBox.Show("时刻表文件出现损坏（或时刻表无效）\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\时刻表\\");
                                return;
                            }
                        }

                    }
                    else if (fileName.IndexOf(".xls") > 0) // 2003版本  
                    {
                        try
                        {
                            workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
                        }
                        catch (Exception e)
                        {
                            if (File.Exists(Application.StartupPath + "\\时刻表\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1]))
                            {
                                MessageBox.Show("时刻表文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                string pLocalFilePath = Application.StartupPath + "\\时刻表\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//要复制的文件路径
                                string pSaveFilePath = fileName;//指定存储的路径
                                File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                                workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
                            }
                            else
                            {
                                MessageBox.Show("时刻表文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\时刻表\\");
                                return;
                            }
                        }
                    }

                    if (workbook != null && !fileName.Contains("自动备份-"))
                    {
                        string pLocalFilePath = fileName.ToString();//要复制的文件路径
                        string pSaveFilePath = Application.StartupPath + "\\时刻表\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//指定存储的路径
                        File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换

                    }

                    //表格样式
                    ICellStyle stoppedTrainStyle = workbook.CreateCellStyle();
                    stoppedTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                    stoppedTrainStyle.FillPattern = FillPattern.SolidForeground;
                    stoppedTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                    stoppedTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    stoppedTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    stoppedTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    stoppedTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    HSSFFont font = (HSSFFont)workbook.CreateFont();
                    font.FontName = "宋体";//字体  
                    font.FontHeightInPoints = short.Parse(fontSize.ToString());//字号  
                    font.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
                    stoppedTrainStyle.SetFont(font);

                    ICellStyle normalTrainStyle = workbook.CreateCellStyle();
                    normalTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                    normalTrainStyle.FillPattern = FillPattern.SolidForeground;
                    normalTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                    normalTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    normalTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    normalTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    normalTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    HSSFFont normalFont = (HSSFFont)workbook.CreateFont();
                    normalFont.FontName = "宋体";//字体  
                    normalFont.FontHeightInPoints = short.Parse(fontSize.ToString());//字号  
                    normalFont.IsBold = true;
                    normalTrainStyle.SetFont(normalFont);

                    ICellStyle tomorrowlTrainStyle = workbook.CreateCellStyle();
                    tomorrowlTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                    tomorrowlTrainStyle.FillPattern = FillPattern.SolidForeground;
                    tomorrowlTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                    tomorrowlTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    tomorrowlTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    tomorrowlTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    tomorrowlTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    tomorrowlTrainStyle.SetFont(normalFont);

                    ICellStyle removeColors = workbook.CreateCellStyle();
                    removeColors.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                    removeColors.FillPattern = FillPattern.SolidForeground;
                    removeColors.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                    removeColors.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    removeColors.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    removeColors.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    removeColors.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

                    ICellStyle addedTrainStyle = workbook.CreateCellStyle();
                    addedTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                    addedTrainStyle.FillPattern = FillPattern.SolidForeground;
                    addedTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                    addedTrainStyle.WrapText = true;
                    addedTrainStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直

                    HSSFFont addFont = (HSSFFont)workbook.CreateFont();
                    addFont.FontName = "宋体";//字体  
                    addFont.FontHeightInPoints = 12;//字号  
                    addFont.IsBold = false;
                    addedTrainStyle.SetFont(addFont);

                    ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  
                    IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  
                    if (sheet.GetRow(0) == null)
                    {
                        sheet.CreateRow(0);
                    }
                    if (sheet.GetRow(0).GetCell(0) == null)
                    {
                        sheet.GetRow(0).CreateCell(0);
                    }
                    string title = sheet.GetRow(0).GetCell(0).ToString().Trim();
                    if (title.Contains("-"))
                    {
                        title = title.Split('-')[1];
                    }
                    int hour = -1;
                    int.TryParse(DateTime.Now.ToString("HH"), out hour);
                    if (hour >= 0 && hour <= 16)
                    {
                        title = DateTime.Now.ToString("yyyy年MM月dd日-") + title;
                    }
                    else
                    {
                        title = DateTime.Now.AddDays(1).ToString("yyyy年MM月dd日-") + title;
                    }
                    sheet.GetRow(0).GetCell(0).SetCellValue(title);
                    for (int i = 0; i <= sheet.LastRowNum; i++)  //对工作表每一行  
                    {
                        row = sheet.GetRow(i);   //row读入第i行数据  
                        if (row != null)
                        {
                            if (row.GetCell(0) != null)
                            {
                                if (row.GetCell(0).ToString().Contains("加开车次") && !sheet.GetRow(0).GetCell(0).ToString().Contains("城际"))
                                {
                                    IRow addedRow;
                                    if (sheet.GetRow(i + 1) == null)
                                    {
                                        sheet.CreateRow(i + 1);
                                    }
                                    addedRow = sheet.GetRow(i + 1);
                                    if (addedRow.GetCell(0) == null)
                                    {
                                        addedRow.CreateCell(0);
                                    }
                                    addedRow.GetCell(0).CellStyle = addedTrainStyle;
                                    addedRow.GetCell(0).SetCellValue(addedTrainText);
                                }
                            }
                            for (int j = 0; j <= row.LastCellNum; j++)  //对工作表每一列  
                            {
                                if (row.GetCell(j) != null)
                                {
                                    if (row.GetCell(j).ToString().Contains("G") ||
                                        row.GetCell(j).ToString().Contains("D") ||
                                        row.GetCell(j).ToString().Contains("C") ||
                                        row.GetCell(j).ToString().Contains("J"))
                                    {//把车次表格先刷白去字
                                        if (!row.GetCell(j).ToString().Contains("由") &&
                                            !row.GetCell(j).ToString().Contains("续") &&
                                            !row.GetCell(j).ToString().Contains("开行"))
                                        {
                                            //时刻表中车次+1
                                            allTrainsInTimeTable++;
                                            //去中文后再找-去掉高峰-周末-临客等字
                                            row.GetCell(j).CellStyle = removeColors;
                                            row.GetCell(j).SetCellValue(Regex.Replace(row.GetCell(j).ToString().Replace("√", "").Replace("×", "").Replace("(", "").Replace(")", ""), @"[\u4e00-\u9fa5]", ""));
                                        }
                                        else
                                        {
                                            //这个格子不是要找的
                                            continue;
                                        }
                                        //若遍历后都没有找到 停运+1
                                        bool ContainsTrainNumber = false;
                                        foreach (CommandModel model in commandModel)
                                        {//根据客调命令刷单元格颜色
                                            if (row.GetCell(j).ToString().Trim().Equals(model.trainNumber) ||
                                                row.GetCell(j).ToString().Trim().Equals(model.secondTrainNumber))
                                            {
                                                ContainsTrainNumber = true;
                                                //车次统计+1
                                                allTrainsCount++;
                                                if (!row.GetCell(j).ToString().Trim().Contains("0G") &&
                                                    !row.GetCell(j).ToString().Trim().Contains("0D") &&
                                                    !row.GetCell(j).ToString().Trim().Contains("0J") &&
                                                    !row.GetCell(j).ToString().Trim().Contains("0C") &&
                                                    !row.GetCell(j).ToString().Trim().Contains("00") &&
                                                    !row.GetCell(j).ToString().Trim().Contains("DJ"))
                                                {
                                                    allPsngerTrainsCount++;
                                                }
                                                if (model.trainType == 1)
                                                {
                                                    row.GetCell(j).SetCellValue("高峰" + row.GetCell(j).ToString().Trim());
                                                }
                                                else if (model.trainType == 2)
                                                {
                                                    row.GetCell(j).SetCellValue("临客" + row.GetCell(j).ToString().Trim());
                                                }
                                                else if (model.trainType == 3)
                                                {
                                                    row.GetCell(j).SetCellValue("周末" + row.GetCell(j).ToString().Trim());
                                                }
                                                if (model.streamStatus == 1)
                                                {
                                                    row.GetCell(j).SetCellValue("√" + row.GetCell(j).ToString().Trim());
                                                    row.GetCell(j).CellStyle = normalTrainStyle;
                                                }
                                                else if (model.streamStatus == 0)
                                                {
                                                    row.GetCell(j).SetCellValue("×" + row.GetCell(j).ToString().Trim());
                                                    stoppedTrainsCount++;
                                                    row.GetCell(j).CellStyle = stoppedTrainStyle;
                                                }
                                                else if (model.streamStatus == 2)
                                                {
                                                    row.GetCell(j).SetCellValue("√" + row.GetCell(j).ToString().Trim());
                                                    row.GetCell(j).CellStyle = tomorrowlTrainStyle;
                                                }

                                            }
                                        }
                                        if (!ContainsTrainNumber)
                                        {//查错
                                            string trainNum = row.GetCell(j).ToString().Trim();
                                            //单车号
                                            bool gotIt = false;
                                            if (commandText.Contains(trainNum))
                                            {
                                                //智能纠错
                                                checkedText = checkedText + " " + trainNum;
                                                int status = searchAndHightlightUnresolvedTrains(trainNum,0);
                                                if (status == 1)
                                                {
                                                    row.GetCell(j).SetCellValue("√" + row.GetCell(j).ToString().Trim());
                                                    row.GetCell(j).CellStyle = normalTrainStyle;
                                                }
                                                else if(status == 0)
                                                {
                                                    row.GetCell(j).SetCellValue("×" + row.GetCell(j).ToString().Trim());
                                                    stoppedTrainsCount++;
                                                    row.GetCell(j).CellStyle = stoppedTrainStyle;
                                                }
                                                gotIt = true;
                                                if (status == -1)
                                                {//如果选择发现找到的都不是这个车
                                                    gotIt = false;
                                                }

                                            }
                                            //双车号
                                            if (!gotIt)
                                            {
                                                string splitedNumber = "";
                                                int originalTrainNumber = 0;
                                                string trainType = "";
                                                string targetString = "";
                                                if (trainNum.Contains("G"))
                                                {
                                                    trainType = "G";
                                                }
                                                else if (trainNum.Contains("D"))
                                                {
                                                    trainType = "D";
                                                }
                                                else if (trainNum.Contains("C"))
                                                {
                                                    trainType = "C";
                                                }

                                                foreach (char item in trainNum)
                                                {
                                                    if (item >= 48 && item <= 58)
                                                    {
                                                        splitedNumber += item;
                                                    }
                                                }
                                                int.TryParse(splitedNumber, out originalTrainNumber);
                                                string targetTrainNum = "";
                                                if (originalTrainNumber != 0)
                                                {
                                                    bool hasGotIt = false;
                                                    for (int ij = 0; ij < 4; ij++)
                                                    {//+1 -1 +3 -3分别试一遍(试该车次的第二个车号)
                                                        if (hasGotIt)
                                                        {
                                                            break;
                                                        }
                                                        switch (ij)
                                                        {
                                                            case 0:
                                                                targetTrainNum = trainType + (originalTrainNumber + 1).ToString() + "/";
                                                                break;
                                                            case 1:
                                                                targetTrainNum = trainType + (originalTrainNumber - 1).ToString() + "/";
                                                                break;
                                                            case 2:
                                                                targetTrainNum = trainType + (originalTrainNumber + 3).ToString() + "/";
                                                                break;
                                                            case 3:
                                                                targetTrainNum = trainType + (originalTrainNumber - 3).ToString() + "/";
                                                                break;
                                                        }
                                                        for (int index = 0; index < trainNum.Length; index++)
                                                        {
                                                            if (trainNum[index] != targetTrainNum[index])
                                                            {
                                                                targetTrainNum = targetTrainNum + trainNum[index];
                                                            }
                                                        }
                                                        if (commandText.Contains(targetTrainNum))
                                                        {
                                                            //智能纠错
                                                            checkedText = checkedText + " " + targetTrainNum;
                                                            int status = searchAndHightlightUnresolvedTrains(targetTrainNum,0);
                                                            if (status == 1)
                                                            {
                                                                row.GetCell(j).SetCellValue("√" + row.GetCell(j).ToString().Trim());
                                                                row.GetCell(j).CellStyle = normalTrainStyle;
                                                            }
                                                            else
                                                            {
                                                                row.GetCell(j).SetCellValue("×" + row.GetCell(j).ToString().Trim());
                                                                stoppedTrainsCount++;
                                                                row.GetCell(j).CellStyle = stoppedTrainStyle;
                                                            }
                                                            gotIt = true;
                                                            hasGotIt = true;
                                                        }
                                                    }
                                                }
                                            }
                                            if (!gotIt)
                                            {
                                                row.GetCell(j).SetCellValue("×" + row.GetCell(j).ToString().Trim());
                                                row.GetCell(j).CellStyle = stoppedTrainStyle;
                                                stoppedTrainsCount++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    /*重新修改文件指定单元格样式*/
                    FileStream fs1 = File.OpenWrite(fileName);
                    workbook.Write(fs1);
                    fs1.Close();
                    fileStream.Close();
                    workbook.Close();
                    //显示车次总数
                    AllTrainsCountLBL.Text = allTrainsCount.ToString();
                    AllPsngerTrainsCountLBL.Text = allPsngerTrainsCount.ToString();
                    stoppedTrainsCountLBL.Text = stoppedTrainsCount.ToString();
                    AllTrainsInTimeTableLBL.Text = allTrainsInTimeTable.ToString();
                    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
                    //info.WorkingDirectory = Application.StartupPath;
                    info.FileName = fileName;
                    info.Arguments = "";
                    if (checkedText.Length != 0)
                    {
                        //MessageBox.Show("请人工核对以下车次（时刻表内有标注）：\n" + checkedText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        try
                        {
                            FileStream file = new FileStream(Application.StartupPath + "\\" + "ErrorLog-" + DateTime.Now.ToString("yyyyMMdd") + ".txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                            StreamWriter writer = new StreamWriter(file);
                            writer.WriteLine("车次：" + checkedText + "\n\n" + command_rTb.Text);
                            writer.Close();
                            file.Close();
                        }
                        catch (Exception _e)
                        {

                        }
                    }
                    try
                    {
                        System.Diagnostics.Process.Start(info);
                    }
                    catch (System.ComponentModel.Win32Exception we)
                    {
                        MessageBox.Show(this, we.Message);
                        return;
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("选中的部分时刻表文件正在使用中，请关闭后重试\n" + fileName, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void checkedChanged()
        {
            if (radioButton1.Checked)
            {
                EMUorEMUC_groupBox.Visible = false;
                label111.Visible = true;
                label222.Visible = true;
                rightGroupBox.Visible = true;
                rightGroupBox_Compare.Visible = false;
                FontSize_tb.Visible = true;
                modeSelect = 1;
                startPath = "时刻表";
                secondStepText_lbl.Text = "2.选择时刻表文件（----支持多选----）";
                start_Btn.Text = "处理时刻表";
                ExcelFile = new List<string>();
                start_Btn.Enabled = false;
                filePath = "";
                filePathLBL.Text = "已选择：";
                Size _size = new Size(210, 283);
                outputTB.Size = _size;
                searchResult_tb.Size = _size;
                hint_label.Text = "绿色为开行，红色为停开，白色为调令未含车次，黄色为次日接入车次。高峰/临客/周末在车次前含有标注";
            }
            else if (radioButton2.Checked)
            {
                EMUorEMUC_groupBox.Visible = true;
                radioButton4.Checked = true;
                label111.Visible = false;
                label222.Visible = false;
                FontSize_tb.Visible = false;
                rightGroupBox.Visible = false;
                rightGroupBox_Compare.Visible = true;
                modeSelect = 2;
                startPath = "基本图";
                secondStepText_lbl.Text = "2.选择基本图文件";
                start_Btn.Text = "创建班计划";
                ExcelFile = new List<string>();
                start_Btn.Enabled = false;
                filePath = "";
                filePathLBL.Text = "已选择：";
                Size _size = new Size(210, 393);
                outputTB.Size = _size;
                searchResult_tb.Size = _size;
                hint_label.Text = "基本图中没有的车次不会显示！无序号白色为客调令多出车次，红色标注为客调停开车次。请进行人工核对。";
            }
            else if (radioButton3.Checked)
            {
                EMUorEMUC_groupBox.Visible = false;
                label111.Visible = false;
                label222.Visible = false;
                FontSize_tb.Visible = false;
                rightGroupBox.Visible = true;
                rightGroupBox_Compare.Visible = false;
                modeSelect = 3;
                startPath = "动车所时刻表";
                secondStepText_lbl.Text = "2.选择动车所时刻表";
                start_Btn.Text = "补全车辆编组";
                ExcelFile = new List<string>();
                filePathLBL.Text = "已选择：";
                Size _size = new Size(210, 393);
                outputTB.Size = _size;
                searchResult_tb.Size = _size;
                hint_label.Text = "";
            }
        }

        //读基本图-存模型
        private void readBasicTrainTable(bool isComparing = false)
        {
            string fileName = ExcelFile[0];
            IWorkbook workbook = null;  //新建IWorkbook对象  
            basicTrainGraphTitle titleInfo = new basicTrainGraphTitle();
            List<DailySchedule> _dailyScheduleModel = new List<DailySchedule>();
            /*
            try
            */
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                {
                    try
                    {
                        workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                    }
                    catch (Exception e)
                    {
                        if (File.Exists(Application.StartupPath + "\\基本图\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1]))
                        {
                            MessageBox.Show("基本图文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            string pLocalFilePath = Application.StartupPath + "\\基本图\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//要复制的文件路径
                            string pSaveFilePath = fileName;//指定存储的路径
                            File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                            fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                            workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
                        }
                        else
                        {
                            MessageBox.Show("基本图文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\基本图\\");
                            return;
                        }
                    }

                }
                else if (fileName.IndexOf(".xls") > 0) // 2003版本  
                {
                    try
                    {
                        workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
                    }
                    catch (Exception e)
                    {
                        if (File.Exists(Application.StartupPath + "\\基本图\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1]))
                        {
                            MessageBox.Show("基本图文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            string pLocalFilePath = Application.StartupPath + "\\基本图\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//要复制的文件路径
                            string pSaveFilePath = fileName;//指定存储的路径
                            File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                            fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                            workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
                        }
                        else
                        {
                            MessageBox.Show("基本图文件出现损坏（或文件无效）\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\基本图\\");
                            return;
                        }
                    }
                }

                if (workbook != null && !fileName.Contains("自动备份-"))
                {
                    string pLocalFilePath = fileName.ToString();//要复制的文件路径
                    string pSaveFilePath = Application.StartupPath + "\\基本图\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//指定存储的路径
                    File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换

                }

                //找表头
                ISheet sheet1 = workbook.GetSheetAt(0);
                List<int> titleRow = new List<int>();
                for (int i = 0; i <= sheet1.LastRowNum; i++)
                {
                    IRow row = sheet1.GetRow(i);
                    //short format = row.GetCell(0).CellStyle.DataFormat;
                    if (row != null)
                    {
                        if (row.GetCell(0) != null)
                        {
                            if (row.GetCell(0).ToString().Contains("序号") ||
                                row.GetCell(0).ToString().Contains("预售"))
                            {
                                titleRow.Add(i);
                                for (int j = 0; j <= row.LastCellNum; j++)
                                {

                                    if (row.GetCell(j) != null)
                                    {
                                        string titleText = row.GetCell(j).ToString();
                                        if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("序号"))
                                        {
                                            titleInfo.idColumn = j;
                                        }
                                        if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("车次"))
                                        {
                                            titleInfo.trainNumColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("始发站"))
                                        {
                                            titleInfo.startStationColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("终到站"))
                                        {
                                            titleInfo.stopStationColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("到时"))
                                        {
                                            titleInfo.stopTimeColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("开时"))
                                        {
                                            titleInfo.startTimeColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("停时"))
                                        {
                                            titleInfo.stopToStartTimeCountColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("股道"))
                                        {
                                            titleInfo.trackNumColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("编组"))
                                        {
                                            titleInfo.trainConnectTypeColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("车型"))
                                        {
                                            titleInfo.trainModelColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("担当"))
                                        {
                                            titleInfo.trainBelongsToColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("新旧"))
                                        {
                                            titleInfo.tipsColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("定员"))
                                        {
                                            titleInfo.ratedSeatsColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("备注"))
                                        {
                                            titleInfo.extraTextColumn = j;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                titleInfo.titleRow = titleRow;

                //找数据
                for (int i = 0; i < titleInfo.titleRow.Count; i++)
                {
                    int lastRow = sheet1.LastRowNum;
                    if (i < titleInfo.titleRow.Count - 1)
                    {
                        lastRow = titleInfo.titleRow[i + 1];
                    }
                    for (int j = titleInfo.titleRow[i]; j < lastRow; j++)
                    {
                        IRow _readingRow = sheet1.GetRow(j);
                        if (_readingRow != null)
                        {
                            DailySchedule tempModel = new DailySchedule();
                            if (_readingRow.GetCell(titleInfo.idColumn) != null)
                            {//ID
                                int id = -1;
                                int.TryParse(_readingRow.GetCell(titleInfo.idColumn).ToString(), out id);
                                if (id != -1)
                                {
                                    tempModel.id = id;
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.trainNumColumn) != null && titleInfo.trainNumColumn != 0)
                            {//车次
                                if (_readingRow.GetCell(titleInfo.trainNumColumn).ToString().Length != 0)
                                {
                                    tempModel.trainNumber = _readingRow.GetCell(titleInfo.trainNumColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.startStationColumn) != null && titleInfo.startStationColumn != 0)
                            {//始发站
                                if (_readingRow.GetCell(titleInfo.startStationColumn).ToString().Length != 0)
                                {
                                    tempModel.startStation = _readingRow.GetCell(titleInfo.startStationColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.stopStationColumn) != null && titleInfo.stopStationColumn != 0)
                            {//终到站
                                if (_readingRow.GetCell(titleInfo.stopStationColumn).ToString().Length != 0)
                                {
                                    tempModel.stopStation = _readingRow.GetCell(titleInfo.stopStationColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.stopTimeColumn) != null && titleInfo.stopTimeColumn != 0)
                            {//到时
                                if (_readingRow.GetCell(titleInfo.stopTimeColumn).ToString().Length != 0)
                                {
                                    tempModel.stopTime = _readingRow.GetCell(titleInfo.stopTimeColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.startTimeColumn) != null && titleInfo.startTimeColumn != 0)
                            {//发时
                                if (_readingRow.GetCell(titleInfo.startTimeColumn).ToString().Length != 0)
                                {
                                    tempModel.startTime = _readingRow.GetCell(titleInfo.startTimeColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.stopToStartTimeCountColumn) != null && titleInfo.stopToStartTimeCountColumn != 0)
                            {//停时
                                if (_readingRow.GetCell(titleInfo.stopToStartTimeCountColumn).ToString().Length != 0)
                                {
                                    tempModel.stopToStartTime = _readingRow.GetCell(titleInfo.stopToStartTimeCountColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.trackNumColumn) != null && titleInfo.trackNumColumn != 0)
                            {//股道
                                if (_readingRow.GetCell(titleInfo.trackNumColumn).ToString().Length != 0)
                                {
                                    tempModel.trackNum = _readingRow.GetCell(titleInfo.trackNumColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.trainConnectTypeColumn) != null && titleInfo.trainConnectTypeColumn != 0)
                            {//编组
                                if (_readingRow.GetCell(titleInfo.trainConnectTypeColumn).ToString().Length != 0)
                                {
                                    tempModel.trainConnectType = _readingRow.GetCell(titleInfo.trainConnectTypeColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.trainModelColumn) != null && titleInfo.trainModelColumn != 0)
                            {//车型
                                if (_readingRow.GetCell(titleInfo.trainModelColumn).ToString().Length != 0)
                                {
                                    tempModel.trainModel = _readingRow.GetCell(titleInfo.trainModelColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.trainBelongsToColumn) != null && titleInfo.trainBelongsToColumn != 0)
                            {//担当
                                if (_readingRow.GetCell(titleInfo.trainBelongsToColumn).ToString().Length != 0)
                                {
                                    tempModel.trainBelongsTo = _readingRow.GetCell(titleInfo.trainBelongsToColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.ratedSeatsColumn) != null && titleInfo.ratedSeatsColumn != 0)
                            {//定员
                                if (_readingRow.GetCell(titleInfo.ratedSeatsColumn).ToString().Length != 0)
                                {
                                    tempModel.ratedSeats = _readingRow.GetCell(titleInfo.ratedSeatsColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.tipsColumn) != null && titleInfo.tipsColumn != 0)
                            {//新旧交替
                                if (_readingRow.GetCell(titleInfo.tipsColumn).ToString().Length != 0)
                                {
                                    tempModel.tipsText = _readingRow.GetCell(titleInfo.tipsColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(titleInfo.extraTextColumn) != null && titleInfo.extraTextColumn != 0)
                            {//新旧交替
                                if (_readingRow.GetCell(titleInfo.extraTextColumn).ToString().Length != 0)
                                {
                                    tempModel.extraText = _readingRow.GetCell(titleInfo.extraTextColumn).ToString();
                                }
                                if (i == 1)
                                {
                                    tempModel.extraText = tempModel.extraText + " 仅供司机换乘";
                                }

                            }
                            if (tempModel.id != 0)
                            {
                                _dailyScheduleModel.Add(tempModel);
                            }
                        }
                    }
                }
                //验错
                detectedCModel = new List<CommandModel>();
                string unresolvedTrains = checkCommandModelWithDailySchedule(_dailyScheduleModel,null, commandModel, commandText);
                if (unresolvedTrains.Length != 0)
                {
                    MessageBox.Show("请核对以下未识别车次：\n" + unresolvedTrains + "\n位于->今日客调命令(←已标红)。\n", "人工核对", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //下一步-处理数据
                if (!isComparing)
                {
                    analyzeDailyScheduleData(_dailyScheduleModel);
                }
                else
                {
                    //查昨日错
                    string yesterdayUnresolvedTrains = checkCommandModelWithDailySchedule(_dailyScheduleModel,null, yesterdayCommandModel, yesterdayCommandText, true);
                    if (yesterdayUnresolvedTrains.Length != 0)
                    {
                        MessageBox.Show("请核对以下未识别车次：\n" + yesterdayUnresolvedTrains + "\n位于->昨日客调命令(↑已标红)。\n", "人工核对", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    compareWithYesterday(_dailyScheduleModel,null);
                }

            }
            /*
            catch (Exception e)
            {
                MessageBox.Show("请确认是否选择了正确的班计划文件~\n" + "错误内容："+e.Message.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            */
        }

        //读动检车图-存模型
        private void readEMUCTable(bool isComparing = false)
        {
            //以下是表头所在列and标题行
            //序号
            int idColumn = -1;
            //备注
            int extraColumn = -1;
            //始发站
            int startStationColumn = -1;
            //车次
            int trainNumberColumn = -1;
            //到时
            int stopTimeColumn = -1;
            //股道
            int trackNumColumn = -1;
            //开时
            int startTimeColumn = -1;
            //终到站
            int destinationColumn = -1;
            int titleRow = -1;
            try { 
            string fileName = ExcelFile[0];
            IWorkbook workbook = null;  //新建IWorkbook对象  
            List<EMUCheckModel> _eMUCheckModel = new List<EMUCheckModel>();
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
            {
                try
                {
                    workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                }
                catch (Exception e)
                {
                    if (File.Exists(Application.StartupPath + "\\基本图\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1]))
                    {
                        MessageBox.Show("基本图文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        string pLocalFilePath = Application.StartupPath + "\\基本图\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//要复制的文件路径
                        string pSaveFilePath = fileName;//指定存储的路径
                        File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                        fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                        workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
                    }
                    else
                    {
                        MessageBox.Show("基本图文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\基本图\\");
                        return;
                    }
                }

            }
            else if (fileName.IndexOf(".xls") > 0) // 2003版本  
            {
                try
                {
                    workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
                }
                catch (Exception e)
                {
                    if (File.Exists(Application.StartupPath + "\\基本图\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1]))
                    {
                        MessageBox.Show("基本图文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        string pLocalFilePath = Application.StartupPath + "\\基本图\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//要复制的文件路径
                        string pSaveFilePath = fileName;//指定存储的路径
                        File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                        fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                        workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
                    }
                    else
                    {
                        MessageBox.Show("基本图文件出现损坏（或文件无效）\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\基本图\\");
                        return;
                    }
                }
            }

            if (workbook != null && !fileName.Contains("自动备份-"))
            {
                string pLocalFilePath = fileName.ToString();//要复制的文件路径
                string pSaveFilePath = Application.StartupPath + "\\基本图\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//指定存储的路径
                File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换

            }

            //找表头
            ISheet sheet1 = workbook.GetSheetAt(0);
            for (int i = 0; i <= sheet1.LastRowNum; i++)
            {
                IRow row = sheet1.GetRow(i);
                //short format = row.GetCell(0).CellStyle.DataFormat;
                if (row != null)
                {
                    if (row.GetCell(0) != null)
                    {
                        if (row.GetCell(0).ToString().Contains("序号") ||
                            row.GetCell(0).ToString().Contains("车次"))
                        {
                            titleRow = i;
                            for (int j = 0; j <= row.LastCellNum; j++)
                            {

                                if (row.GetCell(j) != null)
                                {
                                    string titleText = row.GetCell(j).ToString();
                                    if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("序号"))
                                    {
                                        idColumn = j;
                                    }
                                    if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("备注"))
                                    {
                                        extraColumn = j;
                                    }
                                    else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("始发"))
                                    {
                                        startStationColumn = j;
                                    }
                                    else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("车次"))
                                    {
                                        trainNumberColumn = j;
                                    }
                                    else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("到时"))
                                    {
                                        stopTimeColumn = j;
                                    }
                                    else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("股道"))
                                    {
                                        trackNumColumn = j;
                                    }
                                    else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("开时"))
                                    {
                                        startTimeColumn = j;
                                    }
                                    else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("终到"))
                                    {
                                        destinationColumn = j;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (titleRow == -1)
            {
                MessageBox.Show("未能识别动检全图文件，请确保选择了正确的文件，并且表格内拥有每一列的标题（序号，车次）等","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            //找数据
            int lastRow = sheet1.LastRowNum;
            for (int j = titleRow; j < lastRow; j++)
            {
                IRow _readingRow = sheet1.GetRow(j);
                if (_readingRow != null)
                {
                    EMUCheckModel _tempModel = new EMUCheckModel();
                    if (_readingRow.GetCell(idColumn) != null)
                    {//ID
                        int id = -1;
                        int.TryParse(_readingRow.GetCell(idColumn).ToString(), out id);
                        if (id != -1)
                        {
                            _tempModel.id = id;
                        }
                    }
                    if (_readingRow.GetCell(trainNumberColumn) != null && trainNumberColumn != 0)
                    {//车次
                        if (_readingRow.GetCell(trainNumberColumn).ToString().Length != 0)
                        {
                            _tempModel.trainNumber = _readingRow.GetCell(trainNumberColumn).ToString();
                        }
                    }
                    if (_readingRow.GetCell(startStationColumn) != null && startStationColumn != 0)
                    {//始发站
                        if (_readingRow.GetCell(startStationColumn).ToString().Length != 0)
                        {
                            _tempModel.startStation = _readingRow.GetCell(startStationColumn).ToString();
                        }
                    }
                    if (_readingRow.GetCell(destinationColumn) != null && destinationColumn != 0)
                    {//终到站
                        if (_readingRow.GetCell(destinationColumn).ToString().Length != 0)
                        {
                            _tempModel.destination = _readingRow.GetCell(destinationColumn).ToString();
                        }
                    }
                    if (_readingRow.GetCell(stopTimeColumn) != null && stopTimeColumn != 0)
                    {//到时
                        if (_readingRow.GetCell(stopTimeColumn).ToString().Length != 0)
                        {
                            _tempModel.stopTime = _readingRow.GetCell(stopTimeColumn).ToString();
                        }
                    }
                    if (_readingRow.GetCell(startTimeColumn) != null && startTimeColumn != 0)
                    {//发时
                        if (_readingRow.GetCell(startTimeColumn).ToString().Length != 0)
                        {
                            _tempModel.startTime = _readingRow.GetCell(startTimeColumn).ToString();
                        }
                    }
                    if (_readingRow.GetCell(trackNumColumn) != null && trackNumColumn != 0)
                    {//股道
                        if (_readingRow.GetCell(trackNumColumn).ToString().Length != 0)
                        {
                            _tempModel.trackNum = _readingRow.GetCell(trackNumColumn).ToString();
                        }
                    }
                    if (_readingRow.GetCell(extraColumn) != null && extraColumn != 0)
                    {//备注
                        if (_readingRow.GetCell(extraColumn).ToString().Length != 0)
                        {
                            _tempModel.extra = _readingRow.GetCell(extraColumn).ToString();
                        }
                    }
                    if (_tempModel.id != 0)
                    {
                        _eMUCheckModel.Add(_tempModel);
                    }
                }
            }
            //验错
            detectedCModel = new List<CommandModel>();
            string unresolvedTrains = checkCommandModelWithDailySchedule(null, _eMUCheckModel, commandModel, commandText);
            if (unresolvedTrains.Length != 0)
            {
                MessageBox.Show("请核对以下未识别车次：\n" + unresolvedTrains + "\n位于->今日客调命令(←已标红)。\n未识别车次暂时未添加", "人工核对", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //下一步-处理数据
            if (!isComparing)
            {
                analyzeEMUC_Data(_eMUCheckModel);
            }
            else
            {
                //查昨日错
                string yesterdayUnresolvedTrains = checkCommandModelWithDailySchedule(null, _eMUCheckModel, yesterdayCommandModel, yesterdayCommandText, true);
                if (yesterdayUnresolvedTrains.Length != 0)
                {
                    MessageBox.Show("请核对以下未识别车次：\n" + yesterdayUnresolvedTrains + "\n位于->昨日客调命令(↑已标红)。\n未识别车次暂时未添加", "人工核对", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                compareWithYesterday(null,_eMUCheckModel);
            }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

    }

        //下面三个方法合并起来是班计划-动检车查错用的
        private int searchAndHightlightUnresolvedTrains(string find, int type,bool isYesterDay = false,string secondTrainNumber = "")
        {//找到未识别车并高亮显示(返回0-停开，1-开行，-1-未找到，2-综控-一整行车都没有23333)-行车，东所
            //在模型内添加新车-综控
            //type 0 1 2行车综控东所
            int index = 0;
            if (!isYesterDay)
            {
                index = command_rTb.Find(find, RichTextBoxFinds.WholeWord);//调用find方法，并设置区分全字匹配
            }
            else
            {
                index = yesterdayCommand_rtb.Find(find, RichTextBoxFinds.WholeWord);//调用find方法，并设置区分全字匹配
            }
            int startPos = index;
            int nextIndex = 0;
            while (nextIndex != startPos)//循环查找字符串，并用红色加粗12号Times New Roman标记之
            {
                if (!isYesterDay)
                {
                    if (index == -1)
                    {
                        break;
                    }
                    command_rTb.SelectionStart = index;
                    command_rTb.SelectionLength = find.Length;
                    command_rTb.SelectionColor = Color.Red;
                    command_rTb.SelectionFont = new Font("Times New Roman", (float)12, FontStyle.Bold);
                    command_rTb.Focus();
                    DialogResult result = MessageBox.Show("请人工核对\n" + find + "次是否为当前标红内容？\n(*请注意核查同一条命令内其他车次是否正确)", "人工核对", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        if (type == 0)
                        {//行车
                            DialogResult resultTrainStatus = MessageBox.Show(find + "次在客调命令中是否开行？（开行选择“是”，停运/待定等选择“否”）", "人工核对", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (resultTrainStatus == DialogResult.Yes)
                            {
                                return 1;
                            }
                            else if (resultTrainStatus == DialogResult.No)
                            {
                                return 0;
                            }
                        }
                        else if (type == 1)
                        {//把命令分行，找车次所在行有没有其他车次，找对应车次车号赋给新车，然后再选择停运情况
                            string currentRow = "";
                            string[] _aCommands = removeUnuseableWord(false)[0].Split('。');
                            /*
                            int indexCount = 0;
                            for (int i = 0; i < _aCommands.Length; i++)
                            {
                                indexCount = indexCount + _aCommands[i].Length;
                                if (indexCount > index)
                                {
                                    currentRow = _aCommands[i];
                                    break;
                                }
                            }
                            */
                            for(int i = 0; i < _aCommands.Length; i++)
                            {//笨方法
                                if(_aCommands[i].Contains(find)&&
                                    !_aCommands[i].Contains(find + "0") &&
                                    !_aCommands[i].Contains(find + "1") &&
                                    !_aCommands[i].Contains(find + "2") &&
                                    !_aCommands[i].Contains(find + "3") &&
                                    !_aCommands[i].Contains(find + "4") &&
                                    !_aCommands[i].Contains(find + "5") &&
                                    !_aCommands[i].Contains(find + "6") &&
                                    !_aCommands[i].Contains(find + "7") &&
                                    !_aCommands[i].Contains(find + "8") &&
                                    !_aCommands[i].Contains(find + "9") &&
                                    (_aCommands[i].Contains("CR")|| _aCommands[i].Contains("null")))
                                {
                                    currentRow = _aCommands[i];
                                    break;
                                }
                            }
                            if(currentRow.Length != 0)
                            {
                                analyseCommand(false,currentRow);
                            }
                            if(detectedCModel.Count != 0)
                            {
                                CommandModel _tempCM = new CommandModel();
                                _tempCM.trainNumber = find.Split('/')[0];
                                _tempCM.secondTrainNumber = secondTrainNumber;
                               // _tempCM.secondTrainNumber = "null";
                                foreach(CommandModel _cm in detectedCModel)
                                {
                                    _tempCM.trainIndex = _cm.trainIndex;
                                    _tempCM.trainModel = _cm.trainModel;
                                    _tempCM.trainId = _cm.trainId;
                                    _tempCM.trainConnectType = _cm.trainConnectType;
                                }
                                DialogResult resultTrainStatus = MessageBox.Show(find + "次在客调命令中是否开行？（开行选择“是”，停运/待定等选择“否”）", "人工核对", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (resultTrainStatus == DialogResult.Yes)
                                {
                                    _tempCM.streamStatus = 1;
                                }
                                else if (resultTrainStatus == DialogResult.No)
                                {
                                    _tempCM.streamStatus = 0;
                                }
                                commandModel.Add(_tempCM);
                            }
                            else
                            {//一整行车都没有，自己去核对吧
                                return 2;
                            }
                            detectedCModel = new List<CommandModel>();
                        }
                    }
                    nextIndex = command_rTb.Find(find, index + find.Length, RichTextBoxFinds.WholeWord);
                    if (nextIndex == -1)//若查到文件末尾，则重置nextIndex为初始位置的值，使其达到初始位置，顺利结束循环，否则会有异常。
                        nextIndex = startPos;
                    index = nextIndex;
                }
                else
                {
                    if (index == -1)
                    {
                        break;
                    }
                    yesterdayCommand_rtb.SelectionStart = index;
                    yesterdayCommand_rtb.SelectionLength = find.Length;
                    yesterdayCommand_rtb.SelectionColor = Color.Red;
                    yesterdayCommand_rtb.SelectionFont = new Font("Times New Roman", (float)12, FontStyle.Bold);
                    yesterdayCommand_rtb.Focus();
                    DialogResult result = MessageBox.Show("请人工核对\n" + find + "次是否为↑昨日客调文本框标红内容？\n(*请注意核查同一条命令内其他车次是否正确)", "人工核对", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        {//把命令分行，找车次所在行有没有其他车次，找对应车次车号赋给新车，然后再选择停运情况
                            string currentRow = "";
                            string[] _aCommands = removeUnuseableWord(true)[0].Split('。');
                            for (int i = 0; i < _aCommands.Length; i++)
                            {//笨方法
                                if (_aCommands[i].Contains(find) &&
                                    !_aCommands[i].Contains(find + "0") &&
                                    !_aCommands[i].Contains(find + "1") &&
                                    !_aCommands[i].Contains(find + "2") &&
                                    !_aCommands[i].Contains(find + "3") &&
                                    !_aCommands[i].Contains(find + "4") &&
                                    !_aCommands[i].Contains(find + "5") &&
                                    !_aCommands[i].Contains(find + "6") &&
                                    !_aCommands[i].Contains(find + "7") &&
                                    !_aCommands[i].Contains(find + "8") &&
                                    !_aCommands[i].Contains(find + "9") &&
                                    (_aCommands[i].Contains("CR") || _aCommands[i].Contains("null")))
                                {
                                    currentRow = _aCommands[i];
                                    break;
                                }
                            }
                            if (currentRow.Length != 0)
                            {
                                analyseCommand(false, currentRow);
                            }
                            if (detectedCModel.Count != 0)
                            {
                                CommandModel _tempCM = new CommandModel();
                                _tempCM.trainNumber = find.Split('/')[0];
                                _tempCM.secondTrainNumber = secondTrainNumber;
                                foreach (CommandModel _cm in detectedCModel)
                                {
                                    _tempCM.trainIndex = _cm.trainIndex;
                                    _tempCM.trainModel = _cm.trainModel;
                                    _tempCM.trainId = _cm.trainId;
                                    _tempCM.trainConnectType = _cm.trainConnectType;
                                }
                                DialogResult resultTrainStatus = MessageBox.Show(find + "次在客调命令中是否开行？（开行选择“是”，停运/待定等选择“否”）", "人工核对", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (resultTrainStatus == DialogResult.Yes)
                                {
                                    _tempCM.streamStatus = 1;
                                }
                                else if (resultTrainStatus == DialogResult.No)
                                {
                                    _tempCM.streamStatus = 0;
                                }
                                yesterdayCommandModel.Add(_tempCM);
                            }
                            else
                            {//一整行车都没有，自己去核对吧
                                return 2;
                            }
                        }
                    }
                    nextIndex = yesterdayCommand_rtb.Find(find, index + find.Length, RichTextBoxFinds.WholeWord);
                    if (nextIndex == -1)//若查到文件末尾，则重置nextIndex为初始位置的值，使其达到初始位置，顺利结束循环，否则会有异常。
                        nextIndex = startPos;
                    index = nextIndex;
                }
            }
            return -1;
        }
        
        private string checkCommandModelWithDailySchedule(List<DailySchedule> dailyScheduleModel,List<EMUCheckModel> eMUCheckModel, List<CommandModel> cModel, string cmdText, bool isYesterday = false)
        {//班计划查错(使用基本图或动检车图)
            string unresolvedTrains = "";
            if(dailyScheduleModel != null)
            {
                foreach (DailySchedule _ds in dailyScheduleModel)
                {
                    string checkedError = checkingError(_ds.trainNumber,cModel,cmdText,isYesterday);
                    if (checkedError.Length != 0)
                    {
                        unresolvedTrains = unresolvedTrains + checkedError;
                    }
                }
            }
            else if(eMUCheckModel != null)
            {
                foreach (EMUCheckModel _es in eMUCheckModel)
                {
                    string checkedError = checkingError(_es.trainNumber,cModel,cmdText,isYesterday);
                    if (checkedError.Length != 0)
                    {
                        unresolvedTrains = unresolvedTrains + checkedError;
                    }
                }
            }
            return unresolvedTrains;
        }

        private string checkingError(string rawTrainNumber,List<CommandModel> cModel, string cmdText, bool isYesterday = false)
        {
            string firstTrainNumber = "";
            string secondTrainNumber = "";
            if(rawTrainNumber == null)
            {
                return "";
            }
            if (!rawTrainNumber.Contains("/"))
            {
                firstTrainNumber = rawTrainNumber;
            }
            else
            {
                string[] trainWithDoubleNumber = rawTrainNumber.Split('/');
                firstTrainNumber = trainWithDoubleNumber[0];
                Char[] firstTrainWord = trainWithDoubleNumber[0].ToCharArray();
                for (int q = 0; q < firstTrainWord.Length; q++)
                {
                    if (q != firstTrainWord.Length - trainWithDoubleNumber[1].Length)
                    {
                        secondTrainNumber = secondTrainNumber + firstTrainWord[q];
                    }
                    else
                    {
                        secondTrainNumber = secondTrainNumber + trainWithDoubleNumber[1];
                        break;
                    }
                }
            }
            bool hasGotIt = false;
            foreach (CommandModel _cm in cModel)
            {
                if (_cm.trainNumber.Equals(firstTrainNumber) || (_cm.trainNumber.Length != 0 && _cm.trainNumber.Equals(secondTrainNumber)))
                {//是同一趟车
                    hasGotIt = true;
                    break;
                }
            }
            if (hasGotIt)
            {//找到了说明没错
                return "";
            }
            else
            {//如果已识别车次里没有，就从客调里找找
                if (cmdText.Contains(firstTrainNumber) || secondTrainNumber.Length != 0 && cmdText.Contains(secondTrainNumber))
                {
                    //先尝试找一个车次
                    int result = searchAndHightlightUnresolvedTrains(firstTrainNumber, 1, isYesterday, secondTrainNumber);
                    if (result == -1 || result == 2)
                    {//再找第二个车次
                        int secondResult = searchAndHightlightUnresolvedTrains(secondTrainNumber, 1, isYesterday, firstTrainNumber);
                        if (secondResult == -1 || secondResult == 2)
                        {//加入未识别车次豪华套餐
                            return " " + rawTrainNumber;
                        }
                    }
                }
            }
            return "";
        }

        private void compareWithYesterday(List<DailySchedule> dailyScheduleModel,List<EMUCheckModel> emuCheckModel)
        {
            //先两天命令比一下,找到不同的再去基本图里找找有没有这趟车
            //保存对比文字
            List<string> comparedText = new List<string>();
            List<CommandModel> addedTodayCM = new List<CommandModel>();
            List<CommandModel> addedYesterdayCM = new List<CommandModel>();
            int count = 1;
            if(dailyScheduleModel == null && emuCheckModel == null)
            {
                return;
            }
            if(dailyScheduleModel != null)
            {
                for (int j = 0; j < commandModel.Count; j++)
                {//先过滤基本图
                    CommandModel _cm = commandModel[j];
                    bool hasGotIt = false;
                    for (int i = 0; i < dailyScheduleModel.Count; i++)
                    {
                        if(dailyScheduleModel[i].trainNumber == null)
                        {
                            continue;
                        }
                        if (dailyScheduleModel[i].trainNumber.Split('/')[0].Trim().Equals(commandModel[j].trainNumber.Trim()) ||
                        dailyScheduleModel[i].trainNumber.Split('/')[0].Trim().Equals(commandModel[j].secondTrainNumber.Trim()))
                        {
                            addedTodayCM.Add(_cm);
                            hasGotIt = true;
                        }
                        if (hasGotIt)
                        {
                            break;
                        }
                    }
                }
                for (int j = 0; j < yesterdayCommandModel.Count; j++)
                {//(前日)先过滤基本图
                    CommandModel _cm = yesterdayCommandModel[j];
                    bool hasGotIt = false;
                    for (int i = 0; i < dailyScheduleModel.Count; i++)
                    {
                        if(dailyScheduleModel[i].trainNumber == null)
                        {
                            continue;
                        }
                        if (dailyScheduleModel[i].trainNumber.Split('/')[0].Trim().Equals(yesterdayCommandModel[j].trainNumber.Trim()) ||
                        dailyScheduleModel[i].trainNumber.Split('/')[0].Trim().Equals(yesterdayCommandModel[j].secondTrainNumber.Trim()))
                        {
                            addedYesterdayCM.Add(_cm);
                            hasGotIt = true;
                        }
                        if (hasGotIt)
                        {
                            break;
                        }
                    }
                }
            }
            if(emuCheckModel != null)
            {
                for (int j = 0; j < commandModel.Count; j++)
                {//先过滤基本图
                    CommandModel _cm = commandModel[j];
                    bool hasGotIt = false;
                    for (int i = 0; i < emuCheckModel.Count; i++)
                    {
                        if (emuCheckModel[i].trainNumber.Trim().Equals(commandModel[j].trainNumber.Trim()) ||
                        emuCheckModel[i].trainNumber.Trim().Equals(commandModel[j].secondTrainNumber.Trim()))
                        {
                            addedTodayCM.Add(_cm);
                            hasGotIt = true;
                        }
                        if (hasGotIt)
                        {
                            break;
                        }
                    }
                }
                for (int j = 0; j < yesterdayCommandModel.Count; j++)
                {//(前日)先过滤基本图
                    CommandModel _cm = yesterdayCommandModel[j];
                    bool hasGotIt = false;
                    for (int i = 0; i < emuCheckModel.Count; i++)
                    {
                        if (emuCheckModel[i].trainNumber.Trim().Equals(yesterdayCommandModel[j].trainNumber.Trim()) ||
                        emuCheckModel[i].trainNumber.Trim().Equals(yesterdayCommandModel[j].secondTrainNumber.Trim()))
                        {
                            addedYesterdayCM.Add(_cm);
                            hasGotIt = true;
                        }
                        if (hasGotIt)
                        {
                            break;
                        }
                    }
                }
            }
            foreach (CommandModel _cm in addedTodayCM)
            {//找两个共同的-和今天有昨天没有的
                bool hasGotIt = false;
                foreach (CommandModel _yesterdayCM in addedYesterdayCM)
                {//再从昨天里找不一样的
                        if (_cm.secondTrainNumber.Equals("null") && _yesterdayCM.secondTrainNumber.Equals("null"))
                        {//只比第一个车次
                            if (_cm.trainNumber.Equals(_yesterdayCM.trainNumber))
                            {//车次相同,开始对比
                                string result = detailsCompareWithYesterday(_cm, _yesterdayCM);
                                if (result.Length != 0)
                                {
                                    comparedText.Add(count.ToString() + "、" + result);
                                    count++;
                                }
                                hasGotIt = true;
                            }
                        }
                        else
                        {
                            if (_cm.trainNumber.Equals(_yesterdayCM.trainNumber) || _cm.trainNumber.Equals(_yesterdayCM.secondTrainNumber)
                                || _cm.secondTrainNumber.Equals(_yesterdayCM.trainNumber) || _cm.trainNumber.Equals(_yesterdayCM.secondTrainNumber))
                            {
                                string result = detailsCompareWithYesterday(_cm, _yesterdayCM);
                                if (result.Length != 0)
                                {
                                    comparedText.Add(count.ToString() + "、" + result);
                                    count++;
                                }
                                hasGotIt = true;
                            }
                        }
                    if (hasGotIt)
                    {
                        break;
                    }
                }
                //出来没找到 那就是今天加开的
                if (!hasGotIt)
                {
                    comparedText.Add(count.ToString() + "、" + "今日(←)加开" + _cm.trainNumber + "次，车型" + _cm.trainModel + "，编组" + returnConnectType(_cm.trainConnectType) + "节，位于今日(←)命令第" + _cm.trainIndex + "条。");
                    count++;
                }
            }
            foreach (CommandModel _yesterdayCM in addedYesterdayCM)
            {//找昨天有今天没有的
                bool hasGotIt = false;
                foreach (CommandModel _cm in addedTodayCM)
                {
                        if (_cm.secondTrainNumber.Equals("null") && _yesterdayCM.secondTrainNumber.Equals("null"))
                        {//只比第一个车次
                            if (_cm.trainNumber.Equals(_yesterdayCM.trainNumber))
                            {//车次相同
                                hasGotIt = true;
                            }
                        }
                        else
                        {
                            if (_cm.trainNumber.Equals(_yesterdayCM.trainNumber) || _cm.trainNumber.Equals(_yesterdayCM.secondTrainNumber)
                            || _cm.secondTrainNumber.Equals(_yesterdayCM.trainNumber) || _cm.trainNumber.Equals(_yesterdayCM.secondTrainNumber))
                            {
                                hasGotIt = true;
                            }
                        }
                    if (hasGotIt)
                    {
                        break;
                    }
                }
                if (!hasGotIt)
                {//昨天开的
                    comparedText.Add(count.ToString() + "、" + "今日(←)停运" + _yesterdayCM.trainNumber + "次，位于昨日(↑)命令第" + _yesterdayCM.trainIndex + "条。");
                    count++;
                }
            }
            //显示对比结果
            string allResults = "";
            foreach (string _str in comparedText)
            {
                allResults = allResults + _str + "\n";
            }
            comparedResult_rtb.Text = allResults;

        }

        private string returnConnectType(int connectType)
        {
            switch (connectType)
            {
                case 0:
                    {
                        return "8";
                        break;
                    }
                case 1:
                    {
                        return "16";
                        break;
                    }
                case 2:
                    {
                        return "8+8";
                        break;
                    }

            }
            return "";
        }

        private string detailsCompareWithYesterday(CommandModel today, CommandModel yesterday)
        {
            string compareText = "";
            if (!today.trainModel.Equals(yesterday.trainModel) ||
                !today.trainConnectType.Equals(yesterday.trainConnectType) ||
                !today.streamStatus.Equals(yesterday.streamStatus))
            {
                compareText = today.trainNumber + "次；";
                if (!today.streamStatus.Equals(yesterday.streamStatus))
                {
                    //(0停开，1开行，2次日)
                    if (today.streamStatus == 0 && (yesterday.streamStatus == 1 || yesterday.streamStatus == 2))
                    {//今日停运
                        compareText = compareText + "今日(←)停运；";
                    }
                    else if ((today.streamStatus == 1 || today.streamStatus == 2) && yesterday.streamStatus == 0)
                    {//昨日停运
                        compareText = compareText + "今日(←)恢复开行；";
                    }
                }
                if (!today.trainModel.Equals(yesterday.trainModel))
                {
                    if (returnConnectType(yesterday.trainConnectType).Length != 0)
                    {
                        compareText = compareText + "昨日(↑)车型:" + yesterday.trainModel + ",编组" + returnConnectType(yesterday.trainConnectType) + "；";
                    }
                    if (returnConnectType(today.trainConnectType).Length != 0)
                    {
                        compareText = compareText + "今日(←)车型:" + today.trainModel + ",编组" + returnConnectType(today.trainConnectType) + "；";
                    }
                }
                compareText = compareText + "位于昨日(↑)命令第" + yesterday.trainIndex + "条，今日(←)第" + today.trainIndex + "条。";
            }
            return compareText;
        }

        //处理经过本站时列车车次问题
        private string correctDualNumber(DailySchedule dailyScheduleModel, string[] trainWithDoubleNumber, int upOrDown)
        {
            Char[] firstTrainWord = trainWithDoubleNumber[0].ToCharArray();
            String secondTrainWord = "";
            String tempFirstTrainWord = "";
            bool _hasGotIt = false;
            int outNumber = -1;
            int.TryParse(firstTrainWord[firstTrainWord.Length - 1].ToString(), out outNumber);
            if (upOrDown == 1)
            {
                if ((outNumber >= 0) && (outNumber % 2 != 0))
                {//是单号车
                    return dailyScheduleModel.trainNumber.Trim();
                }
            }
            else
            {
                if ((outNumber >= 0) && (outNumber % 2 == 0))
                {//是单号车
                    return dailyScheduleModel.trainNumber.Trim();
                }
            }
            for (int q = 0; q < firstTrainWord.Length; q++)
            {
                if (q < firstTrainWord.Length - trainWithDoubleNumber[1].Length)
                {
                    secondTrainWord = secondTrainWord + firstTrainWord[q];
                }
                else
                {
                    tempFirstTrainWord = tempFirstTrainWord + firstTrainWord[q].ToString();
                    if (_hasGotIt != true)
                    {
                        secondTrainWord = secondTrainWord + trainWithDoubleNumber[1];
                        _hasGotIt = true;
                    }
                }
            }
            return secondTrainWord + "/" + tempFirstTrainWord;

        }

        //核对客调令，处理班计划顺序
        private void analyzeDailyScheduleData(List<DailySchedule> dailyScheduleModel)
        {
            int counter = 1;
            List<DailySchedule> _dailyScheduleModel = new List<DailySchedule>();

            for (int j = 0; j < commandModel.Count; j++)
            {
                bool hasGotOne = false;
                for (int i = 0; i < dailyScheduleModel.Count; i++)
                {
                    if(dailyScheduleModel[i].trainNumber == null)
                    {
                        continue;
                    }
                    if (dailyScheduleModel[i].trainNumber.Split('/')[0].Trim().Equals(commandModel[j].trainNumber.Trim()) ||
                        dailyScheduleModel[i].trainNumber.Split('/')[0].Trim().Equals(commandModel[j].secondTrainNumber.Trim()))
                    {//对比车次
                        hasGotOne = true;
                        DailySchedule _ds = new DailySchedule();
                        _ds.id = counter;
                        _ds.streamStatus = commandModel[j].streamStatus;
                        _ds.trainType = commandModel[j].trainType;
                        if (commandModel[j].upOrDown != -1)
                        {
                            _ds.upOrDown = commandModel[j].upOrDown;
                        }
                        else
                        {
                            if (upStations.Contains(dailyScheduleModel[i].startStation) ||
                                downStations.Contains(dailyScheduleModel[i].stopStation))
                            {//下行车
                                _ds.upOrDown = 1;
                            }
                            else if (downStations.Contains(dailyScheduleModel[i].startStation) ||
                                upStations.Contains(dailyScheduleModel[i].stopStation))
                            {//上行车
                                _ds.upOrDown = 0;
                            }
                        }
                        //通过上下行判断经过本站时双车次列车的车次号（直接截取为经过本站的车次号）
                        if (dailyScheduleModel[i].trainNumber != null)
                        {
                            if (dailyScheduleModel[i].trainNumber.Contains("/"))
                            {
                                string[] trainWithDoubleNumber = dailyScheduleModel[i].trainNumber.Split('/');
                                if (_ds.upOrDown == 1)
                                {
                                    _ds.trainNumber = correctDualNumber(dailyScheduleModel[i], trainWithDoubleNumber, 1);
                                }
                                else if (_ds.upOrDown == 0)
                                {
                                    _ds.trainNumber = correctDualNumber(dailyScheduleModel[i], trainWithDoubleNumber, 0);
                                }
                                else
                                {
                                    _ds.trainNumber = dailyScheduleModel[i].trainNumber.Trim();
                                }
                            }
                            else
                            {
                                _ds.trainNumber = dailyScheduleModel[i].trainNumber.Trim();
                            }
                        }

                        //后面的和原来对象一样
                        if (dailyScheduleModel[i].stopStation != null)
                            _ds.stopStation = dailyScheduleModel[i].stopStation.Trim();
                        if (dailyScheduleModel[i].startStation != null)
                            _ds.startStation = dailyScheduleModel[i].startStation.Trim();
                        if (dailyScheduleModel[i].stopTime != null)
                            _ds.stopTime = dailyScheduleModel[i].stopTime.Trim();
                        if (dailyScheduleModel[i].startTime != null)
                            _ds.startTime = dailyScheduleModel[i].startTime.Trim();
                        if (dailyScheduleModel[i].stopToStartTime != null)
                            _ds.stopToStartTime = dailyScheduleModel[i].stopToStartTime.Trim();
                        if (dailyScheduleModel[i].trainBelongsTo != null)
                            _ds.trainBelongsTo = dailyScheduleModel[i].trainBelongsTo.Trim();
                        if (dailyScheduleModel[i].trackNum != null)
                            _ds.trackNum = dailyScheduleModel[i].trackNum.Trim();
                        if (dailyScheduleModel[i].ratedSeats != null)
                            _ds.ratedSeats = dailyScheduleModel[i].ratedSeats.Trim();
                        if (dailyScheduleModel[i].extraText != null)
                            _ds.extraText = dailyScheduleModel[i].extraText.Trim();
                        if (dailyScheduleModel[i].tipsText != null)
                            _ds.tipsText = dailyScheduleModel[i].tipsText.Trim();
                        //车型
                        switch (commandModel[j].trainConnectType)
                        {
                            case 0:
                                if (dailyScheduleModel[i].trainConnectType != null)
                                    if (!dailyScheduleModel[i].trainConnectType.Equals("8"))
                                    {
                                        _ds.hasDifferentPart = true;
                                    }
                                _ds.trainConnectType = "8";
                                if (_ds.extraText == null)
                                {
                                    _ds.extraText = "";
                                }
                                if (_ds.extraText != null)
                                {
                                    //判断南北停靠
                                    if (!_ds.extraText.Contains("南") &&
                                        !_ds.extraText.Contains("北"))
                                    {
                                        if (_ds.upOrDown == 0)
                                        {
                                            //特殊情况-出库车，寻找客调令中有没有该车次的出库
                                            //找同一组车底内，有无0G/D(XXX-1)或者0G/D(XXX+1)或者-3+3，有的话是出库车，则停南
                                            string thisTrainNumber = _ds.trainNumber.Split('/')[0];
                                            string trainType = "";
                                            string splitedNumber = "";
                                            string[] targetTrainNum = new string[4];
                                            int originalTrainNumber = 0;
                                            foreach (char item in thisTrainNumber)
                                            {//取数字
                                                if (item >= 48 && item <= 58)
                                                {
                                                    splitedNumber += item;
                                                }
                                            }
                                            int.TryParse(splitedNumber, out originalTrainNumber);
                                            if (thisTrainNumber.Contains("G"))
                                            {//G0D1
                                                trainType = "G";
                                            }
                                            else if (thisTrainNumber.Contains("D"))
                                            {
                                                trainType = "D";
                                            }
                                            else if (thisTrainNumber.Contains("C"))
                                            {
                                                trainType = "C";
                                            }
                                            if (trainType.Length != 0)
                                            {
                                                if (originalTrainNumber != 0)
                                                {
                                                    bool hasGotIt = false;
                                                    for (int ij = 0; ij < 4; ij++)
                                                    {//+1 -1 +3 -3分别试找一遍
                                                        switch (ij)
                                                        {
                                                            case 0:
                                                                targetTrainNum[0] = "0" + trainType + (originalTrainNumber + 1).ToString();
                                                                break;
                                                            case 1:
                                                                targetTrainNum[1] = "0" + trainType + (originalTrainNumber - 1).ToString();
                                                                break;
                                                            case 2:
                                                                targetTrainNum[2] = "0" + trainType + (originalTrainNumber + 3).ToString();
                                                                break;
                                                            case 3:
                                                                targetTrainNum[3] = "0" + trainType + (originalTrainNumber - 3).ToString();
                                                                break;
                                                        }
                                                    }
                                                    foreach (CommandModel _cm in commandModel)
                                                    {//车可能是0J
                                                        if (_cm.trainNumber.Contains("0" + trainType) &&
                                                            _cm.trainId.Equals(commandModel[j].trainId))
                                                        {
                                                            for (int ij = 0; ij < targetTrainNum.Length; ij++)
                                                            {
                                                                if (_cm.trainNumber.Equals(targetTrainNum[ij]))
                                                                {
                                                                    hasGotIt = true;
                                                                }
                                                                if (hasGotIt)
                                                                {
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        if (hasGotIt)
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    if (hasGotIt)
                                                    {
                                                        string extraText = _ds.extraText + " （南）";
                                                        _ds.extraText = extraText;
                                                    }
                                                    else
                                                    {//正常情况
                                                        string extraText = _ds.extraText + " （北）";
                                                        _ds.extraText = extraText;
                                                    }
                                                }
                                            }

                                        }
                                        else if (_ds.upOrDown == 1)
                                        {
                                            string extraText = _ds.extraText + " （南）";
                                            _ds.extraText = extraText;
                                        }
                                        else
                                        {
                                            string extraText = _ds.extraText + " （南/北停靠无法识别）";
                                            _ds.extraText = extraText;
                                        }
                                        if (_ds.extraText.Contains("重联"))
                                        {
                                            string extraText = _ds.extraText.Replace("重联", "");
                                            _ds.extraText = extraText;
                                        }
                                    }
                                }
                                break;
                            case 1:
                                if (dailyScheduleModel[i].trainConnectType != null)
                                    if (!dailyScheduleModel[i].trainConnectType.Equals("16"))
                                    {
                                        _ds.hasDifferentPart = true;
                                    }
                                if (_ds.extraText != null)
                                {
                                    if (_ds.extraText.Contains("南") ||
                                        _ds.extraText.Contains("北"))
                                    {
                                        string extraText = _ds.extraText.Replace("（南）", "").Replace("（北）", "").Replace("(南)", "").Replace("(北)", "");
                                        _ds.extraText = extraText;
                                    }
                                    if (_ds.extraText.Contains("重联"))
                                    {
                                        string extraText = _ds.extraText.Replace("重联", "");
                                        _ds.extraText = extraText;
                                    }
                                }
                                _ds.trainConnectType = "16";
                                break;
                            case 2:
                                if (dailyScheduleModel[i].trainConnectType != null)
                                    if (!dailyScheduleModel[i].trainConnectType.Equals("8+"))
                                    {
                                        _ds.hasDifferentPart = true;
                                    }
                                if (_ds.extraText == null)
                                {
                                    _ds.extraText = "";
                                }
                                if (_ds.extraText != null)
                                {
                                    if (_ds.extraText.Contains("南") ||
                                   _ds.extraText.Contains("北"))
                                    {
                                        string extraText = _ds.extraText.Replace("（南）", "").Replace("（北）", "").Replace("(南)", "").Replace("(北)", "");
                                        _ds.extraText = extraText;
                                    }
                                    if (!_ds.extraText.Contains("重联"))
                                    {
                                        _ds.extraText = _ds.extraText + " 重联";
                                    }
                                }
                                _ds.trainConnectType = "8+8";
                                break;
                        }
                        string trainModel = "";
                        if (dailyScheduleModel[i].trainModel != null)
                        {
                            trainModel = dailyScheduleModel[i].trainModel.Trim();
                        }
                        if (trainModel.Contains("统型"))
                        {
                            trainModel = trainModel.Replace("统型", "");
                        }
                        if (!trainModel.Equals(commandModel[j]))
                        {
                            _ds.hasDifferentPart = true;
                        }
                        if (!commandModel[j].trainModel.Equals("null"))
                        {
                            _ds.trainModel = commandModel[j].trainModel;
                        }

                        _dailyScheduleModel.Add(_ds);
                    }
                    if (hasGotOne)
                    {
                        break;
                    }
                    if (i == dailyScheduleModel.Count - 1)
                    {
                        if (!hasGotOne)
                        {
                            if (!commandModel[j].trainNumber.Contains("0G") &&
                                !commandModel[j].trainNumber.Contains("0J") &&
                                !commandModel[j].trainNumber.Contains("DJ") &&
                                !commandModel[j].trainNumber.Contains("0D") &&
                                !commandModel[j].trainNumber.Contains("0C"))
                            {
                                DailySchedule _ds = new DailySchedule();
                                _ds.id = counter;
                                _ds.streamStatus = commandModel[j].streamStatus;
                                _ds.trainType = commandModel[j].trainType;
                                if (commandModel[j].secondTrainNumber.Equals("null"))
                                {
                                    _ds.trainNumber = commandModel[j].trainNumber;
                                }
                                else
                                {
                                    _ds.trainNumber = commandModel[j].trainNumber + "/" + commandModel[j].secondTrainNumber;
                                }

                                switch (commandModel[j].trainConnectType)
                                {
                                    case 0:
                                        _ds.trainConnectType = "8";
                                        break;
                                    case 1:
                                        _ds.trainConnectType = "16";
                                        break;
                                    case 2:
                                        _ds.trainConnectType = "8+8";
                                        break;
                                }
                                if (!commandModel[j].trainModel.Equals("null"))
                                {
                                    _ds.trainModel = commandModel[j].trainModel;
                                }
                                if (_ds.streamStatus != 0)
                                {
                                    _ds.extraText = "人工核对-客调令多出";
                                    _ds.hasDifferentPart = true;
                                    counter++;
                                    _dailyScheduleModel.Add(_ds);
                                }
                            }
                        }
                    }
                }

            }
            _dailyScheduleModel.Sort();
            int _counter = 1;
            for (int j = 0; j < _dailyScheduleModel.Count; j++)
            {
                if (_dailyScheduleModel[j].startTime != null)
                {//预售时间
                    if (_dailyScheduleModel[j].startTime.Contains(":"))
                    {
                        int presaleTime = 0;
                        int.TryParse(_dailyScheduleModel[j].startTime.Split(':')[0], out presaleTime);
                        _dailyScheduleModel[j].presaleTime = presaleTime;
                    }
                }
                else if (_dailyScheduleModel[j].stopTime != null)
                {
                    if (_dailyScheduleModel[j].stopTime.Contains(":"))
                    {
                        int presaleTime = 0;
                        int.TryParse(_dailyScheduleModel[j].stopTime.Split(':')[0], out presaleTime);
                        _dailyScheduleModel[j].presaleTime = presaleTime;
                    }
                }
                if (_dailyScheduleModel[j].extraText != null)
                {//id
                    if (_dailyScheduleModel[j].extraText.Contains("客调"))
                    {
                        _dailyScheduleModel[j].id = 0;
                        continue;
                    }
                    if (_dailyScheduleModel[j].streamStatus != 0)
                    {
                        _dailyScheduleModel[j].id = _counter;
                        _counter++;
                    }
                    else
                    {
                        _dailyScheduleModel[j].id = 0;
                    }

                }
                else
                {
                    if (_dailyScheduleModel[j].streamStatus != 0)
                    {
                        _dailyScheduleModel[j].id = _counter;
                        _counter++;
                    }
                    else
                    {
                        _dailyScheduleModel[j].id = 0;
                    }
                }
            }
            allDailyScheduleModel = _dailyScheduleModel;
            //最后一步 打印
            createDailySchedule();
        }

        //筛选不开行的动检车
        private void analyzeEMUC_Data(List<EMUCheckModel> emuCheckModel)
        {
            int counter = 1;
            List<EMUCheckModel> _emuCheckModel = new List<EMUCheckModel>();

            for (int j = 0; j < commandModel.Count; j++)
            {
                bool hasGotOne = false;
                for (int i = 0; i < emuCheckModel.Count; i++)
                {
                    if (emuCheckModel[i].trainNumber.Trim().Equals(commandModel[j].trainNumber.Trim()) ||
                        emuCheckModel[i].trainNumber.Trim().Equals(commandModel[j].secondTrainNumber.Trim()))
                    {//对比车次
                        hasGotOne = true;
                        EMUCheckModel _es = new EMUCheckModel();
                        _es.id = counter;
                        _es.trainNumber = emuCheckModel[i].trainNumber.Trim();
                        _es.streamStatus = commandModel[j].streamStatus;
                        //后面的和原来对象一样
                        if (emuCheckModel[i].destination != null)
                            _es.destination = emuCheckModel[i].destination.Trim();
                        if (emuCheckModel[i].startStation != null)
                            _es.startStation = emuCheckModel[i].startStation.Trim();
                        if (emuCheckModel[i].extra != null)
                            _es.startStation = emuCheckModel[i].extra.Trim();
                        if (emuCheckModel[i].stopTime != null)
                            _es.stopTime = emuCheckModel[i].stopTime.Trim();
                        if (emuCheckModel[i].startTime != null)
                            _es.startTime = emuCheckModel[i].startTime.Trim();
                        if (emuCheckModel[i].trackNum != null)
                            _es.trackNum = emuCheckModel[i].trackNum.Trim();
                        _emuCheckModel.Add(_es);
                    }
                    if (hasGotOne)
                    {
                        break;
                    }
                    if (i == emuCheckModel.Count - 1)
                    {
                        if (!hasGotOne)
                        {
                            if (commandModel[j].trainNumber.Contains("0G") ||
                                commandModel[j].trainNumber.Contains("0J") ||
                                commandModel[j].trainNumber.Contains("DJ") ||
                                commandModel[j].trainNumber.Contains("0D") ||
                                commandModel[j].trainNumber.Contains("0C"))
                            {
                                EMUCheckModel _es = new EMUCheckModel();
                                if (commandModel[j].secondTrainNumber.Equals("null"))
                                {
                                    _es.trainNumber = commandModel[j].trainNumber;
                                }
                                else
                                {
                                    _es.trainNumber = commandModel[j].trainNumber + "/" + commandModel[j].secondTrainNumber;
                                }
                                _es.id = -1;
                                _es.streamStatus = commandModel[j].streamStatus;
                                if (_es.streamStatus != 0)
                                {
                                    _es.extra = "客调令多出";
                                    _emuCheckModel.Add(_es);
                                }
                            }
                        }
                    }
                }
            }
            int _counter = 1;
            for (int j = 0; j < _emuCheckModel.Count; j++)
            {
                if (_emuCheckModel[j].extra != null)
                {//id
                    if (_emuCheckModel[j].extra.Contains("客调"))
                    {
                        _emuCheckModel[j].id = -1;
                        continue;
                    }
                    if (_emuCheckModel[j].streamStatus != 0)
                    {
                        _emuCheckModel[j].id = _counter;
                        _counter++;
                    }
                    else
                    {
                        _emuCheckModel[j].id = 0;
                    }

                }
                else
                {
                    if (_emuCheckModel[j].streamStatus != 0)
                    {
                        _emuCheckModel[j].id = _counter;
                        _counter++;
                    }
                    else
                    {
                        _emuCheckModel[j].id = 0;
                    }
                }
            }
            _emuCheckModel.Sort();
            allEmuCheckModel = _emuCheckModel;
            //最后一步 打印
            createEMUC_Table();
        }

        //创建班计划
        private void createDailySchedule()
        {
                //创建Excel文件名称
                FileStream fs = File.Create(Application.StartupPath + "\\" + startPath + "\\" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "班计划.xls");
                //创建工作薄
                IWorkbook workbook = new HSSFWorkbook();

                //表格样式
                ICellStyle boldStyle = workbook.CreateCellStyle();
                boldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                boldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                boldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                boldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                boldStyle.WrapText = true;
                boldStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                boldStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                boldStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                HSSFFont fontBold = (HSSFFont)workbook.CreateFont();
                fontBold.FontName = "宋体";//字体  
                fontBold.FontHeightInPoints = 10;//字号  
                fontBold.IsBold = true;//加粗  
                boldStyle.SetFont(fontBold);

                //表格样式
                ICellStyle stoppedTrainStyle = workbook.CreateCellStyle();
                stoppedTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                stoppedTrainStyle.FillPattern = FillPattern.SolidForeground;
                stoppedTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                stoppedTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTrainStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                stoppedTrainStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                stoppedTrainStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

                ICellStyle stoppedTimeStyle = workbook.CreateCellStyle();
                stoppedTimeStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                stoppedTimeStyle.FillPattern = FillPattern.SolidForeground;
                stoppedTimeStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                stoppedTimeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTimeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTimeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTimeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTimeStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                stoppedTimeStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                stoppedTimeStyle.DataFormat = 20;

                HSSFFont font = (HSSFFont)workbook.CreateFont();
                font.FontName = "宋体";//字体  
                font.FontHeightInPoints = 10;//字号  
                font.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
                stoppedTimeStyle.SetFont(font);
                stoppedTrainStyle.SetFont(font);

                ICellStyle normalStyle = workbook.CreateCellStyle();
                normalStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                normalStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                normalStyle.WrapText = true;
                normalStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                normalStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                normalStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                normalStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                normalStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");


                //IDataFormat dataformat = workbook.CreateDataFormat();
                ICellStyle normalTimeStyle = workbook.CreateCellStyle();
                normalTimeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                normalTimeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                normalTimeStyle.WrapText = true;
                normalTimeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                normalTimeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                normalTimeStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                normalTimeStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                                                                                               //176是综控用于时间的格式
                normalTimeStyle.DataFormat = 20;
                //normalTimeStyle.DataFormat = dataformat.GetFormat("HH:mm");

                HSSFFont fontNormal = (HSSFFont)workbook.CreateFont();
                fontNormal.FontName = "宋体";//字体  
                fontNormal.FontHeightInPoints = 10;//字号  
                normalStyle.SetFont(fontNormal);
                normalTimeStyle.SetFont(fontNormal);


                //创建sheet
                ISheet sheet = workbook.CreateSheet("基本图");
                //标注预售
                int presaleHour = 0;
                int startPresaleRow = 0;
                for (int i = 0; i < 2 + allDailyScheduleModel.Count; i++)
                {
                    IRow row = sheet.CreateRow(i);
                    if (i == 0)
                    {
                        for (int count = 0; count < 16; count++)
                        {
                            row.CreateCell(count);
                            row.GetCell(count).CellStyle = boldStyle;
                        }
                        //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 15));
                        row.Height = 15 * 20;
                        row.CreateCell(0).SetCellValue(DateTime.Now.AddDays(1).ToString("yyyy.MM.dd") + "日班计划");
                        row.GetCell(0).CellStyle = boldStyle;
                    }
                    else if (i == 1)
                    {
                        row.Height = 32 * 20;
                        for (int count = 0; count < 16; count++)
                        {
                            switch (count)
                            {
                                case 0:
                                    row.CreateCell(count).SetCellValue("预售");
                                    sheet.SetColumnWidth(count, 5 * 256);
                                    break;
                                case 1:
                                    row.CreateCell(count).SetCellValue("序\n号");
                                    sheet.SetColumnWidth(count, 5 * 256);
                                    break;
                                case 2:
                                    row.CreateCell(count).SetCellValue("车次");
                                    sheet.SetColumnWidth(count, 9 * 256);
                                    break;
                                case 3:
                                    row.CreateCell(count).SetCellValue("始发站");
                                    sheet.SetColumnWidth(count, 9 * 256);
                                    break;
                                case 4:
                                    row.CreateCell(count).SetCellValue("终到站");
                                    sheet.SetColumnWidth(count, 9 * 256);
                                    break;
                                case 5:
                                    row.CreateCell(count).SetCellValue("到时");
                                    sheet.SetColumnWidth(count, 6 * 256);
                                    break;
                                case 6:
                                    row.CreateCell(count).SetCellValue("开时");
                                    sheet.SetColumnWidth(count, 6 * 256);
                                    break;
                                case 7:
                                    row.CreateCell(count).SetCellValue("停\n时");
                                    sheet.SetColumnWidth(count, 3 * 256);
                                    break;
                                case 8:
                                    row.CreateCell(count).SetCellValue("实\n到");
                                    sheet.SetColumnWidth(count, 3 * 256);
                                    break;
                                case 9:
                                    row.CreateCell(count).SetCellValue("实\n开");
                                    sheet.SetColumnWidth(count, 3 * 256);
                                    break;
                                case 10:
                                    row.CreateCell(count).SetCellValue("正\n晚");
                                    sheet.SetColumnWidth(count, 3 * 256);
                                    break;
                                case 11:
                                    row.CreateCell(count).SetCellValue("股道");
                                    sheet.SetColumnWidth(count, 5 * 256);
                                    break;
                                case 12:
                                    row.CreateCell(count).SetCellValue("编\n组");
                                    sheet.SetColumnWidth(count, 5 * 256);
                                    break;
                                case 13:
                                    row.CreateCell(count).SetCellValue("车型");
                                    sheet.SetColumnWidth(count, 7 * 256);
                                    break;
                                case 14:
                                    row.CreateCell(count).SetCellValue("担当");
                                    sheet.SetColumnWidth(count, 5 * 256);
                                    break;
                                case 15:
                                    row.CreateCell(count).SetCellValue("备注");
                                    sheet.SetColumnWidth(count, 20 * 256);
                                    break;
                            }
                            row.GetCell(count).CellStyle = boldStyle;
                        }
                    }
                    else
                    {
                        row.Height = 15 * 20;
                        for (int column = 0; column < 16; column++)
                        {
                            switch (column)
                            {
                                case 0:
                                    if (presaleHour != allDailyScheduleModel[i - 2].presaleTime &&
                                        allDailyScheduleModel[i - 2].presaleTime != 0 &&
                                        allDailyScheduleModel[i - 2].streamStatus != 0)
                                    {
                                        //先把上一个合并一下
                                        if (startPresaleRow != 0)
                                        {//第一个必须还是要有的
                                         //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                                            sheet.AddMergedRegion(new CellRangeAddress(startPresaleRow, i - 1, 0, 0));
                                            if (presaleHour >= 5)
                                            {
                                                sheet.GetRow(startPresaleRow).GetCell(0).SetCellValue(presaleHour + "点列车预售");
                                            }
                                        }
                                        if (allDailyScheduleModel[i - 2].presaleTime >= 5)
                                        {
                                            startPresaleRow = i;
                                            row.CreateCell(column);
                                            presaleHour = allDailyScheduleModel[i - 2].presaleTime;
                                        }
                                        else
                                        {
                                            row.CreateCell(column);
                                        }
                                    }
                                    else
                                    {
                                        row.CreateCell(column);
                                    }
                                    break;
                                case 1:
                                    if (allDailyScheduleModel[i - 2].id != 0)
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].id);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(" ");
                                    }
                                    break;
                                case 2:
                                    string trainNumber = allDailyScheduleModel[i - 2].trainNumber;
                                    if (trainNumber.Split('/').Length > 2)
                                    {
                                        trainNumber = trainNumber.Split('/')[0] + "/" + trainNumber.Split('/')[1];
                                    }
                                    row.CreateCell(column).SetCellValue(trainNumber);
                                    break;
                                case 3:
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].startStation);
                                    break;
                                case 4:
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].stopStation);
                                    break;
                                case 5:
                                    DateTime dtStop;
                                    DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                                    dtFormat.ShortDatePattern = "HH:mm";
                                    if (allDailyScheduleModel[i - 2].stopTime != null)
                                    {
                                        dtStop = Convert.ToDateTime(allDailyScheduleModel[i - 2].stopTime, dtFormat);
                                        row.CreateCell(column).SetCellType(CellType.Numeric);
                                        row.CreateCell(column).SetCellValue(dtStop);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].stopTime);
                                    }
                                    break;
                                case 6:
                                    DateTime dtStart;
                                    DateTimeFormatInfo dtFormat1 = new DateTimeFormatInfo();
                                    dtFormat1.ShortDatePattern = "HH:mm";
                                    if (allDailyScheduleModel[i - 2].startTime != null)
                                    {
                                        dtStart = Convert.ToDateTime(allDailyScheduleModel[i - 2].startTime, dtFormat1);
                                        row.CreateCell(column).SetCellType(CellType.Numeric);
                                        row.CreateCell(column).SetCellValue(dtStart);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].startTime);
                                    }
                                    break;
                                case 7:
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].stopToStartTime);
                                    break;
                                case 8:
                                    row.CreateCell(column);
                                    break;
                                case 9:
                                    row.CreateCell(column);
                                    break;
                                case 10:
                                    row.CreateCell(column);
                                    break;
                                case 11:
                                    int outTrackNum = 0;
                                    int.TryParse(allDailyScheduleModel[i - 2].trackNum, out outTrackNum);
                                    if (outTrackNum == 0)
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trackNum);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(outTrackNum);
                                    }
                                    break;
                                case 12:
                                    int outConnectType = 0;
                                    int.TryParse(allDailyScheduleModel[i - 2].trainConnectType, out outConnectType);
                                    if (outConnectType == 0)
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trainConnectType);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(outConnectType);
                                    }

                                    break;
                                case 13:
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trainModel);
                                    break;
                                case 14:
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trainBelongsTo);
                                    break;
                                case 15:
                                    if (allDailyScheduleModel[i - 2].streamStatus != 0)
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].extraText);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue("停运");
                                    }

                                    break;
                            }
                            if (column > 1)
                            {
                                row.GetCell(column).CellStyle = normalStyle;
                            }
                            else if (column == 1 || column == 0)
                            {
                                row.GetCell(column).CellStyle = boldStyle;
                            }
                            if (column == 5 || column == 6)
                            {
                                row.GetCell(column).CellStyle = normalTimeStyle;
                            }
                            if (allDailyScheduleModel[i - 2].streamStatus == 0 && column != 0)
                            {
                                if (column == 5 || column == 6)
                                {
                                    row.GetCell(column).CellStyle = stoppedTimeStyle;
                                }
                                else
                                {
                                    row.GetCell(column).CellStyle = stoppedTrainStyle;
                                }
                            }

                        }
                    }
                }

                //向excel文件中写入数据并保保存
                workbook.Write(fs);
                fs.Close();
                System.Diagnostics.ProcessStartInfo info1 = new System.Diagnostics.ProcessStartInfo();
                //info.WorkingDirectory = Application.StartupPath;
                info1.FileName = Application.StartupPath + "\\" + startPath + "\\";
                info1.Arguments = "";
                try
                {
                    System.Diagnostics.Process.Start(info1);
                }
                catch (System.ComponentModel.Win32Exception we)
                {
                    MessageBox.Show(this, we.Message);
                    return;
                }
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
                //info.WorkingDirectory = Application.StartupPath;
                info.FileName = Application.StartupPath + "\\" + startPath + "\\" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "班计划.xls";
                info.Arguments = "";
                try
                {
                    System.Diagnostics.Process.Start(info);
                }
                catch (System.ComponentModel.Win32Exception we)
                {
                    MessageBox.Show(this, we.Message);
                    return;
                }

         

        }

        //创建动检车表格
        private void createEMUC_Table()
        {
            //创建Excel文件名称
            FileStream fs = File.Create(Application.StartupPath + "\\" + startPath + "\\" + DateTime.Now.AddDays(1).ToString("yyyy.MM.dd") + "动检车.xls");
            //创建工作薄
            IWorkbook workbook = new HSSFWorkbook();

            //标题
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.WrapText = true;
            boldStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            boldStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
            boldStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
            HSSFFont fontBold = (HSSFFont)workbook.CreateFont();
            fontBold.FontName = "宋体";//字体  
            fontBold.FontHeightInPoints = 11;//字号  
            fontBold.IsBold = true;//加粗  
            boldStyle.SetFont(fontBold);

            //表格样式
            ICellStyle stoppedStationStyle = workbook.CreateCellStyle();
            stoppedStationStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            stoppedStationStyle.FillPattern = FillPattern.SolidForeground;
            stoppedStationStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            stoppedStationStyle.WrapText = true;
            stoppedStationStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedStationStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedStationStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedStationStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedStationStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            stoppedStationStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
            stoppedStationStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

            //表格样式
            ICellStyle stoppedTrainNumberStyle = workbook.CreateCellStyle();
            stoppedTrainNumberStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            stoppedTrainNumberStyle.FillPattern = FillPattern.SolidForeground;
            stoppedTrainNumberStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            stoppedTrainNumberStyle.WrapText = true;
            stoppedTrainNumberStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedTrainNumberStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedTrainNumberStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedTrainNumberStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedTrainNumberStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            stoppedTrainNumberStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
            stoppedTrainNumberStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

            //表格样式
            ICellStyle stoppedSmallStyle = workbook.CreateCellStyle();
            stoppedSmallStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            stoppedSmallStyle.FillPattern = FillPattern.SolidForeground;
            stoppedSmallStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            stoppedSmallStyle.WrapText = true;
            stoppedSmallStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedSmallStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedSmallStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedSmallStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            stoppedSmallStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            stoppedSmallStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
            stoppedSmallStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

            //停运字体
            //始发站-终到站字体
            HSSFFont stopped_stationsFont = (HSSFFont)workbook.CreateFont();
            stopped_stationsFont.FontName = "宋体";//字体  
            stopped_stationsFont.FontHeightInPoints = 13;//字号  
            stopped_stationsFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
            //stoppedTrainStyle.SetFont(stopped_stationsFont);

            //车次到时股道开时字体
            HSSFFont stopped_trainNumberFont = (HSSFFont)workbook.CreateFont();
            stopped_trainNumberFont.FontName = "Times New Roman";//字体  
            stopped_trainNumberFont.FontHeightInPoints = 15;//字号  
            stopped_trainNumberFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
            //stoppedTrainStyle.SetFont(stopped_trainNumberFont);

            //小字体
            HSSFFont stopped_smallNumberFont = (HSSFFont)workbook.CreateFont();
            stopped_smallNumberFont.FontName = "黑体";//字体  
            stopped_smallNumberFont.FontHeightInPoints = 9;//字号  
            stopped_smallNumberFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
            //stoppedTrainStyle.SetFont(stopped_smallNumberFont);

            stoppedStationStyle.SetFont(stopped_stationsFont);
            stoppedTrainNumberStyle.SetFont(stopped_trainNumberFont);
            stoppedSmallStyle.SetFont(stopped_smallNumberFont);

            ICellStyle normalStationStyle = workbook.CreateCellStyle();
            normalStationStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStationStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStationStyle.WrapText = true;
            normalStationStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStationStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStationStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalStationStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
            normalStationStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

            ICellStyle normalTrainNumberStyle = workbook.CreateCellStyle();
            normalTrainNumberStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalTrainNumberStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            normalTrainNumberStyle.WrapText = true;
            normalTrainNumberStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalTrainNumberStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalTrainNumberStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalTrainNumberStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
            normalTrainNumberStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

            ICellStyle normalSmallNumberStyle = workbook.CreateCellStyle();
            normalSmallNumberStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalSmallNumberStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            normalSmallNumberStyle.WrapText = true;
            normalSmallNumberStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalSmallNumberStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalSmallNumberStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalSmallNumberStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
            normalSmallNumberStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

            //普通字体
            //始发站-终到站字体
            HSSFFont stationsFont = (HSSFFont)workbook.CreateFont();
            stationsFont.FontName = "宋体";//字体  
            stationsFont.FontHeightInPoints = 13;//字号  
            //stoppedTrainStyle.SetFont(stationsFont);

            //车次到时股道开时字体
            HSSFFont trainNumberFont = (HSSFFont)workbook.CreateFont();
            trainNumberFont.FontName = "Times New Roman";//字体  
            trainNumberFont.FontHeightInPoints = 15;//字号  
            //stoppedTrainStyle.SetFont(trainNumberFont);

            //小字体
            HSSFFont smallNumberFont = (HSSFFont)workbook.CreateFont();
            smallNumberFont.FontName = "黑体";//字体  
            smallNumberFont.FontHeightInPoints = 9;//字号  
            //stoppedTrainStyle.SetFont(smallNumberFont);

            normalStationStyle.SetFont(stationsFont);
            normalTrainNumberStyle.SetFont(trainNumberFont);
            normalSmallNumberStyle.SetFont(smallNumberFont);

            //创建sheet
            ISheet sheet = workbook.CreateSheet("基本图");
            for (int i = 0; i < 2 + allEmuCheckModel.Count; i++)
            {
                IRow row = sheet.CreateRow(i);
                if (i == 0)
                {
                    for (int count = 0; count < 16; count++)
                    {
                        row.CreateCell(count);
                        row.GetCell(count).CellStyle = boldStyle;
                    }
                    //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 7));
                    row.Height = 18 * 20;
                    row.CreateCell(0).SetCellValue(DateTime.Now.AddDays(1).ToString("yyyy.MM.dd") + "日动检车和零高车全图");
                    row.GetCell(0).CellStyle = boldStyle;
                }
                else if (i == 1)
                {
                    row.Height = 18 * 20;
                    for (int count = 0; count < 8; count++)
                    {
                        switch (count)
                        {
                            case 0:
                                row.CreateCell(count).SetCellValue("序号");
                                sheet.SetColumnWidth(count, 8 * 256);
                                break;
                            case 1:
                                row.CreateCell(count).SetCellValue("备注");
                                sheet.SetColumnWidth(count, 20 * 256);
                                break;
                            case 2:
                                row.CreateCell(count).SetCellValue("始发站");
                                sheet.SetColumnWidth(count, 16 * 256);
                                break;
                            case 3:
                                row.CreateCell(count).SetCellValue("车次");
                                sheet.SetColumnWidth(count, 16 * 256);
                                break;
                            case 4:
                                row.CreateCell(count).SetCellValue("到时");
                                sheet.SetColumnWidth(count, 16 * 256);
                                break;
                            case 5:
                                row.CreateCell(count).SetCellValue("股道");
                                sheet.SetColumnWidth(count, 13 * 256);
                                break;
                            case 6:
                                row.CreateCell(count).SetCellValue("开时");
                                sheet.SetColumnWidth(count, 16 * 256);
                                break;
                            case 7:
                                row.CreateCell(count).SetCellValue("终到站");
                                sheet.SetColumnWidth(count, 16 * 256);
                                break;
                        }
                        row.GetCell(count).CellStyle = boldStyle;
                    }
                }
                else
                {
                    row.Height = 18 * 20;
                    for (int column = 0; column < 8; column++)
                    {
                        switch (column)
                        {
                            case 0:
                                if (allEmuCheckModel[i - 2].id > 0)
                                {
                                    row.CreateCell(column).SetCellValue(allEmuCheckModel[i - 2].id);
                                }
                                else
                                {
                                    row.CreateCell(column).SetCellValue(" ");
                                }
                                row.GetCell(column).CellStyle = boldStyle;
                                break;
                            case 1:
                                if (allEmuCheckModel[i - 2].streamStatus != 0)
                                {
                                    row.CreateCell(column).SetCellValue(allEmuCheckModel[i - 2].extra);
                                    row.GetCell(column).CellStyle = boldStyle;
                                }
                                else
                                {
                                    row.CreateCell(column).SetCellValue("停运");
                                    row.GetCell(column).CellStyle = stoppedStationStyle;
                                }
                                break;
                            case 2:
                                row.CreateCell(column).SetCellValue(allEmuCheckModel[i - 2].startStation);
                                if (allEmuCheckModel[i - 2].streamStatus != 0)
                                {
                                    row.GetCell(column).CellStyle = normalStationStyle;
                                }
                                else
                                {
                                    row.GetCell(column).CellStyle = stoppedStationStyle;
                                }
                                break;
                            case 3:
                                string trainNumber = allEmuCheckModel[i - 2].trainNumber;
                                row.CreateCell(column).SetCellValue(trainNumber);
                                if (allEmuCheckModel[i - 2].streamStatus != 0)
                                {
                                    row.GetCell(column).CellStyle = normalTrainNumberStyle;
                                }
                                else
                                {
                                    row.GetCell(column).CellStyle = stoppedTrainNumberStyle;
                                }
                                break;
                            case 4:
                                row.CreateCell(column).SetCellValue(allEmuCheckModel[i - 2].stopTime);
                                if(allEmuCheckModel[i - 2].stopTime == null)
                                {
                                    break;
                                }
                                if (!Regex.IsMatch(allEmuCheckModel[i - 2].stopTime, @"[\u4e00-\u9fa5]") && allEmuCheckModel[i - 2].streamStatus != 0)
                                {//没中文-不停运
                                    row.GetCell(column).CellStyle = normalTrainNumberStyle;
                                }
                                if(!Regex.IsMatch(allEmuCheckModel[i - 2].stopTime, @"[\u4e00-\u9fa5]") && allEmuCheckModel[i - 2].streamStatus == 0)
                                {//没中文-停运
                                    row.GetCell(column).CellStyle = stoppedTrainNumberStyle;
                                }
                                if (Regex.IsMatch(allEmuCheckModel[i - 2].stopTime, @"[\u4e00-\u9fa5]") && allEmuCheckModel[i - 2].streamStatus != 0)
                                {//有中文-不停运
                                    row.GetCell(column).CellStyle = normalSmallNumberStyle;
                                }
                                if (Regex.IsMatch(allEmuCheckModel[i - 2].stopTime, @"[\u4e00-\u9fa5]") && allEmuCheckModel[i - 2].streamStatus == 0)
                                {//有中文-停运
                                    row.GetCell(column).CellStyle = stoppedSmallStyle;
                                }

                                break;
                            case 5:
                                int outTrackNum = 0;
                                int.TryParse(allEmuCheckModel[i - 2].trackNum, out outTrackNum);
                                if (outTrackNum == 0)
                                {
                                    row.CreateCell(column).SetCellValue(allEmuCheckModel[i - 2].trackNum);
                                }
                                else
                                {
                                    row.CreateCell(column).SetCellValue(outTrackNum);
                                }
                                if (allEmuCheckModel[i - 2].streamStatus != 0)
                                {
                                    row.GetCell(column).CellStyle = normalTrainNumberStyle;
                                }
                                else
                                {
                                    row.GetCell(column).CellStyle = stoppedTrainNumberStyle;
                                }
                                break;
                            case 6:
                                row.CreateCell(column).SetCellValue(allEmuCheckModel[i - 2].startTime);
                                if (allEmuCheckModel[i - 2].startTime == null)
                                {
                                    break;
                                }
                                if (!Regex.IsMatch(allEmuCheckModel[i - 2].startTime, @"[\u4e00-\u9fa5]") && allEmuCheckModel[i - 2].streamStatus != 0)
                                {//没中文-不停运
                                    row.GetCell(column).CellStyle = normalTrainNumberStyle;
                                }
                                if (!Regex.IsMatch(allEmuCheckModel[i - 2].startTime, @"[\u4e00-\u9fa5]") && allEmuCheckModel[i - 2].streamStatus == 0)
                                {//没中文-停运
                                    row.GetCell(column).CellStyle = stoppedTrainNumberStyle;
                                }
                                if (Regex.IsMatch(allEmuCheckModel[i - 2].startTime, @"[\u4e00-\u9fa5]") && allEmuCheckModel[i - 2].streamStatus != 0)
                                {//有中文-不停运
                                    row.GetCell(column).CellStyle = normalSmallNumberStyle;
                                }
                                if (Regex.IsMatch(allEmuCheckModel[i - 2].startTime, @"[\u4e00-\u9fa5]") && allEmuCheckModel[i - 2].streamStatus == 0)
                                {//有中文-停运
                                    row.GetCell(column).CellStyle = stoppedSmallStyle;
                                }
                                break;
                            case 7:
                                row.CreateCell(column).SetCellValue(allEmuCheckModel[i - 2].destination);
                                if (allEmuCheckModel[i - 2].streamStatus != 0)
                                {
                                    row.GetCell(column).CellStyle = normalStationStyle;
                                }
                                else
                                {
                                    row.GetCell(column).CellStyle = stoppedStationStyle;
                                }
                                break;
                        }

                    }
                }
            }

            //向excel文件中写入数据并保保存
            workbook.Write(fs);
            fs.Close();
            System.Diagnostics.ProcessStartInfo info1 = new System.Diagnostics.ProcessStartInfo();
            //info.WorkingDirectory = Application.StartupPath;
            info1.FileName = Application.StartupPath + "\\" + startPath + "\\";
            info1.Arguments = "";
            try
            {
                System.Diagnostics.Process.Start(info1);
            }
            catch (System.ComponentModel.Win32Exception we)
            {
                MessageBox.Show(this, we.Message);
                return;
            }
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            //info.WorkingDirectory = Application.StartupPath;
            info.FileName = Application.StartupPath + "\\" + startPath + "\\" + DateTime.Now.AddDays(1).ToString("yyyy.MM.dd") + "动检车.xls";
            info.Arguments = "";
            try
            {
                System.Diagnostics.Process.Start(info);
            }
            catch (System.ComponentModel.Win32Exception we)
            {
                MessageBox.Show(this, we.Message);
                return;
            }
        }

        //动车所填车型
        private void trainTypeAutoComplete()
        {
            IWorkbook workbook = null;  //新建IWorkbook对象  
            string fileName = ExcelFile[0];

            try
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                {
                    try
                    {
                        workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                    }
                    catch (Exception e)
                    {
                        if (File.Exists(Application.StartupPath + "\\动车所时刻表\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1]))
                        {
                            MessageBox.Show("时刻表文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            string pLocalFilePath = Application.StartupPath + "\\动车所时刻表\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//要复制的文件路径
                            string pSaveFilePath = fileName;//指定存储的路径
                            File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                            fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                            workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
                        }
                        else
                        {
                            MessageBox.Show("时刻表文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\动车所时刻表\\");
                            return;
                        }
                    }

                }
                else if (fileName.IndexOf(".xls") > 0) // 2003版本  
                {
                    try
                    {
                        workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
                    }
                    catch (Exception e)
                    {
                        if (File.Exists(Application.StartupPath + "\\动车所时刻表\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1]))
                        {
                            MessageBox.Show("时刻表文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            string pLocalFilePath = Application.StartupPath + "\\动车所时刻表\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//要复制的文件路径
                            string pSaveFilePath = fileName;//指定存储的路径
                            File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                            fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                            workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
                        }
                        else
                        {
                            MessageBox.Show("时刻表文件出现损坏（或时刻表无效）\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\动车所时刻表\\");
                            return;
                        }
                    }
                }

                if (workbook != null && !fileName.Contains("自动备份-"))
                {
                    string pLocalFilePath = fileName.ToString();//要复制的文件路径
                    string pSaveFilePath = Application.StartupPath + "\\动车所时刻表\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//指定存储的路径
                    File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换

                }

                //表格样式
                ICellStyle normalStyle = workbook.CreateCellStyle();
                normalStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                normalStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                normalStyle.WrapText = true;
                normalStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                normalStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                normalStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                normalStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                HSSFFont fontNormal = (HSSFFont)workbook.CreateFont();
                fontNormal.FontName = "宋体";//字体  
                fontNormal.FontHeightInPoints = 16;//字号  
                normalStyle.SetFont(fontNormal);

                ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  
                IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  
                string failedLoadingTrain = "";
                for (int i = 0; i <= sheet.LastRowNum; i++)  //对工作表每一行  
                {
                    row = sheet.GetRow(i);   //row读入第i行数据  
                    if (row != null)
                    {
                        Regex regexOnlyNumAndAlphabeta = new Regex(@"^[A-Za-z0-9]+$");
                        for (int j = 0; j <= row.LastCellNum; j++)  //对工作表每一列  
                        {
                            if (row.GetCell(j) != null)
                            {
                                if ((row.GetCell(j).ToString().Contains("G") ||
                                    row.GetCell(j).ToString().Contains("D") ||
                                    row.GetCell(j).ToString().Contains("C") ||
                                    row.GetCell(j).ToString().Contains("J")) &&
                                    regexOnlyNumAndAlphabeta.IsMatch(row.GetCell(j).ToString().Trim()))
                                {
                                    if (row.GetCell(j).ToString().Contains("0G1288"))
                                    {
                                        int IJ = 0;
                                    }
                                    if (row.GetCell(j + 1) == null)
                                    {
                                        row.CreateCell(j + 1);
                                    }
                                    row.GetCell(j + 1).SetCellValue("");
                                    bool hasGotIt = false;
                                    foreach (CommandModel model in commandModel)
                                    {
                                        if (row.GetCell(j).ToString().Trim().Equals(model.trainNumber) ||
                                            row.GetCell(j).ToString().Trim().Equals(model.secondTrainNumber))
                                        {
                                            hasGotIt = true;
                                            if ((row.GetCell(j + 2) == null && row.GetCell(j + 3) == null) ||
                                            (row.GetCell(j + 2).ToString().Length == 0 && row.GetCell(j + 3).ToString().Length == 0))
                                            {
                                                //说明是和别人共用一格 但是在下面（目标单元格被挡住了）所以往上挪一行填
                                                row = sheet.GetRow(i - 1);
                                                if (row.GetCell(j + 1) == null)
                                                {
                                                    row.CreateCell(j + 1);
                                                }
                                            }
                                            if (model.streamStatus == 0)
                                            {
                                                row.GetCell(j + 1).SetCellValue("停开");
                                            }
                                            else
                                            {
                                                row.GetCell(j + 1).SetCellValue(model.trainId);
                                            }
                                            row.GetCell(j + 1).CellStyle = normalStyle;
                                        }
                                    }
                                    if (!hasGotIt)
                                    {
                                        if (commandText.Contains(row.GetCell(j).ToString().Trim()))
                                        {
                                            failedLoadingTrain = failedLoadingTrain + " " + row.GetCell(j).ToString().Trim();
                                            searchAndHightlightUnresolvedTrains(row.GetCell(j).ToString().Trim(), 2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (failedLoadingTrain.Length != 0)
                {
                    MessageBox.Show("请人工检查以下车次是否未填写（在客调令中已标红）：\n" + failedLoadingTrain, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //重新修改文件指定单元格样式
                FileStream fs1 = File.OpenWrite(fileName);
                workbook.Write(fs1);
                fs1.Close();
                Console.ReadLine();
                fileStream.Close();
                workbook.Close();
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
                //info.WorkingDirectory = Application.StartupPath;
                info.FileName = fileName;
                info.Arguments = "";
                try
                {
                    System.Diagnostics.Process.Start(info);
                }
                catch (System.ComponentModel.Win32Exception we)
                {
                    MessageBox.Show(this, we.Message);
                    return;
                }
            }
            catch (IOException)
            {
                MessageBox.Show("时刻表文件正在使用中，请关闭后重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        /*
        //赶点计算器
        private void TrainEarlyCaculator_Btn_Click(object sender, EventArgs e)
        {
            string fileName = ExcelFile[0];
            if (caculatorModel == null ||
                caculatorModel.Count == 0 ||
                !filePath.Equals(fileName.ToString()))
            {
                if (!startCaculator())
                {//返回false 即模型内无内容
                    MessageBox.Show("未匹配到车次，赶点车次为18点以后的回库车 以及全天的旅客列车。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            filePath = fileName.ToString();
            TrainEarlyCaculator form = new TrainEarlyCaculator(caculatorModel);
            form.Show();
        }

        
        public bool startCaculator()
        {
            IWorkbook workbook = null;  //新建IWorkbook对象  
            string fileName = ExcelFile[0];
            //车次统计
            int allTrainsCount = 0;
            int allPsngerTrainsCount = 0;
            int stoppedTrainsCount = 0;
            int allTrainsInTimeTable = 0;
            List<CaculatorModel> _caculatorModelList = new List<CaculatorModel>();
            try
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                {
                    try
                    {
                        workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("时刻表文件出现损坏（或时刻表无效），请杀毒并更换备份文件-位于\\时刻表->backup内，点击确定打开）\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\" + startPath + "\\");
                        return false;
                    }

                }
                else if (fileName.IndexOf(".xls") > 0) // 2003版本  
                {
                    try
                    {
                        workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("时刻表文件出现损坏（或时刻表无效），请杀毒并更换备份文件-位于\\时刻表->backup内，点击确定打开）\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\" + startPath + "\\");
                        return false;
                    }
                }

                if (workbook != null)
                {
                    string pLocalFilePath = fileName.ToString();//要复制的文件路径
                    string pSaveFilePath = Application.StartupPath + "\\" + startPath + "\\自动备份-" + fileName.ToString().Split('\\')[fileName.ToString().Split('\\').Length - 1];//指定存储的路径
                    File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换

                }

                ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  
                IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  
                for (int i = 0; i < sheet.LastRowNum; i++)  //对工作表每一行  
                {
                    row = sheet.GetRow(i);   //row读入第i行数据  
                    if (row != null)
                    {
                        for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (row.GetCell(j).ToString().Contains("G") ||
                                    row.GetCell(j).ToString().Contains("D") ||
                                    row.GetCell(j).ToString().Contains("C") ||
                                    row.GetCell(j).ToString().Contains("J"))
                                {//把车次表格先去字
                                    if (!row.GetCell(j).ToString().Contains("由") &&
                                        !row.GetCell(j).ToString().Contains("续") &&
                                        !row.GetCell(j).ToString().Contains("开行"))
                                    {
                                        //时刻表中车次+1
                                        allTrainsInTimeTable++;
                                        //去中文后再找-去掉高峰-周末-临客等字
                                        row.GetCell(j).SetCellValue(Regex.Replace(row.GetCell(j).ToString(), @"[\u4e00-\u9fa5]", ""));
                                    }
                                    else
                                    {
                                        //这个格子不是要找的
                                        continue;
                                    }
                                    //若遍历后都没有找到 停运+1
                                    bool ContainsTrainNumber = false;
                                    bool GotTheTrain = false;
                                    foreach (CommandModel model in commandModel)
                                    {//找到了-判断是否符合计入赶点统计
                                        if (row.GetCell(j).ToString().Trim().Equals(model.trainNumber) ||
                                            row.GetCell(j).ToString().Trim().Equals(model.secondTrainNumber))
                                        {
                                            ContainsTrainNumber = true;
                                            //车次统计+1
                                            allTrainsCount++;
                                            if (!row.GetCell(j).ToString().Trim().Contains("0G") &&
                                                !row.GetCell(j).ToString().Trim().Contains("0D") &&
                                                !row.GetCell(j).ToString().Trim().Contains("0J") &&
                                                !row.GetCell(j).ToString().Trim().Contains("0C") &&
                                                !row.GetCell(j).ToString().Trim().Contains("00") &&
                                                !row.GetCell(j).ToString().Trim().Contains("DJ"))
                                            {
                                                allPsngerTrainsCount++;
                                            }
                                            if (!row.GetCell(j).ToString().Trim().Contains("0J") &&
                                                !row.GetCell(j).ToString().Trim().Contains("DJ"))
                                            {//0J和DJ不加入
                                                for (int p = j; p < row.LastCellNum; p++)
                                                {//找车次的右边 有没有股道数 找到股道数时 左右两边为该车次的图定时间
                                                    int res = 0;
                                                    if (row.GetCell(p) != null)
                                                    {
                                                        if (int.TryParse(row.GetCell(p).ToString(), out res))
                                                        {
                                                            if (res > 0 && res < 33)
                                                            {//找到股道了，应当break这个for，添加模型(添加时再判断是不是18点以后回库车，是不是旅客列车)
                                                                if (model.streamStatus != 0)
                                                                {//停运的不加进去
                                                                    CaculatorModel tempModel = addToCaculatorModel(model.trainNumber, row.GetCell(p - 1).ToString().Trim(), row.GetCell(p + 1).ToString().Trim());
                                                                    if (tempModel.trainNumber != null)
                                                                    {
                                                                        _caculatorModelList.Add(tempModel);
                                                                    }
                                                                }
                                                                GotTheTrain = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (GotTheTrain)
                                                {
                                                    break;
                                                }
                                            }
                                            if (model.streamStatus != 1)
                                                stoppedTrainsCount++;
                                        }
                                    }
                                    if (!ContainsTrainNumber)
                                    {
                                        stoppedTrainsCount++;
                                    }
                                }
                            }
                        }
                    }
                }
                //重新修改文件指定单元格样式
                FileStream fs1 = File.OpenWrite(fileName);
                workbook.Write(fs1);
                fs1.Close();
                Console.ReadLine();
                fileStream.Close();
                workbook.Close();
                //显示车次总数
                AllTrainsCountLBL.Text = allTrainsCount.ToString();
                AllPsngerTrainsCountLBL.Text = allPsngerTrainsCount.ToString();
                stoppedTrainsCountLBL.Text = stoppedTrainsCount.ToString();
                AllTrainsInTimeTableLBL.Text = allTrainsInTimeTable.ToString();
                caculatorModel = _caculatorModelList;
                if (caculatorModel.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (IOException)
            {
                MessageBox.Show("时刻表文件正在使用中，请关闭后重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private CaculatorModel addToCaculatorModel(string trainNumber, string shouldArriveTime, string shouldStartTime)
        {//判断是否符合赶点车条件，符合则加入model，返回成功/失败
            CaculatorModel _caculatorModel = new CaculatorModel();
            //筛掉18点以前的出库入库车
            int res = 0;
            if (trainNumber.Contains("0G") ||
                trainNumber.Contains("0C") ||
                trainNumber.Contains("0J") ||
                trainNumber.Contains("0D"))
            {//入库回库车
                int.TryParse(shouldStartTime.Split(':')[0], out res);
                if (res > 1 && res < 18)
                {
                    return _caculatorModel;
                }
                else
                {//18点以后只计算回库车
                    if (trainNumber.ToCharArray()[trainNumber.Length - 1].Equals('0') ||
                        trainNumber.ToCharArray()[trainNumber.Length - 1].Equals('2') ||
                        trainNumber.ToCharArray()[trainNumber.Length - 1].Equals('4') ||
                        trainNumber.ToCharArray()[trainNumber.Length - 1].Equals('6') ||
                        trainNumber.ToCharArray()[trainNumber.Length - 1].Equals('8'))
                    {
                        _caculatorModel.trainNumber = trainNumber;
                        _caculatorModel.shouldArriveTime = shouldArriveTime;
                        _caculatorModel.shouldStartTime = shouldStartTime;
                        return _caculatorModel;
                    }
                }

            }
            else
            {//如果不是回库车 就筛掉始发车-终到车
                if (!shouldArriveTime.Contains(":") ||
                    !shouldStartTime.Contains(":"))
                {
                    return _caculatorModel;
                }
                _caculatorModel.trainNumber = trainNumber;
                _caculatorModel.shouldArriveTime = shouldArriveTime;
                _caculatorModel.shouldStartTime = shouldStartTime;
                return _caculatorModel;
            }
            return _caculatorModel;
        }
*/

        private void contextMenuForTextBox_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                粘贴ToolStripMenuItem.Enabled = true;
            }
            else
                粘贴ToolStripMenuItem.Enabled = false;

            ((RichTextBox)contextMenuStrip1.SourceControl).Paste();
            //command_rTb.Paste(); //粘贴
        }



        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)contextMenuStrip1.SourceControl).Clear();
            //command_rTb.Clear(); //清空
        }

        private void 复制toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string selectText = ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }

        private void search_tb_TextChanged(object sender, EventArgs e)
        {
            //右方显示框内容
            String commands = "";
            List<CommandModel> _allModels = new List<CommandModel>();
            string searchText = search_tb.Text.ToString().Trim();
            searchText = searchText.ToUpper();
            if (commandModel == null)
            {
                return;
            }
            if (searchText.Length == 0)
            {
                if (wrongTrain != null)
                {
                    if (wrongTrain.Length != 0)
                    {
                        searchResult_tb.Text = "识别错误车辆：" + "\r\n" + wrongTrain;
                        return;
                    }
                }
            }
            for (int i = 0; i < commandModel.Count; i++)
            {
                CommandModel model = commandModel[i];
                if (model.trainNumber.Contains(searchText) ||
                    model.secondTrainNumber.Contains(searchText))
                {
                    String streamStatus = "";
                    String trainType = "";
                    if (model.streamStatus == 1)
                    {
                        streamStatus = "开行";
                    }
                    else
                    {
                        streamStatus = "停运";
                    }
                    switch (model.trainType)
                    {
                        case 0:
                            trainType = "普通列车";
                            break;
                        case 1:
                            trainType = "高峰";
                            break;
                        case 2:
                            trainType = "临客";
                            break;
                        case 3:
                            trainType = "周末";
                            break;
                    }
                    if (model.secondTrainNumber.Equals("null"))
                    {
                        commands = commands + "第" + model.trainIndex + "条-" + model.trainNumber + "-" + streamStatus + "-" + trainType + "\r\n";
                    }
                    else
                    {
                        commands = commands + "第" + model.trainIndex + "条-" + model.trainNumber + "-" + model.secondTrainNumber + "-" + streamStatus + "-" + trainType + "\r\n";
                    }
                    _allModels.Add(model);
                }
            }
            searchResult_tb.Text = "共" + _allModels.Count.ToString() + "趟" + "\r\n" + commands;
        }

        private void FontSize_tb_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FontSize_tb_TextChanged(object sender, EventArgs e)
        {
            int _fontSize = 0;
            int.TryParse(FontSize_tb.Text, out _fontSize);
            if (_fontSize != 0)
            {
                fontSize = _fontSize;
            }
        }

        private void yesterdayCommand_rtb_TextChanged(object sender, EventArgs e)
        {
            yesterdayCommandText = yesterdayCommand_rtb.Text;
            if (yesterdayCommand_rtb.Text.Length != 0)
            {
                if (yesterdayCommand_rtb.Text.Length != lastYesterdayCommandLength)
                    analyseCommand(true);
                startBtnCheck();
            }
            else
            {
                startBtnCheck();
            }
            lastYesterdayCommandLength = yesterdayCommand_rtb.Text.Length;
        }

        private void compare_btn_Click(object sender, EventArgs e)
        {
            if (commandModel.Count != 0 && yesterdayCommandModel.Count != 0 && hasFilePath && hasText && ExcelFile.Count > 0)
            {//对比
                readBasicTrainTable(true);
            }
            else
            {

            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
