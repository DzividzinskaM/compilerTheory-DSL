
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
                setValueForId(expression.initializationExpression.identifier,
                    expression.expression.Identifier);
                return;
            }
            TranlateDefExpr(expression.expression.definitionExpressions, 
                expression.initializationExpression.identifier);
        }


        private void TranslateAssignExpr(AssignmentExpression expression)
        {
            if (expression.identifier != null)
            {
                if (expression.Right.Identifier != null)
                    setValueForId(expression.identifier,
                    expression.Right.Identifier);
                else
                {
                    TranlateDefExpr(expression.Right.definitionExpressions, expression.identifier);
                }
            } 
            else if(expression.propertiesExpression != null)
            {
                setValueForId(expression.propertiesExpression.identifier,
                    expression.Right.Identifier,
                    expression.propertiesExpression.keyWord);

            }
        }

        private void TranlateDefExpr(List<DefinitionExpression> definitionExpressions, string identifier)
        {
            foreach(var expr in definitionExpressions)
            {
                setValueForId(identifier, expr.identifier, expr.keyWord);
            }
        }

        private void setValueForId(string id, string value, string prop = null)
        {
           Data.SetValueByPropId(id, prop, value);

           
        }

        private void TranslateInitExpr(InitializationExpression expression)
        {
            if (Data.IsAlreadyExist(expression.identifier))
                throw new Exception($"Identifier {expression.identifier} is already exist");
            switch(expression.keyWord)
            {
                case (DataTypes.PRIVATE_ENTERPRENUER_DT):
                    createNewPrivateEnterprenuer(expression.identifier);
                    break;
                case (DataTypes.ONE_TAX_PAYER_REPORT_DT):
                    createNewOneTaxPayerReport(expression.identifier);
                    break;
                //add other types;
            }

        }

        private void createNewOneTaxPayerReport(string identifier)
        {
            Data.OneTaxPayerReports.Add(new OneTaxPayer { IDName = identifier });
            Data.Identifiers.Add(identifier);
        }

        private void createNewPrivateEnterprenuer(string identifier)
        {
            Data.PrivateEnreprenuers.Add(new PrivateEnterprenuer { IDName = identifier });
            Data.Identifiers.Add(identifier);
        }
    }
}
