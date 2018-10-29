using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myixy.App.Utilities
{
    public class Common
    {
        public static DateTime GetChinaStandardTimeNow()
        {
            //TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            //return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cstZone);
            return DateTime.Now;
        }
    }
}
