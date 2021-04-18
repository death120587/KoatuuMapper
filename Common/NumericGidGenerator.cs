using System;

namespace Common
{
    public class NumericGidGenerator
    {
        public static long GetIdentifier()
        {
            return DateTime.Now.Ticks;
        }
    }
}
