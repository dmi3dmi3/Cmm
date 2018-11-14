using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public class Constants
    {
        public static readonly List<char> LatinAlphabetLower = "abcdefghijklmnopqrstuvwxyz".ToList();
        public static readonly List<char> LatinAlphabetUpper = "abcdefghijklmnopqrstuvwxyz".ToUpper().ToList();
        public static readonly List<char> LatinAlphabet;
        public static readonly List<char> CyrillicAlphabetLower = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToList();
        public static readonly List<char> CyrillicAlphabetUpper = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToUpper().ToList();
        public static readonly List<char> CyrillicAlphabet;
        public static readonly List<char> Alphabet;
        public static readonly List<char> Numbers = "0123456789".ToList();
        public static readonly List<char> SpecialSymbols = new List<char>() {';', '='};
        public static readonly List<char> Symbols;
        public static readonly List<char> ArifmeticalOperators = new List<char>(){'+','-','*','/','^','(',')'};

        static Constants()
        {
            
            LatinAlphabet = new List<char>();
            LatinAlphabet.AddRange(LatinAlphabetLower);
            LatinAlphabet.AddRange(LatinAlphabetUpper);

            CyrillicAlphabet = new List<char>();
            CyrillicAlphabet.AddRange(CyrillicAlphabetLower);
            CyrillicAlphabet.AddRange(CyrillicAlphabetUpper);

            Alphabet = new List<char>();
            //Alphabet.AddRange(CyrillicAlphabet);
            Alphabet.AddRange(LatinAlphabet);

            Symbols = new List<char>();
            Symbols.AddRange(Alphabet);
            Symbols.AddRange(Numbers);
        }
    }
}