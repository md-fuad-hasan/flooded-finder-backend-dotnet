using flooded_finder_backend.Models;

namespace flooded_finder_backend.Interface
{
    public interface IUpazilaRepository
    {
        ICollection<Upazila> GetUpazilas();
        Upazila GetUpazila(int id);
        bool UpazilaExists(string Name);
        bool CreateUpazila(Upazila upazila);
        bool DeleteUpazila(Upazila upazila);
        bool Save();
    }
}
