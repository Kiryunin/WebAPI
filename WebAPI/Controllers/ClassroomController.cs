using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers
{
    [Route("api/schools/{schoolId}/classrooms")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<ClassroomDto> _dataShaper;


        public ClassroomController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<ClassroomDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        /// <summary>
        /// Получает список всех учебных классов для определенной школы
        /// </summary>
        /// <param name="schoolId">Id школы</param>
        /// <param name="classroomParameters">Параметры получения ответа от сервера</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetClassroomsForSchool(Guid schoolId, [FromQuery] ClassroomParameters classroomParameters)
        {
            if (!classroomParameters.ValidAgeRange)
                return BadRequest("Max number of seats can't be less than min number of seats.");

            var school = await _repository.School.GetSchoolAsync(schoolId, trackChanges: false);
            if (school == null)
            {
                _logger.LogInfo($"School with id: {schoolId} doesn't exist in the database.");
                return NotFound();
            }
            var classroomsFromDb = await _repository.Classroom.GetClassroomsAsync(schoolId, classroomParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(classroomsFromDb.MetaData));
            var classroomssDto = _mapper.Map<IEnumerable<ClassroomDto>>(classroomsFromDb);
            return Ok(_dataShaper.ShapeData(classroomssDto, classroomParameters.Fields));
        }

        /// <summary>
        /// Получает учебный класс для определенной школы
        /// </summary>
        /// <param name="schoolId">Id школы</param>
        /// <param name="id">Id класса</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClassroomForSchool")]
        public async Task<IActionResult> GetClassroomForSchoolAsync(Guid schoolId, Guid id)
        {
            var school = await _repository.School.GetSchoolAsync(schoolId, trackChanges: false);
            if (school == null)
            {
                _logger.LogInfo($"School with id: {schoolId} doesn't exist in the database.");
                return NotFound();
            }
            var classroomDb = await _repository.Classroom.GetClassroomAsync(schoolId, id, trackChanges: false);
            if (classroomDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var classroom = _mapper.Map<ClassroomDto>(classroomDb);
            return Ok(classroom);
        }

        /// <summary>
        /// Создает учебный класс для определенной школы
        /// </summary>
        /// <param name="schoolId">Id школы</param>
        /// <param name="classroom">Объект для создания класса</param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateClassroomForSchoolAsync(Guid schoolId, [FromBody] ClassroomForCreationDto classroom)
        {
            if (classroom == null)
            {
                _logger.LogError("ClassroomForCreationDto object sent from client is null.");
                return BadRequest("ClassroomForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var school = await _repository.Company.GetCompanyAsync(schoolId, trackChanges: false);
            if (school == null)
            {
                _logger.LogInfo($"School with id: {schoolId} doesn't exist in the database.");
                return NotFound();
            }
            var classroomEntity = _mapper.Map<Classroom>(classroom);
            _repository.Classroom.CreateClassroomForSchool(schoolId, classroomEntity);
            await _repository.SaveAsync();
            var classroomToReturn = _mapper.Map<ClassroomDto>(classroomEntity);
            return CreatedAtRoute("GetClassroomForSchool", new
            { schoolId, id = classroomToReturn.Id }, classroomToReturn);
        }

        /// <summary>
        /// Удаляет учебный класс для определенной школы
        /// </summary>
        /// <param name="schoolId">Id школы</param>
        /// <param name="id">Id класса</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateClassroomForSchoolExistsAttribute))]

        public async Task<IActionResult> DeleteClassroomForSchool (Guid schoolId, Guid id)
        {
            var classroomForSchool = HttpContext.Items["classroom"] as Classroom;
            _repository.Classroom.DeleteClassroom(classroomForSchool);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Редактирует учебный класс для определенной школы
        /// </summary>
        /// <param name="schoolId">Id школы</param>
        /// <param name="id">Id класса</param>
        /// <param name="classroom">Объект редактированного класса</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateClassroomForSchoolExistsAttribute))]
        public async Task<IActionResult> UpdateClassroomForSchool(Guid schoolId, Guid id, [FromBody] ClassroomForUpdateDto classroom)
        {
            var classroomEntity = HttpContext.Items["classroom"] as Classroom;
            _mapper.Map(classroom, classroomEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Редактирует учебный класс для определенной школы
        /// </summary>
        /// <param name="schoolId">Id школы</param>
        /// <param name="id">Id класса</param>
        /// <param name="patchDoc">Параметры patch запроса</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateClassroomForSchoolExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateClassroomForSchool(Guid schoolId, Guid id, [FromBody] JsonPatchDocument<ClassroomForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var classroomEntity = HttpContext.Items["classroom"] as Classroom;
            var classroomToPatch = _mapper.Map<ClassroomForUpdateDto>(classroomEntity);
            patchDoc.ApplyTo(classroomToPatch, ModelState);
            TryValidateModel(classroomToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(classroomToPatch, classroomEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
