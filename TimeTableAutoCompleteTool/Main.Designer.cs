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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AllPsngerTrainsCountLBL = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.AllTrainsCountLBL = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(36, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "1.将当日开行车次客调命令复制于下方文本框";
            // 
            // command_rTb
            // 
            this.command_rTb.Location = new System.Drawing.Point(40, 75);
            this.command_rTb.Name = "command_rTb";
            this.command_rTb.Size = new System.Drawing.Size(465, 245);
            this.command_rTb.TabIndex = 1;
            this.command_rTb.Text = "";
            this.command_rTb.TextChanged += new System.EventHandler(this.command_rTb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(36, 335);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "2.选择时刻表文件";
            // 
            // importTimeTable_Btn
            // 
            this.importTimeTable_Btn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.importTimeTable_Btn.Location = new System.Drawing.Point(410, 326);
            this.importTimeTable_Btn.Name = "importTimeTable_Btn";
            this.importTimeTable_Btn.Size = new System.Drawing.Size(95, 32);
            this.importTimeTable_Btn.TabIndex = 3;
            this.importTimeTable_Btn.Text = "导入时刻表";
            this.importTimeTable_Btn.UseVisualStyleBackColor = true;
            this.importTimeTable_Btn.Click += new System.EventHandler(this.importTimeTable_Btn_Click);
            // 
            // filePathLBL
            // 
            this.filePathLBL.AutoSize = true;
            this.filePathLBL.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePathLBL.Location = new System.Drawing.Point(36, 366);
            this.filePathLBL.Name = "filePathLBL";
            this.filePathLBL.Size = new System.Drawing.Size(56, 17);
            this.filePathLBL.TabIndex = 4;
            this.filePathLBL.Text = "已选择：";
            // 
            // filePath_lbl
            // 
            this.filePath_lbl.AutoSize = true;
            this.filePath_lbl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePath_lbl.Location = new System.Drawing.Point(112, 370);
            this.filePath_lbl.Name = "filePath_lbl";
            this.filePath_lbl.Size = new System.Drawing.Size(0, 17);
            this.filePath_lbl.TabIndex = 5;
            // 
            // start_Btn
            // 
            this.start_Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start_Btn.Location = new System.Drawing.Point(40, 402);
            this.start_Btn.Name = "start_Btn";
            this.start_Btn.Size = new System.Drawing.Size(465, 49);
            this.start_Btn.TabIndex = 6;
            this.start_Btn.Text = "处理时刻表";
            this.start_Btn.UseVisualStyleBackColor = true;
            this.start_Btn.Click += new System.EventHandler(this.start_Btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(41, 48);
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
            this.label5.Location = new System.Drawing.Point(359, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "（请注意客调令其他部分中的列车）";
            // 
            // outputTB
            // 
            this.outputTB.Location = new System.Drawing.Point(540, 75);
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
            this.label6.Location = new System.Drawing.Point(168, 341);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 17);
            this.label6.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label7.Location = new System.Drawing.Point(28, 481);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(486, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "绿色为开行，红色为停开，白色为客调命令未包含车次。高峰/临客/周末在车次前含有标注";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(536, 44);
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
            this.label9.Location = new System.Drawing.Point(537, 481);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(233, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "意见反馈/Bug反馈请联系运转车间-罗思聪";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(752, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(212, 21);
            this.label10.TabIndex = 13;
            this.label10.Text = "部分未识别车次(检查对应行)";
            // 
            // wrongTB
            // 
            this.wrongTB.Location = new System.Drawing.Point(756, 75);
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
            this.label11.Location = new System.Drawing.Point(776, 472);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 17);
            this.label11.TabIndex = 15;
            // 
            // buildLBL
            // 
            this.buildLBL.AutoSize = true;
            this.buildLBL.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buildLBL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buildLBL.Location = new System.Drawing.Point(948, 481);
            this.buildLBL.Name = "buildLBL";
            this.buildLBL.Size = new System.Drawing.Size(56, 17);
            this.buildLBL.TabIndex = 16;
            this.buildLBL.Text = "修订内容";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(31, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 426);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Location = new System.Drawing.Point(521, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 426);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // AllPsngerTrainsCountLBL
            // 
            this.AllPsngerTrainsCountLBL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AllPsngerTrainsCountLBL.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AllPsngerTrainsCountLBL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.AllPsngerTrainsCountLBL.Location = new System.Drawing.Point(56, 21);
            this.AllPsngerTrainsCountLBL.Name = "AllPsngerTrainsCountLBL";
            this.AllPsngerTrainsCountLBL.Size = new System.Drawing.Size(97, 31);
            this.AllPsngerTrainsCountLBL.TabIndex = 22;
            this.AllPsngerTrainsCountLBL.Text = "0";
            this.AllPsngerTrainsCountLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(34, 56);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 17);
            this.label15.TabIndex = 21;
            this.label15.Text = "已选时刻表中开行列车数";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(52, 73);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 17);
            this.label16.TabIndex = 23;
            this.label16.Text = "(去除0G,0J,DJ等)";
            // 
            // AllTrainsCountLBL
            // 
            this.AllTrainsCountLBL.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AllTrainsCountLBL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.AllTrainsCountLBL.Location = new System.Drawing.Point(60, 21);
            this.AllTrainsCountLBL.Name = "AllTrainsCountLBL";
            this.AllTrainsCountLBL.Size = new System.Drawing.Size(88, 31);
            this.AllTrainsCountLBL.TabIndex = 18;
            this.AllTrainsCountLBL.Text = "0";
            this.AllTrainsCountLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(22, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 20);
            this.label12.TabIndex = 17;
            this.label12.Text = "已选时刻表中匹配车次数";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.AllTrainsCountLBL);
            this.groupBox3.Location = new System.Drawing.Point(19, 321);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(210, 99);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.AllPsngerTrainsCountLBL);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Location = new System.Drawing.Point(235, 321);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(210, 99);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 507);
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
            this.Controls.Add(this.start_Btn);
            this.Controls.Add(this.filePath_lbl);
            this.Controls.Add(this.filePathLBL);
            this.Controls.Add(this.importTimeTable_Btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.command_rTb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label AllPsngerTrainsCountLBL;
        private System.Windows.Forms.Label label15;
    }
}

