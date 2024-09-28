using flooded_finder_backend.Data;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace flooded_finder_backend.Repository
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly DataContext _context;

        public DistrictRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateDistrict(District district)
        {
            _context.Districts.Add(district);
            return Save();
        }

        public bool DeleteDistrict(District district)
        {
            _context.Districts.Remove(district);
            return Save();
        }

        public bool DistrictExists(string Name)
        {
            return _context.Districts.Any(x => x.Name == Name);
        }

        public District GetDistrict(int id)
        {
            return _context.Districts.Where(x => x.Id == id).FirstOrDefault();


        }

        public ICollection<District> GetDistricts()
        {
            return _context.Districts.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
