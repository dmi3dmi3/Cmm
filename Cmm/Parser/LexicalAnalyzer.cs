using Parser.Models;
using System.Collections.Generic;
using Parser.Exceptions;
using Tools;

namespace Parser
{
    public static class LexicalAnalyzer
    {
        public static List<TokenModel> DoLexAn(string inputString)
        {
            var stringController = new StringController(inputString);
            var resList = new List<TokenModel>();
            var tmp = new TokenModel("", -1);

            void AddTmpStr()
            {
                if (tmp.Index == -1) return;
                resList.Add(tmp);
                tmp = new TokenModel("", -1);
            }

            while (stringController.TryGetNext(out var cc))
            {
                if (cc == ' ' || cc == '\r' || cc == '\n')
                {
                    AddTmpStr();
                    continue;
                }

                if (Constants.Symbols.Contains(cc))
                {
                    if (tmp.Index == -1)
                        tmp.Index = stringController.GetNextIndex() - 1;
                    tmp.Text += cc;
                    continue;
                }

                if (Constants.SpecialSymbols.Contains(cc) || Constants.ArifmeticalOperators.Contains(cc))
                {
                    AddTmpStr();
                    resList.Add(new TokenModel(cc.ToString(), stringController.GetNextIndex() - 1));
                    continue;
                }

                throw new ParseException($"Unknown symbol - '{cc}'", new TokenModel(cc.ToString(), stringController.GetNextIndex() - 1));
            }
            AddTmpStr();
            return resList;
        }
    }
}
