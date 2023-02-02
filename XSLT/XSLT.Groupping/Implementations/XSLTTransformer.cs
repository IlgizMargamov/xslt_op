using Library;
using Library.DTOs;
using Library.Extensions;
using Library.Interfaces;
using System.Xml;
using System.Xml.Linq;

namespace Library.Implementations
{
    public class XSLTTransformer : IXSLTTransformer
    {
        public IXSLTTransformerOutput TransformFileFromListToGroups(string pathToInputFile, string pathToOutputFile)
        {
            var list = GetListFromInputFile(pathToInputFile);
            var grouppedList = list.OrderBy(x => x.ItemGroup).GroupBy(x => x.ItemGroup);
            var groups = GetGroups(pathToOutputFile, grouppedList);

            return new IXSLTTransformerOutput(list, groups);
        }

        private static List<GroupInfo> GetGroups(string pathToOutputFile, IEnumerable<IGrouping<string, ItemInfo>> grouppedList)
        {
            var outputDoc = new XDocument(new XElement(Resources.Groups_LowCase));

            var groups = new List<GroupInfo>();
            foreach (var group in grouppedList)
            {
                var xmlGroup = new XElement(Resources.Group_LowCase, new XAttribute(Resources.Name_LowCase, group.Key));
                foreach (var itemInfo in group)
                {
                    xmlGroup.Add(new XElement(Resources.Item_LowCase, new XAttribute(Resources.Name_LowCase, itemInfo.ItemName)));
                }
                var itemsCount = group.Count();

                xmlGroup.Add(new XAttribute(Resources.ItemsCount_LowCase, itemsCount));
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
            foreach (XmlNode node in inputDoc.ChildNodes)
            {
                if (node.Name != Resources.List_LowCase)
                {
                    continue;
                }
                
                foreach (XmlElement item in node.ChildNodes)
                {
                    var xmlGroupAttribute = item.Attributes[Resources.Group_LowCase];
                    var xmlNameAttribute = item.Attributes[Resources.Name_LowCase];
                    if (xmlGroupAttribute == null || xmlNameAttribute == null) throw new FormatException();
                    list.Add(new ItemInfo(xmlGroupAttribute.Value, xmlNameAttribute.Value));
                }
            }

            SetItemsCountAttributeToList(inputDoc, list);

            inputDoc.Save(pathToInputFile);
            return list;
        }

        private static void SetItemsCountAttributeToList(XmlDocument inputDoc, List<ItemInfo> list)
        {
            var itemsCountAttribute = inputDoc.CreateAttribute(Resources.ItemsCount_LowCase);
            itemsCountAttribute.Value = list.Count.ToString();
            inputDoc.ChildNodes.GetEnumerator()
                .ToIEnumerable<XmlNode>()
                .FirstOrDefault(x => x.Name == Resources.List_LowCase)?
                .Attributes?.Append(itemsCountAttribute);
        }
    }
}
