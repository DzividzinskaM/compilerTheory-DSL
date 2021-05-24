using System;
using System.Collections.Generic;
using System.Text;
using Word = Microsoft.Office.Interop.Word;

namespace DSL
{
    public class OneTaxPayer : IType
    {
        PrivateEnterprenuer privateEnterprenuer { get; set; }
        public string SFSCode { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public int Month { get; set; }
        public decimal Income { get; set; }
        public decimal SpecifiedAmount { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string IDName { get; set; }

        private float TaxPercent = 10;
        private const int MIN = 6306;

        public void ChangeSingleTaxPercent(float newPercent)
        {
            TaxPercent = newPercent;
        }

        public void Clatify(decimal newAmount)
        {
            SpecifiedAmount = newAmount;
            Type = "clarify";
        }

        public void Show()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            // { "\"reporting\"", "\"reporting-new\"", "\"clarifying\"", "\"additional\"" }
            Console.WriteLine("Податкова декларацiя платника єдиного податку - фiзичної особи - підприємця");
            Console.Write("Тип декларації: ");
            if (Type == "\"reporting\"")
                Console.WriteLine("звітна");
            else if (Type == "\"reporting-new\"")
                Console.WriteLine("звітна нова");
            else if (Type == "\"clarifying\"")
                Console.WriteLine("уточнююча");
            else if (Type == "\"additional\"")
                Console.WriteLine("довідково");

            Console.WriteLine("Податковий звітній період: ");
            if (privateEnterprenuer.Group == 1 || privateEnterprenuer.Group == 2)
                Console.WriteLine("рік");
            else if(Quarter == 1)
                Console.WriteLine("I квартал");
            else if (Quarter == 2)
                Console.WriteLine("півріччя");
            else if (Quarter == 3)
                Console.WriteLine("три квартали");

            if (Month > 0)
                Console.WriteLine($"Місяць {Month}");
            Console.WriteLine($"{Year} року");
            Console.WriteLine($"Номер контролюючого органу - {SFSCode}");
            Console.WriteLine($"Податкова адреса {privateEnterprenuer.Address}");
            Console.WriteLine($"Реєстраційний номер облікової картки платника податків або серія та номер паспорта - {privateEnterprenuer.RNTRC}");
            Console.WriteLine("I. Загальні показники підприжмницької діяльності");
            Console.WriteLine("Фактична чисельність працівників у звітному періоді - 0");
            Console.WriteLine("Види підприємницької діяльності у звітному періоді");
            foreach (var code in privateEnterprenuer.NaceCodes)
                Console.WriteLine($"Код згідно КВЕД {code}");
            Console.WriteLine("II. Показники господарської діяльності для платників єдиного податку першої групи");
            Console.WriteLine($"I квартал - {MIN /100 * TaxPercent}");
            Console.WriteLine($"II квартал - {MIN / 100 * TaxPercent}");
            Console.WriteLine($"III квартал - {MIN / 100 * TaxPercent}");
            Console.WriteLine($"IV квартал - {MIN / 100 * TaxPercent}");

            Console.WriteLine($"Обсяг доходу - {Income}");
            Console.WriteLine($"Дата подання декларації - {SubmissionDate}");

        }


        public void Save()
        {
            string path = "./../../../../../../templates/one-tax-payer-template.xlsx";
            string ResPath = "./../../../../../../results/one-tax-payer-report.xlsx";
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            var wordDoc = wordApp.Documents.Open(path);
            replace(DataTypes.TYPE_ONE_TAX_PAYER_DT, "звітна", wordDoc);
            replace(DataTypes.YEAR_DT, Year.ToString(), wordDoc);

            wordDoc.SaveAs(ResPath);
            wordApp.Visible = true;


        }

        private void replace(string tmp, string value, Word.Document wordDoc)
        {
            var range = wordDoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: tmp, ReplaceWith: value);
        }

        public void SetValue(string prop, string value = null, IType instance = null)
        {
            if (value == null && instance == null)
                return;
            switch(prop)
            {
                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                    if (instance != null && instance is PrivateEnterprenuer)
                        privateEnterprenuer = (PrivateEnterprenuer)instance;
                    break;
                case DataTypes.SFS_CODE_DT:
                    SFSCode = value;
                    break;
                case DataTypes.TYPE_ONE_TAX_PAYER_DT:
                    Type = value;
                    break;
                case DataTypes.YEAR_DT:
                    Year = Int32.Parse(value);
                    break;
                case DataTypes.QUARTER_DT:
                    Quarter = Int32.Parse(value);
                    break;
                case DataTypes.MONTH_DT:
                    Month = Int32.Parse(value);
                    break;
                case DataTypes.INCOME_DT:
                    Income = decimal.Parse(value);
                    break;
                case DataTypes.SPECIFIED_AMOUNT_DT:
                    SpecifiedAmount = decimal.Parse(value);
                    break;
                case DataTypes.SUBMISSION_DATE_DT:
                    SubmissionDate = SpecialParsing.parserDate(value);
                    break;
                default: return;
            }
        }


        public void CallMethod(string methodName, List<string> attributes)
        {
            switch (methodName)
            {
                case DataTypes.CHANGE_SINGLE_TAX_PERCENT_METHOD:
                    ChangeSingleTaxPercent(float.Parse(attributes[0]));
                    break;
                case DataTypes.CLARIFY_METHOD:
                    Clatify(decimal.Parse(attributes[0]));
                    break;
                case DataTypes.SHOW_METHOD:
                    Show();
                    break;
                case DataTypes.SAVE_METHOD:
                    Save();
                    break;
                default: return;
            }
        }

        internal OneTaxPayer Clone()
        {
            return new OneTaxPayer { 
                SFSCode = this.SFSCode, 
                Type = this.Type, 
                Year = this.Year, 
                Quarter = this.Quarter, 
                Income = this.Income,
                SpecifiedAmount = this.SpecifiedAmount,
                SubmissionDate = this.SubmissionDate
            };
        }
    }
}
