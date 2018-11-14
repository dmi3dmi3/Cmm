using System;
using System.Collections.Generic;
using Parser.Models;
using Tools;

namespace Parser
{
    public static class LexAn
    {
        public static List<string> DoLexAn(string inputString)
        {
            StringController sc = new StringController(inputString);
            var resList = new List<string>();
            var tmp = "";

            void AddTmp()
            {
                if (tmp != "")
                {
                    resList.Add(tmp);
                    tmp = "";
                }
            }

            while (sc.TryGetNext(out var c))
            {
                if (c == ' ')
                {
                    AddTmp();
                    continue;
                }

                if (Constants.Alphabet.Contains(c))
                {
                    tmp += c;
                    continue;
                }

                if (Constants.SpecialSymbols.Contains(c))
                {
                    AddTmp();
                    resList.Add(c.ToString());
                    continue;
                }

                throw new Exception($"Unknown symbol '{c}'");
            }
            AddTmp();

            return resList;
        }
    }
}
