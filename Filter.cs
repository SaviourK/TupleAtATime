using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TupleAtATime
{
    class Filter : BasicOperator
    {
        private readonly BasicOperator _input;
        private readonly BasicOperator _filter;

        private int _elementPosition;

        public Filter(BasicOperator input, BasicOperator filter)
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
                _elementPosition = 0;

            }

            while (_input.MoveNext())
            {
                XmlNode xn = _input.Current;

                while (_elementPosition < xn.ChildNodes.Count)
                {
                    // searching for element that satisfy the filter condition
                    _filter.SetContext(xn);
                    if (_filter.MoveNext())
                    {
                        // if the filter condition pass for the current element
                        Current = xn;
                        _filter.Reset();
                        _elementPosition++;
                        return true;
                    }
                    _elementPosition++;
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
