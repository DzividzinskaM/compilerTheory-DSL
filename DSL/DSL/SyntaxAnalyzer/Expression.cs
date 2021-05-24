using System;
using System.Collections.Generic;

namespace DSL
{
   

    public class Expression : AbstractExpression
    {
        public string keyWord { get; set; }

        public List<DefinitionExpression> definitionExpressions { get; set; }

        public string Identifier { get; set; }

        public string Str { get; set; }

        public Expression()
        {
            definitionExpressions = new List<DefinitionExpression>();
            type = ExpressionType.Expression;
        }
    }
}