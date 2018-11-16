using System;
using Parser.Enums;

namespace Parser.Models
{
    public class VarModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Types Type { get; set; }

        public VarModel(string name, int id, Types type)
        {
            Name = name;
            Id = id;
            Type = type;
        }

        public VarModel(string name, int id, string type)
        {
            Name = name;
            Id = id;
            switch (type)
            {
                case "string":
                    Type = Types.String;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}