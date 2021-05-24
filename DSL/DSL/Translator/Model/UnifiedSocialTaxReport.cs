using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class UnifiedSocialTaxReport : IType
    {
        public string IDName { get; set; }
        public PrivateEnterprenuer privateEnterprenuer { get; set; }
        public string Type { get; set; }
        public string SFSCode { get; set; }
        public DateTime Start { get; set; }
        public DateTime Last { get; set; }
        public DateTime SubmissionDate { get; set; }

        public void Show()
        {
            //open docs
        }

        public void Save(string path)
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
                    Save(attributes[0]);
                    break;
            }
        }

        public void SetValue(string prop, string value = null, IType instance = null)
        {
            switch (prop) {
                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                    privateEnterprenuer = (PrivateEnterprenuer)instance;
                    break;
                case DataTypes.TYPE_UNIFIED_SOCIAL_TAX_DT:
                    Type = value;
                    break;
                case DataTypes.SFS_CODE_DT:
                    SFSCode = value;
                    break;
                case DataTypes.START_DATE_DT:
                    Start = SpecialParsing.parserDate(value);
                    break;
                case DataTypes.LAST_DATE_DT:
                    Last = SpecialParsing.parserDate(value);
                    break;
                case DataTypes.SUBMISSION_DATE_DT:
                    SubmissionDate = SpecialParsing.parserDate(value);
                    break;

            }

        }

        internal UnifiedSocialTaxReport Clone()
        {
            return new UnifiedSocialTaxReport
            {
                privateEnterprenuer = this.privateEnterprenuer,
                Type = this.Type,
                SFSCode = this.SFSCode,
                Start = this.Start,
                Last = this.Last,
                SubmissionDate = this.SubmissionDate
            };
        }
    }
}
