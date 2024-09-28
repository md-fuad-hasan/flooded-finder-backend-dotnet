using flooded_finder_backend.Models;

namespace flooded_finder_backend.Dto
{
    public class AreaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }

        public int UpazilaId { get; set; }
        public int DistrictId { get; set; }
        public int DivisionId { get; set; }
    }
}
