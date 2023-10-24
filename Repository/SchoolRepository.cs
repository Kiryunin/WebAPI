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
        public IEnumerable<School> GetAllSchools(bool trackChanges) => FindAll(trackChanges).OrderBy(s => s.NameAndNumber).ToList();
        public School GetSchool(Guid schoolId, bool trackChanges) => FindByCondition(s => s.Id.Equals(schoolId), trackChanges).SingleOrDefault();
        public void CreateSchool(School school) => Create(school);
        public IEnumerable<School> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
        public void DeleteSchool(School school) => Delete(school);

    }
}
