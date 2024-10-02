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


        [HttpGet("TeamsVisitToAreas")]
        public async Task<IActionResult> GetTeamsVisitToAreas()
        {
            var groups = await _context.AppUsers
                .Include(ua => ua.UserAreas)
                .ThenInclude(a => a.Area)
                .Select(user => new
                {
                    teamId = user.Id,
                    Name = user.UserName,
                    TeamPhone = user.Phone,
                    TeamEmail = user.Email,
                    VisitedAreaCount = user.UserAreas.Count(),
                    AreasVisited = user.UserAreas.Select(userArea => new
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

        [HttpGet("Team/{teamId}/VisitArea")]
        public async Task<ActionResult> GetTeamVisitToAreaById(int teamId)
        {

            if (!_appUserRepository.AppUserExists(teamId))
            {
                return NotFound(" Group doesn't exists ");
            }

            var team = _context.AppUsers
                .Where(u => u.Id == teamId)
                .Select(au => new
                {
                    TeamId = au.Id,
                    Name = au.UserName,
                    TeamPhone = au.Phone,
                    TeamEmail = au.Email
                });

            var areas = await _context.UserAreas
                .Where(ua => ua.UserId == teamId)
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

            return Ok(new {Team= team, TotalArea = Total, Areas =areas});
        }



    }
}
