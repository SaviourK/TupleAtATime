using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace TupleAtATime
{


    abstract class BasicOperator: IEnumerator<XmlNode>, IEnumerable<XmlNode>
    {
        protected bool IsOpen { get; set; } = false;

        /// <summary>
        /// XPath representation of the operator.
        /// This enable simple XPath representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
        }


        /// <summary>
        /// Set a context xml node for a current evaluation.
        /// </summary>
        /// <param name="context">Context xml node.
        /// This parameter is null if our context is XML documen itself
        /// </param>
        public abstract void SetContext(XmlNode context);


        /// IEnumerator method
        /// <summary>
        /// Muve cursor to the next position 
        /// </summary>
        /// <returns>True if the end of the cursor was not reached</returns>
        public abstract bool MoveNext();

        // IEnumerator method
        /// <summary>
        /// Reset a cursor of this iterator.
        /// The Current is not reseted, however, during a next MoveNext() call the cursor starts from the begining
        /// </summary>
        public abstract void Reset();

        // IEnumerator property
        /// <summary>
        /// It is a current xml node of the cursor
        /// </summary>
        public XmlNode Current { get; set; }

        // IEnumerator property
        object IEnumerator.Current => Current;

        // IEnumerator property
        public void Dispose()
        {
        }

        // IEnumerable method
        public IEnumerator<XmlNode> GetEnumerator()
        {
            return this;
        }

        // IEnumerable method
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}

