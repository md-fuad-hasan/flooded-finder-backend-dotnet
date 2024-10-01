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
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;
        private readonly IDivisionRepository _divisionRepository;


        public DistrictController( IDistrictRepository districtRepository, IMapper mapper, IDivisionRepository divisionRepository)
        {
            _districtRepository = districtRepository;
            _mapper = mapper;
            _divisionRepository = divisionRepository;

        }

        [HttpGet]
        public ActionResult GetDistricts()
        {
            var districts =_districtRepository.GetDistricts();

            return Ok(districts);
        }

        [HttpGet("ByDivision/{divisionId}")]

        public ActionResult GetDistrictByDivision(int divisionId)
        {
            if(_divisionRepository.GetDivision(divisionId) == null)
            {
                return NotFound("Division doesn't exosts.");
            }

            var districts = _districtRepository.GetDistrictByDivision(divisionId);

            return Ok(districts);
        }



        [HttpPost]
        public ActionResult PostDivisions(DistrictDto districtDto)
        {
            if (districtDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_districtRepository.DistrictExists(districtDto.Name))
            {
                ModelState.AddModelError("", "District already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var district = _mapper.Map<District>(districtDto);

            if (_districtRepository.CreateDistrict(district))
            {
                return Ok("District Created.");
            }

            return BadRequest(ModelState);

        }
    }
}
