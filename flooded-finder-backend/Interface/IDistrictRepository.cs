using flooded_finder_backend.Models;

namespace flooded_finder_backend.Interface
{
    public interface IDistrictRepository
    {
        ICollection<District> GetDistricts();
        District GetDistrict(int id);
        bool DistrictExists(string Name);
        bool CreateDistrict(District district);
        bool DeleteDistrict(District district);
        bool Save();
    }
}
