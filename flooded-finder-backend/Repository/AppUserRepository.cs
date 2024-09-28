using flooded_finder_backend.Data;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;

namespace flooded_finder_backend.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly DataContext _context;

        public AppUserRepository(DataContext context)
        {
            _context = context;
        }

        public bool AppUserExists(string userName, string email)
        {
            return  _context.AppUsers.Any(u=>u.UserName == userName || u.Email == email);
         
        }


        public bool CreateAppUser(AppUser appUser)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            appUser.Password = BCrypt.Net.BCrypt.HashPassword(appUser.Password, salt);
            _context.AppUsers.Add(appUser);

            return Save();
        }

        public ICollection<AppUser> GetAllAppUser()
        {
            return _context.AppUsers.ToList();
        }

        public AppUser GetAppUser(int id)
        {
            return _context.AppUsers.Where(u => u.Id ==  id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
