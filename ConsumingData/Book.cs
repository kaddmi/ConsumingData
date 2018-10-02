using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace ConsumingData
{
    [Serializable]
    class Book
    {
        public string Title { set; get; }
        public string Author { set; get; }
        public int NumberOfPages { set; get; }
        public int PublishingYear { set; get; }

        public Book()
        {
        }

        public Book(string title, string author, int numberOfPages, int publishingYear)
        {
            Title = title;
            Author = author;
            NumberOfPages = numberOfPages;
            PublishingYear = publishingYear;
        }

        public void ReadFromXml(string path)
        {
            using (XmlReader xmlReader = XmlReader.Create(path, new XmlReaderSettings() { IgnoreWhitespace = true }))
            {
                  xmlReader.MoveToContent();
                  xmlReader.ReadStartElement("books");
                  Title = xmlReader.GetAttribute("title");
                  Author = xmlReader.GetAttribute("author");
                  xmlReader.ReadStartElement("book");
                  xmlReader.ReadStartElement("information");
                  NumberOfPages = xmlReader.ReadElementContentAsInt();
                  PublishingYear = xmlReader.ReadElementContentAsInt();
            }
     /*     XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode bn = doc.DocumentElement;
            XmlNodeList book = bn.ChildNodes;
            foreach (XmlNode b in book)
            {
                XmlElement bookElement = (XmlElement)b;
                XmlAttribute attr = bookElement.GetAttributeNode("title");
                Title = attr.InnerText;
                attr = bookElement.GetAttributeNode("author");
                Author = attr.InnerXml;
                XmlNodeList inf = b.ChildNodes;
                foreach (XmlNode infEl in inf)
                {
                    XmlElement infElement = (XmlElement)infEl;
                    NumberOfPages = Convert.ToInt16(infElement["numberOfPages"].InnerText);
                    PublishingYear = Convert.ToInt16(infElement["publishingYear"].InnerText);
                }*/         
        }

        public void SaveToXml(string path)
        {
            using (XmlWriter writer = XmlWriter.Create(path, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("books");
                writer.WriteStartElement("book");
                writer.WriteAttributeString("title", Title);
                writer.WriteAttributeString("author", Author);
                writer.WriteStartElement("information");
                writer.WriteElementString("numberOfPages", NumberOfPages.ToString());
                writer.WriteElementString("publishingYear", PublishingYear.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        public void SaveToJSON(string path)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Newtonsoft.Json.Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("Title");
                writer.WriteValue(Title);
                writer.WritePropertyName("Author");
                writer.WriteValue(Author);
                writer.WritePropertyName("NumberOfPages");
                writer.WriteValue(NumberOfPages);
                writer.WritePropertyName("PublishingYear");
                writer.WriteValue(PublishingYear);
                writer.WriteEndObject();
            }
            File.WriteAllText(path, sb.ToString());
        }

        public Book LoadFromJSON(string path)
        {
            string js = File.ReadAllText(path);
            Book book = JsonConvert.DeserializeObject<Book>(js);
            return book;
        }
    }
}
