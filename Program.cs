using System;
using System.Collections.Generic;
using System.Xml;

namespace TupleAtATime
{
    class Program
    {
        static void Main(string[] args)
        {
            //var queryAB = new Child(new Child(new Root("..\\..\\..\\example.xml"), "a"), "b");
            var queryAB = new Descendant(new Child(new Root("..\\..\\..\\example.xml"), "a"), "e");
            //var queryABB = new Child(new Child(new Child(new Root("..\\..\\..\\example.xml"), "a"), "b"), "b");
            //var queryAB_C = new Filter(new Child(new Child(new Root("..\\..\\..\\example.xml"), "a"), "b"), new Child(new Context(), "c"));

            printResult(queryAB);
            //printResult(queryABB);
            //printResult(queryAB_C);
        }

        static void printResult(BasicOperator result)
        {
            Console.WriteLine("Result of " + result.ToString() + " query:");
            foreach (XmlNode node in result)
            {
                Console.WriteLine(node.OuterXml);
            }

            Console.WriteLine();
        }
    }
}
