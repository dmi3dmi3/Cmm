using Parser.Models;
using System.Collections.Generic;
using Parser.Enums;
using Tools;

namespace Parser
{
    public static class SyntaxAnalyzer
    {
        public static List<Node> Commands;
        public static VarTable VarTable { get; set; }
        public static FuncTable FuncTable { get; set; }

        static SyntaxAnalyzer()
        {
            VarTable = new VarTable();
            FuncTable = new FuncTable();
        }

        public static List<Node> DoSynAn(List<TokenModel> inputList)
        {
            var resList = new List<Node>();
            var lc = new ListController<TokenModel>(inputList);


            return resList;
        }

        public static void Lang(ListController<TokenModel> lc)
        {
            while (true)
            {
                Command(lc);
                if (!lc.TryGetNext(out var sc))
                    throw new ParseException("; expected", lc.GetPrevious(), true);

                if (sc.Text != ";")
                    throw new ParseException("; expected", lc.GetPrevious(), true);

                if (!lc.TryGetNext(out _))
                    return;

                lc.SetBack();
            }
        }

        private static void Command(ListController<TokenModel> lc)
        {
            if (lc.TryGetNext(out var first))
            {
                if (Keywords.Types.Contains(first.Text))
                {
                    var varType = first.Text;
                    string varName = Variable(lc);
                    VarTable.Add(varName, varType);
                }
                else if (VarTable.Contains(first.Text))
                {
                    if (!lc.TryGetNext(out var eq))
                        throw new ParseException("= expected", lc.GetPrevious(), true);

                    if (eq.Text != "=")
                        throw new ParseException("= expected", lc.GetPrevious(), true);

                    var curNode = new Node()
                    {
                        Operation = Operations.assign,
                        ///Todo different nodes assign, Exec, arifmet
                    };

                    var res = Func(lc);
                }
            }
        }

        private static Node Func(ListController<TokenModel> lc)
        {
            //todo parse tokens to node
        }

        private static string Variable(ListController<TokenModel> lc)
        {
            if (!lc.TryGetNext(out var variable))
                throw new ParseException("Variable name expected", lc.GetPrevious(), true);

            if (!Constants.Alphabet.Contains(variable.Text[0]))
                throw new ParseException($"Invalid token '{variable.Text[0]}' in variable name", variable);

            return variable.Text;

        }
    }
}