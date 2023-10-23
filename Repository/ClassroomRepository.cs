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
    }
}
