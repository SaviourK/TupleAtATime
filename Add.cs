using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TupleAtATime
{
    class Add : BasicOperator
    {
        private readonly BasicOperator _input;
        private readonly BasicOperator _filter;

        public Add(BasicOperator input, BasicOperator filter)
        {
            this._input = input;
            this._filter = filter;
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

            }

            while (_input.MoveNext())
            {
                XmlNode xn = _input.Current;
                // searching for element that satisfy the filter condition
                _filter.SetContext(xn);
                if (_filter.MoveNext())
                {
                    // if the filter condition pass for the current element
                    Current = xn;
                    _filter.Reset();
                    return true;
                }
            }

            IsOpen = false;
            return false;
        }

        public override void Reset()
        {
            _input.Reset();
            _filter.Reset();
            IsOpen = false;
        }


        public override string ToString()
        {
            return _input.ToString() + "[" + _filter.ToString() + "]";
        }
    }
}
