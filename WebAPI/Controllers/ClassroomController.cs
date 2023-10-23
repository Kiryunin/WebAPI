using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/schools/{schoolId}/classrooms")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ClassroomController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetClassroomsForSchool(Guid schoolId)
        {
            var school = _repository.School.GetSchool(schoolId, trackChanges: false);
            if (school == null)
            {
                _logger.LogInfo($"School with id: {schoolId} doesn't exist in the database.");
                return NotFound();
            }
            var classroomsFromDb = _repository.Classroom.GetClassrooms(schoolId, trackChanges: false);
            var classroomssDto = _mapper.Map<IEnumerable<ClassroomDto>>(classroomsFromDb);
            return Ok(classroomssDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetClassroomForSchool(Guid schoolId, Guid id)
        {
            var school = _repository.School.GetSchool(schoolId, trackChanges: false);
            if (school == null)
            {
                _logger.LogInfo($"School with id: {schoolId} doesn't exist in the database.");
                return NotFound();
            }
            var classroomDb = _repository.Classroom.GetClassroom(schoolId, id, trackChanges: false);
            if (classroomDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var classroom = _mapper.Map<ClassroomDto>(classroomDb);
            return Ok(classroom);
        }
    }
}
