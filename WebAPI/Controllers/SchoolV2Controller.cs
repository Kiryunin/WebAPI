using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/schools")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
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
