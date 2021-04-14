using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSL
{
    public class ProgramData
    {
        public List<PrivateEnterprenuer> PrivateEnreprenuers;
        public List<OneTaxPayer> OneTaxPayerReports;
        public List<Form1DF> form1DFs;
        public List<Form1DF1Section1> form1DF1Sections;
        public List<Form1DFSection1Unit> form1DFSection1Units;
        public List<UnifiedSocialTax> unifiedSocialTaxes;
        public List<UnifiedSocialTaxReport> unifiedSocialTaxReports;
        public List<string> Identifiers;

        public ProgramData()
        {
            Identifiers = new List<string>();
            PrivateEnreprenuers = new List<PrivateEnterprenuer>();
            OneTaxPayerReports = new List<OneTaxPayer>();
        }

        public bool IsAlreadyExist(string id)
        {
            return Identifiers.Any(item => item == id);
        }


        public string GetIdType(string identifier)
        {
            if (PrivateEnreprenuers.Any(item => item.IDName == identifier))
                return DataTypes.PRIVATE_ENTERPRENUER_DT;
            if (OneTaxPayerReports.Any(item => item.IDName == identifier))
                return DataTypes.ONE_TAX_PAYER_REPORT_DT;
        /*    if (form1DFs.Any(item => item.IDName == identifier))
                return DataTypes.DF_1_REPORT_DT;
            if (form1DF1Sections.Any(item => item.IDName == identifier))
                return DataTypes.SECTION_1_DF_1_DT;
            if (unifiedSocialTaxReports.Any(item => item.IDName == identifier))
                return DataTypes.UNIFIED_SOCIAL_TAX_REPORT_DT;*/

            return null;
        }

        internal void SetValueByPropId(string id, string prop, string value)
        {
            string idDt = GetIdType(id);
            string valueType = GetIdType(value);
/*            if (idType != null)
            {
                switch (idType)
                {
                    case DataTypes.PRIVATE_ENTERPRENUER_DT:
                       PrivateEnterprenuer el =  getPrEntItemByID(value);
                       break;
                }
            }*/

            switch (idDt)
            {
                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                    {
                        //find id                        
                        getPrEntItemByID(id).SetValue(prop, value);
                        break;
                    }
                case DataTypes.ONE_TAX_PAYER_REPORT_DT:
                    {
                        if(valueType != null)
                        {
                            switch (valueType)
                            {
                                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                                    getOneTaxPayerReportItemByID(id).SetValue(prop,
                                        instance: getPrEntItemByID(value));
                                    break;

                            }
                        }
                        getOneTaxPayerReportItemByID(id).SetValue(prop, value);
                        break;
                    }

            }
        }

        private PrivateEnterprenuer getPrEntItemByID( string id)
        {
            return PrivateEnreprenuers.Where(item => item.IDName == id).First();
        }
        private OneTaxPayer getOneTaxPayerReportItemByID(string id)
        {
            return OneTaxPayerReports.Where(item => item.IDName == id).First();
        }

        internal void CallMethod(string identifier, string methodName, List<string> attributes)
        {
            switch (GetIdType(identifier)) {
                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                    getPrEntItemByID(identifier).CallMethod(methodName, attributes);
                    break;
                case DataTypes.ONE_TAX_PAYER_REPORT_DT:
                    getOneTaxPayerReportItemByID(identifier).CallMethod(methodName, attributes);
                    break;
                default: break;

            }
        }

        
    }
}
