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
    public class UpazilaController : ControllerBase
    {
        private readonly IUpazilaRepository _upazilaRepository;
        private readonly IMapper _mapper;
        private readonly IDistrictRepository _districtRepository;

        public UpazilaController(IUpazilaRepository upazilaRepository, IMapper mapper, IDistrictRepository districtRepository)
        {
            _upazilaRepository = upazilaRepository;
            _mapper = mapper;
            _districtRepository = districtRepository;
        }

        [HttpGet]
        public IActionResult GetUpazilas()
        {
            var upazilas = _upazilaRepository.GetUpazilas();

            return Ok(upazilas);
        }

        [HttpGet("ByDistrict/{districtId}")]
        public IActionResult GetUpazilasByDistrict(int districtId)
        {
            if(_districtRepository.GetDistrict(districtId) ==null)
            {
                return NotFound("District doesn't exists");
            }

            var upazilas = _upazilaRepository.GetUpazilasByDistrict(districtId);

            return Ok(upazilas);
        }

        [HttpPost]
        public ActionResult PostUpazila(UpazilaDto upazilaDto)
        {
            if (upazilaDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_upazilaRepository.UpazilaExists(upazilaDto.Name))
            {
                ModelState.AddModelError("", "Division already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var upazila = _mapper.Map<Upazila>(upazilaDto);

            if (_upazilaRepository.CreateUpazila(upazila))
            {
                return Ok("Upazila Created.");
            }

            return BadRequest(ModelState);

        }


    }
}
