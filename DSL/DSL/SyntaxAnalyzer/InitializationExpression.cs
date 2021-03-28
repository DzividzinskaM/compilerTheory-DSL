using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class InitializationExpression : AbstractExpression
    {
        public string keyWord { get; set; }

        public string identifier { get; set; }

        public InitializationExpression()
        {
            type = ExpressionType.Initialization;
        }
    }
}
