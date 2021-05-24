using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class PropertyExpression : AbstractExpression
    {
        public string identifier { get; set; }

        public string keyWord { get; set; }


        public PropertyExpression()
        {
            type = ExpressionType.Property;
        }
    }
}
