using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTOs
{
    public class IXSLTTransformerOutput
    {
        public List<ItemInfo> ItemsList {get; }
        public List<GroupInfo> Groups { get; }

        public IXSLTTransformerOutput(List<ItemInfo> itemsList, List<GroupInfo> groups)
        {
            ItemsList = itemsList;
            Groups = groups;
        }
    }
}
