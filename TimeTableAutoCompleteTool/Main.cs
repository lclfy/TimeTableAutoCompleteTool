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

        }

        private void updateTimeTable()
        {

        }
    }
}
