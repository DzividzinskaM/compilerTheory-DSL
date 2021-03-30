using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DSL
{
   
    public class SemanticAnalyzer
    {
        public DataTypes DataTypes { get; }

        public Dictionary<string, string> InitializationVariables { get; set; }

        public List<AbstractExpression> Expressions { get; set; }

        public SemanticAnalyzer(List<AbstractExpression> expressions)
        {
            Expressions = expressions;
            DataTypes = new DataTypes();
            InitializationVariables = new Dictionary<string, string>();
        }

        public void Analyze()
        {
            foreach(var expr in Expressions)
            {
                AnalyzeExpression(expr);
            }
        }

        private void AnalyzeExpression(AbstractExpression expr)
        {
            if (expr is InitializationExpression)
                AnalyzeInitializationExpr((InitializationExpression)expr);
            else if (expr is AssignmentExpression)
                AnalyzeAssignmentExpr((AssignmentExpression)expr);
            else if (expr is DeclarationExpression)
                AnalyzeDecExpr((DeclarationExpression)expr);
            else if (expr is OperationExpression)
                AnalyzeOpExpr((OperationExpression)expr);
        }

        private void AnalyzeOpExpr(OperationExpression expr)
        {
            if (!InitializationVariables.ContainsKey(expr.identifier))
                throw new Exception($"{expr.identifier} isn't initialized");
            if (!checkPropForMethod(expr.methodName, InitializationVariables[expr.identifier]))
                throw new Exception($"Method ${expr.methodName} isn't available for {InitializationVariables[expr.identifier]} data type");
            checkMethodAttrs(expr.methodName, expr.attributes);
        }

        private void checkMethodAttrs(string method, List<string> attributes)
        {
            List<string> lst = new List<string>();
            switch (method)
            {
                case DataTypes.ADD_NEW_NACE_CODE_METHOD:
                    lst = DataTypes.AttrsForAddNewNaceCode;
                    break;
                case DataTypes.SET_MAIN_NACE_CODE_METHOD:
                    lst = DataTypes.AttrsForSetMainNaceCode;
                    break;
                case DataTypes.CHANGE_SINGLE_TAX_PERCENT_METHOD:
                    lst = DataTypes.AttrsForChangeSingleTaxPercent;
                    break;
                case DataTypes.CLARIFY_METHOD:
                    lst = DataTypes.AttrsForClarify;
                    break;
                case DataTypes.SHOW_METHOD:
                    lst = DataTypes.AttrsForShow;
                    break;
                case DataTypes.SAVE_METHOD:
                    lst = DataTypes.AttrsForSave;
                    break;
                case DataTypes.ADD_MONTH_DATA_METHOD:
                    lst = DataTypes.AttrsForAddMonthData;
                    break;
                case DataTypes.ADD_NEW_RECORD_METHOD:
                    lst = DataTypes.AttrsForAddNewRecord;
                    break;
                case DataTypes.DELETE_RECORD_METHOD:
                    lst = DataTypes.AttrsForDeleteRecord;
                    break;
                case DataTypes.CLARIFY_ADD_NEW_RECORD_METHOD:
                    lst = DataTypes.AttrsForClarifyAddNewRecord;
                    break;
                case DataTypes.CLARIFY_DELETE_RECORD_METHOD:
                    lst = DataTypes.AttrsForClarifyDeleteRecord;
                    break;
                default: 
                    break;
            }

            if (attributes.Count != lst.Count)
                throw new Exception($"{method} doesn't have appropriate attributes");

            for(int i=0; i<lst.Count; i++)
            {
                checkCorrectValueForType(lst[i], attributes[i]);
            }
        }

        private bool checkPropForMethod(string method, string DT)
        {
            List<string> lst = new List<string>();
            switch (DT)
            {
                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                    lst = DataTypes.MethodsForPrEnt;
                    break;
                case DataTypes.ONE_TAX_PAYER_REPORT_DT:
                    lst = DataTypes.MethodsForOneTaxPayerReport;
                    break;
                case DataTypes.UNIFIED_SOCIAL_TAX_REPORT_DT:
                    lst = DataTypes.MethodsForUnifiedSocialTaxReport;
                    break;
                case DataTypes.TABLE_1_UNIFIED_SOCIL_TAX_DT:
                    lst = DataTypes.MethodsForT1UnSocTax;
                    break;
                case DataTypes.DF_1_REPORT_DT:
                    lst = DataTypes.MethodsFor1DFReport;
                    break;
                case DataTypes.SECTION_1_DF_1_DT:
                    lst = DataTypes.MethodsForSection1DF1;
                    break;
                default:
                    break;
            }
            return isLstContain(method, lst);

        }

        private void AnalyzeDecExpr(DeclarationExpression expr)
        {
            AnalyzeInitializationExpr(expr.initializationExpression);
            AnalyzeExpr(expr.expression, expr.initializationExpression.keyWord);
        }

        private void AnalyzeAssignmentExpr(AssignmentExpression expr)
        {
            string leftDT;
            if (expr.identifier != null)
            {
                if (!IsIdInitialized(expr.identifier))
                    createExNotInitializedExpr(expr.identifier);
                leftDT = InitializationVariables[expr.identifier];
            }
            else if (expr.propertiesExpression != null)
            {
                AnalyzePropExpr(expr.propertiesExpression);
                leftDT = expr.propertiesExpression.keyWord;
            }
            else
                throw new Exception("Can not read left part of assignment expression");

            if (expr.Right != null)
                AnalyzeExpr(expr.Right, leftDT);
            else
                throw new Exception("Can not read right part of assignment expression");
        }

        private void AnalyzePropExpr(PropertyExpression expr)
        {
            if (!IsIdInitialized(expr.identifier))
                createExNotInitializedExpr(expr.identifier);
            if(!checkPropForId(expr.keyWord, InitializationVariables[expr.identifier]))
                throw new Exception($"For {expr.identifier} property {expr.keyWord} isn't use");
        }

        private bool checkPropForId(string keyWord, string DT)
        {
            List<string> lst = new List<string>();
            switch (DT)
            {
                case DataTypes.PRIVATE_ENTERPRENUER_DT:
                    lst = DataTypes.PropsForPrEnt;
                    break;
                case DataTypes.ONE_TAX_PAYER_REPORT_DT:
                    lst = DataTypes.PropsForOneTaxPayerReport;
                    break;
                case DataTypes.UNIFIED_SOCIAL_TAX_REPORT_DT:
                    lst = DataTypes.PropsForUnifiedSocialTaxReport;
                    break;
                case DataTypes.TABLE_1_UNIFIED_SOCIL_TAX_DT:
                    lst = DataTypes.PropsForT1UnSocTax;
                    break;
                case DataTypes.DF_1_REPORT_DT:
                    lst = DataTypes.PropsFor1DFReport;
                    break;
                case DataTypes.SECTION_1_DF_1_DT:
                    lst = DataTypes.PropsForSection1DF1;
                    break;
                default:
                    break;
            }
            return isLstContain(keyWord, lst);                
        }

        private bool isLstContain(string keyWord, List<string> propsLst)
        {
            bool a = propsLst.Contains(keyWord);
            return a;
        }

        private void createExNotInitializedExpr(string identifier)
        {
            throw new Exception($"Can't find initialized identifier {identifier}");
        }

        private void AnalyzeExpr(Expression expr, string leftDT)
        {
            if(expr.keyWord != null && expr.definitionExpressions!= null)
            {
                if (leftDT != expr.keyWord)
                    throw new Exception("Right part of expression doesn't match type in left part");
                AnalyzeDefExpr(expr.definitionExpressions, leftDT);
            }else if(expr.Identifier != null)
            {
                checkCorrectValueForType(leftDT, expr.Identifier);
            }
        }

        private void AnalyzeDefExpr(List<DefinitionExpression> exprs, string leftDT)
        {
            foreach(var expr in exprs)
            {
                if (!checkPropForId(expr.keyWord, leftDT))
                    throw new Exception($"Property {expr.keyWord} isn't use for {leftDT}");
                checkCorrectValueForType(expr.keyWord, expr.identifier);
            }
        }

        private void AnalyzeInitializationExpr(InitializationExpression expr)
        {
            if (IsIdInitialized(expr.identifier))
                throw new Exception("You try to initialize identifier again");
            AddNewID(expr.identifier, expr.keyWord);
        }

        private void AddNewID(string identifier, string keyWord)
        {
            InitializationVariables.Add(identifier, keyWord);
        }

        private bool IsIdInitialized(string identifier)
        {
            return InitializationVariables.ContainsKey(identifier);
        }

        private void checkCorrectValueForType(string dataType, string value)
        {
            bool flag = true;
            if (dataType == DataTypes.FULL_NAME_DT)
                flag = isCorrectFullName(value);
            else if (dataType == DataTypes.PHONE_DT)
                flag = isCorrectPhone(value);
            else if (dataType == DataTypes.RNTRC_DT)
                flag = isCorrectRNTRC(value);
            else if (dataType == DataTypes.GROUP_DT)
                flag = isCorrectGroup(value);
            else if (dataType == DataTypes.TAX_SYSTEM_DT)
                flag = isCorrectTaxSystem(value);
            else if (dataType == DataTypes.NACE_CODE_DT)
                flag = isCorrectNaceCode(value);
            else if (dataType == DataTypes.TYPE_ONE_TAX_PAYER_DT)
                flag = isCorrectTypeOneTaxPayer(value);
            else if (dataType == DataTypes.SFS_CODE_DT)
                flag = isCorrectSfsCode(value);
            else if (dataType == DataTypes.YEAR_DT)
                flag = isNumberInRange(value, 0, 9999);
            else if (dataType == DataTypes.QUARTER_DT)
                flag = isNumberInRange(value, 1, 4);
            else if (dataType == DataTypes.MONTH_DT)
                flag = isNumberInRange(value, 1, 12);
            else if (dataType == DataTypes.INCOME_DT || dataType == DataTypes.SPECIFIED_AMOUNT_DT
                || dataType == DataTypes.INCOME_DECLARED_DT || dataType == DataTypes.INCOME_AMOUNT_DT
                || dataType == DataTypes.INCOME_ACCURED_DT || dataType == DataTypes.INCOME_PAID_DT)
                flag = isMoneyValue(value);
            else if (dataType == DataTypes.SUBMISSION_DATE_DT || dataType == DataTypes.START_DATE_DT
                || dataType == DataTypes.LAST_DATE_DT || dataType == DataTypes.DATE_ADOPT_DT
                || dataType == DataTypes.DATE_DISMISS_DT)
                flag = isDateValue(value);
            else if (dataType == DataTypes.TYPE_UNIFIED_SOCIAL_TAX_DT)
                flag = isCorrectTypeUnSocTax(value);
            else if (dataType == DataTypes.TYPE_1_DF_DT)
                flag = isCorrectTypeDF1(value);
            else if (dataType == DataTypes.EMPLOYEE_BY_CIVIL_CONRACT_DT || dataType == DataTypes.EMPLOYEE_BY_EMPLOYMENT_CONRACT_DT)
                flag = isPositiveNum(value);
            else if (dataType == DataTypes.EMPLOYEE_ITN_DT)
                flag = isCorrectRNTRC(value);
            else if (dataType == DataTypes.INCOME_SIGN_DT)
                flag = isCorrectIncomeSign(value);
            else if (dataType == DataTypes.SIGN_DT)
                flag = isNumberInRange(value, 0, 1);
            else if (dataType == DataTypes.NUMBER_DT)
                flag = isPositiveNum(value);

            if (!flag)
                throw new Exception($"Incorrect value {value} for {dataType} data type");
        }


        private bool isCorrectIncomeSign(string value)
        {
            string pattern = @"\d{3}";
            return Regex.IsMatch(value, pattern);
        }

        private bool isPositiveNum(string value)
        {
            if (!Int32.TryParse(value, out int res) || res < 0)
                return false;
            return true;
        }

        private bool isCorrectTypeDF1(string value)
        {
            List<string> types = new List<string>() { "\"reporting\"", "\"reporting-new\"", "\"clarifying\"" };
            return isLstContain(value, types);
        }

        private bool isCorrectTypeUnSocTax(string value)
        {
            List<string> types = new List<string>() { "\"initial\"", "\"liquidation\"", "\"pension\"" };
            return isLstContain(value, types);
        }

        private bool isDateValue(string value)
        {
            return DateTime.TryParse(value, out DateTime res);
        }

        private bool isMoneyValue(string value)
        {
            return decimal.TryParse(value, out decimal res);
        }

        private bool isNumberInRange(string value, int start, int end)
        {
            if (value.Length == 0 || !Int32.TryParse(value, out int res) || res < start || res > end)
                return false;
            return true;
        }

        private bool isCorrectSfsCode(string value)
        {
            string SFSCodePattern = @"\d{4}";
            return Regex.IsMatch(value, SFSCodePattern);
        }

        private bool isCorrectTypeOneTaxPayer(string value)
        {
            List<string> types = new List<string>() { "\"reporting\"", "\"reporting-new\"", "\"clarifying\"", "\"additional\"" };
            return isLstContain(value, types);
        }

        private bool isCorrectNaceCode(string value)
        {
            string NaceCodePattern = @"\d{2}.\d{2}";
            return Regex.IsMatch(value, NaceCodePattern);
        }

        private bool isCorrectTaxSystem(string value)
        {
            List<string> taxSystems = new  List<string>() { "\"simple\"", "\"general\""};
            return isLstContain(value, taxSystems);
        }

        private bool isCorrectGroup(string value)
        {
            string groupPattern = @"[1-4]{1}";
            if(value.Length != 1 || !Regex.IsMatch(value, groupPattern))
                return false;
            return true;
        }

        private bool isCorrectRNTRC(string value)
        {
            string RNTRCpattern1 = @"\d{10}";
            string RNTRCpattern2 = @"\d{9}";
            string RNTRCpattern3 = @"\w\w\d{6}";
            if (!Regex.IsMatch(value, RNTRCpattern1) && !Regex.IsMatch(value, RNTRCpattern2)
                && !Regex.IsMatch(value, RNTRCpattern3))
                return false;
            return true;
        }

        private bool isCorrectPhone(string value)
        {
            return true;
        }

        private bool isCorrectFullName(string value)
        {
            if (!isStrContainsOnlyLetter(value.Substring(1, value.Length-2)))
                return false;
            return true;
        }

        private bool isStrContainsOnlyLetter(string value)
        {
            foreach (var sym in value)
                if (!Char.IsLetter(sym))
                    return false;
            return true;
        }
    }
}
