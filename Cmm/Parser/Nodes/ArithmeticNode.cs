using Parser.Enums;

namespace Parser.Nodes
{
    public class ArithmeticNode : INode
    {
        public INode Left { get; set; }
        public INode Right { get; set; }
        public ArithmeticOperations Operation { get ; set; }
    }
}