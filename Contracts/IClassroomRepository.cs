using Entities.Models;

namespace Contracts
{
    public interface IClassroomRepository
    {
        IEnumerable<Classroom> GetClassrooms(Guid schoolId, bool trackChanges);
        Classroom GetClassroom(Guid schoolId, Guid id, bool trackChanges);
        void CreateClassroomForSchool(Guid schoolId, Classroom classroom);
        public void DeleteClassroom(Classroom classroom);

    }
}
