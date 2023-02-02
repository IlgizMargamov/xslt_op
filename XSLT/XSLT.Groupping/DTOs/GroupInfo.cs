namespace XSLT.DTOs
{
    public class GroupInfo
    {
        public string GroupName { get; set; }
        public int ItemsCount { get; set; }

        public GroupInfo(string name, int itemsCount)
        {
            GroupName = name;
            ItemsCount = itemsCount;
        }
    }
}
