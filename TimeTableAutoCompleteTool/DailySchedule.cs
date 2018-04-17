using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TimeTableAutoCompleteTool
{
    class DailySchedule : IComparable<DailySchedule>
    {
        public int id { get; set; }
        public string trainNumber { get; set; }
        public int streamStatus { get; set; }
        public string startStation { get; set; }
        public string stopStation { get; set; }
        public string stopTime { get; set; }
        public string startTime { get; set; }
        //0为普通-1为高峰-2为临客-3为周末-有的再加
        public int trainType { get; set; }
        //1始发 2终到
        public int stopStartStatus { get; set; }
        public string stopToStartTime { get; set; }
        public string trainBelongsTo { get; set; }
        public string trackNum { get; set; }
        //编组
        public string trainConnectType { get; set; }
        //定员
        public string ratedSeats { get; set; }
        public string trainModel { get; set; }
        //备注
        public string extraText { get; set; }
        //新旧交替
        public string tipsText { get; set; }
        public bool hasDifferentPart = false;
        //列车预售时间
        public int presaleTime { get; set; }
        //南1-北0
        public int upOrDown { get; set; }


        public int CompareTo(DailySchedule other)
        {
            if (other == null)
            {
                return 0;
            }
            string thisStartedTime = "";
            string otherStartedTime = "";
            if(startTime == null)
            {
                if (stopTime != null)
                    thisStartedTime = stopTime.Replace(":", "");
            }
            else
            {
                thisStartedTime = startTime.Replace(":", "");
            }

            if (other.startTime == null)
            {
                if(other.stopTime != null)
                otherStartedTime = other.stopTime.Replace(":", "");
            }
            else
            {
                otherStartedTime = other.startTime.Replace(":", "");
            }
            int StartedTimeInt = 0;
            int.TryParse(thisStartedTime, out StartedTimeInt);
            if(StartedTimeInt != 0 &&
                StartedTimeInt < 500)
            {
                StartedTimeInt = StartedTimeInt + 2400;
                thisStartedTime = StartedTimeInt.ToString();
            }
            int.TryParse(otherStartedTime, out StartedTimeInt);
            if (StartedTimeInt != 0 &&
                StartedTimeInt < 500)
            {
                StartedTimeInt = StartedTimeInt + 2400;
                otherStartedTime = StartedTimeInt.ToString();
            }
            if(streamStatus == 0)
            {
                thisStartedTime = "0";
            }
            if(other.streamStatus == 0)
            {
                otherStartedTime = "0";
            }
            char[] arr1 = thisStartedTime.ToCharArray();
            char[] arr2 = otherStartedTime.ToCharArray();
            int i = 0, j = 0;
            while (i < arr1.Length && j < arr2.Length)
            {
                if (char.IsDigit(arr1[i]) && char.IsDigit(arr2[j]))
                {
                    string s1 = "", s2 = "";
                    while (i < arr1.Length && char.IsDigit(arr1[i]))
                    {
                        s1 += arr1[i];
                        i++;
                    }
                    while (j < arr2.Length && char.IsDigit(arr2[j]))
                    {
                        s2 += arr2[j];
                        j++;
                    }
                    if (int.Parse(s1) > int.Parse(s2))
                    {
                        return 1;
                    }
                    if (int.Parse(s1) < int.Parse(s2))
                    {
                        return -1;
                    }
                }
                else
                {
                    if (arr1[i] > arr2[j])
                    {
                        return 1;
                    }
                    if (arr1[i] < arr2[j])
                    {
                        return -1;
                    }
                    i++;
                    j++;
                }
            }
            if (arr1.Length == arr2.Length)
            {
                return 0;
            }
            else
            {
                return arr1.Length > arr2.Length ? 1 : -1;
            }
            throw new NotImplementedException();
        }
    }
}
