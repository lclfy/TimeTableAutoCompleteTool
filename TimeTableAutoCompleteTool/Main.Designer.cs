﻿namespace TimeTableAutoCompleteTool
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondStepText_lbl = new System.Windows.Forms.Label();
            this.filePathLBL = new System.Windows.Forms.Label();
            this.filePath_lbl = new System.Windows.Forms.Label();
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.compareDailySchedue_btn = new CCWin.SkinControl.SkinButton();
            this.command_rTb = new System.Windows.Forms.RichTextBox();
            this.EMUorEMUC_groupBox = new System.Windows.Forms.GroupBox();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.FontSize_tb = new CCWin.SkinControl.SkinWaterTextBox();
            this.label222 = new CCWin.SkinControl.SkinLabel();
            this.label111 = new CCWin.SkinControl.SkinLabel();
            this.start_Btn = new CCWin.SkinControl.SkinButton();
            this.importTimeTable_Btn = new CCWin.SkinControl.SkinButton();
            this.rightGroupBox = new System.Windows.Forms.GroupBox();
            this.dataAnalyse_btn = new CCWin.SkinControl.SkinButton();
            this.search_tb = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.contentOfDeveloper = new System.Windows.Forms.ToolTip(this.components);
            this.updateReadMe = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.rightGroupBox_Compare = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comparedResult_rtb = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.compare_btn = new CCWin.SkinControl.SkinButton();
            this.label7 = new System.Windows.Forms.Label();
            this.yesterdayCommand_rtb = new System.Windows.Forms.RichTextBox();
            this.rightGroupBox_EMUGarage = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.emptyTrackList_rtb = new System.Windows.Forms.RichTextBox();
            this.matchTrackWithTrain_Project_btn = new CCWin.SkinControl.SkinButton();
            this.trainPorjectFilePath_lbl = new System.Windows.Forms.Label();
            this.importTrainProjectFile_btn = new CCWin.SkinControl.SkinButton();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.EMUGarage_YesterdayCommand_rtb = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.EMUorEMUC_groupBox.SuspendLayout();
            this.rightGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.rightGroupBox_Compare.SuspendLayout();
            this.rightGroupBox_EMUGarage.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(36, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(496, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "1.将当日开行车次客调命令全部复制于下方文本框(无需删除多余内容)";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制toolStripMenuItem1,
            this.粘贴ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 复制toolStripMenuItem1
            // 
            this.复制toolStripMenuItem1.Name = "复制toolStripMenuItem1";
            this.复制toolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.复制toolStripMenuItem1.Text = "复制";
            this.复制toolStripMenuItem1.Click += new System.EventHandler(this.复制toolStripMenuItem1_Click);
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
            // secondStepText_lbl
            // 
            this.secondStepText_lbl.AutoSize = true;
            this.secondStepText_lbl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.secondStepText_lbl.Location = new System.Drawing.Point(6, 316);
            this.secondStepText_lbl.Name = "secondStepText_lbl";
            this.secondStepText_lbl.Size = new System.Drawing.Size(220, 22);
            this.secondStepText_lbl.TabIndex = 2;
            this.secondStepText_lbl.Text = "2.选择时刻表文件/基本图文件";
            // 
            // filePathLBL
            // 
            this.filePathLBL.AutoSize = true;
            this.filePathLBL.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePathLBL.Location = new System.Drawing.Point(9, 361);
            this.filePathLBL.Name = "filePathLBL";
            this.filePathLBL.Size = new System.Drawing.Size(56, 17);
            this.filePathLBL.TabIndex = 4;
            this.filePathLBL.Text = "已选择：";
            // 
            // filePath_lbl
            // 
            this.filePath_lbl.AutoSize = true;
            this.filePath_lbl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePath_lbl.Location = new System.Drawing.Point(111, 404);
            this.filePath_lbl.Name = "filePath_lbl";
            this.filePath_lbl.Size = new System.Drawing.Size(0, 17);
            this.filePath_lbl.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(41, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(365, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "示例：218、2018年02月13日，CRH380BG-5708+5811：G1294/5/4。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(537, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "（请注意客调令其他部分中的列车）";
            // 
            // outputTB
            // 
            this.outputTB.Location = new System.Drawing.Point(18, 32);
            this.outputTB.Name = "outputTB";
            this.outputTB.ReadOnly = true;
            this.outputTB.Size = new System.Drawing.Size(211, 353);
            this.outputTB.TabIndex = 9;
            this.outputTB.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(168, 375);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 17);
            this.label6.TabIndex = 8;
            // 
            // hint_label
            // 
            this.hint_label.AutoSize = true;
            this.hint_label.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hint_label.ForeColor = System.Drawing.SystemColors.Highlight;
            this.hint_label.Location = new System.Drawing.Point(27, 532);
            this.hint_label.Name = "hint_label";
            this.hint_label.Size = new System.Drawing.Size(570, 17);
            this.hint_label.TabIndex = 10;
            this.hint_label.Text = "绿色为开行，红色为停开，白色为调令未含车次，黄色为次日接入车次。高峰/临客/周末在车次前含有标注";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(15, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(186, 22);
            this.label8.TabIndex = 11;
            this.label8.Text = "客调命令中提取出的车次";
            // 
            // developerLabel
            // 
            this.developerLabel.AutoSize = true;
            this.developerLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.developerLabel.ForeColor = System.Drawing.Color.DarkOrange;
            this.developerLabel.Location = new System.Drawing.Point(603, 532);
            this.developerLabel.Name = "developerLabel";
            this.developerLabel.Size = new System.Drawing.Size(229, 17);
            this.developerLabel.TabIndex = 12;
            this.developerLabel.Text = "反馈请联系运转车间-罗思聪（或技术科）";
            // 
            // secondListTitle_lbl
            // 
            this.secondListTitle_lbl.AutoSize = true;
            this.secondListTitle_lbl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.secondListTitle_lbl.Location = new System.Drawing.Point(231, 2);
            this.secondListTitle_lbl.Name = "secondListTitle_lbl";
            this.secondListTitle_lbl.Size = new System.Drawing.Size(74, 22);
            this.secondListTitle_lbl.TabIndex = 13;
            this.secondListTitle_lbl.Text = "搜索车次";
            // 
            // searchResult_tb
            // 
            this.searchResult_tb.Location = new System.Drawing.Point(231, 32);
            this.searchResult_tb.Name = "searchResult_tb";
            this.searchResult_tb.ReadOnly = true;
            this.searchResult_tb.Size = new System.Drawing.Size(211, 353);
            this.searchResult_tb.TabIndex = 14;
            this.searchResult_tb.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label11.Location = new System.Drawing.Point(777, 517);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 17);
            this.label11.TabIndex = 15;
            // 
            // buildLBL
            // 
            this.buildLBL.AutoSize = true;
            this.buildLBL.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buildLBL.ForeColor = System.Drawing.Color.Tomato;
            this.buildLBL.Location = new System.Drawing.Point(841, 532);
            this.buildLBL.Name = "buildLBL";
            this.buildLBL.Size = new System.Drawing.Size(56, 17);
            this.buildLBL.TabIndex = 16;
            this.buildLBL.Text = "修订内容";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.compareDailySchedue_btn);
            this.groupBox1.Controls.Add(this.command_rTb);
            this.groupBox1.Controls.Add(this.EMUorEMUC_groupBox);
            this.groupBox1.Controls.Add(this.FontSize_tb);
            this.groupBox1.Controls.Add(this.label222);
            this.groupBox1.Controls.Add(this.label111);
            this.groupBox1.Controls.Add(this.start_Btn);
            this.groupBox1.Controls.Add(this.importTimeTable_Btn);
            this.groupBox1.Controls.Add(this.filePathLBL);
            this.groupBox1.Controls.Add(this.secondStepText_lbl);
            this.groupBox1.Location = new System.Drawing.Point(31, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 452);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(325, 373);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(87, 21);
            this.checkBox1.TabIndex = 32;
            this.checkBox1.Text = "删除停运车";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // compareDailySchedue_btn
            // 
            this.compareDailySchedue_btn.BackColor = System.Drawing.Color.Transparent;
            this.compareDailySchedue_btn.BaseColor = System.Drawing.Color.DeepPink;
            this.compareDailySchedue_btn.BorderColor = System.Drawing.Color.DeepPink;
            this.compareDailySchedue_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.compareDailySchedue_btn.DownBack = null;
            this.compareDailySchedue_btn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.compareDailySchedue_btn.ForeColor = System.Drawing.Color.White;
            this.compareDailySchedue_btn.Location = new System.Drawing.Point(379, 352);
            this.compareDailySchedue_btn.MouseBack = null;
            this.compareDailySchedue_btn.Name = "compareDailySchedue_btn";
            this.compareDailySchedue_btn.NormlBack = null;
            this.compareDailySchedue_btn.Size = new System.Drawing.Size(95, 41);
            this.compareDailySchedue_btn.TabIndex = 31;
            this.compareDailySchedue_btn.Text = "计划文件对比";
            this.compareDailySchedue_btn.UseVisualStyleBackColor = false;
            this.compareDailySchedue_btn.Click += new System.EventHandler(this.compareDailySchedue_btn_Click);
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
            // EMUorEMUC_groupBox
            // 
            this.EMUorEMUC_groupBox.BackColor = System.Drawing.Color.White;
            this.EMUorEMUC_groupBox.Controls.Add(this.radioButton6);
            this.EMUorEMUC_groupBox.Controls.Add(this.radioButton4);
            this.EMUorEMUC_groupBox.Controls.Add(this.radioButton5);
            this.EMUorEMUC_groupBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EMUorEMUC_groupBox.ForeColor = System.Drawing.Color.DarkOrange;
            this.EMUorEMUC_groupBox.Location = new System.Drawing.Point(207, 304);
            this.EMUorEMUC_groupBox.Name = "EMUorEMUC_groupBox";
            this.EMUorEMUC_groupBox.Size = new System.Drawing.Size(90, 86);
            this.EMUorEMUC_groupBox.TabIndex = 26;
            this.EMUorEMUC_groupBox.TabStop = false;
            this.EMUorEMUC_groupBox.Text = "全图类型";
            this.EMUorEMUC_groupBox.Enter += new System.EventHandler(this.EMUorEMUC_groupBox_Enter);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton6.Location = new System.Drawing.Point(11, 53);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(50, 21);
            this.radioButton6.TabIndex = 27;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "上水";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton4.Location = new System.Drawing.Point(11, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(62, 21);
            this.radioButton4.TabIndex = 26;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "班计划";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton5.Location = new System.Drawing.Point(11, 37);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(62, 21);
            this.radioButton5.TabIndex = 25;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "动检车";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged_1);
            // 
            // FontSize_tb
            // 
            this.FontSize_tb.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FontSize_tb.Location = new System.Drawing.Point(415, 359);
            this.FontSize_tb.Name = "FontSize_tb";
            this.FontSize_tb.Size = new System.Drawing.Size(25, 30);
            this.FontSize_tb.TabIndex = 30;
            this.FontSize_tb.Text = "12";
            this.FontSize_tb.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.FontSize_tb.WaterText = "";
            this.FontSize_tb.TextChanged += new System.EventHandler(this.FontSize_tb_TextChanged);
            // 
            // label222
            // 
            this.label222.AutoSize = true;
            this.label222.BackColor = System.Drawing.Color.Transparent;
            this.label222.BorderColor = System.Drawing.Color.White;
            this.label222.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label222.Location = new System.Drawing.Point(279, 373);
            this.label222.Name = "label222";
            this.label222.Size = new System.Drawing.Size(0, 17);
            this.label222.TabIndex = 29;
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.BackColor = System.Drawing.Color.Transparent;
            this.label111.BorderColor = System.Drawing.Color.White;
            this.label111.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label111.Location = new System.Drawing.Point(341, 357);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(68, 17);
            this.label111.TabIndex = 1;
            this.label111.Text = "时刻表字体";
            // 
            // start_Btn
            // 
            this.start_Btn.BackColor = System.Drawing.Color.Transparent;
            this.start_Btn.BaseColor = System.Drawing.Color.DodgerBlue;
            this.start_Btn.BorderColor = System.Drawing.Color.DodgerBlue;
            this.start_Btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.start_Btn.DownBack = null;
            this.start_Btn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start_Btn.ForeColor = System.Drawing.Color.White;
            this.start_Btn.Location = new System.Drawing.Point(9, 396);
            this.start_Btn.MouseBack = null;
            this.start_Btn.Name = "start_Btn";
            this.start_Btn.NormlBack = null;
            this.start_Btn.Size = new System.Drawing.Size(465, 43);
            this.start_Btn.TabIndex = 8;
            this.start_Btn.Text = "生成时刻表/班计划";
            this.start_Btn.UseVisualStyleBackColor = false;
            this.start_Btn.Click += new System.EventHandler(this.start_Btn_Click);
            // 
            // importTimeTable_Btn
            // 
            this.importTimeTable_Btn.BackColor = System.Drawing.Color.Transparent;
            this.importTimeTable_Btn.BaseColor = System.Drawing.Color.DodgerBlue;
            this.importTimeTable_Btn.BorderColor = System.Drawing.Color.DodgerBlue;
            this.importTimeTable_Btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.importTimeTable_Btn.DownBack = null;
            this.importTimeTable_Btn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.importTimeTable_Btn.ForeColor = System.Drawing.Color.White;
            this.importTimeTable_Btn.Location = new System.Drawing.Point(379, 312);
            this.importTimeTable_Btn.MouseBack = null;
            this.importTimeTable_Btn.Name = "importTimeTable_Btn";
            this.importTimeTable_Btn.NormlBack = null;
            this.importTimeTable_Btn.Size = new System.Drawing.Size(95, 41);
            this.importTimeTable_Btn.TabIndex = 7;
            this.importTimeTable_Btn.Text = "导入全部";
            this.importTimeTable_Btn.UseVisualStyleBackColor = false;
            this.importTimeTable_Btn.Click += new System.EventHandler(this.importTimeTable_Btn_Click);
            // 
            // rightGroupBox
            // 
            this.rightGroupBox.Controls.Add(this.dataAnalyse_btn);
            this.rightGroupBox.Controls.Add(this.search_tb);
            this.rightGroupBox.Controls.Add(this.outputTB);
            this.rightGroupBox.Controls.Add(this.searchResult_tb);
            this.rightGroupBox.Controls.Add(this.label8);
            this.rightGroupBox.Controls.Add(this.secondListTitle_lbl);
            this.rightGroupBox.Location = new System.Drawing.Point(521, 77);
            this.rightGroupBox.Name = "rightGroupBox";
            this.rightGroupBox.Size = new System.Drawing.Size(465, 452);
            this.rightGroupBox.TabIndex = 20;
            this.rightGroupBox.TabStop = false;
            // 
            // dataAnalyse_btn
            // 
            this.dataAnalyse_btn.BackColor = System.Drawing.Color.Transparent;
            this.dataAnalyse_btn.BaseColor = System.Drawing.Color.DeepPink;
            this.dataAnalyse_btn.BorderColor = System.Drawing.Color.DeepPink;
            this.dataAnalyse_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.dataAnalyse_btn.DownBack = null;
            this.dataAnalyse_btn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataAnalyse_btn.ForeColor = System.Drawing.Color.White;
            this.dataAnalyse_btn.Location = new System.Drawing.Point(18, 396);
            this.dataAnalyse_btn.MouseBack = null;
            this.dataAnalyse_btn.Name = "dataAnalyse_btn";
            this.dataAnalyse_btn.NormlBack = null;
            this.dataAnalyse_btn.Size = new System.Drawing.Size(423, 43);
            this.dataAnalyse_btn.TabIndex = 28;
            this.dataAnalyse_btn.Text = "查看统计数据";
            this.dataAnalyse_btn.UseVisualStyleBackColor = false;
            this.dataAnalyse_btn.Click += new System.EventHandler(this.dataAnalyse_btn_Click);
            // 
            // search_tb
            // 
            this.search_tb.Location = new System.Drawing.Point(311, 2);
            this.search_tb.Name = "search_tb";
            this.search_tb.Size = new System.Drawing.Size(133, 21);
            this.search_tb.TabIndex = 27;
            this.search_tb.TextChanged += new System.EventHandler(this.search_tb_TextChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.Location = new System.Drawing.Point(15, 17);
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
            this.radioButton2.Location = new System.Drawing.Point(83, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(62, 21);
            this.radioButton2.TabIndex = 23;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "综控室";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.radioButton3);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.Color.DarkOrange;
            this.groupBox4.Location = new System.Drawing.Point(756, 35);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(231, 40);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "应用范围";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton3.Location = new System.Drawing.Point(151, 17);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(62, 21);
            this.radioButton3.TabIndex = 24;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "动车所";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Tomato;
            this.label2.Location = new System.Drawing.Point(810, 549);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "鼠标移动至版本号查看更新内容";
            // 
            // rightGroupBox_Compare
            // 
            this.rightGroupBox_Compare.Controls.Add(this.label10);
            this.rightGroupBox_Compare.Controls.Add(this.comparedResult_rtb);
            this.rightGroupBox_Compare.Controls.Add(this.label9);
            this.rightGroupBox_Compare.Controls.Add(this.compare_btn);
            this.rightGroupBox_Compare.Controls.Add(this.label7);
            this.rightGroupBox_Compare.Controls.Add(this.yesterdayCommand_rtb);
            this.rightGroupBox_Compare.Location = new System.Drawing.Point(521, 77);
            this.rightGroupBox_Compare.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.rightGroupBox_Compare.Name = "rightGroupBox_Compare";
            this.rightGroupBox_Compare.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.rightGroupBox_Compare.Size = new System.Drawing.Size(477, 452);
            this.rightGroupBox_Compare.TabIndex = 28;
            this.rightGroupBox_Compare.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Tomato;
            this.label10.Location = new System.Drawing.Point(183, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(164, 17);
            this.label10.TabIndex = 29;
            this.label10.Text = "上方对比内容应为前一日客调";
            // 
            // comparedResult_rtb
            // 
            this.comparedResult_rtb.ContextMenuStrip = this.contextMenuStrip1;
            this.comparedResult_rtb.Location = new System.Drawing.Point(12, 227);
            this.comparedResult_rtb.Name = "comparedResult_rtb";
            this.comparedResult_rtb.ReadOnly = true;
            this.comparedResult_rtb.Size = new System.Drawing.Size(442, 155);
            this.comparedResult_rtb.TabIndex = 33;
            this.comparedResult_rtb.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(9, 199);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(154, 22);
            this.label9.TabIndex = 32;
            this.label9.Text = "对比结果（可复制）";
            // 
            // compare_btn
            // 
            this.compare_btn.BackColor = System.Drawing.Color.Transparent;
            this.compare_btn.BaseColor = System.Drawing.Color.DodgerBlue;
            this.compare_btn.BorderColor = System.Drawing.Color.DodgerBlue;
            this.compare_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.compare_btn.DownBack = null;
            this.compare_btn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.compare_btn.ForeColor = System.Drawing.Color.White;
            this.compare_btn.Location = new System.Drawing.Point(12, 396);
            this.compare_btn.MouseBack = null;
            this.compare_btn.Name = "compare_btn";
            this.compare_btn.NormlBack = null;
            this.compare_btn.Size = new System.Drawing.Size(441, 43);
            this.compare_btn.TabIndex = 31;
            this.compare_btn.Text = "（选择文件后）对比两日客调";
            this.compare_btn.UseVisualStyleBackColor = false;
            this.compare_btn.Click += new System.EventHandler(this.compare_btn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(9, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(266, 22);
            this.label7.TabIndex = 29;
            this.label7.Text = "（复制对比命令）对比客调粘贴位置";
            // 
            // yesterdayCommand_rtb
            // 
            this.yesterdayCommand_rtb.ContextMenuStrip = this.contextMenuStrip1;
            this.yesterdayCommand_rtb.Location = new System.Drawing.Point(12, 32);
            this.yesterdayCommand_rtb.Name = "yesterdayCommand_rtb";
            this.yesterdayCommand_rtb.Size = new System.Drawing.Size(442, 164);
            this.yesterdayCommand_rtb.TabIndex = 31;
            this.yesterdayCommand_rtb.Text = "";
            this.yesterdayCommand_rtb.TextChanged += new System.EventHandler(this.yesterdayCommand_rtb_TextChanged);
            // 
            // rightGroupBox_EMUGarage
            // 
            this.rightGroupBox_EMUGarage.Controls.Add(this.label17);
            this.rightGroupBox_EMUGarage.Controls.Add(this.emptyTrackList_rtb);
            this.rightGroupBox_EMUGarage.Controls.Add(this.matchTrackWithTrain_Project_btn);
            this.rightGroupBox_EMUGarage.Controls.Add(this.trainPorjectFilePath_lbl);
            this.rightGroupBox_EMUGarage.Controls.Add(this.importTrainProjectFile_btn);
            this.rightGroupBox_EMUGarage.Controls.Add(this.label18);
            this.rightGroupBox_EMUGarage.Controls.Add(this.label19);
            this.rightGroupBox_EMUGarage.Controls.Add(this.label20);
            this.rightGroupBox_EMUGarage.Controls.Add(this.label21);
            this.rightGroupBox_EMUGarage.Controls.Add(this.EMUGarage_YesterdayCommand_rtb);
            this.rightGroupBox_EMUGarage.Location = new System.Drawing.Point(531, 75);
            this.rightGroupBox_EMUGarage.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.rightGroupBox_EMUGarage.Name = "rightGroupBox_EMUGarage";
            this.rightGroupBox_EMUGarage.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.rightGroupBox_EMUGarage.Size = new System.Drawing.Size(465, 452);
            this.rightGroupBox_EMUGarage.TabIndex = 37;
            this.rightGroupBox_EMUGarage.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(9, 191);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(385, 22);
            this.label17.TabIndex = 38;
            this.label17.Text = "夜班计划文件中空股道列表(存场不过夜股道，共72G)";
            // 
            // emptyTrackList_rtb
            // 
            this.emptyTrackList_rtb.ContextMenuStrip = this.contextMenuStrip1;
            this.emptyTrackList_rtb.Cursor = System.Windows.Forms.Cursors.Default;
            this.emptyTrackList_rtb.Location = new System.Drawing.Point(9, 224);
            this.emptyTrackList_rtb.Name = "emptyTrackList_rtb";
            this.emptyTrackList_rtb.ReadOnly = true;
            this.emptyTrackList_rtb.Size = new System.Drawing.Size(442, 84);
            this.emptyTrackList_rtb.TabIndex = 37;
            this.emptyTrackList_rtb.Text = "";
            // 
            // matchTrackWithTrain_Project_btn
            // 
            this.matchTrackWithTrain_Project_btn.BackColor = System.Drawing.Color.Transparent;
            this.matchTrackWithTrain_Project_btn.BaseColor = System.Drawing.Color.OrangeRed;
            this.matchTrackWithTrain_Project_btn.BorderColor = System.Drawing.Color.OrangeRed;
            this.matchTrackWithTrain_Project_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.matchTrackWithTrain_Project_btn.DownBack = null;
            this.matchTrackWithTrain_Project_btn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.matchTrackWithTrain_Project_btn.ForeColor = System.Drawing.Color.White;
            this.matchTrackWithTrain_Project_btn.Location = new System.Drawing.Point(12, 397);
            this.matchTrackWithTrain_Project_btn.MouseBack = null;
            this.matchTrackWithTrain_Project_btn.Name = "matchTrackWithTrain_Project_btn";
            this.matchTrackWithTrain_Project_btn.NormlBack = null;
            this.matchTrackWithTrain_Project_btn.Size = new System.Drawing.Size(441, 43);
            this.matchTrackWithTrain_Project_btn.TabIndex = 31;
            this.matchTrackWithTrain_Project_btn.Text = "（新）整理调车计划";
            this.matchTrackWithTrain_Project_btn.UseVisualStyleBackColor = false;
            this.matchTrackWithTrain_Project_btn.Click += new System.EventHandler(this.matchTrackWithTrain_Project_btn_Click);
            // 
            // trainPorjectFilePath_lbl
            // 
            this.trainPorjectFilePath_lbl.AutoSize = true;
            this.trainPorjectFilePath_lbl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.trainPorjectFilePath_lbl.Location = new System.Drawing.Point(15, 366);
            this.trainPorjectFilePath_lbl.Name = "trainPorjectFilePath_lbl";
            this.trainPorjectFilePath_lbl.Size = new System.Drawing.Size(56, 17);
            this.trainPorjectFilePath_lbl.TabIndex = 35;
            this.trainPorjectFilePath_lbl.Text = "已选择：";
            // 
            // importTrainProjectFile_btn
            // 
            this.importTrainProjectFile_btn.BackColor = System.Drawing.Color.Transparent;
            this.importTrainProjectFile_btn.BaseColor = System.Drawing.Color.OrangeRed;
            this.importTrainProjectFile_btn.BorderColor = System.Drawing.Color.OrangeRed;
            this.importTrainProjectFile_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.importTrainProjectFile_btn.DownBack = null;
            this.importTrainProjectFile_btn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.importTrainProjectFile_btn.ForeColor = System.Drawing.Color.White;
            this.importTrainProjectFile_btn.Location = new System.Drawing.Point(357, 312);
            this.importTrainProjectFile_btn.MouseBack = null;
            this.importTrainProjectFile_btn.Name = "importTrainProjectFile_btn";
            this.importTrainProjectFile_btn.NormlBack = null;
            this.importTrainProjectFile_btn.Size = new System.Drawing.Size(95, 41);
            this.importTrainProjectFile_btn.TabIndex = 36;
            this.importTrainProjectFile_btn.Text = "导入";
            this.importTrainProjectFile_btn.UseVisualStyleBackColor = false;
            this.importTrainProjectFile_btn.Click += new System.EventHandler(this.importTrainProjectFile_btn_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(13, 320);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 22);
            this.label18.TabIndex = 35;
            this.label18.Text = "动车所：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Tomato;
            this.label19.Location = new System.Drawing.Point(183, 203);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(0, 17);
            this.label19.TabIndex = 29;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(9, 199);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(0, 22);
            this.label20.TabIndex = 32;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(9, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(448, 22);
            this.label21.TabIndex = 29;
            this.label21.Text = "复制昨日命令至此（△现在可以自动区分当日/次日进行填表）";
            // 
            // EMUGarage_YesterdayCommand_rtb
            // 
            this.EMUGarage_YesterdayCommand_rtb.ContextMenuStrip = this.contextMenuStrip1;
            this.EMUGarage_YesterdayCommand_rtb.Location = new System.Drawing.Point(12, 32);
            this.EMUGarage_YesterdayCommand_rtb.Name = "EMUGarage_YesterdayCommand_rtb";
            this.EMUGarage_YesterdayCommand_rtb.Size = new System.Drawing.Size(442, 148);
            this.EMUGarage_YesterdayCommand_rtb.TabIndex = 31;
            this.EMUGarage_YesterdayCommand_rtb.Text = "";
            this.EMUGarage_YesterdayCommand_rtb.TextChanged += new System.EventHandler(this.EMUGarage_YesterdayCommand_rtb_TextChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionBackColorBottom = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1028, 592);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buildLBL);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.developerLabel);
            this.Controls.Add(this.hint_label);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.filePath_lbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rightGroupBox);
            this.Controls.Add(this.rightGroupBox_EMUGarage);
            this.Controls.Add(this.rightGroupBox_Compare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.EMUorEMUC_groupBox.ResumeLayout(false);
            this.EMUorEMUC_groupBox.PerformLayout();
            this.rightGroupBox.ResumeLayout(false);
            this.rightGroupBox.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.rightGroupBox_Compare.ResumeLayout(false);
            this.rightGroupBox_Compare.PerformLayout();
            this.rightGroupBox_EMUGarage.ResumeLayout(false);
            this.rightGroupBox_EMUGarage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label secondStepText_lbl;
        private System.Windows.Forms.Label filePathLBL;
        private System.Windows.Forms.Label filePath_lbl;
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
        private System.Windows.Forms.GroupBox rightGroupBox;
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
        private System.Windows.Forms.RadioButton radioButton3;
        private CCWin.SkinControl.SkinButton importTimeTable_Btn;
        private CCWin.SkinControl.SkinButton start_Btn;
        private CCWin.SkinControl.SkinLabel label222;
        private CCWin.SkinControl.SkinLabel label111;
        private System.Windows.Forms.RichTextBox command_rTb;
        private CCWin.SkinControl.SkinWaterTextBox FontSize_tb;
        private System.Windows.Forms.GroupBox rightGroupBox_Compare;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox yesterdayCommand_rtb;
        private CCWin.SkinControl.SkinButton compare_btn;
        private System.Windows.Forms.RichTextBox comparedResult_rtb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem 复制toolStripMenuItem1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox EMUorEMUC_groupBox;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.GroupBox rightGroupBox_EMUGarage;
        private System.Windows.Forms.Label trainPorjectFilePath_lbl;
        private CCWin.SkinControl.SkinButton importTrainProjectFile_btn;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RichTextBox EMUGarage_YesterdayCommand_rtb;
        private CCWin.SkinControl.SkinButton matchTrackWithTrain_Project_btn;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.RichTextBox emptyTrackList_rtb;
        private CCWin.SkinControl.SkinButton compareDailySchedue_btn;
        private CCWin.SkinControl.SkinButton dataAnalyse_btn;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton radioButton6;
    }
}

