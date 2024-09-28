namespace flooded_finder_backend.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public Upazila Upazila { get; set; }
        public District District { get; set; }
        public Division Division { get; set; }

        public int UpazilaId { get; set; }
        public int DistrictId { get; set; }
        public int DivisionId { get; set; }

        public ICollection<UserArea> UserAreas { get; set; }
    }
}
