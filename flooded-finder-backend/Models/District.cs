namespace flooded_finder_backend.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Division Division { get; set; }
        public int DivisionId { get; set; }
        public ICollection<Upazila> Upazilas { get; set; }
        public ICollection<Area> Areas { get; set; }
    }
}
