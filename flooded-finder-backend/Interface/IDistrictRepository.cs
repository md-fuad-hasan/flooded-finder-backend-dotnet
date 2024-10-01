using flooded_finder_backend.Dto;
using flooded_finder_backend.Models;

namespace flooded_finder_backend.Interface
{
    public interface IDistrictRepository
    {
        ICollection<DistrictDetailDto> GetDistricts();
        ICollection<DistrictDetailDto> GetDistrictByDivision(int divisionId);

        District GetDistrict(int id);
        bool DistrictExists(string Name);
        bool CreateDistrict(District district);
        bool DeleteDistrict(District district);
        bool Save();
    }
}
