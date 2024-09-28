using flooded_finder_backend.Data;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace flooded_finder_backend.Repository
{
    public class DivisionRepository : IDivisionRepository
    {
        private readonly DataContext _context;

        public DivisionRepository( DataContext context)
        {
            _context = context;
        }

        public bool CreateDivision(Division division)
        {
            _context.Divisions.Add(division);

            return Save();
        }

        public bool DeleteDivision(Division division)
        {
            
            _context.Divisions.Remove(division);
            return Save();
        }

        public bool DivisionExists(string Name)
        {
            return _context.Divisions.Any(x => x.Name == Name);
            
        }

        public Division GetDivision(int id)
        {
            return _context.Divisions.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Division> GetDivisions()
        {
            return _context.Divisions.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
