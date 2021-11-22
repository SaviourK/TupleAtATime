using System.Collections.Generic;
using System.Xml;

namespace TupleAtATime
{
    class Descendant : BasicOperator
    {
        private readonly BasicOperator _input;
        private readonly string _tagName;
        private int _elementPosition;
        private int _childElementPosition;

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
                _elementPosition = 0;
                _childElementPosition = 0;

            }

            // for each input element
            while (_input.MoveNext())
            {
                XmlNode element = _input.Current;
                // for each child node
                while (_childElementPosition < element.ChildNodes.Count)
                {
                    if (MoveNextRecurse(element.ChildNodes[_childElementPosition])) {
                        // if yes, set it as a current cursor position and return true
                        _input.Reset();
                        Current = element.ChildNodes[_childElementPosition++];
                        return true;
                    }

                    _childElementPosition++;
                }

                _elementPosition++;
                _childElementPosition = 0;
            }

            IsOpen = false;
            return false;
        }

        public bool MoveNextRecurse(XmlNode child)
        {
            int _childChildElementPosition = 0;
            if (child.Name == _tagName)
            {
                // if yes, set it as a current cursor position and return true
                Current = child;
                return true;
            }
            while (_childChildElementPosition < child.ChildNodes.Count)
            {
                if (MoveNextRecurse(child.ChildNodes[_childChildElementPosition++]))
                {
                    return true;
                }
            }
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
