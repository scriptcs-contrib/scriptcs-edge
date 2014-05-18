using System;
using System.Threading.Tasks;
using ScriptCs.Contracts;

namespace ScriptCs.Edge
{
    public class EdgePack : IScriptPackContext
    {
        public Func<object, Task<object>> Func(string code)
        {
            return EdgeJs.Edge.Func(code);
        }    
    }
}