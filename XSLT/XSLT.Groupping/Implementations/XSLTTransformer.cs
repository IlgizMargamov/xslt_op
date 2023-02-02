using System.Xml;
using System.Xml.Linq;
using XSLT.DTOs;
using XSLT.Extensions;
using XSLT.Interfaces;

namespace XSLT.Implementations
{
    public class XSLTTransformer : IXSLTTransformer
    {
        public IXSLTTransformerOutput TransformFile(string pathToInputFile, string pathToOutputFile)
        {
            var list = GetListFromInputFile(pathToInputFile);
            var grouppedList = list.OrderBy(x => x.ItemGroup).GroupBy(x => x.ItemGroup);
            var groups = GetGroups(pathToOutputFile, grouppedList);

            return new IXSLTTransformerOutput(list, groups);
        }

        private static List<GroupInfo> GetGroups(string pathToOutputFile, IEnumerable<IGrouping<string, ItemInfo>> grouppedList)
        {
            var outputDoc = new XDocument(new XElement("groups"));

            var groups = new List<GroupInfo>();
            foreach (var group in grouppedList)
            {
                var xmlGroup = new XElement("group", new XAttribute("name", group.Key));
                foreach (var itemInfo in group)
                {
                    xmlGroup.Add(new XElement("item", new XAttribute("name", itemInfo.ItemName)));
                }
                var itemsCount = group.Count();

                xmlGroup.Add(new XAttribute("itemCount", itemsCount));
                outputDoc.Root.Add(xmlGroup);
                groups.Add(new GroupInfo(group.Key, itemsCount));
            }

            outputDoc.Save(pathToOutputFile);
            return groups;
        }

        private static List<ItemInfo> GetListFromInputFile(string pathToInputFile)
        {
            var inputDoc = new XmlDocument();
            inputDoc.Load(pathToInputFile);

            var list = new List<ItemInfo>();
            foreach (var node in from XmlNode node in inputDoc.ChildNodes
                                 where node.Name == "list"
                                 select node)
            {
                foreach (XmlElement item in node.ChildNodes)
                {
                    var xmlGroupAttribute = item.Attributes["group"];
                    var xmlNameAttribute = item.Attributes["name"];
                    if (xmlGroupAttribute == null || xmlNameAttribute == null) throw new FormatException();
                    list.Add(new ItemInfo(xmlGroupAttribute.Value, xmlNameAttribute.Value));
                }

            }

            SetItemsCountAttribute(inputDoc, list);

            inputDoc.Save(pathToInputFile);
            return list;
        }

        private static void SetItemsCountAttribute(XmlDocument inputDoc, List<ItemInfo> list)
        {
            var itemsCountAttribute = inputDoc.CreateAttribute("itemsCount");
            itemsCountAttribute.Value = list.Count.ToString();
            inputDoc.ChildNodes.GetEnumerator()
                .ToIEnumerable<XmlNode>()
                .FirstOrDefault(x => x.Name == "list")?.Attributes?.Append(itemsCountAttribute);
        }
    }
}
