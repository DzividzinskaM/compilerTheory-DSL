using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class DataTypes
    {
        public const string PRIVATE_ENTERPRENUER_DT = "private-entrepreneur";
        public const string FULL_NAME_DT = "full-name";
        public const string ADDRESS_DT = "address";
        public const string PHONE_DT = "phone";
        public const string RNTRC_DT = "RNTRC";
        public const string GROUP_DT = "group";
        public const string TAX_SYSTEM_DT = "tax-system";
        public const string NACE_CODE_DT = "NACE-code";
        public const string TYPE_ONE_TAX_PAYER_DT = "type-one-tax-payer";
        public const string SFS_CODE_DT = "SFS-code";
        public const string YEAR_DT = "year";
        public const string QUARTER_DT = "quarter";
        public const string MONTH_DT = "month";
        public const string INCOME_DT = "income";
        public const string SPECIFIED_AMOUNT_DT = "specified-amount";
        public const string SUBMISSION_DATE_DT = "submission-date";
        public const string TYPE_UNIFIED_SOCIAL_TAX_DT = "type-unified-social-tax";
        public const string START_DATE_DT = "start-date";
        public const string LAST_DATE_DT = "LAST-date";
        public const string TABLE_1_UNIFIED_SOCIL_TAX_DT = "table-1-unified-social-tax";
        public const string INCOME_DECLARED_DT = "income-declared";
        public const string INCOME_AMOUNT_DT = "income-amount";
        public const string TYPE_1_DF_DT = "type-1DF";
        public const string EMPLOYEE_BY_CIVIL_CONRACT_DT = "employee-by-civil-contract";
        public const string EMPLOYEE_BY_EMPLOYMENT_CONRACT_DT = "employee-by-employment-contract";
        public const string SECTION_1_DF_1_DT = "section-1-DF1";
        public const string EMPLOYEE_ITN_DT = "employee-ITN";
        public const string INCOME_ACCURED_DT = "income-accured";
        public const string INCOME_PAID_DT = "income-paid";
        public const string INCOME_SIGN_DT = "income-sign";
        public const string DATE_ADOPT_DT = "date-adopt";
        public const string DATE_DISMISS_DT = "date-dismiss";
        public const string SIGN_DT = "sign";
        public const string UNIFIED_SOCIAL_TAX_REPORT_DT = "unified-social-tax-report";
        public const string ONE_TAX_PAYER_REPORT_DT = "one-tax-payer-report";
        public const string DF_1_REPORT_DT = "DF1-report";

        public const string NEW_METHOD = "new";
        public const string ADD_NEW_NACE_CODE_METHOD = "add-new-NACE-code";
        public const string SET_MAIN_NACE_CODE_METHOD = "set-main-NACE-code";

        public const string CHANGE_SINGLE_TAX_PERCENT_METHOD = "change-single-tax-percent";
        public const string CLARIFY_METHOD = "clarify";
        public const string SHOW_METHOD = "show";
        public const string SAVE_METHOD = "save";
        public const string ADD_MONTH_DATA_METHOD = "add-month-data";
        public const string ADD_NEW_RECORD_METHOD = "add-new-record";
        public const string DELETE_RECORD_METHOD = "delete-record";
        public const string CLARIFY_ADD_NEW_RECORD_METHOD = "clarify-add-new-record";
        public const string CLARIFY_DELETE_RECORD_METHOD = "clarify-delete-record";
        public const string NUMBER_DT = "number";

        public List<string> PropsForPrEnt = new List<string>{FULL_NAME_DT, ADDRESS_DT, PHONE_DT,
            RNTRC_DT, NACE_CODE_DT, GROUP_DT, TAX_SYSTEM_DT};
        public List<string> PropsForOneTaxPayerReport = new List<string> { PRIVATE_ENTERPRENUER_DT, TYPE_ONE_TAX_PAYER_DT, SFS_CODE_DT, YEAR_DT,
            QUARTER_DT, MONTH_DT, INCOME_DT, SPECIFIED_AMOUNT_DT, SUBMISSION_DATE_DT};
        public List<string> PropsForUnifiedSocialTaxReport = new List<string> { PRIVATE_ENTERPRENUER_DT, TYPE_UNIFIED_SOCIAL_TAX_DT, SFS_CODE_DT, START_DATE_DT,
            LAST_DATE_DT, SUBMISSION_DATE_DT, TABLE_1_UNIFIED_SOCIL_TAX_DT};
        public List<string> PropsForT1UnSocTax = new List<string> { MONTH_DT, INCOME_DECLARED_DT, INCOME_AMOUNT_DT };
        public List<string> PropsFor1DFReport = new List<string> { PRIVATE_ENTERPRENUER_DT, TYPE_1_DF_DT, QUARTER_DT, YEAR_DT, EMPLOYEE_BY_EMPLOYMENT_CONRACT_DT,
            SECTION_1_DF_1_DT};
        public List<string> PropsForSection1DF1 = new List<string> { EMPLOYEE_ITN_DT, INCOME_ACCURED_DT, INCOME_PAID_DT, INCOME_SIGN_DT, DATE_ADOPT_DT,
            DATE_DISMISS_DT, SIGN_DT};


        public List<string> MethodsForPrEnt = new List<string> { NEW_METHOD, ADD_NEW_NACE_CODE_METHOD, SET_MAIN_NACE_CODE_METHOD };
        public List<string> MethodsForOneTaxPayerReport = new List<string> { NEW_METHOD, CHANGE_SINGLE_TAX_PERCENT_METHOD, CLARIFY_METHOD,
            SHOW_METHOD, SAVE_METHOD};
        public List<string> MethodsForUnifiedSocialTaxReport = new List<string> { NEW_METHOD, SHOW_METHOD, SAVE_METHOD };
        public List<string> MethodsForT1UnSocTax = new List<string> { NEW_METHOD, ADD_MONTH_DATA_METHOD };
        public List<string> MethodsFor1DFReport = new List<string> { NEW_METHOD, SHOW_METHOD, SAVE_METHOD };
        public List<string> MethodsForSection1DF1 = new List<string> { ADD_NEW_RECORD_METHOD, DELETE_RECORD_METHOD,
            CLARIFY_ADD_NEW_RECORD_METHOD, CLARIFY_DELETE_RECORD_METHOD};

        public List<string> AttrsForAddNewNaceCode = new List<string> { NACE_CODE_DT };
        public List<string> AttrsForSetMainNaceCode = new List<string> { NACE_CODE_DT };
        public List<string> AttrsForChangeSingleTaxPercent = new List<string> { NUMBER_DT };
        public List<string> AttrsForClarify = new List<string> { SPECIFIED_AMOUNT_DT };
        public List<string> AttrsForShow = new List<string>();
        public List<string> AttrsForSave = new List<string>();
        public List<string> AttrsForAddMonthData = new List<string> { MONTH_DT, INCOME_DECLARED_DT, INCOME_AMOUNT_DT };
        public List<string> AttrsForAddNewRecord = new List<string> { EMPLOYEE_ITN_DT, INCOME_ACCURED_DT, INCOME_DECLARED_DT, INCOME_SIGN_DT,
            DATE_ADOPT_DT, DATE_DISMISS_DT};
        public List<string> AttrsForDeleteRecord = new List<string> { EMPLOYEE_ITN_DT, INCOME_ACCURED_DT, INCOME_DECLARED_DT, INCOME_SIGN_DT,
            DATE_ADOPT_DT, DATE_DISMISS_DT};
        public List<string> AttrsForClarifyAddNewRecord = new List<string> {EMPLOYEE_ITN_DT, INCOME_ACCURED_DT, INCOME_DECLARED_DT, INCOME_SIGN_DT,
            DATE_ADOPT_DT, DATE_DISMISS_DT };
        public List<string> AttrsForClarifyDeleteRecord = new List<string> {EMPLOYEE_ITN_DT};

    }
}
