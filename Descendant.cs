using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TupleAtATime
{
    class Descendant : BasicOperator
    {
        private readonly BasicOperator _input;
        private readonly string _tagName;

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
                IsOpen = true;
            }

            // for each input element
            while (_input.MoveNext())
            {
                XmlNode xn = _input.Current;
                if(MoveNextRecurse(xn))
                {
                    return true;
                }
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
                if(MoveNextRecurse(child.ChildNodes[_childChildElementPosition++]))
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
