using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class UnifiedSocialTax : IType
    {
        public string IDName { get; set; }
        public int Month { get; set; }
        public decimal IncomeDeclared { get; set; }
        public decimal IncomeAmount { get; set; }
        public List<UnifiedSocialTax> unifiedSocialTaxes { get; set; }

        public void CallMethod(string methodName, List<string> attributes)
        {
            return;
        }

        public void SetValue(string prop, string value = null, IType instance = null)
        {
            switch (prop)
            {
                case DataTypes.MONTH_DT:
                    Month = int.Parse(value);
                    break;
                case DataTypes.INCOME_DECLARED_DT:
                    IncomeDeclared = decimal.Parse(value);
                    break;
                case DataTypes.INCOME_AMOUNT_DT:
                    IncomeAmount = decimal.Parse(value);
                    break;
            }
        }
    }
}
