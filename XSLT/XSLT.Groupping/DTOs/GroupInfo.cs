namespace XSLT.DTOs
{
    public class GroupInfo
    {
        public string Name { get; set; }
        public int ItemsCount { get; set; }

        public GroupInfo(string name, int itemsCount)
        {
            Name = name;
            ItemsCount = itemsCount;
        }
    }
}
