namespace Library.DTOs
{
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