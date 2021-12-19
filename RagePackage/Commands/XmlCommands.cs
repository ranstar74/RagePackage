using System.Xml;

namespace RagePackage.Commands
{
    public class XmlInsertCommand : XmlCommand
    {
        public string Xml { get; set; }
        public string After { get; set; }

        public override void Execute()
        {
            XmlNode node = Document.DocumentElement.SelectSingleNode(XPath);

            if (node.InnerXml.ToLower().Contains(Xml.ToLower()))
                return;

            if (string.IsNullOrEmpty(After))
            {
                node.InnerXml += Xml;
            }
            else
            {
                // Index at the end of 'After' string
                int index = node.InnerXml.IndexOf(After) + After.Length;
                node.InnerXml = node.InnerXml.Insert(index, Xml);
            }
        
            base.Execute();
        }
    }

    public class XmlReplaceCommand : XmlCommand
    {
        public string Xml { get; set; }

        public override void Execute()
        {
            XmlNode node = Document.DocumentElement.SelectSingleNode(XPath);
            node.InnerXml = Xml;

            base.Execute();
        }
    }

    public class XmlDeleteCommand : XmlCommand
    {
        public override void Execute()
        {
            XmlNode node = Document.DocumentElement.SelectSingleNode(XPath);
            node.InnerXml = string.Empty;

            base.Execute();
        }
    }
}
