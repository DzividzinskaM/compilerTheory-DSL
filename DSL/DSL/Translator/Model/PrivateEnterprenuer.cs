using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class PrivateEnterprenuer : IType
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string RNTRC { get; set; }
        public int Group { get; set; }
        public string TaxSystem { get; set; }
        public List<string> NaceCodes { get; set; }
        public string MainNaceCode { get; private set; }
        public string IDName { get; set; }

        public void AddNewNaceCode(string naceCode)
        {
            if (NaceCodes == null)
                NaceCodes = new List<string>();

            NaceCodes.Add(naceCode);
        }

        public void SetMainNaceCode(string NaceCode)
        {
            MainNaceCode = NaceCode;
        }

        public void SetValue(string prop, string value = null, IType instance = null)
        {
            switch (prop)
            {
                case DataTypes.FULL_NAME_DT:
                    FullName = value;
                    break;
                case DataTypes.ADDRESS_DT:
                    Address = value;
                    break;
                case DataTypes.PHONE_DT:
                    Phone = value;
                    break;
                case DataTypes.RNTRC_DT:
                    RNTRC = value;
                    break;
                case DataTypes.GROUP_DT:
                    Group = Int32.Parse(value);
                    break;
                case DataTypes.TAX_SYSTEM_DT:
                    TaxSystem = value;
                    break;
                case DataTypes.NACE_CODE_DT:
                    if (NaceCodes == null)
                        NaceCodes = new List<string>();
                    NaceCodes.Add(value);
                    if (MainNaceCode == null) MainNaceCode = value;
                    break;
                default: break;
            }
        }

        public void CallMethod(string methodName, List<string> attributes)
        {
            switch(methodName){
                case DataTypes.ADD_NEW_NACE_CODE_METHOD:
                    AddNewNaceCode(attributes[0]);
                    break;
                case DataTypes.SET_MAIN_NACE_CODE_METHOD:
                    SetMainNaceCode(attributes[0]);
                    break;
            }
        }
    }
}
