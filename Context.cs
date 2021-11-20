using System.Xml;

namespace TupleAtATime
{
    class Context: BasicOperator
    {
        public override void SetContext(XmlNode context)
        {
            Current = context;
        }

        public override bool MoveNext()
        {
            if (!IsOpen)
            {
                IsOpen = true;
                return true;
            }
            else
            {
                IsOpen = false;
                return false;
            }
        }

        public override void Reset()
        {
            IsOpen = false;
        }


        public override string ToString()
        {
            return ".";
        }
    }
}
