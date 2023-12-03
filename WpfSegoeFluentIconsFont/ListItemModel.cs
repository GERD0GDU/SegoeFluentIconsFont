namespace WpfSegoeFluentIconsFont
{
    public class ListItemModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public string UnicodeValue
        {
            get
            {
                return !(Value is null) && (Value.Length > 0)
                    ? $"{(int)Value[0]:x4}"
                    : "";
            }
        }
    }
}
