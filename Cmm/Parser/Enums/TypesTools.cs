using System;
using System.Collections.Generic;

namespace Parser.Enums
{
    public static class TypesTools
    {
        public static Types ToTypes(string str)
        {
            switch (str)
            {
                case "string":
                    return Types.String;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown type '{str}'");
            }
        }

        public static List<Types> ToTypes(List<string> list)
        {
            var res = new List<Types>();
            foreach (var item in list)
            {
                res.Add(ToTypes(item));
            }

            return res;
        }
    }
}