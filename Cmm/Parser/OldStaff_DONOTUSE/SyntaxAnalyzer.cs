using System;
using Parser.Models;
using Parser.Nodes;
using System.Collections.Generic;
using System.Linq;
using Parser.Exceptions;
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
                    AssingNode asNode = new AssingNode();
                    asNode.Left = new VarNode()
                    {
                        Variable = VarTable.Get(first.Text),
                    };


                    if (!lc.TryGetNext(out var next))
                    {
                        throw new ParseException("Function or expression expected", lc.GetPrevious(), true);
                    }

                    if (FuncTable.Contains(next.Text))
                    {
                        lc.SetBack();
                        asNode.Right = Func(lc);
                    }
                    else if (VarTable.Contains(next.Text) || next.Text.All(c => Constants.Numbers.Contains(c)))
                    {
                        lc.SetBack();
                        //todo arithmetic
                        throw new NotImplementedException("Arithmetic don't included, for now");
                    }
                    else
                    {
                        throw new ParseException("Function or expression expected", lc.GetPrevious(), true);
                    }
                }
            }
        }

        private static FunctionNode Func(ListController<TokenModel> lc)
        {
            if (!lc.TryGetNext(out var funcName))
            {
                throw new ParseException("Function expected", lc.GetPrevious(), true);
            }

            if (!FuncTable.Contains(funcName.Text))
            {
                throw new ParseException("Unknown function", lc.GetPrevious(), true);
            }

            var resNode = new FunctionNode();
            resNode.FuncName = funcName.Text;
            if (!lc.TryGetNext(out var opNest))
            {
                throw new ParseException();
            }
            else
            {
                if (opNest.Text == "(")
                {
                    while (true)
                    {
                        if (lc.TryGetNext(out var next))
                        {
                            if (next.Text == ")")
                            {
                                break;
                            }

                            if ()
                            {
                            }
                        }
                    }
                }
            }

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