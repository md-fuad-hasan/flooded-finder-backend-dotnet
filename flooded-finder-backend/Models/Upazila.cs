namespace flooded_finder_backend.Models
{
    public class Upazila
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public District District { get; set; }
        public Division Division { get; set; }

        public int DistrictId { get; set; }
        public int DivisionId { get; set; }

        public ICollection<Area> Areas { get; set; }
    }
}
