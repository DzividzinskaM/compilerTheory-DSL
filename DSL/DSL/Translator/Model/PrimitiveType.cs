using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class PrimitiveType : IType
    {
        public string StrValue { get; set; }
        public string Type { get; set; }
        public string IDName { get; set; }

        public void CallMethod(string methodName, List<string> attributes)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string prop, string value = null, IType instance = null)
        {
            throw new NotImplementedException();
        }
    }
}
