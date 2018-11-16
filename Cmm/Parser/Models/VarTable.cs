using System.Collections.Generic;
using System.Linq;

namespace Parser.Models
{
    public class VarTable
    {
        private List<VarModel> Variables { get; set; }

        public VarTable()
        {
            Variables = new List<VarModel>();
        }

        public VarModel Get(int index)
        {
            return Variables[index];
        }

        public VarModel Get(string name)
        {
            return Variables.First(model => model.Name == name);
        }

        public bool Contains(string name)
        {
            return Variables.Exists(model => model.Name == name);
        }

        public void Add(string name, string type)
        {
            var i = Variables.Count;
            Variables.Add(new VarModel(name, i, type));
        }
    }
}