using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{ 
    public enum ExpressionType
    {
        Expression,
        Initialization,
        Property,
        Definition,
        Declaration,
        Assignment, 
        Operation

    }
    public abstract class AbstractExpression
    {
        public ExpressionType type { get; set; }
        public int Line { get; set; }

    }
}
