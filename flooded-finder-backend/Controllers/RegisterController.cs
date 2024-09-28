using AutoMapper;
using flooded_finder_backend.Dto;
using flooded_finder_backend.Interface;
using flooded_finder_backend.Models;
using flooded_finder_backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace flooded_finder_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RegisterController(IAppUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var appUsers = _mapper.Map<List<AppUserDto>>(_userRepository.GetAllAppUser());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            return Ok(appUsers);
        }
        [HttpPost]
        public IActionResult CreateUser(RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest(ModelState);
            }

            var appUser = _userRepository.AppUserExists(registerDto.UserName, registerDto.Email);

            if (appUser)
            {
                ModelState.AddModelError("", "User already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUserMap = _mapper.Map<AppUser>(registerDto);

            if (!_userRepository.CreateAppUser(appUserMap))
            {
                ModelState.AddModelError("", "Something went Worng");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");

        }
    }
}
