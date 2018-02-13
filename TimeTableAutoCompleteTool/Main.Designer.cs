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
            this.label1 = new System.Windows.Forms.Label();
            this.command_rTb = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.importTimeTable_Btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.filePath_lbl = new System.Windows.Forms.Label();
            this.start_Btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.testTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(36, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "1.将当日开行车次客调命令复制于此";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(36, 366);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "已选择：";
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
            this.start_Btn.Size = new System.Drawing.Size(465, 65);
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
            this.label5.Location = new System.Drawing.Point(305, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "（请注意客调令其他部分中的列车）";
            // 
            // testTB
            // 
            this.testTB.Location = new System.Drawing.Point(511, 75);
            this.testTB.Name = "testTB";
            this.testTB.Size = new System.Drawing.Size(465, 245);
            this.testTB.TabIndex = 9;
            this.testTB.Text = "";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 489);
            this.Controls.Add(this.testTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.start_Btn);
            this.Controls.Add(this.filePath_lbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.importTimeTable_Btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.command_rTb);
            this.Controls.Add(this.label1);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox command_rTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button importTimeTable_Btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label filePath_lbl;
        private System.Windows.Forms.Button start_Btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox testTB;
    }
}

