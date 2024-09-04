using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.Commons
{
    public class Common
    {
        public static string FormatNumber(object value, int sosaudayphay = 2)
        {
            bool isNumber = IsNumeric(value);
            decimal GT = 0;
            if (isNumber)
            {
                GT = Convert.ToDecimal(value);
            }
            string str = "";
            string thapPhan = "";
            for (int i = 0; i < sosaudayphay; i++)
            {
                thapPhan += "#";
            }
            if (thapPhan.Length > 0) thapPhan = "." + thapPhan;
            string snumformat = string.Format("0:#,##0{0}", thapPhan);
            str = string.Format("{" + snumformat + "}", GT);
            return str;
        }
        public static bool IsNumeric(object value)
        {
            return value is sbyte ||
                 value is byte ||
                 value is short ||
                 value is ushort ||
                 value is int ||
                 value is uint ||
                 value is long ||
                 value is ulong ||
                 value is float ||
                 value is double ||
                 value is decimal;
        }
    }
}