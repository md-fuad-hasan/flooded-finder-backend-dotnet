using flooded_finder_backend.Models;

namespace flooded_finder_backend.Dto
{
    public class UpazilaDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string DistrictName { get; set; }
        public string DivisionName { get; set; }

        public int DistrictId { get; set; }
        public int DivisionId { get; set; }

    }
}
