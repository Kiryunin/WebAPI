using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}", Name = "GetClassroomForSchool")]
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

        [HttpPost]
        public IActionResult CreateClassroomForSchool(Guid schoolId, [FromBody] ClassroomForCreationDto classroom)
        {
            if (classroom == null)
            {
                _logger.LogError("ClassroomForCreationDto object sent from client is null.");
                return BadRequest("ClassroomForCreationDto object is null");
            }
            var school = _repository.Company.GetCompany(schoolId, trackChanges: false);
            if (school == null)
            {
                _logger.LogInfo($"School with id: {schoolId} doesn't exist in the database.");
                return NotFound();
            }
            var classroomEntity = _mapper.Map<Classroom>(classroom);
            _repository.Classroom.CreateClassroomForSchool(schoolId, classroomEntity);
            _repository.Save();
            var classroomToReturn = _mapper.Map<ClassroomDto>(classroomEntity);
            return CreatedAtRoute("GetClassroomForSchool", new
            { schoolId, id = classroomToReturn.Id }, classroomToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClassroomForSchool (Guid schoolId, Guid id)
        {
            var school = _repository.Company.GetCompany(schoolId, trackChanges: false);
            if (school == null)
            {
                _logger.LogInfo($"School with id: {schoolId} doesn't exist in the database.");
                return NotFound();
            }
            var classroomForSchool= _repository.Classroom.GetClassroom(schoolId, id, trackChanges: false);
            if (classroomForSchool == null)
            {
                _logger.LogInfo($"Classroom with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Classroom.DeleteClassroom(classroomForSchool);
            _repository.Save();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClassroomForSchool(Guid schoolId, Guid id, [FromBody] ClassroomForUpdateDto classroom)
        {
            if (classroom == null)
            {
                _logger.LogError("ClassroomForUpdateDto object sent from client is null.");
                return BadRequest("ClassroomForUpdateDto object is null");
            }
            var school = _repository.School.GetSchool(schoolId, trackChanges: false);
            if (school == null)
            {
                _logger.LogInfo($"Company with id: {schoolId} doesn't exist in the database.");
                return NotFound();
            }
            var classroomEntity = _repository.Classroom.GetClassroom(schoolId, id, trackChanges: true);
            if (classroomEntity == null)
            {
                _logger.LogInfo($"Classroom with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(classroom, classroomEntity);
            _repository.Save();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateClassroomForSchool(Guid schoolId, Guid id, [FromBody] JsonPatchDocument<ClassroomForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var company = _repository.School.GetSchool(schoolId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"School with id: {schoolId} doesn't exist in the database.");
                return NotFound();
            }
            var classroomEntity = _repository.Classroom.GetClassroom(schoolId, id, trackChanges: true);
            if (classroomEntity == null)
            {
                _logger.LogInfo($"Classroom with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var classroomToPatch = _mapper.Map<ClassroomForUpdateDto>(classroomEntity);
            patchDoc.ApplyTo(classroomToPatch);
            _mapper.Map(classroomToPatch, classroomEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
