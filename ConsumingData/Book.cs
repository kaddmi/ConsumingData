using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


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

        public void LoadFromJSON(string path)
        {
            using (var js = new StreamReader(path))
            {
                var reader = new JsonTextReader(js);
                var jObj = JObject.Load(reader);
                Title = jObj.GetValue("Title").Value<string>();
                Author = jObj.GetValue("Author").Value<string>();
                NumberOfPages = jObj.GetValue("NumberOfPages").Value<int>();
                PublishingYear = jObj.GetValue("PublishingYear").Value<int>();
            }
        }
    }
}
