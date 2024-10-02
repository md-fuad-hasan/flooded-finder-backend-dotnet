using flooded_finder_backend.Data;
using flooded_finder_backend.Dto;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace flooded_finder_backend.Repository
{
    public class UpazilaRepository : IUpazilaRepository
    {
        private readonly DataContext _context;

        public UpazilaRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUpazila(Upazila upazila)
        {
            _context.Upazilas.Add(upazila);

            return Save();
        }

        public bool DeleteUpazila(Upazila upazila)
        {
            _context.Upazilas.Remove(upazila);
            return Save();
        }

        public Upazila GetUpazila(int id)
        {
            return _context.Upazilas.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<UpazilaDetailDto> GetUpazilas()
        {
            var upazila = _context.Upazilas.Select(u=> new UpazilaDetailDto
            {
                Id = u.Id,
                Name = u.Name,
                DistrictName = u.District.Name,
                DivisionName = u.Division.Name,
                DistrictId = u.DistrictId,
                DivisionId = u.DivisionId,

            }).ToList();

            return upazila;
        }

        public ICollection<UpazilaDetailDto> GetUpazilasByDistrict(int districtId)
        {
            var upazila = _context.Upazilas.Where(u=>u.DistrictId == districtId).Select(u => new UpazilaDetailDto
            {
                Id = u.Id,
                Name = u.Name,
                DistrictName = u.District.Name,
                DivisionName = u.Division.Name,
                DistrictId = u.DistrictId,
                DivisionId = u.DivisionId,

            }).ToList();

            return upazila;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpazilaExists(string Name)
        {
            return _context.Upazilas.Any(x => x.Name == Name);  
        }
    }
}
