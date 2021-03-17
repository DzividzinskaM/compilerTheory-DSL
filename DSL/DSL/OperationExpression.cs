using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class OperationExpression : AbstractExpression
    { 
        public string identifier { get;set; }
        public string methodName { get; set; }

        public List<string> attributes { get; set; }

        public OperationExpression()
        {
            type = ExpressionType.Operation;
        }

    }
}
