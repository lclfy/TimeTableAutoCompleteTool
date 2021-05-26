using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;

namespace TimeTableAutoCompleteTool
{
    public partial class Welcome : Skin_Mac
    {
        Main main;
        public Welcome(Main _main)
        {
            main = _main;
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            main.modeSelect = 1;
            main.ModeSelect();
            this.Hide();
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            main.modeSelect = 2;
            main.ModeSelect();
            this.Hide();
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {
            main.modeSelect = 3;
            main.ModeSelect();
            this.Hide();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(main.modeSelect == 0)
            {
                main.modeSelect = 1;
                main.ModeSelect();
            }
            base.OnClosing(e);
        }
    }
}
