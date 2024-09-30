using AutoMapper;
using flooded_finder_backend.Data;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace flooded_finder_backend.Repository
{
    public class AreaRepository : IAreaRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AreaRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AreaExists(int id)
        {
            return _context.Areas.Any(a => a.Id == id);
        }

        public bool CreateArea(Area area)
        {
            _context.Areas.Add(area);
            return Save();
        }

        public bool DeleteArea(Area area)
        {
            _context.Areas.Remove(area);
            return Save();
        }

        public Area GetArea(int id)
        {
            return _context.Areas.Where(x => x.Id == id).FirstOrDefault();  
        }

        public ICollection<Area> GetAreas()
        {
           return _context.Areas.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
