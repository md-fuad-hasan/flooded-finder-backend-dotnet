using flooded_finder_backend.Data;
using flooded_finder_backend.Dto;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace flooded_finder_backend.Repository
{
    public class UserAreaRepository : IUserAreaRepository
    {
        private readonly DataContext _context;

        public UserAreaRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateGroupVisitedArea(UserArea userArea)
        {
            _context.UserAreas.Add(userArea);

            return Save();
        }

        public bool DeleteGroupVisitedArea(UserArea userArea)
        {
            _context.UserAreas.Remove(userArea);
            return Save();
        }

        public bool UserAreaExists(int userId, int areaId)
        {
            return _context.UserAreas.Any(x=>x.UserId == userId && x.AreaId == areaId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

 
    }
}
 