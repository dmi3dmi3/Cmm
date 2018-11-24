using Parser.Models;

namespace Parser.Nodes
{
    public class AssingNode : INode
    {
        public VarModel Left { get; set; }
        public INode Right { get; set; }
    }
}