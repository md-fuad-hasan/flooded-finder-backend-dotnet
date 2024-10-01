using flooded_finder_backend.Data;
using flooded_finder_backend.Dto;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public ICollection<DistrictDetailDto> GetDistrictByDivision(int divisionId)
        {
            return _context.Districts.Where(d => d.DivisionId == divisionId).Select(d => new DistrictDetailDto
            {
                Id = d.Id,
                Name = d.Name,
                DivisionId = d.DivisionId,
                DivisionName = d.Division.Name

            }).ToList();
        }

        public ICollection<DistrictDetailDto> GetDistricts()
        {
            return _context.Districts.Select(d => new DistrictDetailDto
            {
                Id = d.Id,
                Name = d.Name,
                DivisionId = d.DivisionId,
                DivisionName = d.Division.Name

            }).ToList();
        }

      

        

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

       
    }
}
