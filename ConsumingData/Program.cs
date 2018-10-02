using System;
using System.Collections.Generic;
using System.Xml;

namespace ConsumingData
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book1 = new Book();
            Book book2 = new Book();
            Book book3 = new Book("The Little Prince", "Antoine de Saint-Exupéry", 104,2003);
            book1.ReadFromXml("D:\\ConsumingData\\1.xml");
            Console.WriteLine(book1.Author);     
            book3.SaveToXml("D:\\ConsumingData\\2.xml");
            book3.SaveToJSON("D:\\ConsumingData\\2.json");
            book2 = book2.LoadFromJSON("D:\\ConsumingData\\2.json");
            Console.WriteLine(book2.Author);
            Console.ReadLine();
        }
    }
}
