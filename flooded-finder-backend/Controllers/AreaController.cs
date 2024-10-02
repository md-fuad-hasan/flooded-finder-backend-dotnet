using AutoMapper;
using flooded_finder_backend.Data;
using flooded_finder_backend.Dto;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;
using flooded_finder_backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace flooded_finder_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public AreaController(IAreaRepository areaRepository, IMapper mapper, DataContext context)
        {
            _areaRepository = areaRepository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public ActionResult GetArea()
        {
            var areas = _mapper.Map<List<AreaDto>>(_areaRepository.GetAreas());

            return Ok(areas);
        }

        [HttpGet("AreasVisitByTeam")]
        public async Task<IActionResult> GetAreasVisitByGroup()
        {
            var areas = await _context.Areas
                .Include(ua=>ua.UserAreas)
                .ThenInclude(u=>u.AppUser)
                .Select(area => new
                {
                    AreaId = area.Id,
                    AreaName = area.Name,
                    AreaPhone = area.Phone,
                    AreaUpazila = area.Upazila.Name,
                    AreaDistrict = area.District.Name,
                    AreaDivision = area.Division.Name,
                    VisitedTeamCount = area.UserAreas.Count(),
                    VisitedTeam = area.UserAreas.Select(userArea=> new
                    {
                        TeamId = userArea.AppUser.Id,
                        TeamName = userArea.AppUser.UserName,
                        TeamEmail = userArea.AppUser.Email,
                        TeamPhone = userArea.AppUser.Phone,
                    }).ToList()
                                                

                })
                .ToListAsync();

            


            return Ok(areas);

        }


        [HttpGet("{areaId}/VisitByTeam")]
        public async Task<IActionResult> GetAreaVisitByTeam(int areaId)
        {
            if (!_areaRepository.AreaExists(areaId))
            {
                return NotFound("Area doesn't exists");
            }

            var area =  _context.Areas
                .Where(a => a.Id == areaId)
                .Select(ar => new
                {
                    AreaId = ar.Id,
                    AreaName = ar.Name,
                    AreaPhone = ar.Phone,
                    Upazila= ar.Upazila.Name,
                    District = ar.District.Name,
                    Division = ar.Division.Name,
                }).FirstOrDefault();

            var teams = await _context.UserAreas
                .Where(ua => ua.AreaId == areaId)
                .Select(userArea => new
                {
                    TeamId = userArea.AppUser.Id,
                    TeamName = userArea.AppUser.UserName,
                    TeamPhone = userArea.AppUser.Phone,
                    TeamEmail = userArea.AppUser.Email
                })
                .ToListAsync();


            var Total = teams.Count();

            return Ok(new {Area = area, TotalTeam = Total, Teams = teams });

        }

        [HttpPost]
        public ActionResult PostArea(AreaDto areaDto)
        {
            if (areaDto == null)
            {
                return BadRequest("Area Data is null");
            }
            

            if (!ModelState.IsValid)
            {
                return BadRequest("invalid Error");
            }

            var area = _mapper.Map<Area>(areaDto);

            if (_areaRepository.CreateArea(area))
            {
                return Ok("Area Created Successfully.");
            }

            return BadRequest(ModelState);

        }
    }
}
