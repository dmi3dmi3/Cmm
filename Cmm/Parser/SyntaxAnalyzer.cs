using System;
using System.Collections.Generic;
using Tools;

namespace Parser
{
    public static class SyntaxAnalyzer
    {
        public static List<string> DoSynAn(List<string> inputList)
        {
            var resList = new List<string>();
            var lc = new ListController<string>(inputList);

            void CheckCommandEnd()
            {
                if (lc.TryGetNext(out var s))
                {
                    if (s == ";")
                    {
                        return;
                    }
                }
                throw new Exception("';' awaited");
            }


            while (lc.TryGetNext(out var s))
            {
                if (s == "print")
                {
                    resList.Add("print");
                    CheckCommandEnd();
                }
                else if (s == "read")
                {
                    resList.Add("read");
                    CheckCommandEnd();
                }
                else
                {
                    throw new Exception($"Unknown command '{s}'");
                }
            }
            return resList;
        }
    }
}