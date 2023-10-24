using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ModelBinders;

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

        [HttpGet("{id}", Name = "SchoolById")]
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

        [HttpPost]
        public IActionResult CreateSchool([FromBody] SchoolForCreationDto school)
        {
            if (school == null)
            {
                _logger.LogError("SchoolForCreationDto object sent from client is null.");
                return BadRequest("SchoolForCreationDto object is null");
            }
            var schoolEntity = _mapper.Map<School>(school);
            _repository.School.CreateSchool(schoolEntity);
            _repository.Save();
            var schoolToReturn = _mapper.Map<SchoolDto>(schoolEntity);
            return CreatedAtRoute("SchoolById", new { id = schoolToReturn.Id }, schoolToReturn);
        }

        [HttpGet("collection/({ids})", Name = "SchoolCollection")]
        public IActionResult GetSchoolCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var schoolEntities = _repository.School.GetByIds(ids, trackChanges: false);
            if (ids.Count() != schoolEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var schoolsToReturn = _mapper.Map<IEnumerable<SchoolDto>>(schoolEntities);
            return Ok(schoolsToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateSchoolCollection([FromBody] IEnumerable<SchoolForCreationDto> schoolCollection)
        {
            if (schoolCollection == null)
            {
                _logger.LogError("School collection sent from client is null.");
                return BadRequest("School collection is null");
            }
            var schoolEntities = _mapper.Map<IEnumerable<School>>(schoolCollection);
            foreach (var school in schoolEntities)
            {
                _repository.School.CreateSchool(school);
            }
            _repository.Save();
            var schoolCollectionToReturn = _mapper.Map<IEnumerable<SchoolDto>>(schoolEntities);
            var ids = string.Join(",", schoolCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("SchoolCollection", new { ids }, schoolCollectionToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSchool (Guid id)
        {
            var school = _repository.School.GetSchool(id, trackChanges: false);
            if (school == null)
            {
                _logger.LogInfo($"School with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.School.DeleteSchool(school);
            _repository.Save();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompany(Guid id, [FromBody] SchoolForUpdateDto school)
        {
            if (school == null)
            {
                _logger.LogError("SchoolForUpdateDto object sent from client is null.");
                return BadRequest("SchoolForUpdateDto object is null");
            }
            var schoolEntity = _repository.School.GetSchool(id, trackChanges: true);
            if (schoolEntity == null)
            {
                _logger.LogInfo($"School with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(school, schoolEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
