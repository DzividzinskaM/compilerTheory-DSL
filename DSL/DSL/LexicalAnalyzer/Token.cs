using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class Token
    {
        public TokenType Type { get; }

        public string Value { get; }

        public int Line { get; set; }

        public Token(TokenType type, string value, int line)
        {
            this.Type = type;
            this.Value = value;
            this.Line = line;
        }

        public Token()
        {

        }
    }
}
