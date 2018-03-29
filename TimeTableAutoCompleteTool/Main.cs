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

namespace TimeTableAutoCompleteTool
{
    public partial class Main : Form
    {
        private Boolean hasText = false;
        private Boolean hasFilePath = false;
        private List<CommandModel> commandModel;
        private List<CaculatorModel> caculatorModel;
        OpenFileDialog ExcelFile;
        string filePath = "";

        public Main()
        {
            
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "TrainTimetableAutoCompleteTool-时刻表辅助工具";
            buildLBL.Text = "180303";
            start_Btn.Enabled = false;
            TrainEarlyCaculator_Btn.Enabled = false;
        }

        private void command_rTb_TextChanged(object sender, EventArgs e)
        {
            if(command_rTb.Text.Length != 0)
            {
                hasText = true;
                startBtnCheck();
                analyseCommand();
            }
            else
            {
                hasText = false;
                startBtnCheck();
            }
        }

        private void importTimeTable_Btn_Click(object sender, EventArgs e)
        {
            SelectPath();
        }

        private void startBtnCheck()
        {
            if(hasFilePath && hasText)
            {
                start_Btn.Enabled = true;
                TrainEarlyCaculator_Btn.Enabled = true;
            }
            else
            {
                start_Btn.Enabled = false;
                TrainEarlyCaculator_Btn.Enabled = false;
            }
        }

        private void start_Btn_Click(object sender, EventArgs e)
        {
            if(commandModel.Count != 0)
            {
                updateTimeTable();
            }
            else
            {
                MessageBox.Show("未检测到任何车次信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void analyseCommand()
        {   //分析客调命令
            //删除不需要的标点符号-字符
            String[] AllCommand = removeUnuseableWord().Split('。');
            List<CommandModel> AllModels = new List<CommandModel>();
            for (int i = 0; i < AllCommand.Length; i++)
            {
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
                        if(!command[1].Contains("G") &&
                        !command[1].Contains("D") &&
                        !command[1].Contains("C") &&
                        !command[1].Contains("J") )
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

                    for(int timeCount = 0;timeCount < command.Length; timeCount++)
                    {
                        if (command[timeCount].Contains("CR"))
                        {
                            for(int word = 0; word < command[timeCount].Split('，').Length; word++)
                            {
                                if(command[timeCount].Split('，')[word].Contains("CR")||
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
                                if(command[command.Length - n - 1].Contains("G")||
                                    command[command.Length - n - 1].Contains("D") ||
                                    command[command.Length - n - 1].Contains("C") ||
                                    command[command.Length - n - 1].Contains("J") ||
                                    command[command.Length - n - 1].Contains("0"))
                                {//如果停运标记后面还有车的话
                                    List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(command[command.Length - n - 1], @"[\u4e00-\u9fa5]", "").Replace('）', ' ').Replace('，', ' ').Split('-'), 1, trainType,trainModel);
                                    foreach (CommandModel model in tempModels)
                                    {
                                        AllModels.Add(model);
                                    }
                                }
                                isNormal = false;
                                AllTrainNumberInOneRaw = command[1].Split('-');
                                //寻找车次中的括号左半部分
                                //从前往后找，找到标记后的车次为停开
                                bool stopped = false ;
                                for (int m = 0; m < AllTrainNumberInOneRaw.Length; m++)
                                {
                                    if(AllTrainNumberInOneRaw[m].Contains("（G")||
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
                                        List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[m], @"[\u4e00-\u9fa5]", "").Replace("（","").Replace("）","").Split('-'), 0, trainType, trainModel);
                                        foreach (CommandModel model in tempModels)
                                        {
                                            AllModels.Add(model);
                                        }
                                    }
                                    else if(stopped == false)
                                    {//开
                                        List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[m], @"[\u4e00-\u9fa5]", "").Replace("（","").Replace("）", "").Split('-'), 1, trainType,trainModel);
                                        foreach (CommandModel model in tempModels)
                                        {
                                            AllModels.Add(model);
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
                                List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（","").Replace("）", "").Split('-'),0,trainType, trainModel);
                                foreach(CommandModel model in tempModels)
                                {
                                    AllModels.Add(model);
                                }
                            }
                            else
                            {
                                List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 1, trainType, trainModel);
                                foreach (CommandModel model in tempModels)
                                {
                                    AllModels.Add(model);
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
                                    tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 2, trainType, trainModel);
                                }
                                else
                                {
                                    tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), streamStatus, trainType, trainModel);
                                }
                                foreach (CommandModel model in tempModels)
                                {
                                    AllModels.Add(model);
                                }
                            }
                            else
                            {
                                List<CommandModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), streamStatus, trainType, trainModel);
                                foreach (CommandModel model in tempModels)
                                {
                                    AllModels.Add(model);
                                }
                            }
                        }
                    }
                    else if(isNormal)
                    {//如果一切正常 则
                        //把车次单独分离-去中文-去横杠-由于部分情况下无法辨认小括号-因此必须在此处去除小括号
                        AllTrainNumberInOneRaw = Regex.Replace(command[1], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-');
                        //把车次添加模型
                        List<CommandModel> tempModels = trainModelAddFunc(AllTrainNumberInOneRaw, streamStatus, trainType, trainModel);
                        foreach(CommandModel model in tempModels)
                        {
                            AllModels.Add(model);
                        }
                    }
                }
            }
            //右方显示框内容
            String commands = "";
            String wrongCommands = "";
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
                if (!IsTrainNumber(model.trainNumber))
                {//筛选不合规车次
                    wrongCommands = wrongCommands + model.trainNumber + "-" + streamStatus + "-" + trainType + "\r\n";
                }
                else
                {
                    commands = commands + model.trainNumber + "-" + streamStatus + "-" + trainType + "\r\n";
                }
                
            }
            outputTB.Text = "共"+AllModels.Count.ToString()+"趟" +"\r\n"+commands;
            wrongTB.Text = wrongCommands;
            commandModel = AllModels;
            
        }

        public bool IsTrainNumber(string input)
        {//判断是否是符合规范的车次 若不符合 则给予识别错误提示
            bool _isTrainNumber = false;
            Regex regexOnlyNumAndAlphabeta = new Regex(@"^[A-Za-z0-9]+$");
            Regex regexOnlyAlphabeta = new Regex(@"^[A-Za-z]+$");
            if (regexOnlyNumAndAlphabeta.IsMatch(input) &&
                !regexOnlyAlphabeta.IsMatch(input)&&
                input.Length < 8 &&
                input.Length > 1)
            {
                _isTrainNumber = true;
            }
            return _isTrainNumber ;
        }

        private String removeUnuseableWord()
        {//字符转换
            String standardCommand = command_rTb.Text.ToString();
            if (standardCommand.Contains(":"))
                standardCommand = standardCommand.Replace(":", "：");
            if (standardCommand.Contains("~"))
                standardCommand = standardCommand.Replace("~", "～");
            if (standardCommand.Contains("～"))
                standardCommand = standardCommand.Replace("～", "");
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
            if(standardCommand.Contains("【"))
                standardCommand = standardCommand.Replace("【", "（");
            if (standardCommand.Contains("】"))
                standardCommand = standardCommand.Replace("】", "）");
            if (standardCommand.Contains("{"))
                standardCommand = standardCommand.Replace("{", "）");
            if (standardCommand.Contains("}"))
                standardCommand = standardCommand.Replace("}", "）");
            return standardCommand;
        }

        private List<CommandModel> trainModelAddFunc(String[] AllTrainNumberInOneRaw, int streamStatus,int trainType,string trainModel)
        {//建立车次模型-通用方法
            //处理单程双车次车辆
            int trainConnectType = -1;
            List<CommandModel> AllModels = new List<CommandModel>();
            if(!trainModel.Equals("null"))
            {//0短编 1长编 2重联
                if(trainModel.Contains("L")||
                    trainModel.Contains("2B")
                    )
                {
                    trainConnectType = 1;
                }else if (trainModel.Contains("+"))
                {
                    trainConnectType = 2;
                }
                else
                {
                    trainConnectType = 0;
                }

            }
        for (int k = 0; k < AllTrainNumberInOneRaw.Length; k++)
        {
            if(AllTrainNumberInOneRaw[k].Contains("G") ||
               AllTrainNumberInOneRaw[k].Contains("D") ||
               AllTrainNumberInOneRaw[k].Contains("C") ||
               AllTrainNumberInOneRaw[k].Contains("J") ||
               AllTrainNumberInOneRaw[k].Contains("00"))
                {
                    if (AllTrainNumberInOneRaw[k].Contains("/"))
                    {
                        String[] trainWithDoubleNumber = AllTrainNumberInOneRaw[k].Split('/');
                        //先添加第一个车次
                        CommandModel m1 = new CommandModel();
                        m1.trainNumber = trainWithDoubleNumber[0].Trim();
                        m1.streamStatus = streamStatus;
                        m1.trainType = trainType;
                        m1.trainModel = trainModel;
                        m1.trainConnectType = trainConnectType;
                        AllModels.Add(m1);

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
                                CommandModel m2 = new CommandModel();
                                m2.trainNumber = secondTrainWord.Trim();
                                m2.streamStatus = streamStatus;
                                m2.trainType = trainType;
                                m2.trainModel = trainModel;
                                m2.trainConnectType = trainConnectType;
                                AllModels.Add(m2);
                                break;
                            }
                        }
                    }
                    else if (AllTrainNumberInOneRaw[k].Length != 0)
                    {
                        CommandModel model = new CommandModel();
                        model.trainNumber = AllTrainNumberInOneRaw[k].Trim();
                        model.streamStatus = streamStatus;
                        model.trainType = trainType;
                        model.trainModel = trainModel;
                        model.trainConnectType = trainConnectType;
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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();   //显示选择文件对话框 
            openFileDialog1.Filter = "Excel 文件 |*.xlsx;*.xls";
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\时刻表\\";
            //openFileDialog1.Filter = "Excel 2003 文件 (*.xls)|*.xls";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.filePathLBL.Text = "已选择："+openFileDialog1.FileName;     //显示文件路径 
                hasFilePath = true;
                ExcelFile = openFileDialog1;
                startBtnCheck();
            }
        }

        private void updateTimeTable()
        {
            IWorkbook workbook = null;  //新建IWorkbook对象  
            string fileName = ExcelFile.FileName;
            //车次统计
            int allTrainsCount = 0;
            int allPsngerTrainsCount = 0;
            int stoppedTrainsCount = 0;
            int allTrainsInTimeTable = 0;

            try
            {
                FileStream fileStream = new FileStream(ExcelFile.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                {
                    try
                    {
                        workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                    }catch(Exception e)
                    {
                        if(File.Exists(Application.StartupPath + "\\时刻表\\自动备份-" + ExcelFile.FileName.ToString().Split('\\')[ExcelFile.FileName.ToString().Split('\\').Length - 1]))
                        {
                            MessageBox.Show("时刻表文件出现损坏【已启用热备恢复文件:)】请对本机进行病毒扫描\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            string pLocalFilePath = Application.StartupPath + "\\时刻表\\自动备份-" + ExcelFile.FileName.ToString().Split('\\')[ExcelFile.FileName.ToString().Split('\\').Length - 1];//要复制的文件路径
                            string pSaveFilePath = ExcelFile.FileName;//指定存储的路径
                            File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                            fileStream = new FileStream(ExcelFile.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                            workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
                        }
                        else
                        {
                            MessageBox.Show("时刻表文件出现损坏（或时刻表无效），请杀毒并从车间复制时刻表文件至此\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    catch(Exception e)
                    {
                        if (File.Exists(Application.StartupPath + "\\时刻表\\自动备份-" + ExcelFile.FileName.ToString().Split('\\')[ExcelFile.FileName.ToString().Split('\\').Length - 1]))
                        {
                            MessageBox.Show("时刻表文件出现损坏【已启用热备恢复文件:)】请对本机进行病毒扫描\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            string pLocalFilePath = Application.StartupPath + "\\时刻表\\自动备份-" + ExcelFile.FileName.ToString().Split('\\')[ExcelFile.FileName.ToString().Split('\\').Length - 1];//要复制的文件路径
                            string pSaveFilePath = ExcelFile.FileName;//指定存储的路径
                            File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                            fileStream = new FileStream(ExcelFile.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                            workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
                        }
                        else
                        {
                            MessageBox.Show("时刻表文件出现损坏（或时刻表无效），请杀毒并从车间复制时刻表文件至此\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\时刻表\\");
                            return;
                        }
                    }      
                }

                if (workbook != null && !ExcelFile.FileName.Contains("自动备份-"))
                {
                    string pLocalFilePath = ExcelFile.FileName.ToString();//要复制的文件路径
                    string pSaveFilePath = Application.StartupPath + "\\时刻表\\自动备份-" + ExcelFile.FileName.ToString().Split('\\')[ExcelFile.FileName.ToString().Split('\\').Length - 1];//指定存储的路径
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

                ICellStyle normalTrainStyle = workbook.CreateCellStyle();
                normalTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                normalTrainStyle.FillPattern = FillPattern.SolidForeground;
                normalTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                normalTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                normalTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                normalTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                normalTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

                ICellStyle tomorrowlTrainStyle = workbook.CreateCellStyle();
                tomorrowlTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                tomorrowlTrainStyle.FillPattern = FillPattern.SolidForeground;
                tomorrowlTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                tomorrowlTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                tomorrowlTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                tomorrowlTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                tomorrowlTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

                ICellStyle removeColors = workbook.CreateCellStyle();
                removeColors.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                removeColors.FillPattern = FillPattern.SolidForeground;
                removeColors.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                removeColors.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                removeColors.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                removeColors.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                removeColors.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

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
                                {//把车次表格先刷白去字
                                    if (!row.GetCell(j).ToString().Contains("由") &&
                                        !row.GetCell(j).ToString().Contains("续") &&
                                        !row.GetCell(j).ToString().Contains("开行"))
                                    {
                                        //时刻表中车次+1
                                        allTrainsInTimeTable++;
                                        //去中文后再找-去掉高峰-周末-临客等字
                                        row.GetCell(j).CellStyle = removeColors;
                                        row.GetCell(j).SetCellValue(Regex.Replace(row.GetCell(j).ToString(), @"[\u4e00-\u9fa5]", ""));
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
                                        if (row.GetCell(j).ToString().Trim().Equals(model.trainNumber))
                                        {
                                            ContainsTrainNumber = true;
                                            //车次统计+1
                                            allTrainsCount++;
                                            if (!row.GetCell(j).ToString().Trim().Contains("0G")&&
                                                !row.GetCell(j).ToString().Trim().Contains("0D") &&
                                                !row.GetCell(j).ToString().Trim().Contains("0J") &&
                                                !row.GetCell(j).ToString().Trim().Contains("0C") &&
                                                !row.GetCell(j).ToString().Trim().Contains("00") &&
                                                !row.GetCell(j).ToString().Trim().Contains("DJ"))
                                            {
                                                allPsngerTrainsCount++;
                                            }
                                            if (model.streamStatus == 1)
                                            {
                                                row.GetCell(j).CellStyle = normalTrainStyle;
                                            }
                                            else if(model.streamStatus == 0)
                                            {
                                                stoppedTrainsCount++;
                                                row.GetCell(j).CellStyle = stoppedTrainStyle;
                                            }else if(model.streamStatus == 2)
                                            {
                                                row.GetCell(j).CellStyle = tomorrowlTrainStyle;
                                            }
                                            if(model.trainType == 1)
                                            {
                                                row.GetCell(j).SetCellValue("高峰"+ row.GetCell(j).ToString());
                                            }
                                            else if (model.trainType == 2)
                                            {
                                                row.GetCell(j).SetCellValue("临客" + row.GetCell(j).ToString());
                                            }
                                            else if (model.trainType == 3)
                                            {
                                                row.GetCell(j).SetCellValue("周末" + row.GetCell(j).ToString());
                                            }
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
                /*重新修改文件指定单元格样式*/
                    FileStream fs1 = File.OpenWrite(ExcelFile.FileName);
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
                MessageBox.Show("时刻表修改完成，点击确定后将打开文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
                //info.WorkingDirectory = Application.StartupPath;
                 info.FileName = ExcelFile.FileName;
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
                MessageBox.Show("时刻表文件正在使用中，请关闭后重试","提示",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        //赶点计算器
        private void TrainEarlyCaculator_Btn_Click(object sender, EventArgs e)
        {
            if(caculatorModel == null ||
                caculatorModel.Count == 0||
                !filePath.Equals(ExcelFile.FileName.ToString()))
            {
                if (!startCaculator())
                {//返回false 即模型内无内容
                    MessageBox.Show("未匹配到车次，赶点车次为18点以后的回库车 以及全天的旅客列车。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            filePath = ExcelFile.FileName.ToString();
            TrainEarlyCaculator form = new TrainEarlyCaculator(caculatorModel);
            form.Show();
        }

        public bool startCaculator()
        {
            IWorkbook workbook = null;  //新建IWorkbook对象  
            string fileName = ExcelFile.FileName;
            //车次统计
            int allTrainsCount = 0;
            int allPsngerTrainsCount = 0;
            int stoppedTrainsCount = 0;
            int allTrainsInTimeTable = 0;
            List<CaculatorModel> _caculatorModelList = new List<CaculatorModel>();
            try
            {
                FileStream fileStream = new FileStream(ExcelFile.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                {
                    try
                    {
                        workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("时刻表文件出现损坏（或时刻表无效），请杀毒并更换备份文件-位于\\时刻表->backup内，点击确定打开）\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\时刻表\\");
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
                        System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + "\\时刻表\\");
                        return false;
                    }
                }

                if (workbook != null)
                {
                    string pLocalFilePath = ExcelFile.FileName.ToString();//要复制的文件路径
                    string pSaveFilePath = Application.StartupPath + "\\时刻表\\自动备份-" + ExcelFile.FileName.ToString().Split('\\')[ExcelFile.FileName.ToString().Split('\\').Length - 1];//指定存储的路径
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
                                        if (row.GetCell(j).ToString().Trim().Equals(model.trainNumber))
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
                                            if(!row.GetCell(j).ToString().Trim().Contains("0J") &&
                                                !row.GetCell(j).ToString().Trim().Contains("DJ"))
                                            {//0J和DJ不加入
                                                for (int p = j; p < row.LastCellNum; p++)
                                                {//找车次的右边 有没有股道数 找到股道数时 左右两边为该车次的图定时间
                                                    int res = 0;
                                                    if(row.GetCell(p)!= null)
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
                /*重新修改文件指定单元格样式*/
                FileStream fs1 = File.OpenWrite(ExcelFile.FileName);
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
                if(caculatorModel.Count == 0)
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
                return false ;
            }
        }

        private CaculatorModel addToCaculatorModel(string trainNumber, string shouldArriveTime, string shouldStartTime)
        {//判断是否符合赶点车条件，符合则加入model，返回成功/失败
            CaculatorModel _caculatorModel = new CaculatorModel();
            //筛掉18点以前的出库入库车
            int res = 0;
            if(trainNumber.Contains("0G") ||
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
                    if (trainNumber.ToCharArray()[trainNumber.Length - 1].Equals('0')||
                        trainNumber.ToCharArray()[trainNumber.Length-1].Equals('2') ||
                        trainNumber.ToCharArray()[trainNumber.Length-1].Equals('4') ||
                        trainNumber.ToCharArray()[trainNumber.Length-1].Equals('6') ||
                        trainNumber.ToCharArray()[trainNumber.Length-1].Equals('8'))
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
    }
}
