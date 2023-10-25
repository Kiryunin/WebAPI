using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System.ComponentModel.Design;

namespace Repository
{
    public class ClassroomRepository: RepositoryBase<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {
        }
        public async Task<PagedList<Classroom>> GetClassroomsAsync(Guid schoolId, ClassroomParameters classroomParameters, bool trackChanges) 
        {
            var classrooms = await FindByCondition(e => e.SchoolId.Equals(schoolId), trackChanges).FilterClassrooms(classroomParameters.MinNuberOfSeats, classroomParameters.MaxNuberOfSeats)
                .SortClassrooms(classroomParameters.OrderBy).SearchClassrooms(classroomParameters.SearchTerm).OrderBy(e => e.Type).ToListAsync();
            return PagedList<Classroom>.ToPagedList(classrooms, classroomParameters.PageNumber, classroomParameters.PageSize);
        }
        public Task<Classroom> GetClassroomAsync(Guid schoolId, Guid id, bool trackChanges) => FindByCondition(c => c.SchoolId.Equals(schoolId) && c.Id.Equals(id),
        trackChanges).SingleOrDefaultAsync();       
        public void CreateClassroomForSchool(Guid schoolId, Classroom classroom)
        {
            classroom.SchoolId = schoolId;
            Create(classroom);
        }
        public void DeleteClassroom(Classroom classroom) => Delete(classroom);

    }
}
