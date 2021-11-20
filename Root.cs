using System.Xml;

namespace TupleAtATime
{
    class Root: BasicOperator
    {
        private readonly XmlDocument _root = new XmlDocument();
        public Root(string xmlfile)
        {
            _root.Load(xmlfile);
            Current = _root;
        }

        public override void SetContext(XmlNode context) { }

        public override bool MoveNext()
        {
            if (!IsOpen)
            {
                IsOpen = true;
            }
            else
            {
                IsOpen = false;
            }
            return IsOpen;
        }

        public override void Reset()
        {
            IsOpen = false;
        }

        public override string ToString()
        {
            return "DOC";
        }
    }

}
