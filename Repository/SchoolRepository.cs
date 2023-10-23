using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class SchoolRepository: RepositoryBase<School>, ISchoolRepository
    {
        public SchoolRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {
        }
    }
}
