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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is PartModel pm)) return false;
            return pm.Index == Index && pm.Text == Text;
        }
    }
}