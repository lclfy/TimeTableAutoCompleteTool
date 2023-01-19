using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TimeTableAutoCompleteTool
{
    class EMUCheckModel : IComparable<EMUCheckModel>
    {//动检车模型
        public int id { get; set; }
        public string extra { get; set; }
        public string startStation { get; set; }
        public string trainNumber { get; set; }
        public string stopTime { get; set; }
        public string trackNum { get; set; }
        public string startTime { get; set; }
        public string destination { get; set; }
        public int streamStatus { get; set; }

        /*
        public int CompareTo(EMUCheckModel other)
        {
            if (other == null || id == other.id)
            {
                return 0;
            }
            if(id > other.id)
            {
                return 1;
            }
            else
            {
                return -1;
            }
            throw new NotImplementedException();
        }
        */
        //重写的CompareTo方法，根据Id排序
        public int CompareTo(EMUCheckModel otherTrain)
        {
            {
                /*
                if (null == otherTrain)
                {
                    return 1;//空值比较大，返回1
                }
                //return this.Id.CompareTo(other.Id);//升序
                return this.startTime.CompareTo(otherTrain.startTime);//降序
                */
                //判断一下发车时间有没有汉字，有汉字说明是接续，此时使用终到时间进行排序。
                string thisStartedTime = "";
                string otherStartedTime = "";

                Regex reg = new Regex(@"[\u4e00-\u9fa5]");

                if (reg.IsMatch(this.startTime) || startTime.Contains("改"))
                {//有中文，则有接续
                    thisStartedTime = stopTime.Replace(":", "").Trim();
                }
                else
                {
                    thisStartedTime = startTime.Replace(":", "").Trim();
                }
                if (reg.IsMatch(otherTrain.startTime) || otherTrain.startTime.Contains("--"))
                {
                    otherStartedTime = otherTrain.stopTime.Replace(":", "").Trim();
                }
                else
                {
                    otherStartedTime = otherTrain.startTime.Replace(":", "").Trim();
                }

                if (this == null || otherTrain == null)
                    throw new ArgumentException("Parameters can't be null");
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
                //            return string.Compare( fileA, fileB );
                //            return( (new CaseInsensitiveComparer()).Compare( y, x ) );
            }
            return 0;
        }
        
    }
    

}
