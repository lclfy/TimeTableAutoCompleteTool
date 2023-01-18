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
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Collections.Specialized;
using TimeTableAutoCompleteTool.Models;
using SiEncrypt;
using NPOI.HSSF.Util;

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
        private List<DailySchedule> yesterdayAllDailyScheduleModel;
        private List<EMUCheckModel> allEmuCheckModel;
        private List<TrainProjectModel> allTrainProjectModels;
        private List<EMUGarageTableModel> allEMUGarageTableModels;
        List<string> ExcelFile = new List<string>();
        //综控班计划对比用的
        string yesterdayExcelFile = "";
        List<string> EMUGarageFile = new List<string>();
        private string startPath = "";
        private string wrongTrain = "";
        private string commandText = "";
        private int lastCommandLength = 0;
        //动车所的昨天命令框拉伸到274像素，综控为164
        private string yesterdayCommandText = "";
        //动车所情况特殊 有无昨日均可制作
        private bool EMUGarage_hasYesterdayText = false;
        private int lastYesterdayCommandLength = 0;
        //统计加载到哪个文件了
        int fileCounter = 0;
        int fontSize = 12;
        string filePath = "";
        string addedTrainText = "";
        //调车作业计划辅助
        private string trainProjectFile = "";
        private bool hasTrainProjectFile = false;
        private string trainProjectText = "";
        //morning == 0 night == 1
        private int morningOrNight = -1;
        //行车1，综控2，动车所3；
        public int modeSelect;
        //开行信息分析文本
        public string operationChangedAnalyse = "";
        //接续列车发现问题文本
        public string continueTrainAnalyse = "";
        float dpiX, dpiY;
        bool automaticDeleteStoppedTrains = true;

        string developer = "反馈请联系17638570597（罗思聪）\n*亦可联系黄楠/高雅雯";
        string upStations = "京广-（新乡东 安阳东 鹤壁东 邯郸东 石家庄 保定东 定州东 正定机场 邢台东 高碑店东 涿州东 北京西）石地区-（太原南 定州东 阳泉北 石家庄东 藁城南 辛集南 衡水北 景州 德州东 平原东 禹城东 齐河）京沪北-（北京南 廊坊 天津西 天津 天津南 沧州西 德州东 泰安 曲阜东 滕州东 枣庄）徐兰-（ 开封北 兰考南 商丘 永城北 砀山南 萧县北 徐州东）京沪南-（ 宿州东 蚌埠南 定远 滁州 南京南 南京 镇江南 丹阳北 常州北 无锡东 苏州 苏州北 昆山南 上海 上海虹桥）胶济-（济南西 威海 荣成 胶州北 高密 潍坊 昌乐 青州市 淄博 周村东 章丘 济南东 烟台 青岛北 青岛） 城际-（宋城路）  京东北-（ 辽阳 铁岭西 开原西 昌图西 四平东 公主岭南 长春西 德惠西 扶余北 双城北 哈尔滨西 秦皇岛 沈阳北 沈阳 承德南 承德 怀柔南 朝阳 大连北 长春 哈尔滨西 ） 郑东南-（肥东 巢北 黄庵 全椒 江浦 黄山北 金华南 宁波 杭州东 温州南 义乌 松江南 金山北 嘉善南 嘉兴南 桐乡 海宁西 余杭 ） ";
        string downStations = "郑州 郑州西 京广-（ 许昌东 漯河西 驻马店西 信阳东 明港东 孝感北 武汉 汉口 咸宁北 赤壁北 岳阳东 汨罗东 长沙南 株洲西 衡山西 衡阳东 耒阳西 郴州西 韶关 英德西 清远 广州北 深圳北 福田 深圳北 广州南 庆盛 虎门 光明城 西九龙 珠海）城际-（ 新郑机场 焦作）徐兰-（ 巩义南 洛阳龙门 三门峡西 灵宝西 华山北 渭南北 临潼东 西安北 汉中 宝鸡南 天水南 秦安 通渭 定西北 榆中 兰州西）西南-（ 成都东 重庆西 重庆北 贵阳北 昆明南 南宁东 怀化南 湘潭北 韶山南 芷江 新晃西 娄底南 桂林 玉溪 宜昌东 恩施 襄阳北 汉川 天门南 仙桃西 潜江 荆州 枝江北 湛江西）东南-（ 黄冈东 萍乡北 新余北 宜春东 鹰潭北 南昌西 九江  赣州西 厦门北 潮汕 漳州 惠州南）郑万-（长葛北 禹州 郏县 平顶山西 方城 邓州东 南阳东 襄阳东 南漳 保康县 神农架 兴山 巴东北 巫山 奉节 云阳 万州北） 郑合-（许昌北 鄢陵 扶沟南 西华 周口东 淮阳南 沈丘北 界首南 临泉 阜阳西）";
        string[] allEMUGarageTracks = {"1G", "2G", "3G", "4G1", "4G2", "5G1", "5G2", "6G1", "6G2", "7G1", "7G2", "8G1", "8G2", "9G1", "9G2", "10G1", "10G2", "11G1", "11G2", "12G1", "12G2", "13G1", "13G2",
        "14G", "15G","16G1", "16G2","17G1", "17G2","18G1", "18G2","19G", "20G","21G1", "21G2","22G", "23G","24G", "25G","26G", "27G","28G", "29G","30G", "31G","32G", "33G1", "33G2","34G1", "34G2",
        "35G1", "35G2","36G1", "36G2","37G1", "37G2","38G1", "38G2","39G1", "39G2","40G1", "40G2","41G1", "41G2","42G1", "42G2","43G", "44G","45G1", "45G2","46G1", "46G2","47G1", "47G2","48G1", "48G2"
        ,"49G1", "49G2","50G1", "50G2","51G1", "51G2","52G1", "52G2","53G1", "53G2","54G1", "54G2","55G1", "55G2","56G1", "56G2","57G1", "57G2","58G1", "58G2","59G1", "59G2","60G1", "60G2","61G1", "61G2"
        ,"62G1", "62G2","63G1", "63G2","64G1", "64G2","65G1", "65G2","66G1", "66G2","67G1", "67G2","68G1", "68G2","69G1", "69G2","70G", "71G","72G"};
        string build = "build 84 - v20230118";
        string readMe = "build84更新内容:\n" +
            "修复大令bug，“删除停运车”改为默认不勾选";
        //综控可以读取07版Excel（运转仅03版）
        public Main()
        {
            InitializeComponent();
        }

        //检查激活状态
        /*
        private void checkRegist()
        {
            SiEncryptForm _encryptForm = new SiEncryptForm();
            _encryptForm.Show();
            _encryptForm.Hide();
            if (!_encryptForm.isRegist)
            {
                _encryptForm.ShowDialog();
                System.Environment.Exit(System.Environment.ExitCode);
                this.Hide();
            }
        }
        */

        private void Main_Load(object sender, EventArgs e)
        {
            //checkRegist();
            Graphics graphics = this.CreateGraphics();
            dpiX = graphics.DpiX;
            dpiY = graphics.DpiY;
            this.Size = new Size((int)(1033*(dpiX/96)),(int)(595*(dpiY/96)));
            this.Text = "客调命令辅助工具";
            buildLBL.Text = build;
            start_Btn.Enabled = false;
            dataAnalyse_btn.Enabled = false;
            compare_btn.Enabled = false;
            //TrainEarlyCaculator_Btn.Enabled = false;
            load();
            checkedChanged();
            contentOfDeveloper.IsBalloon = true;
            updateReadMe.IsBalloon = false;
            updateReadMe.AutoPopDelay = 60000;
            updateReadMe.AutomaticDelay = 60000;
            updateReadMe.InitialDelay = 0;
            updateReadMe.ReshowDelay = 0;
            // Force the ToolTip text to be displayed whether or not the form is active.
            updateReadMe.ShowAlways = true;
            updateReadMe.SetToolTip(this.buildLBL, readMe);
            FontSize_tb.Text = fontSize.ToString();
            checkBox1.Checked = false;
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

        private void checkedChanged()
        {
            if (radioButton1.Checked)
            {
                checkBox1.Visible = true;
                EMUorEMUC_groupBox.Visible = false;
                compareDailySchedue_btn.Visible = false;
                label111.Visible = true;
                label222.Visible = true;
                rightGroupBox.Visible = true;
                rightGroupBox_Compare.Visible = false;
                rightGroupBox_EMUGarage.Visible = false;
                FontSize_tb.Visible = true;
                yesterdayExcelFile = "";
                operationChangedAnalyse = "";
                continueTrainAnalyse = "";
                modeSelect = 1;
                startPath = "时刻表";
                secondStepText_lbl.Text = "2.选择时刻表文件【框选所有时刻表!】";
                developerLabel.Text = "";
                start_Btn.Text = "处理时刻表";
                ExcelFile = new List<string>();
                start_Btn.Enabled = false;
                filePath = "";
                filePathLBL.Text = "已选择：";
                Size _size = new Size(Convert.ToInt32(210 * (dpiX/96)), Convert.ToInt32(345 * (dpiY/96)));
                outputTB.Size = _size;
                searchResult_tb.Size = _size;
                hint_label.Text = "绿色为开行，红色为停开，蓝色为调令未含车次，黄色为次日接入车次。高峰/临客/周末在车次前含有标注\n*每次调图更换新时刻表后，请检查“加开车次”部分是否有误。";
            }
            else if (radioButton2.Checked)
            {
                checkBox1.Visible = false;
                EMUorEMUC_groupBox.Visible = true;
                compareDailySchedue_btn.Visible = true;
                yesterdayCommandText = "";
                yesterdayExcelFile = "";
                operationChangedAnalyse = "";
                continueTrainAnalyse = "";
                yesterdayCommandModel = new List<CommandModel>();
                EMUGarage_YesterdayCommand_rtb.Text = "";
                yesterdayCommand_rtb.Text = "";
                developerLabel.Text = developer;
                startBtnCheck();
                radioButton4.Checked = true;
                label111.Visible = false;
                label222.Visible = false;
                FontSize_tb.Visible = false;
                rightGroupBox.Visible = false;
                rightGroupBox_Compare.Visible = true;
                rightGroupBox_EMUGarage.Visible = false;
                modeSelect = 2;
                startPath = "基本图";
                secondStepText_lbl.Text = "2.选择基本图文件";
                start_Btn.Text = "创建班计划";
                ExcelFile = new List<string>();
                start_Btn.Enabled = false;
                filePath = "";
                filePathLBL.Text = "已选择：";
                hint_label.Text = "基本图中没有的车次不会显示！无序号白色为客调令多出车次，红色标注为客调停开车次。请进行人工核对。";
            }
            else if (radioButton3.Checked)
            {
                checkBox1.Visible = false;
                //归零调车计划文件
                trainProjectFile = "";
                compareDailySchedue_btn.Visible = false;
                hasTrainProjectFile = false;
                trainProjectText = "";
                yesterdayExcelFile = "";
                trainPorjectFilePath_lbl.Text = "";
                operationChangedAnalyse = "";
                continueTrainAnalyse = "";
                EMUorEMUC_groupBox.Visible = false;
                yesterdayCommandText = "";
                developerLabel.Text = " ";
                yesterdayCommandModel = new List<CommandModel>();
                EMUGarage_YesterdayCommand_rtb.Text = "";
                yesterdayCommand_rtb.Text = "";
                startBtnCheck();
                label111.Visible = false;
                label222.Visible = false;
                FontSize_tb.Visible = false;
                rightGroupBox.Visible = false;
                rightGroupBox_Compare.Visible = false ;
                rightGroupBox_EMUGarage.Visible = true;
                modeSelect = 3;
                startPath = "动车所时刻表";
                //<作业计划优化辅助工具>\n（首先补全车型-并在右侧选择计划）
                secondStepText_lbl.Text = "2.选择动车所时刻表";
                hint_label.Text = "时刻表中浅黄色标注为在60G条件下，最佳出库走行线与存场股道不匹配的车。\n夜班空股道为：在晚间未占用过的股道；或回库后存放没有超过凌晨的，在凌晨之前就进入检修库的车所曾占用但夜间空闲的股道。";
                start_Btn.Text = "补全车辆信息";
                label18.Text = "动车所：选择动车段给出的计划文件\n(请从值班室拷贝word,可在时刻表中标记股道)";
                ExcelFile = new List<string>();
                filePathLBL.Text = "已选择：";
                matchTrackWithTrain_Project_btn.Enabled = false;
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


        private void yesterdayCommand_rtb_TextChanged(object sender, EventArgs e)
        {//综控室的昨日命令
            yesterdayCommandText = yesterdayCommand_rtb.Text;
            yesterdayCommandModel = new List<CommandModel>();
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

        private void EMUGarage_YesterdayCommand_rtb_TextChanged(object sender, EventArgs e)
        {//动车所的昨日命令
            yesterdayCommandText = EMUGarage_YesterdayCommand_rtb.Text;
            yesterdayCommandModel = new List<CommandModel>();
            if (EMUGarage_YesterdayCommand_rtb.Text.Length != 0)
            {
                if (EMUGarage_YesterdayCommand_rtb.Text.Length != lastYesterdayCommandLength)
                    analyseCommand(true);
                startBtnCheck();
            }
            else
            {
                startBtnCheck();
            }
            lastYesterdayCommandLength = EMUGarage_YesterdayCommand_rtb.Text.Length;
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
                dataAnalyse_btn.Enabled = true;
                //TrainEarlyCaculator_Btn.Enabled = true;
            }
            else if(hasText)
            {
                dataAnalyse_btn.Enabled = true;
                //TrainEarlyCaculator_Btn.Enabled = false;
            }
            else
            {
                dataAnalyse_btn.Enabled = false;
                start_Btn.Enabled = false;
            }
            //综控班计划对比
            if (hasFilePath)
            {
                compareDailySchedue_btn.Enabled = true;
            }
            else
            {
                compareDailySchedue_btn.Enabled = false;
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
            /*
            try
            */
            {
                //把streamstatus为4的清除
                analyseCommand();
                if (commandModel.Count != 0 && radioButton1.Checked)
                {
                    if (FontSize_tb.Text.Length == 0)
                    {
                        FontSize_tb.Text = "12";
                    }
                    fileCounter = 0;
                    updateTimeTable();

                }
                else if (commandModel.Count != 0 && radioButton2.Checked)
                {
                    if (radioButton4.Checked)
                    {
                        readBasicTrainTable();
                    }
                    else if (radioButton5.Checked)
                    {
                        readEMUCTable();
                    }
                }
                else if (commandModel.Count != 0 && radioButton3.Checked)
                {
                    if (EMUGarage_YesterdayCommand_rtb.Text.Length == 0)
                    {
                        EMUGarage_hasYesterdayText = false;
                    }
                    else if (yesterdayCommandModel.Count != 0)
                    {
                        EMUGarage_hasYesterdayText = true;
                    }
                    //动车所的初版 受代码所限暂时先这么写…带true的是获取车次 开车方向和动车走行线的部分
                    if (allTrainProjectModels != null && allTrainProjectModels.Count != 0)
                    {
                        trainTypeAutoComplete(true);
                    }
                    trainTypeAutoComplete();
                }
                else
                {
                    MessageBox.Show("未检测到任何车次信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            /*
            catch (Exception ee)
            {
                MessageBox.Show("出现错误：" + ee.ToString().Split('。')[0], "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            */

        }

        private void analyseCommand(bool isYesterday = false, string detectedTrainRow = "", bool isDetecting = false)
        {   //分析客调命令
            //删除不需要的标点符号-字符
            int addedTrainCount = 0;
            //出现“临客”后为高峰车
            bool isRushHourTrain = false;
            //try
            {
                string wrongNumber = "";
                List<string> _commands = removeUnuseableWord(isYesterday);
                String[] AllCommand;
                if (!isDetecting)
                {//不是抽样调查
                    //所有\n前面加上句号
                    string testStr = _commands[0];
                    testStr = testStr.Replace('\n', '。').Replace("。。", "。"); ;
                    AllCommand = testStr.Split('。');
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
                    if (AllCommand[i].Contains("临客"))
                    {
                        isRushHourTrain = true;
                        AllCommand[i].Replace("临客", "");
                    }
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
                            try
                            {
                                addedTrainText = addedTrainText + addedTrainCount + "、" + addedCommand.Split('：')[addedCommand.Split('：').Length - 1].Remove(0, 2) + "。\n";
                            }
                            catch(Exception ex)
                            {

                            }

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
                        if (isRushHourTrain)
                        {
                            trainType = 2;
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
                         //对于命令中含有时间的，Regex.Replace(X:XX && XX:XX)即可去除
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
                //高峰车次精确查找 20200110
                int rushHourTrain = 0;
                int tempTrain = 0;
                int weekendTrain = 0;
                int addedTrain = 0;
                if (AllModels != null && AllModels.Count > 0)
                {
                    //重新遍历命令，先选出之前标注为高峰临客的车逐一寻找并确定
                    for(int cmCount = 0; cmCount < AllModels.Count; cmCount++)
                    {
                        CommandModel _tempCM = AllModels[cmCount];
                        if(_tempCM.trainType == 0)
                        {
                            continue;
                        }
                        //找命令
                        for(int ij = 0; ij < AllCommand.Length; ij++)
                        {
                            bool hasGotIt = false;
                            string[] command;
                            /*
                            243、2018年10月27日，CRH380AL-2595：G74-G9782(高峰线)-G9781(高峰线)-0G9782(高峰线)，0G74(停运)。
                             252、2019年06月01日，CRH380AL - 2595：G74 - 0G74(停运) - G9196(高峰线) - G9195(高峰线) - 0G9196(高峰线)
                            303、2019年06月01日，CRH380B - 5754：高峰线 - G4292 - G4291。
                            311、2019年07月10日，CRH380AL-2606：高峰线-0G4567-G4568-G4567-0G4568。
                            296、2019年07月10日，CRH380A-2664+2705：周末线0G9201(高峰线)-G9202(高峰线)-0G6695(停运)-G6695-G6696-G6697-G6698-G6699-G6700-0G6700。
                             296、2019年07月10日，CRH380A-2664+2705：0G9201(高峰线)-G9202(高峰线)-0G6695(停运)-G6695-G6696-G6697-G6698-G6699-G6700(高峰线)-0G6700。
                            */
                            //先切块
                            command = AllCommand[ij].Split('：');
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
                                command[1] = command[1].Trim();
                                if (command[1].Contains(_tempCM.trainNumber) || command[1].Contains(_tempCM.secondTrainNumber))
                                {//找到对应行，先看下车次本身是否有标注，有标注的直接标记并跳过，无标注的判断是否一整行都是，是的话保留标注，否则取消标注
                                    string[] spCommand = command[1].Split('-');
                                    for (int spCount = 0; spCount < spCommand.Length; spCount++)
                                    {
                                        if (spCommand[spCount].Split('（')[0].Split('/')[0].Equals(_tempCM.trainNumber) || spCommand[spCount].Split('（')[0].Split('/')[0].Equals(_tempCM.secondTrainNumber))
                                        {
                                            if (spCommand[spCount].Contains("高峰"))
                                            {//当前车次被标注
                                                AllModels[cmCount].trainType = 1;
                                                rushHourTrain++;
                                                hasGotIt = true;
                                                break;
                                            }
                                            else if (spCommand[spCount].Contains("临客"))
                                            {
                                                AllModels[cmCount].trainType = 2;
                                                tempTrain++;
                                                hasGotIt = true;
                                                break;
                                            }
                                            else if (spCommand[spCount].Contains("周末"))
                                            {
                                                AllModels[cmCount].trainType = 3;
                                                weekendTrain++;
                                                hasGotIt = true;
                                                break;
                                            }
                                            else if (spCommand[spCount].Contains("加开"))
                                            {
                                                AllModels[cmCount].trainType = 4;
                                                addedTrain++;
                                                hasGotIt = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (!hasGotIt)
                                    {
                                        if (command[1].Contains("高峰-") ||
                                   command[1].Contains("高峰G") ||
                                   command[1].Contains("高峰D") ||
                                   command[1].Contains("高峰C") ||
                                   command[1].Contains("高峰0") ||
                                   command[1].Contains("高峰线-") ||
                                   command[1].Contains("高峰线G") ||
                                   command[1].Contains("高峰线D") ||
                                   command[1].Contains("高峰线C") ||
                                   command[1].Contains("高峰线0"))
                                        {//有整行标注
                                            AllModels[cmCount].trainType = 1;
                                            rushHourTrain++;
                                        }
                                        else if (command[1].Contains("周末-") ||
                                   command[1].Contains("周末G") ||
                                   command[1].Contains("周末D") ||
                                   command[1].Contains("周末C") ||
                                   command[1].Contains("周末0") ||
                                   command[1].Contains ("周末线-") ||
                                   command[1].Contains("周末线G") ||
                                   command[1].Contains("周末线D") ||
                                   command[1].Contains("周末线C") ||
                                   command[1].Contains("周末线0"))
                                        {
                                            AllModels[cmCount].trainType = 3;
                                            weekendTrain++;
                                        }
                                        else if ( command[1].Contains("临客G") ||
                                   command[1].Contains("临客D") ||
                                   command[1].Contains("临客C") ||
                                   command[1].Contains("临客0") ||
                                   command[1].Contains("临客-") ||
                                   command[1].Contains("临客线-") ||
                                   command[1].Contains("临客线G") ||
                                   command[1].Contains("临客线D") ||
                                   command[1].Contains("临客线C") ||
                                   command[1].Contains("临客线0"))
                                        {
                                            AllModels[cmCount].trainType = 2;
                                            tempTrain++;
                                        }
                                        else
                                        {//不是整行标注，则取消标注
                                            AllModels[cmCount].trainType = 0;
                                        }
                                        hasGotIt = true;
                                    }
                                }
                            }
                            if (hasGotIt)
                            {
                                break;
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
                        streamStatus = "√开";
                    }
                    else
                    {
                        streamStatus = "×停";
                    }
                    switch (model.trainType)
                    {
                        case 0:
                            trainType = "";
                            break;
                        case 1:
                            trainType = "-高峰";
                            break;
                        case 2:
                            trainType = "-临客";
                            break;
                        case 3:
                            trainType = "-周末";
                            break;
                    }
                    if (model.secondTrainNumber.Equals("null"))
                    {
                        commands = commands + "第" + model.trainIndex.Trim() + "条-" + model.trainNumber + "-"+model.trainModel+"-"+model.trainId+"-" + streamStatus + trainType + "\r\n";
                    }
                    else
                    {
                        commands = commands + "第" + model.trainIndex.Trim() + "条-" + model.trainNumber + "/" + model.secondTrainNumber + "-" + model.trainModel +"-"+ model.trainId + "-" + streamStatus  + trainType + "\r\n";
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
                if(!isDetecting)
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
            //catch (Exception e)
            {
                //MessageBox.Show("出现错误：" + e.ToString().Split('。')[0], "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    standardCommand = yesterdayCommandText;
                }
            }
            else
            {
                standardCommand = detectedCommand;
            }
            List<string> commands = new List<string>();
            standardCommand = removing(standardCommand.Trim());
            /*
            if (!isYesterday)
            {
                command_rTb.Text = standardCommand;
            }
            else
            {
                yesterdayCommand_rtb.Text = standardCommand;
                yesterdayCommandText = standardCommand;
            }
            */
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
            //（1）2022年xxx（2）2022年xxx，在前面加上个“\n”以准确识别
            standardCommand = standardCommand.Replace("(1)、2", "\n(1)、2");
            standardCommand = standardCommand.Replace("(2)、2", "\n(2)、2");
            standardCommand = standardCommand.Replace("(3)、2", "\n(3)、2");
            standardCommand = standardCommand.Replace("(4)、2", "\n(4)、2");
            standardCommand = standardCommand.Replace("(5)、2", "\n(5)、2");
            standardCommand = standardCommand.Replace("(6)、2", "\n(6)、2");
            standardCommand = standardCommand.Replace("(7)、2", "\n(7)、2");
            standardCommand = standardCommand.Replace("(8)、2", "\n(8)、2");
            standardCommand = standardCommand.Replace("(9)、2", "\n(9)、2");
            standardCommand = standardCommand.Replace("(10)、2", "\n(10)、2");
            if (standardCommand.Contains("临时定点列车："))
                standardCommand = standardCommand.Replace("临时定点列车：", "临客");
            if (standardCommand.Contains("担当局："))
                standardCommand = standardCommand.Replace("担当局：", "。");
            if (standardCommand.Contains("1\t2"))
                standardCommand = standardCommand.Replace("1\t2", "1、2");
            if (standardCommand.Contains("2\t2"))
                standardCommand = standardCommand.Replace("2\t2", "2、2");
            if (standardCommand.Contains("3\t2"))
                standardCommand = standardCommand.Replace("3\t2", "3、2");
            if (standardCommand.Contains("4\t2"))
                standardCommand = standardCommand.Replace("4\t2", "4、2");
            if (standardCommand.Contains("5\t2"))
                standardCommand = standardCommand.Replace("5\t2", "5、2");
            if (standardCommand.Contains("6\t2"))
                standardCommand = standardCommand.Replace("6\t2", "6、2");
            if (standardCommand.Contains("7\t2"))
                standardCommand = standardCommand.Replace("7\t2", "7、2");
            if (standardCommand.Contains("8\t2"))
                standardCommand = standardCommand.Replace("8\t2", "8、2");
            if (standardCommand.Contains("9\t2"))
                standardCommand = standardCommand.Replace("9\t2", "9、2");
            if (standardCommand.Contains("0\t2"))
                standardCommand = standardCommand.Replace("0\t2", "0、2");
            if (standardCommand.Contains("1道"))
                standardCommand = standardCommand.Replace("1道", "");
            if (standardCommand.Contains("I道"))
                standardCommand = standardCommand.Replace("I道", "");
            if (standardCommand.Contains("2道"))
                standardCommand = standardCommand.Replace("2道", "");
            if (standardCommand.Contains("3道"))
                standardCommand = standardCommand.Replace("3道", "");
            if (standardCommand.Contains("4道"))
                standardCommand = standardCommand.Replace("4道", "");
            if (standardCommand.Contains("5道"))
                standardCommand = standardCommand.Replace("5道", "");
            if (standardCommand.Contains("6道"))
                standardCommand = standardCommand.Replace("6道", "");
            if (standardCommand.Contains("7道"))
                standardCommand = standardCommand.Replace("7道", "");
            if (standardCommand.Contains("8道"))
                standardCommand = standardCommand.Replace("8道", "");
            if (standardCommand.Contains("9道"))
                standardCommand = standardCommand.Replace("9道", "");
            if (standardCommand.Contains("0道"))
                standardCommand = standardCommand.Replace("0道", "");
            if (standardCommand.Contains("V道"))
                standardCommand = standardCommand.Replace("V道", "");
            if (standardCommand.Contains("X道"))
                standardCommand = standardCommand.Replace("X道", "");
            if (standardCommand.Contains("车："))
                standardCommand = standardCommand.Replace("车：", "");

            string s1 = string.Empty;
            foreach (char c in standardCommand)
            {
                if (c == '\t' )
                {
                    continue;
                }
                s1 += c;
            }
            standardCommand = s1;
            if (standardCommand.Contains(","))
                standardCommand = standardCommand.Replace(",", "");
            if (standardCommand.Contains("~"))
                standardCommand = standardCommand.Replace("~", "");
            if (standardCommand.Contains("～"))
                standardCommand = standardCommand.Replace("～", "");
            if (standardCommand.Contains("〜"))
                standardCommand = standardCommand.Replace("〜", "");
            if (standardCommand.Contains("—"))
                standardCommand = standardCommand.Replace("—", "-");
            if (standardCommand.Contains("签发："))
                standardCommand = standardCommand.Replace("签发：", "");
            if (standardCommand.Contains("会签："))
                standardCommand = standardCommand.Replace("会签：", "");
            if (standardCommand.Contains("–"))
                standardCommand = standardCommand.Replace("–", "-");
            
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
            if (standardCommand.Contains("GG"))
                standardCommand = standardCommand.Replace("GG", "G");
            if (standardCommand.Contains("00G"))
                standardCommand = standardCommand.Replace("00G", "0G");
            if (standardCommand.Contains("DD"))
                standardCommand = standardCommand.Replace("DD", "D");
            if (standardCommand.Contains("CC"))
                standardCommand = standardCommand.Replace("CC", "C");
            if (standardCommand.Contains("JJ"))
                standardCommand = standardCommand.Replace("JJ", "J");

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
            if (standardCommand.Contains("人："))
                standardCommand = standardCommand.Replace("人：", "");

            return standardCommand;
        }

        private List<CommandModel> trainModelAddFunc(String[] AllTrainNumberInOneRaw, int streamStatus, int trainType, string trainModel, string index)
        {//建立车次模型-通用方法
            //处理单程双车次车辆
            int trainConnectType = -1;
            string trainId = "";
            List<CommandModel> AllModels = new List<CommandModel>();
            //20210625-增加400AF-Z
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
                else if(trainModel.Contains("AF-B")||
                    trainModel.Contains("BF-B"))
                {//新增的 17节(AF-BZ BF-BZ)
                    trainConnectType = 3;
                }
                else if (trainModel.Contains("AF-Z") ||
                    trainModel.Contains("BF-Z")||
                    trainModel.Contains("AF-GZ") ||
                    trainModel.Contains("BF-GZ"))
                {
                    trainConnectType = 0;
                    int test = 0;
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
                if (trainModel.Contains("-AZ"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-AZ";
                }
                else if (trainModel.Contains("-BZ"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-BZ";
                }
                else if (trainModel.Contains("-A"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-A";
                }
                else if (trainModel.Contains("-B") && !trainModel.Contains("-BZ"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-B";
                }

                else if (trainModel.Contains("-Z"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-Z";
                }
                else if (trainModel.Contains("-G"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-G";
                }
                else
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim();
                }
            }
            else
            {
                if (trainModel.Contains("-A"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-A+";
                }
                else if (trainModel.Contains("-B"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-B+";
                }
                else if (trainModel.Contains("-Z"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-Z+";
                }
                else if (trainModel.Contains("-GZ"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-GZ+";
                }
                else if (trainModel.Contains("-G"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-G+";
                }
                else
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "+";
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
                    //230118，把车次前面的无关字符去掉，避免bug
                    Char[] TrainChar = AllTrainNumberInOneRaw[k].ToCharArray();
                    string tempWord = "";
                    for(int p = 0; p < TrainChar.Length; p++)
                    {
                        if (TrainChar[p].ToString().Contains("G")||
                            TrainChar[p].ToString().Contains("D") ||
                            TrainChar[p].ToString().Contains("C") ||
                            TrainChar[p].ToString().Contains("J") ||
                            TrainChar[p].ToString().Contains("00"))
                        {
                            //找到了，后面的都是车次
                            for(int u = p; u < TrainChar.Length; u++)
                            {
                                tempWord = tempWord + TrainChar[u];
                            }
                            break;
                        }
                    }
                    AllTrainNumberInOneRaw[k] = tempWord;
                    if (AllTrainNumberInOneRaw[k].Contains("/") && !AllTrainNumberInOneRaw[k].Contains("G/"))
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
                        if (m1.trainNumber.Contains("0J") ||
                        m1.trainNumber.Contains("DJ") ||
                        m1.trainNumber.Contains("0D") ||
                        m1.trainNumber.Contains("0G") ||
                        m1.trainNumber.Contains("0C"))
                        {
                            m1.psngerTrain = false;
                        }
                        else
                        {
                            m1.psngerTrain = true;
                        }
                        if (!IsTrainNumber(m1.trainNumber))
                        {
                            m1.trainNumber = "未识别-(" + m1.trainNumber + ")";
                        }
                        Char[] firstTrainWord = trainWithDoubleNumber[0].ToCharArray();
                        String secondTrainWord = "";
                        if(trainWithDoubleNumber.Length > 1)
                        {
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
                        if (model.trainNumber.Contains("0J") ||
                        model.trainNumber.Contains("DJ") ||
                        model.trainNumber.Contains("0D") ||
                        model.trainNumber.Contains("0G") ||
                        model.trainNumber.Contains("0C"))
                        {
                            model.psngerTrain = false;
                        }
                        else
                        {
                            model.psngerTrain = true;
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
        private void SelectPath(bool compareDailySchedue = false)
        {
            yesterdayExcelFile = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();   //显示选择文件对话框 
            if (radioButton2.Checked)
            {
                openFileDialog1.Filter = "Excel 文件 (*.xls,*.xlsx)|*.xls;*.xlsx";
            }
            else
            {
                openFileDialog1.Filter = "Excel 2003 文件 (*.xls)|*.xls";
            }
            if (!radioButton2.Checked)
            {
                openFileDialog1.InitialDirectory = Application.StartupPath + "\\" + startPath + "\\";
            }


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
                if (!compareDailySchedue)
                {
                    ExcelFile = new List<string>();
                    int fileCount = 0;
                    String fileNames = "已选择：";
                    foreach (string fileName in openFileDialog1.FileNames)
                    {
                        fileCount++;
                        ExcelFile.Add(fileName);
                    }

                    if (radioButton1.Checked || radioButton2.Checked)
                    {
                        this.filePathLBL.Text = "已选择：" + fileCount + "个文件";
                    }
                    else
                    {
                        this.filePathLBL.Text = "已选择：" + openFileDialog1.FileName;     //显示文件路径 
                    }
                    hasFilePath = true;
                }
                else
                {//综控班计划对比
                    foreach (string fileName in openFileDialog1.FileNames)
                    {
                        yesterdayExcelFile = fileName;
                        compareDailySchedues();
                    }
                }
                startBtnCheck();
            }
        }

        //202204读取时刻表表头内容
        //inputtype默认为1
        private TimeTable GetStationsFromCurrentTables(IWorkbook workbook, int _inputType = 1)
        {
            //通过标题寻找车站（线路所模糊匹配，东三场南站动车所精确匹配）
            List<TimeTable> _timeTables = new List<TimeTable>();
            int counter = 0;
            //foreach (IWorkbook workbook in _timeTablesWorkbooks)
            {
                TimeTable _timeTable = new TimeTable();
                string allStations = "";
                ISheet sheet = workbook.GetSheetAt(0);  //获取工作表  
                IRow row;

                for (int i = 0; i < sheet.LastRowNum; i++)  //对工作表每一行  
                {
                    row = sheet.GetRow(i);   //row读入第i行数据  
                    bool hasGotStationsRow = false;
                    if (row != null)
                    {
                        for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
                        {
                            if (row.GetCell(j) != null)
                            {
                                string titleName = "";
                                bool hasgot = false;
                                titleName = row.GetCell(j).ToString().Trim().Replace(" ", "");
                                if (_inputType == 0)
                                {
                                    if ((titleName.Contains("郑州东站") &&
                                   titleName.Contains("上行")) ||
                                  (titleName.Contains("郑州东站") &&
                                    titleName.Contains("下行")))
                                    {
                                        _timeTable.Title = "郑州东";
                                        _timeTable.titleRow = i;
                                        hasgot = true;
                                    }
                                }
                                else if (_inputType == 1)
                                {
                                    string[] _allStations = new string[] { "曹古寺", "二郎庙", "鸿宝", "郑州东京广场", "南曹", "寺后", "郑州东徐兰场", "郑开", "郑州南郑万场", "郑州东城际场", "郑州南城际场", "郑州东疏解区", "郑州东动车所", "郑州南动车所", "新郑机场", "周口东", "民权北", "兰考南", "开封北", "许昌北", "许昌东", "鄢陵", "扶沟南", "西华", "淮阳南", "沈丘北", "长葛北", "禹州", "郏县" };
                                    for (int ij = 0; ij < _allStations.Length; ij++)
                                    {
                                        if ((titleName.Contains(_allStations[ij]) &&
                                                                 titleName.Contains("上行")) ||
                                                                (titleName.Contains(_allStations[ij]) &&
                                                                titleName.Contains("下行")))
                                        {
                                            //表头与车站不符的特殊情况
                                            if (titleName.Contains("郑开"))
                                            {
                                                _timeTable.Title = "宋城路";
                                            }
                                            else
                                            {
                                                _timeTable.Title = _allStations[ij];
                                            }
                                            _timeTable.titleRow = i;
                                            break;
                                        }
                                    }
                                }
                                if (row.GetCell(j).ToString().Contains("始发") ||
                                    row.GetCell(j).ToString().Contains("备注") ||
                                     hasGotStationsRow)
                                {
                                    hasGotStationsRow = true;
                                    _timeTable.stationRow = i;
                                }
                                if (!row.GetCell(j).ToString().Trim().Replace(" ", "").Contains("时刻") &&
                                    row.GetCell(j).ToString().Length != 0)
                                {
                                    string currentStation = row.GetCell(j).ToString();
                                    if (currentStation.Contains("线路所"))
                                    {
                                        currentStation = currentStation.Replace("线路所", "");
                                    }
                                    if (currentStation.Contains("车站"))
                                    {
                                        currentStation = currentStation.Replace("车站", "车次");
                                    }
                                    if (currentStation.Contains("站"))
                                    {
                                        currentStation = currentStation.Replace("站", "");
                                    }

                                    if (currentStation.Contains("郑州东"))
                                    {
                                        //郑州南城际场修改
                                        /*
                                        if (currentStation.Equals("郑州东城际场"))
                                        {
                                            continue;
                                        }
                                        */
                                        if (_inputType == 0)
                                        {
                                            currentStation = "郑州东";
                                        }
                                        else if (_inputType == 1)
                                        {
                                            if (currentStation.Contains("郑州东京广场"))
                                            {
                                                currentStation = "郑州东京广场";
                                            }
                                            if (currentStation.Contains("郑州东城际场"))
                                            {
                                                currentStation = "郑州东城际场";
                                            }
                                            if (currentStation.Contains("郑州东徐兰场"))
                                            {
                                                currentStation = "郑州东徐兰场";
                                            }
                                        }
                                        //currentStation = currentStation.Replace("郑州东", "");
                                    }
                                    currentStation = currentStation.Trim();
                                    Stations_TimeTable _tempStation = new Stations_TimeTable();
                                    //此时需要找这个站是上行还是下行
                                    IRow titleRow = sheet.GetRow(_timeTable.titleRow);
                                    _tempStation.stationColumn = j;
                                    _tempStation.stationName = currentStation;
                                    if (titleRow != null)
                                    {
                                        bool hasGotData = false;
                                        for (int k = j; k >= 0; k--)
                                        {//往上找写了上下行的那行，往左找 直到找到字为止
                                            if (titleRow.GetCell(k) != null)
                                            {
                                                string cellInfo = titleRow.GetCell(k).ToString();
                                                if (cellInfo.Contains("上行"))
                                                {//说明是上行的
                                                    _tempStation.upOrDown = false;
                                                    hasGotData = true;
                                                }
                                                else if (cellInfo.Contains("下行"))
                                                {
                                                    _tempStation.upOrDown = true;
                                                    hasGotData = true;
                                                }
                                            }
                                            if (hasGotData)
                                            {
                                                break;
                                            }
                                        }
                                        if (!allStations.Contains(currentStation))
                                        {
                                            allStations = allStations + "-" + currentStation;
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("选定的列车时刻表表头不具有规定格式：“郑州东站…时刻表（上行）”或“（线路所）…时刻表（上行）”，不影响使用，请联系我（思聪）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return null;
                                    }
                                    //此时依然不能直接添加，需要寻找到达-股道-发出所在列
                                    IRow stopStartRow = sheet.GetRow(_timeTable.stationRow + 1);
                                    if (stopStartRow != null)
                                    {
                                        string cellInfo = "";
                                        if (stopStartRow.GetCell(j) != null)
                                        {
                                            cellInfo = stopStartRow.GetCell(j).ToString().Trim();

                                            if (cellInfo.Contains("通过") || cellInfo.Contains("发出"))
                                            {
                                                _tempStation.startedTimeColumn = j;
                                            }
                                            if (cellInfo.Contains("到达"))
                                            {
                                                _tempStation.stoppedTimeColumn = j;
                                            }
                                            if (cellInfo.Contains("股道"))
                                            {
                                                _tempStation.trackNumColumn = j;
                                            }
                                            //此时往右，再往上，看看get到的是不是自己，是的话就看是股道还是发出，直到不是的再退出循环
                                            for (int k = j + 1; k < stopStartRow.LastCellNum; k++)
                                            {//
                                                string nextCell = "";
                                                if (stopStartRow.GetCell(k) != null)
                                                {
                                                    nextCell = stopStartRow.GetCell(k).ToString().Trim();
                                                }
                                                IRow stationRow = sheet.GetRow(_timeTable.stationRow);
                                                if (stationRow != null)
                                                {
                                                    if (stationRow.GetCell(k) == null)
                                                    {
                                                        if (nextCell.Contains("股道"))
                                                        {
                                                            _tempStation.trackNumColumn = k;
                                                        }
                                                        else if (nextCell.Contains("发出"))
                                                        {
                                                            _tempStation.startedTimeColumn = k;
                                                        }
                                                    }
                                                    else if (stationRow.GetCell(k).ToString().Length == 0)
                                                    {
                                                        if (nextCell.Contains("股道"))
                                                        {
                                                            _tempStation.trackNumColumn = k;
                                                        }
                                                        else if (nextCell.Contains("发出"))
                                                        {
                                                            _tempStation.startedTimeColumn = k;
                                                        }
                                                    }
                                                    else
                                                    {//有字就不对了，应该跳出
                                                        break;
                                                    }
                                                }
                                            }

                                        }
                                        else
                                        {
                                            MessageBox.Show("选定的列车时刻表表头不具有规定格式：到达-股道-发出，不影响使用，请联系我（思聪）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("选定的列车时刻表表头不具有规定格式：到达-股道-发出，不影响使用，请联系我（思聪）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return null;
                                    }
                                    _timeTable.currentStations.Add(_tempStation);
                                }
                            }
                        }
                    }
                    if (hasGotStationsRow)
                    {
                        break;
                    }
                }
                //仅使用郑万时刻表，作为徐兰场使用
                if (_timeTable.Title.Contains("郑万") && _timeTables.Count == 1)
                {
                    _timeTable.Title = "徐兰";
                }
                else if (_timeTable.Title == null || _timeTable.Title.Length == 0)
                {
                    MessageBox.Show("选定的列车时刻表表头不具有规定格式：“郑州东站…时刻表（上行）”或“（线路所）…时刻表（上行）”，不影响使用，请联系我（思聪）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
                allStations = allStations.Remove(0, 1);
                _timeTable.stations = allStations.Split('-');

                _timeTable.timeTablePlace = counter;
                _timeTables.Add(_timeTable);
                counter++;
            }
            //20220411 只有一张时刻表，原设计是多张，不想改代码了，就凑合用吧
            TimeTable _tm = new TimeTable();
            if (_timeTables.Count > 0)
            {
                _tm = _timeTables[0];
            }
            return _tm;

        }



        //202204删除所需对应内容，目标行，起始列，结束列
        private IWorkbook deleteTargetRow(IWorkbook _workbook,int startColumn,int stopColumn,int targetRow)
        {

            ISheet sheet = _workbook.GetSheetAt(0);
            //for(int i = 0; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(targetRow);
                for(int j = startColumn; j <= stopColumn; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        row.GetCell(j).SetCellValue("");
                    }
                }
            }
            return _workbook;
        }

        //202204把空的部分从下挪上来，从上往下找，标题行之下，分两组（以表头“终到”为基准）
        //若有空的，标记该空行，新循环往下找，找到有车次号的，整行内容搬运过来，新行删除，再从原来找到的空行位置+1继续找
        //加开车次挪上来，整表刷格式
        private IWorkbook fixEmptyRows(IWorkbook workbook,List<int> stoppedStationAt,List<int> trainNumAt,int titleRow)
        {
            if(stoppedStationAt.Count == 0 || trainNumAt.Count == 0)
            {
                return workbook;
            }
            ISheet sheet = workbook.GetSheetAt(0);
            //把没有字的后面行都删掉（加开车次除外）
            int lastFoundRow = -1;
            //首次循环，找空行
            for (int i =titleRow + 1; i <= sheet.LastRowNum; i++)
            {
                if (sheet.GetRow(i) != null)
                    {
                        IRow row = sheet.GetRow(i);
                        //如果是空的（车次那一列被删了）
                        if (row.GetCell(trainNumAt[0]) == null)
                        {
                            continue;
                        }
                        if (row.GetCell(trainNumAt[0]).ToString().Length == 0)
                        {
                            //往下找内容，挪动到这一行
                            for(int next = i + 1; next <= sheet.LastRowNum; next++)
                            {
                                if (sheet.GetRow(next) == null)
                                {
                                    continue;
                                }
                                IRow nextRow = sheet.GetRow(next);
                                if(nextRow.GetCell(trainNumAt[0]) == null)
                                {
                                    continue;
                                }
                                if (nextRow.GetCell(trainNumAt[0]).ToString().Length != 0)
                                {
                                    //找每一列，挪动到上面
                                    //找到内容了，挪到上面，本行内容删除
                                    for (int j1 = 0;j1<= stoppedStationAt[0]; j1++)
                                    {
                                        row.GetCell(j1).SetCellValue(nextRow.GetCell(j1).ToString().Trim());
                                        nextRow.GetCell(j1).SetCellValue("");
                                    }
                                    //因为把字挪动到上面行了，因此上面行作为“最后找到有字的行”标识
                                    if (i > lastFoundRow)
                                    {
                                        lastFoundRow = i;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                if(stoppedStationAt.Count == 2 && trainNumAt.Count == 2)
                {
                    if (sheet.GetRow(i) != null)
                    {
                        IRow row = sheet.GetRow(i);
                        //如果是空的（车次那一列被删了）
                        if(row.GetCell(trainNumAt[1]) == null)
                        {
                            continue;
                        }
                        if (row.GetCell(trainNumAt[1]).ToString().Length == 0)
                        {
                            //往下找内容，挪动到这一行
                            for (int next = i + 1; next <= sheet.LastRowNum; next++)
                            {
                                if (sheet.GetRow(next) == null)
                                {
                                    continue;
                                }
                                IRow nextRow = sheet.GetRow(next);
                                if (nextRow.GetCell(trainNumAt[1]) == null)
                                {
                                    continue;
                                }
                                if (nextRow.GetCell(trainNumAt[1]).ToString().Length != 0)
                                {
                                    //找每一列，挪动到上面
                                    //找到内容了，挪到上面，本行内容删除
                                    for (int j2 = stoppedStationAt[0] + 1; j2 <= stoppedStationAt[1]; j2++)
                                    {
                                        row.GetCell(j2).SetCellValue(nextRow.GetCell(j2).ToString().Trim());
                                        nextRow.GetCell(j2).SetCellValue("");
                                    }
                                    //因为把字挪动到上面行了，因此上面行作为“最后找到有字的行”标识
                                    if(i > lastFoundRow)
                                    {
                                        lastFoundRow = i;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

            }
            //取消从第五行（标题以下）至最后有字的行（不包含加开车次）的合并单元格，需要先取消合并，再挪动
            int MergedCount = sheet.NumMergedRegions;
            for (int i = MergedCount - 1; i >= 0; i--)
            {
                /**
               CellRangeAddress对象属性有：FirstColumn，FirstRow，LastColumn，LastRow 进行操作 取消合并单元格
                **/
                var temp = sheet.GetMergedRegion(i);
                string t2 = temp.FirstRow.ToString();
                if(temp.FirstRow == 1 && temp.LastRow > 10)
                {
                    sheet.RemoveMergedRegion(i);
                }
                if (temp.FirstRow >= 4 && temp.FirstRow <= lastFoundRow)
                {
                    sheet.RemoveMergedRegion(i);
                }
            }
            //把下方空白区域直接删除,把加开车次挪到上面
            //此算法用于找到加开车次并挪动，已弃用（原有删除方法可以把加开车次自动挪上来）
            //把加开车次合并
            /*
            for (int addedTrainRow = 0; addedTrainRow <= sheet.LastRowNum; addedTrainRow++)
            {
                IRow row = sheet.GetRow(addedTrainRow);
                if (row.GetCell(0) != null)
                {
                    if (row.GetCell(0).ToString().Contains("加开车次"))
                    {
                        if(stoppedStationAt.Count > 1)
                        {
                            sheet.AddMergedRegion(new CellRangeAddress(addedTrainRow, addedTrainRow, 0, stoppedStationAt[1]));
                            sheet.AddMergedRegion(new CellRangeAddress(addedTrainRow + 1, addedTrainRow + 6, 0, stoppedStationAt[1]));
                        }
                        else
                        {
                            sheet.AddMergedRegion(new CellRangeAddress(addedTrainRow, addedTrainRow, 0, stoppedStationAt[0]));
                            sheet.AddMergedRegion(new CellRangeAddress(addedTrainRow + 1, addedTrainRow + 6, 0, stoppedStationAt[0]));
                        }

                    }
                }
            }
            */


            //删除空行
            int lastRow = sheet.LastRowNum;
                for (int ij = lastFoundRow + 1; ij <= lastRow; ij++)
                {
                    if (ij > sheet.LastRowNum - 1)
                    {
                        break;
                    }
                    if (sheet.GetRow(ij) == null)
                    {
                        sheet.ShiftRows(ij + 1, lastRow, -1);
                        ij = ij - 1;
                        lastRow = lastRow - 1;
                    }
                    else
                    {
                        if (sheet.GetRow(ij).GetCell(0) == null)
                        {
                            sheet.ShiftRows(ij + 1, lastRow, -1);
                            ij = ij - 1;
                            lastRow = lastRow - 1;
                        }
                        else
                        {
                            if (sheet.GetRow(ij).GetCell(0).ToString().Trim().Length == 0)
                            {
                                sheet.ShiftRows(ij + 1, lastRow, -1);
                                ij = ij - 1;
                                lastRow = lastRow - 1;
                            }
                        }
                    }

                }
            //202204找遗漏车：G1263
            /*
            for (int testR = 5; testR <= sheet.LastRowNum; testR++)
            {
                IRow testRow = sheet.GetRow(testR);
                for (int testC = 0; testC <= testRow.LastCellNum; testC++)
                {
                    if (testRow.GetCell(testC) != null)
                    {
                        if (testRow.GetCell(testC).ToString().Contains("G1263"))
                        {
                            bool found = true;
                        }
                    }
                }
            }
            */
            //把表的格式刷一下（用制表工具的方法）
            //用来给表格填斜杠用的
            //格式-标准
            ICellStyle standard = workbook.CreateCellStyle();
            standard.FillForegroundColor = HSSFColor.White.Index;
            standard.FillPattern = FillPattern.SolidForeground;
            standard.FillBackgroundColor = HSSFColor.White.Index;
            standard.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            standard.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            standard.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            standard.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            standard.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            HSSFFont standardFont = (HSSFFont)workbook.CreateFont();
            standardFont.FontName = "Times New Roman";//字体  
            standardFont.FontHeightInPoints = 15;//字号  
            standard.SetFont(standardFont);

            //格式-续开
            ICellStyle continuedTrainCell = workbook.CreateCellStyle();
            continuedTrainCell.FillForegroundColor = HSSFColor.White.Index;
            continuedTrainCell.FillPattern = FillPattern.SolidForeground;
            continuedTrainCell.FillBackgroundColor = HSSFColor.White.Index;
            continuedTrainCell.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            continuedTrainCell.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            continuedTrainCell.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            continuedTrainCell.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            continuedTrainCell.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            HSSFFont font9B = (HSSFFont)workbook.CreateFont();
            font9B.FontName = "黑体";//字体  
            font9B.FontHeightInPoints = 9;//字号  
            continuedTrainCell.SetFont(font9B);
            /*
            font.Underline = NPOI.SS.UserModel.FontUnderlineType.Double;//下划线  
            font.IsStrikeout = true;//删除线  
            font.IsItalic = true;//斜体  
            font.IsBold = true;//加粗  
            */

            //格式-起点终点
            ICellStyle startAndStop = workbook.CreateCellStyle();
            startAndStop.FillForegroundColor = HSSFColor.White.Index;
            startAndStop.FillPattern = FillPattern.SolidForeground;
            startAndStop.FillBackgroundColor = HSSFColor.White.Index;
            startAndStop.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            startAndStop.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            startAndStop.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            startAndStop.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            startAndStop.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            HSSFFont startAndStopFont = (HSSFFont)workbook.CreateFont();
            startAndStopFont.FontName = "宋体";//字体  
            startAndStopFont.FontHeightInPoints = 13;//字号  
            startAndStop.SetFont(startAndStopFont);

            //格式-车次
            ICellStyle trainNumberCell = workbook.CreateCellStyle();
            trainNumberCell.FillForegroundColor = HSSFColor.White.Index;
            trainNumberCell.FillPattern = FillPattern.SolidForeground;
            trainNumberCell.FillBackgroundColor = HSSFColor.White.Index;
            trainNumberCell.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            trainNumberCell.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            trainNumberCell.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            trainNumberCell.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            trainNumberCell.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            HSSFFont trainNumberFont = (HSSFFont)workbook.CreateFont();
            trainNumberFont.FontName = "Times New Roman";//字体  
            trainNumberFont.FontHeightInPoints = 14;//字号  
            trainNumberCell.SetFont(trainNumberFont);

            //斜杠格式
            ICellStyle empty = workbook.CreateCellStyle();
            empty.BorderDiagonalLineStyle = NPOI.SS.UserModel.BorderStyle.Thin;
            empty.BorderDiagonal = BorderDiagonal.Forward;
            empty.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            empty.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            empty.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            empty.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            empty.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            empty.TopBorderColor = HSSFColor.Black.Index;

            //仅左右有杠格式
            ICellStyle onlyLR = workbook.CreateCellStyle();
            onlyLR.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            onlyLR.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            onlyLR.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            onlyLR.TopBorderColor = HSSFColor.Black.Index;

            //空的加斜杠，不空的加空格，有“续”，“改”的，变小字
            for (int i = 4; i <= sheet.LastRowNum; i++)
            {
                if (sheet.GetRow(i) != null)
                {
                    IRow _row = sheet.GetRow(i);
                    int lastColumn = stoppedStationAt[0];
                    if(stoppedStationAt.Count > 1)
                    {
                        lastColumn = stoppedStationAt[1];
                    }
                    for (int j = 0; j <= lastColumn; j++)
                    {
                        if(_row.GetCell(j) == null)
                        {
                            _row.CreateCell(j);
                        }

                        if (!_row.GetCell(j).IsMergedCell)
                        {
                            if (_row.GetCell(j).ToString().Trim().Length == 0)
                            {
                                //上下行中间的位置不填斜杠
                                if(j != stoppedStationAt[0] + 1)
                                {
                                    _row.GetCell(j).CellStyle = empty;
                                }
                                else
                                {
                                    _row.GetCell(j).CellStyle = onlyLR;
                                }
                            }
                            else
                            {
                                _row.GetCell(j).CellStyle = startAndStop;
                            }
                        }
                        //"通过"合并
                        if (_row.GetCell(j).ToString().Trim().Equals("通过"))
                        {
                            //合并左中右三个格子，左格子写上“通过”
                            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                            sheet.AddMergedRegion(new CellRangeAddress(i, i, j, j+2));
                            if (_row.GetCell(j) == null)
                            {
                                _row.CreateCell(j);
                            }
                            if (_row.GetCell(j+2) == null)
                            {
                                _row.CreateCell(j+2);
                            }
                            _row.GetCell(j).SetCellValue("通过");
                            _row.GetCell(j).CellStyle = standard;
                            _row.GetCell(j+2).CellStyle = standard;
                        }
                        //用小字
                        if(_row.GetCell(j).ToString().Contains("改")||
                            _row.GetCell(j).ToString().Contains("续"))
                        {
                            _row.GetCell(j).CellStyle = continuedTrainCell;
                        }
                        if (j == trainNumAt[0])
                        {
                            _row.GetCell(j).CellStyle = trainNumberCell;
                        }
                        if (trainNumAt.Count > 1)
                        {
                            if (j == trainNumAt[1])
                            {
                                _row.GetCell(j).CellStyle = trainNumberCell;
                            }
                        }
                    }
                }

            }

            //避免标题被删除
            if (sheet.GetRow(0) == null)
                {
                    sheet.CreateRow(0);
                }
            return workbook;
        }

        //202204寻找各方向车进行统计
        private string findTrainsWay(IWorkbook workbook,TimeTable table,List<int> mainStationAt)
        {
            if(mainStationAt.Count == 0)
            {
                return "";
            }
            bool upOrDownChanged = false;
            int record_upOrDown = -1;
            List<string> texts = new List<string>();
            ISheet sheet = workbook.GetSheetAt(0);
            //先判断一个车站是上行还是下行，第几列，数量找出来，再找它和主站的位置关系（1左，1右2左，2右），通过上下行是否变换判断是去还是来
            for(int _tCount = 0; _tCount < table.currentStations.Count; _tCount++)
            {
                int trainCount = 0;
                string upOrDown = "";
                int targetStationColumn = -1;
                Stations_TimeTable _st = table.currentStations[_tCount];

                if (_st.stationName.Contains("始发")||
                    _st.stationName.Contains("终到") ||
                    _st.stationName.Contains("车站") ||
                    _st.stationName.Contains("车次"))
                {
                    continue;
                }
                if(_st.upOrDown == true)
                {
                    upOrDown = "下行";
                 
                }
                else
                {
                    upOrDown = "上行";
                }
                //判断上下行变了没
                if (record_upOrDown == -1)
                {
                    if (_st.upOrDown)
                    {
                        record_upOrDown = 1;
                    }
                    else
                    {
                        record_upOrDown = 0;
                    }
                }
                else
                {
                    int _uod = -1;
                    if (_st.upOrDown)
                    {
                        _uod = 1;
                    }
                    else
                    {
                        _uod = 0;
                    }
                    if (record_upOrDown != _uod)
                    {
                        upOrDownChanged = true;
                    }
                    record_upOrDown = _uod;
                }
                targetStationColumn = _st.startedTimeColumn;
                if(targetStationColumn == 0)
                {
                    targetStationColumn = _st.stoppedTimeColumn;
                }
                //获得了数据，开始找有多少车
                if (_st.stationName.Contains("动车所"))
                {
                    int test = 0;
                }
                for(int i = 4; i <= sheet.LastRowNum; i++)
                {
                    if(sheet.GetRow(i) == null)
                    {
                        continue;
                    }
                    IRow row = sheet.GetRow(i);
                    if (row.GetCell(targetStationColumn) == null ||
                        row.GetCell(targetStationColumn).ToString().Trim().Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        //统计数据+1
                        if(row.GetCell(targetStationColumn).ToString().Trim().Contains(":")||
                            row.GetCell(targetStationColumn).ToString().Trim().Contains("："))
                        {
                            trainCount++;
                        }

                    }
                }

                if(trainCount != 0)
                {
                    if(_st.stationColumn == mainStationAt[0])
                    {
                        continue;
                    }
                    if(mainStationAt.Count > 1)
                    {
                        if(_st.stationColumn == mainStationAt[1])
                        {
                            continue;
                        }
                    }
                    //判断方向
                    if (targetStationColumn < mainStationAt[0])
                    {
                        //xx方向来
                        bool hasGot = false;
                        for(int k = 0; k < texts.Count; k++)
                        {
                            if (hasGot)
                            {
                                break;
                            }
                            if (texts[k].Split('-')[0].Equals(_st.stationName))
                            {
                                texts[k] = texts[k] + "，"+_st.stationName + "-方向来" + trainCount + "列";
                                hasGot = true;
                            }
                        }
                        if (!hasGot)
                        {
                            texts.Add(_st.stationName + "-方向来" + trainCount + "列");
                        }
                    }
                    else if (mainStationAt.Count > 1)
                    {
                        if (targetStationColumn < mainStationAt[1])
                        {
                            //判断变了没
                            if (upOrDownChanged)
                            {
                                //变了，xx方向来
                                bool hasGot = false;
                                for (int k = 0; k < texts.Count; k++)
                                {
                                    if (hasGot)
                                    {
                                        break;
                                    }
                                    if (texts[k].Split('-')[0].Equals(_st.stationName))
                                    {
                                        texts[k] = texts[k] + "，" + _st.stationName + "-方向来" + trainCount + "列";
                                        hasGot = true;
                                    }
                                }
                                if (!hasGot)
                                {
                                    texts.Add(_st.stationName + "-方向来" + trainCount + "列");
                                }
                            }
                            else
                            {
                                //没变，去xx方向
                                bool hasGot = false;
                                for (int k = 0; k < texts.Count; k++)
                                {
                                    if (hasGot)
                                    {
                                        break;
                                    }
                                    if (texts[k].Split('-')[0].Equals(_st.stationName))
                                    {
                                        texts[k] = texts[k] + "，" + "去" + _st.stationName + "-方向" + trainCount + "列";
                                        hasGot = true;
                                    }
                                }
                                if (!hasGot)
                                {
                                    texts.Add("去" + _st.stationName + "-方向" + trainCount + "列");
                                }
                            }
                        }
                        else
                        {
                            //去xx方向
                            bool hasGot = false;
                            for (int k = 0; k < texts.Count; k++)
                            {
                                if (hasGot)
                                {
                                    break;
                                }
                                if (texts[k].Split('-')[0].Equals(_st.stationName))
                                {
                                    texts[k] = texts[k] + "，" + "去" + _st.stationName + "-方向" + trainCount + "列";
                                    hasGot = true;
                                }
                            }
                            if (!hasGot)
                            {
                                texts.Add("去" + _st.stationName + "-方向" + trainCount + "列");
                            }
                        }
                    }
                }
            }
            string text = "";
            foreach(string _t in texts)
            {
                text = text + _t.Replace("-","") + "\n";
            }
            return text;
        }

        //罗马数字转阿拉伯数字
        public int RomanToInt(string roman)
        {

            roman = roman.ToUpper();
            roman.Replace("_V", "S");
            roman.Replace("_X", "R");
            roman.Replace("_L", "Q");
            roman.Replace("_C", "P");
            roman.Replace("_D", "O");
            roman.Replace("_M", "N");

            int arabic = 0, sidx = 0;
            int len = roman.Length - 1;
            char[] data = roman.ToCharArray();

            while (len >= 0)
            {
                int i = 0;
                sidx = len;

                arabic += Convert2(data[sidx]);
                i++;
                len--;
            }

            return arabic;
        }

        private int Convert2(char c)
        {
            switch (c)
            {
                case 'I':
                    return 1;
                case 'V':
                    return 5;
                case 'X':
                    return 10;
                case 'L':
                    return 50;
                case 'C':
                    return 100;
                case 'D':
                    return 500;
                case 'M':
                    return 1000;
                case 'S':
                    return 5000;
                case 'R':
                    return 10000;
                case 'Q':
                    return 50000;
                case 'P':
                    return 100000;
                case 'O':
                    return 500000;
                case 'N':
                    return 1000000;
                default:
                    return 0;

            }
        }


        private void updateTimeTable()
        {
            
            operationChangedAnalyse = "";
            continueTrainAnalyse = "";
            if (ExcelFile == null)
            {
                MessageBox.Show("请重新选择时刻表文件~", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (string fileName in ExcelFile)
            {
                IWorkbook workbook = null;  //新建IWorkbook对象 
                                            //车次统计
                int allTrainsCount = 0;
                int allPsngerTrainsCount = 0;
                int stoppedTrainsCount = 0;
                int allTrainsInTimeTable = 0;
                //仅本场开行列车
                int onlyThisStationStartTrains = 0;
                int onlyThisStationStopTrains = 0;
                //京广场两个车次删掉一个
                bool hasFoundDJ5902 = false;
                bool hasFound0J5901 = false;
                //高峰临客周末统计
                int rushHourTrain = 0;
                int tempTrain = 0;
                int weekendTrain = 0;
                int addedTrain = 0;
                string checkedText = "";


                //未在客调中发现的车，临时储存。streamStatus为4
                List<CommandModel> notMatchedModels = new List<CommandModel>();
                try
                {
                    FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                    {
                        MessageBox.Show("提示：请将excel时刻表文件转存为2003版格式(.XLS)");
                    }
                    else if (fileName.IndexOf(".xls") > 0) // 2003版本  
                    {
                        try
                        {
                            workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
                        }
                        catch (Exception e)
                        {
                        
                            MessageBox.Show("时刻表文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }


                    //表格样式
                    HSSFFont font = (HSSFFont)workbook.CreateFont();
                    font.FontName = "宋体";//字体  
                    font.FontHeightInPoints = short.Parse(fontSize.ToString());//字号  
                    font.Color = NPOI.HSSF.Util.HSSFColor.White.Index;

                    ICellStyle nonMatchedTrainStype = workbook.CreateCellStyle();
                    nonMatchedTrainStype.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                    nonMatchedTrainStype.FillPattern = FillPattern.SolidForeground;
                    nonMatchedTrainStype.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                    nonMatchedTrainStype.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    nonMatchedTrainStype.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    nonMatchedTrainStype.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    nonMatchedTrainStype.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    HSSFFont nonMatchFont = (HSSFFont)workbook.CreateFont();
                    nonMatchFont.FontName = "宋体";//字体  
                    nonMatchFont.IsStrikeout = true;
                    nonMatchFont.IsBold = true;
                    nonMatchFont.IsItalic = true;
                    nonMatchFont.FontHeightInPoints = short.Parse(fontSize.ToString());//字号  
                    nonMatchedTrainStype.SetFont(nonMatchFont);

                    ICellStyle normalTrainStyle = workbook.CreateCellStyle();
                    //normalTrainStyle.FillPattern = FillPattern.SolidForeground;
                    normalTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                    normalTrainStyle.FillPattern = FillPattern.SolidForeground;
                    normalTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                    normalTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    normalTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    normalTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    normalTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    HSSFFont normalFont = (HSSFFont)workbook.CreateFont();
                    normalFont.FontName = "宋体";//字体  
                    normalFont.FontHeightInPoints = short.Parse(fontSize.ToString());//字号  
                    normalTrainStyle.SetFont(normalFont);

                    ICellStyle tomorrowlTrainStyle = workbook.CreateCellStyle();
                    tomorrowlTrainStyle.FillPattern = FillPattern.SolidForeground;
                    tomorrowlTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                    tomorrowlTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
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
                    //202204-上下行时刻表的终止位置，上下行所在位置（左边，右边）,主站所在列，时刻表生成工具算法
                    //算法：先把时刻表内容全部读取，将不包含的车相应位置内容清除，单写一个算法把下面的行都往上挪（郑州站系统里有这个算法）
                    //利用主站所在列，小于主站列的是在运行后方，大于主站列的是运行前方，可做统计
                    //额外保存，保存名称加上所制作的日期
                    string station = "";
                    //第一个，第二个“终到”
                    List<int> stoppedStationAt = new List<int>();
                    //第一个，第二个主站位置
                    List<int> mainStationAt = new List<int>();
                    TimeTable currentTimeTable = new TimeTable();
                    //因停运而被删除的车位置，格式为 "行-列"
                    List<string> removedTrains = new List<string>();
                    //车次位置，用来判断这一行有没有车
                    //车次列
                    List<int> trainNumColumnNum = new List<int>();
                    //股道列
                    List<int> trackNumColumnNum = new List<int>();

                    //标题行
                    int titleRowNum = -1;
                    if (title.Contains("京广"))
                    {
                        station = "京广";
                    }
                    else if (title.Contains("徐兰"))
                    {
                        station = "徐兰";
                    }
                    else if (title.Contains("东城际"))
                    {
                        station = "东城际";
                    }
                    else if (title.Contains("郑万"))
                    {
                        station = "郑万";
                    }
                    else if (title.Contains("郑阜"))
                    {
                        station = "郑阜";
                    }
                    else if (title.Contains("南城际"))
                    {
                        station = "南城际";
                    }
                    else if (title.Contains("寺后"))
                    {
                        station = "寺后";
                    }

                    //20220411读取时刻表内容
                    currentTimeTable = GetStationsFromCurrentTables(workbook);


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
                    //标题加上停运数
                   // sheet.GetRow(0).GetCell(0).SetCellValue(title);
                    //寻找加开车次字样，没有的创建
                    bool hasGotIt = false;
                    int lastCell = 0;
                    for(int searchRow = 0; searchRow <= sheet.LastRowNum; searchRow++)
                    {
                        if(!title.Contains("京广") && !title.Contains("徐兰"))
                        {
                            break;
                        }
                        IRow _searchRow = sheet.GetRow(searchRow);
                        if (_searchRow == null)
                        {
                            sheet.CreateRow(searchRow);
                            _searchRow = sheet.GetRow(searchRow);
                        }
                        if (_searchRow.LastCellNum > lastCell)
                        {
                            if (_searchRow.GetCell(_searchRow.LastCellNum) != null && _searchRow.GetCell(_searchRow.LastCellNum).ToString().Trim().Length != 0)
                            {
                                lastCell = _searchRow.LastCellNum;
                            }
                            else
                            {//找最后一列有字的
                                for (int reverise = _searchRow.LastCellNum; reverise > 0; reverise--)
                                {
                                    if (_searchRow.GetCell(reverise) != null && _searchRow.GetCell(reverise).ToString().Trim().Length != 0)
                                    {
                                        if (reverise > lastCell)
                                        {
                                            lastCell = reverise;
                                        }
                                        break;
                                    }
                                }
                            }

                        }
                        for (int searchColumn = 0; searchColumn <= _searchRow.LastCellNum; searchColumn++)
                        {
                            if (_searchRow.GetCell(searchColumn) != null)
                            {
                                if (_searchRow.GetCell(searchColumn).ToString().Contains("加开车次"))
                                {
                                    hasGotIt = true;
                                }
                            }
                            else
                            {
                                continue;
                            }
                            if (hasGotIt)
                            {
                                break;
                            }
                        }
                    }

                    //寻找标题行，车次列/场内股道列
                    //找车次列
                    for(int searchRow = 0;searchRow<= sheet.LastRowNum; searchRow++)
                    {
                        IRow tempRow = sheet.GetRow(searchRow);
                        if(tempRow == null)
                        {
                            sheet.CreateRow(searchRow);
                            continue;
                        }
                        for(int searchColumn = 0;searchColumn<= tempRow.LastCellNum; searchColumn++)
                        {
                            
                            if(searchColumn > 255)
                            {
                                break;
                            }
                            if(tempRow.GetCell(searchColumn) == null)
                            {
                                tempRow.CreateCell(searchColumn);
                            }
                            
                            if (searchColumn != 0)
                            {
                                if (tempRow != null && tempRow.GetCell(searchColumn) != null && tempRow.GetCell(searchColumn - 1) != null)
                                {
                                    if ((tempRow.GetCell(searchColumn).ToString().Equals("车次") || tempRow.GetCell(searchColumn).ToString().Equals("车站")) &&
                                    tempRow.GetCell(searchColumn - 1).ToString().Contains("始发"))
                                    {
                                        titleRowNum = searchRow;
                                        trainNumColumnNum.Add(searchColumn);
                                    }
                                }
                            }
                            else
                            {
                                if (tempRow.GetCell(searchColumn).ToString().Equals("车次"))
                                {
                                    titleRowNum = searchRow;
                                    trainNumColumnNum.Add(searchColumn);
                                }
                            }
                        }
                    }
                    //找股道列，主站列和终到列
                    for (int searchRow = 0; searchRow <= sheet.LastRowNum; searchRow++)
                    {
                        IRow tempRow = sheet.GetRow(searchRow);
                        for (int searchColumn = 0; searchColumn <= tempRow.LastCellNum; searchColumn++)
                        {
                            if(tempRow.GetCell(searchColumn) == null)
                            {
                                int a = 0;
                            }
                          if (tempRow != null && sheet.GetRow(searchRow+1) != null && sheet.GetRow(searchRow + 1).GetCell(searchColumn + 1) != null && tempRow.GetCell(searchColumn) != null)
                            {
                                if (tempRow.GetCell(searchColumn).ToString().Contains(station))
                                {
                                    if (sheet.GetRow(searchRow + 1).GetCell(searchColumn + 1).ToString().Equals("股道"))
                                    {
                                        //202204，获取主站位置
                                        mainStationAt.Add(searchColumn);
                                        trackNumColumnNum.Add(searchColumn + 1);
                                    }
                                }
                                if (tempRow.GetCell(searchColumn).ToString().Contains("终到"))
                                {
                                    {
                                        //202204，获取"终到"位置
                                        stoppedStationAt.Add(searchColumn);
                                    }
                                }
                            }
                        }
                    }
                    if (!hasGotIt)
                    {
                        int currentLast = sheet.LastRowNum;
                        for(int createRow = 1; createRow < 8; createRow++)
                        {
                            IRow _createRow = sheet.CreateRow(createRow + currentLast);
                            switch (createRow)
                            {
                                case 1:
                                    for (int cell = 0; cell < lastCell; cell++)
                                    {
                                        if(cell == 0)
                                        {
                                            _createRow.CreateCell(cell).SetCellValue("加开车次");
                                        }
                                        else
                                        {
                                            _createRow.CreateCell(cell);
                                        }
                                    }
                                    break;
                                default:
                                    for (int cell = 0; cell < lastCell; cell++)
                                    {
                                            _createRow.CreateCell(cell);
                                    }
                                    break;
                            }
                        }
                        //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                        /*
                        sheet.AddMergedRegion(new CellRangeAddress(currentLast + 1, currentLast + 1, 0, lastCell ));
                        sheet.AddMergedRegion(new CellRangeAddress(currentLast + 2, sheet.LastRowNum, 0, lastCell));
                        */
                    }
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
                                    //判断是不是在车次列内
                                    bool hasGot = false;
                                    for(int count= 0; count< trainNumColumnNum.Count; count++)
                                    {
                                        if(j == trainNumColumnNum[count])
                                        {
                                            hasGot = true;
                                        }
                                    }
                                    if ((row.GetCell(j).ToString().Contains("G") ||
                                        row.GetCell(j).ToString().Contains("D") ||
                                        row.GetCell(j).ToString().Contains("C") ||
                                        row.GetCell(j).ToString().Contains("J")) &&
                                        hasGot)
                                    {//把车次表格先刷白去字
                                        if (!row.GetCell(j).ToString().Contains("由") &&
                                            !row.GetCell(j).ToString().Contains("续") &&
                                            !row.GetCell(j).ToString().Contains("开行"))
                                        {

                                            //时刻表中车次+1
                                            allTrainsInTimeTable++;
                                            //去中文后再找-去掉高峰-周末-临客等字
                                            //去除次日的XX日
                                            string tomorrowTrain = "";
                                            if (row.GetCell(j).ToString().Split('√').Length > 0)
                                            {
                                                tomorrowTrain = row.GetCell(j).ToString().Split('√')[0];
                                            }
                                            if(!tomorrowTrain.Contains("日") || tomorrowTrain.Length == 0)
                                            {
                                                tomorrowTrain += "日";
                                            }
                                            row.GetCell(j).CellStyle = removeColors;
                                            row.GetCell(j).SetCellValue(Regex.Replace(row.GetCell(j).ToString().Replace(tomorrowTrain,"").Replace("√", "").Replace("×", "").Replace("(", "").Replace(")", ""), @"[\u4e00-\u9fa5]", ""));

                                        }
                                        else
                                        {
                                            //这个格子不是要找的
                                            continue;
                                        }
                                        //若遍历后都没有找到 停运+1
                                        bool ContainsTrainNumber = false;

                                        for (int ij = 0; ij < commandModel.Count; ij++)
                                        {
                                            //客调不含的停运车
                                            if(commandModel[ij].streamStatus == 4)
                                            {
                                                continue;
                                            }
                                            if (row.GetCell(j).ToString().Trim().Contains("GF") || row.GetCell(j).ToString().Trim().Contains("ZM"))
                                                row.GetCell(j).SetCellValue(row.GetCell(j).ToString().Replace("GF", "").Replace("ZM", ""));
                                            if (row.GetCell(j).ToString().Trim().Replace("GF", "").Replace("ZM", "").Equals(commandModel[ij].trainNumber) ||
                                            row.GetCell(j).ToString().Trim().Replace("GF", "").Replace("ZM", "").Equals(commandModel[ij].secondTrainNumber))
                                            {
                                                ContainsTrainNumber = true;
                                                commandModel[ij].MatchedWithTimeTable = true;
                                                //车次统计+1
                                                allTrainsCount++;
                                                //202204两个DJ5902只统计一次（特殊车统计方法）
                                                if (station.Contains("京广") && 
                                                    commandModel[ij].trainNumber.Equals("DJ5902") &&
                                                    commandModel[ij].streamStatus != 0 &&
                                                    !hasFoundDJ5902)
                                                {
                                                    allTrainsInTimeTable = allTrainsInTimeTable - 1;
                                                    hasFoundDJ5902 = true;
                                                    onlyThisStationStartTrains = onlyThisStationStartTrains - 1;
                                                }
                                                else if(station.Contains("京广") &&
                                                    commandModel[ij].trainNumber.Equals("DJ5902") &&
                                                    commandModel[ij].streamStatus == 0 &&
                                                    !hasFoundDJ5902)
                                                {
                                                    stoppedTrainsCount = stoppedTrainsCount - 1;
                                                    hasFoundDJ5902 = true;
                                                    onlyThisStationStopTrains = onlyThisStationStopTrains - 1;
                                                }
                                                //202204两个0J5901只统计一次（特殊车统计方法）
                                                if (station.Contains("京广") &&
                                                    commandModel[ij].trainNumber.Equals("0J5901") &&
                                                    commandModel[ij].streamStatus != 0 &&
                                                    !hasFound0J5901)
                                                {
                                                    allTrainsInTimeTable = allTrainsInTimeTable - 1;
                                                    hasFound0J5901 = true;
                                                    onlyThisStationStartTrains = onlyThisStationStartTrains - 1;
                                                }
                                                else if (station.Contains("京广") &&
                                                    commandModel[ij].trainNumber.Equals("0J5901") &&
                                                    commandModel[ij].streamStatus == 0 &&
                                                    !hasFound0J5901)
                                                {
                                                    stoppedTrainsCount = stoppedTrainsCount - 1;
                                                    hasFound0J5901 = true;
                                                    onlyThisStationStopTrains = onlyThisStationStopTrains - 1;
                                                }

                                                int _trainNumber = -1;
                                                string getNumber = Regex.Replace(row.GetCell(j).ToString().Trim(), @"[^0-9]+", "");
                                                int.TryParse(getNumber, out _trainNumber);
                                                if (_trainNumber != -1)
                                                {
                                                    switch (_trainNumber % 2)
                                                    {
                                                        case 0:
                                                            commandModel[ij].upOrDown = 0;
                                                            break;
                                                        case 1:
                                                            commandModel[ij].upOrDown = 1;
                                                            break;
                                                    }
                                                }
                                                if (!row.GetCell(j).ToString().Trim().Contains("0G") &&
                                                    !row.GetCell(j).ToString().Trim().Contains("0D") &&
                                                    !row.GetCell(j).ToString().Trim().Contains("0J") &&
                                                    !row.GetCell(j).ToString().Trim().Contains("0C") &&
                                                    !row.GetCell(j).ToString().Trim().Contains("DJ"))
                                                {
                                                    allPsngerTrainsCount++;
                                                    commandModel[ij].psngerTrain = true;
                                                }
                                                else
                                                {
                                                    commandModel[ij].psngerTrain = false;
                                                }
                                                if (commandModel[ij].trainType == 1)
                                                {
                                                    row.GetCell(j).SetCellValue("高峰" + row.GetCell(j).ToString().Trim());
                                                    if(commandModel[ij].streamStatus != 0)
                                                         rushHourTrain++;
                                                }
                                                else if (commandModel[ij].trainType == 2)
                                                {
                                                    row.GetCell(j).SetCellValue("临客" + row.GetCell(j).ToString().Trim());
                                                    if (commandModel[ij].streamStatus != 0)
                                                        tempTrain++;
                                                }
                                                else if (commandModel[ij].trainType == 3)
                                                {
                                                    row.GetCell(j).SetCellValue("周末" + row.GetCell(j).ToString().Trim());
                                                    if (commandModel[ij].streamStatus != 0)
                                                        weekendTrain++;
                                                }
                                                else if (commandModel[ij].trainType == 4)
                                                {
                                                    row.GetCell(j).SetCellValue("加开" + row.GetCell(j).ToString().Trim());
                                                    if (commandModel[ij].streamStatus != 0)
                                                        addedTrain++;
                                                }
                                                if (commandModel[ij].streamStatus == 1)
                                                {
                                                    row.GetCell(j).SetCellValue("√" + row.GetCell(j).ToString().Trim());
                                                    row.GetCell(j).CellStyle = normalTrainStyle;
                                                }
                                                else if (commandModel[ij].streamStatus == 0)
                                                {
                                                    row.GetCell(j).SetCellValue("×" + row.GetCell(j).ToString().Trim());
                                                    stoppedTrainsCount++;
                                                    row.GetCell(j).CellStyle = nonMatchedTrainStype;
                                                    //202204 停运，加入删除行
                                                    removedTrains.Add(i + "-" + j);
                                                }
                                                else if (commandModel[ij].streamStatus == 2)
                                                {
                                                    string date = "";
                                                    if (hour >= 0 && hour <= 16)
                                                    {
                                                        date = DateTime.Now.AddDays(1).ToString("dd日");
                                                    }
                                                    else
                                                    {
                                                        date = DateTime.Now.AddDays(2).ToString("dd日");
                                                    }
                                                    row.GetCell(j).SetCellValue(date+"√" + row.GetCell(j).ToString().Trim());
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
                                                    row.GetCell(j).CellStyle = nonMatchedTrainStype;
                                                    //202204 停运，加入删除行
                                                    removedTrains.Add(i + "-" + j);
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
                                                    hasGotIt = false;
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
                                                                row.GetCell(j).CellStyle = nonMatchedTrainStype;
                                                                //202204 停运，加入删除行
                                                                removedTrains.Add(i + "-" + j);
                                                            }
                                                            gotIt = true;
                                                            hasGotIt = true;
                                                        }
                                                    }
                                                }
                                            }
                                            //添加时刻表内有的客调命令不含的车
                                            if (!gotIt)
                                            {
                                                row.GetCell(j).SetCellValue("×" + row.GetCell(j).ToString().Trim());
                                                row.GetCell(j).CellStyle = nonMatchedTrainStype;
                                                //202204 停运，加入删除行
                                                removedTrains.Add(i + "-" + j);
                                                stoppedTrainsCount++;
                                                //在此处加入commandmodel，streamStatus为4，后续检测到4时直接跳过
                                                CommandModel _cm = new CommandModel();
                                                _cm.trainNumber = row.GetCell(j).ToString().Trim().Replace("×", "");
                                                _cm.secondTrainNumber = "null";
                                                _cm.streamStatus = 4;
                                                _cm.notMatchedTabelName = station;
                                                //判断载客与否
                                                string _trainNum = row.GetCell(j).ToString().Trim().Replace("×", "");
                                                if (trainNum.Contains("0G") ||
                                                    trainNum.Contains("0J") ||
                                                    trainNum.Contains("DJ") ||
                                                    trainNum.Contains("0D") ||
                                                    trainNum.Contains("0C"))
                                                {
                                                    _cm.psngerTrain = false;
                                                }
                                                else
                                                {
                                                    _cm.psngerTrain = true;
                                                }
                                                //不添加重复的
                                                bool hasGotSame = false;
                                                foreach(CommandModel _tempCM in commandModel)
                                                {
                                                    if(_tempCM.streamStatus != 4)
                                                    {
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        if (_tempCM.trainNumber.Equals(_cm.trainNumber))
                                                        {
                                                            hasGotSame = true;
                                                            _tempCM.notMatchedTabelName = _tempCM.notMatchedTabelName + "/" + station;
                                                            break;
                                                        }
                                                    }
                                                }
                                                if (!hasGotSame)
                                                {
                                                    notMatchedModels.Add(_cm);
                                                }
                                            }
                                        }
                                        //判断是否仅本场
                                        bool isCurrentStationTrain = false;
                                            int currentTrainNumIndex = -1;
                                            if (trainNumColumnNum.Count >= 2 && trackNumColumnNum.Count >= 2)
                                            {
                                                for (int c = 0; c < trainNumColumnNum.Count; c++)
                                                {
                                                    if (j == trainNumColumnNum[c])
                                                    {
                                                        currentTrainNumIndex = c;
                                                    }
                                                }
                                            }
                                            if (currentTrainNumIndex != -1)
                                            {//条件判断完成，可以判断是否为本场车(以及找接续车次)
                                             //目前只有东三站
                                                int tempTrack = 0;
                                                if (station.Contains("徐兰"))
                                                {//相应的股道列不为空，并且是21-32
                                                    if (row.GetCell(j)!=null &&row.GetCell(trackNumColumnNum[currentTrainNumIndex])!=null &&
                                                    row.GetCell(trackNumColumnNum[currentTrainNumIndex]).ToString().Trim().Length != 0)
                                                    {
                                                    string numberOfTrack = row.GetCell(trackNumColumnNum[currentTrainNumIndex]).ToString().Trim();
                                                    int.TryParse(numberOfTrack, out tempTrack);
                                                    if(tempTrack == 0)
                                                    {
                                                        //罗马转阿拉伯数字
                                                        tempTrack = RomanToInt(numberOfTrack);
                                                    }
                                                        if (tempTrack >= 21 && tempTrack <= 32)
                                                        {//是本场车，+1
                                                        isCurrentStationTrain = true;
                                                            if (row.GetCell(j).ToString().Contains("√"))
                                                                onlyThisStationStartTrains++;
                                                            else if (row.GetCell(j).ToString().Contains("×"))
                                                                onlyThisStationStopTrains++;
                                                        }
                                                    }

                                                }
                                                else if (station.Contains("京广"))
                                                {
                                                    if (row.GetCell(j) != null && row.GetCell(trackNumColumnNum[currentTrainNumIndex]) != null)
                                                    {
                                                    string numberOfTrack = row.GetCell(trackNumColumnNum[currentTrainNumIndex]).ToString().Trim();
                                                    int.TryParse(numberOfTrack, out tempTrack);
                                                    if (tempTrack == 0)
                                                    {
                                                        //罗马转阿拉伯数字
                                                        tempTrack = RomanToInt(numberOfTrack);
                                                    }
                                                        if (tempTrack >= 1 && tempTrack <= 16)
                                                        {//是本场车，+1
                                                        isCurrentStationTrain = true;
                                                        if (row.GetCell(j).ToString().Contains("√"))
                                                                onlyThisStationStartTrains++;
                                                            else if(row.GetCell(j).ToString().Contains("×"))
                                                                onlyThisStationStopTrains++;
                                                        }
                                                    }
                                                }
                                                else if (station.Contains("城际"))
                                                {
                                                    if (row.GetCell(j) != null && row.GetCell(trackNumColumnNum[currentTrainNumIndex]) != null)
                                                    {
                                                    string numberOfTrack = row.GetCell(trackNumColumnNum[currentTrainNumIndex]).ToString().Trim();
                                                    int.TryParse(numberOfTrack, out tempTrack);
                                                    if (tempTrack == 0)
                                                    {
                                                        //罗马转阿拉伯数字
                                                        tempTrack = RomanToInt(numberOfTrack);
                                                    }
                                                    if (tempTrack >= 17 && tempTrack <= 20)
                                                        {//是本场车，+1
                                                        isCurrentStationTrain = true;
                                                        if (row.GetCell(j).ToString().Contains("√"))
                                                                onlyThisStationStartTrains++;
                                                            else if (row.GetCell(j).ToString().Contains("×"))
                                                                onlyThisStationStopTrains++;
                                                        }
                                                    }
                                                }
                                                

                                            //寻找接续问题
                                            string currentTrainNum = "";
                                            string continueTrainNum = "";
                                            string newContinueTrainNum = "";
                                            bool hasGotThat = false;
                                            string currentTrainTime = "";
                                            int currentTrainPlace = -1;
                                            string log = "";

                                            currentTrainNum = row.GetCell(j).ToString().Trim().Replace("√", "").Replace("×", "").Replace("高峰", "").Replace("临客", "").Replace("周末", "").Replace("加开", "").Replace("非动","");
                                                //不是自己场的不要找
                                                if (isCurrentStationTrain)
                                            {
                                                //判断有无接
                                                if (row.GetCell(trackNumColumnNum[currentTrainNumIndex] - 1) != null)
                                                {//有接
                                                    if (row.GetCell(trackNumColumnNum[currentTrainNumIndex] - 1).ToString().Contains("由") &&
                                                        row.GetCell(trackNumColumnNum[currentTrainNumIndex] - 1).ToString().Contains("改"))
                                                    {
                                                        //先判断接续是否为自己的第二车次，并记下自身位置
                                                        continueTrainNum = row.GetCell(trackNumColumnNum[currentTrainNumIndex] - 1).ToString().Replace("由", "").Replace("改", "").Replace("非动", "");
                                                        for (int ik = 0; ik < commandModel.Count; ik++)
                                                        {
                                                            if (commandModel[ik].trainNumber.Equals(currentTrainNum))
                                                            {
                                                                //自身不能停运
                                                                if (commandModel[ik].streamStatus == 0 ||
                                                                    commandModel[ik].streamStatus == 4)
                                                                {
                                                                    hasGotThat = true;
                                                                    break;
                                                                }
                                                                currentTrainPlace = ik;
                                                                if (commandModel[ik].secondTrainNumber.Equals(continueTrainNum))
                                                                {
                                                                    hasGotThat = true;
                                                                    break;
                                                                }
                                                            }
                                                            else if (commandModel[ik].secondTrainNumber.Equals(currentTrainNum))
                                                            {
                                                                currentTrainPlace = ik;
                                                                if (commandModel[ik].trainNumber.Equals(continueTrainNum))
                                                                {
                                                                    hasGotThat = true;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        //不是，则开始在模型库中寻找
                                                        //先把时间存下
                                                        if (row.GetCell(trackNumColumnNum[currentTrainNumIndex] + 1) != null)
                                                        {
                                                            currentTrainTime = row.GetCell(trackNumColumnNum[currentTrainNumIndex] + 1).ToString().Trim();
                                                        }
                                                        //找目标车辆前面的车，必须同车型同车号的
                                                        //currentTrainPlace == -1 ，跳过
                                                        if (currentTrainPlace == -1)
                                                        {
                                                            hasGotThat = true;
                                                        }
                                                        for (int il = currentTrainPlace - 1; il >= 0; il--)
                                                        {
                                                            if (hasGotThat)
                                                            {
                                                                break;
                                                            }
                                                            if (commandModel[il].streamStatus != 0 && commandModel[il].streamStatus != 4)
                                                            {
                                                                if (commandModel[il].trainModel.Equals(commandModel[currentTrainPlace].trainModel) &&
                                                                    commandModel[il].trainId.Equals(commandModel[currentTrainPlace].trainId))
                                                                {//找到了前序列车，和原有进行对比
                                                                    if (commandModel[il].trainNumber.Equals(continueTrainNum) ||
                                                                        commandModel[il].secondTrainNumber.Equals(continueTrainNum))
                                                                    {//一致，不改
                                                                        break;
                                                                    }
                                                                    //类似时刻表中为“0G1869/G9202”
                                                                    else if (continueTrainNum.Contains("/"))
                                                                    {
                                                                        string numA = continueTrainNum.Split('/')[0];
                                                                        string numB = continueTrainNum.Split('/')[1];
                                                                        if (commandModel[il].trainNumber.Equals(numA) ||
                                                                       commandModel[il].secondTrainNumber.Equals(numA))
                                                                        {//一致，不改
                                                                            break;
                                                                        }
                                                                        if (commandModel[il].trainNumber.Equals(numB) ||
                                                                        commandModel[il].secondTrainNumber.Equals(numB))
                                                                        {//一致，不改
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {//不一致，判断新的车次是否在图内
                                                                        for (int im = 0; im <= sheet.LastRowNum; im++)
                                                                        {
                                                                            IRow tempRow = sheet.GetRow(im);
                                                                            if (tempRow != null)
                                                                            {
                                                                                for (int ib = 0; ib <= tempRow.LastCellNum; ib++)
                                                                                {
                                                                                    if (tempRow.GetCell(ib) != null)
                                                                                    {
                                                                                        if (!tempRow.GetCell(ib).ToString().Contains("由") &&
                                                                                            !tempRow.GetCell(ib).ToString().Contains("续开"))
                                                                                        {
                                                                                            if (tempRow.GetCell(ib).ToString().Trim().Replace("√", "").Replace("×", "").Replace("高峰", "").Replace("周末", "").Replace("临客", "").Replace("加开", "").Equals(commandModel[il].trainNumber))
                                                                                            {
                                                                                                //图中找到了相应车次，提供修改建议
                                                                                                hasGotThat = true;
                                                                                                newContinueTrainNum = commandModel[il].trainNumber;
                                                                                                log = station + "场" + currentTrainNum + "次(" + currentTrainTime + "出发)的前序列车在客调命令第" + commandModel[il].trainIndex + "条中可能为" + newContinueTrainNum + "次,原前序列车为" + continueTrainNum + "次。";
                                                                                                break;
                                                                                            }
                                                                                            if (tempRow.GetCell(ib).ToString().Trim().Replace("√", "").Replace("×", "").Replace("高峰", "").Replace("周末", "").Replace("临客", "").Replace("加开", "").Equals(commandModel[il].secondTrainNumber))
                                                                                            {
                                                                                                hasGotThat = true;
                                                                                                newContinueTrainNum = commandModel[il].secondTrainNumber;
                                                                                                log = station + "场" + currentTrainNum + "次(" + currentTrainTime + "出发)的前序列车在客调命令第" + commandModel[il].trainIndex + "条中可能为" + newContinueTrainNum + "次,原前序列车为" + continueTrainNum + "次。";
                                                                                                break;
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                            if (hasGotThat)
                                                                            {
                                                                                break;
                                                                            }
                                                                            //202204 接续列车不在图里
                                                                            else
                                                                            {
                                                                                hasGotThat = true;
                                                                                newContinueTrainNum = commandModel[il].trainNumber;
                                                                                log = "【！】" + station + "场的" + newContinueTrainNum + "次开行，且不在底图内，请立即检查底图\n" + station + "场" + currentTrainNum + "次(" + currentTrainTime + "到达)的后序列车在客调命令第" + commandModel[il].trainIndex + "条中可能为" + newContinueTrainNum + "次,原后序列车为" + continueTrainNum + "次。";
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                //判断有无续
                                                if (row.GetCell(trackNumColumnNum[currentTrainNumIndex] + 1) != null)
                                                {//有续
                                                    if (row.GetCell(trackNumColumnNum[currentTrainNumIndex] + 1).ToString().Contains("续开"))
                                                    {
                                                        //先判断接续是否为自己的第二车次
                                                        continueTrainNum = row.GetCell(trackNumColumnNum[currentTrainNumIndex] + 1).ToString().Replace("续开", "").Replace("非动", "");
                                                        for (int ik = 0; ik < commandModel.Count; ik++)
                                                        {
                                                            if (commandModel[ik].trainNumber.Equals(currentTrainNum))
                                                            {
                                                                //自身不能停运
                                                                if (commandModel[ik].streamStatus == 0 ||
                                                                    commandModel[ik].streamStatus == 4)
                                                                {
                                                                    hasGotThat = true;
                                                                    break;
                                                                }
                                                                currentTrainPlace = ik;
                                                                if (commandModel[ik].secondTrainNumber.Equals(continueTrainNum))
                                                                {
                                                                    hasGotThat = true;
                                                                    break;
                                                                }
                                                            }
                                                            else if (commandModel[ik].secondTrainNumber.Equals(currentTrainNum))
                                                            {
                                                                currentTrainPlace = ik;
                                                                if (commandModel[ik].trainNumber.Equals(continueTrainNum))
                                                                {
                                                                    hasGotThat = true;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        //不是，则开始在模型库中寻找
                                                        //先把时间存下
                                                        if (row.GetCell(trackNumColumnNum[currentTrainNumIndex] - 1) != null)
                                                        {
                                                            currentTrainTime = row.GetCell(trackNumColumnNum[currentTrainNumIndex] - 1).ToString().Trim();
                                                        }
                                                        //找目标车辆后面的车，必须同车型同车号的
                                                        //currentTrainPlace == -1 ，跳过
                                                        if (currentTrainPlace == -1)
                                                        {
                                                            hasGotThat = true;
                                                        }
                                                        for (int il = currentTrainPlace + 1; il < commandModel.Count; il++)
                                                        {
                                                            if (hasGotThat)
                                                            {
                                                                break;
                                                            }
                                                            if (commandModel[il].streamStatus != 0 && commandModel[il].streamStatus != 4)
                                                            {
                                                                if (commandModel[il].trainModel.Equals(commandModel[currentTrainPlace].trainModel) &&
                                                                    commandModel[il].trainId.Equals(commandModel[currentTrainPlace].trainId))
                                                                {//找到了后序列车，和原有进行对比
                                                                    if (commandModel[il].trainNumber.Equals(continueTrainNum) ||
                                                                        commandModel[il].secondTrainNumber.Equals(continueTrainNum))
                                                                    {//一致，不改
                                                                        break;
                                                                    }
                                                                    //类似时刻表中为“0G1869/G9202”
                                                                    else if (continueTrainNum.Contains("/"))
                                                                    {
                                                                        string numA = continueTrainNum.Split('/')[0];
                                                                        string numB = continueTrainNum.Split('/')[1];
                                                                        if (commandModel[il].trainNumber.Equals(numA) ||
                                                                       commandModel[il].secondTrainNumber.Equals(numA))
                                                                        {//一致，不改
                                                                            break;
                                                                        }
                                                                        if (commandModel[il].trainNumber.Equals(numB) ||
                                                                        commandModel[il].secondTrainNumber.Equals(numB))
                                                                        {//一致，不改
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {//不一致，判断新的车次是否在图内
                                                                        for (int im = 0; im <= sheet.LastRowNum; im++)
                                                                        {
                                                                            IRow tempRow = sheet.GetRow(im);
                                                                            if (tempRow != null)
                                                                            {
                                                                                for (int ib = 0; ib <= tempRow.LastCellNum; ib++)
                                                                                {
                                                                                    if (tempRow.GetCell(ib) != null)
                                                                                    {
                                                                                        if (!tempRow.GetCell(ib).ToString().Contains("由") &&
                                                                                            !tempRow.GetCell(ib).ToString().Contains("续开"))
                                                                                        {
                                                                                            if (tempRow.GetCell(ib).ToString().Trim().Replace("√", "").Replace("×", "").Replace("高峰", "").Replace("周末", "").Replace("临客", "").Replace("加开", "").Equals(commandModel[il].trainNumber))
                                                                                            {
                                                                                                //图中找到了相应车次，提供修改建议
                                                                                                hasGotThat = true;
                                                                                                newContinueTrainNum = commandModel[il].trainNumber;
                                                                                                log = station + "场" + currentTrainNum + "次(" + currentTrainTime + "到达)的后序列车在客调命令第" + commandModel[il].trainIndex + "条中可能为" + newContinueTrainNum + "次,原后序列车为" + continueTrainNum + "次。";
                                                                                                break;
                                                                                            }
                                                                                            if (tempRow.GetCell(ib).ToString().Trim().Replace("√", "").Replace("×", "").Replace("高峰", "").Replace("周末", "").Replace("临客", "").Replace("加开", "").Equals(commandModel[il].secondTrainNumber))
                                                                                            {
                                                                                                hasGotThat = true;
                                                                                                newContinueTrainNum = commandModel[il].secondTrainNumber;
                                                                                                log = station + "场" + currentTrainNum + "次(" + currentTrainTime + "到达)的后序列车在客调命令第" + commandModel[il].trainIndex + "条中可能为" + newContinueTrainNum + "次,原后序列车为" + continueTrainNum + "次。";
                                                                                                break;
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                            if (hasGotThat)
                                                                            {
                                                                                break;
                                                                            }
                                                                            //202204 接续列车不在图里
                                                                            else
                                                                            {
                                                                                hasGotThat = true;
                                                                                newContinueTrainNum = commandModel[il].trainNumber;
                                                                                log = "【！】" + station + "场的" + newContinueTrainNum + "次开行，且不在底图内，请立即检查底图\n" + station + "场" + currentTrainNum + "次(" + currentTrainTime + "到达)的后序列车在客调命令第" + commandModel[il].trainIndex + "条中可能为" + newContinueTrainNum + "次,原后序列车为" + continueTrainNum + "次。";
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }


                                                    }
                                                }

                                                if (log.Length != 0 && !log.Equals(""))
                                                {
                                                    continueTrainAnalyse = continueTrainAnalyse + log + "\n";
                                                }
                                            }


                                        }


                                    }
                                }
                            }
                        }
                    }
                    //细节去某站多少列
                    string extraDetailText = "";
                    //202204 已经收集到所有需要删除的车，下面进行删除工作
                    if (automaticDeleteStoppedTrains)
                    {
                        foreach (string targetPlace in removedTrains)
                        {
                            if (!targetPlace.Contains("-"))
                            {
                                continue;
                            }
                            int targetRow = -1;
                            int.TryParse(targetPlace.Split('-')[0], out targetRow);
                            int targetColumn = -1;
                            int.TryParse(targetPlace.Split('-')[1], out targetColumn);
                            int startColumn = -1;
                            int stopColumn = -1;
                            if (targetColumn != -1 && stoppedStationAt.Count > 0)
                            {
                                if (targetColumn < stoppedStationAt[0])
                                {
                                    startColumn = 0;
                                    stopColumn = stoppedStationAt[0];
                                }
                                else if (stoppedStationAt.Count > 1 && targetColumn < stoppedStationAt[1])
                                {
                                    startColumn = stoppedStationAt[0] + 1;
                                    stopColumn = stoppedStationAt[1];
                                }
                            }
                            //条件齐全，开始删除
                            if (targetColumn != -1 && targetRow != -1 && startColumn != -1 && stopColumn != -1)
                            {
                                deleteTargetRow(workbook, startColumn, stopColumn, targetRow);
                            }
                        }
                        //202204全删完了，挪动表格填满，刷格式
                        fixEmptyRows(workbook, stoppedStationAt, trainNumColumnNum, titleRowNum);
                        //202204对剩下的车判断各个方向
                        extraDetailText = findTrainsWay(workbook, currentTimeTable, mainStationAt);
                    }



                    //把这张表内未识别的添加进去
                    foreach(CommandModel _cm in notMatchedModels)
                    {
                        commandModel.Add(_cm);
                    }
                    fileCounter++;
                    string titleEnd = "";
                    titleEnd = title.Replace(title.Split('-')[0], "");
                    if(((allTrainsInTimeTable - stoppedTrainsCount) == onlyThisStationStartTrains && stoppedTrainsCount == onlyThisStationStartTrains) || (onlyThisStationStartTrains == 0&&onlyThisStationStopTrains==0))
                    {
                        title = "(" + title.Split('-')[0] + ",开" + (allTrainsInTimeTable - stoppedTrainsCount).ToString() + "列,停" + stoppedTrainsCount.ToString() + "列";
                    }
                    else
                    {
                        title = "(" + title.Split('-')[0] + ",图内开" + (allTrainsInTimeTable - stoppedTrainsCount).ToString() + "列(本场" + onlyThisStationStartTrains + "列),停" + stoppedTrainsCount.ToString() + "列(本场" + onlyThisStationStopTrains + "列)";
                    }

                    if (rushHourTrain > 0)
                    {
                        title += ",开高峰" + rushHourTrain.ToString() + "列";
                    }
                    if(tempTrain > 0)
                    {
                        title += ",开临客" + tempTrain.ToString() + "列";
                    }
                    if(weekendTrain > 0)
                    {
                        title += ",开周末" + weekendTrain.ToString() + "列";
                    }
                    if(addedTrain > 0)
                    {
                        title += ",加开" + addedTrain.ToString() + "列";
                    }
                    if (station.Contains("城际"))
                    {
                        int a = 0;
                    }
                    operationChangedAnalyse = operationChangedAnalyse + "\n" + station + "场" + title.Replace("(","").Replace(")","").Replace("本场",",本场停靠") + "。\n"+
                        "明细：\n"+extraDetailText;
                    title += ")" + titleEnd;
                    if (fileCounter == ExcelFile.Count)
                    {
                        DataAnalyse _daForm = new DataAnalyse(commandModel, operationChangedAnalyse,continueTrainAnalyse);
                        _daForm.Show();
                    }
                    sheet.GetRow(0).GetCell(0).SetCellValue(title);
                    /*重新修改文件指定单元格样式*/
                    string newFileName = "";
                    string filePath = "";
                    int fileNameSplitCount = fileName.Split('\\').Length;
                    for(int fileCount = 0;fileCount< fileNameSplitCount; fileCount++)
                    {
                        if(fileCount<= fileNameSplitCount - 2)
                        {
                            newFileName = newFileName + fileName.Split('\\')[fileCount] + "\\";
                        }
                        else
                        {
                            filePath = newFileName;
                            newFileName = newFileName + "\\处理后-" + station + "-"+title.Split('-')[0] + ".xls";
                        }
                    }
                    FileStream fs1 = File.OpenWrite(newFileName);
                    workbook.Write(fs1);
                    fs1.Close();
                    fileStream.Close();
                    workbook.Close();
                    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
                    //info.WorkingDirectory = Application.StartupPath;
                    info.FileName = newFileName;
                    info.Arguments = "";
                    System.Diagnostics.ProcessStartInfo info1 = new System.Diagnostics.ProcessStartInfo();
                    //info.WorkingDirectory = Application.StartupPath;
                    info1.FileName = filePath;
                    info1.Arguments = "";
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
                        System.Diagnostics.Process.Start(info1);
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

        //读基本图-存模型
        private void readBasicTrainTable(bool isComparing = false,bool isCompareingDailySchedues = false)
        {
            if (ExcelFile == null)
            {
                MessageBox.Show("请重新选择班计划文件~", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(isCompareingDailySchedues && yesterdayExcelFile.Length == 0)
            {
                MessageBox.Show("请重新选择用于对比的班计划文件~", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string fileName = "";
            if (!isCompareingDailySchedues)
            {
                fileName = ExcelFile[0];
            }
            else
            {
                fileName = yesterdayExcelFile;
            }
            IWorkbook workbook = null;  //新建IWorkbook对象  
            basicTrainGraphTitle titleInfo = new basicTrainGraphTitle();
            List<DailySchedule> _dailyScheduleModel = new List<DailySchedule>();
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
                        MessageBox.Show("基本图文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

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

                        MessageBox.Show("基本图文件出现损坏（或文件无效）\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
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
                        if (row.GetCell(0) != null && row.GetCell(1) != null)
                        {//发送为民权北
                            if (row.GetCell(0).ToString().Contains("序号") ||
                                row.GetCell(0).ToString().Contains("预售")||
                                row.GetCell(0).ToString().Contains("发送")||
                                row.GetCell(1).ToString().Contains("车次"))
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
                    for (int j = titleInfo.titleRow[i]; j <= lastRow; j++)
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
                            {//备注
                                if (_readingRow.GetCell(titleInfo.extraTextColumn).ToString().Length != 0)
                                {
                                    tempModel.extraText = _readingRow.GetCell(titleInfo.extraTextColumn).ToString();
                                    string[] splitedExtra = tempModel.extraText.Split('、');
                                    if (splitedExtra.Length >= 1)
                                    {
                                        for (int ij = 0; ij < splitedExtra.Length; ij++)
                                        {
                                            switch (splitedExtra[ij].Trim().Replace(" ", ""))
                                            {
                                                case "（北）":
                                                    tempModel.extra_stoppingPlace = 2;
                                                    break;
                                                case "(北)":
                                                    tempModel.extra_stoppingPlace = 2;
                                                    break;
                                                case "(南)":
                                                    tempModel.extra_stoppingPlace = 1;
                                                    break;
                                                case "（南）":
                                                    tempModel.extra_stoppingPlace = 1;
                                                    break;
                                                case "上水":
                                                    tempModel.extra_plugingWater = true;
                                                    break;
                                                case "吸污":
                                                    tempModel.extra_unloading = true;
                                                    break;
                                                case "重联":
                                                    tempModel.extra_doubleConnected = true;
                                                    break;
                                                case "始发":
                                                    tempModel.extra_original = true;
                                                    break;
                                                case "终到":
                                                    tempModel.extra_terminal = true;
                                                    break;
                                                case "反编":
                                                    tempModel.extra_reversedTrain = true;
                                                    break;
                                                case "周末":
                                                    tempModel.extra_weekendTrain = true;
                                                    break;
                                                case "周末线":
                                                    tempModel.extra_weekendTrain = true;
                                                    break;
                                                case "高峰":
                                                    tempModel.extra_rushHourTrain = true;
                                                    break;
                                                case "高峰线":
                                                    tempModel.extra_rushHourTrain = true;
                                                    break;
                                            }
                                        }
                                    }
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
                if (!isCompareingDailySchedues && yesterdayExcelFile.Length == 0)
                {
                    detectedCModel = new List<CommandModel>();
                    string unresolvedTrains = checkCommandModelWithDailySchedule(_dailyScheduleModel, null, commandModel, commandText);
                    if (unresolvedTrains.Trim().Length != 0)
                    {
                        MessageBox.Show("请核对以下未识别车次：\n" + unresolvedTrains + "\n位于->今日客调命令(←)。\n", "人工核对", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //下一步-处理数据
                    if (!isComparing)
                    {
                        analyzeDailyScheduleData(_dailyScheduleModel);
                    }
                    else
                    {
                        //查昨日错
                        string yesterdayUnresolvedTrains = checkCommandModelWithDailySchedule(_dailyScheduleModel, null, yesterdayCommandModel, yesterdayCommandText, true);
                        if (yesterdayUnresolvedTrains.Trim().Length != 0)
                        {
                            MessageBox.Show("请核对以下未识别车次：\n" + yesterdayUnresolvedTrains + "\n位于->昨日客调命令(↑)。\n", "人工核对", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        compareWithYesterday(_dailyScheduleModel, null);
                    }
                }
                //双班计划对比
                else if(isCompareingDailySchedues)
                {
                    yesterdayAllDailyScheduleModel = _dailyScheduleModel;
                }
                else if (!isCompareingDailySchedues)
                {
                    allDailyScheduleModel = _dailyScheduleModel;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("请确认是否选择了正确的班计划文件~\n" + "错误内容："+e.Message.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
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
                        MessageBox.Show("基本图文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
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
                        MessageBox.Show("基本图文件出现损坏（或文件无效）\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
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
                                    else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("到时") || row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("到达"))
                                    {
                                        stopTimeColumn = j;
                                    }
                                    else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("股道"))
                                    {
                                        trackNumColumn = j;
                                    }
                                    else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("开时") || row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("发车"))
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
            for (int j = titleRow; j <= lastRow; j++)
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
                            _tempModel.trainNumber = _readingRow.GetCell(trainNumberColumn).ToString().Replace("TQ","");
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

        //下面三个方法合并起来是查错用的
        private int searchAndHightlightUnresolvedTrains(string find, int type,int isYesterDay = 0,string secondTrainNumber = "")
        {//找到未识别车并高亮显示(返回0-停开，1-开行，-1-未找到，2-综控-一整行车都没有23333)-行车，东所
            //isYesterday : 0今天 1综控昨天 2动车所昨天
            //在模型内添加新车-综控
            //type 0 1 2行车综控东所
            int index = 0;
            if (isYesterDay == 0)
            {//不是昨天
                index = command_rTb.Find(find, RichTextBoxFinds.WholeWord);//调用find方法，并设置区分全字匹配
            }
            else if(isYesterDay == 1)
            {//综控室昨天的
                index = yesterdayCommand_rtb.Find(find, RichTextBoxFinds.WholeWord);//
            }
            else if(isYesterDay == 2)
            {//动车所昨天的
                index = EMUGarage_YesterdayCommand_rtb.Find(find, RichTextBoxFinds.WholeWord);//
            }
            int startPos = index;
            int nextIndex = 0;
            while (nextIndex != startPos)//循环查找字符串，并用红色加粗12号Times New Roman标记之
            {
                if (isYesterDay == 0)
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
                            return 1;
                            /*
                            DialogResult resultTrainStatus = MessageBox.Show(find + "次在客调命令中是否开行？（开行选择“是”，停运/待定等选择“否”）", "人工核对", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (resultTrainStatus == DialogResult.Yes)
                            {
                                return 1;
                            }
                            else if (resultTrainStatus == DialogResult.No)
                            {
                                return 0;
                            }
                            */
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
                                analyseCommand(false,currentRow, true);
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
                                _tempCM.streamStatus = 1;
                                /*
                                DialogResult resultTrainStatus = MessageBox.Show(find + "次在客调命令中是否开行？（开行选择“是”，停运/待定等选择“否”）", "人工核对", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (resultTrainStatus == DialogResult.Yes)
                                {
                                    _tempCM.streamStatus = 1;
                                }
                                else if (resultTrainStatus == DialogResult.No)
                                {
                                    _tempCM.streamStatus = 0;
                                }
                                */
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
                else if(isYesterDay == 1)
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
                                analyseCommand(false, currentRow,true);
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
                                _tempCM.streamStatus = 1;
                                /*
                                DialogResult resultTrainStatus = MessageBox.Show(find + "次在客调命令中是否开行？（开行选择“是”，停运/待定等选择“否”）", "人工核对", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (resultTrainStatus == DialogResult.Yes)
                                {
                                    _tempCM.streamStatus = 1;
                                }
                                else if (resultTrainStatus == DialogResult.No)
                                {
                                    _tempCM.streamStatus = 0;
                                }
                                */
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
                else if(isYesterDay == 2)
                {
                    if (index == -1)
                    {
                        break;
                    }
                    EMUGarage_YesterdayCommand_rtb.SelectionStart = index;
                    EMUGarage_YesterdayCommand_rtb.SelectionLength = find.Length;
                    EMUGarage_YesterdayCommand_rtb.SelectionColor = Color.Red;
                    EMUGarage_YesterdayCommand_rtb.SelectionFont = new Font("Times New Roman", (float)12, FontStyle.Bold);
                    EMUGarage_YesterdayCommand_rtb.Focus();
                    DialogResult result = MessageBox.Show("(在昨天的命令中)请人工核对\n" + find + "次是否为当前标红内容？\n(*请注意核查同一条命令内其他车次是否正确)", "人工核对", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        {//把命令分行，找车次所在行有没有其他车次，找对应车次车号赋给新车，然后再选择停运情况
                            string currentRow = "";
                            string[] _aCommands = removeUnuseableWord(false)[0].Split('。');
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
                                analyseCommand(false, currentRow,true);
                            }
                            if (detectedCModel.Count != 0)
                            {
                                CommandModel _tempCM = new CommandModel();
                                _tempCM.trainNumber = find.Split('/')[0];
                                _tempCM.secondTrainNumber = secondTrainNumber;
                                // _tempCM.secondTrainNumber = "null";
                                foreach (CommandModel _cm in detectedCModel)
                                {
                                    _tempCM.trainIndex = _cm.trainIndex;
                                    _tempCM.trainModel = _cm.trainModel;
                                    _tempCM.trainId = _cm.trainId;
                                    _tempCM.trainConnectType = _cm.trainConnectType;
                                }
                                _tempCM.streamStatus = 1;
                                /*
                                DialogResult resultTrainStatus = MessageBox.Show(find + "次在客调命令中是否开行？（开行选择“是”，停运/待定等选择“否”）", "人工核对", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (resultTrainStatus == DialogResult.Yes)
                                {
                                    _tempCM.streamStatus = 1;
                                }
                                else if (resultTrainStatus == DialogResult.No)
                                {
                                    _tempCM.streamStatus = 0;
                                }
                                */
                                yesterdayCommandModel.Add(_tempCM);
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
                    int intIsYesterday = 0;
                    if (isYesterday)
                    {
                        intIsYesterday = 1;
                    }
                    else
                    {
                        intIsYesterday = 0;
                    }
                    int result = searchAndHightlightUnresolvedTrains(firstTrainNumber, 1, intIsYesterday, secondTrainNumber);
                    if (result == -1 || result == 2)
                    {//再找第二个车次
                        int secondResult = searchAndHightlightUnresolvedTrains(secondTrainNumber, 1, intIsYesterday, firstTrainNumber);
                        if (secondResult == -1 || secondResult == 2)
                        {//加入未识别车次豪华套餐
                            return " " + rawTrainNumber;
                        }
                    }
                }
            }
            return "";
        }

        private void compareWithYesterday(List<DailySchedule> dailyScheduleModel, List<EMUCheckModel> emuCheckModel)
        {
            //先两天命令比一下,找到不同的再去基本图里找找有没有这趟车
            //保存对比文字
            List<string> comparedText = new List<string>();
            //无区别的列车列表
            string normalText = "";
            List<CommandModel> addedTodayCM = new List<CommandModel>();
            List<CommandModel> addedYesterdayCM = new List<CommandModel>();
            int count = 1;
            int normalCount = 1;
            if (dailyScheduleModel == null && emuCheckModel == null)
            {
                return;
            }
            if (dailyScheduleModel != null)
            {
                for (int j = 0; j < commandModel.Count; j++)
                {//先过滤基本图
                    CommandModel _cm = commandModel[j];
                    bool hasGotIt = false;
                    for (int i = 0; i < dailyScheduleModel.Count; i++)
                    {
                        if (dailyScheduleModel[i].trainNumber == null)
                        {
                            continue;
                        }
                        if (dailyScheduleModel[i].trainNumber.Trim().Contains("G2205"))
                        {
                            if (commandModel[j].trainNumber.Trim().Contains("G2205"))
                            {
                                int c = 0;
                            }
                        }

                        if (dailyScheduleModel[i].trainNumber.Trim().Split('/')[0].Trim().Equals(commandModel[j].trainNumber.Trim()) ||
                        dailyScheduleModel[i].trainNumber.Trim().Split('/')[0].Trim().Equals(commandModel[j].secondTrainNumber.Trim()))
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
                        if (dailyScheduleModel[i].trainNumber == null)
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
            if (emuCheckModel != null)
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
                            if (!result.Contains("相同"))
                            {
                                comparedText.Add(count.ToString() + "、" + result);
                                count++;
                            }
                            normalText = normalText + "\n" + normalCount + "、" + result;
                            normalCount++;
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
                                if (!result.Contains("相同"))
                                {
                                    comparedText.Add(count.ToString() + "、" + result);
                                    count++;
                                }
                                normalText = normalText + "\n" + normalCount + "、" + result;
                                normalCount++;
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
            if (allResults.Trim().Replace(" ", "").Length == 0)
            {
                comparedResult_rtb.Text = "对比完成，两日客调无区别。对比结果如下：\n" + normalText;
            }
            else
            {
                comparedResult_rtb.Text = "对比结果：\n" + allResults + "\n全部结果如下：\n" + normalText;
            }
            createStaticDoc(allResults,normalText);

        }
        //班计划统计word
        private void createStaticDoc(string allResults, string normalText)
        {

            if (allResults.Length == 0 && normalText.Length == 0)
            {
                return;
            }
            Document doc = new Document();

            Section section = doc.AddSection();
            //Add Paragraph
            Paragraph Para1 = section.AddParagraph();
            //Append Text
            string title = "列车运行变化统计";
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
            FileStream fs = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + title + ".doc");
            fs.Close();
            Para1.AppendText(title + "\n\n");

            Paragraph Para2 = section.AddParagraph();
            Para2.AppendText("列车开行变化情况：\n" + allResults + "\n\n");

            Paragraph Para3 = section.AddParagraph();
            Para3.AppendText("全部对比结果：\n" + normalText);

            //写入数据并保存
            doc.SaveToFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + title + ".doc", FileFormat.Doc);
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            //info.WorkingDirectory = Application.StartupPath;
            info.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + title + ".doc";
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
            bool hasDifference = false;
            if (!today.trainModel.Equals(yesterday.trainModel) ||
                !today.trainConnectType.Equals(yesterday.trainConnectType) ||
                !today.streamStatus.Equals(yesterday.streamStatus))
            {
                compareText = today.trainNumber + "次；";
                if (!today.streamStatus.Equals(yesterday.streamStatus))
                {
                    hasDifference = true;
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
                    hasDifference = true;
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
            if (hasDifference == false)
            {
                compareText = today.trainNumber + "次(相同)；" + "位于昨日(↑)命令第" + yesterday.trainIndex + "条，今日(←)第" + today.trainIndex + "条。";
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
                                                        string extraText = _ds.extraText + " 、（南）";
                                                        _ds.extraText = extraText;
                                                    }
                                                    else
                                                    {//正常情况
                                                        string extraText = _ds.extraText + " 、（北）";
                                                        _ds.extraText = extraText;
                                                    }
                                                }
                                            }

                                        }
                                        else if (_ds.upOrDown == 1)
                                        {
                                            string extraText = _ds.extraText + " 、（南）";
                                            _ds.extraText = extraText;
                                        }
                                        else
                                        {
                                            string extraText = _ds.extraText + " 、（南/北停靠无法识别）";
                                            _ds.extraText = extraText;
                                        }
                                        if (_ds.extraText.Contains("重联"))
                                        {
                                            string extraText = _ds.extraText.Replace("、重联", "").Replace("重联", "");
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
                                        string extraText = _ds.extraText.Replace("、（南）", "").Replace("（南）", "").Replace("、（北）", "").Replace("（北）", "").Replace("、(南)", "").Replace("(南)", "").Replace("、(北)", "").Replace("(北)", "");
                                        _ds.extraText = extraText;
                                    }
                                    if (_ds.extraText.Contains("重联"))
                                    {
                                        string extraText = _ds.extraText.Replace("、重联", "").Replace("重联", "");
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
                                        string extraText = _ds.extraText.Replace("、（南）", "").Replace("（南）", "").Replace("、（北）", "").Replace("（北）", "").Replace("、(南)", "").Replace("(南)", "").Replace("、(北)", "").Replace("(北)", "");
                                        _ds.extraText = extraText;
                                    }
                                    if (!_ds.extraText.Contains("重联"))
                                    {
                                        _ds.extraText = _ds.extraText + " 、重联";
                                    }
                                }
                                _ds.trainConnectType = "8+8";
                                break;
                            case 3:
                                if (dailyScheduleModel[i].trainConnectType != null)
                                    if (!dailyScheduleModel[i].trainConnectType.Equals("17"))
                                    {
                                        _ds.hasDifferentPart = true;
                                    }
                                if (_ds.extraText != null)
                                {
                                    if (_ds.extraText.Contains("南") ||
                                        _ds.extraText.Contains("北"))
                                    {
                                        string extraText = _ds.extraText.Replace("、（南）", "").Replace("（南）", "").Replace("、（北）", "").Replace("（北）", "").Replace("、(南)", "").Replace("(南)", "").Replace("、(北)", "").Replace("(北)", "");
                                        _ds.extraText = extraText;
                                    }
                                    if (_ds.extraText.Contains("重联"))
                                    {
                                        string extraText = _ds.extraText.Replace("、重联", "").Replace("重联", "");
                                        _ds.extraText = extraText;
                                    }
                                }
                                _ds.trainConnectType = "17";
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
                                    case 3:
                                        _ds.trainConnectType = "17";
                                        break;
                                }
                                if (!commandModel[j].trainModel.Equals("null"))
                                {
                                    _ds.trainModel = commandModel[j].trainModel;
                                }
                                if (_ds.streamStatus != 0)
                                {
                                    _ds.id = -1;
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
                if (_dailyScheduleModel[j].startTime != null && _dailyScheduleModel[j].startTime.Length != 0)
                {//预售时间
                    if (_dailyScheduleModel[j].startTime.Contains(":"))
                    {
                        int presaleTime = 0;
                        int.TryParse(_dailyScheduleModel[j].startTime.Split(':')[0], out presaleTime);
                        _dailyScheduleModel[j].presaleTime = presaleTime;
                    }
                }
                else if (_dailyScheduleModel[j].stopTime != null && _dailyScheduleModel[j].stopTime.Length != 0)
                {
                    if (_dailyScheduleModel[j].stopTime.Contains(":"))
                    {
                        int presaleTime = 0;
                        int.TryParse(_dailyScheduleModel[j].stopTime.Split(':')[0], out presaleTime);
                        _dailyScheduleModel[j].presaleTime = presaleTime;
                    }
                }
                if (_dailyScheduleModel[j].extraText != null && _dailyScheduleModel[j].extraText.Length != 0)
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
                    if (emuCheckModel[i].trainNumber.Trim().Contains("0G370"))
                    {
                        int test = 0;
                        if (commandModel[i].trainNumber.Trim().Contains("0G370"))
                        {
                            int test1 = 0;
                        }
                    }
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
            //_emuCheckModel.Sort();
            List<EMUCheckModel> _sortedCheckModel = new List<EMUCheckModel>();
            List<EMUCheckModel> _nonContainedCheckModel = new List<EMUCheckModel>();
            //202206 EMUTrain Sort
            foreach(EMUCheckModel _temp in _emuCheckModel)
            {
                if(_temp.startTime != null)
                {
                    if(_temp.startTime.Trim().Length != 0)
                    {
                        _sortedCheckModel.Add(_temp);
                    }
                    else
                    {
                        _nonContainedCheckModel.Add(_temp);
                    }
                }
                else
                {
                    _nonContainedCheckModel.Add(_temp);
                }
            }
            _sortedCheckModel.Sort();
            for(int ij = 0; ij < _sortedCheckModel.Count; ij++)
            {
                _sortedCheckModel[ij].id = ij + 1;
            }
            List<EMUCheckModel> newEMUCheckModel = new List<EMUCheckModel>();
            foreach(EMUCheckModel _temp1 in _nonContainedCheckModel)
            {
                newEMUCheckModel.Add(_temp1);
            }
            foreach (EMUCheckModel _temp2 in _sortedCheckModel)
            {
                newEMUCheckModel.Add(_temp2);
            }
            allEmuCheckModel = newEMUCheckModel;
            //最后一步 打印
            createEMUC_Table();
        }

        //创建班计划
        private void createDailySchedule(bool isCompareingDailySchedues = false)
        {
            //创建Excel文件名称

            try
            {
            FileStream fs = File.Create(Application.StartupPath + "\\" + startPath + "\\" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "班计划.xls");
            if (isCompareingDailySchedues)
            {
                fs.Close();
                fs = File.Create(Application.StartupPath + "\\" + startPath + "\\" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "对比结果.xls");
            }

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

            //对比出不同的值时候的黄色
            ICellStyle differentCellStyle = workbook.CreateCellStyle();
            differentCellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
            differentCellStyle.FillPattern = FillPattern.SolidForeground;
            differentCellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
            differentCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            differentCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            differentCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            differentCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            differentCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            differentCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
            differentCellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

            ICellStyle differentTimeStyle = workbook.CreateCellStyle();
            differentTimeStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
            differentTimeStyle.FillPattern = FillPattern.SolidForeground;
            differentTimeStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
            differentTimeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            differentTimeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            differentTimeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            differentTimeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            differentTimeStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            differentTimeStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
            differentTimeStyle.DataFormat = 20;

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
            differentCellStyle.SetFont(fontNormal);
            differentTimeStyle.SetFont(fontNormal);

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
                    if (!isCompareingDailySchedues)
                    {
                        row.CreateCell(0).SetCellValue(DateTime.Now.AddDays(1).ToString("yyyy.MM.dd") + "日班计划");
                    }
                    else
                    {
                        row.CreateCell(0).SetCellValue(DateTime.Now.AddDays(1).ToString("yyyy.MM.dd") + "日对比");
                    }
                    row.GetCell(0).CellStyle = boldStyle;
                }
                else if (i == 1)
                {
                    row.Height = 32 * 20;
                    for (int count = 0; count < 17; count++)
                    {
                        if (count == 16 && !isCompareingDailySchedues)
                        {
                            break;
                        }
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
                            case 16:
                                if (isCompareingDailySchedues)
                                {
                                    row.CreateCell(count).SetCellValue("对比信息");
                                    sheet.SetColumnWidth(count, 20 * 256);
                                }
                                break;
                        }
                        row.GetCell(count).CellStyle = boldStyle;
                    }
                }
                else
                {
                    row.Height = 15 * 20;
                    for (int column = 0; column < 17; column++)
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
                                if (allDailyScheduleModel[i - 2].startStation.Contains("<difference>"))
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].startStation.Split('<')[0]);
                                    row.GetCell(column).CellStyle = differentCellStyle;
                                }
                                else
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].startStation);
                                }
                                break;
                            case 4:
                                if (allDailyScheduleModel[i - 2].stopStation.Contains("<difference>"))
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].stopStation.Split('<')[0]);
                                    row.GetCell(column).CellStyle = differentCellStyle;
                                }
                                else
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].stopStation);
                                }
                                break;
                                case 5:
                                    DateTime dtStop;
                                    DateTime dtTest;
                                    DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                                    dtFormat.ShortDatePattern = "HH:mm";
                                    if (allDailyScheduleModel[i - 2].stopTime != null && allDailyScheduleModel[i - 2].stopTime.Length != 0)
                                    {
                                        if (allDailyScheduleModel[i - 2].stopTime.Contains("<difference>"))
                                        {
                                            try
                                            {
                                                dtTest = Convert.ToDateTime(allDailyScheduleModel[i - 2].stopTime.Split('<')[0], dtFormat);
                                                row.CreateCell(column).SetCellType(CellType.Numeric);
                                                row.CreateCell(column).SetCellValue(dtTest);
                                                row.GetCell(column).CellStyle = differentTimeStyle;
                                            }
                                            catch
                                            {
                                                row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].stopTime);
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                dtStop = Convert.ToDateTime(allDailyScheduleModel[i - 2].stopTime, dtFormat);
                                                row.CreateCell(column).SetCellType(CellType.Numeric);
                                                row.CreateCell(column).SetCellValue(dtStop);
                                            }
                                            catch
                                            {
                                                row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].stopTime);
                                            }

                                        }

                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].stopTime);
                                    }
                                    break;
                                case 6:
                                    DateTime dtStart;
                                    DateTime dtTestStart;
                                    DateTimeFormatInfo dtFormat1 = new DateTimeFormatInfo();
                                    dtFormat1.ShortDatePattern = "HH:mm";
                                    if (allDailyScheduleModel[i - 2].startTime != null && allDailyScheduleModel[i - 2].startTime.Length != 0)
                                    {
                                        if (allDailyScheduleModel[i - 2].startTime.Contains("<difference>"))
                                        {
                                            try
                                            {
                                                dtTestStart = Convert.ToDateTime(allDailyScheduleModel[i - 2].startTime.Split('<')[0], dtFormat1);
                                                row.CreateCell(column).SetCellType(CellType.Numeric);
                                                row.CreateCell(column).SetCellValue(dtTestStart);
                                                row.GetCell(column).CellStyle = differentTimeStyle;
                                            }
                                            catch
                                            {
                                                row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].startTime);
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                dtStart = Convert.ToDateTime(allDailyScheduleModel[i - 2].startTime, dtFormat1);
                                                row.CreateCell(column).SetCellType(CellType.Numeric);
                                                row.CreateCell(column).SetCellValue(dtStart);
                                            }
                                            catch
                                            {
                                                row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].startTime);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].startTime);
                                    }
                                    break;
                                case 7:
                                if (allDailyScheduleModel[i - 2].stopToStartTime.Contains("<difference>"))
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].stopToStartTime.Split('<')[0]);
                                    row.GetCell(column).CellStyle = differentCellStyle;
                                }
                                else
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].stopToStartTime);
                                }
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
                                if (allDailyScheduleModel[i - 2].trackNum.Contains("<difference>"))
                                {
                                    int.TryParse(allDailyScheduleModel[i - 2].trackNum.Split('<')[0], out outTrackNum);
                                    if (outTrackNum == 0)
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trackNum.Split('<')[0]);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(outTrackNum);
                                    }
                                    row.GetCell(column).CellStyle = differentCellStyle;
                                }
                                else
                                {
                                    int.TryParse(allDailyScheduleModel[i - 2].trackNum, out outTrackNum);
                                    if (outTrackNum == 0)
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trackNum);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(outTrackNum);
                                    }
                                }

                                break;
                            case 12:
                                int outConnectType = 0;
                                if (allDailyScheduleModel[i - 2].trainConnectType.Contains("<difference>"))
                                {
                                    int.TryParse(allDailyScheduleModel[i - 2].trainConnectType.Split('<')[0], out outConnectType);
                                    if (outConnectType == 0)
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trainConnectType.Split('<')[0]);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(outConnectType);
                                    }
                                }
                                else
                                {
                                    int.TryParse(allDailyScheduleModel[i - 2].trainConnectType, out outConnectType);
                                    if (outConnectType == 0)
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trainConnectType);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue(outConnectType);
                                    }
                                }


                                break;
                            case 13:
                                if (allDailyScheduleModel[i - 2].trainModel.Contains("<difference>"))
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trainModel.Split('<')[0]);
                                    row.GetCell(column).CellStyle = differentCellStyle;
                                }
                                else
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trainModel);
                                }
                                break;
                            case 14:
                                if (allDailyScheduleModel[i - 2].trainBelongsTo.Contains("<difference>"))
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trainBelongsTo.Split('<')[0]);
                                    row.GetCell(column).CellStyle = differentCellStyle;
                                }
                                else
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].trainBelongsTo);
                                }
                                break;
                            case 15:
                                if (allDailyScheduleModel[i - 2].extraHasDifference)
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].extraText);
                                    row.GetCell(column).CellStyle = differentCellStyle;
                                }
                                else
                                {
                                    if (allDailyScheduleModel[i - 2].streamStatus != 0)
                                    {
                                        row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].extraText);
                                    }
                                    else
                                    {
                                        row.CreateCell(column).SetCellValue("停运");
                                    }
                                }
                                break;
                            case 16:
                                if (isCompareingDailySchedues)
                                {
                                    row.CreateCell(column).SetCellValue(allDailyScheduleModel[i - 2].tipsText);
                                }
                                break;
                        }
                        if (!isCompareingDailySchedues)
                        {
                            if (column > 15)
                            {
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
            }

            //向excel文件中写入数据并保存
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
            if (!isCompareingDailySchedues)
            {
                info.FileName = Application.StartupPath + "\\" + startPath + "\\" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "班计划.xls";
            }
            else
            {
                info.FileName = Application.StartupPath + "\\" + startPath + "\\" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "对比结果.xls";
            }
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
            catch (Exception e1)
            {
                MessageBox.Show("出现错误，请重试，持续出现问题可联系17638570597（罗思聪）：" + e1.ToString().Split('。')[0] + "。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //综控班计划对比按钮
        private void compareDailySchedue_btn_Click(object sender, EventArgs e)
        {
            SelectPath(true);
        }

        //综控班计划对比
        private void compareDailySchedues()
        {
            List<DailySchedule> _compareingResult = new List<DailySchedule>();
            //读原图
            readBasicTrainTable();
            //读对比图
            readBasicTrainTable(false, true);
            if(allDailyScheduleModel!=null && allDailyScheduleModel.Count!=0 &&
                yesterdayAllDailyScheduleModel != null && yesterdayAllDailyScheduleModel.Count != 0)
            {
                //A有B也有的车，A有B没有的车
                foreach(DailySchedule _ds in allDailyScheduleModel)
                {
                    bool hasGotIt = false;
                    foreach (DailySchedule _dsCompare in yesterdayAllDailyScheduleModel)
                    {
                        DailySchedule originalDS = new DailySchedule();
                        DailySchedule compareDS = new DailySchedule();
                        if(_ds.trainNumber.Contains("/")&& _dsCompare.trainNumber.Contains("/"))
                        {
                            List<string> originalTrainNumber = splitTrainNumber(_ds.trainNumber);
                            List<string> compareTrainNumber = splitTrainNumber(_dsCompare.trainNumber);
                            if(originalTrainNumber.Count >1 && compareTrainNumber.Count > 1)
                            {
                                if (originalTrainNumber[0].Equals(compareTrainNumber[0]) ||
                                    originalTrainNumber[0].Equals(compareTrainNumber[1]))
                                {//相同车次
                                    hasGotIt = true;
                                    //去掉车型里面的汉字
                                    if (_ds.startStation.Equals(_dsCompare.startStation) &&
                                        _ds.stopStation.Equals(_dsCompare.stopStation) &&
                                        _ds.stopTime.Equals(_dsCompare.stopTime) &&
                                        _ds.startTime.Equals(_dsCompare.startTime) &&
                                        _ds.stopToStartTime.Equals(_dsCompare.stopToStartTime) &&
                                        _ds.stopStartStatus.Equals(_dsCompare.stopStartStatus) &&
                                        _ds.trainBelongsTo.Equals(_dsCompare.trainBelongsTo) &&
                                        _ds.trackNum.Equals(_dsCompare.trackNum) &&
                                        _ds.trainConnectType.Equals(_dsCompare.trainConnectType) &&
                                        _ds.ratedSeats.Equals(_dsCompare.ratedSeats) &&
                                        Regex.Replace(_ds.trainModel, @"[\u4e00-\u9fa5]", "").Equals(Regex.Replace(_dsCompare.trainModel, @"[\u4e00-\u9fa5]", "")) &&
                                        _ds.extra_doubleConnected == _dsCompare.extra_doubleConnected &&
                                        _ds.extra_original == _dsCompare.extra_original &&
                                        _ds.extra_plugingWater == _dsCompare.extra_plugingWater &&
                                        _ds.extra_reversedTrain == _dsCompare.extra_reversedTrain &&
                                        _ds.extra_rushHourTrain == _dsCompare.extra_rushHourTrain &&
                                        _ds.extra_stoppingPlace == _dsCompare.extra_stoppingPlace &&
                                        _ds.extra_terminal == _dsCompare.extra_terminal &&
                                        _ds.extra_unloading == _dsCompare.extra_unloading &&
                                        _ds.extra_weekendTrain == _dsCompare.extra_weekendTrain)
                                    {//对比内容 始发站 终到站 到时 开时 站停时 始发终到状态 归属 股道 编组方式 定员 车型
                                        break;
                                    }
                                    else
                                    {
                                        DailySchedule _tempOriginalDS = new DailySchedule();
                                        DailySchedule _tempCompareDS = new DailySchedule();
                                        _tempOriginalDS = (DailySchedule)_ds.Clone();
                                        _tempCompareDS = (DailySchedule)_dsCompare.Clone();
                                        if (!_ds.startStation.Equals(_dsCompare.startStation))
                                        {
                                            _tempOriginalDS.startStation = _tempOriginalDS.startStation + "<difference>";
                                            _tempCompareDS.startStation = _tempCompareDS.startStation + "<difference>";
                                        }
                                        if (!_ds.stopStation.Equals(_dsCompare.stopStation))
                                        {
                                            _tempOriginalDS.stopStation = _tempOriginalDS.stopStation + "<difference>";
                                            _tempCompareDS.stopStation = _tempCompareDS.stopStation + "<difference>";
                                        }
                                        if (!_ds.stopTime.Equals(_dsCompare.stopTime))
                                        {
                                            _tempOriginalDS.stopTime = _tempOriginalDS.stopTime + "<difference>";
                                            _tempCompareDS.stopTime = _tempCompareDS.stopTime + "<difference>";
                                        }
                                        if (!_ds.startTime.Equals(_dsCompare.startTime))
                                        {
                                            _tempOriginalDS.startTime = _tempOriginalDS.startTime + "<difference>";
                                            _tempCompareDS.startTime = _tempCompareDS.startTime + "<difference>";
                                        }
                                        if (!_ds.stopToStartTime.Equals(_dsCompare.stopToStartTime))
                                        {
                                            _tempOriginalDS.stopToStartTime = _tempOriginalDS.stopToStartTime + "<difference>";
                                            _tempCompareDS.stopToStartTime = _tempCompareDS.stopToStartTime + "<difference>";
                                        }
                                        if (!_ds.trainBelongsTo.Equals(_dsCompare.trainBelongsTo))
                                        {
                                            _tempOriginalDS.trainBelongsTo = _tempOriginalDS.trainBelongsTo + "<difference>";
                                            _tempCompareDS.trainBelongsTo = _tempCompareDS.trainBelongsTo + "<difference>";
                                        }
                                        if (!_ds.trackNum.Equals(_dsCompare.trackNum))
                                        {
                                            _tempOriginalDS.trackNum = _tempOriginalDS.trackNum + "<difference>";
                                            _tempCompareDS.trackNum = _tempCompareDS.trackNum + "<difference>";
                                        }
                                        if (!_ds.trainConnectType.Equals(_dsCompare.trainConnectType))
                                        {
                                            _tempOriginalDS.trainConnectType = _tempOriginalDS.trainConnectType + "<difference>";
                                            _tempCompareDS.trainConnectType = _tempCompareDS.trainConnectType + "<difference>";
                                        }
                                        if (!_ds.ratedSeats.Equals(_dsCompare.ratedSeats))
                                        {
                                            _tempOriginalDS.ratedSeats = _tempOriginalDS.ratedSeats + "<difference>";
                                            _tempCompareDS.ratedSeats = _tempCompareDS.ratedSeats + "<difference>";
                                        }
                                        if(!Regex.Replace(_ds.trainModel, @"[\u4e00-\u9fa5]", "").Equals(Regex.Replace(_dsCompare.trainModel, @"[\u4e00-\u9fa5]", "")))
                                        {
                                            _tempOriginalDS.trainModel = _tempOriginalDS.trainModel + "<difference>";
                                            _tempCompareDS.trainModel = _tempCompareDS.trainModel + "<difference>";
                                        }
                                        if(_ds.extra_doubleConnected != _dsCompare.extra_doubleConnected ||
                                            _ds.extra_original != _dsCompare.extra_original ||
                                            _ds.extra_plugingWater != _dsCompare.extra_plugingWater ||
                                            _ds.extra_reversedTrain != _dsCompare.extra_reversedTrain ||
                                            _ds.extra_rushHourTrain != _dsCompare.extra_rushHourTrain ||
                                            _ds.extra_stoppingPlace != _dsCompare.extra_stoppingPlace ||
                                            _ds.extra_terminal != _dsCompare.extra_terminal ||
                                            _ds.extra_unloading != _dsCompare.extra_unloading ||
                                            _ds.extra_weekendTrain != _dsCompare.extra_weekendTrain)
                                        {
                                            _ds.extraHasDifference = true;
                                            _dsCompare.extraHasDifference = true;
                                        }
                                        _tempOriginalDS.tipsText = "原计划";
                                        _tempCompareDS.tipsText = "↑对比计划↑";
                                        _compareingResult.Add(_tempOriginalDS);
                                        _compareingResult.Add(_tempCompareDS);
                                    }
                                }
                            }
                        }
                        else if(!_ds.trainNumber.Contains("/") && !_dsCompare.trainNumber.Contains("/"))
                        {
                            if (_ds.trainNumber.Equals(_dsCompare.trainNumber))
                            {//相同车次
                                hasGotIt = true;
                                if (_ds.startStation.Equals(_dsCompare.startStation) &&
                                    _ds.stopStation.Equals(_dsCompare.stopStation) &&
                                       _ds.stopTime.Equals(_dsCompare.stopTime) &&
                                       _ds.startTime.Equals(_dsCompare.startTime) &&
                                       _ds.stopToStartTime.Equals(_dsCompare.stopToStartTime) &&
                                       _ds.stopStartStatus.Equals(_dsCompare.stopStartStatus) &&
                                       _ds.trainBelongsTo.Equals(_dsCompare.trainBelongsTo) &&
                                       _ds.trackNum.Equals(_dsCompare.trackNum) &&
                                       _ds.trainConnectType.Equals(_dsCompare.trainConnectType) &&
                                       _ds.ratedSeats.Equals(_dsCompare.ratedSeats) &&
                                       _ds.trainModel.Equals(_dsCompare.trainModel)&&
                                        _ds.extra_doubleConnected == _dsCompare.extra_doubleConnected &&
                                        _ds.extra_original == _dsCompare.extra_original &&
                                        _ds.extra_plugingWater == _dsCompare.extra_plugingWater &&
                                        _ds.extra_reversedTrain == _dsCompare.extra_reversedTrain &&
                                        _ds.extra_rushHourTrain == _dsCompare.extra_rushHourTrain &&
                                        _ds.extra_stoppingPlace == _dsCompare.extra_stoppingPlace &&
                                        _ds.extra_terminal == _dsCompare.extra_terminal &&
                                        _ds.extra_unloading == _dsCompare.extra_unloading &&
                                        _ds.extra_weekendTrain == _dsCompare.extra_weekendTrain)
                                {//对比内容 始发站 终到站 到时 开时 站停时 始发终到状态 归属 股道 编组方式 定员 车型
                                    break;
                                }
                                else
                                {
                                    DailySchedule _tempOriginalDS = new DailySchedule();
                                    DailySchedule _tempCompareDS = new DailySchedule();
                                    _tempOriginalDS = (DailySchedule)_ds.Clone();
                                    _tempCompareDS = (DailySchedule)_dsCompare.Clone();
                                    if (!_ds.startStation.Equals(_dsCompare.startStation))
                                    {
                                        _tempOriginalDS.startStation = _tempOriginalDS.startStation + "<difference>";
                                        _tempCompareDS.startStation = _tempCompareDS.startStation + "<difference>";
                                    }
                                    if (!_ds.stopStation.Equals(_dsCompare.stopStation))
                                    {
                                        _tempOriginalDS.stopStation = _tempOriginalDS.stopStation + "<difference>";
                                        _tempCompareDS.stopStation = _tempCompareDS.stopStation + "<difference>";
                                    }
                                    if (!_ds.stopTime.Equals(_dsCompare.stopTime))
                                    {
                                        _tempOriginalDS.stopTime = _tempOriginalDS.stopTime + "<difference>";
                                        _tempCompareDS.stopTime = _tempCompareDS.stopTime + "<difference>";
                                    }
                                    if (!_ds.startTime.Equals(_dsCompare.startTime))
                                    {
                                        _tempOriginalDS.startTime = _tempOriginalDS.startTime + "<difference>";
                                        _tempCompareDS.startTime = _tempCompareDS.startTime + "<difference>";
                                    }
                                    if (!_ds.stopToStartTime.Equals(_dsCompare.stopToStartTime))
                                    {
                                        _tempOriginalDS.stopToStartTime = _tempOriginalDS.stopToStartTime + "<difference>";
                                        _tempCompareDS.stopToStartTime = _tempCompareDS.stopToStartTime + "<difference>";
                                    }
                                    if (!_ds.trainBelongsTo.Equals(_dsCompare.trainBelongsTo))
                                    {
                                        _tempOriginalDS.trainBelongsTo = _tempOriginalDS.trainBelongsTo + "<difference>";
                                        _tempCompareDS.trainBelongsTo = _tempCompareDS.trainBelongsTo + "<difference>";
                                    }
                                    if (!_ds.trackNum.Equals(_dsCompare.trackNum))
                                    {
                                        _tempOriginalDS.trackNum = _tempOriginalDS.trackNum + "<difference>";
                                        _tempCompareDS.trackNum = _tempCompareDS.trackNum + "<difference>";
                                    }
                                    if (!_ds.trainConnectType.Equals(_dsCompare.trainConnectType))
                                    {
                                        _tempOriginalDS.trainConnectType = _tempOriginalDS.trainConnectType + "<difference>";
                                        _tempCompareDS.trainConnectType = _tempCompareDS.trainConnectType + "<difference>";
                                    }
                                    if (!_ds.ratedSeats.Equals(_dsCompare.ratedSeats))
                                    {
                                        _tempOriginalDS.ratedSeats = _tempOriginalDS.ratedSeats + "<difference>";
                                        _tempCompareDS.ratedSeats = _tempCompareDS.ratedSeats + "<difference>";
                                    }
                                    if (!Regex.Replace(_ds.trainModel, @"[\u4e00-\u9fa5]", "").Equals(Regex.Replace(_dsCompare.trainModel, @"[\u4e00-\u9fa5]", "")))
                                    {
                                        _tempOriginalDS.trainModel = _tempOriginalDS.trainModel + "<difference>";
                                        _tempCompareDS.trainModel = _tempCompareDS.trainModel + "<difference>";
                                    }
                                    if (_ds.extra_doubleConnected != _dsCompare.extra_doubleConnected ||
                                        _ds.extra_original != _dsCompare.extra_original ||
                                        _ds.extra_plugingWater != _dsCompare.extra_plugingWater ||
                                        _ds.extra_reversedTrain != _dsCompare.extra_reversedTrain ||
                                        _ds.extra_rushHourTrain != _dsCompare.extra_rushHourTrain ||
                                        _ds.extra_stoppingPlace != _dsCompare.extra_stoppingPlace ||
                                        _ds.extra_terminal != _dsCompare.extra_terminal ||
                                        _ds.extra_unloading != _dsCompare.extra_unloading ||
                                        _ds.extra_weekendTrain != _dsCompare.extra_weekendTrain)
                                    {
                                        _ds.extraHasDifference = true;
                                        _dsCompare.extraHasDifference = true;
                                    }
                                    _tempOriginalDS.tipsText = "原计划";
                                    _tempCompareDS.tipsText = "↑对比计划↑";
                                    _compareingResult.Add(_tempOriginalDS);
                                    _compareingResult.Add(_tempCompareDS);
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (!hasGotIt)
                    {//A有B没有的车
                        DailySchedule _tempOriginalDS = new DailySchedule();
                        _tempOriginalDS = (DailySchedule)_ds.Clone();
                        _tempOriginalDS.tipsText = "原计划中新增车次";
                        _compareingResult.Add(_tempOriginalDS);
                    }
                }
                //B多的车
                foreach (DailySchedule _ds in yesterdayAllDailyScheduleModel)
                {
                    bool hasGotIt = false;
                    foreach (DailySchedule _dsCompare in allDailyScheduleModel)
                    {
                        DailySchedule originalDS = new DailySchedule();
                        DailySchedule compareDS = new DailySchedule();
                        if (_ds.trainNumber.Contains("/") && _dsCompare.trainNumber.Contains("/"))
                        {
                            List<string> originalTrainNumber = splitTrainNumber(_ds.trainNumber);
                            List<string> compareTrainNumber = splitTrainNumber(_dsCompare.trainNumber);
                            if (originalTrainNumber.Count > 1 && compareTrainNumber.Count > 1)
                            {
                                if (originalTrainNumber[0].Equals(compareTrainNumber[0]) ||
                                    originalTrainNumber[0].Equals(compareTrainNumber[1]))
                                {//相同车次
                                    hasGotIt = true;
                                    break;
                                }
                            }
                        }
                        else if (!_ds.trainNumber.Contains("/") && !_dsCompare.trainNumber.Contains("/"))
                        {
                            if (_ds.trainNumber.Equals(_dsCompare.trainNumber))
                            {//相同车次
                                hasGotIt = true;
                                break;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (hasGotIt)
                    {
                        continue;
                    }
                    else
                    {//多的
                        DailySchedule _tempOriginalDS = new DailySchedule();
                        _tempOriginalDS = (DailySchedule)_ds.Clone();
                        _tempOriginalDS.tipsText = "对比计划中新增车次";
                        _compareingResult.Add(_tempOriginalDS);
                    }
                }
            }
            //因为创建班计划写定了是用当日的班计划模型 所以改起来很麻烦 故采用赋值方式
            List<DailySchedule> _tDSAllModel = new List<DailySchedule>();
            foreach(DailySchedule _tDS in allDailyScheduleModel)
            {
                DailySchedule _cloneTDS = (DailySchedule)_tDS.Clone();
                _tDSAllModel.Add(_cloneTDS);
            }
            allDailyScheduleModel = new List<DailySchedule>();
            allDailyScheduleModel = _compareingResult;
            createDailySchedule(true);
            allDailyScheduleModel = _tDSAllModel;
        }

        //车次一拆为二
        private List<string> splitTrainNumber(string trainNumber)
        {
            if(!trainNumber.Contains("/"))
            {
                return null;
            }
            string[] trainWithDoubleNumber = trainNumber.Split('/');
            bool _hasGotIt = false;
            string firstTrainWord = trainNumber.Split('/')[0];
            string secondTrainWord = "";
            for (int q = 0; q < firstTrainWord.Length; q++)
            {
                if (q < firstTrainWord.Length - trainWithDoubleNumber[1].Length)
                {
                    secondTrainWord = secondTrainWord + firstTrainWord[q];
                }
                else
                {
                    if (_hasGotIt != true)
                    {
                        secondTrainWord = secondTrainWord + trainWithDoubleNumber[1];
                        _hasGotIt = true;
                    }
                }
                if (_hasGotIt)
                {
                    break;
                }
            }
            List<string> _values = new List<string>();
            _values.Add(firstTrainWord);
            _values.Add(secondTrainWord);
            return _values;
        }

        //动车所填车型
        private void trainTypeAutoComplete(bool isProjectHelper = false)
        {
            try
            {
                if (ExcelFile == null || ExcelFile.Count == 0)
                {
                    MessageBox.Show("请重新选择时刻表文件~", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //白班和夜班的开始所在行
                int nightStart = -1;
                int morningStart = -1;
                //是否是II场
                int secondSection = -1;
                //如果夜班在前面的话
                bool nightFirst = false;
                //如果这个车是前一天的话
                bool isYesterday = false;
                IWorkbook workbook = null;  //新建IWorkbook对象  
                List<EMUGarageTableModel> _eMUGarageTableModels = new List<EMUGarageTableModel>();
                if (isProjectHelper)
                {
                    allEMUGarageTableModels = new List<EMUGarageTableModel>();
                    //动车走行线模型
                }
                //标题行
                int titleRowNum = -1;
                //动车所股道所在列
                int firstTrackNumColumn = -1;
                int secondTrackNumColumn = -1;
                //备注列
                int first_extraColumn = -1;
                int second_extraColumn = -1;
                //动车所车型车号列
                int firstTrainIDColumn = -1;
                int secondTrainIDColumn = -1;
                string fileName = ExcelFile[0];
                /*
                try
                {
                */
                    FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                    {
                    MessageBox.Show("提示：若出现错误，请将excel时刻表文件转存为2003版格式(.XLS)");
                        try
                        {
                            workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("时刻表文件出现损坏\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
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
                            MessageBox.Show("时刻表文件出现损坏（或时刻表无效）\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;

                        }
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
                    HSSFFont fontNormalID = (HSSFFont)workbook.CreateFont();
                    fontNormalID.FontName = "宋体";//字体  
                    fontNormalID.FontHeightInPoints = 16;//字号  
                    normalStyle.SetFont(fontNormalID);

                    //表格样式
                    ICellStyle trackNumStyle = workbook.CreateCellStyle();
                    trackNumStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    trackNumStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    trackNumStyle.WrapText = true;
                    trackNumStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    trackNumStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    trackNumStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    trackNumStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                    HSSFFont trackNumFont = (HSSFFont)workbook.CreateFont();
                    trackNumFont.FontName = "宋体";//字体  
                    trackNumFont.FontHeightInPoints = 14;//字号  
                    trackNumStyle.SetFont(trackNumFont);

                    //表格样式
                    ICellStyle notRecommandedTrackNumStyle = workbook.CreateCellStyle();
                    notRecommandedTrackNumStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    notRecommandedTrackNumStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    notRecommandedTrackNumStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                    notRecommandedTrackNumStyle.FillPattern = FillPattern.SolidForeground;
                    notRecommandedTrackNumStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                    notRecommandedTrackNumStyle.WrapText = true;
                    notRecommandedTrackNumStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    notRecommandedTrackNumStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    notRecommandedTrackNumStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    notRecommandedTrackNumStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                    notRecommandedTrackNumStyle.SetFont(trackNumFont);

                    //表格样式
                    ICellStyle normalNumberStyle = workbook.CreateCellStyle();
                    normalNumberStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    normalNumberStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    normalNumberStyle.WrapText = true;
                    normalNumberStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    normalNumberStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    normalNumberStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    normalNumberStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                    HSSFFont fontNormalNumber = (HSSFFont)workbook.CreateFont();
                    fontNormalNumber.FontName = "宋体";//字体  
                    fontNormalNumber.FontHeightInPoints = 17;//字号  
                    normalNumberStyle.SetFont(fontNormalNumber);

                    //表格样式
                    ICellStyle stoppedTrainStyle = workbook.CreateCellStyle();
                    stoppedTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                    stoppedTrainStyle.FillPattern = FillPattern.SolidForeground;
                    stoppedTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                    stoppedTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    stoppedTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    stoppedTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    stoppedTrainStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    stoppedTrainStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                    stoppedTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                HSSFFont font = (HSSFFont)workbook.CreateFont();
                font.FontName = "宋体";//字体  
                font.IsStrikeout = true;
                font.IsBold = true;
                font.IsItalic = true;
                font.FontHeightInPoints =17;//字号  
                stoppedTrainStyle.SetFont(font);
                stoppedTrainStyle.SetFont(font);

                    //表格样式
                    ICellStyle yesterdayNumberStyle = workbook.CreateCellStyle();
                    yesterdayNumberStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    yesterdayNumberStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    yesterdayNumberStyle.WrapText = true;
                    yesterdayNumberStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    yesterdayNumberStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    yesterdayNumberStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    yesterdayNumberStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直
                    HSSFFont fontYesterdayNumber = (HSSFFont)workbook.CreateFont();
                    fontYesterdayNumber.FontName = "宋体";//字体  
                    fontYesterdayNumber.FontHeightInPoints = 17;//字号  
                    fontYesterdayNumber.Underline = NPOI.SS.UserModel.FontUnderlineType.Double;//下划线
                    fontYesterdayNumber.IsItalic = true;//斜体
                    yesterdayNumberStyle.SetFont(fontYesterdayNumber);

                    ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  
                    IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  
                    IRow titleRow = sheet.GetRow(3);
                    string failedLoadingTrain = "";
                    for (int i = 0; i <= sheet.LastRowNum; i++)  //对工作表每一行  
                    {
                        isYesterday = false;
                        row = sheet.GetRow(i);   //row读入第i行数据  
                        if (row != null)
                        {
                        if(row.GetCell (0) == null)
                        {
                            row.CreateCell(0);
                        }
                            //判断当前位置是白班还是夜班/标题行
                            {
                                {//标题行
                                    titleRowNum = i;
                                    titleRow = sheet.GetRow(i);
                                    //找一下动车所的股道所在列
                                    for (int b = 0; b <= row.LastCellNum; b++)
                                    {
                                        if (row.GetCell(b) == null)
                                        {
                                            continue;
                                        }
                                        if (row.GetCell(b).ToString().Contains("动车所"))
                                        {
                                        IRow nextRow = sheet.GetRow(i + 1);
                                        if(nextRow != null)
                                        {
                                            if(nextRow.GetCell(b) != null)
                                            {
                                                if (nextRow.GetCell(b).ToString().Trim().Contains("股道"))
                                                {
                                                    if (firstTrackNumColumn == -1)
                                                    {
                                                        firstTrackNumColumn = b;
                                                    }
                                                    else
                                                    {
                                                        secondTrackNumColumn = b;
                                                    }
                                                }
                                            }
                                            if (nextRow.GetCell(b+1) != null)
                                            {
                                                if (nextRow.GetCell(b+1).ToString().Trim().Contains("股道"))
                                                {
                                                    if (firstTrackNumColumn == -1)
                                                    {
                                                        firstTrackNumColumn = b+1;
                                                    }
                                                    else
                                                    {
                                                        secondTrackNumColumn = b+1;
                                                    }
                                                }
                                            }
                                        }

                                        }
                                    if (row.GetCell(b).ToString().Contains("车型"))
                                    {
                                        if (firstTrainIDColumn == -1)
                                        {
                                            firstTrainIDColumn = b;
                                        }
                                        else
                                        {
                                            secondTrainIDColumn = b;
                                        }

                                    }
                                    if (row.GetCell(b).ToString().Contains("备注"))
                                    {
                                        if(titleRowNum == -1)
                                        {
                                            titleRowNum = i;
                                        }
                                        if (first_extraColumn == -1)
                                        {
                                            first_extraColumn = b;
                                        }
                                        else
                                        {
                                            second_extraColumn = b;
                                        }
                                    }
                                }
                                }
                            if (i == 0&& row.GetCell(0).ToString().Contains("-") && row.GetCell(0).ToString().Contains("动车所"))
                            {
                                row.GetCell(0).SetCellValue(row.GetCell(0).ToString().Split('-')[0] + "-" + DateTime.Now.ToString("yyyy.MM.dd"));
                            }
                            else if(i == 0 && row.GetCell(0).ToString().Contains("动车所"))
                            {
                                row.GetCell(0).SetCellValue(row.GetCell(0).ToString() + "-"+DateTime.Now.ToString("yyyy.MM.dd"));
                            }
                                if(secondSection == -1)
                                {//是否二场
                                /*
                                    if(row.GetCell(0).ToString().Contains("二场") ||
                                        row.GetCell(0).ToString().Contains("II场") ||
                                        row.GetCell(0).ToString().Contains("2场"))
                                    {
                                        nightStart = i;
                                        secondSection = 1;
                                    }
                                */
                                }
                                if (nightStart == -1)
                                {
                                    if (row.GetCell(0).ToString().Contains("夜班"))
                                    {
                                        nightStart = i;
                                    }
                                }
                                if (morningStart == -1)
                                {
                                    if (row.GetCell(0).ToString().Contains("白班"))
                                    {
                                        morningStart = i;
                                    }
                                }
                            }
                            if (nightStart != -1 && morningStart != -1)
                            {//如果白班夜班都获取到了 判断一下两个的大小
                                if (nightStart < morningStart)
                                {
                                    nightFirst = true;
                                }
                                else if (nightStart > morningStart)
                                {
                                    nightFirst = false;
                                }
                            }
                            else if (nightStart != -1 && morningStart == -1)
                            {
                                nightFirst = true;
                            }
                            else if (nightStart == -1 && morningStart != -1)
                            {
                                nightFirst = false;
                            }
                            bool hasFoundThisRow = false;
                            Regex regexOnlyNumAndAlphabeta = new Regex(@"^[A-Za-z0-9]+$");
                            //共线车
                            bool isInUpRow = false;
                            for (int j = 0; j <= row.LastCellNum; j++)  //对工作表每一列  
                            {
                                if (row.GetCell(j) != null)
                                {
                                    if ((row.GetCell(j).ToString().Contains("G") ||
                                        row.GetCell(j).ToString().Contains("D") ||
                                        row.GetCell(j).ToString().Contains("C") ||
                                        row.GetCell(j).ToString().Contains("J")) &&
                                        regexOnlyNumAndAlphabeta.IsMatch(row.GetCell(j).ToString().Trim())
                                        && j != firstTrackNumColumn && j != secondTrackNumColumn)
                                    {
                                        if (row.GetCell(j + 1) == null)
                                        {
                                            row.CreateCell(j + 1);
                                        }
                                        //新任务：找对应的动车走行线
                                        //添加一下车次，并且找一下动车走行线
                                        if (isProjectHelper)
                                        {
                                            EMUGarageTableModel _gtm = new EMUGarageTableModel();
                                            _gtm.id = i;
                                            _gtm.trainNumber = row.GetCell(j).ToString();
                                            //判断是入库还是出库
                                            if (row.GetCell(j).ToString().ToCharArray()[row.GetCell(j).ToString().ToCharArray().Length - 1] % 2 == 0)
                                            {//偶数结尾 入库
                                                _gtm.isGettingInGarage = 1;
                                            }
                                            else if (row.GetCell(j).ToString().ToCharArray()[row.GetCell(j).ToString().ToCharArray().Length - 1] % 2 == 1)
                                            {
                                                _gtm.isGettingInGarage = 0;
                                            }
                                            for (int line = j + 1; line <= row.LastCellNum; line++)
                                            {
                                            if(line > 255)
                                            {
                                                break;
                                            }
                                            if(row.GetCell(line) == null)
                                            {
                                                row.CreateCell(line);
                                            }
                                                if ((row.GetCell(line).ToString().Contains("G") ||
                                                    row.GetCell(line).ToString().Contains("D") ||
                                                        row.GetCell(line).ToString().Contains("C") ||
                                                        row.GetCell(line).ToString().Contains("J")) &&
                                                        regexOnlyNumAndAlphabeta.IsMatch(row.GetCell(line).ToString().Trim()) &&
                                                        line != firstTrackNumColumn && line != secondTrackNumColumn)
                                                {
                                                    break;
                                                }
                                                if (titleRow != null && titleRow.GetCell(line) != null && titleRow.GetCell(line).ToString().Contains("线别"))
                                                {
                                                    if (row.GetCell(line) != null && row.GetCell(line).ToString().Length != 0)
                                                    {
                                                        int outLine = -1;
                                                        int.TryParse(row.GetCell(line).ToString(), out outLine);
                                                        if (outLine != -1)
                                                        {
                                                            _gtm.trackLine = outLine;
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        IRow upRow = sheet.GetRow(i - 1);
                                                        if (upRow.GetCell(line) != null)
                                                        {
                                                            int outLine = -1;
                                                            int.TryParse(upRow.GetCell(line).ToString(), out outLine);
                                                            if (outLine != -1)
                                                            {
                                                                _gtm.trackLine = outLine;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            _eMUGarageTableModels.Add(_gtm);
                                            continue;
                                        }
                                        //先判断这个是昨天还是今天的 昨天的在夜班里 时间范围是18:00-次日0:30
                                        //先看是不是在夜班里
                                        if ((nightFirst && (i > nightStart && i < morningStart) || (i > nightStart && morningStart == -1)) ||
                                            (!nightFirst && i > nightStart))
                                        {
                                            for (int timeColumn = j + 1; timeColumn <= row.LastCellNum; timeColumn++)
                                            {
                                                //空的直接跳过
                                                if (row.GetCell(timeColumn) == null)
                                                {
                                                    continue;
                                                }
                                                //如果找到的是车次 说明找过头了 直接跳出
                                                if ((row.GetCell(timeColumn).ToString().Contains("G") ||
                                                        row.GetCell(timeColumn).ToString().Contains("D") ||
                                                        row.GetCell(timeColumn).ToString().Contains("C") ||
                                                        row.GetCell(timeColumn).ToString().Contains("J")) &&
                                                        regexOnlyNumAndAlphabeta.IsMatch(row.GetCell(timeColumn).ToString().Trim()) &&
                                                        timeColumn != firstTrackNumColumn && timeColumn != secondTrackNumColumn)
                                                {
                                                    break;
                                                }
                                                if((row.GetCell(j + 2) == null)){
                                                row.CreateCell(j + 2);
                                            }
                                            if ((row.GetCell(j + 3) == null))
                                            {
                                                row.CreateCell(j + 3);
                                            }
                                            if ((row.GetCell(j + 2).ToString().Length == 0 && row.GetCell(j + 3).ToString().Length == 0))
                                                {
                                                    //说明是和别人共用一格 但是在下面（目标单元格被挡住了）所以往上挪一行填
                                                    row = sheet.GetRow(i - 1);
                                                    if (row.GetCell(j + 1) == null)
                                                    {
                                                        row.CreateCell(j + 1);
                                                    }
                                                }
                                                if (Regex.IsMatch(row.GetCell(timeColumn).ToString().Trim(), @"[0-9]{2}(:)[0-9]{2}") ||
                                                Regex.IsMatch(row.GetCell(timeColumn).ToString().Trim(), @"[0-9]{1}(:)[0-9]{2}") ||
                                                Regex.IsMatch(row.GetCell(timeColumn).ToString().Trim(), @"[0-9]{2}(：)[0-9]{2}") ||
                                                Regex.IsMatch(row.GetCell(timeColumn).ToString().Trim(), @"[0-9]{1}(：)[0-9]{2}"))
                                                {//如果包含时间的话
                                                    string timeStr = row.GetCell(timeColumn).ToString().Trim();
                                                    timeStr = timeStr.Replace("：", ":");
                                                    if (timeStr.Contains(":"))
                                                    {
                                                        int hours = -1;
                                                        int.TryParse(timeStr.Split(':')[0], out hours);
                                                        int minutes = -1;
                                                        int.TryParse(timeStr.Split(':')[1], out minutes);
                                                        if (hours == -1)
                                                        {
                                                            continue;
                                                        }
                                                        if ((hours >= 18 && hours <= 23) ||
                                                            (hours == 0 && minutes <= 30 && minutes >= 0))
                                                        {//18-23点/0:30之前，是昨天的车
                                                            isYesterday = true;
                                                            break;
                                                        }
                                                        else
                                                        {//不是昨天的车
                                                            isYesterday = false;
                                                            break;
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                        row = sheet.GetRow(i);
                                        row.GetCell(j + 1).SetCellValue("");
                                    int aaa = 0;
                                        row.GetCell(j).CellStyle = normalNumberStyle;
                                        //把股道所在列和备注列清空一下
                                        int rowi = i;
                                        if (!hasFoundThisRow)
                                        {
                                            if (row.GetCell(first_extraColumn) != null)
                                            {
                                                row.GetCell(first_extraColumn).SetCellValue(row.GetCell(0).ToString().Split('-')[0]);
                                            }
                                            if(row.GetCell(second_extraColumn) != null)
                                            {
                                                row.GetCell(second_extraColumn).SetCellValue(row.GetCell(second_extraColumn).ToString().Split('-')[0]);
                                            }
                                            if (row.GetCell(firstTrackNumColumn) != null && row.GetCell(firstTrackNumColumn).ToString().Trim().Length != 0 && regexOnlyNumAndAlphabeta.IsMatch(row.GetCell(firstTrackNumColumn).ToString().Trim()))
                                            {
                                                row.GetCell(firstTrackNumColumn).SetCellValue("");
                                                row.GetCell(firstTrackNumColumn).CellStyle = trackNumStyle;
                                            }

                                            if (row.GetCell(secondTrackNumColumn) != null && row.GetCell(secondTrackNumColumn).ToString().Trim().Length != 0 && regexOnlyNumAndAlphabeta.IsMatch(row.GetCell(secondTrackNumColumn).ToString().Trim()))
                                            {
                                                row.GetCell(secondTrackNumColumn).SetCellValue("");
                                                row.GetCell(secondTrackNumColumn).CellStyle = trackNumStyle;
                                            }
                                            /*
                                            if(row.GetCell(0).ToString().Split('-').Length > 1)
                                            {
                                                row.GetCell(0).SetCellValue(row.GetCell(0).ToString().Split('-')[1]);
                                            }
                                            */
                                            hasFoundThisRow = true;
                                        }
                                        bool hasGotIt = false;
                                        if (!isYesterday || !EMUGarage_hasYesterdayText)
                                        {
                                            row.GetCell(j).CellStyle = normalNumberStyle;
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
                                                        isInUpRow = true;
                                                        if (row.GetCell(j + 1) == null)
                                                        {
                                                            row.CreateCell(j + 1);
                                                        }
                                                    }
                                                    if (model.streamStatus == 0 || model.streamStatus == 4)
                                                    {
                                                    row.GetCell(j).CellStyle = stoppedTrainStyle;
                                                    row.GetCell(j + 1).SetCellValue("停开");
                                                    }
                                                    else
                                                    {
                                                        row.GetCell(j + 1).SetCellValue(model.trainId);
                                                    }
                                                    row.GetCell(j + 1).CellStyle = normalStyle;
                                                    //填写计划中的股道
                                                    string targetTrackNum = "";
                                                    string projectNum = "";
                                                    if (allTrainProjectModels != null && allTrainProjectModels.Count > 0)
                                                    {
                                                        int _tpmCount = 0;
                                                        foreach (TrainProjectModel _tpm in allTrainProjectModels)
                                                        {
                                                            if (_tpm.getInside_trainNum != null && _tpm.getInside_trainNum.Length != 0)
                                                            {
                                                                if (row.GetCell(j).ToString().Trim().Equals(_tpm.getInside_trainNum))
                                                                {//是该项目的入库车
                                                                    //判断还有没有另外的符合条件的
                                                                    bool hasSameOne = false;
                                                                    int _sameTPM;
                                                                    for (_sameTPM = _tpmCount+1; _sameTPM < allTrainProjectModels.Count; _sameTPM++) {
                                                                        if(allTrainProjectModels[_sameTPM].getInside_trainNum != null && allTrainProjectModels[_sameTPM].getInside_trainNum.Length != 0)
                                                                        {
                                                                            if (row.GetCell(j).ToString().Trim().Equals(allTrainProjectModels[_sameTPM].getInside_trainNum))
                                                                            {//判断用哪个
                                                                                hasSameOne = project_day_analyse(_tpm.getOutside_day, allTrainProjectModels[_sameTPM].getOutside_day);
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                    if(!hasSameOne)
                                                                    {
                                                                        projectNum = _tpm.projectIndex;
                                                                        if (_tpm.trainProjectWorkingModel.Count > 0)
                                                                        {//入库股道
                                                                            targetTrackNum = findGetInOrOutsideTPW(_tpm, _tpm.getInside_time, false, morningOrNight).track;
                                                                            //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                        }
                                                                    }
                                                                    else
                                                                    {//用新计划
                                                                        projectNum = allTrainProjectModels[_sameTPM].projectIndex;
                                                                        if (allTrainProjectModels[_sameTPM].trainProjectWorkingModel.Count > 0)
                                                                        {//入库股道
                                                                            targetTrackNum = findGetInOrOutsideTPW(allTrainProjectModels[_sameTPM], allTrainProjectModels[_sameTPM].getInside_time, false, morningOrNight).track;
                                                                            //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                        }
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                            if (_tpm.getOutside_trainNum != null && _tpm.getOutside_trainNum.Length != 0)
                                                            {
                                                                if (row.GetCell(j).ToString().Trim().Equals(_tpm.getOutside_trainNum))
                                                                {//是该项目的出库车
                                                                 //判断还有没有另外的符合条件的
                                                                    bool hasSameOne = false;
                                                                     int _sameTPM;
                                                                    for (_sameTPM = _tpmCount + 1; _sameTPM < allTrainProjectModels.Count; _sameTPM++)
                                                                    {
                                                                        if (allTrainProjectModels[_sameTPM].getOutside_trainNum != null && allTrainProjectModels[_sameTPM].getOutside_trainNum.Length != 0)
                                                                        {
                                                                            if (row.GetCell(j).ToString().Trim().Equals(allTrainProjectModels[_sameTPM].getOutside_trainNum))
                                                                            {//判断用哪个
                                                                                hasSameOne = project_day_analyse(_tpm.getOutside_day, allTrainProjectModels[_sameTPM].getOutside_day);
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                    if (!hasSameOne)
                                                                    {
                                                                        projectNum = _tpm.projectIndex;
                                                                        if (_tpm.trainProjectWorkingModel.Count > 0)
                                                                        {//出库股道
                                                                            targetTrackNum = findGetInOrOutsideTPW(_tpm, _tpm.getOutside_time, true, morningOrNight).track;
                                                                            //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                        }
                                                                    }
                                                                    else
                                                                    {//用新计划
                                                                        projectNum = allTrainProjectModels[_sameTPM].projectIndex;
                                                                        if (allTrainProjectModels[_sameTPM].trainProjectWorkingModel.Count > 0)
                                                                        {//出库股道
                                                                            targetTrackNum = findGetInOrOutsideTPW(allTrainProjectModels[_sameTPM], allTrainProjectModels[_sameTPM].getOutside_time, true, morningOrNight).track;
                                                                            //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                        }
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                            _tpmCount++;
                                                        }
                                                        if (targetTrackNum.Length == 0)
                                                        {
                                                            foreach (TrainProjectModel _tpm in allTrainProjectModels)
                                                            {
                                                                if (_tpm.getInside_trainNum != null && _tpm.getInside_trainNum.Length != 0)
                                                                {
                                                                    if (model.trainNumber.Trim().Equals(_tpm.getInside_trainNum))
                                                                    {//是该项目的入库车
                                                                     //判断还有没有另外的符合条件的
                                                                        bool hasSameOne = false;
                                                                         int _sameTPM;
                                                                        for (_sameTPM = _tpmCount + 1; _sameTPM < allTrainProjectModels.Count; _sameTPM++)
                                                                        {
                                                                            if (allTrainProjectModels[_sameTPM].getInside_trainNum != null && allTrainProjectModels[_sameTPM].getInside_trainNum.Length != 0)
                                                                            {
                                                                                if (row.GetCell(j).ToString().Trim().Equals(allTrainProjectModels[_sameTPM].getInside_trainNum))
                                                                                {//判断用哪个
                                                                                    hasSameOne = project_day_analyse(_tpm.getOutside_day, allTrainProjectModels[_sameTPM].getOutside_day);
                                                                                    break;
                                                                                }
                                                                            }
                                                                        }
                                                                        if (!hasSameOne)
                                                                        {
                                                                            projectNum = _tpm.projectIndex;
                                                                            if (_tpm.trainProjectWorkingModel.Count > 0)
                                                                            {//入库股道
                                                                                targetTrackNum = findGetInOrOutsideTPW(_tpm, _tpm.getInside_time, false, morningOrNight).track;
                                                                                //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                            }
                                                                        }
                                                                        else
                                                                        {//用新计划
                                                                            projectNum = allTrainProjectModels[_sameTPM].projectIndex;
                                                                            if (allTrainProjectModels[_sameTPM].trainProjectWorkingModel.Count > 0)
                                                                            {//入库股道
                                                                                targetTrackNum = findGetInOrOutsideTPW(allTrainProjectModels[_sameTPM], allTrainProjectModels[_sameTPM].getInside_time, false, morningOrNight).track;
                                                                                //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                            }
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                                if (_tpm.getOutside_trainNum != null && _tpm.getOutside_trainNum.Length != 0)
                                                                {
                                                                    if (model.trainNumber.Trim().Equals(_tpm.getOutside_trainNum))
                                                                    {//是该项目的出库车
                                                                     //判断还有没有另外的符合条件的
                                                                        bool hasSameOne = false;
                                                                         int _sameTPM;
                                                                        for (_sameTPM = _tpmCount + 1; _sameTPM < allTrainProjectModels.Count; _sameTPM++)
                                                                        {
                                                                            if (allTrainProjectModels[_sameTPM].getOutside_trainNum != null && allTrainProjectModels[_sameTPM].getOutside_trainNum.Length != 0)
                                                                            {
                                                                                if (row.GetCell(j).ToString().Trim().Equals(allTrainProjectModels[_sameTPM].getOutside_trainNum))
                                                                                {//判断用哪个
                                                                                    hasSameOne = project_day_analyse(_tpm.getOutside_day, allTrainProjectModels[_sameTPM].getOutside_day);
                                                                            break;
                                                                                }
                                                                            }
                                                                        }
                                                                        if (!hasSameOne)
                                                                        {
                                                                            projectNum = _tpm.projectIndex;
                                                                            if (_tpm.trainProjectWorkingModel.Count > 0)
                                                                            {//出库股道
                                                                                targetTrackNum = findGetInOrOutsideTPW(_tpm, _tpm.getOutside_time, true, morningOrNight).track;
                                                                                //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                            }
                                                                        }
                                                                        else
                                                                        {//用新计划
                                                                            projectNum = allTrainProjectModels[_sameTPM].projectIndex;
                                                                            if (allTrainProjectModels[_sameTPM].trainProjectWorkingModel.Count > 0)
                                                                            {//出库股道
                                                                                targetTrackNum = findGetInOrOutsideTPW(allTrainProjectModels[_sameTPM], allTrainProjectModels[_sameTPM].getOutside_time, true, morningOrNight).track;
                                                                                //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                            }
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (targetTrackNum.Length != 0)
                                                    {
                                                        //计算用
                                                        int trackNumInt = 0;
                                                        int.TryParse(targetTrackNum.Split('G')[0], out trackNumInt);
                                                        if (secondSection == 1 && (trackNumInt < 44 && trackNumInt != 0)
                                                            || targetTrackNum.Trim().Equals("JC1G") 
                                                            || targetTrackNum.Trim().Equals("JC2G")
                                                            || targetTrackNum.Trim().Equals("JC3G")
                                                            || targetTrackNum.Trim().Equals("JC4G")
                                                            || targetTrackNum.Trim().Equals("JC5G")
                                                            || targetTrackNum.Trim().Equals("JC6G"))
                                                        {//二场并且<44道
                                                            //row.GetCell(j).CellStyle = stoppedTrainStyle;
                                                        }


                                                        if (j < firstTrackNumColumn)
                                                        {
                                                            if (row.GetCell(firstTrackNumColumn) == null)
                                                            {
                                                                row.CreateCell(firstTrackNumColumn);
                                                            }
                                                            row.GetCell(firstTrackNumColumn).SetCellValue(targetTrackNum);
                                                            row.GetCell(firstTrackNumColumn).CellStyle = trackNumStyle;
                                                            if (projectNum.Length != 0)
                                                            {
                                                                if (row.GetCell(first_extraColumn) == null)
                                                                {
                                                                    row.CreateCell(first_extraColumn);
                                                                }
                                                                row.GetCell(first_extraColumn).SetCellValue(row.GetCell(first_extraColumn).ToString().Split('-')[0].ToString().Trim() + "-" + projectNum + "钩");
                                                            }
                                                        }
                                                        else if (j < secondTrackNumColumn)
                                                        {
                                                            if (row.GetCell(secondTrackNumColumn) == null)
                                                            {
                                                                row.CreateCell(secondTrackNumColumn);
                                                            }
                                                            row.GetCell(secondTrackNumColumn).SetCellValue(targetTrackNum);
                                                            row.GetCell(secondTrackNumColumn).CellStyle = trackNumStyle;
                                                            if (projectNum.Length != 0)
                                                            {
                                                                if (row.GetCell(second_extraColumn) == null)
                                                                {
                                                                    row.CreateCell(second_extraColumn);
                                                                }
                                                                row.GetCell(second_extraColumn).SetCellValue(row.GetCell(second_extraColumn).ToString().Split('-')[0].ToString().Trim() + "-" + projectNum + "钩");
                                                            }
                                                            if (trackNumInt != 0)
                                                            {
                                                                foreach (EMUGarageTableModel _emugtm in allEMUGarageTableModels)
                                                                {
                                                                    if (model.trainNumber.Trim().Equals(_emugtm.trainNumber))
                                                                    {
                                                                        if (_emugtm.isGettingInGarage == 0)
                                                                        {//必须是出库车
                                                                            if ((_emugtm.trackLine == 1 && trackNumInt > 13) ||
                                                                                (_emugtm.trackLine == 2 && (trackNumInt <= 13 || trackNumInt > 25)) ||
                                                                                (_emugtm.trackLine == 3 && (trackNumInt <= 25 || trackNumInt > 33)) ||
                                                                                (_emugtm.trackLine == 4 && trackNumInt <= 33))
                                                                            {//动存线-走行线匹配判断
                                                                                //row.GetCell(secondTrackNumColumn).CellStyle = notRecommandedTrackNumStyle;
                                                                            }
                                                                            if((_emugtm.trackLine ==1 || _emugtm.trackLine == 2) && trackNumInt >= 58)
                                                                            {//58G及以上不能向动一动二开车
                                                                            //row.GetCell(secondTrackNumColumn).CellStyle = stoppedTrainStyle;
                                                                        }
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if (!hasGotIt)
                                            {
                                                if (commandText.Contains(row.GetCell(j).ToString().Trim()))
                                                {
                                                    failedLoadingTrain = failedLoadingTrain + " " + row.GetCell(j).ToString().Trim();
                                                    //searchAndHightlightUnresolvedTrains(row.GetCell(j).ToString().Trim(), 0, 2);
                                                }
                                            }
                                        }
                                        else
                                        {//昨天的
                                            row.GetCell(j).CellStyle = yesterdayNumberStyle;
                                            foreach (CommandModel model in yesterdayCommandModel)
                                            {
                                                if (row.GetCell(j).ToString().Trim().Equals(model.trainNumber) ||
                                                    row.GetCell(j).ToString().Trim().Equals(model.secondTrainNumber))
                                                {
                                                    hasGotIt = true;
                                                    if ((row.GetCell(j + 2) == null && row.GetCell(j + 3) == null) ||
                                                    (row.GetCell(j + 2).ToString().Length == 0 && row.GetCell(j + 3).ToString().Length == 0))
                                                    {
                                                        //说明是和别人共用一格 但是在下面（目标单元格被挡住了）所以往上挪一行填
                                                        isInUpRow = true;
                                                        row = sheet.GetRow(i - 1);
                                                        if (row.GetCell(j + 1) == null)
                                                        {
                                                            row.CreateCell(j + 1);
                                                        }
                                                    }
                                                    if (model.streamStatus == 0 || model.streamStatus == 4)
                                                    {
                                                    row.GetCell(j).CellStyle = stoppedTrainStyle;
                                                    row.GetCell(j + 1).SetCellValue("停开");
                                                    }
                                                    else
                                                    {
                                                        row.GetCell(j + 1).SetCellValue(model.trainId);
                                                    }
                                                    row.GetCell(j + 1).CellStyle = normalStyle;
                                                    string targetTrackNum = "";
                                                    string projectNum = "";
                                                    if (allTrainProjectModels != null && allTrainProjectModels.Count > 0)
                                                    {
                                                        int _tpmCount = 0;
                                                        foreach (TrainProjectModel _tpm in allTrainProjectModels)
                                                        {
                                                            if (_tpm.getInside_trainNum != null && _tpm.getInside_trainNum.Length != 0)
                                                            {
                                                                if (row.GetCell(j).ToString().Trim().Equals(_tpm.getInside_trainNum))
                                                                {//是该项目的入库车
                                                                    //判断还有没有另外的符合条件的
                                                                    bool hasSameOne = false;
                                                                    int _sameTPM;
                                                                    for (_sameTPM = _tpmCount + 1; _sameTPM < allTrainProjectModels.Count; _sameTPM++)
                                                                    {
                                                                        if (allTrainProjectModels[_sameTPM].getInside_trainNum != null && allTrainProjectModels[_sameTPM].getInside_trainNum.Length != 0)
                                                                        {
                                                                            if (row.GetCell(j).ToString().Trim().Equals(allTrainProjectModels[_sameTPM].getInside_trainNum))
                                                                            {
                                                                                hasSameOne =project_day_analyse(_tpm.getOutside_day, allTrainProjectModels[_sameTPM].getOutside_day);
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                    if (!hasSameOne)
                                                                    {
                                                                        projectNum = _tpm.projectIndex;
                                                                        if (_tpm.trainProjectWorkingModel.Count > 0)
                                                                        {//入库股道
                                                                            targetTrackNum = findGetInOrOutsideTPW(_tpm, _tpm.getInside_time, false, morningOrNight).track;
                                                                            //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                        }
                                                                    }
                                                            else if (_sameTPM < allTrainProjectModels.Count && _sameTPM >= 0)
                                                            {//用新计划
                                                                        projectNum = allTrainProjectModels[_sameTPM].projectIndex;
                                                                        if (allTrainProjectModels[_sameTPM].trainProjectWorkingModel.Count > 0)
                                                                        {//入库股道
                                                                            targetTrackNum = findGetInOrOutsideTPW(allTrainProjectModels[_sameTPM], allTrainProjectModels[_sameTPM].getInside_time, false, morningOrNight).track;
                                                                            //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                        }
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                            if (_tpm.getOutside_trainNum != null && _tpm.getOutside_trainNum.Length != 0)
                                                            {
                                                                if (row.GetCell(j).ToString().Trim().Equals(_tpm.getOutside_trainNum))
                                                                {//是该项目的出库车
                                                                 //判断还有没有另外的符合条件的
                                                                    bool hasSameOne = false;
                                                                    int _sameTPM;
                                                                    for (_sameTPM = _tpmCount + 1; _sameTPM < allTrainProjectModels.Count; _sameTPM++)
                                                                    {
                                                                        if (allTrainProjectModels[_sameTPM].getOutside_trainNum != null && allTrainProjectModels[_sameTPM].getOutside_trainNum.Length != 0)
                                                                        {
                                                                            if (row.GetCell(j).ToString().Trim().Equals(allTrainProjectModels[_sameTPM].getOutside_trainNum))
                                                                            {
                                                                                hasSameOne = project_day_analyse(_tpm.getOutside_day, allTrainProjectModels[_sameTPM].getOutside_day);
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                    if (!hasSameOne)
                                                                    {
                                                                        projectNum = _tpm.projectIndex;
                                                                        if (_tpm.trainProjectWorkingModel.Count > 0)
                                                                        {//出库股道
                                                                            targetTrackNum = findGetInOrOutsideTPW(_tpm, _tpm.getOutside_time, true, morningOrNight).track;
                                                                            //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                        }
                                                                    }
                                                            else if (_sameTPM < allTrainProjectModels.Count && _sameTPM >= 0)
                                                            {//用新计划
                                                                        projectNum = allTrainProjectModels[_sameTPM].projectIndex;
                                                                        if (allTrainProjectModels[_sameTPM].trainProjectWorkingModel.Count > 0)
                                                                        {//出库股道
                                                                            targetTrackNum = findGetInOrOutsideTPW(allTrainProjectModels[_sameTPM], allTrainProjectModels[_sameTPM].getOutside_time, true, morningOrNight).track;
                                                                            //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                        }
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                            _tpmCount++;
                                                        }
                                                        if (targetTrackNum.Length == 0)
                                                        {
                                                            foreach (TrainProjectModel _tpm in allTrainProjectModels)
                                                            {
                                                                if (_tpm.getInside_trainNum != null && _tpm.getInside_trainNum.Length != 0)
                                                                {
                                                                    if (model.trainNumber.Trim().Equals(_tpm.getInside_trainNum))
                                                                    {//是该项目的入库车
                                                                     //判断还有没有另外的符合条件的
                                                                        bool hasSameOne = false;
                                                                        int _sameTPM;
                                                                        for (_sameTPM = _tpmCount + 1; _sameTPM < allTrainProjectModels.Count; _sameTPM++)
                                                                        {
                                                                            if (allTrainProjectModels[_sameTPM].getInside_trainNum != null && allTrainProjectModels[_sameTPM].getInside_trainNum.Length != 0)
                                                                            {
                                                                                if (row.GetCell(j).ToString().Trim().Equals(allTrainProjectModels[_sameTPM].getInside_trainNum))
                                                                                {
                                                                                    hasSameOne = project_day_analyse(_tpm.getOutside_day, allTrainProjectModels[_sameTPM].getOutside_day);
                                                                                }
                                                                            }
                                                                        }
                                                                        if (!hasSameOne)
                                                                        {
                                                                            projectNum = _tpm.projectIndex;
                                                                            if (_tpm.trainProjectWorkingModel.Count > 0)
                                                                            {//入库股道
                                                                                targetTrackNum = findGetInOrOutsideTPW(_tpm, _tpm.getInside_time, false, morningOrNight).track;
                                                                                //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                            }
                                                                        }
                                                                        else if(_sameTPM < allTrainProjectModels.Count && _sameTPM >= 0)
                                                                        {//用新计划
                                                                            projectNum = allTrainProjectModels[_sameTPM].projectIndex;
                                                                            if (allTrainProjectModels[_sameTPM].trainProjectWorkingModel.Count > 0)
                                                                            {//入库股道
                                                                                targetTrackNum = findGetInOrOutsideTPW(allTrainProjectModels[_sameTPM], allTrainProjectModels[_sameTPM].getInside_time, false, morningOrNight).track;
                                                                                //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                            }
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                                if (_tpm.getOutside_trainNum != null && _tpm.getOutside_trainNum.Length != 0)
                                                                {
                                                                    if (model.trainNumber.Trim().Equals(_tpm.getOutside_trainNum))
                                                                    {//是该项目的出库车
                                                                     //判断还有没有另外的符合条件的
                                                                        bool hasSameOne = false;
                                                                        int _sameTPM;
                                                                        for (_sameTPM = _tpmCount + 1; _sameTPM < allTrainProjectModels.Count; _sameTPM++)
                                                                        {
                                                                            if (allTrainProjectModels[_sameTPM].getOutside_trainNum != null && allTrainProjectModels[_sameTPM].getOutside_trainNum.Length != 0)
                                                                            {
                                                                                if (row.GetCell(j).ToString().Trim().Equals(allTrainProjectModels[_sameTPM].getOutside_trainNum))
                                                                                {//判断用哪个
                                                                                    hasSameOne = project_day_analyse(_tpm.getOutside_day, allTrainProjectModels[_sameTPM].getOutside_day);
                                                                                }
                                                                            }
                                                                        }
                                                                        if (!hasSameOne)
                                                                        {
                                                                            projectNum = _tpm.projectIndex;
                                                                            if (_tpm.trainProjectWorkingModel.Count > 0)
                                                                            {//出库股道
                                                                                targetTrackNum = findGetInOrOutsideTPW(_tpm, _tpm.getOutside_time, true, morningOrNight).track;
                                                                                //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                            }
                                                                        }
                                                                else if (_sameTPM < allTrainProjectModels.Count && _sameTPM >= 0)
                                                                {//用新计划
                                                                            projectNum = allTrainProjectModels[_sameTPM].projectIndex;
                                                                            if (allTrainProjectModels[_sameTPM].trainProjectWorkingModel.Count > 0)
                                                                            {//出库股道
                                                                                targetTrackNum = findGetInOrOutsideTPW(allTrainProjectModels[_sameTPM], allTrainProjectModels[_sameTPM].getOutside_time, true, morningOrNight).track;
                                                                                //targetTrackNum = _tpm.trainProjectWorkingModel[0].track;
                                                                            }
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (targetTrackNum.Length != 0)
                                                    {
                                                        //计算用
                                                        int trackNumInt = 0;
                                                        int.TryParse(targetTrackNum.Split('G')[0], out trackNumInt);
                                                        if (secondSection == 1 && (trackNumInt < 44 && trackNumInt != 0)
                                                            || targetTrackNum.Trim().Equals("JC1G")
                                                            || targetTrackNum.Trim().Equals("JC2G")
                                                            || targetTrackNum.Trim().Equals("JC3G")
                                                            || targetTrackNum.Trim().Equals("JC4G")
                                                            || targetTrackNum.Trim().Equals("JC5G")
                                                            || targetTrackNum.Trim().Equals("JC6G"))
                                                        {//二场并且<44道
                                                            //row.GetCell(j).CellStyle = stoppedTrainStyle;
                                                        }
                                                        if (j < firstTrackNumColumn)
                                                        {
                                                            if (row.GetCell(firstTrackNumColumn) == null)
                                                            {
                                                                row.CreateCell(firstTrackNumColumn);
                                                            }
                                                            row.GetCell(firstTrackNumColumn).SetCellValue(targetTrackNum);
                                                            row.GetCell(firstTrackNumColumn).CellStyle = trackNumStyle;
                                                            if (projectNum.Length != 0)
                                                            {
                                                                if (row.GetCell(first_extraColumn) == null)
                                                                {
                                                                    row.CreateCell(first_extraColumn);
                                                                }
                                                                row.GetCell(first_extraColumn).SetCellValue(row.GetCell(first_extraColumn).ToString().Split('-')[0].ToString().Trim() + "-" + projectNum + "钩");
                                                            }
                                                        }
                                                        else if (j < secondTrackNumColumn)
                                                        {
                                                            if (row.GetCell(secondTrackNumColumn) == null)
                                                            {
                                                                row.CreateCell(secondTrackNumColumn);
                                                            }
                                                            row.GetCell(secondTrackNumColumn).SetCellValue(targetTrackNum);
                                                            row.GetCell(secondTrackNumColumn).CellStyle = trackNumStyle;
                                                            if (projectNum.Length != 0)
                                                            {
                                                                if (row.GetCell(second_extraColumn) == null)
                                                                {
                                                                    row.CreateCell(second_extraColumn);
                                                                }
                                                                row.GetCell(second_extraColumn).SetCellValue(row.GetCell(second_extraColumn).ToString().Split('-')[0].ToString().Trim() + "-" + projectNum + "钩");
                                                            }
                                                            if (trackNumInt != 0)
                                                            {
                                                                foreach (EMUGarageTableModel _emugtm in allEMUGarageTableModels)
                                                                {
                                                                    if (model.trainNumber.Trim().Equals(_emugtm.trainNumber))
                                                                    {
                                                                        if (_emugtm.isGettingInGarage == 0)
                                                                        {//必须是出库车
                                                                            if ((_emugtm.trackLine == 1 && trackNumInt > 13) ||
                                                                                (_emugtm.trackLine == 2 && (trackNumInt <= 13 || trackNumInt > 25)) ||
                                                                                (_emugtm.trackLine == 3 && (trackNumInt <= 25 || trackNumInt > 33)) ||
                                                                                (_emugtm.trackLine == 4 && trackNumInt <= 33))
                                                                            {//动存线-走行线匹配判断
                                                                                //row.GetCell(secondTrackNumColumn).CellStyle = notRecommandedTrackNumStyle;
                                                                            }
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            if (!hasGotIt)
                                            {
                                                if (yesterdayCommandText.Contains(row.GetCell(j).ToString().Trim()) && row.GetCell(j).ToString().Trim().Length != 0)
                                                {
                                                    failedLoadingTrain = failedLoadingTrain + " (→昨天的)" + row.GetCell(j).ToString().Trim();
                                                    //searchAndHightlightUnresolvedTrains(row.GetCell(j).ToString().Trim(), 2, 2);
                                                }
                                            }
                                        }
                                    }
                                }
                                if (isInUpRow == true)
                                {
                                    row = sheet.GetRow(i);
                                }
                            }
                        }
                    }
                    //动车所停开标注为停开
                    for(int check = 0; check <= sheet.LastRowNum; check++)
                {
                    IRow checkRow = sheet.GetRow(check);
                    if(checkRow == null)
                    {
                        continue;
                    }
                    ICell firstTrainNumCell = checkRow.GetCell(firstTrainIDColumn - 1);
                    ICell firstTrainIDCell = checkRow.GetCell(firstTrainIDColumn);
                    if(firstTrainNumCell == null)
                    {

                    }
                    else if(firstTrainNumCell.ToString().Trim().Length != 0)
                    {
                        //停开车标停开
                        if(firstTrainIDCell == null || firstTrainIDCell.ToString().Trim().Length == 0 || firstTrainIDCell.ToString().Trim().Contains("停"))
                        {
                            firstTrainNumCell.CellStyle = stoppedTrainStyle;
                        }
                    }
                    ICell secondTrainNumCell = checkRow.GetCell(secondTrainIDColumn - 1);
                    ICell secondTrainIDCell = checkRow.GetCell(secondTrainIDColumn);
                    if(secondTrainNumCell == null)
                    {

                    }
                    else if (secondTrainNumCell.ToString().Trim().Length != 0)
                    {
                        //停开车标停开
                        if (secondTrainIDCell == null || secondTrainIDCell.ToString().Trim().Length == 0 || secondTrainIDCell.ToString().Trim().Contains("停"))
                        {
                            secondTrainNumCell.CellStyle = stoppedTrainStyle;
                        }
                    }

                }
                    if (failedLoadingTrain.Length != 0)
                    {
                        MessageBox.Show("请人工检查以下车次是否未填写（在昨天的客调令中已标红）：\n" + failedLoadingTrain, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (isProjectHelper)
                    {//不需要打开文件
                        allEMUGarageTableModels = _eMUGarageTableModels;
                        return;
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

    catch(Exception autoCompleteE)
    {
        MessageBox.Show("运行出现错误，请重试，若持续错误请联系车间。\n" + autoCompleteE.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

        }

        //判断动车段时刻表应该用哪趟车
        private bool project_day_analyse(string str_originalDay, string str_sameDay)
        {
            //有两天的车都在计划里，这时候应该…用前一天的车
            int originalDay = -1;
            int sameDay = -1;
            int.TryParse(str_originalDay, out originalDay);
            int.TryParse(str_sameDay, out sameDay);
            if (originalDay == -1 || sameDay == -1)
            {//此时还是用原来的计划内容
                return false;
            }
            if (originalDay == (sameDay - 1))
            {
                return false;
            }
            else if (originalDay == (sameDay + 1))
            {//此时应该用新计划
                return true;
            }
            else if (originalDay - sameDay > 1)
            {//30 1号切换的时候,用原计划
                return false;
            }
            else if (sameDay - originalDay > 1)
            {//用新计划
                return true;
            }
            return false;
        }

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
                    if (model.streamStatus != 0)
                    {
                        streamStatus = "√开";
                    }
                    else
                    {
                        streamStatus = "×停";
                    }
                    switch (model.trainType)
                    {
                        case 0:
                            trainType = "";
                            break;
                        case 1:
                            trainType = "-高峰";
                            break;
                        case 2:
                            trainType = "-临客";
                            break;
                        case 3:
                            trainType = "-周末";
                            break;
                    }
                    if (model.secondTrainNumber.Equals("null"))
                    {
                        commands = commands + "第" + model.trainIndex + "条-" + model.trainNumber + "-"+model.trainModel+"-"+model.trainId+"-" + streamStatus +  trainType + "\r\n";
                    }
                    else
                    {
                        commands = commands + "第" + model.trainIndex + "条-" + model.trainNumber + "-" + model.secondTrainNumber + "-" + model.trainModel + "-" + model.trainId + "-" + streamStatus +  trainType + "\r\n";
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

        //
        //
        //
        //
        //此处开始是调车作业计划辅助程序
        //
        //---------------------------------------------------------
        //
        //此处开始是调车作业计划辅助程序
        //
        //
        //
        //

        private void importTrainProjectFile_btn_Click(object sender, EventArgs e)
        {
            try
            {
                EMUGarageFile = new List<string>();
                OpenFileDialog openFileDialog1 = new OpenFileDialog();   //显示选择文件对话框 
                openFileDialog1.Filter = "Word 文件 |*.docx;*.doc";
                openFileDialog1.InitialDirectory = Application.StartupPath + "\\动车所-调车作业计划\\";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    String fileNames = "已选择：";
                    trainProjectFile = openFileDialog1.FileName;
                    this.trainPorjectFilePath_lbl.Text = "已选择：" + trainProjectFile;     //显示文件路径
                                                                                        //判断是doc还是docx
                    FileFormat fileFormat;
                    if (trainProjectFile.Split('.')[trainProjectFile.Split('.').Length - 1].Equals("doc"))
                    {
                        fileFormat = FileFormat.Doc;
                    }
                    else if (trainProjectFile.Split('.')[trainProjectFile.Split('.').Length - 1].Equals("docx"))
                    {
                        fileFormat = FileFormat.Docx;
                    }
                    else
                    {
                        fileFormat = FileFormat.Docx;
                    }
                    Document doc = new Document();
                    doc.LoadFromFile(trainProjectFile, fileFormat);

                    if (doc.GetText().Contains("夜班"))
                    {
                        morningOrNight = 1;
                    }
                    else if (doc.GetText().Contains("白班"))
                    {
                        morningOrNight = 0;
                    }
                    //把钩数据加以处理
                    //获取文本框中第一个表格
                    if (doc.Sections[0] == null)
                    {
                        MessageBox.Show("选中的计划文件中未读取到内容。若为计划文件格式变更，请将相关文件及情况报告技术科。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //实例化StringBuilder类
                    StringBuilder sb = new StringBuilder();
                    if (doc.Sections[0].Tables[0] == null)
                    {
                        MessageBox.Show("选中的计划文件中不含表格，若为计划文件格式变更，请将相关文件及情况报告技术科。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        Table table = doc.Sections[0].Tables[0] as Table;
                        //获取到第一个之后再开始记录
                        bool hasGotFirstOne = false;
                        //遍历表格中的段落并提取文本
                        foreach (TableRow row in table.Rows)
                        {
                            foreach (TableCell cell in row.Cells)
                            {
                                foreach (Paragraph paragraph in cell.Paragraphs)
                                {
                                    if (Regex.IsMatch(paragraph.Text.ToString().Trim(), @"^\d+$"))
                                    {//在某一个表格里仅有数字，则为钩序
                                        sb.AppendLine("|" + paragraph.Text + "、");
                                        //获取到第一个了 开始记录
                                        hasGotFirstOne = true;
                                    }
                                    else if (hasGotFirstOne)
                                    {
                                        //将不是末尾的句号都改成逗号
                                        string currentText = paragraph.Text;
                                        currentText = currentText.Replace("。", "，").Replace(",", "，").Replace("（", "(").Replace("）", ")").TrimEnd('，');
                                        sb.AppendLine(currentText);
                                    }
                                }
                            }
                        }
                    }
                    trainProjectText = sb.ToString();
                    analyseTrainProjects();
                    if(allTrainProjectModels != null && allTrainProjectModels.Count != 0)
                    {
                        emptyTrackList_rtb.Text = checkEmptyTrack(allTrainProjectModels);
                        matchTrackWithTrain_Project_btn.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("未检测到计划，请确认是否选择了正确的计划文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    /*
                    File.WriteAllText("CommentExtraction.txt", sb.ToString());
                    System.Diagnostics.Process.Start("CommentExtraction.txt");
                    */
                }
            }
            catch(IOException eIO)
            {
                MessageBox.Show("选中的计划文件正在使用中，请关闭后再试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        
        }

        private void analyseTrainProjects()
        {//分析调车作业计划
            //以添加的分隔符（“|第x”“钩、”）为分界，包含逗号的为同一辆车
            try
            {
                allTrainProjectModels = new List<TrainProjectModel>();
                List<TrainProjectModel> _trainProjectModels = new List<TrainProjectModel>();
                string[] trainProjectList = trainProjectText.Split('|');
                for (int i = 0; i < trainProjectList.Length; i++)
                {//对于每一条
                    if (trainProjectList[i] != null)
                    {
                        TrainProjectModel _pm = new TrainProjectModel();
                        //解编后的
                        TrainProjectModel _pmBreak = new TrainProjectModel();
                        //是否启用解编后的对象
                        bool hasBroken = false;
                        //重复车应该同一条只用找一次
                        bool hasFoundOthers = false;
                        //单组车已经添加进重联车组 不需要继续寻找后续内容时
                        bool skipToNext = false;
                        //用于记录已找到相同车号的钩号
                        string sameTrainIndex = "";
                        string currentTrain = trainProjectList[i];
                        if (currentTrain.Length == 0)
                        {
                            continue;
                        }
                        //钩号
                        int index = -1;
                        int.TryParse(currentTrain.Split('、')[0].TrimStart('|'), out index);
                        if (index == -1)
                        {
                            continue;
                        }
                        else
                        {
                            _pm.projectIndex = index.ToString();
                        }
                        string[] workFlows;
                        if (currentTrain.Split('、').Length > 1)
                        {
                            string _cTrain = "";
                            for (int ij = 1; ij < currentTrain.Split('、').Length; ij++)
                            {
                                _cTrain = _cTrain + currentTrain.Split('、')[ij];
                            }
                            workFlows = _cTrain.Split('，');
                        }
                        else
                        {
                            continue;
                        }
                        if (workFlows.Length == 0)
                        {
                            continue;
                        }
                        //工作流
                        for (int j = 0; j < workFlows.Length; j++)
                        {
                            string _currentWork = workFlows[j];
                            //入库车都在第一部分，先找入库车
                            if (j == 0)
                            {
                                if ((_currentWork.Contains("0G") ||
                                    _currentWork.Contains("DJ") ||
                                    _currentWork.Contains("0J") ||
                                    _currentWork.Contains("0C") ||
                                    _currentWork.Contains("0D") ||
                                    _currentWork.Contains("00")) &&
                                    _currentWork.Contains("次"))
                                {
                                    _pm.getInside_trainNum = _currentWork.Split('次')[0].TrimStart('\r').TrimStart('\n');
                                }
                                //入库时间
                                Regex regInTime;
                                Match mInTime;
                                //如果找到了入库时间 则钩开始时间为入库时间
                                if (Regex.IsMatch(_currentWork.Trim(), @"[0-9]{2}(:)[0-9]{2}"))
                                {
                                    regInTime = new Regex(@"[0-9]{2}(:)[0-9]{2}", RegexOptions.None);
                                    mInTime = regInTime.Match(_currentWork.Trim());
                                    _pm.getInside_time = mInTime.Value;
                                    _pm.startTime = mInTime.Value;
                                }
                                else if (Regex.IsMatch(_currentWork.Trim(), @"[0-9]{1}(:)[0-9]{2}"))
                                {
                                    regInTime = new Regex(@"[0-9]{1}(:)[0-9]{2}", RegexOptions.None);
                                    mInTime = regInTime.Match(_currentWork.Trim());
                                    _pm.getInside_time = mInTime.Value;
                                    _pm.startTime = mInTime.Value;
                                }
                                //车号
                                try
                                {
                                    Regex regTrain = new Regex(@"\(([^)]*)\)");
                                    //@"\(.*?\)"
                                    MatchCollection mTrain = regTrain.Matches(_currentWork);
                                    foreach (Match match in mTrain)
                                    {
                                        string value = match.Value.Trim('(', ')');
                                        if (value.Contains("CR") && value.Split('-').Length > 1)
                                        {
                                            string trainModel = value.Split('-')[0].Trim();
                                            string trainID = value.Split('-')[1].Trim();
                                            _pm.trainModel = trainModel;
                                            //20210625 新增400AF-BZ/Z车型
                                            if (trainModel.Contains("L") ||
                                                trainModel.Contains("2B") ||
                                                trainModel.Contains("AF-A") ||
                                                trainModel.Contains("AF-B") ||
                                                trainModel.Contains("BF-A") ||
                                                trainModel.Contains("BF-B") ||
                                                 trainModel.Contains("BF-BZ") ||
                                                  trainModel.Contains("AF-BZ"))
                                            {
                                                _pm.trainConnectType = 1;
                                                if (trainModel.Contains("-A") || trainModel.Contains("-B"))
                                                {
                                                    if(value.Split('-').Length == 3)
                                                    {
                                                        _pm.trainId = value.Split('-')[2].Trim();
                                                    }
                                                    else
                                                    {
                                                        _pm.trainId = value.Split('-')[1].Trim();
                                                    }

                                                }
                                                else
                                                {
                                                    _pm.trainId = value.Split('-')[1].Trim();
                                                }

                                            }
                                            else if (trainID.Contains("+"))
                                            {
                                                _pm.trainConnectType = 2;
                                                //400AF-Z-XXX+XXX
                                                if (trainID.Contains("-Z") || trainID.Contains("-z") || trainID.Contains("-GZ") || trainID.Contains("-gz"))
                                                {
                                                    if(value.Split('-').Length >= 3)
                                                    {
                                                        _pm.trainId = value.Split('-')[2].Split('+')[0].Trim();
                                                        _pm.secondTrainId = value.Split('-')[1].Split('+')[1].Trim();
                                                    }
                                                    else
                                                    {
                                                        _pm.trainId = value.Split('-')[1].Split('+')[0].Trim();
                                                        _pm.secondTrainId = value.Split('-')[1].Split('+')[1].Trim();
                                                    }
                                                }
                                                else
                                                {
                                                    _pm.trainId = value.Split('-')[1].Split('+')[0].Trim();
                                                    _pm.secondTrainId = value.Split('-')[1].Split('+')[1].Trim();
                                                }

                                            }
                                            else
                                            {
                                                //400AF-Z-XXX
                                                _pm.trainConnectType = 0;
                                                if (trainID.Contains("-Z") || trainID.Contains("-z") || trainID.Contains("-GZ") || trainID.Contains("-gz"))
                                                {
                                                    if (value.Split('-').Length >= 3)
                                                    {
                                                        _pm.trainId = value.Split('-')[2].Trim();
                                                    }
                                                    else
                                                    {
                                                        _pm.trainId = value.Split('-')[1].Trim();
                                                    }
                                                }
                                                else
                                                {
                                                    _pm.trainId = value.Split('-')[1].Trim();
                                                }

                                            }
                                        }
                                    }
                                }
                                catch (ArgumentException ex)
                                {
                                    // Syntax error in the regular expression
                                }
                            }
                            //找出库车
                            if (_currentWork.Contains("备开") &&
                                (_currentWork.Contains("0G") ||
                                _currentWork.Contains("DJ") ||
                                    _currentWork.Contains("0J") ||
                                    _currentWork.Contains("0C") ||
                                    _currentWork.Contains("0D") ||
                                    _currentWork.Contains("00")))
                            {
                                //日期
                                string _day = _currentWork.Split('备')[1].Split('开')[1].Split('日')[0];
                                //车次
                                string _trainNum = "";
                                char[] _workToChar = _currentWork.Replace("次", "").ToCharArray();
                                for (int c = _workToChar.Length - 1; c > 0; c--)
                                {//从后往前找“次”前面的字符，直到碰到第一个不是数字字母的
                                    if (Regex.IsMatch(_workToChar[c].ToString(), @"^[A-Za-z0-9]+$"))
                                    {
                                        _trainNum = _workToChar[c].ToString() + _trainNum;
                                    }
                                    else
                                    {
                                        if (Regex.IsMatch(_trainNum, @"^[A-Za-z0-9]+$"))
                                        {
                                            if (!hasBroken)
                                            {//分为解编后和解编前
                                                _pm.getOutside_trainNum = _trainNum;
                                                _pm.getOutside_day = _day;
                                            }
                                            else
                                            {
                                                _pmBreak.getOutside_trainNum = _trainNum;
                                                _pmBreak.getOutside_day = _day;
                                            }
                                            break;
                                        }
                                    }
                                }

                                //时间
                                //出库时间
                                //如果找到了出库时间，则该钩结束时间为出库时间
                                Regex regOutTime;
                                Match mOutTime;
                                if (Regex.IsMatch(_currentWork.Trim(), @"[0-9]{2}(:)[0-9]{2}"))
                                {
                                    regOutTime = new Regex(@"[0-9]{2}(:)[0-9]{2}", RegexOptions.None);
                                    mOutTime = regOutTime.Match(_currentWork.Trim());
                                    if (!hasBroken)
                                    {
                                        _pm.getOutside_time = mOutTime.Value;
                                        _pm.endTime = mOutTime.Value;
                                    }
                                    else
                                    {
                                        _pmBreak.getOutside_time = mOutTime.Value;
                                        _pmBreak.endTime = mOutTime.Value;
                                    }
                                }
                                else if (Regex.IsMatch(_currentWork.Trim(), @"[0-9]{1}(:)[0-9]{2}"))
                                {
                                    regOutTime = new Regex(@"[0-9]{1}(:)[0-9]{2}", RegexOptions.None);
                                    mOutTime = regOutTime.Match(_currentWork.Trim());
                                    if (!hasBroken)
                                    {
                                        _pm.getOutside_time = mOutTime.Value;
                                        _pm.endTime = mOutTime.Value;
                                    }
                                    else
                                    {
                                        _pmBreak.getOutside_time = mOutTime.Value;
                                        _pmBreak.endTime = mOutTime.Value;
                                    }

                                }
                            }
                            //找其他位置是否已经有该车号，若其他位置已有，则标记为重复车，读取完成后与已存在的模型进行同步
                            foreach (TrainProjectModel _compareModel in _trainProjectModels)
                            {
                                if (hasFoundOthers)
                                {
                                    break;
                                }
                                //应付顺序变化的情况
                                string currentID_A = _pm.trainId;
                                string currentID_B = "";
                                string compareID_A = _compareModel.trainId;
                                string compareID_B = "";
                                if (_pm.secondTrainId.Length != 0)
                                {
                                    currentID_B = _pm.secondTrainId;
                                }
                                if (_compareModel.secondTrainId.Length != 0)
                                {
                                    compareID_B = _compareModel.secondTrainId;
                                }
                                if (((currentID_A + "+" + currentID_B).Equals(compareID_A + "+" + compareID_B) ||
                                    (currentID_A + "+" + currentID_B).Equals(compareID_B + "+" + compareID_A)) &&
                                   !_compareModel.projectIndex.Contains("-"))
                                {
                                    //标记为重复车(重联发现了重联)
                                    _pm.trainWorkingMode = 3;
                                    _pm.sameTrain_ProjectIndex = _compareModel.projectIndex;
                                    break;
                                }
                                else if ((compareID_A + "+" + compareID_B).Contains(currentID_A)
                                    && compareID_B.Length != 0 && currentID_B.Length == 0 &&
                                   !_compareModel.projectIndex.Contains("-"))
                                {
                                    //标记为（短变长）车（短编发现了重联）
                                    _pm.trainWorkingMode = 1;
                                    _pm.sameTrain_ProjectIndex = _compareModel.projectIndex;
                                    break;
                                }
                            }
                            if (!hasFoundOthers)
                            {
                                hasFoundOthers = true;
                            }
                            //找工作流
                            if (!_currentWork.Contains("备开"))
                            {
                                TrainProjectWorking _tpw = new TrainProjectWorking();
                                string tempOriginalText = _currentWork;
                                tempOriginalText = tempOriginalText.Replace("：", ":");
                                tempOriginalText = Regex.Replace(tempOriginalText, @"[0-9]{2}(:)[0-9]{2}", "");
                                tempOriginalText = Regex.Replace(tempOriginalText, @"[0-9]{1}(:)[0-9]{2}", "");
                                if (_currentWork.Contains(")") && !_currentWork.Contains("重联") && !_currentWork.Contains("解编"))
                                {
                                    _tpw.originalText = tempOriginalText.Split(')')[1];
                                }
                                else
                                {
                                    _tpw.originalText = tempOriginalText;
                                }

                                //找时间
                                Regex regWorkTime;
                                Match mWorkTime;
                                if (Regex.IsMatch(_currentWork.Trim(), @"[0-9]{2}(:)[0-9]{2}"))
                                {
                                    regWorkTime = new Regex(@"[0-9]{2}(:)[0-9]{2}", RegexOptions.None);
                                    mWorkTime = regWorkTime.Match(_currentWork.Trim());
                                    _tpw.time = mWorkTime.Value;
                                }
                                else if (Regex.IsMatch(_currentWork.Trim(), @"[0-9]{1}(:)[0-9]{2}"))
                                {
                                    regWorkTime = new Regex(@"[0-9]{1}(:)[0-9]{2}", RegexOptions.None);
                                    mWorkTime = regWorkTime.Match(_currentWork.Trim());
                                    _tpw.time = mWorkTime.Value;
                                }
                                else
                                {
                                    //如果不包含时间，就往前寻找时间
                                    bool hasGotIt2 = false;
                                    if (!hasBroken)
                                    {
                                        for (int _t = _pm.trainProjectWorkingModel.Count - 1; _t > 0; _t--)
                                        {
                                            if (_pm.trainProjectWorkingModel[_t].time.Length != 0)
                                            {
                                                _tpw.time = _pm.trainProjectWorkingModel[_t].time;
                                                hasGotIt2 = true;
                                                break;
                                            }
                                        }
                                    }
                                    else//解编后
                                    {
                                        bool hasGotInBreakOne = false;
                                        for (int _t = _pmBreak.trainProjectWorkingModel.Count - 1; _t > 0; _t--)
                                        {
                                            if (_pmBreak.trainProjectWorkingModel[_t].time.Length != 0)
                                            {
                                                _tpw.time = _pmBreak.trainProjectWorkingModel[_t].time;
                                                hasGotInBreakOne = true;
                                                hasGotIt2 = true;
                                                break;
                                            }
                                        }
                                        if (!hasGotInBreakOne)
                                        {//从前面找
                                            for (int _t = _pm.trainProjectWorkingModel.Count - 1; _t > 0; _t--)
                                            {
                                                if (_pm.trainProjectWorkingModel[_t].time.Length != 0)
                                                {
                                                    _tpw.time = _pm.trainProjectWorkingModel[_t].time;
                                                    hasGotIt2 = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (!hasGotIt2)
                                    {//默认为开始时间(同时将该钩开始时间调整)
                                        if (morningOrNight == 0)
                                        {
                                            _tpw.time = "8:00";
                                            _pm.startTime = "8:00";
                                        }
                                        else if (morningOrNight == 1)
                                        {
                                            _tpw.time = "16:00";
                                            _pm.startTime = "16:00";
                                        }
                                    }
                                }
                                //找股道
                                string trackNum = "";
                                string throughTrack = "";
                                if (_currentWork.Split('道').Length == 2)
                                {
                                    for (int tNum = 1; tNum <= 72; tNum++)
                                    {
                                        if (_currentWork.Contains(tNum + "道"))
                                        {
                                            trackNum = tNum.ToString();
                                        }
                                        if (_currentWork.Contains("JC" + tNum + "道"))
                                        {
                                            trackNum = "JC" + tNum.ToString();
                                        }
                                    }
                                }
                                //13	0G2046次(CRH380B-5864)18:23终到后经28道西端转JC3道西端停放。
                                //19	0G55482次(CRH380B-5654)09:31终到后经26道西端转JC5道西端停放。17:45转40道东端停放，18:35与(CRH380B-5837)进行重联作业。
                                else if (_currentWork.Split('道').Length == 3 && _currentWork.Contains("经"))
                                {
                                    throughTrack = _currentWork.Split('经')[1].Split('道')[0] + "G";
                                    if (_currentWork.Split('经')[1].Split('道')[1].Contains("西"))
                                    {
                                        throughTrack = throughTrack + "1";
                                    }
                                    else if (_currentWork.Split('经')[1].Split('道')[1].Contains("东"))
                                    {
                                        throughTrack = throughTrack + "2";
                                    }
                                    for (int tNum = 1; tNum <= 72; tNum++)
                                    {
                                        if ((_currentWork.Split('道')[1] + "道").Contains(tNum + "道"))
                                        {
                                            trackNum = tNum.ToString() + "G";
                                        }
                                        if ((_currentWork.Split('道')[1] + "道").Contains("JC" + tNum + "道"))
                                        {
                                            trackNum = "JC" + tNum.ToString() + "G";
                                        }
                                    }
                                    if (_currentWork.Split('道')[2].Contains("西"))
                                    {
                                        trackNum = trackNum + "1";
                                    }
                                    else if (_currentWork.Split('道')[2].Contains("东"))
                                    {
                                        trackNum = trackNum + "2";
                                    }

                                }

                                if (trackNum.Length == 0)
                                {
                                    //如果不包含股道，就往前寻找股道
                                    bool hasGotIt1 = false;
                                    if (!hasBroken)
                                    {
                                        for (int _t = _pm.trainProjectWorkingModel.Count - 1; _t > 0; _t--)
                                        {
                                            if (_pm.trainProjectWorkingModel[_t].track.Length != 0)
                                            {
                                                trackNum = _pm.trainProjectWorkingModel[_t].track;
                                                hasGotIt1 = true;
                                                break;
                                            }
                                        }
                                    }
                                    else//解编后
                                    {
                                        bool hasGotInBreakOne = false;
                                        for (int _t = _pmBreak.trainProjectWorkingModel.Count - 1; _t > 0; _t--)
                                        {
                                            if (_pmBreak.trainProjectWorkingModel[_t].track.Length != 0)
                                            {
                                                trackNum = _pmBreak.trainProjectWorkingModel[_t].track;
                                                hasGotInBreakOne = true;
                                                hasGotIt1 = true;
                                                break;
                                            }
                                        }
                                        if (!hasGotInBreakOne)
                                        {//从前面找
                                            for (int _t = _pm.trainProjectWorkingModel.Count - 1; _t > 0; _t--)
                                            {
                                                if (_pm.trainProjectWorkingModel[_t].track.Length != 0)
                                                {
                                                    trackNum = _pm.trainProjectWorkingModel[_t].track;
                                                    hasGotIt1 = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (!hasGotIt1)
                                    {//从头到尾都没有股道信息 这就很尴尬了。。我也不知道咋办了

                                    }
                                }
                                if (!trackNum.Contains("G"))
                                {
                                    trackNum = trackNum + "G";
                                }
                                //找一列位二列位
                                if (!trackNum.Contains("G1") && !trackNum.Contains("G2"))
                                {
                                    if (_currentWork.Contains("东端"))
                                    {
                                        trackNum = trackNum + "2";
                                    }
                                    else if (_currentWork.Contains("西端"))
                                    {
                                        trackNum = trackNum + "1";
                                    }
                                }
                                _tpw.track = trackNum;
                                _tpw.throughTrack = throughTrack;
                                //作业内容
                                string workingInformation = "";
                                if (trackNum.Contains("G1") || trackNum.Contains("G2"))
                                {
                                    workingInformation = _currentWork.Split('端')[_currentWork.Split('端').Length - 1];
                                }
                                else
                                {
                                    workingInformation = _currentWork.Split('道')[_currentWork.Split('道').Length - 1];
                                }
                                if (_currentWork.Contains("重联"))
                                {
                                    workingInformation = _currentWork.Replace(_tpw.track.Replace("G", "") + "道", "");
                                }
                                //如果本模型没有作业内容，那说明与前一个是同一项作业，直接把全部内容填写到前面去
                                //|第1钩、(CRH380A - 2876)JC2道西端停放，19:50转42道东端停放，与2645重联作业，与2645解编作业，备开10日(05:48)0G55481次。
                                if (workingInformation.Length == 0)
                                {
                                    for (int _t = _pm.trainProjectWorkingModel.Count - 1; _t > 0; _t--)
                                    {
                                        if (_pm.trainProjectWorkingModel[_t].workingInformation.Length != 0)
                                        {
                                            _pm.trainProjectWorkingModel[_t].workingInformation = _pm.trainProjectWorkingModel[_t].workingInformation + _currentWork;
                                            break;
                                        }
                                    }
                                    continue;
                                }
                                _tpw.workingInformation = workingInformation;
                                //特殊情况：重联
                                //重联前二者的工作流不同，分别存储，第一次找到的变长编，复制自身作为短编
                                /*短找长找到的：
                                将自身变成 - B钩，添加进找到的长编车前序序列，结束时间为重联时间，将长编车添加进自身后序序列
                                */
                                //如果之前已经有该短编被重联后的模型
                                if (_currentWork.Contains("重联") && _currentWork.Contains("(CR"))
                                {
                                    //短找长合并，合并后的事件可以不用管了，自身创建的时候就弄了，自身标记为3 自动忽略
                                    bool hasGotIt = false;
                                    if (_pm.trainConnectType == 0 && _pm.trainWorkingMode == 1 && !_pm.sameTrain_ProjectIndex.Equals("-1"))
                                    {
                                        for (int ij = 0; ij < _trainProjectModels.Count; ij++)
                                        {
                                            if (_trainProjectModels[ij].projectIndex.Equals(_pm.sameTrain_ProjectIndex))
                                            {
                                                //已弃用的方法
                                                //TrainProjectModel _itsOwn = (TrainProjectModel)_pm.Clone();
                                                //_trainProjectModels[ij].connectedTrainProjectModels.Add(_itsOwn);
                                                //_pm.trainWorkingMode = 3;

                                                //自身标记为-B钩
                                                _pm.projectIndex = _pm.projectIndex + "-B";
                                                //自身结束时间
                                                _pm.endTime = _tpw.time;
                                                //自身为短变长
                                                _pm.trainWorkingMode = 1;
                                                _pm.secondTrainId = "";
                                                //添加到已存在长编车的代表自身短编的前序序列
                                                TrainProjectStruct _tps_SelfInLong = new TrainProjectStruct();
                                                _tps_SelfInLong.trainId = _pm.trainId;
                                                _tps_SelfInLong.trainConnectType = 0;
                                                _tps_SelfInLong.projectIndex = _pm.projectIndex;
                                                //这个时间为重联前的最后一次工作时间
                                                if (_pm.trainProjectWorkingModel.Count > 0)
                                                {
                                                    _tps_SelfInLong.relatedMovingTime = _pm.trainProjectWorkingModel[_pm.trainProjectWorkingModel.Count - 1].time;
                                                }
                                                else
                                                {
                                                    _tps_SelfInLong.relatedMovingTime = "";
                                                }
                                                _trainProjectModels[ij].previousProject.Add(_tps_SelfInLong);
                                                //添加到自身短编车的长编车所在后序序列
                                                TrainProjectStruct _tps_LongInSelf = new TrainProjectStruct();
                                                _tps_LongInSelf.trainId = _trainProjectModels[ij].trainId;
                                                _tps_LongInSelf.secondTrainId = _trainProjectModels[ij].secondTrainId;
                                                _tps_LongInSelf.projectIndex = _trainProjectModels[ij].projectIndex;
                                                _tps_LongInSelf.trainConnectType = 2;
                                                _tps_LongInSelf.relatedMovingTime = "";
                                                _pm.nextProject.Add(_tps_LongInSelf);
                                                //此时对比长编车中的两个作业时间，取后来者为重联的发生时间，且统一后来者为两个车的结束时间
                                                if (_trainProjectModels[ij].previousProject.Count == 2)
                                                {
                                                    string timeA = _trainProjectModels[ij].previousProject[0].relatedMovingTime;
                                                    string timeB = _trainProjectModels[ij].previousProject[1].relatedMovingTime;
                                                    string indexA = _trainProjectModels[ij].previousProject[0].projectIndex;
                                                    string indexB = _trainProjectModels[ij].previousProject[1].projectIndex;
                                                    int timeA_INT, timeB_INT = 0;
                                                    int.TryParse(timeA.Replace(":", ""), out timeA_INT);
                                                    int.TryParse(timeB.Replace(":", ""), out timeB_INT);
                                                    //A大false还是B大true
                                                    bool AorB = false;
                                                    //注意0-8点排在19-23点前面
                                                    if (timeA_INT % 100 <= 8 && timeB_INT % 100 > 8)
                                                    {
                                                        AorB = false;
                                                    }
                                                    else if (timeA_INT % 100 > 8 && timeB_INT % 100 <= 8)
                                                    {
                                                        AorB = true;
                                                    }
                                                    else if ((timeA_INT % 100 <= 8 && timeB_INT % 100 <= 8) ||
                                                       (timeA_INT % 100 > 8 && timeB_INT % 100 > 8))
                                                    {
                                                        if (timeA_INT > timeB_INT)
                                                        {
                                                            AorB = false;
                                                        }
                                                        else
                                                        {
                                                            AorB = true;
                                                        }
                                                    }
                                                    if (timeA.Equals(timeB))
                                                    {
                                                        _trainProjectModels[ij].startTime = timeA;
                                                    }
                                                    else if (!AorB)
                                                    {
                                                        _trainProjectModels[ij].startTime = timeA;
                                                        //把B的结束时间替换为A的结束时间
                                                        for (int k = 0; k < _trainProjectModels.Count; k++)
                                                        {
                                                            if (_trainProjectModels[k].projectIndex.Equals(indexB))
                                                            {
                                                                _trainProjectModels[k].endTime = timeA;
                                                            }
                                                        }
                                                    }
                                                    else if (AorB)
                                                    {
                                                        _trainProjectModels[ij].startTime = timeB;
                                                        //把A的结束时间替换为B的结束时间
                                                        for (int k = 0; k < _trainProjectModels.Count; k++)
                                                        {
                                                            if (_trainProjectModels[k].projectIndex.Equals(indexA))
                                                            {
                                                                _trainProjectModels[k].endTime = timeB;
                                                            }
                                                        }
                                                    }
                                                    skipToNext = true;
                                                    hasGotIt = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (hasGotIt)
                                    {
                                        break;
                                    }
                                    /*短找长没找到时：
                                    将自身写作短变长，复制自身，
                                    将复制后的变成-A钩，自身的工作流清零，
                                    加入第二车次，写作重联，将复制后的后序写上自身，
                                    自身的前序加上复制后的，自身的开始时间为重联时间，
                                    复制后的结束时间为重联时间，继续寻找自身的。
                                     */
                                    if (_pm.trainConnectType == 0 && _pm.sameTrain_ProjectIndex.Equals("-1"))
                                    {
                                        //此时原有单组车存为重联车的一部分
                                        //车号
                                        try
                                        {
                                            Regex regTrain = new Regex(@"\(([^)]*)\)");
                                            //@"\(.*?\)"
                                            MatchCollection mTrain = regTrain.Matches(_currentWork);
                                            foreach (Match match in mTrain)
                                            {
                                                string value = match.Value.Trim('(', ')');
                                                if (!value.Contains("-"))
                                                {
                                                    continue;
                                                }
                                                if (!value.Split('-')[1].Equals(_pm.trainId) && value.Contains("CR"))
                                                {
                                                    _pm.secondTrainId = value.Split('-')[1];
                                                    break;
                                                }
                                            }
                                        }
                                        catch (ArgumentException ex)
                                        {
                                            // Syntax error in the regular expression
                                        }
                                        //自身写作短变长
                                        _pm.trainWorkingMode = 1;
                                        //复制自身作为短编车，改为-A钩，相同车钩号改为自身，直接存储起来不记录后序工作流
                                        TrainProjectModel _shortTPM = _pm.Clone() as TrainProjectModel;
                                        _shortTPM.sameTrain_ProjectIndex = _shortTPM.projectIndex;
                                        _shortTPM.projectIndex = _shortTPM.projectIndex + "-A";
                                        _shortTPM.secondTrainId = "";
                                        //结束时间为重联的前一个动作时间
                                        _shortTPM.endTime = _tpw.time;
                                        //在复制的短编车内标记自身长编车
                                        TrainProjectStruct _tps_LongInSelf = new TrainProjectStruct();
                                        _tps_LongInSelf.trainId = _pm.trainId;
                                        _tps_LongInSelf.secondTrainId = _pm.secondTrainId;
                                        _tps_LongInSelf.trainConnectType = 2;
                                        _tps_LongInSelf.projectIndex = _pm.projectIndex;
                                        _tps_LongInSelf.relatedMovingTime = "";
                                        _shortTPM.nextProject.Add(_tps_LongInSelf);
                                        //在自身标记短编车
                                        //添加到自身长编车的代表复制后短编的前序序列
                                        TrainProjectStruct _tps_SelfInLong = new TrainProjectStruct();
                                        _tps_SelfInLong.trainId = _shortTPM.trainId;
                                        _tps_SelfInLong.trainConnectType = 0;
                                        _tps_SelfInLong.projectIndex = _shortTPM.projectIndex;
                                        //这个时间为重联前的最后一次工作时间
                                        if (_shortTPM.trainProjectWorkingModel.Count > 0)
                                        {
                                            _tps_SelfInLong.relatedMovingTime = _shortTPM.trainProjectWorkingModel[_shortTPM.trainProjectWorkingModel.Count - 1].time;
                                        }
                                        else
                                        {
                                            _tps_SelfInLong.relatedMovingTime = "";
                                        }
                                        _pm.previousProject.Add(_tps_SelfInLong);
                                        //自身工作流清空
                                        _pm.trainProjectWorkingModel.Clear();
                                        //自身写作重联车
                                        _pm.trainConnectType = 2;
                                        //开始时间为重联时间
                                        _pm.startTime = _tpw.time;
                                        //存储短编车
                                        _trainProjectModels.Add(_shortTPM);
                                        /*已弃用的方法
                                        TrainProjectModel _itsOwn = (TrainProjectModel)_pm.Clone();
                                        _pm.trainProjectWorkingModel.Clear();
                                        _pm.trainConnectType = 2;
                                        _pm.trainWorkingMode = 1;
                                        _pm.connectedTrainProjectModels.Add(_itsOwn);
                                        */
                                    }
                                    if (_pm.trainConnectType == 2 && _pm.sameTrain_ProjectIndex.Equals("-1"))
                                    {//长找长没找到
                                        _pm.trainWorkingMode = 1;
                                    }
                                }
                                //长找长找到了的 后面加完计划后再找
                                //特殊情况：解编
                                //解编时自身前半部分模型是公用的，后半部分各自引用
                                if (_currentWork.Contains("进行解编") && _currentWork.Contains("道"))
                                {
                                    /*1、长找长没找到：
                                    自身标记为长变短，
                                    复制为 - A钩，
                                    复制后的（删除车次2，改为短编，
                                    删除工作流，开始时间为解编时间，
                                    将自身添加进前序序列），将复制后的添加进自身后序序列，
                                    自身结束时间为解编时间
                                    */
                                    //大概率是还不知道是解编后的哪个车，因此后面如果获得了，就替换车次1，删除车次2
                                    if (_pm.sameTrain_ProjectIndex.Equals("-1"))
                                    {
                                        _pm.trainWorkingMode = 2;
                                        _pmBreak = (TrainProjectModel)_pm.Clone();
                                        _pmBreak.projectIndex = _pmBreak.projectIndex + "-A";
                                        _pmBreak.trainConnectType = 0;
                                        _pmBreak.trainProjectWorkingModel.Clear();
                                        _pmBreak.startTime = _tpw.time;
                                        _pm.endTime = _tpw.time;
                                        //为便于识别，将_pm的sametrain改为-2
                                        _pm.sameTrain_ProjectIndex = "-2";
                                        //将长编车添加到短编车内
                                        TrainProjectStruct _tps_LongInShort = new TrainProjectStruct();
                                        _tps_LongInShort.trainId = _pm.trainId;
                                        _tps_LongInShort.secondTrainId = _pm.secondTrainId;
                                        _tps_LongInShort.trainConnectType = 2;
                                        _tps_LongInShort.projectIndex = _pm.projectIndex;
                                        //这个时间为解编前的最后一次工作时间

                                        if (_pm.trainProjectWorkingModel.Count > 0)
                                        {
                                            _tps_LongInShort.relatedMovingTime = _pm.trainProjectWorkingModel[_pm.trainProjectWorkingModel.Count - 1].time;
                                        }
                                        else
                                        {
                                            _tps_LongInShort.relatedMovingTime = "";
                                        }
                                        _pmBreak.previousProject.Add(_tps_LongInShort);
                                        //直接存储长编序列
                                        _trainProjectModels.Add(_pm);
                                        //可以进行下一次循环了，后面再添加序列
                                    }
                                    /*2、长找长找到了
                                    自身改为长变短，改为 - B钩，删除车次2，改为短编，删除工作流，开始时间为解编时间，将自身添加进找到的长编车的后序序列，将长编车添加进自身的前序序列
                                    */
                                    else
                                    {
                                        //其实就是把自身当做短编车来做了
                                        _pm.trainWorkingMode = 2;
                                        _pm.projectIndex = _pm.projectIndex + "-B";
                                        _pm.trainConnectType = 0;
                                        _pm.startTime = _tpw.time;
                                        _pm.trainProjectWorkingModel.Clear();
                                        _pmBreak = (TrainProjectModel)_pm.Clone();
                                        //将自身添加进找到的长编车
                                        for (int k = 0; k < _trainProjectModels.Count; k++)
                                        {
                                            if (_trainProjectModels[k].projectIndex.Equals(_pm.sameTrain_ProjectIndex))
                                            {
                                                //需要记录一下k的值，后面再添加（不知道自身解编后的车号）
                                                sameTrainIndex = k.ToString();
                                                //将长编车存于自身的前序序列
                                                TrainProjectStruct _tps_LongInSelf = new TrainProjectStruct();
                                                _tps_LongInSelf.trainId = _trainProjectModels[k].trainId;
                                                _tps_LongInSelf.secondTrainId = _trainProjectModels[k].secondTrainId;
                                                _tps_LongInSelf.projectIndex = _trainProjectModels[k].projectIndex;
                                                //这个时间为解编前的最后一次工作时间
                                                if (_trainProjectModels[k].trainProjectWorkingModel.Count > 0)
                                                {
                                                    _tps_LongInSelf.relatedMovingTime = _trainProjectModels[k].trainProjectWorkingModel[_trainProjectModels[k].trainProjectWorkingModel.Count - 1].time;
                                                }
                                                else
                                                {
                                                    _tps_LongInSelf.relatedMovingTime = "";
                                                }
                                                _pmBreak.previousProject.Add(_tps_LongInSelf);
                                            }
                                        }
                                        /*
                                    _pmBreak = (TrainProjectModel)_pm.Clone();
                                    _pm.trainWorkingMode = 3;
                                    _pmBreak.trainConnectType = 0;
                                    _pmBreak.trainWorkingMode = 2;
                                    _pmBreak.trainProjectWorkingModel.Clear();
                                    */
                                    }
                                    hasBroken = true;
                                }
                                //解编后遇到第二次循环，里面有车号的时候
                                if (_currentWork.Contains("(CR") && _currentWork.Contains("-") && hasBroken)
                                {//取该车号为解编后的自身车号，车号2删掉
                                    string brokenTrainID = _currentWork.Split('-')[1].Split(')')[0].Trim();
                                    _pmBreak.trainId = brokenTrainID;
                                    _pmBreak.secondTrainId = "";
                                    //将短编车存储于长编车的后序序列
                                    TrainProjectStruct _tps_selfInLong = new TrainProjectStruct();
                                    _tps_selfInLong.trainId = brokenTrainID;
                                    _tps_selfInLong.projectIndex = _pmBreak.projectIndex;
                                    _tps_selfInLong.trainConnectType = 0;
                                    _tps_selfInLong.relatedMovingTime = _tpw.time;
                                    //长找长没找到时
                                    if (_pm.sameTrain_ProjectIndex.Equals("-2"))
                                    {
                                        _pm.nextProject.Add(_tps_selfInLong);
                                    }
                                    else
                                    {//找到了时
                                        if (sameTrainIndex.Trim().Length != 0)
                                        {
                                            try
                                            {
                                                _trainProjectModels[int.Parse(sameTrainIndex)].nextProject.Add(_tps_selfInLong);
                                            }
                                            catch (Exception eMath)
                                            {
                                                int jk = 0;
                                            }
                                        }
                                    }
                                }
                                //添加工作流数据
                                if (!hasBroken)
                                {
                                    _pm.trainProjectWorkingModel.Add(_tpw);
                                }
                                else
                                {//解编后
                                    _pmBreak.trainProjectWorkingModel.Add(_tpw);
                                }
                                //可以跳过这一行了
                                if (skipToNext)
                                {
                                    continue;
                                }
                            }
                            //重联长找长，合并数据
                            if (_pm.trainConnectType == 2 && !_pm.sameTrain_ProjectIndex.Equals("-1") && _pm.nextProject.Count == 0)
                            {
                                for (int ij = 0; ij < _trainProjectModels.Count; ij++)
                                {
                                    if (_trainProjectModels[ij].projectIndex.Equals(_pm.sameTrain_ProjectIndex))
                                    {
                                        //检查入库车和出库车，最后将工作流添加进去
                                        if (_trainProjectModels[ij].getInside_trainNum.Length == 0 && _pm.getInside_trainNum.Length != 0)
                                        {
                                            _trainProjectModels[ij].getInside_trainNum = _pm.getInside_trainNum;
                                        }
                                        if (_trainProjectModels[ij].getInside_track.Length == 0 && _pm.getInside_track.Length != 0)
                                        {
                                            _trainProjectModels[ij].getInside_track = _pm.getInside_track;
                                        }
                                        if (_trainProjectModels[ij].getInside_time.Length == 0 && _pm.getInside_time.Length != 0)
                                        {
                                            _trainProjectModels[ij].getInside_time = _pm.getInside_time;
                                        }
                                        if (_trainProjectModels[ij].getOutside_time.Length == 0 && _pm.getOutside_time.Length != 0)
                                        {
                                            _trainProjectModels[ij].getOutside_time = _pm.getOutside_time;
                                        }
                                        if (_trainProjectModels[ij].getOutside_track.Length == 0 && _pm.getOutside_track.Length != 0)
                                        {
                                            _trainProjectModels[ij].getOutside_track = _pm.getOutside_track;
                                        }
                                        if (_trainProjectModels[ij].getOutside_trainNum.Length == 0 && _pm.getOutside_trainNum.Length != 0)
                                        {
                                            _trainProjectModels[ij].getOutside_trainNum = _pm.getOutside_trainNum;
                                        }
                                        foreach (TrainProjectWorking _tpwm in _pm.trainProjectWorkingModel)
                                        {//如果时间没有一样的就往里加
                                            bool hasGotKey = false;
                                            for (int ik = 0; ik < _trainProjectModels[ij].trainProjectWorkingModel.Count; ik++)
                                            {
                                                if (_tpwm.time.Equals(_trainProjectModels[ij].trainProjectWorkingModel[ik].time))
                                                {
                                                    hasGotKey = true;
                                                    break;
                                                }
                                            }
                                            if (hasGotKey)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                _trainProjectModels[ij].trainProjectWorkingModel.Add(_tpwm);
                                            }
                                        }
                                        //自身标记为3 默认不显示
                                        _pm.trainWorkingMode = 3;
                                        break;
                                    }
                                }
                            }
                        }
                        if (!hasBroken)
                        {
                            _trainProjectModels.Add(_pm);
                        }
                        else
                        {
                            _trainProjectModels.Add(_pmBreak);
                        }

                    }
                }
                allTrainProjectModels = _trainProjectModels;
                //调车作业计划排序
                //sortTrainProject(_trainProjectModels,morningOrNight);
            }
            catch(Exception trainProjectE)
            {
                MessageBox.Show("运行出现错误，请重试，若持续错误请联系车间。\n" + trainProjectE.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        //调车作业计划排序
        //0白1夜
        private void sortTrainProject(List<TrainProjectModel> _allTPM,int morningOrNight)
        {
            List<TrainProjectSortModel> _allTPSM = new List<TrainProjectSortModel>(); 
            foreach (TrainProjectModel _tp in _allTPM)
            {
                int getInCount = 0;
                //避免找到重复的入库信息
                string getInOriginalText = "";
                foreach (TrainProjectWorking _tpw in _tp.trainProjectWorkingModel)
                {
                    if(getInOriginalText.Length !=0 && getInOriginalText.Equals(_tpw.originalText))
                    {//找到重复的入库信息
                        continue;
                    }
                    TrainProjectSortModel _tps = new TrainProjectSortModel();
                    _tps.getInside_trainNum = _tp.getInside_trainNum;
                    _tps.getOutside_trainNum = _tp.getOutside_trainNum;
                    _tps.trainNumA = _tp.trainId;
                    _tps.trainNumB = _tp.secondTrainId;
                    _tps.morningOrNight = morningOrNight;
                    _tps.trainModel = _tp.trainModel;
                    _tps.projectIndex = _tp.projectIndex;
                    if (_tp.getInside_time.Length != 0 && getInCount == 0)
                    {//找入库车
                        TrainProjectWorking _getInTPW = findGetInOrOutsideTPW(_tp,_tp.getInside_time,false,morningOrNight);
                        _tps.originalText = _tps.getInside_trainNum + "次入所-" + _getInTPW.originalText;
                        _tps.track = _getInTPW.track;
                        _tps.time = _getInTPW.time;
                        getInOriginalText = _getInTPW.originalText;
                        getInCount = 1;
                    }
                    else
                    {
                        _tps.originalText = _tpw.originalText;
                        _tps.track = _tpw.track;
                        _tps.time = _tpw.time;
                    }
                    _allTPSM.Add(_tps);
                }
                if(_tp.getOutside_time.Length != 0)
                {
                    TrainProjectSortModel _tpsGetOutside = new TrainProjectSortModel();
                    _tpsGetOutside.getInside_trainNum = _tp.getInside_trainNum;
                    _tpsGetOutside.getOutside_trainNum = _tp.getOutside_trainNum;
                    _tpsGetOutside.morningOrNight = morningOrNight;
                    _tpsGetOutside.trainNumA = _tp.trainId;
                    _tpsGetOutside.trainNumB = _tp.secondTrainId;
                    //找出库车的真正股道
                    _tpsGetOutside.track = findGetInOrOutsideTPW(_tp, _tp.getOutside_time, true, morningOrNight).track;
                    _tpsGetOutside.originalText ="("+_tp.getOutside_day+"日)"+ _tpsGetOutside.track + "道" + _tp.getOutside_trainNum + "次出所";
                    _tpsGetOutside.time = _tp.getOutside_time;
                    _tpsGetOutside.trainModel = _tp.trainModel;
                    _tpsGetOutside.projectIndex = _tp.projectIndex;
                    _allTPSM.Add(_tpsGetOutside);
                }

            }
            _allTPSM.Sort();
                Document document = new Document();
                Section section = document.AddSection();

            Paragraph paraTitle = section.AddParagraph();
            string morNightStr = "";
            if (morningOrNight == 0)
            {
                morNightStr = "（白班）";
            }else if(morningOrNight == 1)
            {
                morNightStr = "（夜班）";
            }
            TextRange title = paraTitle.AppendText("动车所调车作业计划"+morNightStr+"-" + DateTime.Now.ToString("yyyyMMdd"));

            ParagraphStyle styleTitle = new ParagraphStyle(document);
            styleTitle.Name = "titleStyle";
            styleTitle.CharacterFormat.FontSize = 20;
            styleTitle.CharacterFormat.TextColor = Color.Black;
            styleTitle.CharacterFormat.FontName = "黑体";
            //将自定义样式添加到文档
            document.Styles.Add(styleTitle);
            paraTitle.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            paraTitle.ApplyStyle(styleTitle.Name);

            Table table = section.AddTable(true);
                table.TableFormat.HorizontalAlignment = RowAlignment.Center;
                table.TableFormat.LeftIndent = 34;
            //序号，时间，车号，内容，入库，出库，钩号
            table.ResetCells(_allTPSM.Count + 1, 7);

                TableRow row = table.Rows[0];
                row.IsHeader = true;

                Paragraph para = row.Cells[0].AddParagraph();
                    //设置自定义样式
            ParagraphStyle style = new ParagraphStyle(document);
            style.Name = "TableStyle";
            style.CharacterFormat.FontSize = 10;
            style.CharacterFormat.TextColor = Color.Black;
            style.CharacterFormat.FontName = "宋体";
            //将自定义样式添加到文档
            document.Styles.Add(style);

                TextRange TR = para.AppendText("序");
                table.ApplyStyle(DefaultTableStyle.LightGridAccent1);

                para = row.Cells[1].AddParagraph();
            TR = para.AppendText("时间");

                para = row.Cells[2].AddParagraph();
            TR = para.AppendText("车号");

                para = row.Cells[3].AddParagraph();
            TR = para.AppendText("作业内容");

                para = row.Cells[4].AddParagraph();
            TR = para.AppendText("入库");

                para = row.Cells[5].AddParagraph();
            TR = para.AppendText("出库");

            para = row.Cells[6].AddParagraph();
            TR = para.AppendText("钩");

            for (int i = 0; i < _allTPSM.Count; i++)
                {
                    TableRow rowData = table.Rows[i+1];
                rowData.IsHeader = false;

                    Paragraph paraData = rowData.Cells[0].AddParagraph();
                    TextRange TRData = paraData.AppendText((i+1).ToString());

                paraData = rowData.Cells[1].AddParagraph();
                if ((_allTPSM[i].morningOrNight == 0 && _allTPSM[i].time.Equals("8:00")) ||
                    (_allTPSM[i].morningOrNight == 1 && _allTPSM[i].time.Equals("16:00")))
                {
                    TRData = paraData.AppendText("已办理");
                }
                else
                {
                    TRData = paraData.AppendText(_allTPSM[i].time);
                }

                string trainString = _allTPSM[i].trainModel + "-" + _allTPSM[i].trainNumA;
                if (_allTPSM[i].trainNumB.Length != 0)
                {
                    trainString = trainString + "+" + _allTPSM[i].trainNumB;
                }
                paraData = rowData.Cells[2].AddParagraph();
                TRData = paraData.AppendText(trainString);

                paraData = rowData.Cells[3].AddParagraph();
                TRData = paraData.AppendText(_allTPSM[i].originalText.Trim().Replace("\n","").Replace("\r\n",""));

                paraData = rowData.Cells[4].AddParagraph();
                TRData = paraData.AppendText(_allTPSM[i].getInside_trainNum);

                paraData = rowData.Cells[5].AddParagraph();
                TRData = paraData.AppendText(_allTPSM[i].getOutside_trainNum);

                paraData = rowData.Cells[6].AddParagraph();
                TRData = paraData.AppendText(_allTPSM[i].projectIndex.Split('-')[0]);
            }

                for(int j = 0; j < table.Rows.Count; j++)
            {
                //获取第一个表格
                Table selectedTable = section.Tables[0] as Table;
                TableRow selectedRow = selectedTable.Rows[j];
                selectedRow.Cells[0].Width = 5;
                selectedRow.Cells[1].Width = 50;
                selectedRow.Cells[2].Width = 120;
                selectedRow.Cells[3].Width = 180;
                selectedRow.Cells[4].Width = 40;
                selectedRow.Cells[5].Width = 40;
                selectedRow.Cells[6].Width = 5;
                for (int l = 0; l < 7; l++)
                {//默认7列
                    selectedTable[j, l].Paragraphs[0].Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    selectedTable[j, l].Paragraphs[0].Format.TextAlignment = Spire.Doc.Documents.TextAlignment.Center;
                    table[j, l].Paragraphs[0].ApplyStyle(style.Name);
                }

            }

                document.SaveToFile(Application.StartupPath + "\\动车所-调车作业计划\\" + "已整理-" + DateTime.Now.ToString("yyyyMMdd") + ".docx");
                System.Diagnostics.Process.Start(Application.StartupPath + "\\动车所-调车作业计划\\" + "已整理-" + DateTime.Now.ToString("yyyyMMdd") + ".docx");
        }

        //寻找入库出库股道和所在的计划（通过时间排序）
        //从目标时间开始往前（后）找，直至找到第一个时间为止（碰到24点换日）
        //In == false Out == true
        private TrainProjectWorking findGetInOrOutsideTPW(TrainProjectModel _tpm,string time,bool InOrOut,int morningOrNight)
        {
            TrainProjectWorking _targetTPW = new TrainProjectWorking();
            string trackNumber = "";
            int targetTimeInt = -1;
            int.TryParse(time.Replace(":", ""), out targetTimeInt);
            int nearestTimeInt = -1;
            if(targetTimeInt == -1)
            {
                return null;
            }
            if (InOrOut && morningOrNight == 0)
            {//白班出库，找与目标时间最接近的前序时间
                foreach(TrainProjectWorking _tpw in _tpm.trainProjectWorkingModel)
                {
                    int compareTimeInt = -1;
                    int.TryParse(_tpw.time.Replace(":", ""), out compareTimeInt);
                    if(compareTimeInt == -1 || (compareTimeInt > targetTimeInt))
                    {
                        continue;
                    }
                    else if(nearestTimeInt == -1)
                    {
                        nearestTimeInt = compareTimeInt;
                        trackNumber = _tpw.track;
                        _targetTPW = _tpw;
                    }
                    else
                    //比较
                    {
                        if ((targetTimeInt - compareTimeInt) < (targetTimeInt - nearestTimeInt))
                        {
                            nearestTimeInt = compareTimeInt;
                            trackNumber = _tpw.track;
                            _targetTPW = _tpw;
                        }
                    }
                }
            }
            else if(InOrOut && morningOrNight == 1)
            {//夜班出库，以16点为基准时间
                if(targetTimeInt < 1600)
                {
                    targetTimeInt = targetTimeInt + 2400;
                }
                foreach (TrainProjectWorking _tpw in _tpm.trainProjectWorkingModel)
                {
                    int compareTimeInt = -1;
                    int.TryParse(_tpw.time.Replace(":", ""), out compareTimeInt);
                    if(compareTimeInt < 1600 && compareTimeInt != -1)
                    {
                        compareTimeInt = compareTimeInt + 2400;
                    }
                    if (compareTimeInt == -1 || (compareTimeInt > targetTimeInt))
                    {
                        continue;
                    }else if (nearestTimeInt == -1)
                    {
                        nearestTimeInt = compareTimeInt;
                        trackNumber = _tpw.track;
                        _targetTPW = _tpw;
                    }
                    else
                    //比较
                    {
                        if ((targetTimeInt - compareTimeInt) < (targetTimeInt - nearestTimeInt))
                        {
                            nearestTimeInt = compareTimeInt;
                            trackNumber = _tpw.track;
                            _targetTPW = _tpw;
                        }
                    }
                }
            }
            else if (!InOrOut && morningOrNight == 0)
            {//白班入库，找与目标时间最接近的后序时间
                foreach (TrainProjectWorking _tpw in _tpm.trainProjectWorkingModel)
                {
                    int compareTimeInt = -1;
                    int.TryParse(_tpw.time.Replace(":", ""), out compareTimeInt);
                    if (compareTimeInt == -1 || (compareTimeInt < targetTimeInt))
                    {
                        continue;
                    }
                    else if (nearestTimeInt == -1)
                    {
                        nearestTimeInt = compareTimeInt;
                        trackNumber = _tpw.track;
                        _targetTPW = _tpw;
                    }
                    else
                    //比较
                    {
                        if ((compareTimeInt - targetTimeInt) < (nearestTimeInt - targetTimeInt))
                        {
                            nearestTimeInt = compareTimeInt;
                            trackNumber = _tpw.track;
                            _targetTPW = _tpw;
                        }
                    }
                }
            }
            else if (!InOrOut && morningOrNight == 1)
            {//夜班入库，以16点为基准时间
                if (targetTimeInt < 1600)
                {
                    targetTimeInt = targetTimeInt + 2400;
                }
                foreach (TrainProjectWorking _tpw in _tpm.trainProjectWorkingModel)
                {
                    int compareTimeInt = -1;
                    int.TryParse(_tpw.time.Replace(":", ""), out compareTimeInt);
                    if (compareTimeInt < 1600 && compareTimeInt != -1)
                    {
                        compareTimeInt = compareTimeInt + 2400;
                    }
                    if (compareTimeInt == -1 || (compareTimeInt < targetTimeInt))
                    {
                        continue;
                    }
                    else if (nearestTimeInt == -1)
                    {
                        nearestTimeInt = compareTimeInt;
                        trackNumber = _tpw.track;
                        _targetTPW = _tpw;
                    }
                    else
                    //比较
                    {
                        if ((compareTimeInt - targetTimeInt) < (nearestTimeInt - targetTimeInt))
                        {
                            nearestTimeInt = compareTimeInt;
                            trackNumber = _tpw.track;
                            _targetTPW = _tpw;
                        }
                    }
                }
            }
            return _targetTPW;
        }

        private string checkEmptyTrack(List<TrainProjectModel> _trainProjectModels)
        {//找计划中的空股道
            try
            {
                string[] _allTracks = allEMUGarageTracks;
                List<string> _emptyTracks = new List<string>();
                for (int j = 0; j < _allTracks.Length; j++)
                {
                    string _t = _allTracks[j];
                    _emptyTracks.Add(_t);
                }
                foreach (TrainProjectModel _tpm in _trainProjectModels)
                {
                    string track = "";
                    if (_tpm.trainProjectWorkingModel.Count == 0)
                    {
                        continue;
                    }
                    else
                    {
                        track = _tpm.trainProjectWorkingModel[_tpm.trainProjectWorkingModel.Count - 1].track;
                    }
                    if (track != null && track.Length != 0)
                    {//开始匹配
                        if (!track.Contains("JC"))
                        {
                            bool hasGotIt = false;
                            for (int i = 0; i < _emptyTracks.Count; i++)
                            {//优先全字匹配
                                if (track.Equals(_emptyTracks[i]))
                                {
                                    _emptyTracks.RemoveAt(i);
                                    i--;
                                    hasGotIt = true;
                                    break;
                                }
                            }
                            if (!hasGotIt)
                            {
                                for (int i = 0; i < _emptyTracks.Count; i++)
                                {//如果匹配不上的话再split匹配
                                    if (track.Split('G')[0].Equals(_emptyTracks[i]))
                                    {
                                        _emptyTracks.RemoveAt(i);
                                        i--;
                                        hasGotIt = true;
                                        break;
                                    }
                                }
                            }
                            if (!hasGotIt)
                            {//还没匹配上的话，自身为长编 但是可能停到了分割信号里
                                for (int i = 0; i < _emptyTracks.Count; i++)
                                {
                                    if (track.Split('G')[0].Equals(_emptyTracks[i].Split('G')[0]))
                                    {
                                        _emptyTracks.RemoveAt(i);
                                        i--;
                                    }
                                }
                            }
                        }
                        else
                        {//最后一钩进检查线的车，判断进检查线的时间是否在0点后，是的话之前进的存场股道 不计为空股道
                            int hours = -1;
                            int.TryParse(_tpm.trainProjectWorkingModel[_tpm.trainProjectWorkingModel.Count - 1].time.Split(':')[0], out hours);
                            if (hours < 8 && hours != -1)
                            {
                                bool hasGotIt = false;
                                if (_tpm.trainProjectWorkingModel.Count > 1)
                                {
                                    if (_tpm.trainProjectWorkingModel[_tpm.trainProjectWorkingModel.Count - 2].track != null &&
                                        _tpm.trainProjectWorkingModel[_tpm.trainProjectWorkingModel.Count - 2].track.Length != 0 &&
                                        !_tpm.trainProjectWorkingModel[_tpm.trainProjectWorkingModel.Count - 2].track.Contains("JC"))
                                    {
                                        track = _tpm.trainProjectWorkingModel[_tpm.trainProjectWorkingModel.Count - 2].track;
                                    }
                                }
                                for (int i = 0; i < _emptyTracks.Count; i++)
                                {//优先全字匹配
                                    if (track.Equals(_emptyTracks[i]))
                                    {
                                        _emptyTracks.RemoveAt(i);
                                        i--;
                                        hasGotIt = true;
                                        break;
                                    }
                                }
                                if (!hasGotIt)
                                {
                                    for (int i = 0; i < _emptyTracks.Count; i++)
                                    {//如果匹配不上的话再split匹配
                                        if (track.Split('G')[0].Equals(_emptyTracks[i]))
                                        {
                                            _emptyTracks.RemoveAt(i);
                                            i--;
                                            hasGotIt = true;
                                            break;
                                        }
                                    }
                                }
                                if (!hasGotIt)
                                {//还没匹配上的话，自身为长编 但是可能停到了分割信号里
                                    for (int i = 0; i < _emptyTracks.Count; i++)
                                    {
                                        if (track.Split('G')[0].Equals(_emptyTracks[i].Split('G')[0]))
                                        {
                                            _emptyTracks.RemoveAt(i);
                                            i--;
                                        }
                                    }
                                }
                            }
                        }

                    }

                }
                string returnTracks = "";
                foreach (string _t in _emptyTracks)
                {
                    returnTracks = returnTracks + _t + "  ";
                }
                return returnTracks;
            }
            catch (Exception checkEmpE)
            {
                MessageBox.Show("运行出现错误，请重试，若持续错误请联系车间。\n" + checkEmpE.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return "";

        }
        private void matchTrackWithTrain_Project_btn_Click(object sender, EventArgs e)
        {
            sortTrainProject(allTrainProjectModels, morningOrNight);
            /*
            trainTypeAutoComplete(true);
            Form _displaySystem = new Display();
            _displaySystem.Show();
            */

        }

        private void radioButton5_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void startDataAnalyse()
        {
            DataAnalyse _form = new DataAnalyse(commandModel,operationChangedAnalyse,continueTrainAnalyse);
            _form.Show();
        }

        private void dataAnalyse_btn_Click(object sender, EventArgs e)
        {
            startDataAnalyse();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                automaticDeleteStoppedTrains = true;
            }
            else
            {
                automaticDeleteStoppedTrains = false;
            }
        }

        private void trainProjectBtnCheck()
        {//判断是否启用调车作业辅助(测试版)
            if (hasTrainProjectFile)
            {
                matchTrackWithTrain_Project_btn.Enabled = true;
            }
            else
            {
                matchTrackWithTrain_Project_btn.Enabled = false;
            }
        }

    }
}
