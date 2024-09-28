using flooded_finder_backend.Models;

namespace flooded_finder_backend.Interface
{
    public interface IAppUserRepository
    {
        ICollection<AppUser> GetAllAppUser();
        AppUser GetAppUser(int id);
        bool AppUserExists(string userName, string email);
        bool CreateAppUser(AppUser appUser);
        bool Save();
    }
}
