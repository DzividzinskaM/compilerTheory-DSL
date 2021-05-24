using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class Form1DF1Section1 : IType
    {
        public string IDName { get; set; }
        public List<Form1DFSection1Unit> Units { get; set; }

        public void AddNewRecord(string empItn, decimal incomeAccured, decimal incomePaid, decimal incomeSign, DateTime dateAdopt,
            DateTime dateDismiss)
        {
            Units.Add(new Form1DFSection1Unit
            {
                num = Units.Count + 1,
                EmpItn = empItn,
                IncomeAccured = incomeAccured,
                IncomePaid = incomePaid,
                IncomeSign = incomeSign,
                DateAdopt = dateAdopt,
                DateDismiss = dateDismiss,
                Sign = 0
            });
        }

        public void DeleteRecord(int num)
        {
            var unit = Units.Find((unit) => unit.num == num);
            if (unit.Sign == 1)
                throw new Exception($"Record number {num} is already deleted");
            unit.Sign =1;

        }

      
        public void CallMethod(string methodName, List<string> attributes)
        {
            switch (methodName)
            {
                case DataTypes.ADD_NEW_RECORD_METHOD:
                    AddNewRecord(attributes[0], decimal.Parse(attributes[1]), decimal.Parse(attributes[2]), decimal.Parse(attributes[3]),
                        DateTime.Parse(attributes[4]), DateTime.Parse(attributes[5]));
                    break;
                case DataTypes.DELETE_RECORD_METHOD:
                    DeleteRecord(Int32.Parse(attributes[0]));
                    break;
            }
        }

        public void SetValue(string prop, string value = null, IType instance = null)
        {
            throw new NotImplementedException();
        }

        public Form1DF1Section1 Clone()
        {
            return new Form1DF1Section1 { Units = this.Units };
        }
    }
}
