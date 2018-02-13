using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTableAutoCompleteTool
{
    public partial class Main : Form
    {
        private Boolean hasText = false;
        private Boolean hasFilePath = false;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
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

        private void SelectPath()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();   //显示选择文件对话框 
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.filePath_lbl.Text = openFileDialog1.FileName;     //显示文件路径 
                hasFilePath = true;
                startBtnCheck();
            }
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
        }

        private void analyseCommand()
        {   //分析客调命令
            String[] AllCommand = removeUnuseableWord().Split('。');
            List<CommandModel> AllModels = new List<CommandModel>();
            //所有英文字符转中文字符

            for (int i = 0; i < AllCommand.Length; i++)
            {
                String[] command;
                String[] AllTrainNumberInOneRaw;
                Boolean streamStatus = true;
                int trainType = 0;
                   command = AllCommand[i].Split('：');
                    if (command.Length == 2 ||
                        command.Length == 3)
                    {
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

        private void updateTimeTable()
        {
      
        }
    }
}
