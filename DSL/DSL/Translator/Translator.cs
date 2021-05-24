
using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class Translator
    {
        private ProgramData Data;
        List<AbstractExpression> Expressions;

        public Translator(List<AbstractExpression> exressions)
        {
            Expressions = exressions;
            Data = new ProgramData();
        }

        public void Translate()
        {
            foreach(var expression in Expressions)
            {
                if (expression is InitializationExpression)
                    TranslateInitExpr((InitializationExpression)expression);
                else if (expression is AssignmentExpression)
                    TranslateAssignExpr((AssignmentExpression)expression);
                else if (expression is DeclarationExpression)
                    TranslateDeclExpression((DeclarationExpression)expression);
                else if (expression is OperationExpression)
                    TranslateOpExpr((OperationExpression)expression);

            }

        }

        private void TranslateOpExpr(OperationExpression expression)
        {
            Data.CallMethod(expression.identifier, expression.methodName, expression.attributes);
        }

        private void TranslateDeclExpression(DeclarationExpression expression)
        {
            TranslateInitExpr(expression.initializationExpression);

            if(expression.expression.Identifier != null)
            {
                setIdById(expression.initializationExpression.identifier,
                    expression.expression.Identifier);
                return;
            }
            else if(expression.expression.Str != null)
            {
                setStringById(expression.initializationExpression.identifier, expression.expression.Str);
            }
            TranlateDefExpr(expression.expression.definitionExpressions, 
                expression.initializationExpression.identifier);
        }

        private void setStringById(string identifier, string str)
        {
            Data.SetStrById(identifier, str);
        }

        private void TranslateAssignExpr(AssignmentExpression expression)
        {
            if (expression.identifier != null)
            {
                if (expression.Right.Identifier != null)
                    setIdById(expression.identifier, expression.Right.Identifier);
                else
                {
                    TranlateDefExpr(expression.Right.definitionExpressions, expression.identifier);
                }
            } 
            else if(expression.propertiesExpression != null)
            {
                if (expression.Right.Identifier != null)
                    setIdPropByID(expression.propertiesExpression.identifier, expression.propertiesExpression.keyWord,
                        expression.Right.Identifier);
                if (expression.Right.Str != null)
                    setStringPropByID(expression.propertiesExpression.identifier, expression.propertiesExpression.keyWord,
                        expression.Right.Str);
            }
        }

        private void TranlateDefExpr(List<DefinitionExpression> definitionExpressions, string identifier)
        {
            foreach(var expr in definitionExpressions)
            {
                if (expr.str != null) //property id
                    setStringPropByID(identifier, expr.keyWord, expr.str);
                else if(Int32.TryParse(expr.str, out int result))
                {
                    setStringPropByID(identifier, expr.keyWord, result.ToString());
                }
                if (expr.identifier != null)
                    setIdPropByID(identifier, expr.keyWord, expr.identifier);
                //setValueForId(identifier, expr.identifier, expr.keyWord);
            }
        }

        private void setIdPropByID(string ID, string propName, string valueID)
        {
            Data.SetIdPropValueByID(ID, propName, valueID);
        }

        private void setStringPropByID(string identifier, string propName, string propValueStr)
        {
            Data.SetStrPropValueByID(identifier, propName, propValueStr);
        }

        private void setIdById(string id, string valueId)
        {
            Data.SetIdById(id, valueId);
        }

       /* private void setValueForId(string id, string value, string prop = null)
        {
            Data.SetValueByPropId(id, prop, value);
        }*/

        private void TranslateInitExpr(InitializationExpression expression)
        {
            if (Data.IsAlreadyExist(expression.identifier))
                throw new Exception($"Line {expression.Line}: Identifier {expression.identifier} is already exist");
            bool isPrimitive = true;
            switch(expression.keyWord)
            {
                case (DataTypes.PRIVATE_ENTERPRENUER_DT):
                    createNewPrivateEnterprenuer(expression.identifier);
                    isPrimitive = false;
                    break;
                case (DataTypes.ONE_TAX_PAYER_REPORT_DT):
                    createNewOneTaxPayerReport(expression.identifier);
                    isPrimitive = false;
                    break;
                case (DataTypes.DF_1_REPORT_DT):
                    createNewForm1DF(expression.identifier);
                    isPrimitive = false;
                    break;
                case (DataTypes.SECTION_1_DF_1_DT):
                    createNewForm1DFSection1(expression.identifier);
                    isPrimitive = false;
                    break;
                case (DataTypes.UNIFIED_SOCIAL_TAX_REPORT_DT):
                    createNewUnifiedSocialTaxReport(expression.identifier);
                    isPrimitive = false;
                    break;
                case (DataTypes.TABLE_1_UNIFIED_SOCIL_TAX_DT):
                    createNewUnifiedSocialTaxTable1(expression.identifier);
                    isPrimitive = false;
                    break;
            }

            if (isPrimitive)
                Data.CreatePrimitive(expression.keyWord, expression.identifier);



        }

        private void createNewOneTaxPayerReport(string identifier)
        {
            var instance = new OneTaxPayer { IDName = identifier };
            Data.OneTaxPayerReports.Add(instance);
            Data.Identifiers.Add(identifier);
            Data.Instances.Add(instance);

        }

        private void createNewPrivateEnterprenuer(string identifier)
        {
            var instance = new PrivateEnterprenuer { IDName = identifier };
            Data.PrivateEnreprenuers.Add(instance);
            Data.Identifiers.Add(identifier);
            Data.Instances.Add(instance);

        }

        private void createNewForm1DF(string identifier)
        {
            var instance = new Form1DF { IDName = identifier };
            Data.formDF1s.Add(instance);
            Data.Identifiers.Add(identifier);
            Data.Instances.Add(instance);
        }

        private void createNewForm1DFSection1(string identifier)
        {
            var instance = new Form1DF1Section1 { IDName = identifier };
            Data.Identifiers.Add(identifier);
            Data.form1DF1Sections.Add(instance);
            Data.Instances.Add(instance);
        }

        private void createNewUnifiedSocialTaxReport(string identifier)
        {
            var instance = new UnifiedSocialTaxReport { IDName = identifier };
            Data.Identifiers.Add(identifier);
            Data.unifiedSocialTaxReports.Add(instance);
            Data.Instances.Add(instance);
        }

        private void createNewUnifiedSocialTaxTable1(string identifier)
        {
            var instance = new UnifiedSocialTax { IDName = identifier };
            Data.Identifiers.Add(identifier);
            Data.unifiedSocialTaxes.Add(instance);
            Data.Instances.Add(instance);
        }
    }
}
