using Parser.Models;

namespace Parser.Exceptions
{
    public class ParseException : CmmException
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