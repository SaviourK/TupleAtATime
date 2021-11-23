using System.Collections.Generic;
using System.Xml;

namespace TupleAtATime
{
    class Descendant : BasicOperator
    {
        private readonly BasicOperator _input;
        private readonly string _tagName;
        private List<int> _childElementPositionList;

        public Descendant(BasicOperator input, string tagName)
        {
            this._input = input;
            this._tagName = tagName;
        }

        public override void SetContext(XmlNode context)
        {
            _input.SetContext(context);
        }

        public override bool MoveNext()
        {
            if (!IsOpen)
            {
                // we open the input operator
                IsOpen = true;
                _childElementPositionList = new List<int> { 0, 0, 0, 0, 0, 0 };

            }

            // for each input element
            while (_input.MoveNext()) 
            {
                XmlNode element = _input.Current;
                int depth = 0;
                int childElementPosition = _childElementPositionList[depth];
                // for each child node
                while (childElementPosition < element.ChildNodes.Count)
                {
                    if (MoveNextRecurse(element.ChildNodes[childElementPosition], depth))
                    {
                        // if yes, set it as a current cursor position and return true
                        _input.Reset();
                        Current = element.ChildNodes[childElementPosition];
                        _childElementPositionList[depth] += 1;
                        return true;
                    }

                    _childElementPositionList[depth] += 1;
                }

                _childElementPositionList[depth] = 0;
            }

            IsOpen = false;
            return false;
        }

        public bool MoveNextRecurse(XmlNode child, int depth)
        {
            depth++;
            int childElementPosition = _childElementPositionList[depth];
            if (child.Name == _tagName)
            {
                // if yes, set it as a current cursor position and return true
                _input.Reset();
                Current = child;
                _childElementPositionList[depth] += 1;
                return true;
            }
            while (childElementPosition < child.ChildNodes.Count)
            {
                if (MoveNextRecurse(child.ChildNodes[childElementPosition++], depth))
                {
                    return true;
                }
            }
            _childElementPositionList[depth] = 0;
            return false;
        }

        public override void Reset()
        {
            _input.Reset();
            IsOpen = false;
        }

        public override string ToString()
        {
            return _input.ToString() + "/" + _tagName;
        }
    }
}
