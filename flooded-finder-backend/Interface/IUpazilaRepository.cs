using flooded_finder_backend.Dto;
using flooded_finder_backend.Models;

namespace flooded_finder_backend.Interface
{
    public interface IUpazilaRepository
    {
        ICollection<UpazilaDetailDto> GetUpazilas();
        Upazila GetUpazila(int id);
        bool UpazilaExists(string Name);
        bool CreateUpazila(Upazila upazila);
        bool DeleteUpazila(Upazila upazila);
        bool Save();
    }
}
