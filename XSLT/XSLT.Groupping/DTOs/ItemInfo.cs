namespace XSLT.DTOs
{
    public class ItemInfo
    {
        public string ItemGroup { get; set; }
        public string ItemName { get; set; }

        public ItemInfo(string itemGroup, string itemName)
        {
            ItemGroup = itemGroup;
            ItemName = itemName;
        }
    }
}
