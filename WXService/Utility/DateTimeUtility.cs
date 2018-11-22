using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WXService.Utility
{
    /// <summary>
    /// 日期工具类。
    /// </summary>
    public static class DateTimeUtility
    {

        public static DateTime Parse(long createTime)
        {

            long time_JAVA_Long = createTime * 1000L;//java长整型日期，毫秒为单位                

            DateTime dt_1970 = new DateTime(1970, 1, 1, 0, 0, 0);

            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度                         

            long time_tricks = tricks_1970 + time_JAVA_Long * 10000;//日志日期刻度                         

            DateTime dt = new DateTime(time_tricks).AddHours(8);//转化为DateTime

            return dt;

        }  
    }
}
