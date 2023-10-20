namespace Models
{
    public class Title
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int UserRating { get; set; }
        public decimal AverageRating { get; set; }
        public List<Character> Characters { get; set; }
        public List<Staff> Staffs { get; set; }

        public bool IsApproved;
    }
}