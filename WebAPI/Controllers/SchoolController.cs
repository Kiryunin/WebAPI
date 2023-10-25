using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionFilters;
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
        public async Task<IActionResult> GetSchools() 
        {
            var schools = await _repository.School.GetAllSchoolsAsync(trackChanges: false);
            var schoolsDto = _mapper.Map<IEnumerable<SchoolDto>>(schools);
            return Ok(schoolsDto);
        }

        [HttpGet("{id}", Name = "SchoolById")]
        public async Task<IActionResult> GetSchool(Guid id)
        {
            var school = await _repository.School.GetSchoolAsync(id, trackChanges: false);
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateSchool([FromBody] SchoolForCreationDto school)
        {
            var schoolEntity = _mapper.Map<School>(school);
            _repository.School.CreateSchool(schoolEntity);
            await _repository.SaveAsync();
            var schoolToReturn = _mapper.Map<SchoolDto>(schoolEntity);
            return CreatedAtRoute("SchoolById", new { id = schoolToReturn.Id }, schoolToReturn);
        }

        [HttpGet("collection/({ids})", Name = "SchoolCollection")]
        public async Task<IActionResult> GetSchoolCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var schoolEntities = await _repository.School.GetByIdsAsync(ids, trackChanges: false);
            if (ids.Count() != schoolEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var schoolsToReturn = _mapper.Map<IEnumerable<SchoolDto>>(schoolEntities);
            return Ok(schoolsToReturn);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateSchoolCollection([FromBody] IEnumerable<SchoolForCreationDto> schoolCollection)
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
            await _repository.SaveAsync();
            var schoolCollectionToReturn = _mapper.Map<IEnumerable<SchoolDto>>(schoolEntities);
            var ids = string.Join(",", schoolCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("SchoolCollection", new { ids }, schoolCollectionToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateSchoolExistsAttribute))]
        public async Task<IActionResult> DeleteSchool (Guid id)
        {
            var school = HttpContext.Items["school"] as School;
            _repository.School.DeleteSchool(school);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateSchoolExistsAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] SchoolForUpdateDto school)
        {           
            var schoolEntity = HttpContext.Items["school"] as School;
            _mapper.Map(school, schoolEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetSchoolsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
