using System;
using System.Collections.Generic;

namespace Tools
{
    public class ListController<T> :ICloneable
    {
        private readonly List<T> _currentList;
        private int _iterator;

        public ListController(List<T> data)
        {
            _currentList = data;
            _iterator = 0;
        }

        public bool TryGetNext(out T result)
        {
            if (_iterator >= _currentList.Count)
            {
                result = default(T);
                return false;
            }
            result = _currentList[_iterator++];
            return true;
        }

        public T GetNext()
        {
            return _currentList[_iterator++];
        }

        public void SetBack()
        {
            if (_iterator < 2)
            {
                _iterator = 0;
            }
            else
            {
                _iterator -= 1;
            }
        }

        public T GetPrevious()
        {
            return _iterator < 1 ? default(T) : _currentList[_iterator - 1];
        }

        public T ShowNext()
        {
            return _currentList.Count > _iterator? _currentList[_iterator]: default(T);
        }

        public T ShowPrevious()
        {
            return 1 < _iterator ? _currentList[_iterator - 2] : default(T);
        }

        public int GetLastIndex()
        {
            return _iterator - 1;
        }

        public bool Contains(T toSearch)
        {
            return _currentList.Contains(toSearch);
        }

        public object Clone()
        {
            return new ListController<T>(_currentList) { _iterator = _iterator};
        }
    }
}