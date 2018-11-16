using System.Collections.Generic;

namespace Parser
{
    public static class Translator
    {
        public static string Translate(string inputStr)
        {
            var tokens = LexicalAnalyzer.DoLexAn(inputStr);
            var synTree = SyntaxAnalyzer.DoSynAn(new List<string>());
            var simpleIL = ILCodeGenerator.GenerateIL(synTree);
            var resStr = ILTemplate.Replace("<CODE_HERE>", simpleIL);
            return resStr;
        }

        public static readonly string ILTemplate =
            ".assembly extern mscorlib\r\n" +
            "{\r\n" +
            "  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )\r\n" +
            "  .ver 2:0:0:0\r\n" +
            "}\r\n" +
            ".assembly CmmTest\r\n" +
            "{\r\n" +
            "  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 )\r\n" +
            "  .hash algorithm 0x00008004\r\n" +
            "  .ver 0:0:0:0\r\n" +
            "}\r\n" +
            "\r\n" +
            ".module CmmTest.exe\r\n" +
            ".imagebase 0x00400000\r\n" +
            ".file alignment 0x00000200\r\n" +
            ".stackreserve 0x00100000\r\n" +
            ".subsystem 0x0003\r\n" +
            ".corflags 0x00000001\r\n" +
            ".class public auto ansi beforefieldinit CmmTest extends [mscorlib]System.Object\r\n" +
            "{\r\n" +
            "  .method public hidebysig static void  Main(string[] args) cil managed\r\n" +
            "  {\r\n" +
            "    .entrypoint\r\n" +
            "    .maxstack  8\r\n" +
            "    <CODE_HERE>\r\n" +
            "    ret\r\n" +
            "  }\r\n" +
            "\r\n" +
            "  .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed\r\n" +
            "  {\r\n" +
            "    .maxstack  8\r\n" +
            "    ldarg.0\r\n" +
            "    call    instance void [mscorlib]System.Object::.ctor()\r\n" +
            "    ret\r\n" +
            "  }\r\n" +
            "}\r\n";
    }
}