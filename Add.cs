using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TupleAtATime
{
    class And : BasicOperator
    {
        private readonly BasicOperator _input;
        private readonly BasicOperator _filter1;
        private readonly BasicOperator _filter2;

        private int _elementPosition;

        public And(BasicOperator input, BasicOperator filter1, BasicOperator filter2)
        {
            this._input = input;
            this._filter1 = filter1;
            this._filter2 = filter2;
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
                    _filter1.SetContext(xn);
                    _filter2.SetContext(xn);
                    if (_filter1.MoveNext() && _filter2.MoveNext())
                    {
                        // if the filter condition pass for the current element
                        Current = xn;
                        _filter1.Reset();
                        _filter2.Reset();
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
            _filter1.Reset();
            _filter2.Reset();
            IsOpen = false;
        }


        public override string ToString()
        {
            return _input.ToString() + "[" + _filter1.ToString() + "|" + _filter2 + "]";
        }
    }
}
