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
        public DataAnalyse(List<CommandModel> _allCm)
        {
            if (_allCm != null)
            {
                allCommandModels = _allCm;
            }
            InitializeComponent();
        }

        private void DataAnalyse_Load(object sender, EventArgs e)
        {
            init();
        }

        private void init()
        {
            start_cb.Checked = true;
            stop_cb.Checked = true;
            normal_cb.Checked = true;
            temp_cb.Checked = true;
            psngerTrain_cb.Checked = true;
            nonPsngerTrain_cb.Checked = true;
            start_cb.Checked = true;
            checked_cb.Checked = true;
            nonChecked_cb.Checked = true;

            AllTrainsInCommand_lbl.Text = allCommandModels.Count.ToString();

            getSelectedTrains(start_cb.Checked,stop_cb.Checked, normal_cb.Checked,temp_cb.Checked, psngerTrain_cb.Checked, nonPsngerTrain_cb.Checked, checked_cb.Checked, nonChecked_cb.Checked);
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
                if(_tempCM[j].streamStatus != 0)
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

        private void getSelectedTrains(bool trainOperationTrue, bool trainOperationFalse, bool normalTrain, bool tempTrain, bool psnger, bool nonPsnger, bool hasChecked, bool nonChecked)
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
                        if((allCommandModels[i].trainType == 0 && normalTrain == true) ||
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
            RefreshList(_tempCM);

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
            
            getSelectedTrains(start_cb.Checked, stop_cb.Checked, normal_cb.Checked, temp_cb.Checked, psngerTrain_cb.Checked, nonPsngerTrain_cb.Checked, checked_cb.Checked, nonChecked_cb.Checked);
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
    }
}
