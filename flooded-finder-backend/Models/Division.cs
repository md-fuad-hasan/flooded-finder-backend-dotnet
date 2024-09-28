namespace flooded_finder_backend.Models
{
    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<District> Districts { get; set; }
        public ICollection<Upazila> Upazilas { get; set; }
        public ICollection<Area> Areas { get; set; }
    }
}
