using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class Form1DF1Section1 : IType
    {
        public string IDName { get; set; }
        public List<Form1DFSection1Unit> Units { get; set; }

        public void AddNewRecord()
        {

        }

        public void DeleteRecord()
        {

        }

        public void ClarifyAddNewRecord()
        {

        }

        public void ClarifyDeleteRecord()
        {

        }

        public void CallMethod(string methodName, List<string> attributes)
        {
            switch (methodName)
            {
                case DataTypes.ADD_NEW_RECORD_METHOD:
                    AddNewRecord();
                    break;
                case DataTypes.DELETE_RECORD_METHOD:
                    DeleteRecord();
                    break;
                case DataTypes.CLARIFY_ADD_NEW_RECORD_METHOD:
                    ClarifyAddNewRecord();
                    break;
                case DataTypes.CLARIFY_DELETE_RECORD_METHOD:
                    ClarifyDeleteRecord();
                    break;
            }
        }

        public void SetValue(string prop, string value = null, IType instance = null)
        {
            throw new NotImplementedException();
        }
    }
}
