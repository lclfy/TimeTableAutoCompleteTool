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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.command_rTb = new System.Windows.Forms.RichTextBox();
            this.secondStepText_lbl = new System.Windows.Forms.Label();
            this.importTimeTable_Btn = new System.Windows.Forms.Button();
            this.filePathLBL = new System.Windows.Forms.Label();
            this.filePath_lbl = new System.Windows.Forms.Label();
            this.start_Btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.outputTB = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.hint_label = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.developerLabel = new System.Windows.Forms.Label();
            this.secondListTitle_lbl = new System.Windows.Forms.Label();
            this.searchResult_tb = new System.Windows.Forms.RichTextBox();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.search_tb = new System.Windows.Forms.TextBox();
            this.contentOfDeveloper = new System.Windows.Forms.ToolTip(this.components);
            this.updateReadMe = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(34, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(497, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "1.将当日开行车次客调命令全部复制于下方文本框(无需删除多余内容)";
            // 
            // command_rTb
            // 
            this.command_rTb.ContextMenuStrip = this.contextMenuStrip1;
            this.command_rTb.Location = new System.Drawing.Point(9, 32);
            this.command_rTb.Name = "command_rTb";
            this.command_rTb.Size = new System.Drawing.Size(465, 274);
            this.command_rTb.TabIndex = 1;
            this.command_rTb.Text = "";
            this.command_rTb.TextChanged += new System.EventHandler(this.command_rTb_TextChanged);
            // 
            // secondStepText_lbl
            // 
            this.secondStepText_lbl.AutoSize = true;
            this.secondStepText_lbl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.secondStepText_lbl.Location = new System.Drawing.Point(6, 316);
            this.secondStepText_lbl.Name = "secondStepText_lbl";
            this.secondStepText_lbl.Size = new System.Drawing.Size(222, 21);
            this.secondStepText_lbl.TabIndex = 2;
            this.secondStepText_lbl.Text = "2.选择时刻表文件/基本图文件";
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
            this.filePath_lbl.Location = new System.Drawing.Point(110, 376);
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
            this.label4.Location = new System.Drawing.Point(39, 54);
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
            this.label5.Location = new System.Drawing.Point(535, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "（请注意客调令其他部分中的列车）";
            // 
            // outputTB
            // 
            this.outputTB.Location = new System.Drawing.Point(538, 81);
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
            this.label6.Location = new System.Drawing.Point(166, 347);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 17);
            this.label6.TabIndex = 8;
            // 
            // hint_label
            // 
            this.hint_label.AutoSize = true;
            this.hint_label.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hint_label.ForeColor = System.Drawing.SystemColors.Highlight;
            this.hint_label.Location = new System.Drawing.Point(26, 493);
            this.hint_label.Name = "hint_label";
            this.hint_label.Size = new System.Drawing.Size(570, 17);
            this.hint_label.TabIndex = 10;
            this.hint_label.Text = "绿色为开行，红色为停开，白色为调令未含车次，黄色为次日接入车次。高峰/临客/周末在车次前含有标注";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(534, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(186, 21);
            this.label8.TabIndex = 11;
            this.label8.Text = "客调命令中提取出的车次";
            // 
            // developerLabel
            // 
            this.developerLabel.AutoSize = true;
            this.developerLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.developerLabel.ForeColor = System.Drawing.Color.DarkOrange;
            this.developerLabel.Location = new System.Drawing.Point(602, 493);
            this.developerLabel.Name = "developerLabel";
            this.developerLabel.Size = new System.Drawing.Size(229, 17);
            this.developerLabel.TabIndex = 12;
            this.developerLabel.Text = "反馈请联系运转车间-罗思聪（或技术科）";
            // 
            // secondListTitle_lbl
            // 
            this.secondListTitle_lbl.AutoSize = true;
            this.secondListTitle_lbl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.secondListTitle_lbl.Location = new System.Drawing.Point(750, 50);
            this.secondListTitle_lbl.Name = "secondListTitle_lbl";
            this.secondListTitle_lbl.Size = new System.Drawing.Size(74, 21);
            this.secondListTitle_lbl.TabIndex = 13;
            this.secondListTitle_lbl.Text = "搜索车次";
            // 
            // searchResult_tb
            // 
            this.searchResult_tb.Location = new System.Drawing.Point(754, 81);
            this.searchResult_tb.Name = "searchResult_tb";
            this.searchResult_tb.ReadOnly = true;
            this.searchResult_tb.Size = new System.Drawing.Size(210, 283);
            this.searchResult_tb.TabIndex = 14;
            this.searchResult_tb.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label11.Location = new System.Drawing.Point(774, 478);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 17);
            this.label11.TabIndex = 15;
            // 
            // buildLBL
            // 
            this.buildLBL.AutoSize = true;
            this.buildLBL.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buildLBL.ForeColor = System.Drawing.Color.Tomato;
            this.buildLBL.Location = new System.Drawing.Point(867, 493);
            this.buildLBL.Name = "buildLBL";
            this.buildLBL.Size = new System.Drawing.Size(56, 17);
            this.buildLBL.TabIndex = 16;
            this.buildLBL.Text = "修订内容";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.importTimeTable_Btn);
            this.groupBox1.Controls.Add(this.filePathLBL);
            this.groupBox1.Controls.Add(this.secondStepText_lbl);
            this.groupBox1.Controls.Add(this.start_Btn);
            this.groupBox1.Controls.Add(this.command_rTb);
            this.groupBox1.Location = new System.Drawing.Point(29, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 435);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // TrainEarlyCaculator_Btn
            // 
            this.TrainEarlyCaculator_Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TrainEarlyCaculator_Btn.Location = new System.Drawing.Point(219, 8);
            this.TrainEarlyCaculator_Btn.Name = "TrainEarlyCaculator_Btn";
            this.TrainEarlyCaculator_Btn.Size = new System.Drawing.Size(10, 10);
            this.TrainEarlyCaculator_Btn.TabIndex = 21;
            this.TrainEarlyCaculator_Btn.UseVisualStyleBackColor = true;
            this.TrainEarlyCaculator_Btn.Click += new System.EventHandler(this.TrainEarlyCaculator_Btn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.search_tb);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.TrainEarlyCaculator_Btn);
            this.groupBox2.Location = new System.Drawing.Point(519, 49);
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
            this.radioButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.Location = new System.Drawing.Point(80, 10);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(62, 21);
            this.radioButton1.TabIndex = 22;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "行车室";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton2.Location = new System.Drawing.Point(147, 10);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(62, 21);
            this.radioButton2.TabIndex = 23;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "综控室";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Menu;
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.Color.DarkOrange;
            this.groupBox4.Location = new System.Drawing.Point(754, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(230, 35);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "应用范围";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.粘贴ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // search_tb
            // 
            this.search_tb.Location = new System.Drawing.Point(311, 2);
            this.search_tb.Name = "search_tb";
            this.search_tb.Size = new System.Drawing.Size(133, 21);
            this.search_tb.TabIndex = 27;
            this.search_tb.TextChanged += new System.EventHandler(this.search_tb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Tomato;
            this.label2.Location = new System.Drawing.Point(808, 510);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "鼠标移动至版本号查看更新内容";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 529);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buildLBL);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.searchResult_tb);
            this.Controls.Add(this.secondListTitle_lbl);
            this.Controls.Add(this.developerLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.hint_label);
            this.Controls.Add(this.outputTB);
            this.Controls.Add(this.label6);
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
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox command_rTb;
        private System.Windows.Forms.Label secondStepText_lbl;
        private System.Windows.Forms.Button importTimeTable_Btn;
        private System.Windows.Forms.Label filePathLBL;
        private System.Windows.Forms.Label filePath_lbl;
        private System.Windows.Forms.Button start_Btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox outputTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label hint_label;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label developerLabel;
        private System.Windows.Forms.Label secondListTitle_lbl;
        private System.Windows.Forms.RichTextBox searchResult_tb;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.TextBox search_tb;
        private System.Windows.Forms.ToolTip contentOfDeveloper;
        private System.Windows.Forms.ToolTip updateReadMe;
        private System.Windows.Forms.Label label2;
    }
}

