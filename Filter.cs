using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TupleAtATime
{
    class Filter: BasicOperator
    {
        private readonly BasicOperator _input;
        private readonly BasicOperator _filter;

        private List<XmlNode> _elements;
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

                // and read all the input nodes
                _elements = new List<XmlNode>();
                while (_input.MoveNext())
                {
                    _elements.Add(_input.Current);
                }
            }

            while (_elementPosition < _elements.Count)
            {
                // searching for element that satisfy the filter condition
                _filter.SetContext(_elements[_elementPosition]);
                if (_filter.MoveNext())
                {
                    // if the filter condition pass for the current element
                    Current = _elements[_elementPosition];
                    _filter.Reset();
                    _elementPosition++;
                    return true;
                }
                _elementPosition++;
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
