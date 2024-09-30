using flooded_finder_backend.Dto;
using flooded_finder_backend.Models;

namespace flooded_finder_backend.Interface
{
    public interface IUserAreaRepository
    {
        bool UserAreaExists(int userId, int areaId);

        bool CreateGroupVisitedArea(UserArea userArea);

        bool DeleteGroupVisitedArea(UserArea userArea);

        bool Save();
        
    }
}
