using AutoMapper;
using flooded_finder_backend.Dto;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace flooded_finder_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAreaController : ControllerBase
    {
        private readonly IUserAreaRepository _userAreaRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;

        public UserAreaController(IUserAreaRepository userAreaRepository,IAppUserRepository appUserRepository, IAreaRepository areaRepository, IMapper mapper)
        {
            _userAreaRepository = userAreaRepository;
            _appUserRepository = appUserRepository;
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateUserArea(UserAreaDto userAreaDto)
        {
            var appUser = _appUserRepository.AppUserExists(userAreaDto.UserId);
            var area = _areaRepository.AreaExists(userAreaDto.AreaId);

            if (!appUser || !area)
            {
                return BadRequest("Group or Area not exists");
            }

            if (_userAreaRepository.UserAreaExists(userAreaDto.UserId, userAreaDto.AreaId))
            {
                return BadRequest("Group is already visited the area");
            }

            var userArea = _mapper.Map<UserArea>(userAreaDto);

            if (_userAreaRepository.CreateGroupVisitedArea(userArea))
            {
                return Ok("Group will be visited the area");
            }

            return BadRequest(ModelState);


        }
    }
}
