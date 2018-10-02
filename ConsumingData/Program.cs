using System;
using System.Collections.Generic;
using System.Xml;

namespace ConsumingData
{
    class Program
    {
        static void Main(string[] args)
        {
            Book l = new Book();
            Book k = new Book();
            Book t = new Book("The Little Prince", "Antoine de Saint-Exupéry", 104,2003);
            l.ReadFromXml("D:\\ConsumingData\\1.xml");
            Console.WriteLine(l.Author);     
            t.SaveToXml("D:\\ConsumingData\\2.xml");
            t.SaveToJSON("D:\\ConsumingData\\2.json");
            k = k.LoadFromJSON("D:\\ConsumingData\\2.json");
            Console.WriteLine(k.Author);
            Console.ReadLine();
        }
    }
}
