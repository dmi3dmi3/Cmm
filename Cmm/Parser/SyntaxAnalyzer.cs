using Parser.Enums;
using Parser.Exceptions;
using Parser.Models;
using Parser.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace Parser.Syntax
{
    public static class SyntaxAnalyzer
    {
        public static VarTable VarTable { get; set; }
        public static FuncTable FuncTable { get; set; }
        public static List<INode> Commands { get; set; }

        static SyntaxAnalyzer()
        {
            VarTable = new VarTable();
            FuncTable = new FuncTable();
        }

        public static void DoSyntaxParse(List<TokenModel> tokens)
        {
            var lc = new ListController<TokenModel>(tokens);
            Lang(lc);
        }

        public static void Lang(ListController<TokenModel> lc)
        {
            while (true)
            {
                if (!lc.TryGetNext(out var next))
                    break;
                lc.SetBack();
                Command(lc);
                CheckEqual(TryGetNext(lc, "; expected"), ";", "; expected");
            }
        }
        //TODO TESTS
        private static void Command(ListController<TokenModel> lc)
        {
            var beg = lc.GetNext();
            if (TypesTools.IsType(beg.Text))
            {
                lc.SetBack();
                Initialization(lc);
            }
            else if (VarTable.Contains(beg.Text))
            {
                lc.SetBack();
                var node = VariableAssign(lc);
                Commands.Add(node);
            }
            else if (FuncTable.Contains(beg.Text))
            {
                lc.SetBack();
                var node = FuncExecute(lc);
                Commands.Add(node);
            }
            else
            {
                throw new ParseException($"Cannot resolve '{beg.Text}'", beg);
            }

            //todo lately var = Arithmetic Expression
            //todo more lately type func
        }

        //TODO TESTS
        private static void Initialization(ListController<TokenModel> lc)
        {
            var type = lc.GetNext();
            var name = TryGetNext(lc, "Variable name expected");
            if (!Constants.Alphabet.Contains(name.Text[0]))
            {
                throw new ParseException("Unexpected token", name);
            }

            if (VarTable.Contains(name.Text) || FuncTable.Contains(name.Text))
            {
                throw new ParseException("Member with the same name is already declared", name);
            }

            VarTable.Add(name.Text, type.Text);
        }

        //TODO TESTS
        private static AssingNode VariableAssign(ListController<TokenModel> lc)
        {
            var variableToken = lc.GetNext();
            CheckEqual(TryGetNext(lc, "= expected"), "=", "= expected");
            var func = TryGetNext(lc, "Expression expected");
            var res = new AssingNode();
            var variable = VarTable.Get(variableToken.Text);
            res.Left = variable;
            if (FuncTable.Contains(func.Text))
            {
                lc.SetBack();
                var fn = FuncExecute(lc);
                res.Right = fn;
            }
            else
            {
                throw new ParseException("Function expected", func);
            }

            return res;
        }

        //TODO TESTS
        private static FunctionNode FuncExecute(ListController<TokenModel> lc)
        {
            var name = lc.GetNext();
            CheckEqual(TryGetNext(lc, "( expected"), "(", "( expected");
            var funcs = (List<FuncModel>)FuncTable.Funcs.Where(model => model.Name == name.Text);
            var args = new List<VarModel>();

            var flag = true;
            var checkNext = TryGetNext(lc, ") or variable expected");

            if (checkNext.Text == ")")
            {
                flag = false;
                lc.SetBack();
            }

            while (flag)
            {
                var next = TryGetNext(lc, ") or variable expected");

                if (VarTable.Contains(next.Text))
                {
                    args.Add(VarTable.Get(next.Text));
                    var afterVar = TryGetNext(lc, ") or , expected");
                    if (afterVar.Text == ",")
                    {
                        continue;
                    }

                    if (next.Text == ")")
                    {
                        lc.SetBack();
                        break;
                    }
                }

                throw new ParseException($"Unexpected token {next.Text}", next);
            }

            var closingNest = lc.GetNext();
            var suitableFunc = funcs.FirstOrDefault(model => model.ArgsType.Count == args.Count);
            if (suitableFunc == null)
            {
                throw new ParseException($"Cannot invoke function {name.Text} with {args.Count} arguments", name);
            }

            return new FunctionNode { FuncName = suitableFunc, Args = args };
        }

        public static TokenModel TryGetNext(ListController<TokenModel> lc, string errorMessage)
        {
            if (lc.TryGetNext(out var ret))
            {
                return ret;
            }
            throw new ParseException(errorMessage, lc.GetPrevious(), true);
        }

        public static void CheckEqual(TokenModel tm, string compareTo, string errorMessage)
        {
            if (tm.Text != compareTo)
            {
                throw new ParseException(errorMessage, tm);
            }
        }
    }
}