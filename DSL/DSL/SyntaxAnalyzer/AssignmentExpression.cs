using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class AssignmentExpression : AbstractExpression
    {
        public string identifier;

        public Expression Right;

        public PropertyExpression propertiesExpression;

        public AssignmentExpression()
        {
            type = ExpressionType.Assignment;
        }

    }
}
