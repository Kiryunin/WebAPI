using Entities.Models;

namespace Contracts
{
    public interface IClassroomRepository
    {
        Task<IEnumerable<Classroom>> GetClassroomsAsync(Guid schoolId, bool trackChanges);
        Task<Classroom> GetClassroomAsync(Guid schoolId, Guid id, bool trackChanges);
        void CreateClassroomForSchool(Guid schoolId, Classroom classroom);
        public void DeleteClassroom(Classroom classroom);

    }
}
