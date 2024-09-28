namespace flooded_finder_backend.Models
{
    public class UserArea
    {
        public int UserId { get; set; }
        public int AreaId { get; set; }

        public AppUser AppUser { get; set; }
        public Area Area { get; set; }
    }
}
