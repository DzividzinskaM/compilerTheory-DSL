using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class DeclarationExpression : AbstractExpression
    {
        public InitializationExpression initializationExpression { get; set; }

        public Expression expression { get; set; }

        public DeclarationExpression()
        {
            type = ExpressionType.Declaration;
        }
    }
}
