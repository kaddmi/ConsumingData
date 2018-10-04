using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsumingData
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book1 = new Book("Harry Potter and the Half-Blood Prince", "Joanne Rowling", 600, 2009);
            Book book2 = new Book("The Little Prince", "Antoine de Saint-Exupéry", 104,2003);
            List<Book> books = new List<Book>();
            books.Add(book1);
            books.Add(book2);
            int counter = 1;
            foreach (var book in books)
            {
                book.SaveToXml(@"D:\gk\ConsumingData\book" + (counter++).ToString() + ".xml");
                book.SaveToJSON(@"D:\gk\ConsumingData\book" + (counter++).ToString() + ".json");
            }
            foreach (var file in Directory.EnumerateFiles(@"D:\gk\ConsumingData\").Where(x => x.EndsWith(".json")))
            {
                var book = new Book();
                book.LoadFromJSON(file);
                Console.WriteLine(book.Author);
            }
            foreach (var file in Directory.EnumerateFiles(@"D:\gk\ConsumingData\").Where(x => x.EndsWith(".xml")))
            {
                var book = new Book();
                book.ReadFromXml(file);
                Console.WriteLine(book.Title);
            }
            Console.ReadLine();
        }
    }
}
