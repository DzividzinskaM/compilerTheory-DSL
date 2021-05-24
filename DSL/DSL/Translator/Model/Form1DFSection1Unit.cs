using System;
using System.Collections.Generic;
using System.Text;

namespace DSL 
{ 
    public class Form1DFSection1Unit : IType
    {
        public int num { get; set; }
        public string IDName { get; set; }
        public string EmpItn { get; set; }
        public decimal IncomeAccured { get; set; }
        public decimal IncomePaid { get; set; }
        public decimal IncomeSign { get; set; }
        public DateTime DateAdopt { get; set; }
        public DateTime DateDismiss { get; set; }
        public int Sign { get; set; }


        public void CallMethod(string methodName, List<string> attributes)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string prop, string value = null, IType instance = null)
        {
            switch (prop)
            {
                case DataTypes.EMPLOYEE_ITN_DT:
                    EmpItn = value;
                    break;
                case DataTypes.INCOME_ACCURED_DT:
                    IncomeAccured = decimal.Parse(value);
                    break;
                case DataTypes.INCOME_PAID_DT:
                    IncomePaid = decimal.Parse(value);
                    break;
                case DataTypes.INCOME_SIGN_DT:
                    IncomeSign = decimal.Parse(value);
                    break;
                case DataTypes.DATE_ADOPT_DT:
                    DateAdopt = SpecialParsing.parserDate(value);
                    break;
                case DataTypes.DATE_DISMISS_DT:
                    DateDismiss = SpecialParsing.parserDate(value);
                    break;
                case DataTypes.SIGN_DT:
                    Sign = int.Parse(value);
                    break;
            }
        }
    }
}
