using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CCWin;
using System.Windows.Forms;

namespace TimeTableAutoCompleteTool
{
    public partial class DataAnalyse : Skin_Mac
    {
        public List<CommandModel> allCommandModels;
        public string operationString;
        int timerCount = 0;
        //窗口置于最前
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow(); //获得本窗体的句柄
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体
                                                                   //定义变量,句柄类型
        public IntPtr Handle1;
        public DataAnalyse(List<CommandModel> _allCm, string _operationString)
        {
            if (_allCm != null)
            {
                allCommandModels = _allCm;
            }
            if (_operationString != null)
            {
                operationString = _operationString;
            }
            InitializeComponent();
        }

        private void DataAnalyse_Load(object sender, EventArgs e)
        {
            init();
            Handle1 = this.Handle;
            timer1.Enabled = true;
        }

        private void init()
        {
            timerCount = 0;
            start_cb.Checked = true;
            stop_cb.Checked = true;
            normal_cb.Checked = true;
            temp_cb.Checked = true;
            psngerTrain_cb.Checked = true;
            nonPsngerTrain_cb.Checked = true;
            start_cb.Checked = true;
            checked_cb.Checked = true;
            nonChecked_cb.Checked = true;
            getDataStatistics();
            AllTrainsInCommand_lbl.Text = allCommandModels.Count.ToString();

            getSelectedTrains(false, start_cb.Checked, stop_cb.Checked, normal_cb.Checked, temp_cb.Checked, psngerTrain_cb.Checked, nonPsngerTrain_cb.Checked, checked_cb.Checked, nonChecked_cb.Checked);
        }

        private void RefreshList(List<CommandModel> _tempCM)
        {
            currentShow_lbl.Text = _tempCM.Count.ToString();
            data_lv.Items.Clear();
            data_lv.BeginUpdate();
            for (int j = 0; j < _tempCM.Count; j++)
            {
                ListViewItem _lvi;
                if (_tempCM[j].trainIndex != null && _tempCM[j].trainIndex.Length != 0)
                {
                    _lvi = new ListViewItem(_tempCM[j].trainIndex);
                }
                else
                {
                    _lvi = new ListViewItem("—");
                }
                if (_tempCM[j].MatchedWithTimeTable)
                {
                    _lvi.SubItems.Add("√是");
                }
                else
                {
                    _lvi.SubItems.Add("×否");
                }
                if (_tempCM[j].trainNumber != null && _tempCM[j].trainNumber.Length != 0)
                {
                    _lvi.SubItems.Add(_tempCM[j].trainNumber);
                }
                else
                {
                    _lvi.SubItems.Add("—");
                }

                if (_tempCM[j].secondTrainNumber != null && _tempCM[j].secondTrainNumber.Length != 0)
                {
                    if (_tempCM[j].secondTrainNumber.Trim().Contains("null"))
                    {
                        _lvi.SubItems.Add("无");
                    }
                    else
                    {
                        _lvi.SubItems.Add(_tempCM[j].secondTrainNumber);
                    }
                }
                else
                {
                    _lvi.SubItems.Add(" ");
                }
                switch (_tempCM[j].trainType)
                {
                    case 0:
                        _lvi.SubItems.Add("普通");
                        break;
                    case 1:
                        _lvi.SubItems.Add("高峰");
                        break;
                    case 2:
                        _lvi.SubItems.Add("临客");
                        break;
                    case 3:
                        _lvi.SubItems.Add("周末");
                        break;
                    case 4:
                        _lvi.SubItems.Add("加开");
                        break;
                }
                if (_tempCM[j].streamStatus != 0)
                    _lvi.SubItems.Add("开");
                else
                    _lvi.SubItems.Add("停");
                if (_tempCM[j].psngerTrain)
                {
                    _lvi.SubItems.Add("载客");
                }
                else
                {
                    _lvi.SubItems.Add("其他");
                }

                if (_tempCM[j].trainModel != null && _tempCM[j].trainModel.Length != 0)
                {
                    _lvi.SubItems.Add(_tempCM[j].trainModel);
                }
                else
                {
                    _lvi.SubItems.Add(" ");
                }

                if (_tempCM[j].trainId != null && _tempCM[j].trainId.Length != 0)
                {
                    _lvi.SubItems.Add(_tempCM[j].trainId);
                }
                else
                {
                    _lvi.SubItems.Add(" ");
                }

                data_lv.Items.Add(_lvi);

            }
            data_lv.EndUpdate();
        }

        //数据统计字符串
        private void getDataStatistics()
        {
            string statisticsText = "郑州东（本站）本日实际开行旅客列车" + getSelectedTrains(true, true, false, true, true, true, false, true, false).Count +
                "列（其中临客" + getSelectedTrains(true, true, false, false, true, true, false, true, false).Count + "列），开行其他列车" + getSelectedTrains(true, true, false, true, true, false, true, true, false).Count +
                "列（其中临客" + getSelectedTrains(true, true, false, false, true, false, true, true, false).Count + "列）;停运旅客列车" + getSelectedTrains(true, false, true, true, true, true, false, true, false).Count +
                "列（其中临客" + getSelectedTrains(true, false, true, false, true, true, false, true, false).Count + "列）;停运其他列车" + getSelectedTrains(true, false, true, true, true, false, true, true, false).Count +
                "列（其中临客" + getSelectedTrains(true, false, true, false, true, false, true, true, false).Count + "列。\n" + operationString + "\n" +
                "未在图列车（右侧显示）" + getSelectedTrains(true, true, true, true, true, true, true, false, true).Count + "列";

            string unrecognazedTrains = "";
            int count = 0;
            foreach (CommandModel _cm in getSelectedTrains(true, true, true, true, true, true, true, false, true))
            {
                count++;
                if (_cm.secondTrainNumber.Contains("null"))
                {
                    unrecognazedTrains = unrecognazedTrains + "第" + _cm.trainIndex + "条-" + _cm.trainNumber;
                }
                else
                {
                    unrecognazedTrains = unrecognazedTrains + "第" + _cm.trainIndex + "条-" + _cm.trainNumber + "-" + _cm.secondTrainNumber;
                }
                if(_cm.trainType == 4)
                {
                    unrecognazedTrains = unrecognazedTrains + "-标注加开\n";
                }
                else
                {
                    unrecognazedTrains = unrecognazedTrains + "\n";
                }

            }
            unrecognazedTrains = unrecognazedTrains + "\n共" + count + "列";
            operationChanged_rtb.Text = statisticsText;
            unrecognizedTrain_rtb.Text = unrecognazedTrains;
        }

        private List<CommandModel> getSelectedTrains(bool init, bool trainOperationTrue, bool trainOperationFalse, bool normalTrain, bool tempTrain, bool psnger, bool nonPsnger, bool hasChecked, bool nonChecked)
        {
            List<CommandModel> _tempCM = new List<CommandModel>();
            //条件筛选
            for (int i = 0; i < allCommandModels.Count; i++)
            {
                //先确定最外层-筛选-未筛选
                if ((allCommandModels[i].MatchedWithTimeTable == true &&
                    hasChecked == true))
                {//再确定开/停
                    if ((allCommandModels[i].streamStatus != 0 && trainOperationTrue == true) ||
                    (allCommandModels[i].streamStatus == 0 && trainOperationFalse == true))
                    //确定是否为普通列车
                    {
                        if ((allCommandModels[i].trainType == 0 && normalTrain == true) ||
                    (allCommandModels[i].trainType != 0 && tempTrain == true))
                        {//最后确定是否载客
                            if ((allCommandModels[i].psngerTrain == true && psnger == true) ||
                                (allCommandModels[i].psngerTrain == false && nonPsnger == true))
                            {
                                _tempCM.Add(allCommandModels[i]);
                            }
                        }
                    }
                }
                else if (allCommandModels[i].MatchedWithTimeTable == false &&
                    nonChecked == true)
                {//未筛选的可能有上下行未知的情况
                    if ((allCommandModels[i].streamStatus != 0 && trainOperationTrue == true) ||
                    (allCommandModels[i].streamStatus == 0 && trainOperationFalse == true))
                    {
                        if ((allCommandModels[i].trainType == 0 && normalTrain == true) ||
                   (allCommandModels[i].trainType != 0 && tempTrain == true))
                        {//最后确定是否载客
                            if ((allCommandModels[i].psngerTrain == true && psnger == true) ||
                            (allCommandModels[i].psngerTrain == false && nonPsnger == true))
                            {
                                _tempCM.Add(allCommandModels[i]);
                            }
                        }
                    }
                }
            }
            if (!init)
            {
                RefreshList(_tempCM);
            }
            return _tempCM;
        }

        private void searchList(string text)
        {
            List<CommandModel> _tempCM = new List<CommandModel>();
            for (int i = 0; i < allCommandModels.Count; i++)
            {
                if (allCommandModels[i].trainNumber.Contains(text) ||
                    allCommandModels[i].secondTrainNumber.Contains(text))
                {
                    _tempCM.Add(allCommandModels[i]);
                }
            }
            RefreshList(_tempCM);
        }
        private void checkedChanged()
        {

            getSelectedTrains(false, start_cb.Checked, stop_cb.Checked, normal_cb.Checked, temp_cb.Checked, psngerTrain_cb.Checked, nonPsngerTrain_cb.Checked, checked_cb.Checked, nonChecked_cb.Checked);
        }

        private void up_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void down_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void psngerTrain_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void nonPsngerTrain_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void checked_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void nonChecked_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void search_tb_TextChanged(object sender, EventArgs e)
        {
            if (search_tb.Text.ToString().Length != 0)
            {
                searchList(search_tb.Text.ToString());
            }
            else
            {
                checkedChanged();
            }
        }

        private void stop_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void temp_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Handle1 != GetForegroundWindow()) //持续使该窗体置为最前,屏蔽该行则单次置顶
            {
                SetForegroundWindow(Handle1);

                timerCount++;
                if(timerCount > 20)
                {
                    timer1.Stop();//此处可以关掉定时器，则实现单次置顶
                }
            }
        }
    }
}
