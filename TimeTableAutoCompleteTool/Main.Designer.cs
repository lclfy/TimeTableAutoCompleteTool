namespace TimeTableAutoCompleteTool
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.command_rTb = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.importTimeTable_Btn = new System.Windows.Forms.Button();
            this.filePathLBL = new System.Windows.Forms.Label();
            this.filePath_lbl = new System.Windows.Forms.Label();
            this.start_Btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.outputTB = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.wrongTB = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.buildLBL = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TrainEarlyCaculator_Btn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AllTrainsInTimeTableLBL = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.stoppedTrainsCountLBL = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.AllPsngerTrainsCountLBL = new System.Windows.Forms.Label();
            this.AllTrainsCountLBL = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(29, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "1.将当日开行车次客调命令复制于下方文本框";
            // 
            // command_rTb
            // 
            this.command_rTb.Location = new System.Drawing.Point(9, 32);
            this.command_rTb.Name = "command_rTb";
            this.command_rTb.Size = new System.Drawing.Size(465, 274);
            this.command_rTb.TabIndex = 1;
            this.command_rTb.Text = "";
            this.command_rTb.TextChanged += new System.EventHandler(this.command_rTb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 316);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "2.选择时刻表文件/基本图文件";
            // 
            // importTimeTable_Btn
            // 
            this.importTimeTable_Btn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.importTimeTable_Btn.Location = new System.Drawing.Point(382, 312);
            this.importTimeTable_Btn.Name = "importTimeTable_Btn";
            this.importTimeTable_Btn.Size = new System.Drawing.Size(95, 32);
            this.importTimeTable_Btn.TabIndex = 3;
            this.importTimeTable_Btn.Text = "导入";
            this.importTimeTable_Btn.UseVisualStyleBackColor = true;
            this.importTimeTable_Btn.Click += new System.EventHandler(this.importTimeTable_Btn_Click);
            // 
            // filePathLBL
            // 
            this.filePathLBL.AutoSize = true;
            this.filePathLBL.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePathLBL.Location = new System.Drawing.Point(10, 353);
            this.filePathLBL.Name = "filePathLBL";
            this.filePathLBL.Size = new System.Drawing.Size(56, 17);
            this.filePathLBL.TabIndex = 4;
            this.filePathLBL.Text = "已选择：";
            // 
            // filePath_lbl
            // 
            this.filePath_lbl.AutoSize = true;
            this.filePath_lbl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePath_lbl.Location = new System.Drawing.Point(105, 396);
            this.filePath_lbl.Name = "filePath_lbl";
            this.filePath_lbl.Size = new System.Drawing.Size(0, 17);
            this.filePath_lbl.TabIndex = 5;
            // 
            // start_Btn
            // 
            this.start_Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start_Btn.Location = new System.Drawing.Point(9, 379);
            this.start_Btn.Name = "start_Btn";
            this.start_Btn.Size = new System.Drawing.Size(465, 44);
            this.start_Btn.TabIndex = 6;
            this.start_Btn.Text = "生成时刻表/班计划";
            this.start_Btn.UseVisualStyleBackColor = true;
            this.start_Btn.Click += new System.EventHandler(this.start_Btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(34, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(399, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "示例：218、2018年02月13日，CRH380BG-5708+5811：G1294/5/4。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(352, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "（请注意客调令其他部分中的列车）";
            // 
            // outputTB
            // 
            this.outputTB.Location = new System.Drawing.Point(533, 101);
            this.outputTB.Name = "outputTB";
            this.outputTB.ReadOnly = true;
            this.outputTB.Size = new System.Drawing.Size(210, 283);
            this.outputTB.TabIndex = 9;
            this.outputTB.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(161, 367);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 17);
            this.label6.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label7.Location = new System.Drawing.Point(21, 513);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(570, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "绿色为开行，红色为停开，白色为调令未含车次，黄色为次日接入车次。高峰/临客/周末在车次前含有标注";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(529, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(186, 21);
            this.label8.TabIndex = 11;
            this.label8.Text = "客调命令中提取出的车次";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label9.Location = new System.Drawing.Point(677, 513);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(233, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "意见反馈/Bug反馈请联系运转车间-罗思聪";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(745, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(212, 21);
            this.label10.TabIndex = 13;
            this.label10.Text = "部分未识别车次(检查对应行)";
            // 
            // wrongTB
            // 
            this.wrongTB.Location = new System.Drawing.Point(749, 101);
            this.wrongTB.Name = "wrongTB";
            this.wrongTB.ReadOnly = true;
            this.wrongTB.Size = new System.Drawing.Size(210, 283);
            this.wrongTB.TabIndex = 14;
            this.wrongTB.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label11.Location = new System.Drawing.Point(769, 498);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 17);
            this.label11.TabIndex = 15;
            // 
            // buildLBL
            // 
            this.buildLBL.AutoSize = true;
            this.buildLBL.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buildLBL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buildLBL.Location = new System.Drawing.Point(941, 513);
            this.buildLBL.Name = "buildLBL";
            this.buildLBL.Size = new System.Drawing.Size(56, 17);
            this.buildLBL.TabIndex = 16;
            this.buildLBL.Text = "修订内容";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.importTimeTable_Btn);
            this.groupBox1.Controls.Add(this.filePathLBL);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.start_Btn);
            this.groupBox1.Controls.Add(this.command_rTb);
            this.groupBox1.Location = new System.Drawing.Point(24, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 435);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // TrainEarlyCaculator_Btn
            // 
            this.TrainEarlyCaculator_Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TrainEarlyCaculator_Btn.Location = new System.Drawing.Point(-1, -2);
            this.TrainEarlyCaculator_Btn.Name = "TrainEarlyCaculator_Btn";
            this.TrainEarlyCaculator_Btn.Size = new System.Drawing.Size(10, 10);
            this.TrainEarlyCaculator_Btn.TabIndex = 21;
            this.TrainEarlyCaculator_Btn.UseVisualStyleBackColor = true;
            this.TrainEarlyCaculator_Btn.Click += new System.EventHandler(this.TrainEarlyCaculator_Btn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(514, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 435);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AllTrainsInTimeTableLBL);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.stoppedTrainsCountLBL);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.AllPsngerTrainsCountLBL);
            this.groupBox3.Controls.Add(this.AllTrainsCountLBL);
            this.groupBox3.Location = new System.Drawing.Point(19, 321);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(426, 105);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            // 
            // AllTrainsInTimeTableLBL
            // 
            this.AllTrainsInTimeTableLBL.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AllTrainsInTimeTableLBL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.AllTrainsInTimeTableLBL.Location = new System.Drawing.Point(142, 18);
            this.AllTrainsInTimeTableLBL.Name = "AllTrainsInTimeTableLBL";
            this.AllTrainsInTimeTableLBL.Size = new System.Drawing.Size(73, 31);
            this.AllTrainsInTimeTableLBL.TabIndex = 28;
            this.AllTrainsInTimeTableLBL.Text = "0";
            this.AllTrainsInTimeTableLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(41, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 17);
            this.label14.TabIndex = 27;
            this.label14.Text = "时刻表内车次数";
            // 
            // stoppedTrainsCountLBL
            // 
            this.stoppedTrainsCountLBL.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stoppedTrainsCountLBL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.stoppedTrainsCountLBL.Location = new System.Drawing.Point(347, 62);
            this.stoppedTrainsCountLBL.Name = "stoppedTrainsCountLBL";
            this.stoppedTrainsCountLBL.Size = new System.Drawing.Size(73, 31);
            this.stoppedTrainsCountLBL.TabIndex = 26;
            this.stoppedTrainsCountLBL.Text = "0";
            this.stoppedTrainsCountLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(238, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 17);
            this.label13.TabIndex = 25;
            this.label13.Text = "标注停运+客调未含";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(261, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "停开车次数";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(41, 62);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(92, 17);
            this.label15.TabIndex = 21;
            this.label15.Text = "匹配旅客列车数";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(38, 79);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 17);
            this.label16.TabIndex = 23;
            this.label16.Text = "(去除0G,0J,DJ等)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(261, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 17);
            this.label12.TabIndex = 17;
            this.label12.Text = "匹配车次数";
            // 
            // AllPsngerTrainsCountLBL
            // 
            this.AllPsngerTrainsCountLBL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AllPsngerTrainsCountLBL.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AllPsngerTrainsCountLBL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.AllPsngerTrainsCountLBL.Location = new System.Drawing.Point(141, 59);
            this.AllPsngerTrainsCountLBL.Name = "AllPsngerTrainsCountLBL";
            this.AllPsngerTrainsCountLBL.Size = new System.Drawing.Size(75, 37);
            this.AllPsngerTrainsCountLBL.TabIndex = 22;
            this.AllPsngerTrainsCountLBL.Text = "0";
            this.AllPsngerTrainsCountLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AllTrainsCountLBL
            // 
            this.AllTrainsCountLBL.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AllTrainsCountLBL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.AllTrainsCountLBL.Location = new System.Drawing.Point(345, 17);
            this.AllTrainsCountLBL.Name = "AllTrainsCountLBL";
            this.AllTrainsCountLBL.Size = new System.Drawing.Size(75, 31);
            this.AllTrainsCountLBL.TabIndex = 18;
            this.AllTrainsCountLBL.Text = "0";
            this.AllTrainsCountLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(173, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 22;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "行车室";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(238, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 23;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "综控室";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(30, 15);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(119, 21);
            this.label17.TabIndex = 24;
            this.label17.Text = "0.选择应用范围";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 543);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.TrainEarlyCaculator_Btn);
            this.Controls.Add(this.buildLBL);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.wrongTB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.outputTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.filePath_lbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox command_rTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button importTimeTable_Btn;
        private System.Windows.Forms.Label filePathLBL;
        private System.Windows.Forms.Label filePath_lbl;
        private System.Windows.Forms.Button start_Btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox outputTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RichTextBox wrongTB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label buildLBL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label AllTrainsCountLBL;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label AllPsngerTrainsCountLBL;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label stoppedTrainsCountLBL;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label AllTrainsInTimeTableLBL;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button TrainEarlyCaculator_Btn;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label17;
    }
}

