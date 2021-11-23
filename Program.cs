using System;
using System.Collections.Generic;
using System.Xml;

namespace TupleAtATime
{
    class Program
    {
        static void Main(string[] args)
        {
            //var queryAB = new Child(new Child(new Root("..\\..\\..\\example.xml"), "a"), "b"); // /a/b
            var queryAB = new Descendant(new Child(new Root("..\\..\\..\\example.xml"), "a"), "b"); // /a//b
            //var queryABB = new Child(new Child(new Child(new Root("..\\..\\..\\example.xml"), "a"), "b"), "b"); // /a/b/b
            //var queryAB_C = new Filter(new Child(new Child(new Root("..\\..\\..\\example.xml"), "a"), "b"), new Child(new Context(), "c")); /a/b[./c][./d]
            //var queryAB_C_D = new Filter(new Filter(new Child(new Child(new Root("..\\..\\..\\example.xml"), "a"), "b"), new Child(new Context(), "c")), new Child(new Context(), "d")); // a/b[./c][./d]
            //var queryAB_CD = new And(new Child(new Child(new Root("..\\..\\..\\example.xml"), "a"), "b"), new Child(new Context(), "c"), new Child(new Context(), "d")); // a/b[./c][./d]

            printResult(queryAB);
            //printResult(queryABB);
            //printResult(queryAB_C);
            //printResult(queryAB_C_D);
            //printResult(queryAB_CD);
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
