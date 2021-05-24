using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class SpecialParsing
    {
        public static DateTime parserDate(string value)
        {
            return DateTime.Parse(value.Substring(1, value.Length - 2));
        }
    }
}
