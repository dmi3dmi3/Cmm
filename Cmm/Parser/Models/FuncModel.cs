using System.Collections.Generic;
using Parser.Enums;

namespace Parser.Models
{
    public class FuncModel
    {
        public string Name { get; set; }
        public List<List<Types>> ArgsType { get; set; }

        public FuncModel(string name)
        {
            Name = name;
        }

        public FuncModel(string name, List<Types> args)
        {
            Name = name;
            ArgsType.Add(args);
        }

        public bool CheckArgs(List<Types> args)
        {
            return ArgsType.Contains(args);
        }
    }
}