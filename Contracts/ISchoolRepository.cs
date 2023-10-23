using Entities.Models;

namespace Contracts
{
    public interface ISchoolRepository
    {
        public IEnumerable<School> GetAllSchools(bool trackChanges);
        public School GetSchool(Guid schoolId, bool trackChanges);

    }
}
