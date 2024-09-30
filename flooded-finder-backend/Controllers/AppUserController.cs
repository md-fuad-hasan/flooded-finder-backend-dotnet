using flooded_finder_backend.Data;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flooded_finder_backend.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAppUserRepository _appUserRepository;

        public AppUserController(DataContext context, IAppUserRepository appUserRepository)
        {
            _context = context;
            _appUserRepository = appUserRepository;
        }


        [HttpGet("GroupsVisitToAreas")]
        public async Task<IActionResult> GetGroupsVisitToAreas()
        {
            var groups = await _context.AppUsers
                .Include(ua => ua.UserAreas)
                .ThenInclude(a => a.Area)
                .Select(user => new
                {
                    UserId = user.Id,
                    Name = user.UserName,
                    UserPhone = user.Phone,
                    UserEmail = user.Email,
                    VisitedAreaCount = user.UserAreas.Count(),
                    AreaVisited = user.UserAreas.Select(userArea => new
                    {
                        AreaId = userArea.Area.Id,
                        AreaName = userArea.Area.Name,
                        AreaPhone = userArea.Area.Phone,
                        AreaUpazila = userArea.Area.Upazila.Name,
                        AreaDistrict = userArea.Area.District.Name,
                        AreaDivision = userArea.Area.Division.Name,
                    }).ToList()


                })
                .ToListAsync();


            return Ok(groups);

        }

        [HttpGet("Group/{groupId}/VisitArea")]
        public async Task<ActionResult> GetGroupVisitToAreasById(int groupId)
        {

            if (!_appUserRepository.AppUserExists(groupId))
            {
                return NotFound(" Group doesn't exists ");
            }

            var group = _context.AppUsers
                .Where(u => u.Id == groupId)
                .Select(au => new
                {
                    UserId = au.Id,
                    Name = au.UserName,
                    UserPhone = au.Phone,
                    UserEmail = au.Email
                });

            var areas = await _context.UserAreas
                .Where(ua => ua.UserId == groupId)
                .Select(userArea => new
                {
                    AreaId = userArea.Area.Id,
                    AreaName = userArea.Area.Name,
                    AreaPhone = userArea.Area.Phone,
                    AreaUpazila = userArea.Area.Upazila.Name,
                    AreaDistrict = userArea.Area.District.Name,
                    AreaDivision = userArea.Area.Division.Name,
                })
                .ToListAsync();

            var Total = areas.Count();

            return Ok(new {Group= group, TotalArea = Total, Areas =areas});
        }



    }
}
