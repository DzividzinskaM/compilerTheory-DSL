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
        public List<Form1DF> formDF1s;
        public List<Form1DF1Section1> form1DF1Sections;
        public List<Form1DFSection1Unit> form1DFSection1Units;
        public List<UnifiedSocialTax> unifiedSocialTaxes;
        public List<UnifiedSocialTaxReport> unifiedSocialTaxReports;
        public List<string> Identifiers;
        public List<IType> Instances;
        public List<PrimitiveType> Primitives;

        public ProgramData()
        {
            Identifiers = new List<string>();
            PrivateEnreprenuers = new List<PrivateEnterprenuer>();
            OneTaxPayerReports = new List<OneTaxPayer>();
            formDF1s = new List<Form1DF>();
            form1DF1Sections = new List<Form1DF1Section1>();
            form1DFSection1Units = new List<Form1DFSection1Unit>();
            unifiedSocialTaxes = new List<UnifiedSocialTax>();
            unifiedSocialTaxReports = new List<UnifiedSocialTaxReport>();
            Instances = new List<IType>();
            Primitives = new List<PrimitiveType>();
        }

        public bool IsAlreadyExist(string id)
        {
            return Identifiers.Any(item => item == id);
        }


        public string GetIdType(string identifier)
        {
            if (PrivateEnreprenuers.Count != 0 && PrivateEnreprenuers.Any(item => item.IDName == identifier))
                return DataTypes.PRIVATE_ENTERPRENUER_DT;
            if (OneTaxPayerReports.Count != 0 && OneTaxPayerReports.Any(item => item.IDName == identifier))
                return DataTypes.ONE_TAX_PAYER_REPORT_DT;
            if (formDF1s.Count != 0 &&  formDF1s.Any(item => item.IDName == identifier))
                return DataTypes.DF_1_REPORT_DT;
            if (form1DF1Sections.Count != 0 && form1DF1Sections.Any(item => item.IDName == identifier))
                return DataTypes.SECTION_1_DF_1_DT;
            if (unifiedSocialTaxReports.Count != 0 && unifiedSocialTaxReports.Any(item => item.IDName == identifier))
                return DataTypes.UNIFIED_SOCIAL_TAX_REPORT_DT;
            if (unifiedSocialTaxes.Count != 0 && unifiedSocialTaxes.Any(item => item.IDName == identifier))
                return DataTypes.TABLE_1_UNIFIED_SOCIL_TAX_DT;

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

        internal void SetStrById(string identifier, string str)
        {
            var instance = GetPrimitiveInstance(identifier);
            instance.StrValue = str;
            changePrimitives(identifier, instance);
        }

        private void changePrimitives(string identifier, PrimitiveType instance)
        {
            Instances.Remove(GetPrimitiveInstance(identifier));
            Instances.Add(instance);
        }

        private PrimitiveType GetPrimitiveInstance(string id)
        {
            return Primitives.Where(item => item.IDName == id).FirstOrDefault();
        }

        internal void SetStrPropValueByID(string id, string propName, string value)
        {
            string idType = GetIdType(id);
            switch (idType)
            {
                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                    {
                        var instance = getPrEntItemByID(id);
                        instance.SetValue(propName, value);
                        changeForNewInstancePrEnt(id, instance);
                        break;
                    }
                case DataTypes.ONE_TAX_PAYER_REPORT_DT:
                    {
                        var instance = getOneTaxPayerReportItemByID(id);
                        instance.SetValue(propName, value);
                        changeForNewInstanceOneTaxPayerReport(id, instance);
                        break;
                    }
                    
                case DataTypes.DF_1_REPORT_DT:
                    {
                        var instance = getForm1DFReportItemByID(id);
                        instance.SetValue(propName, value);
                        changeForNewInstanceDF1(id, instance);
                        break;
                    }
                case DataTypes.SECTION_1_DF_1_DT:
                    {
                        var instance = getForm1DFSection1ItemByID(id);
                        instance.SetValue(propName, value);
                        changeForNewInstanceDF1Section1(id, instance);
                        break;
                    }
                case DataTypes.UNIFIED_SOCIAL_TAX_REPORT_DT:
                    {
                        var instance = getUnifiedSocilTaxReportItemByID(id);
                        instance.SetValue(propName, value);
                        changeForNewInstanceUnifiedSocialTax(id, instance);
                        break;
                    }
                case DataTypes.TABLE_1_UNIFIED_SOCIL_TAX_DT:
                    {
                        var instance = getUnifiedSocilTaxTable1ItemByID(id);
                        instance.SetValue(propName, value);
                        changeForNewInstanceUnifiedSocialTaxTable1(id, instance);
                        break;
                    }

            }
        }

        private void changeForNewInstanceUnifiedSocialTaxTable1(string id, UnifiedSocialTax instance)
        {
            unifiedSocialTaxes.Remove(getUnifiedSocilTaxTable1ItemByID(id));
        }

        private void changeForNewInstanceUnifiedSocialTax(string id, UnifiedSocialTaxReport instance)
        {
            unifiedSocialTaxReports.Remove(getUnifiedSocilTaxReportItemByID(id));
            unifiedSocialTaxReports.Add(instance);
        }

        private void changeForNewInstanceDF1Section1(string id, Form1DF1Section1 instance)
        {
            form1DF1Sections.Remove(getForm1DFSection1ItemByID(id));
            form1DF1Sections.Add(instance);
        }

        private void changeForNewInstanceDF1(string id, Form1DF instance)
        {
            formDF1s.Remove(getForm1DFReportItemByID(id));
            formDF1s.Add(instance);
        }

        private void changeForNewInstanceOneTaxPayerReport(string id, OneTaxPayer instance)
        {
            OneTaxPayerReports.Remove(getOneTaxPayerReportItemByID(id));
            OneTaxPayerReports.Add(instance);
        }

        private void changeForNewInstancePrEnt(string id, PrivateEnterprenuer newInstance)
        {
            PrivateEnreprenuers.Remove(getPrEntItemByID(id));
            PrivateEnreprenuers.Add(newInstance);
        }

        internal void SetIdById(string id, string valueId)
        {
/*            var instance = GetInstance(id);*/
            var instanceValue = GetInstance(valueId);
            /*         Instances.Remove(instance);
                     instance = instanceValue;
                     Instances.Add(instance);*/

            bool isPrimitive = true;

            string idType = GetIdType(id);
            switch (idType)
            {
                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                    {
                        PrivateEnterprenuer instance = getPrEntItemByID(id);
                        var name = instance.IDName;
                        if (instanceValue is PrivateEnterprenuer)
                        {
                            PrivateEnterprenuer value = (PrivateEnterprenuer)instanceValue;
                            instance = value.Clone();
                            instance.IDName = name;
                        }
                        else
                            showExIdTypesAreDifferent(DataTypes.PRIVATE_ENTERPRENUER_DT, instanceValue);
                        changeForNewInstancePrEnt(id, instance);
                        isPrimitive = false;
                        break;
                    }
                case DataTypes.ONE_TAX_PAYER_REPORT_DT:
                    {
                        var instance = getOneTaxPayerReportItemByID(id);
                        var name = instance.IDName;
                        if (instanceValue is OneTaxPayer)
                        {
                            OneTaxPayer value = (OneTaxPayer)instanceValue;
                            instance = value.Clone();
                            instance.IDName = name;
                        }
                        else
                            showExIdTypesAreDifferent(DataTypes.ONE_TAX_PAYER_REPORT_DT, instanceValue);
                        changeForNewInstanceOneTaxPayerReport(id, instance);
                        isPrimitive = false;

                        break;
                    }

                case DataTypes.DF_1_REPORT_DT:
                    {
                        var instance = getForm1DFReportItemByID(id);
                        var name = instance.IDName;
                        if (instanceValue is Form1DF)
                        {
                            Form1DF value = (Form1DF)instanceValue;
                            instance = value.Clone();
                            instance.IDName = name;
                        }
                        else
                            showExIdTypesAreDifferent(DataTypes.DF_1_REPORT_DT, instanceValue);
                        changeForNewInstanceDF1(id, instance);
                        isPrimitive = false;

                        break;
                    }
                case DataTypes.SECTION_1_DF_1_DT:
                    {
                        var instance = getForm1DFSection1ItemByID(id);
                        var name = instance.IDName;
                        if (instanceValue is Form1DF1Section1)
                        {
                            Form1DF1Section1 value = (Form1DF1Section1)instanceValue;
                            instance = value.Clone();
                            instance.IDName = name;
                        }
                        else
                            showExIdTypesAreDifferent(DataTypes.SECTION_1_DF_1_DT, instanceValue);
                        changeForNewInstanceDF1Section1(id, instance);
                        isPrimitive = false;

                        break;
                    }
                case DataTypes.UNIFIED_SOCIAL_TAX_REPORT_DT:
                    {
                        var instance = getUnifiedSocilTaxReportItemByID(id);
                        var name = instance.IDName;
                        if (instanceValue is UnifiedSocialTaxReport)
                        {
                            var value = (UnifiedSocialTaxReport)instanceValue;
                            instance = value.Clone();
                            instance.IDName = name;
                        }
                        else
                            showExIdTypesAreDifferent(DataTypes.UNIFIED_SOCIAL_TAX_REPORT_DT, instanceValue);
                        changeForNewInstanceUnifiedSocialTax(id, instance);
                        isPrimitive = false;

                        break;
                    }
                case DataTypes.TABLE_1_UNIFIED_SOCIL_TAX_DT:
                    {
                        var instance = getUnifiedSocilTaxTable1ItemByID(id);
                        var name = instance.IDName;
                        if (instanceValue is UnifiedSocialTax)
                        {
                            var value = (UnifiedSocialTax)instanceValue;
                            instance = value.Clone();
                            instance.IDName = name;
                        }
                        else
                            showExIdTypesAreDifferent(DataTypes.TABLE_1_UNIFIED_SOCIL_TAX_DT, instanceValue);
                        changeForNewInstanceUnifiedSocialTaxTable1(id, instance);
                        isPrimitive = false;

                        break;
                    }

            }

            if(isPrimitive && instanceValue is PrimitiveType)
            {
                var primitiveInstance = GetPrimitiveInstance(id);
                var curr = (PrimitiveType)instanceValue;
                primitiveInstance.StrValue = curr.StrValue;
                changePrimitives(id, primitiveInstance);
            }
        }

        private void showExIdTypesAreDifferent(string expectedDT, IType instanceValue)
        {
            throw new Exception($"Expected identifier of {expectedDT}, but actual isn't according for it");
        }

        internal void SetIdPropValueByID(string id, string propName, string valueID)
        {
            string idType = GetIdType(id);
            var instanceValue = GetInstance(valueID);
            string value = null;
            if (instanceValue is PrimitiveType)
            {
                var curr = (PrimitiveType)instanceValue;
                value = curr.StrValue;
            }

            IType instance = GetInstance(id);
            /*switch (idType)
            {
                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                    {
                        instance = getPrEntItemByID(id);
                        break;
                    }
                case DataTypes.ONE_TAX_PAYER_REPORT_DT:
                    {
                        instance = getOneTaxPayerReportItemByID(id);
                        break;
                    }
                case DataTypes.DF_1_REPORT_DT:
                    {
                        instance = getForm1DFReportItemByID(id);
                        break;
                    }
                case DataTypes.SECTION_1_DF_1_DT:
                    {
                        instance = getForm1DFSection1ItemByID(id);
                        break;
                    }
                case DataTypes.UNIFIED_SOCIAL_TAX_REPORT_DT:
                    {
                        instance = getUnifiedSocilTaxReportItemByID(id);
                        break;
                    }
                case DataTypes.TABLE_1_UNIFIED_SOCIL_TAX_DT:
                    {
                        instance = getUnifiedSocilTaxTable1ItemByID(id);
                        break;
                    }
            }
*/
           if(instance != null)
           {
                if (value != null)
                {
                    instance.SetValue(propName, value);
                    return;
                }
                if (instanceValue != null)
                {
                    instance.SetValue(propName, instance: instanceValue);
                    return;
                }
           }
            
        }

        public void CreatePrimitive(string type, string identifier)
        {
            var instance = new PrimitiveType { Type = type, IDName = identifier };
            Primitives.Add(instance);
            Identifiers.Add(identifier);

        }

        private IType GetInstance(string id)
        {
            return Instances.Where(item => item.IDName == id).FirstOrDefault();
        }

        private PrivateEnterprenuer getPrEntItemByID( string id)
        {
            return PrivateEnreprenuers.Where(item => item.IDName == id).First();
        }
        private OneTaxPayer getOneTaxPayerReportItemByID(string id)
        {
            return OneTaxPayerReports.Where(item => item.IDName == id).First();
        }
        private Form1DF getForm1DFReportItemByID(string id)
        {
            return formDF1s.Where(item => item.IDName == id).First();
        }
        private Form1DF1Section1 getForm1DFSection1ItemByID(string id)
        {
            return form1DF1Sections.Where(item => item.IDName == id).First();
        }

        private UnifiedSocialTaxReport getUnifiedSocilTaxReportItemByID(string id)
        {
            return unifiedSocialTaxReports.Where(item => item.IDName == id).First();
        }

        private UnifiedSocialTax getUnifiedSocilTaxTable1ItemByID(string id)
        {
            return unifiedSocialTaxes.Where(item => item.IDName == id).First();
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
