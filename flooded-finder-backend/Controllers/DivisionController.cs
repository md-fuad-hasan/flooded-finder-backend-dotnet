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
    public class DivisionController : ControllerBase
    {
        private readonly IDivisionRepository _divisionRepository;
        private readonly IMapper _mapper;

        public DivisionController( IDivisionRepository divisionRepository , IMapper mapper)
        {
            _divisionRepository = divisionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetDivisions()
        {
            var divisions = _mapper.Map<List<DivisionDto>>(_divisionRepository.GetDivisions());

            return Ok(divisions);
        }

        [HttpPost]
        public ActionResult PostDivisions(DivisionDto divisionDto)
        {
            if (divisionDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_divisionRepository.DivisionExists(divisionDto.Name))
            {
                ModelState.AddModelError("", "Division already exists");
                return BadRequest(ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var division = _mapper.Map<Division>(divisionDto);

            if (_divisionRepository.CreateDivision(division))
            {
                return Ok("Division Created.");
            }

            return BadRequest(ModelState);

        }
    }
}
