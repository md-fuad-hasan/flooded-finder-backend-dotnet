using flooded_finder_backend.Models;

namespace flooded_finder_backend.Dto
{
    public class AreaDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string Upazila { get; set; }
        public string District { get; set; }
        public string Division { get; set; }

       
    }
}
