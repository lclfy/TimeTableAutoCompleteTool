using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.SS.UserModel;
//2003
using NPOI.HSSF.UserModel;
//2007以后
using NPOI.XSSF.UserModel;
using System.IO;

namespace TimeTableAutoCompleteTool
{
    public partial class Main : Form
    {
        private Boolean hasText = false;
        private Boolean hasFilePath = false;
        private List<CommandModel> commandModel;
        OpenFileDialog ExcelFile;

        public Main()
        {
            
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "TrainTimetableAutoCompleteTool-基于客调命令的时刻表自动完成工具";
            start_Btn.Enabled = false;
        }

        private void command_rTb_TextChanged(object sender, EventArgs e)
        {
            if(command_rTb.Text.Length != 0)
            {
                hasText = true;
                startBtnCheck();
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
            }
            else
            {
                start_Btn.Enabled = false;
            }
        }

        private void start_Btn_Click(object sender, EventArgs e)
        {
            analyseCommand();
            updateTimeTable();
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
                Boolean streamStatus = true;
                int trainType = 0;
                   command = AllCommand[i].Split('：');
                    if (command.Length > 1)
                    {
                        if(command.Length > 3)
                        {                //特殊数据
                                     //304、2018年02月11日，null-G4326/7：18：50分出库11日当天请令：临客线-G4326/7。
                                     //305、2018年02月11日，null - G4328 / 5：18：50分出库11日当天请令：临客线-G4328/5。
                            for (int r = 0; r < command.Length; r++)
                            {//从后往前开始找车次
                                if (command[command.Length - r - 1].Contains("G") ||
                                    command[command.Length - r - 1].Contains("D") ||
                                    command[command.Length - r - 1].Contains("C") ||
                                    command[command.Length - r - 1].Contains("J"))
                                {//有就用那个了…
                                command[1] = command[command.Length - r - 1];
                                break;
                                }
                            }
                        }
                    if (command.Length == 3)
                    //标注停运状态
                    {
                        streamStatus = !command[2].Contains("停");
                    }
                    if (command[1].Contains("高"))
                    {
                        trainType = 1;
                    }
                    else if (command[1].Contains("临"))
                    {
                        trainType = 2;
                    }
                    else if (command[1].Contains("周"))
                    {
                        trainType = 3;
                    }
                    //判断某车底中仅停运一部分的特殊停运车次
                    //示例：236、2018年02月12日，CRH380AL-2607：0D5699(停运)-D5700(停运)-0G75-G75(郑州东始发)。
                    if (command[1].Contains("停"))
                    {
                        AllTrainNumberInOneRaw = command[1].Split('-');
                        //如果部分停开-则停开与开行分开进行建模
                        for (int h = 0; h < AllTrainNumberInOneRaw.Length; h++)
                        {
                            if (AllTrainNumberInOneRaw[h].Contains("停"))
                            {//去中文添加
                                List<CommandModel> tempModels = trainModelAddFunc(System.Text.RegularExpressions.Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Split('-'),false,trainType);
                                foreach(CommandModel model in tempModels)
                                {
                                    AllModels.Add(model);
                                }
                            }
                            else
                            {
                                List<CommandModel> tempModels = trainModelAddFunc(System.Text.RegularExpressions.Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Split('-'), true, trainType);
                                foreach (CommandModel model in tempModels)
                                {
                                    AllModels.Add(model);
                                }
                            }
                        }
                    }
                    else
                    {
                        //把车次单独分离-去中文-去横杠
                        AllTrainNumberInOneRaw = System.Text.RegularExpressions.Regex.Replace(command[1], @"[\u4e00-\u9fa5]", "").Split('-');
                        //把车次添加模型
                        List<CommandModel> tempModels = trainModelAddFunc(AllTrainNumberInOneRaw, streamStatus, trainType);
                        foreach(CommandModel model in tempModels)
                        {
                            AllModels.Add(model);
                        }
                    }
                }
            }
            //测试用
            String commands = "";
            foreach (CommandModel model in AllModels)
            {
                String streamStatus = "";
                String trainType = "";
                if (model.streamStatus == true)
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
                commands = commands + model.trainNumber + "-" + streamStatus + "-" + trainType + "\r\n";
            }
            testTB.Text = commands;
            commandModel = AllModels;
            
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
            if (standardCommand.Contains("（"))
                standardCommand = standardCommand.Replace("（", "");
            if (standardCommand.Contains("）"))
                standardCommand = standardCommand.Replace("）", "");
            if (standardCommand.Contains("d"))
                standardCommand = standardCommand.Replace("d", "D");
            if (standardCommand.Contains("g"))
                standardCommand = standardCommand.Replace("g", "G");
            if (standardCommand.Contains("c"))
                standardCommand = standardCommand.Replace("c", "C");
            if (standardCommand.Contains("j"))
                standardCommand = standardCommand.Replace("j", "J");
            if (standardCommand.Contains("CRH"))
                standardCommand = standardCommand.Replace("CRH", "");
            if (standardCommand.Contains("；"))
                standardCommand = standardCommand.Replace("；", "");
            return standardCommand;
        }

        private List<CommandModel> trainModelAddFunc(String[] AllTrainNumberInOneRaw, Boolean streamStatus,int trainType)
        {//建立车次模型-通用方法
            //处理单程双车次车辆
            List<CommandModel> AllModels = new List<CommandModel>();
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


        //以下为使用NPOI进行Excel操作
        private void SelectPath()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();   //显示选择文件对话框 
            openFileDialog1.Filter = "Excel 2007 文件 (*.xlsx)|*.xlsx|Excel 2003 文件 (*.xls)|*.xls";
            //openFileDialog1.Filter = "Excel 2003 文件 (*.xls)|*.xls";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.filePath_lbl.Text = openFileDialog1.FileName;     //显示文件路径 
                hasFilePath = true;
                ExcelFile = openFileDialog1;
                startBtnCheck();
            }
        }

        private void updateTimeTable()
        {
            IWorkbook workbook = null;  //新建IWorkbook对象  
            string fileName = ExcelFile.FileName;
            try
            {
                FileStream fileStream = new FileStream(ExcelFile.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                {
                    workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                }
                else if (fileName.IndexOf(".xls") > 0) // 2003版本  
                {
                    workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
                }
                
                //两种表格样式
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
                                        //去中文后再找
                                        row.GetCell(j).CellStyle = removeColors;
                                        row.GetCell(j).SetCellValue(System.Text.RegularExpressions.Regex.Replace(row.GetCell(j).ToString(), @"[\u4e00-\u9fa5]", ""));
                                    }
                                    else
                                    {
                                        //这个格子不是要找的
                                        continue;
                                    }
                                    foreach (CommandModel model in commandModel)
                                    {//根据客调命令刷单元格颜色
                                        if (row.GetCell(j).ToString().Trim().Equals(model.trainNumber))
                                        {
                                            if (model.streamStatus)
                                            {
                                                row.GetCell(j).CellStyle = normalTrainStyle;
                                            }
                                            else
                                            {
                                                row.GetCell(j).CellStyle = stoppedTrainStyle;
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
                MessageBox.Show("该文件正在使用中，请关闭后重试","提示",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }
    }
}
