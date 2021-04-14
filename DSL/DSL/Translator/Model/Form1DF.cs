using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class Form1DF : IType
    {
        public string IDName { get; set; }
        public PrivateEnterprenuer PrivateEnterprenuer { get; set; }
        public string Type { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public int EmployeeByEmplContract { get; set; }
        public int EmployeeByCivivlContract { get; set; }
        public Form1DF1Section1 Section1 { get; set; }

        public void Show()
        {

        }

        public void Save()
        {

        }


        public void CallMethod(string methodName, List<string> attributes)
        {
            switch (methodName)
            {
                case DataTypes.SHOW_METHOD:
                    Show();
                    break;
                case DataTypes.SAVE_METHOD:
                    Save();
                    break;
            }
        }

        public void SetValue(string prop, string value = null, IType instance = null)
        {
            switch (prop)
            {
                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                    PrivateEnterprenuer = (PrivateEnterprenuer) instance;
                    break;
                case DataTypes.TYPE_1_DF_DT:
                    Type = value;
                    break;
                case DataTypes.QUARTER_DT:
                    Quarter = int.Parse(value);
                    break;
                case DataTypes.YEAR_DT:
                    Year = int.Parse(value);
                    break;
                case DataTypes.EMPLOYEE_BY_EMPLOYMENT_CONRACT_DT:
                    EmployeeByEmplContract = int.Parse(value);
                    break;
                case DataTypes.EMPLOYEE_BY_CIVIL_CONRACT_DT:
                    EmployeeByCivivlContract = int.Parse(value);
                    break;
                case DataTypes.SECTION_1_DF_1_DT:
                    Section1 = (Form1DF1Section1)instance;
                    break;

            }
        }
    }
}
