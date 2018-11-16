using NUnit.Framework.Constraints;

namespace Parser.Models
{
    public class TokenModel
    {
        public string Text { get; set; }
        public int Index { get; set; }

        public TokenModel(string text, int index)
        {
            Text = text;
            Index = index;
        }

        public TokenModel(string text)
        {
            Text = text;
            Index = -1;
        }

        public static bool operator ==(TokenModel l, TokenModel r)
        {
            return l.Index == r.Index && l.Text == r.Text;
        }

        public static bool operator !=(TokenModel l, TokenModel r)
        {
            return !(l == r);
        }
    }
}