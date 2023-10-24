using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ClassroomRepository: RepositoryBase<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Classroom>> GetClassroomsAsync(Guid schoolId, bool trackChanges) => await FindByCondition(c => c.SchoolId.Equals(schoolId), trackChanges).OrderBy(c => c.Type).ToListAsync();
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
