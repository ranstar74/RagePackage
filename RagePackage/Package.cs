using Newtonsoft.Json;
using RageIO;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace RagePackage
{
    public class Package
    {
        public List<Command> Commands { get; } = new List<Command>();

        public void ExecuteAll()
        {
            foreach (var command in Commands)
            {
                if (command is XmlCommand xmlCommand)
                    xmlCommand.Initialize();

                command.Execute();
            }
        }
    }

    public abstract class Command
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public abstract void Execute();
    }

    public abstract class XmlCommand : Command
    {
        public string XmlPath { get; set; }
        public string XPath { get; set; }

        [JsonIgnore]
        public XmlDocument Document { get; set; }

        private StreamWriter _xmlWriter;

        public void Initialize()
        {
            FileIO xmlFile = new FileIO(XmlPath);

            using (StreamReader xmlReader = new StreamReader(xmlFile.Open()))
            {
                Document = new XmlDocument();
                Document.LoadXml(xmlReader.ReadToEnd());
            }

            _xmlWriter = new StreamWriter(xmlFile.Open(true));
        }

        public override void Execute()
        {
            _xmlWriter.Write(GetFormattedDocument(Document));
            _xmlWriter.Flush();
            _xmlWriter.Dispose();
        }

        private static string GetFormattedDocument(XmlDocument doc)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlTextWriter docWriter = new XmlTextWriter(ms, Encoding.UTF8)
                {
                    Formatting = System.Xml.Formatting.Indented
                };
                doc.WriteContentTo(docWriter);
                docWriter.Flush();
                ms.Flush();

                ms.Position = 0;

                using (StreamReader docReader = new StreamReader(ms))
                {
                    return docReader.ReadToEnd();
                }
            }
        }
    }
}
