using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Parser.Models;

namespace Parser.Tests
{
    [TestFixture]
    public class LexicalAnalyzerTests
    {
        [Test]
        public void Test1()
        {
            var input = "string s; s = read(); print(s); \r\n";

            var res = LexicalAnalyzer.DoLexAn(input);
            var normal = new List<TokenModel>()
            {
                new TokenModel("string",0),
                new TokenModel("s",7),
                new TokenModel(";",8),
                new TokenModel("s",10),
                new TokenModel("=",12),
                new TokenModel("read",14),
                new TokenModel("(",18),
                new TokenModel(")",19),
                new TokenModel(";",20),
                new TokenModel("print",22),
                new TokenModel("(",27),
                new TokenModel("s",28),
                new TokenModel(")",29),
                new TokenModel(";",30),
            };

            var b = res.SequenceEqual(normal);
            var b2 = res[0] == normal[0];

            if (res.Count != normal.Count)
            {
                Assert.Fail();
            }
            for (int i = 0; i < res.Count; i++)
            {
                if (res[i] != normal[i])
                {
                    Assert.Fail();
                }
            }
            Assert.Pass();
        }

    }
}