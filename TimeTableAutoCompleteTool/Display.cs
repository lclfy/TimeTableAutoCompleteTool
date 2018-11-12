using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TimeTableAutoCompleteTool.Model;
using TimeTableAutoCompleteTool.Modify;

namespace TimeTableAutoCompleteTool
{
    public partial class Display : Form
    {
        public List<PowerSupplyModel> psModel;
        public List<TrackLine> tLine;
        public List<TrackPoint> tPoint;
        public List<Signal> signal;
        public List<Button> allButtons;
        Graphics graphic;
        bool pointShown = false;
        bool allPointsShown = false;
        bool showSettings = false;
        bool showFunctionalPoints = true;
        float zoomX = 0;
        public List<string> shownPowerSupplyModelName = new List<string>();
        int startTrackNum = 0;
        int stopTrackNum = 0;
        float zoomCount = 1;
        Point mouseOff = new Point();
        public Point movingPixels = new Point();
        public Point lastMovingPlace = new Point();
        bool leftFlag = false;
        bool checkSignalAndPS = false;

        public Display()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Controls.Remove(buttons_pnl);
            loadData();
            checkEmptyObject();
            createButtons();
            checkSettings();
            checkResolution();
            RelocateButtons();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();
            this.AutoScrollMinSize = new Size(1920, 1080);
        }

        private void checkResolution()
        {
            int SH = SystemInformation.WorkingArea.Height - 100;
            int SW = SystemInformation.WorkingArea.Width;
            zoomX = (float)(1920.0/SW);
            this.Size = new Size(SW, SH);

        }

        private float transformScreenOffsets(int arg, bool XorY)
        {//计算屏幕偏移量绘制坐标
            if (XorY)
            {//是X
                return (arg / zoomX) + movingPixels.X;
            }
            else
            {
                return (arg / zoomX) + movingPixels.Y;
            }
        }

        private float transformScreenOffsets(float arg, bool XorY)
        {//计算屏幕偏移量绘制坐标
            if (XorY)
            {//是X
                return (arg / zoomX) + movingPixels.X;
            }
            else
            {
                return (arg / zoomX) + movingPixels.Y;
            }
        }

        public void checkEmptyObject()
        {
            if(tLine == null)
            {
                tLine = new List<TrackLine>();
            }
            if(tPoint == null)
            {
                tPoint = new List<TrackPoint>();
            }
            if(psModel == null)
            {
                psModel = new List<PowerSupplyModel>();
            }
            if(signal == null)
            {
                signal = new List<Signal>();
            }
            for (int j = 0; j < tLine.Count; j++)
            {//检查线里面有没有无用点

                if (tLine[j].leftTrackPoint != null)
                {
                    bool hasGotIt = false;
                    for (int i = 0; i < tPoint.Count; i++)
                    {
                        if (tPoint[i].trackPointID.Equals(tLine[j].leftTrackPoint.trackPointID))
                        {
                            hasGotIt = true;
                        }
                        if (hasGotIt)
                        {
                            break;
                        }
                    }
                    if (!hasGotIt)
                    {
                        tLine[j].leftTrackPoint = null;
                    }
                }
                if (tLine[j].rightTrackPoint != null)
                {
                    bool hasGotIt = false;
                    for (int i = 0; i < tPoint.Count; i++)
                    {
                        if (tPoint[i].trackPointID.Equals(tLine[j].rightTrackPoint.trackPointID))
                        {
                            hasGotIt = true;
                        }
                        if (hasGotIt)
                        {
                            break;
                        }
                    }
                    if (!hasGotIt)
                    {
                        tLine[j].rightTrackPoint = null;
                    }
                }

            }

            
        
            //检查点里面有没有无用线
            for(int iPoint  = 0;iPoint <tPoint.Count; iPoint++)
            {
                bool firstGotIt = false;
                bool secondGotIt = false;
                bool thirdGotIt = false;
                foreach(TrackLine _tLine in tLine)
                {
                    if (_tLine.trackLineID == tPoint[iPoint].firstTrackLine)
                    {
                        firstGotIt = true;
                    }
                    if (_tLine.trackLineID  == tPoint[iPoint].secondTrackLine)
                    {
                        secondGotIt = true;
                    }
                    if (_tLine.trackLineID == tPoint[iPoint].thirdTrackLine)
                    {
                        thirdGotIt = true;
                    }
                    if(firstGotIt && secondGotIt && thirdGotIt)
                    {
                        break;
                    }
                }
                if (!firstGotIt)
                {
                    tPoint[iPoint].firstTrackLine = 0;
                }
                if (!secondGotIt)
                {
                    tPoint[iPoint].secondTrackLine =  0;
                }
                if (!thirdGotIt)
                {
                    tPoint[iPoint].thirdTrackLine = 0;
                }
            }


            for(int iPS = 0; iPS < psModel.Count; iPS++)
            {
                //检查供电臂内有没有无用线
                for (int iPSLine = 0; iPSLine < psModel[iPS].containedTrackLine.Count; iPSLine++)
                {
                    bool hasGotIt = false;
                    foreach (TrackLine _tl in tLine)
                    {
                        if(psModel[iPS].containedTrackLine[iPSLine].trackLineID == _tl.trackLineID)
                        {
                            hasGotIt = true;
                        }
                        if (hasGotIt)
                        {
                            break;
                        }
                    }
                    if (!hasGotIt)
                    {
                        psModel[iPS].containedTrackLine.RemoveAt(iPSLine);
                        iPSLine--;
                    }
                }

                //检查供电臂内有没有无用点
                for(int iPSPoint = 0;iPSPoint < psModel[iPS].containedTrackPoint.Count; iPSPoint++)
                {
                    bool hasGotIt = false;
                    foreach (TrackPoint _tp in tPoint)
                    {
                        if (psModel[iPS].containedTrackPoint[iPSPoint].trackPointID.Equals(_tp.trackPointID))
                        {
                            hasGotIt = true;
                        }
                        if (hasGotIt)
                        {
                            break;
                        }
                    }
                    if (!hasGotIt)
                    {
                        psModel[iPS].containedTrackPoint.RemoveAt(iPSPoint);
                        iPSPoint--;
                    }
                }

                //检查供电臂内有没有无用信号机
                if(psModel[iPS].functionalTrackPoint == null)
                {
                    psModel[iPS].functionalTrackPoint = new List<Signal>();
                }
                for (int iPSSignal = 0; iPSSignal < psModel[iPS].functionalTrackPoint.Count; iPSSignal++)
                {
                    bool hasGotIt = false;
                    foreach (Signal _tp in signal)
                    {
                        if (psModel[iPS].functionalTrackPoint[iPSSignal].signalID.Equals(_tp.signalID))
                        {
                            hasGotIt = true;
                        }
                        if (hasGotIt)
                        {
                            break;
                        }
                    }
                    if (!hasGotIt)
                    {
                        if (psModel[iPS].functionalTrackPoint != null)
                        {
                            psModel[iPS].functionalTrackPoint.RemoveAt(iPSSignal);
                            iPSSignal--;
                        }
                    }
                }
            }


        }

        private void checkSettings()
        {
            if (showSettings)
            {
                modifyPowerSupplyModel_btn.Visible = true;
                modifyTrackLine_btn.Visible = true;
                modifyTrackPoint_btn.Visible = true;
                save_btn.Visible = true;
                showPoint_btn.Visible = true;
                title_tb.Visible = true;
                modifySignal_btn.Visible = true;
                //标题
                label1.Visible = true;
                //站台范围
                label2.Visible = true;
                //-号
                label3.Visible = true;
                startTrackNum_tb.Visible = true;
                stopTrackNum_tb.Visible = true;
            }
            else
            {
                modifyPowerSupplyModel_btn.Visible = false;
                modifyTrackLine_btn.Visible = false;
                modifyTrackPoint_btn.Visible = false;
                save_btn.Visible = false;
                showPoint_btn.Visible = false;
                modifySignal_btn.Visible = false;
                title_tb.Visible = false;
                label1.Visible = false;
                //标题
                label1.Visible = false;
                //站台范围
                label2.Visible = false;
                //-号
                label3.Visible = false;
                startTrackNum_tb.Visible = false;
                stopTrackNum_tb.Visible = false;
            }
        }

        private void loadData()
        {
            ModelData _data = new ModelData();
            IFormatter formatter = new BinaryFormatter();
            formatter.Binder = new UBinder();
            if (true)
            {
                try
                {
                    Stream stream = new FileStream(Application.StartupPath + "\\Data\\modelData.bin", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    _data = (ModelData)formatter.Deserialize(stream);
                    stream.Close();
                }
                catch (Exception e)
                {
                    int ij = 0;
                }
                try
                {
                    tPoint = _data.tPoint;
                }
                catch (Exception e)
                {
                    tPoint = new List<TrackPoint>();
                }
                try
                {
                    psModel = _data.psModel;
                }
                catch (Exception e)
                {
                    psModel = new List<PowerSupplyModel>();
                }
                foreach (PowerSupplyModel _ps in psModel)
                {
                    if (_ps.powerSupplyRange == null)
                    {
                        _ps.powerSupplyRange = "";
                    }
                }
                try
                {
                    tLine = _data.tLine;
                }
                catch (Exception e)
                {
                    tLine = new List<TrackLine>();
                }
                try
                {
                    signal = _data.signal;
                }
                catch (Exception e)
                {
                    signal = new List<Signal>();
                }
                foreach (Signal _s in signal)
                {
                    if (_s.tip == null)
                    {
                        _s.tip = "";
                    }
                }
                if (_data.title != null)
                {
                    title_tb.Text = _data.title;
                    title_lbl.Text = _data.title;
                }
                if (_data.startTrackNum != 0)
                {
                    startTrackNum = _data.startTrackNum;
                    startTrackNum_tb.Text = startTrackNum.ToString();
                }
                if (_data.stopTrackNum != 0)
                {
                    stopTrackNum = _data.stopTrackNum;
                    stopTrackNum_tb.Text = stopTrackNum.ToString();
                }
                if (tLine != null)
                {
                    tLine.Sort();
                }
                if (tPoint != null)
                {
                    tPoint.Sort();
                }
            }


            if (true)
            {
                try
                {
                    Stream streamLine = new FileStream(Application.StartupPath + "\\Data\\tLine.bin", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    tLine = (List<TrackLine>)formatter.Deserialize(streamLine);
                    streamLine.Close();
                    tLine.Sort();
                }
                catch (Exception e1)
                {
                    tPoint = new List<TrackPoint>();
                }
                try
                {
                    Stream streamPoint = new FileStream(Application.StartupPath + "\\Data\\tPoint.bin", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    tPoint = (List<TrackPoint>)formatter.Deserialize(streamPoint);
                    streamPoint.Close();
                    tPoint.Sort();
                }
                catch (Exception e1)
                {
                    tPoint = new List<TrackPoint>();
                }
                try
                {
                    Stream streamPS = new FileStream(Application.StartupPath + "\\Data\\psModel.bin", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    psModel = (List<PowerSupplyModel>)formatter.Deserialize(streamPS);
                    streamPS.Close();
                }
                catch (Exception e2)
                {
                    psModel = new List<PowerSupplyModel>();
                }
                try
                {
                    Stream streamSignal = new FileStream(Application.StartupPath + "\\Data\\signal.bin", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    signal = (List<Signal>)formatter.Deserialize(streamSignal);
                    streamSignal.Close();
                }
                catch (Exception e2)
                {
                    signal = new List<Signal>();
                }
            }
            
        }
        
        private void createButtons()
        {
            allButtons = new List<Button>();
            int count = 0;
            foreach(PowerSupplyModel _ps in psModel)
            {
                Button btn = new Button();
                btn.Parent = buttons_pnl;
                btn.Size = new Size(150,65);
                btn.Location = new Point(400 + 170*count, this.Height - 200);
                btn.BackColor = Color.White;
                btn.Name = _ps.powerSupplyID.ToString();
                btn.Text = _ps.powerSupplyName;
                btn.Font = new Font("微软雅黑", 12.0f, FontStyle.Bold);
                btn.Click += new EventHandler(btn_Click);
                this.Controls.Add(btn);
                allButtons.Add(btn);
                count++;
            }

        }

        public void refreshButtons()
        {
            foreach(Control ctl in this.Controls)
            {
                if (ctl is Button)//挑选出是按钮类型的
                {
                   foreach(Button btn in allButtons)
                    {
                        if(ctl.Text.Equals(btn.Text))
                        {
                            this.Controls.Remove(ctl);
                        }
                    }
                }
            }
            createButtons();
        }

        void btn_Click(object sender, EventArgs e)
        {//点击相应供电臂后绘制相应内容
            //MessageBox.Show("" + ((Button)sender).Name);
            string name = ((Button)sender).Text;
            bool isOpening = false;
            foreach(string _name in shownPowerSupplyModelName)
            {//如果有的话说明已经添加过了
                if (_name.Equals(name))
                {
                    isOpening = true;
                    shownPowerSupplyModelName.Remove(_name);
                    break;
                }
            }
            if (!isOpening)
            {
                shownPowerSupplyModelName.Add(name);
                ((Button)sender).BackColor = Color.Red;
                ((Button)sender).ForeColor = Color.White;
            }
            foreach(Button btn in allButtons)
            {//检查其他按钮情况
                bool hasGotIt = false;
                foreach(string _name in shownPowerSupplyModelName)
                {
                    if (btn.Text.Equals(_name))
                    {
                        hasGotIt = true;
                        break;
                    }
                }
                if (!hasGotIt)
                {
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.Black;
                }
            }
            this.Refresh();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否关闭程序？已做的修改将被保存。", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                saveData();
                e.Cancel = false;  //点击OK   
            }
            else
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }

        private bool saveData(string saveText = "")
        {
            ModelData _dt = new ModelData();
            _dt.startTrackNum = startTrackNum;
            _dt.stopTrackNum = stopTrackNum;
            _dt.tLine = this.tLine;
            _dt.tPoint = this.tPoint;
            _dt.psModel = this.psModel;
            _dt.signal = this.signal;
            _dt.title = title_lbl.Text;
            
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Application.StartupPath + "\\Data\\modelData.bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                formatter.Serialize(stream, _dt);
                stream.Close();
                /*
                Stream streamLine = new FileStream(Application.StartupPath + "\\Data\\tLine.bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                formatter.Serialize(streamLine, tLine);
                streamLine.Close();
                Stream streamPoint = new FileStream(Application.StartupPath + "\\Data\\tPoint.bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                formatter.Serialize(streamPoint, tPoint);
                streamPoint.Close();
                Stream streamPS = new FileStream(Application.StartupPath + "\\Data\\psModel.bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                formatter.Serialize(streamPS, psModel);
                streamPS.Close();
                Stream streamSignal = new FileStream(Application.StartupPath + "\\Data\\signal.bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                formatter.Serialize(streamSignal, signal);
                streamSignal.Close();
                */
                
                return true;
            }catch(Exception e)
            {
                return false;
            }

        }

        private void paintLine(TrackLine _tl)
        {
            if(_tl == null)
            {
                return;
            }
            else if (_tl.selfLeftPoint == null ||
                        _tl.selfRightPoint == null)
            {
                    return;
            }
            Point point1 = _tl.selfLeftPoint;
            Point point2 = _tl.selfRightPoint;
            String pointText = _tl.trackLineID.ToString();
            Pen p = new Pen(Color.Gray, 3);
            graphic.DrawLine(p, transformScreenOffsets(point1.X,true), transformScreenOffsets(point1.Y,false), transformScreenOffsets(point2.X,true), transformScreenOffsets(point2.Y,false));
            Font font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
            //graphic.DrawString(pointText, base.Font, Brushes.White, 0, 0, new StringFormat(StringFormatFlags.DirectionVertical));
            Font font1 = new Font("微软雅黑", 8.0f, FontStyle.Bold);
            Font font2 = new Font("微软雅黑", 12.0f, FontStyle.Bold);
            //graphic.DrawString(_tl.selfLeftPoint.ToString(), font1, Brushes.Yellow, point1.X, point1.Y - 20, new StringFormat(StringFormatFlags.DirectionVertical));
            //graphic.DrawString(_tl.selfRightPoint.ToString(), font1, Brushes.Yellow, point2.X, point2.Y - 20, new StringFormat(StringFormatFlags.DirectionVertical));
            bool hasGotIt = false;
            //方向文字

            if (_tl.leftWayTo != null &&_tl.leftWayTo.Length != 0)
            {
                graphic.DrawString("⇋ " + _tl.leftWayTo, font2, Brushes.Green, transformScreenOffsets(point1.X-40,true), transformScreenOffsets((((point1.Y + point2.Y) / 2) - 35),false));
            }
            if (_tl.rightWayTo != null &&_tl.rightWayTo.Length != 0)
            {
                graphic.DrawString(_tl.rightWayTo + "⇌", font2, Brushes.Green, transformScreenOffsets(point2.X-40,true), transformScreenOffsets((((point1.Y + point2.Y) / 2) - 35),false));
            }
            if (startTrackNum <= _tl.trackLineID && _tl.trackLineID <= stopTrackNum )
            {
                //真正的站台字符画中间
                graphic.DrawString(pointText, font, Brushes.White, transformScreenOffsets(((1920 /2)-20),true), transformScreenOffsets((((point1.Y + point2.Y) / 2) - 20),false));
                hasGotIt = true;
            }
            if(startTrackNum == 0 && stopTrackNum == 0)
            {
                graphic.DrawString(pointText, font, Brushes.White, transformScreenOffsets(((point1.X + point2.X) / 2),true), transformScreenOffsets((((point1.Y + point2.Y) / 2) - 20),false));
            }
            if (pointShown)
            {//绘制坐标&有效绘画区域
                if (!hasGotIt)
                {
                    graphic.DrawString(pointText, font, Brushes.White, transformScreenOffsets(((point1.X + point2.X) / 2),true), transformScreenOffsets((((point1.Y + point2.Y) / 2) - 20),false));
                }
                graphic.DrawString("(" + _tl.selfLeftPoint.X.ToString() + ",\n" + _tl.selfLeftPoint.Y.ToString() + ")", font1, Brushes.Yellow, transformScreenOffsets(point1.X,true), transformScreenOffsets((point1.Y - 30),false));
                graphic.DrawString("(" + _tl.selfRightPoint.X.ToString() + ",\n" + _tl.selfRightPoint.Y.ToString() + ")", font1, Brushes.Yellow, transformScreenOffsets(point2.X,true), transformScreenOffsets((point2.Y - 30),false));
                Pen p1 = new Pen(Color.Yellow, 5);
                /*
                graphic.DrawLine(p, 0, 100 / zoomX, 1920 / zoomX, 100 / zoomX);
                graphic.DrawLine(p, 0, 850 / zoomX, 1920 / zoomX, 850 / zoomX);
                graphic.DrawString("上边界建议(Y=100)", font1, Brushes.Yellow, 940/zoomX, 80 / zoomX);
                graphic.DrawString("下边界建议(Y=850)", font1, Brushes.Yellow, 940/zoomX, 830 / zoomX);
                */
            }

        }

        private void paintPoint(TrackPoint _tp, bool isOnShow = false)
        {//isOnShow为该点是否应该高亮展示
            if(_tp == null)
            {
                return;
            }
            else if(_tp.trackPoint == null)
            {
                return;
            }
            Point point = _tp.trackPoint;
            Font font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
            Font font1 = new Font("微软雅黑", 8.0f, FontStyle.Bold);
            Font fontPoint = new Font("微软雅黑", 7.0f, FontStyle.Bold);
            string lineText = _tp.trackPointID.ToString();
            Pen p;
                if (isOnShow)
                {
                    p = new Pen(Color.Red, 10);
                    graphic.DrawLine(p, transformScreenOffsets((point.X - 5),true), transformScreenOffsets(point.Y ,false), transformScreenOffsets((point.X + 5),true), transformScreenOffsets(point.Y ,false));
                    if (_tp.switchDirection == 1)
                    {
                        graphic.DrawString("锁定位", font1, Brushes.Yellow, transformScreenOffsets((point.X - 10),true), transformScreenOffsets((point.Y + 10),false));
                    }
                    else if (_tp.switchDirection == 2)
                    {
                        graphic.DrawString("锁反位", font1, Brushes.Yellow, transformScreenOffsets((point.X) ,true), transformScreenOffsets((point.Y + 10) ,false));
                    }
                else
                {
                    p = new Pen(Color.Green, 8);
                    graphic.DrawLine(p, transformScreenOffsets((point.X - 4),true), transformScreenOffsets((point.Y),false), transformScreenOffsets((point.X + 4),true), transformScreenOffsets(point.Y,false));
                }
            }
            graphic.DrawString(lineText.ToString(), font, Brushes.White, transformScreenOffsets(point.X,true), transformScreenOffsets((point.Y - 20),false));
            if (allPointsShown && !isOnShow)
            {
                graphic.DrawString("(" +point.X.ToString() + "," + point.Y.ToString() + ")", fontPoint, Brushes.Yellow, transformScreenOffsets(point.X,true), transformScreenOffsets((point.Y - 30),false));
            }
        }

        private void paintSignalTower(Signal _s ,  bool isOnShow = false)
        {
            //isOnShow为该点是否应该高亮展示
            if (_s == null)
            {
                return;
            }
            else if (_s.signalPoint == null)
            {
                return;
            }
            Point point = _s.signalPoint;
            Font font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
            Font fontPoint = new Font("微软雅黑", 7.0f, FontStyle.Bold);
            string lineText = _s.signalID.ToString();
            Pen p;
            float textLocation = (point.X - 10);

                if (isOnShow && showFunctionalPoints)
                {
                    lineText += "-封闭";
                    p = new Pen(Color.Red, 20);
                    graphic.DrawLine(p, transformScreenOffsets((point.X - 10) ,true), transformScreenOffsets(point.Y,false), transformScreenOffsets((point.X + 10) ,true), transformScreenOffsets(point.Y ,false));
                }
                else
                {
                    p = new Pen(Color.Green, 20);
                    graphic.DrawLine(p, transformScreenOffsets((point.X - 10),true), transformScreenOffsets((point.Y),false), transformScreenOffsets((point.X + 10) ,true), transformScreenOffsets(point.Y,false));
                }
            graphic.DrawString(lineText.ToString(), font, Brushes.White, transformScreenOffsets(textLocation ,true), transformScreenOffsets((point.Y - 25),false));
            if (allPointsShown && !isOnShow)
            {
                graphic.DrawString("(" + point.X.ToString() + "," + point.Y.ToString() + ")", fontPoint, Brushes.Yellow, transformScreenOffsets(point.X,true), transformScreenOffsets((point.Y - 25),false));
            }
        }

        private void paintPowerSupply(PowerSupplyModel _ps)
        {
            if (_ps == null)
            {
                return;
            }
            else if (_ps.containedTrackLine == null)
            {
                return;
            }
            Pen p = new Pen(Color.Purple, 6);
            foreach(TrackLine _tl in _ps.containedTrackLine)
            {
                /*
                PointF[] sbxAll = new PointF[]{
                new PointF(_tl.selfLeftPoint.X/zoomX, (_tl.selfLeftPoint.Y + 15)/zoomX),
                new PointF(_tl.selfRightPoint.X/zoomX, (_tl.selfRightPoint.Y + 15)/zoomX),
               new PointF(_tl.selfRightPoint.X/zoomX, (_tl.selfRightPoint.Y - 15)/zoomX),
                new PointF(_tl.selfLeftPoint.X/zoomX, (_tl.selfLeftPoint.Y - 15)/zoomX)
                };
                PointF[] sbxLeftIn = new PointF[]{
                new PointF(_tl.selfLeftPoint.X/zoomX, (_tl.selfLeftPoint.Y + 15)/zoomX),
                new PointF(((_tl.selfRightPoint.X + _tl.selfLeftPoint.X)/2)/zoomX, ((_tl.selfLeftPoint.Y+_tl.selfRightPoint.Y)/2 + 15)/zoomX),
                new PointF(((_tl.selfRightPoint.X + _tl.selfLeftPoint.X)/2)/zoomX, ((_tl.selfLeftPoint.Y+_tl.selfRightPoint.Y)/2 - 15)/zoomX),
                new PointF(_tl.selfLeftPoint.X/zoomX, (_tl.selfLeftPoint.Y - 15)/zoomX)
                };
                PointF[] sbxRightIn = new PointF[]{
                new PointF(((_tl.selfRightPoint.X + _tl.selfLeftPoint.X)/2)/zoomX, ((_tl.selfLeftPoint.Y+_tl.selfRightPoint.Y)/2 + 15)/zoomX),
                new PointF(_tl.selfRightPoint.X/zoomX, (_tl.selfRightPoint.Y + 15)/zoomX),
                new PointF(_tl.selfRightPoint.X/zoomX, (_tl.selfRightPoint.Y - 15)/zoomX),
                new PointF(((_tl.selfRightPoint.X + _tl.selfLeftPoint.X)/2)/zoomX, ((_tl.selfLeftPoint.Y+_tl.selfRightPoint.Y)/2 - 15)/zoomX)
                };
                if (_tl.containsInPS == 0)
                {//绘制全部区域(上15，下15)
                    graphic.FillPolygon(new SolidBrush(Color.FromArgb(125, Color.LightYellow)), sbxAll);
                }else if(_tl.containsInPS == 1)
                {//绘制左半在内
                    graphic.FillPolygon(new SolidBrush(Color.FromArgb(125, Color.LightYellow)), sbxLeftIn);
                }
                else if (_tl.containsInPS == 2)
                {//绘制右半在内
                    graphic.FillPolygon(new SolidBrush(Color.FromArgb(125, Color.LightYellow)), sbxRightIn);
                }
                */

                if (_tl.containsInPS == 0)
                {//绘制全部区域
                    graphic.DrawLine(p, transformScreenOffsets(_tl.selfLeftPoint.X ,true), transformScreenOffsets(_tl.selfLeftPoint.Y,false), transformScreenOffsets(_tl.selfRightPoint.X ,true), transformScreenOffsets(_tl.selfRightPoint.Y ,false));
                }
                else if (_tl.containsInPS == 1)
                {//绘制左半在内
                    graphic.DrawLine(p, transformScreenOffsets(_tl.selfLeftPoint.X,true), transformScreenOffsets(_tl.selfLeftPoint.Y,false), transformScreenOffsets((_tl.selfRightPoint.X + _tl.selfLeftPoint.X) /2,true), transformScreenOffsets((_tl.selfRightPoint.Y + _tl.selfLeftPoint.Y) /2,false));
                }
                else if (_tl.containsInPS == 2)
                {//绘制右半在内
                    graphic.DrawLine(p, transformScreenOffsets((_tl.selfRightPoint.X + _tl.selfLeftPoint.X) / 2,true), transformScreenOffsets((_tl.selfRightPoint.Y + _tl.selfLeftPoint.Y) /2,false), transformScreenOffsets(_tl.selfRightPoint.X ,true), transformScreenOffsets(_tl.selfRightPoint.Y ,false));
                }

            }

            Font font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            showFunctionalPoints = !showFunctionalPoints;
            this.Refresh();
        }

        public void selfPaint(bool mouseMovingPaint = false, Graphics g = null)
        {

            if (graphic== null)
            {
                if(g == null)
                {
                    graphic = this.CreateGraphics();
                }
                else
                {
                    graphic = g;
                }
            }
            foreach (TrackLine _tl in tLine)
            {
                paintLine(_tl);
            }
            foreach (PowerSupplyModel _ps in psModel)
            {
                foreach (string _name in shownPowerSupplyModelName)
                {
                    if (_ps.powerSupplyName.Equals(_name))
                    {
                        paintPowerSupply(_ps);
                        foreach (TrackPoint _tp in _ps.containedTrackPoint)
                        {
                            paintPoint(_tp, true);
                        }
                        foreach (Signal _s in _ps.functionalTrackPoint)
                        {
                            paintSignalTower(_s, true);
                        }
                    }
                }
            }
            foreach (TrackPoint _tp in tPoint)
            {
                paintPoint(_tp);
            }
            foreach(Signal _s in signal)
            {
                paintSignalTower(_s,false);
            }
            foreach (PowerSupplyModel _ps in psModel)
            {
                foreach (string _name in shownPowerSupplyModelName)
                {
                    if (_ps.powerSupplyName.Equals(_name))
                    {
                        foreach (TrackPoint _tp in _ps.containedTrackPoint)
                        {
                            paintPoint(_tp, true);
                        }
                        foreach (Signal _s in _ps.functionalTrackPoint)
                        {
                            paintSignalTower(_s, true);
                        }
                    }
                }
            }

        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            graphic = e.Graphics;
            graphic.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
            selfPaint(false,e.Graphics);
        }


        public void updateTrackPoint(List<TrackPoint> _tPoint)
        {
            tPoint = _tPoint;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModifyTrackLine form = new ModifyTrackLine(this);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModifyTrackPoint form = new ModifyTrackPoint(this);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ModifyPowerSupplyModel form = new ModifyPowerSupplyModel(this);
            form.Show();
        }

        private void modifySignal_btn_Click(object sender, EventArgs e)
        {
            ModifySignal form = new ModifySignal(this);
            form.Show();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            saveData();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            saveData("自动");
        }

        private void showPoint_btn_Click(object sender, EventArgs e)
        {
            if (!pointShown && !allPointsShown)
            {
                pointShown = true;
                this.Refresh();
            }
            else if(pointShown && !allPointsShown)
            {
                allPointsShown = true;
                this.Refresh();
            }
            else
            {
                pointShown = false;
                allPointsShown = false;
                this.Refresh();
            }
        }

        private void RelocateButtons()
        {
            int count = 0;
            if (allButtons == null)
            {
                return;
            }
            int countMinus = 0;
            foreach (Button btn in allButtons)
            {
                if((zoomX == 1 && count <= 8)||(zoomX > 1 && count <= 6)||(zoomX < 1 && count <= 12))
                {
                    if(zoomX == 1 && allButtons.Count > 8)
                    {
                        countMinus = 8;
                    }else if(zoomX > 1 && allButtons.Count > 6)
                    {
                        countMinus = 6;
                    }else if(zoomX < 1 && allButtons.Count > 12)
                    {
                        countMinus = 12;
                    }
                    else
                    {
                        countMinus = allButtons.Count;
                    }
                    btn.Location = new Point((this.Width - 170 * countMinus) / 2 + 170 * count, this.Height - (int)(this.Height * 0.2));
                }
                else if((zoomX == 1 && count > 8) || (zoomX > 1 && count > 6 ) || (zoomX < 1 && count > 12 ))
                {
                    btn.Location = new Point((this.Width - 170 * (allButtons.Count - countMinus -1)) / 2 + 170 * (count-countMinus-1), this.Height - (int)(this.Height * 0.2) + 70);
                }
                count++;
            }
            setting_btn.Location = new Point(16,  this.Height - 80);
            modifyTrackLine_btn.Location = new Point(16, this.Height - 120);
            modifyTrackPoint_btn.Location = new Point(16, this.Height - 160);
            modifySignal_btn.Location = new Point(16, this.Height - 200);
            modifyPowerSupplyModel_btn.Location = new Point(16, this.Height - 240);
            showPoint_btn.Location = new Point(16, this.Height - 280);
            save_btn.Location = new Point(16, this.Height - 320);
            startTrackNum_tb.Location = new Point(16, this.Height - 350);
            label3.Location = new Point(56, this.Height - 350);
            stopTrackNum_tb.Location = new Point(76, this.Height - 350);
            label2.Location = new Point(16, this.Height - 375);
            title_tb.Location = new Point(16, this.Height - 405);
            label1.Location = new Point(16, this.Height - 430);
            title_lbl.Location = new Point((this.Width / 2)-(title_lbl.Text.Length*35), 30);  
        }

        private void findMouseMovingSignalAndPS(int mouseX, int mouseY)
        {//寻找鼠标经过的信号机和供电臂
         //先找信号机
            //mousePoint_lbl.Text = "当前：";
            bool hasGotIt = false;
            foreach (Signal _s in signal)
            {
                Point point = _s.signalPoint;
                //X1Y1 = leftX , Y - 10; X1Y2 = leftX , Y + 10; X2Y1 = rightX , Y - 10 ; X2Y2 = rightX, Y + 10;
                //如果鼠标在该框内，则 mouseX > leftX && mouseX < rightX && mouseY > Y - 10 && mouseY < Y + 10
                if (mouseX >= transformScreenOffsets((point.X - 10) ,true) &&
                    mouseX <= transformScreenOffsets((point.X + 10) ,true) &&
                    mouseY >= transformScreenOffsets((point.Y - 10) ,false)&&
                    mouseY <= transformScreenOffsets((point.Y + 10) ,false))
                {
                    hasGotIt = true;
                    string tip = "";
                    if(_s.tip.Length == 0)
                    {
                        tip = "未注明";
                    }
                    else
                    {
                        tip = _s.tip;
                    }
                    mousePoint_lbl.Text = "当前："+_s.signalID + "信号机"+"\n公里标："+tip;
                }
                if (hasGotIt)
                {
                    break;
                }
            }
            if (!hasGotIt)
            {//找供电臂
                foreach(PowerSupplyModel _ps in psModel)
                {//找供电臂内的线
                    foreach (TrackLine _tLine in _ps.containedTrackLine)
                    {//如果鼠标在该框内，则 mouseX > leftX && mouseX < rightX && mouseY > lowY && mouseY < highY
                        //如果…填写的时候左右是反的 就把它反过来
                        Point realLeftPoint = new Point();
                        Point realRightPoint = new Point();
                        if(_tLine.selfLeftPoint.X > _tLine.selfRightPoint.X)
                        {
                            realLeftPoint = _tLine.selfRightPoint;
                            realRightPoint = _tLine.selfLeftPoint;
                        }
                        else
                        {
                            realLeftPoint = _tLine.selfLeftPoint;
                            realRightPoint = _tLine.selfRightPoint;
                        }
                        string range = "";
                        if (realLeftPoint.Y < realRightPoint.Y)
                        {//左（数值）低右（数值）高
                            if(mouseX >= transformScreenOffsets(realLeftPoint.X ,true) && mouseX <= transformScreenOffsets(realRightPoint.X ,true) && mouseY > transformScreenOffsets(realLeftPoint.Y ,false) && mouseY < transformScreenOffsets(realRightPoint.Y,false))
                            {
                                if(_ps.powerSupplyRange.Length == 0)
                                {
                                    range = "未注明";
                                }
                                else
                                {
                                    range = _ps.powerSupplyRange;
                                }
                                mousePoint_lbl.Text = "当前供电臂：" + _ps.powerSupplyName + "；\n供电范围："+ range;
                            }
                        }
                        else if (realLeftPoint.Y > realRightPoint.Y)
                        {
                            if (_ps.powerSupplyRange.Length == 0)
                            {
                                range = "未注明";
                            }
                            else
                            {
                                range = _ps.powerSupplyRange;
                            }
                            if (mouseX >= transformScreenOffsets(realLeftPoint.X ,true) && mouseX <= transformScreenOffsets(realRightPoint.X,true) && mouseY < transformScreenOffsets(realLeftPoint.Y,false) && mouseY > transformScreenOffsets(realRightPoint.Y,false))
                            {
                                mousePoint_lbl.Text = "当前供电臂：" + _ps.powerSupplyName + "\n供电范围："+range;
                            }
                        }
                        else if(realLeftPoint.Y == realRightPoint.Y)
                        {//一样高的时候
                            if (_ps.powerSupplyRange.Length == 0)
                            {
                                range = "未注明";
                            }
                            else
                            {
                                range = _ps.powerSupplyRange;
                            }
                            if (mouseX >= transformScreenOffsets(realLeftPoint.X ,true) && mouseX <= transformScreenOffsets(realRightPoint.X ,true) && mouseY < transformScreenOffsets((realLeftPoint.Y + 15),false) &&mouseY > transformScreenOffsets((realLeftPoint.Y - 15) ,false))
                            {
                                mousePoint_lbl.Text = "当前供电臂：" + _ps.powerSupplyName + "\n供电范围："+range;
                            }
                        }
                    }
                }
            }

        }

        private void Main_Resize(object sender, EventArgs e)
        {
            RelocateButtons();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showSettings = !showSettings;
            checkSettings();
        }

        private void title_tb_TextChanged(object sender, EventArgs e)
        {
            title_lbl.Text = title_tb.Text;
            RelocateButtons();
        }

        private void Main_Scroll(object sender, ScrollEventArgs e)
        {
            RelocateButtons();
        }

        private void startTrackNum_tb_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if(int.TryParse(startTrackNum_tb.Text,out result))
            {
                startTrackNum = result;
            }
        }

        private void startTrackNum_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
        }

        private void stopTrackNum_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void stopTrackNum_tb_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (int.TryParse(stopTrackNum_tb.Text, out result))
            {
                stopTrackNum = result;
            }
        }

        private void zoomIn_btn_Click(object sender, EventArgs e)
        {
            zoomX = zoomX / 1.1f;
            zoomCount = zoomCount * 1.1f;
            label4.Text = "放大/缩小（当前："+zoomCount.ToString()+"倍）";
        }

        private void zoomOut_btn_Click(object sender, EventArgs e)
        {
            zoomX = zoomX * 1.1f;
            zoomCount = zoomCount / 1.1f;
            label4.Text = "放大/缩小（当前：" + zoomCount.ToString() + "倍）";
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y);
                leftFlag = true;
                timer1.Interval = 50;
            }
            else if(e.Button == MouseButtons.Right)
            {
                findMouseMovingSignalAndPS(e.X, e.Y);
            }
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;
            }
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                movingPixels.X = lastMovingPlace.X + mouseSet.X + mouseOff.X;
                movingPixels.Y = lastMovingPlace.Y + (mouseSet.Y-20) + mouseOff.Y;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);
                Location = mouseSet;
                checkSignalAndPS = false;
            }
            else
            {
                checkSignalAndPS = true;
                lastMovingPlace = movingPixels;
                timer1.Interval = 1000;
            }
            if (checkSignalAndPS == true)
            {
                findMouseMovingSignalAndPS(e.X,e.Y);
            }
        }

        private void resetPlace_btn_Click(object sender, EventArgs e)
        {
            lastMovingPlace.X = 0;
            lastMovingPlace.Y = 0;
            movingPixels.X = 0;
            movingPixels.Y = 0;
        }
    }
}
