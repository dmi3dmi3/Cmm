using Parser.Models;

namespace Parser.Nodes
{
    public class VarNode : INode
    {
        public VarModel Variable { get; set; }
    }
}