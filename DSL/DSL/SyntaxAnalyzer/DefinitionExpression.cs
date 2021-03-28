using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class DefinitionExpression : AbstractExpression
    {
        public string keyWord { get; set; }

        public string identifier { get; set; }

        public DefinitionExpression()
        {
            type = ExpressionType.Definition;
        }

        
    }
}
