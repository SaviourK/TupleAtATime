using System.Collections.Generic;
using System.Xml;

namespace TupleAtATime
{
    class Child : BasicOperator
    {
        private readonly BasicOperator _input;
        private readonly string _tagName;
        private int _childElementPosition;

        public Child(BasicOperator input, string tagName)
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
                _childElementPosition = 0;

            }

            // for each input element
            while (_input.MoveNext())
            {
                XmlNode xn = _input.Current;
                // for each child node
                while (_childElementPosition < xn.ChildNodes.Count)
                {
                    // check whether the child node has a correct tag name or not
                    if (xn.ChildNodes[_childElementPosition].Name == _tagName)
                    {
                        // if yes, set it as a current cursor position and return true
                        Current = xn.ChildNodes[_childElementPosition++];
                        return true;
                    }

                    _childElementPosition++;
                }

                _childElementPosition = 0;
            }

            IsOpen = false;
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
