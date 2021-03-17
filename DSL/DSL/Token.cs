using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class Token
    {
        public TokenType Type { get; }

        public string Value { get; }

        public Token(TokenType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public Token()
        {

        }
    }
}
