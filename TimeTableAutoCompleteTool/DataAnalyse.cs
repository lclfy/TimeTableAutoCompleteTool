using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CCWin;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;

namespace TimeTableAutoCompleteTool
{
    public partial class DataAnalyse : Skin_Mac
    {
        public List<CommandModel> allCommandModels;
        public string operationString;
        public string continueTrainAnalyse;
        int timerCount = 0;
        //必须创建过doc才能手动打开
        bool couldCreateDoc = false;
        
        string wordFile = "";
        float dpiX, dpiY;
        //窗口置于最前
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow(); //获得本窗体的句柄
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体
                                                                   //定义变量,句柄类型
        public IntPtr Handle1;
        public DataAnalyse(List<CommandModel> _allCm, string _operationString,string _continueTrainAnalyse)
        {
            if (_allCm != null)
            {
                allCommandModels = _allCm;
            }
            if (_operationString != null)
            {
                operationString = _operationString;
            }
            if(_continueTrainAnalyse != null)
            {
                continueTrainAnalyse = _continueTrainAnalyse;
            }
            InitializeComponent();
        }

        private void DataAnalyse_Load(object sender, EventArgs e)
        {
            init();
            Handle1 = this.Handle;
            timer1.Enabled = true;
        }

        private void init()
        {
            skinButton2.Enabled = false;
            Graphics graphics = this.CreateGraphics();
            dpiX = graphics.DpiX;
            dpiY = graphics.DpiY;
            this.Size = new Size((int)(1200 * (dpiX / 96)), (int)(550 * (dpiY / 96)));
            timerCount = 0;
            start_cb.Checked = true;
            stop_cb.Checked = true;
            normal_cb.Checked = true;
            temp_cb.Checked = true;
            psngerTrain_cb.Checked = true;
            nonPsngerTrain_cb.Checked = true;
            start_cb.Checked = true;
            checked_cb.Checked = true;
            nonChecked_cb.Checked = true;

            getDataStatistics();
            int count = 0;
            foreach(CommandModel _cm in allCommandModels)
            {
                if(_cm.streamStatus != 4)
                {
                    count++;
                }
            }
            AllTrainsInCommand_lbl.Text = count.ToString();

            getSelectedTrains(false, start_cb.Checked, stop_cb.Checked, normal_cb.Checked, temp_cb.Checked, psngerTrain_cb.Checked, nonPsngerTrain_cb.Checked, checked_cb.Checked, nonChecked_cb.Checked);
        }

        private void RefreshList(List<CommandModel> _tempCM)
        {
            currentShow_lbl.Text = _tempCM.Count.ToString();
            data_lv.Items.Clear();
            data_lv.BeginUpdate();
            for (int j = 0; j < _tempCM.Count; j++)
            {
                ListViewItem _lvi;
                if (_tempCM[j].trainIndex != null && _tempCM[j].trainIndex.Length != 0)
                {
                    _lvi = new ListViewItem(_tempCM[j].trainIndex);
                }
                else
                {
                    _lvi = new ListViewItem("—");
                }
                if (_tempCM[j].MatchedWithTimeTable)
                {
                    _lvi.SubItems.Add("√是");
                }
                else
                {
                    _lvi.SubItems.Add("×否");
                }
                if (_tempCM[j].trainNumber != null && _tempCM[j].trainNumber.Length != 0)
                {
                    _lvi.SubItems.Add(_tempCM[j].trainNumber);
                }
                else
                {
                    _lvi.SubItems.Add("—");
                }

                if (_tempCM[j].secondTrainNumber != null && _tempCM[j].secondTrainNumber.Length != 0)
                {
                    if (_tempCM[j].secondTrainNumber.Trim().Contains("null"))
                    {
                        _lvi.SubItems.Add("无");
                    }
                    else
                    {
                        _lvi.SubItems.Add(_tempCM[j].secondTrainNumber);
                    }
                }
                else
                {
                    _lvi.SubItems.Add(" ");
                }
                switch (_tempCM[j].trainType)
                {
                    case 0:
                        _lvi.SubItems.Add("普通");
                        break;
                    case 1:
                        _lvi.SubItems.Add("高峰");
                        break;
                    case 2:
                        _lvi.SubItems.Add("临客");
                        break;
                    case 3:
                        _lvi.SubItems.Add("周末");
                        break;
                    case 4:
                        _lvi.SubItems.Add("加开");
                        break;
                }
                if (_tempCM[j].streamStatus != 0)
                    _lvi.SubItems.Add("开");
                else
                    _lvi.SubItems.Add("停");
                if (_tempCM[j].psngerTrain)
                {
                    _lvi.SubItems.Add("载客");
                }
                else
                {
                    _lvi.SubItems.Add("其他");
                }

                if (_tempCM[j].trainModel != null && _tempCM[j].trainModel.Length != 0)
                {
                    _lvi.SubItems.Add(_tempCM[j].trainModel);
                }
                else
                {
                    _lvi.SubItems.Add(" ");
                }

                if (_tempCM[j].trainId != null && _tempCM[j].trainId.Length != 0)
                {
                    _lvi.SubItems.Add(_tempCM[j].trainId);
                }
                else
                {
                    _lvi.SubItems.Add(" ");
                }

                data_lv.Items.Add(_lvi);

            }
            data_lv.EndUpdate();
        }

        //数据统计字符串
        private void getDataStatistics()
        {
            List<CommandModel> startPsngerTrains = getSelectedTrains(true, true, false, true, true, true, false, true, false);
            List<CommandModel> startOtherTrains = getSelectedTrains(true, true, false, true, true, false, true, true, false);
            List<CommandModel> stopPsngerTrains = getSelectedTrains(true, false, true, true, true, true, false, true, false);
            List<CommandModel> stopOtherTrains = getSelectedTrains(true, false, true, true, true, false, true, true, false);
            //还需要把时刻表中有的未匹配停运车找出来
            foreach (CommandModel _cm in allCommandModels)
            {
                if (_cm.streamStatus == 4)
                {
                    if (_cm.psngerTrain)
                    {
                        stopPsngerTrains.Add(_cm);
                    }
                    else
                    {
                        stopOtherTrains.Add(_cm);
                    }
                }
                else
                {
                    continue;
                }
            }
            string statisticsText = "";
            if (getSelectedTrains(true, true, false, false, true, true, false, true, false).Count != 0)
            {
                statisticsText = "郑州东（本站）本日实际开行旅客列车" + startPsngerTrains.Count +
                "列（其中临客" + getSelectedTrains(true, true, false, false, true, true, false, true, false).Count + "列），开行其他列车" + startOtherTrains.Count +
                "列（其中临客" + getSelectedTrains(true, true, false, false, true, false, true, true, false).Count + "列）;停运旅客列车" + stopPsngerTrains.Count +
                "列（其中临客" + getSelectedTrains(true, false, true, false, true, true, false, true, false).Count + "列）;停运其他列车" + stopOtherTrains.Count +
                "列（其中临客" + getSelectedTrains(true, false, true, false, true, false, true, true, false).Count + "列。\n" + operationString + "\n";

            }
            else
            {
                statisticsText = "郑州东（本站）本日实际开行旅客列车" + startPsngerTrains.Count +
                "列，开行其他列车" + startOtherTrains.Count +
                "列;停运旅客列车" + stopPsngerTrains.Count +
                "列;停运其他列车" + stopOtherTrains.Count +
                "列\n" + operationString + "\n";

            }

            string unrecognazedTrains = "";
            string notMatchedTrains = "";
            int notMatchedCount = 0;
            int count = 0;
            foreach (CommandModel _cm in getSelectedTrains(true, true, true, true, true, true, true, false, true))
            {
                count++;
                if (_cm.secondTrainNumber.Contains("null"))
                {
                    unrecognazedTrains = unrecognazedTrains + "第" + _cm.trainIndex + "条-" + _cm.trainNumber;
                }
                else
                {
                    unrecognazedTrains = unrecognazedTrains + "第" + _cm.trainIndex + "条-" + _cm.trainNumber + "-" + _cm.secondTrainNumber;
                }
                if(_cm.trainType == 4)
                {
                    unrecognazedTrains = unrecognazedTrains + "-标注加开、";
                }
                else
                {
                    unrecognazedTrains = unrecognazedTrains + "、";
                }

            }
            unrecognazedTrains = unrecognazedTrains + "\n共" + count + "列\n";
            operationChanged_rtb.Text = statisticsText;
            unrecognizedTrain_rtb.Text = unrecognazedTrains;
            continueTrainProblems_rtb.Text = continueTrainAnalyse;
            //找客调中不含的车
            for (int i = 0;i< allCommandModels.Count; i++)
            {
                if(allCommandModels[i].streamStatus == 4)
                {
                    notMatchedCount++;
                    notMatchedTrains = notMatchedTrains + notMatchedCount + "、" + allCommandModels[i].trainNumber +"-"+allCommandModels[i].notMatchedTabelName+"\n";
                }
            }
            notMatchedTrains = notMatchedTrains + "\n共" + notMatchedCount + "列";
            notMatchedTrains_rtb.Text = notMatchedTrains;

            //开行旅客列车不为空，创建统计word文档
            if(startPsngerTrains.Count != 0)
            {
                createStaticDoc(statisticsText,continueTrainAnalyse, unrecognazedTrains);
            }
        }

        private List<CommandModel> getSelectedTrains(bool init, bool trainOperationTrue, bool trainOperationFalse, bool normalTrain, bool tempTrain, bool psnger, bool nonPsnger, bool hasChecked, bool nonChecked)
        {
            List<CommandModel> _tempCM = new List<CommandModel>();
            //条件筛选
            for (int i = 0; i < allCommandModels.Count; i++)
            {
                //先确定最外层-筛选-未筛选
                if ((allCommandModels[i].MatchedWithTimeTable == true &&
                    hasChecked == true))
                {//再确定开/停
                    if ((allCommandModels[i].streamStatus != 0 && allCommandModels[i].streamStatus != 4 && trainOperationTrue == true) ||
                    (allCommandModels[i].streamStatus == 0 && trainOperationFalse == true))
                    //确定是否为普通列车
                    {
                        if ((allCommandModels[i].trainType == 0 && normalTrain == true) ||
                    (allCommandModels[i].trainType != 0 && tempTrain == true))
                        {//最后确定是否载客
                            if ((allCommandModels[i].psngerTrain == true && psnger == true) ||
                                (allCommandModels[i].psngerTrain == false && nonPsnger == true))
                            {
                                _tempCM.Add(allCommandModels[i]);
                            }
                        }
                    }
                }
                else if (allCommandModels[i].MatchedWithTimeTable == false &&
                    nonChecked == true)
                {//未筛选的可能有上下行未知的情况
                    if ((allCommandModels[i].streamStatus != 0 && allCommandModels[i].streamStatus != 4 && trainOperationTrue == true) ||
                    (allCommandModels[i].streamStatus == 0 && trainOperationFalse == true))
                    {
                        if ((allCommandModels[i].trainType == 0 && normalTrain == true) ||
                   (allCommandModels[i].trainType != 0&& tempTrain == true))
                        {//最后确定是否载客
                            if ((allCommandModels[i].psngerTrain == true && psnger == true) ||
                            (allCommandModels[i].psngerTrain == false && nonPsnger == true))
                            {
                                _tempCM.Add(allCommandModels[i]);
                            }
                        }
                    }
                }
            }
            if (!init)
            {
                RefreshList(_tempCM);
            }
            return _tempCM;
        }

        //创建统计word文档
        private void createStaticDoc(string staticText, string continueTrainText, string unrecognazedTrains)
        {
            
            if (staticText.Length == 0 && continueTrainText.Length == 0)
            {
                return;
            }
            Document doc = new Document();

            Section section = doc.AddSection();
            //Add Paragraph
            Paragraph Para1 = section.AddParagraph();
            //Append Text
            string title = "列车运行数据统计";
            int hour = -1;
            int.TryParse(DateTime.Now.ToString("HH"), out hour);
            if (hour >= 0 && hour <= 16)
            {
                title = DateTime.Now.ToString("yyyy年MM月dd日-") + title;
            }
            else
            {
                title = DateTime.Now.AddDays(1).ToString("yyyy年MM月dd日-") + title;
            }
            FileStream fs = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + title + ".doc");
            fs.Close();
            Para1.AppendText(title + "\n\n");

            Paragraph Para2 = section.AddParagraph();
            Para2.AppendText("一、列车开行情况：\n"+staticText + "\n\n");

            Paragraph Para3 = section.AddParagraph();
            Para3.AppendText("二、客调命令多出列车（包含识别错误车次，可能有加开车，请三场留存）：\n" + unrecognazedTrains);

            Paragraph Para4 = section.AddParagraph();
            Para4.AppendText("三、接续列车修改情况：（！出现错误时，请联系车间修改底图）\n"+continueTrainText);

            

            //写入数据并保存
            doc.SaveToFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + title + ".doc", FileFormat.Doc);
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            //info.WorkingDirectory = Application.StartupPath;
            info.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + title + ".doc";
            info.Arguments = "";
            try
            {
                System.Diagnostics.Process.Start(info);
            }
            catch (System.ComponentModel.Win32Exception we)
            {
                MessageBox.Show(this, we.Message);
                return;
            }


            skinButton2.Enabled = true;
        }
        private void searchList(string text)
        {
            List<CommandModel> _tempCM = new List<CommandModel>();
            for (int i = 0; i < allCommandModels.Count; i++)
            {
                if (allCommandModels[i].trainNumber.Contains(text) ||
                    allCommandModels[i].secondTrainNumber.Contains(text))
                {
                    _tempCM.Add(allCommandModels[i]);
                }
            }
            RefreshList(_tempCM);
        }

        //创建数据统计excel文件
        private void createStaticsFile()
        {
            try
            {
                FileStream fs = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "统计结果.xls");

                //创建工作薄
                IWorkbook workbook = new HSSFWorkbook();
                string fontSize = "14";

                //表格样式
                ICellStyle stoppedTrainStyle = workbook.CreateCellStyle();
                stoppedTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                stoppedTrainStyle.FillPattern = FillPattern.SolidForeground;
                stoppedTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                stoppedTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                stoppedTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                HSSFFont font = (HSSFFont)workbook.CreateFont();
                font.FontName = "宋体";//字体  
                font.FontHeightInPoints = short.Parse(fontSize.ToString());//字号  
                font.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
                stoppedTrainStyle.SetFont(font);

                ICellStyle nonMatchedTrainStype = workbook.CreateCellStyle();
                nonMatchedTrainStype.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;
                nonMatchedTrainStype.FillPattern = FillPattern.SolidForeground;
                nonMatchedTrainStype.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;
                nonMatchedTrainStype.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                nonMatchedTrainStype.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                nonMatchedTrainStype.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                nonMatchedTrainStype.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                HSSFFont nonMatchFont = (HSSFFont)workbook.CreateFont();
                nonMatchFont.FontName = "宋体";//字体  
                nonMatchFont.FontHeightInPoints = short.Parse(fontSize.ToString());//字号  
                nonMatchFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
                nonMatchedTrainStype.SetFont(nonMatchFont);

                ICellStyle normalTrainStyle = workbook.CreateCellStyle();
                normalTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                normalTrainStyle.FillPattern = FillPattern.SolidForeground;
                normalTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                normalTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                normalTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                normalTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                normalTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                HSSFFont normalFont = (HSSFFont)workbook.CreateFont();
                normalFont.FontName = "宋体";//字体  
                normalFont.FontHeightInPoints = short.Parse(fontSize.ToString());//字号  
                normalFont.IsBold = true;
                normalTrainStyle.SetFont(normalFont);

                ICellStyle tomorrowlTrainStyle = workbook.CreateCellStyle();
                tomorrowlTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                tomorrowlTrainStyle.FillPattern = FillPattern.SolidForeground;
                tomorrowlTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                tomorrowlTrainStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                tomorrowlTrainStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                tomorrowlTrainStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                tomorrowlTrainStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                tomorrowlTrainStyle.SetFont(normalFont);

                ICellStyle removeColors = workbook.CreateCellStyle();
                removeColors.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                removeColors.FillPattern = FillPattern.SolidForeground;
                removeColors.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                removeColors.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                removeColors.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                removeColors.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                removeColors.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

                ICellStyle addedTrainStyle = workbook.CreateCellStyle();
                addedTrainStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                addedTrainStyle.FillPattern = FillPattern.SolidForeground;
                addedTrainStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                addedTrainStyle.WrapText = true;
                addedTrainStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直

                HSSFFont addFont = (HSSFFont)workbook.CreateFont();
                addFont.FontName = "宋体";//字体  
                addFont.FontHeightInPoints = 12;//字号  
                addFont.IsBold = false;
                addedTrainStyle.SetFont(addFont);

                //创建sheet
                ISheet sheet = workbook.CreateSheet("数据统计");

                //开行旅客列车
                List<CommandModel> startPsngerTrains = getSelectedTrains(true, true, false, true, true, true, false, true, false);
                List<CommandModel> startOtherTrains = getSelectedTrains(true, true, false, true, true, false, true, true, false);
                List<CommandModel> stopPsngerTrains = getSelectedTrains(true, false, true, true, true, true, false, true, false);
                List<CommandModel> stopOtherTrains = getSelectedTrains(true, false, true, true, true, false, true, true, false);
                List<CommandModel> notMatchedTrains = getSelectedTrains(true, true, true, true, true, true, true, false, true);
                //还需要把时刻表中有的未匹配停运车找出来
                foreach (CommandModel _cm in allCommandModels)
                {
                    if(_cm.streamStatus == 4)
                    {
                        if (_cm.psngerTrain)
                        {
                            stopPsngerTrains.Add(_cm);
                        }
                        else
                        {
                            stopOtherTrains.Add(_cm);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                //第一列 开行旅客列车
                int columnCount =  0;
                for (int rowNum = 0;rowNum< startPsngerTrains.Count +1; rowNum++)
                {
                    IRow row;
                    if(sheet.GetRow(rowNum) == null)
                    {
                        row=sheet.CreateRow(rowNum);
                    }
                    else
                    {
                        row = sheet.GetRow(rowNum);
                    }
                    ICell cell;
                    if (row.GetCell(columnCount) == null)
                    {
                        cell = row.CreateCell(columnCount);
                    }
                    else
                    {
                        cell = row.GetCell(columnCount);
                    }
                    sheet.SetColumnWidth(columnCount, 25 * 256);
                    if (rowNum == 0)
                    {
                        cell.SetCellValue("开行旅客列车"+ startPsngerTrains.Count+"列");
                        cell.CellStyle = removeColors;
                    }
                    else
                    {
                        string trainDetails = startPsngerTrains[rowNum - 1].trainNumber;
                        if (!startPsngerTrains[rowNum -1].secondTrainNumber.Equals("null"))
                        {
                            trainDetails =  trainDetails + "/" + startPsngerTrains[rowNum - 1].secondTrainNumber;
                        }
                        if(startPsngerTrains[rowNum - 1].streamStatus == 2)
                        {
                            trainDetails = "(次日开)" + trainDetails;
                        }
                        switch (startPsngerTrains[rowNum - 1].trainType)
                        {
                            case 0:
                                trainDetails = "√" + trainDetails;
                                break;
                            case 1:
                                trainDetails = "√高峰" + trainDetails;
                                break;
                            case 2:
                                trainDetails = "√临客" + trainDetails;
                                break;
                            case 3:
                                trainDetails = "√周末" + trainDetails;
                                break;
                            case 4:
                                trainDetails = "√加开" + trainDetails;
                                break;
                        }
                        cell.SetCellValue(trainDetails);
                        cell.CellStyle = normalTrainStyle;

                    }

                }
                columnCount++;
                //第二列 开行其他列车
                for (int rowNum = 0; rowNum < startOtherTrains.Count + 1; rowNum++)
                {
                    IRow row;
                    if (sheet.GetRow(rowNum) == null)
                    {
                        row = sheet.CreateRow(rowNum);
                    }
                    else
                    {
                        row = sheet.GetRow(rowNum);
                    }
                    ICell cell;
                    if (row.GetCell(columnCount) == null)
                    {
                        cell = row.CreateCell(columnCount);
                    }
                    else
                    {
                        cell = row.GetCell(columnCount);
                    }
                    sheet.SetColumnWidth(columnCount, 25 * 256);
                    if (rowNum == 0)
                    {
                        cell.SetCellValue("开行其他列车" + startOtherTrains.Count + "列");
                        cell.CellStyle = removeColors;
                    }
                    else
                    {
                        string trainDetails = startOtherTrains[rowNum - 1].trainNumber;
                        if (!startOtherTrains[rowNum - 1].secondTrainNumber.Equals("null"))
                        {
                            trainDetails = trainDetails + "/" + startOtherTrains[rowNum - 1].secondTrainNumber;
                        }
                        if (startOtherTrains[rowNum - 1].streamStatus == 2)
                        {
                            trainDetails = "(次日开)" + trainDetails;
                        }
                        switch (startOtherTrains[rowNum - 1].trainType)
                        {
                            case 0:
                                trainDetails = "√" + trainDetails;
                                break;
                            case 1:
                                trainDetails = "√高峰" + trainDetails;
                                break;
                            case 2:
                                trainDetails = "√临客" + trainDetails;
                                break;
                            case 3:
                                trainDetails = "√周末" + trainDetails;
                                break;
                            case 4:
                                trainDetails = "√加开" + trainDetails;
                                break;
                        }
                        cell.SetCellValue(trainDetails);
                        cell.CellStyle = normalTrainStyle;

                    }

                }
                columnCount++;
                //第三列 停运旅客列车
                for (int rowNum = 0; rowNum < stopPsngerTrains.Count + 1; rowNum++)
                {
                    IRow row;
                    if (sheet.GetRow(rowNum) == null)
                    {
                        row = sheet.CreateRow(rowNum);
                    }
                    else
                    {
                        row = sheet.GetRow(rowNum);
                    }
                    ICell cell;
                    if (row.GetCell(columnCount) == null)
                    {
                        cell = row.CreateCell(columnCount);
                    }
                    else
                    {
                        cell = row.GetCell(columnCount);
                    }
                    sheet.SetColumnWidth(columnCount, 25 * 256);
                    if (rowNum == 0)
                    {
                        cell.SetCellValue("停运旅客列车"+ stopPsngerTrains.Count+"列");
                        cell.CellStyle = removeColors;
                    }
                    else
                    {
                        string trainDetails = stopPsngerTrains[rowNum - 1].trainNumber;
                        if (!stopPsngerTrains[rowNum - 1].secondTrainNumber.Equals("null"))
                        {
                            trainDetails = trainDetails + "/" + stopPsngerTrains[rowNum - 1].secondTrainNumber;
                        }
                        switch (stopPsngerTrains[rowNum - 1].trainType)
                        {
                            case 0:
                                trainDetails = "×" + trainDetails;
                                break;
                            case 1:
                                trainDetails = "×高峰" + trainDetails;
                                break;
                            case 2:
                                trainDetails = "×临客" + trainDetails;
                                break;
                            case 3:
                                trainDetails = "×周末" + trainDetails;
                                break;
                            case 4:
                                trainDetails = "×加开" + trainDetails;
                                break;
                        }
                        cell.SetCellValue(trainDetails);
                        if(stopPsngerTrains[rowNum - 1].streamStatus == 4)
                        {
                            cell.CellStyle = nonMatchedTrainStype;
                        }
                        else
                        {
                            cell.CellStyle = stoppedTrainStyle;
                        }

                    }

                }
                columnCount++;
                //第四列 停运其他列车
                for (int rowNum = 0; rowNum < stopOtherTrains.Count + 1; rowNum++)
                {
                    IRow row;
                    if (sheet.GetRow(rowNum) == null)
                    {
                        row = sheet.CreateRow(rowNum);
                    }
                    else
                    {
                        row = sheet.GetRow(rowNum);
                    }
                    ICell cell;
                    if (row.GetCell(columnCount) == null)
                    {
                        cell = row.CreateCell(columnCount);
                    }
                    else
                    {
                        cell = row.GetCell(columnCount);
                    }
                    sheet.SetColumnWidth(columnCount, 25 * 256);
                    if (rowNum == 0)
                    {
                        cell.SetCellValue("停运其他列车" + stopOtherTrains.Count + "列");
                        cell.CellStyle = removeColors;
                    }
                    else
                    {
                        string trainDetails = stopOtherTrains[rowNum - 1].trainNumber;
                        if (!stopOtherTrains[rowNum - 1].secondTrainNumber.Equals("null"))
                        {
                            trainDetails = trainDetails + "/" + stopOtherTrains[rowNum - 1].secondTrainNumber;
                        }
                        switch (stopOtherTrains[rowNum - 1].trainType)
                        {
                            case 0:
                                trainDetails = "×" + trainDetails;
                                break;
                            case 1:
                                trainDetails = "×高峰" + trainDetails;
                                break;
                            case 2:
                                trainDetails = "×临客" + trainDetails;
                                break;
                            case 3:
                                trainDetails = "×周末" + trainDetails;
                                break;
                            case 4:
                                trainDetails = "×加开" + trainDetails;
                                break;
                        }
                        cell.SetCellValue(trainDetails);
                        if (stopOtherTrains[rowNum - 1].streamStatus == 4)
                        {
                            cell.CellStyle = nonMatchedTrainStype;
                        }
                        else
                        {
                            cell.CellStyle = stoppedTrainStyle;
                        }

                    }

                }
                columnCount++;
                //第五列 未匹配列车
                for (int rowNum = 0; rowNum < notMatchedTrains.Count + 1; rowNum++)
                {
                    IRow row;
                    if (sheet.GetRow(rowNum) == null)
                    {
                        row = sheet.CreateRow(rowNum);
                    }
                    else
                    {
                        row = sheet.GetRow(rowNum);
                    }
                    ICell cell;
                    if (row.GetCell(columnCount) == null)
                    {
                        cell = row.CreateCell(columnCount);
                    }
                    else
                    {
                        cell = row.GetCell(columnCount);
                    }
                    sheet.SetColumnWidth(columnCount, 25 * 256);
                    if (rowNum == 0)
                    {
                        cell.SetCellValue("未匹配列车" + notMatchedTrains.Count + "列");
                        cell.CellStyle = removeColors;
                    }
                    else
                    {
                        string trainDetails = notMatchedTrains[rowNum - 1].trainNumber;
                        if (!notMatchedTrains[rowNum - 1].secondTrainNumber.Equals("null"))
                        {
                            trainDetails = trainDetails + "/" + notMatchedTrains[rowNum - 1].secondTrainNumber;
                        }
                        if(notMatchedTrains[rowNum - 1].streamStatus == 0)
                        {
                            switch (notMatchedTrains[rowNum - 1].trainType)
                            {
                                case 0:
                                    trainDetails = "(停)" + trainDetails;
                                    break;
                                case 1:
                                    trainDetails = "(停)高峰" + trainDetails;
                                    break;
                                case 2:
                                    trainDetails = "(停)临客" + trainDetails;
                                    break;
                                case 3:
                                    trainDetails = "(停)周末" + trainDetails;
                                    break;
                                case 4:
                                    trainDetails = "(停)加开" + trainDetails;
                                    break;
                            }
                        }
                        else
                        {
                            switch (notMatchedTrains[rowNum - 1].trainType)
                            {
                                case 0:
                                    trainDetails = "(开)" + trainDetails;
                                    break;
                                case 1:
                                    trainDetails = "(开)高峰" + trainDetails;
                                    break;
                                case 2:
                                    trainDetails = "(开)临客" + trainDetails;
                                    break;
                                case 3:
                                    trainDetails = "(开)周末" + trainDetails;
                                    break;
                                case 4:
                                    trainDetails = "(开)加开" + trainDetails;
                                    break;
                            }
                        }
                       
                        cell.SetCellValue(trainDetails);
                        cell.CellStyle = tomorrowlTrainStyle;

                    }

                }

                //向excel文件中写入数据并保保存
                workbook.Write(fs);
                fs.Close();
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
                //info.WorkingDirectory = Application.StartupPath;
                info.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+  "\\" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "统计结果.xls";
                info.Arguments = "";
                try
                {
                    System.Diagnostics.Process.Start(info);
                }
                catch (System.ComponentModel.Win32Exception we)
                {
                    MessageBox.Show(this, we.Message);
                    return;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("出现错误请重试~" + e1.ToString().Split('。')[0] + "。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void checkedChanged()
        {

            getSelectedTrains(false, start_cb.Checked, stop_cb.Checked, normal_cb.Checked, temp_cb.Checked, psngerTrain_cb.Checked, nonPsngerTrain_cb.Checked, checked_cb.Checked, nonChecked_cb.Checked);
        }

        private void up_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void down_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void psngerTrain_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void nonPsngerTrain_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void checked_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void nonChecked_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void search_tb_TextChanged(object sender, EventArgs e)
        {
            if (search_tb.Text.ToString().Length != 0)
            {
                searchList(search_tb.Text.ToString());
            }
            else
            {
                checkedChanged();
            }
        }

        private void stop_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void temp_cb_CheckedChanged(object sender, EventArgs e)
        {
            checkedChanged();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                粘贴ToolStripMenuItem.Enabled = true;
            }
            else
                粘贴ToolStripMenuItem.Enabled = false;

            ((RichTextBox)contextMenuStrip1.SourceControl).Paste();
            //command_rTb.Paste(); //粘贴
        }



        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)contextMenuStrip1.SourceControl).Clear();
            //command_rTb.Clear(); //清空
        }

        private void 复制toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string selectText = ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerCount++;
            if (Handle1 != GetForegroundWindow()) //持续使该窗体置为最前,屏蔽该行则单次置顶
            {
                SetForegroundWindow(Handle1);
                if(timerCount > 20)
                {
                    timer1.Stop();//此处可以关掉定时器，则实现单次置顶
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void notMatchedTrains_rtb_TextChanged(object sender, EventArgs e)
        {

        }

        private void unrecognizedTrain_rtb_TextChanged(object sender, EventArgs e)
        {

        }

        //word文档
        private void skinButton2_Click(object sender, EventArgs e)
        {

        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            createStaticsFile();
        }
    }
}
