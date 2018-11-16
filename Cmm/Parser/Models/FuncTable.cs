using Parser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parser.Models
{
    public class FuncTable
    {
        public List<FuncModel> Funcs { get; set; }

        public FuncTable()
        {
            Funcs = new List<FuncModel>();

            Funcs.Add(new FuncModel("read"));
            Funcs.Add(new FuncModel("write", new List<Types>() { Types.String }));
        }

        public bool Contains(string name)
        {
            return Funcs.Exists(model => model.Name == name);
        }

        public FuncModel Get(string name)
        {
            return Funcs.First(model => model.Name == name);
        }

        public bool CheckFunc(string name, List<string> args)
        {
            if (!Contains(name))
            {
                throw new Exception($"Cannot find function '{name}'");
            }

            var argsType = TypesTools.ToTypes(args);
            var item = Get(name);
            return Funcs.Exists(model => model.ArgsType.Contains(argsType));
        }

        public bool CheckFunc(string name, List<Types> args)
        {
            if (!Contains(name))
            {
                throw new Exception($"Cannot find function '{name}'");
            }

            var item = Get(name);
            return Funcs.Exists(model => model.ArgsType.Contains(args));
        }

        public void Add(string name, List<string> argsType)
        {
            var types = TypesTools.ToTypes(argsType);
            if (Contains(name))
            {
                Get(name).ArgsType.Add(types);
            }
            else
            {
                if (CheckFunc(name, types))
                    throw new Exception($"Function '{name}' with such arguments is already declared");

                Funcs.Add(new FuncModel(name, types));
            }
        }
    }
}