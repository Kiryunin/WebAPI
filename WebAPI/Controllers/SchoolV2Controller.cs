using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/schools")]
    [ApiController]
    public class SchoolV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public SchoolV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetSchools()
        {
            var companies = await _repository.School.GetAllSchoolsAsync(trackChanges: false);
            return Ok(companies);
        }

    }
}
