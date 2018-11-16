using System.Collections.Generic;
using Parser.Enums;

namespace Parser.Models
{
    public class Node
    {
        public Operations Operation { get; set; }
        public List<Node> Args { get; set; }
    }
}