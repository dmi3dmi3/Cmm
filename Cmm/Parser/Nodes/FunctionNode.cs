using System.Collections.Generic;
using Parser.Enums;
using Parser.Models;

namespace Parser.Nodes
{
    public class FunctionNode : INode
    {
        public FuncModel FuncName { get; set; }
        public List<VarModel> Args { get; set; }
    }
}