using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ClassroomRepository: RepositoryBase<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {
        }

        public IEnumerable<Classroom> GetClassrooms(Guid schoolId, bool trackChanges) => FindByCondition(c => c.SchoolId.Equals(schoolId), trackChanges).OrderBy(c => c.Type);
        public Classroom GetClassroom(Guid schoolId, Guid id, bool trackChanges) => FindByCondition(c => c.SchoolId.Equals(schoolId) && c.Id.Equals(id),
            trackChanges).SingleOrDefault();

        public void CreateClassroomForSchool(Guid schoolId, Classroom classroom)
        {
            classroom.SchoolId = schoolId;
            Create(classroom);
        }
    }
}
