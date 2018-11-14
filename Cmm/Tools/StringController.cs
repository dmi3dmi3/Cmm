using System;

namespace Tools
{
    public class StringController
    {
        private readonly string _currentString;
        private int _iterator;

        public StringController(string currentString)
        {
            _currentString = currentString;
            _iterator = 0;
        }

        public char GetNext()
        {
            if (_iterator >= _currentString.Length)
                throw new IndexOutOfRangeException("Strting Ended");
            return _currentString[_iterator++];
        }

        public bool TryGetNext(out char result)
        {
            if (_iterator >= _currentString.Length)
            {
                result = default(char); 
                return false;
            }
            result = _currentString[_iterator++];
            return true;
        }

        public bool IsFinish()
        {
            return _iterator >= _currentString.Length;
        }

        public string GetCompleteString()
        {
            return _currentString;
        }

        public bool IsRestContains(string s)
        {
            var t = _currentString.Substring(_iterator);
            return t.Contains(s);
        }

        public int GetNextIndex()
        {
            return _iterator;
        }

    }
}
