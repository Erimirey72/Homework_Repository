namespace Models
{
    public class Staff
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public List<Title> Titles { get; set; }
        public string Description { get; set; }

        public bool IsAproved;
    }
}
