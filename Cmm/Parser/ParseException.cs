using System;
using Parser.Models;

namespace Parser
{
    public class ParseException : Exception
    {
        public TokenModel Token { get; set; }
        public bool IsExcepAfter { get; set; }

        public ParseException(string message, TokenModel token, bool isExcepAfter = false) : base(message)
        {
            Token = token;
            IsExcepAfter = isExcepAfter;
        }
    }
}