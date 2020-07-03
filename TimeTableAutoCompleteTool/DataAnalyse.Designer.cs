namespace TimeTableAutoCompleteTool
{
    partial class DataAnalyse
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.start_cb = new CCWin.SkinControl.SkinCheckBox();
            this.normal_cb = new CCWin.SkinControl.SkinCheckBox();
            this.psngerTrain_cb = new CCWin.SkinControl.SkinCheckBox();
            this.nonPsngerTrain_cb = new CCWin.SkinControl.SkinCheckBox();
            this.checked_cb = new CCWin.SkinControl.SkinCheckBox();
            this.nonChecked_cb = new CCWin.SkinControl.SkinCheckBox();
            this.AllTrainsInCommand_lbl = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.currentShow_lbl = new System.Windows.Forms.Label();
            this.search_tb = new System.Windows.Forms.TextBox();
            this.secondListTitle_lbl = new System.Windows.Forms.Label();
            this.data_lv = new System.Windows.Forms.ListView();
            this.Match = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trainIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trainNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.secondTrainNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._streamStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._trainType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._trainModel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._trainID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.operationChanged_rtb = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._normalOrAdded = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stop_cb = new CCWin.SkinControl.SkinCheckBox();
            this.temp_cb = new CCWin.SkinControl.SkinCheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.label7 = new System.Windows.Forms.Label();
            this.unrecognizedTrain_rtb = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notMatchedTrains_rtb = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // start_cb
            // 
            this.start_cb.AutoSize = true;
            this.start_cb.BackColor = System.Drawing.Color.Transparent;
            this.start_cb.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.start_cb.DownBack = null;
            this.start_cb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start_cb.Location = new System.Drawing.Point(301, 921);
            this.start_cb.MouseBack = null;
            this.start_cb.Name = "start_cb";
            this.start_cb.NormlBack = null;
            this.start_cb.SelectedDownBack = null;
            this.start_cb.SelectedMouseBack = null;
            this.start_cb.SelectedNormlBack = null;
            this.start_cb.Size = new System.Drawing.Size(94, 35);
            this.start_cb.TabIndex = 1;
            this.start_cb.Text = "开行";
            this.start_cb.UseVisualStyleBackColor = false;
            this.start_cb.CheckedChanged += new System.EventHandler(this.up_cb_CheckedChanged);
            // 
            // normal_cb
            // 
            this.normal_cb.AutoSize = true;
            this.normal_cb.BackColor = System.Drawing.Color.Transparent;
            this.normal_cb.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.normal_cb.DownBack = null;
            this.normal_cb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.normal_cb.Location = new System.Drawing.Point(514, 921);
            this.normal_cb.MouseBack = null;
            this.normal_cb.Name = "normal_cb";
            this.normal_cb.NormlBack = null;
            this.normal_cb.SelectedDownBack = null;
            this.normal_cb.SelectedMouseBack = null;
            this.normal_cb.SelectedNormlBack = null;
            this.normal_cb.Size = new System.Drawing.Size(94, 35);
            this.normal_cb.TabIndex = 2;
            this.normal_cb.Text = "普通";
            this.normal_cb.UseVisualStyleBackColor = false;
            this.normal_cb.CheckedChanged += new System.EventHandler(this.down_cb_CheckedChanged);
            // 
            // psngerTrain_cb
            // 
            this.psngerTrain_cb.AutoSize = true;
            this.psngerTrain_cb.BackColor = System.Drawing.Color.Transparent;
            this.psngerTrain_cb.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.psngerTrain_cb.DownBack = null;
            this.psngerTrain_cb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.psngerTrain_cb.Location = new System.Drawing.Point(659, 921);
            this.psngerTrain_cb.MouseBack = null;
            this.psngerTrain_cb.Name = "psngerTrain_cb";
            this.psngerTrain_cb.NormlBack = null;
            this.psngerTrain_cb.SelectedDownBack = null;
            this.psngerTrain_cb.SelectedMouseBack = null;
            this.psngerTrain_cb.SelectedNormlBack = null;
            this.psngerTrain_cb.Size = new System.Drawing.Size(142, 35);
            this.psngerTrain_cb.TabIndex = 3;
            this.psngerTrain_cb.Text = "旅客列车";
            this.psngerTrain_cb.UseVisualStyleBackColor = false;
            this.psngerTrain_cb.CheckedChanged += new System.EventHandler(this.psngerTrain_cb_CheckedChanged);
            // 
            // nonPsngerTrain_cb
            // 
            this.nonPsngerTrain_cb.AutoSize = true;
            this.nonPsngerTrain_cb.BackColor = System.Drawing.Color.Transparent;
            this.nonPsngerTrain_cb.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.nonPsngerTrain_cb.DownBack = null;
            this.nonPsngerTrain_cb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nonPsngerTrain_cb.Location = new System.Drawing.Point(659, 962);
            this.nonPsngerTrain_cb.MouseBack = null;
            this.nonPsngerTrain_cb.Name = "nonPsngerTrain_cb";
            this.nonPsngerTrain_cb.NormlBack = null;
            this.nonPsngerTrain_cb.SelectedDownBack = null;
            this.nonPsngerTrain_cb.SelectedMouseBack = null;
            this.nonPsngerTrain_cb.SelectedNormlBack = null;
            this.nonPsngerTrain_cb.Size = new System.Drawing.Size(176, 35);
            this.nonPsngerTrain_cb.TabIndex = 4;
            this.nonPsngerTrain_cb.Text = "出库/检测车";
            this.nonPsngerTrain_cb.UseVisualStyleBackColor = false;
            this.nonPsngerTrain_cb.CheckedChanged += new System.EventHandler(this.nonPsngerTrain_cb_CheckedChanged);
            // 
            // checked_cb
            // 
            this.checked_cb.AutoSize = true;
            this.checked_cb.BackColor = System.Drawing.Color.Transparent;
            this.checked_cb.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.checked_cb.DownBack = null;
            this.checked_cb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checked_cb.Location = new System.Drawing.Point(77, 921);
            this.checked_cb.MouseBack = null;
            this.checked_cb.Name = "checked_cb";
            this.checked_cb.NormlBack = null;
            this.checked_cb.SelectedDownBack = null;
            this.checked_cb.SelectedMouseBack = null;
            this.checked_cb.SelectedNormlBack = null;
            this.checked_cb.Size = new System.Drawing.Size(118, 35);
            this.checked_cb.TabIndex = 5;
            this.checked_cb.Text = "已核对";
            this.checked_cb.UseVisualStyleBackColor = false;
            this.checked_cb.CheckedChanged += new System.EventHandler(this.checked_cb_CheckedChanged);
            // 
            // nonChecked_cb
            // 
            this.nonChecked_cb.AutoSize = true;
            this.nonChecked_cb.BackColor = System.Drawing.Color.Transparent;
            this.nonChecked_cb.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.nonChecked_cb.DownBack = null;
            this.nonChecked_cb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nonChecked_cb.Location = new System.Drawing.Point(77, 962);
            this.nonChecked_cb.MouseBack = null;
            this.nonChecked_cb.Name = "nonChecked_cb";
            this.nonChecked_cb.NormlBack = null;
            this.nonChecked_cb.SelectedDownBack = null;
            this.nonChecked_cb.SelectedMouseBack = null;
            this.nonChecked_cb.SelectedNormlBack = null;
            this.nonChecked_cb.Size = new System.Drawing.Size(142, 35);
            this.nonChecked_cb.TabIndex = 6;
            this.nonChecked_cb.Text = "未在图内";
            this.nonChecked_cb.UseVisualStyleBackColor = false;
            this.nonChecked_cb.CheckedChanged += new System.EventHandler(this.nonChecked_cb_CheckedChanged);
            // 
            // AllTrainsInCommand_lbl
            // 
            this.AllTrainsInCommand_lbl.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AllTrainsInCommand_lbl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.AllTrainsInCommand_lbl.Location = new System.Drawing.Point(290, 145);
            this.AllTrainsInCommand_lbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.AllTrainsInCommand_lbl.Name = "AllTrainsInCommand_lbl";
            this.AllTrainsInCommand_lbl.Size = new System.Drawing.Size(146, 62);
            this.AllTrainsInCommand_lbl.TabIndex = 30;
            this.AllTrainsInCommand_lbl.Text = "0";
            this.AllTrainsInCommand_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(450, 162);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(158, 31);
            this.label14.TabIndex = 29;
            this.label14.Text = "当前筛选数量";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(70, 162);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 31);
            this.label1.TabIndex = 31;
            this.label1.Text = "客调命令总车次数：";
            // 
            // currentShow_lbl
            // 
            this.currentShow_lbl.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.currentShow_lbl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.currentShow_lbl.Location = new System.Drawing.Point(620, 145);
            this.currentShow_lbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.currentShow_lbl.Name = "currentShow_lbl";
            this.currentShow_lbl.Size = new System.Drawing.Size(146, 62);
            this.currentShow_lbl.TabIndex = 32;
            this.currentShow_lbl.Text = "0";
            this.currentShow_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // search_tb
            // 
            this.search_tb.Location = new System.Drawing.Point(897, 162);
            this.search_tb.Margin = new System.Windows.Forms.Padding(6);
            this.search_tb.Name = "search_tb";
            this.search_tb.Size = new System.Drawing.Size(262, 35);
            this.search_tb.TabIndex = 34;
            this.search_tb.TextChanged += new System.EventHandler(this.search_tb_TextChanged);
            // 
            // secondListTitle_lbl
            // 
            this.secondListTitle_lbl.AutoSize = true;
            this.secondListTitle_lbl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.secondListTitle_lbl.Location = new System.Drawing.Point(775, 162);
            this.secondListTitle_lbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.secondListTitle_lbl.Name = "secondListTitle_lbl";
            this.secondListTitle_lbl.Size = new System.Drawing.Size(110, 31);
            this.secondListTitle_lbl.TabIndex = 33;
            this.secondListTitle_lbl.Text = "搜索车次";
            // 
            // data_lv
            // 
            this.data_lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Match,
            this.trainIndex,
            this.trainNumber,
            this.secondTrainNumber,
            this._normalOrAdded,
            this._streamStatus,
            this._trainType,
            this._trainModel,
            this._trainID});
            this.data_lv.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data_lv.FullRowSelect = true;
            this.data_lv.GridLines = true;
            this.data_lv.HideSelection = false;
            this.data_lv.Location = new System.Drawing.Point(76, 224);
            this.data_lv.Name = "data_lv";
            this.data_lv.Size = new System.Drawing.Size(1083, 622);
            this.data_lv.TabIndex = 35;
            this.data_lv.UseCompatibleStateImageBehavior = false;
            this.data_lv.View = System.Windows.Forms.View.Details;
            // 
            // Match
            // 
            this.Match.Text = "位于";
            this.Match.Width = 40;
            // 
            // trainIndex
            // 
            this.trainIndex.Text = "匹配";
            this.trainIndex.Width = 40;
            // 
            // trainNumber
            // 
            this.trainNumber.Text = "车次";
            this.trainNumber.Width = 70;
            // 
            // secondTrainNumber
            // 
            this.secondTrainNumber.Text = "车次(2)";
            this.secondTrainNumber.Width = 70;
            // 
            // _streamStatus
            // 
            this._streamStatus.Text = "开/停";
            this._streamStatus.Width = 70;
            // 
            // _trainType
            // 
            this._trainType.Text = "种类";
            this._trainType.Width = 70;
            // 
            // _trainModel
            // 
            this._trainModel.Text = "车型";
            this._trainModel.Width = 70;
            // 
            // _trainID
            // 
            this._trainID.Text = "车号";
            this._trainID.Width = 120;
            // 
            // operationChanged_rtb
            // 
            this.operationChanged_rtb.ContextMenuStrip = this.contextMenuStrip1;
            this.operationChanged_rtb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.operationChanged_rtb.Location = new System.Drawing.Point(1236, 160);
            this.operationChanged_rtb.Name = "operationChanged_rtb";
            this.operationChanged_rtb.Size = new System.Drawing.Size(508, 686);
            this.operationChanged_rtb.TabIndex = 36;
            this.operationChanged_rtb.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label2.Location = new System.Drawing.Point(1230, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 42);
            this.label2.TabIndex = 37;
            this.label2.Text = "调图(每日)统计：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Tomato;
            this.label3.Location = new System.Drawing.Point(70, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 42);
            this.label3.TabIndex = 38;
            this.label3.Text = "基本统计：";
            // 
            // _normalOrAdded
            // 
            this._normalOrAdded.Text = "列车类型";
            this._normalOrAdded.Width = 68;
            // 
            // stop_cb
            // 
            this.stop_cb.AutoSize = true;
            this.stop_cb.BackColor = System.Drawing.Color.Transparent;
            this.stop_cb.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.stop_cb.DownBack = null;
            this.stop_cb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stop_cb.Location = new System.Drawing.Point(301, 962);
            this.stop_cb.MouseBack = null;
            this.stop_cb.Name = "stop_cb";
            this.stop_cb.NormlBack = null;
            this.stop_cb.SelectedDownBack = null;
            this.stop_cb.SelectedMouseBack = null;
            this.stop_cb.SelectedNormlBack = null;
            this.stop_cb.Size = new System.Drawing.Size(94, 35);
            this.stop_cb.TabIndex = 39;
            this.stop_cb.Text = "停开";
            this.stop_cb.UseVisualStyleBackColor = false;
            this.stop_cb.CheckedChanged += new System.EventHandler(this.stop_cb_CheckedChanged);
            // 
            // temp_cb
            // 
            this.temp_cb.AutoSize = true;
            this.temp_cb.BackColor = System.Drawing.Color.Transparent;
            this.temp_cb.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.temp_cb.DownBack = null;
            this.temp_cb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.temp_cb.Location = new System.Drawing.Point(514, 962);
            this.temp_cb.MouseBack = null;
            this.temp_cb.Name = "temp_cb";
            this.temp_cb.NormlBack = null;
            this.temp_cb.SelectedDownBack = null;
            this.temp_cb.SelectedMouseBack = null;
            this.temp_cb.SelectedNormlBack = null;
            this.temp_cb.Size = new System.Drawing.Size(94, 35);
            this.temp_cb.TabIndex = 40;
            this.temp_cb.Text = "临客";
            this.temp_cb.UseVisualStyleBackColor = false;
            this.temp_cb.CheckedChanged += new System.EventHandler(this.temp_cb_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Tomato;
            this.label4.Location = new System.Drawing.Point(70, 861);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(976, 36);
            this.label4.TabIndex = 41;
            this.label4.Text = "筛选类型(示例:已核对 并 开行 的 普通 旅客列车，为当日在图非临客载客列车)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(240, 946);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 31);
            this.label5.TabIndex = 42;
            this.label5.Text = "并";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(426, 946);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 31);
            this.label6.TabIndex = 43;
            this.label6.Text = "的";
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.BaseColor = System.Drawing.Color.DarkGreen;
            this.skinButton1.BorderColor = System.Drawing.Color.ForestGreen;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton1.ForeColor = System.Drawing.Color.White;
            this.skinButton1.Location = new System.Drawing.Point(1227, 920);
            this.skinButton1.Margin = new System.Windows.Forms.Padding(6);
            this.skinButton1.MouseBack = null;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(1067, 82);
            this.skinButton1.TabIndex = 45;
            this.skinButton1.Text = "创建统计Excel文件";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.DarkGreen;
            this.label7.Location = new System.Drawing.Point(1230, 861);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(735, 36);
            this.label7.TabIndex = 46;
            this.label7.Text = "“创建统计Excel文件”可将所有数据分类列于Excel表格内\r\n";
            // 
            // unrecognizedTrain_rtb
            // 
            this.unrecognizedTrain_rtb.ContextMenuStrip = this.contextMenuStrip1;
            this.unrecognizedTrain_rtb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.unrecognizedTrain_rtb.Location = new System.Drawing.Point(1786, 160);
            this.unrecognizedTrain_rtb.Name = "unrecognizedTrain_rtb";
            this.unrecognizedTrain_rtb.ReadOnly = true;
            this.unrecognizedTrain_rtb.Size = new System.Drawing.Size(508, 304);
            this.unrecognizedTrain_rtb.TabIndex = 47;
            this.unrecognizedTrain_rtb.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label8.Location = new System.Drawing.Point(1779, 99);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(488, 41);
            this.label8.TabIndex = 48;
            this.label8.Text = "不在图车次(其他车站或未匹配)：";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制toolStripMenuItem1,
            this.粘贴ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 118);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 复制toolStripMenuItem1
            // 
            this.复制toolStripMenuItem1.Name = "复制toolStripMenuItem1";
            this.复制toolStripMenuItem1.Size = new System.Drawing.Size(136, 38);
            this.复制toolStripMenuItem1.Text = "复制";
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(136, 38);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(136, 38);
            this.清空ToolStripMenuItem.Text = "清空";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notMatchedTrains_rtb
            // 
            this.notMatchedTrains_rtb.ContextMenuStrip = this.contextMenuStrip1;
            this.notMatchedTrains_rtb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.notMatchedTrains_rtb.Location = new System.Drawing.Point(1786, 546);
            this.notMatchedTrains_rtb.Name = "notMatchedTrains_rtb";
            this.notMatchedTrains_rtb.ReadOnly = true;
            this.notMatchedTrains_rtb.Size = new System.Drawing.Size(508, 300);
            this.notMatchedTrains_rtb.TabIndex = 50;
            this.notMatchedTrains_rtb.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label9.Location = new System.Drawing.Point(1779, 484);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(370, 41);
            this.label9.TabIndex = 51;
            this.label9.Text = "时刻表内客调不含车次：";
            // 
            // DataAnalyse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.White;
            this.CaptionBackColorBottom = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(2384, 1112);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.notMatchedTrains_rtb);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.unrecognizedTrain_rtb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.temp_cb);
            this.Controls.Add(this.stop_cb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.operationChanged_rtb);
            this.Controls.Add(this.data_lv);
            this.Controls.Add(this.search_tb);
            this.Controls.Add(this.secondListTitle_lbl);
            this.Controls.Add(this.currentShow_lbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AllTrainsInCommand_lbl);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.nonChecked_cb);
            this.Controls.Add(this.checked_cb);
            this.Controls.Add(this.nonPsngerTrain_cb);
            this.Controls.Add(this.psngerTrain_cb);
            this.Controls.Add(this.normal_cb);
            this.Controls.Add(this.start_cb);
            this.Name = "DataAnalyse";
            this.Text = "核对数据";
            this.Load += new System.EventHandler(this.DataAnalyse_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CCWin.SkinControl.SkinCheckBox start_cb;
        private CCWin.SkinControl.SkinCheckBox normal_cb;
        private CCWin.SkinControl.SkinCheckBox psngerTrain_cb;
        private CCWin.SkinControl.SkinCheckBox nonPsngerTrain_cb;
        private CCWin.SkinControl.SkinCheckBox checked_cb;
        private CCWin.SkinControl.SkinCheckBox nonChecked_cb;
        private System.Windows.Forms.Label AllTrainsInCommand_lbl;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentShow_lbl;
        private System.Windows.Forms.TextBox search_tb;
        private System.Windows.Forms.Label secondListTitle_lbl;
        private System.Windows.Forms.ListView data_lv;
        private System.Windows.Forms.ColumnHeader trainIndex;
        private System.Windows.Forms.ColumnHeader Match;
        private System.Windows.Forms.ColumnHeader trainNumber;
        private System.Windows.Forms.ColumnHeader secondTrainNumber;
        private System.Windows.Forms.ColumnHeader _streamStatus;
        private System.Windows.Forms.ColumnHeader _trainType;
        private System.Windows.Forms.ColumnHeader _trainModel;
        private System.Windows.Forms.ColumnHeader _trainID;
        private System.Windows.Forms.RichTextBox operationChanged_rtb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader _normalOrAdded;
        private CCWin.SkinControl.SkinCheckBox stop_cb;
        private CCWin.SkinControl.SkinCheckBox temp_cb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private CCWin.SkinControl.SkinButton skinButton1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox unrecognizedTrain_rtb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox notMatchedTrains_rtb;
        private System.Windows.Forms.Label label9;
    }
}