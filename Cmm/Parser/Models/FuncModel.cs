using System.Collections.Generic;
using Parser.Enums;

namespace Parser.Models
{
    public class FuncModel
    {
        public string Name { get; set; }
        public bool HasReturn { get; set; }
        public List<Types> ArgsType { get; set; }
        public Types ReturnType { get; set; }

        public FuncModel(string name)
        {
            Name = name;
        }

        public FuncModel(string name, Types returnType, List<Types> args)
        {
            Name = name;
            ArgsType = args;
            ReturnType = returnType;
            HasReturn = true;
        }

        public FuncModel(string name, List<Types> args)
        {
            Name = name;
            ArgsType = args;
            HasReturn = false;
        }
    }
}