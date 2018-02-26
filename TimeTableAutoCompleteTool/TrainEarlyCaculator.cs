using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeTableAutoCompleteTool
{
    public partial class TrainEarlyCaculator : Form
    {
        List<CaculatorModel> caculatorModelList;
        public TrainEarlyCaculator(List<CaculatorModel> _caculatorModel)
        {
            InitializeComponent();
            caculatorModelList = _caculatorModel;
        }

        private void TrainEarlyCaculator_Load(object sender, EventArgs e)
        {
            trainsInformation_lv.View = View.Details;
            string[] informationTitle = new string[] { "序号", "车次", "图定到", "实际到", "图定发", "实际发", "赶点" };
            this.trainsInformation_lv.BeginUpdate();
            for (int i = 0; i < informationTitle.Count(); i++)
            {
                ColumnHeader ch = new ColumnHeader();
                ch.Text = informationTitle[i];   //设置列标题 
                if (i == 0)
                {
                    ch.Width = 40;
                }
                else
                {
                    ch.Width = 50;
                }
                
                this.trainsInformation_lv.Columns.Add(ch);    //将列头添加到ListView控件。
            }
            ListViewItem lvi = new ListViewItem();
            lvi.SubItems[0].Text = "100";
            lvi.SubItems.Add("0G1830");
            lvi.SubItems.Add("22:22");
            lvi.SubItems.Add("22:50");
            lvi.SubItems.Add("22:30");
            lvi.SubItems.Add("22:55");
            lvi.SubItems.Add("3");
            this.trainsInformation_lv.Items.Add(lvi);

            this.trainsInformation_lv.EndUpdate();

        }

        private void ActuallyArriveTime_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ActuallyArriveTime_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void ActuallyStartTime_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ActuallyStartTime_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void trainsInformation_lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
