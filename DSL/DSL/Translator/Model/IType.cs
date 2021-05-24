using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public interface IType
    {
        public string IDName { get; set; }

        public void SetValue(string prop, string value = null, IType instance = null);
        public void CallMethod(string methodName, List<string> attributes);

    }
}
