using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/schools")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public SchoolController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSchools() 
        {
            var schools = _repository.School.GetAllSchools(trackChanges: false);
            var schoolsDto = _mapper.Map<IEnumerable<SchoolDto>>(schools);
            return Ok(schoolsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetSchool(Guid id)
        {
            var school = _repository.School.GetSchool(id, trackChanges: false);
            if (school == null)
            {
                _logger.LogInfo($"School with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var schoolDto = _mapper.Map<SchoolDto>(school);
                return Ok(schoolDto);
            }
        }
    }
}
