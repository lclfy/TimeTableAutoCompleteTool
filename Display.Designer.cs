namespace TimeTableAutoCompleteTool
{
    partial class Display
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.modifyTrackLine_btn = new System.Windows.Forms.Button();
            this.modifyTrackPoint_btn = new System.Windows.Forms.Button();
            this.modifyPowerSupplyModel_btn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.save_btn = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.showPoint_btn = new System.Windows.Forms.Button();
            this.buttons_pnl = new System.Windows.Forms.Panel();
            this.setting_btn = new System.Windows.Forms.Button();
            this.title_lbl = new System.Windows.Forms.Label();
            this.title_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.modifySignal_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.startTrackNum_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.stopTrackNum_tb = new System.Windows.Forms.TextBox();
            this.zoomIn_btn = new System.Windows.Forms.Button();
            this.zoomOut_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.resetPlace_btn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.mousePoint_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // modifyTrackLine_btn
            // 
            this.modifyTrackLine_btn.Location = new System.Drawing.Point(14, 354);
            this.modifyTrackLine_btn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.modifyTrackLine_btn.Name = "modifyTrackLine_btn";
            this.modifyTrackLine_btn.Size = new System.Drawing.Size(80, 23);
            this.modifyTrackLine_btn.TabIndex = 0;
            this.modifyTrackLine_btn.Text = "轨道";
            this.modifyTrackLine_btn.UseVisualStyleBackColor = true;
            this.modifyTrackLine_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // modifyTrackPoint_btn
            // 
            this.modifyTrackPoint_btn.Location = new System.Drawing.Point(14, 325);
            this.modifyTrackPoint_btn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.modifyTrackPoint_btn.Name = "modifyTrackPoint_btn";
            this.modifyTrackPoint_btn.Size = new System.Drawing.Size(80, 23);
            this.modifyTrackPoint_btn.TabIndex = 1;
            this.modifyTrackPoint_btn.Text = "道岔";
            this.modifyTrackPoint_btn.UseVisualStyleBackColor = true;
            this.modifyTrackPoint_btn.Click += new System.EventHandler(this.button2_Click);
            // 
            // modifyPowerSupplyModel_btn
            // 
            this.modifyPowerSupplyModel_btn.Location = new System.Drawing.Point(14, 267);
            this.modifyPowerSupplyModel_btn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.modifyPowerSupplyModel_btn.Name = "modifyPowerSupplyModel_btn";
            this.modifyPowerSupplyModel_btn.Size = new System.Drawing.Size(80, 23);
            this.modifyPowerSupplyModel_btn.TabIndex = 2;
            this.modifyPowerSupplyModel_btn.Text = "供电臂";
            this.modifyPowerSupplyModel_btn.UseVisualStyleBackColor = true;
            this.modifyPowerSupplyModel_btn.Click += new System.EventHandler(this.button3_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(14, 210);
            this.save_btn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(80, 23);
            this.save_btn.TabIndex = 3;
            this.save_btn.Text = "保存";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // showPoint_btn
            // 
            this.showPoint_btn.Location = new System.Drawing.Point(14, 238);
            this.showPoint_btn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.showPoint_btn.Name = "showPoint_btn";
            this.showPoint_btn.Size = new System.Drawing.Size(80, 23);
            this.showPoint_btn.TabIndex = 4;
            this.showPoint_btn.Text = "显/隐坐标";
            this.showPoint_btn.UseVisualStyleBackColor = true;
            this.showPoint_btn.Click += new System.EventHandler(this.showPoint_btn_Click);
            // 
            // buttons_pnl
            // 
            this.buttons_pnl.Location = new System.Drawing.Point(104, 768);
            this.buttons_pnl.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttons_pnl.Name = "buttons_pnl";
            this.buttons_pnl.Size = new System.Drawing.Size(1702, 172);
            this.buttons_pnl.TabIndex = 5;
            // 
            // setting_btn
            // 
            this.setting_btn.Location = new System.Drawing.Point(14, 383);
            this.setting_btn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.setting_btn.Name = "setting_btn";
            this.setting_btn.Size = new System.Drawing.Size(80, 23);
            this.setting_btn.TabIndex = 6;
            this.setting_btn.Text = "编辑模式";
            this.setting_btn.UseVisualStyleBackColor = true;
            this.setting_btn.Click += new System.EventHandler(this.button4_Click);
            // 
            // title_lbl
            // 
            this.title_lbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.title_lbl.AutoSize = true;
            this.title_lbl.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.title_lbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.title_lbl.Location = new System.Drawing.Point(608, 17);
            this.title_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.title_lbl.Name = "title_lbl";
            this.title_lbl.Size = new System.Drawing.Size(0, 75);
            this.title_lbl.TabIndex = 7;
            this.title_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // title_tb
            // 
            this.title_tb.Font = new System.Drawing.Font("微软雅黑", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.title_tb.Location = new System.Drawing.Point(14, 154);
            this.title_tb.Margin = new System.Windows.Forms.Padding(2);
            this.title_tb.Name = "title_tb";
            this.title_tb.Size = new System.Drawing.Size(81, 23);
            this.title_tb.TabIndex = 8;
            this.title_tb.TextChanged += new System.EventHandler(this.title_tb_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(10, 137);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "标题";
            // 
            // modifySignal_btn
            // 
            this.modifySignal_btn.Location = new System.Drawing.Point(14, 296);
            this.modifySignal_btn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.modifySignal_btn.Name = "modifySignal_btn";
            this.modifySignal_btn.Size = new System.Drawing.Size(80, 23);
            this.modifySignal_btn.TabIndex = 10;
            this.modifySignal_btn.Text = "信号机";
            this.modifySignal_btn.UseVisualStyleBackColor = true;
            this.modifySignal_btn.Click += new System.EventHandler(this.modifySignal_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(10, 171);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "站台范围";
            // 
            // startTrackNum_tb
            // 
            this.startTrackNum_tb.Font = new System.Drawing.Font("微软雅黑", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startTrackNum_tb.Location = new System.Drawing.Point(15, 190);
            this.startTrackNum_tb.Margin = new System.Windows.Forms.Padding(2);
            this.startTrackNum_tb.Name = "startTrackNum_tb";
            this.startTrackNum_tb.Size = new System.Drawing.Size(30, 23);
            this.startTrackNum_tb.TabIndex = 12;
            this.startTrackNum_tb.Text = "0";
            this.startTrackNum_tb.TextChanged += new System.EventHandler(this.startTrackNum_tb_TextChanged);
            this.startTrackNum_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.startTrackNum_tb_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(48, 190);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "-";
            // 
            // stopTrackNum_tb
            // 
            this.stopTrackNum_tb.Font = new System.Drawing.Font("微软雅黑", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stopTrackNum_tb.Location = new System.Drawing.Point(63, 190);
            this.stopTrackNum_tb.Margin = new System.Windows.Forms.Padding(2);
            this.stopTrackNum_tb.Name = "stopTrackNum_tb";
            this.stopTrackNum_tb.Size = new System.Drawing.Size(32, 23);
            this.stopTrackNum_tb.TabIndex = 14;
            this.stopTrackNum_tb.Text = "0";
            this.stopTrackNum_tb.TextChanged += new System.EventHandler(this.stopTrackNum_tb_TextChanged);
            this.stopTrackNum_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.stopTrackNum_tb_KeyPress);
            // 
            // zoomIn_btn
            // 
            this.zoomIn_btn.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zoomIn_btn.Location = new System.Drawing.Point(14, 49);
            this.zoomIn_btn.Name = "zoomIn_btn";
            this.zoomIn_btn.Size = new System.Drawing.Size(40, 39);
            this.zoomIn_btn.TabIndex = 15;
            this.zoomIn_btn.Text = "+";
            this.zoomIn_btn.UseVisualStyleBackColor = true;
            this.zoomIn_btn.Click += new System.EventHandler(this.zoomIn_btn_Click);
            // 
            // zoomOut_btn
            // 
            this.zoomOut_btn.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zoomOut_btn.Location = new System.Drawing.Point(60, 49);
            this.zoomOut_btn.Name = "zoomOut_btn";
            this.zoomOut_btn.Size = new System.Drawing.Size(40, 39);
            this.zoomOut_btn.TabIndex = 16;
            this.zoomOut_btn.Text = "-";
            this.zoomOut_btn.UseVisualStyleBackColor = true;
            this.zoomOut_btn.Click += new System.EventHandler(this.zoomOut_btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(12, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 17;
            this.label4.Text = "放大/缩小";
            // 
            // resetPlace_btn
            // 
            this.resetPlace_btn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.resetPlace_btn.Location = new System.Drawing.Point(115, 49);
            this.resetPlace_btn.Name = "resetPlace_btn";
            this.resetPlace_btn.Size = new System.Drawing.Size(77, 39);
            this.resetPlace_btn.TabIndex = 18;
            this.resetPlace_btn.Text = "重置位置";
            this.resetPlace_btn.UseVisualStyleBackColor = true;
            this.resetPlace_btn.Click += new System.EventHandler(this.resetPlace_btn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "label5";
            // 
            // mousePoint_lbl
            // 
            this.mousePoint_lbl.AutoSize = true;
            this.mousePoint_lbl.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mousePoint_lbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mousePoint_lbl.Location = new System.Drawing.Point(10, 100);
            this.mousePoint_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mousePoint_lbl.Name = "mousePoint_lbl";
            this.mousePoint_lbl.Size = new System.Drawing.Size(49, 14);
            this.mousePoint_lbl.TabIndex = 20;
            this.mousePoint_lbl.Text = "当前：";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(842, 427);
            this.Controls.Add(this.mousePoint_lbl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.resetPlace_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.zoomOut_btn);
            this.Controls.Add(this.zoomIn_btn);
            this.Controls.Add(this.stopTrackNum_tb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.startTrackNum_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.modifySignal_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.title_tb);
            this.Controls.Add(this.title_lbl);
            this.Controls.Add(this.setting_btn);
            this.Controls.Add(this.buttons_pnl);
            this.Controls.Add(this.showPoint_btn);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.modifyPowerSupplyModel_btn);
            this.Controls.Add(this.modifyTrackPoint_btn);
            this.Controls.Add(this.modifyTrackLine_btn);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Main_Scroll);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Main_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_MouseUp);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button modifyTrackLine_btn;
        private System.Windows.Forms.Button modifyTrackPoint_btn;
        private System.Windows.Forms.Button modifyPowerSupplyModel_btn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button showPoint_btn;
        private System.Windows.Forms.Panel buttons_pnl;
        private System.Windows.Forms.Button setting_btn;
        private System.Windows.Forms.Label title_lbl;
        private System.Windows.Forms.TextBox title_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button modifySignal_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox startTrackNum_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox stopTrackNum_tb;
        private System.Windows.Forms.Button zoomIn_btn;
        private System.Windows.Forms.Button zoomOut_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button resetPlace_btn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label mousePoint_lbl;
    }
}

