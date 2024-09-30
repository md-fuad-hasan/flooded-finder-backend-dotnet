using flooded_finder_backend.Models;

namespace flooded_finder_backend.Interface
{
    public interface IAreaRepository
    {
        ICollection<Area> GetAreas();
        Area GetArea(int id);
        bool AreaExists(int id);
        bool CreateArea(Area area);
        bool DeleteArea(Area area);
        bool Save();
    }
}
