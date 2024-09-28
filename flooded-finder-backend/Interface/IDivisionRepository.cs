using flooded_finder_backend.Models;

namespace flooded_finder_backend.Interface
{
    public interface IDivisionRepository
    {
        ICollection<Division> GetDivisions();
        Division GetDivision(int id);
        bool DivisionExists(string Name);
        bool CreateDivision(Division division);
        bool DeleteDivision(Division division);
        bool Save();
    }
}
