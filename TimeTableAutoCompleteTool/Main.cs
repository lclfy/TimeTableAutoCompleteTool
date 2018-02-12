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
            String[] AllCommand = command_rTb.Text.ToString().Split('。');
            List<CommandModel> AllModels = new List<CommandModel>();
            for(int i = 0; i < AllCommand.Length; i++)
            {
                //若有英文冒号 则分割英文冒号，若有中文冒号，则分割中文冒号
                String[] command;
                String[] AllTrainNumberInOneRaw;
                Boolean streamStatus = true;
                if (AllCommand[i].Contains("：") &&
                    !AllCommand[i].Contains(":"))
                {
                   command = AllCommand[i].Split('：');
                    if (command.Length == 2 ||
                        command.Length == 3)
                    {
                        //把车次单独分离-去中文-去横杠
                        AllTrainNumberInOneRaw = System.Text.RegularExpressions.Regex.Replace(command[1], @"[\u4e00-\u9fa5]", "").Split('-');
                        if(command.Length == 3)
                            //标注停运状态
                        {
                            streamStatus = !command[2].Contains("停");
                        }
                        for(int k = 0; k < AllTrainNumberInOneRaw.Length; k++)
                        {
                            //处理单程双车次车辆
                            if (AllTrainNumberInOneRaw[k].Contains("/"))
                            {
                                String[] trainWithDoubleNumber = AllTrainNumberInOneRaw[k].Split('/');
                                //先添加第一个车次
                                CommandModel m1 = new CommandModel();
                                m1.trainNumber = trainWithDoubleNumber[0];
                                m1.streamStatus = streamStatus;
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
                                        m2.trainNumber = secondTrainWord;
                                        m2.streamStatus = streamStatus;
                                        AllModels.Add(m2);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                CommandModel model = new CommandModel();
                                model.trainNumber = AllTrainNumberInOneRaw[k];
                                model.streamStatus = streamStatus;
                                AllModels.Add(model);
                            }
                        }
   
                    }

                }else if(!AllCommand[i].Contains("：") &&
                    AllCommand[i].Contains(":"))
                {
                    command = AllCommand[i].Split(':');

                }
                else if(AllCommand[i].Contains("：") &&
                    AllCommand[i].Contains(":"))
                {

                }

                
            }
        }

        private void updateTimeTable()
        {
      
        }
    }
}
