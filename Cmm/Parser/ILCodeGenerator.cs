using System;
using System.Collections.Generic;
using Tools;

namespace Parser
{
    public static class ILCodeGenerator
    {
        public static string GenerateIL(List<string> inputList)
        {
            var lc = new ListController<string>(inputList);
            var resStr = "";

            var isReadWas = false;

            while (lc.TryGetNext(out var s))
            {
                if (s == "read")
                {
                    resStr += "call  string [mscorlib]System.Console::ReadLine()\r\n";
                    isReadWas = true;
                    if (lc.ShowNext() == "read")
                    {
                        resStr += "pop\r\n";
                    }
                }
                else if (s == "print")
                {
                    if (lc.ShowPrevious() != "read")
                    {
                        throw new Exception("Nothing to read");
                    }
                    resStr += "call  void [mscorlib]System.Console::WriteLine(string)\r\n";
                }
            }
            return resStr;
        }
    }
}