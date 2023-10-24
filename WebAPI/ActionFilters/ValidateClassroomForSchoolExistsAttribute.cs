using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.ActionFilters
{
    public class ValidateClassroomForSchoolExistsAttribute: IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateClassroomForSchoolExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var schoolId = (Guid)context.ActionArguments["schoolId"];
            var school = await _repository.School.GetSchoolAsync(schoolId, false);
            if (school == null)
            {
                _logger.LogInfo($"School with id: {schoolId} doesn't exist in the database.");
                return;
                context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var classroom = await _repository.Classroom.GetClassroomAsync(schoolId, id, trackChanges);
            if (classroom == null)
            {
                _logger.LogInfo($"Classroom with id: {id} doesn't exist in the database.");

                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("classroom", classroom);
                await next();
            }
        }
    }
}
