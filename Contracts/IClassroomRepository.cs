using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IClassroomRepository
    {
        Task<PagedList<Classroom>> GetClassroomsAsync(Guid schoolId, ClassroomParameters classroomParameters, bool trackChanges);
        Task<Classroom> GetClassroomAsync(Guid schoolId, Guid id, bool trackChanges);
        void CreateClassroomForSchool(Guid schoolId, Classroom classroom);
        public void DeleteClassroom(Classroom classroom);

    }
}
