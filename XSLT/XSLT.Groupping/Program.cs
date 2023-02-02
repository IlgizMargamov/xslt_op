using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace XSLT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var inputFilePath = args[0];
            var inputDoc = new XmlDocument();
            inputDoc.Load(inputFilePath);

            var list=new List<ListItem>();
            var outputFile = new XmlDocument();
            foreach (XmlNode node in inputDoc.ChildNodes)
            {
                if (node.Name == "list")
                {
                    list.AddRange(from XmlElement item in node.ChildNodes
                                  select new ListItem(item.Attributes["name"].Value, item.Attributes["group"].Value));
                }
            }

            var grouppedList = list.OrderBy(x => x.Group).GroupBy(x=>x.Group);
            
            var outputFilePath = args[1];

            var outputDoc = new XDocument(new XElement("groups"));

            foreach (var group in grouppedList)
            {
                var currentGroup = new XElement("group", new XAttribute("name", group.Key));
                foreach (var item in group)
                {
                    currentGroup.Add(new XElement("item", new XAttribute("name", item.Name)));
                }
                outputDoc.Root.Add(currentGroup);
            }

            outputDoc.Save(outputFilePath);
        }
    }

    public class ListItem
    {
        public string Name { get; set; }
        public string Group { get; set; }

        public ListItem(string name, string group)
        {
            Name = name;
            Group = group;
        }
    }
}