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
    public class AreaController : ControllerBase
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;

        public AreaController(IAreaRepository areaRepository, IMapper mapper)
        {
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetArea()
        {
            var areas = _mapper.Map<List<AreaDto>>(_areaRepository.GetAreas());

            return Ok(areas);
        }

        [HttpPost]
        public ActionResult PostArea(AreaDto areaDto)
        {
            if (areaDto == null)
            {
                return BadRequest(ModelState);
            }
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
