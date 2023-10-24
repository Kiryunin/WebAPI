using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SchoolRepository: RepositoryBase<School>, ISchoolRepository
    {
        public SchoolRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {
        }
        public async Task<IEnumerable<School>> GetAllSchoolsAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(s => s.NameAndNumber).ToListAsync();
        public async Task<School> GetSchoolAsync(Guid schoolId, bool trackChanges) => await FindByCondition(s => s.Id.Equals(schoolId), trackChanges).SingleOrDefaultAsync();
        public void CreateSchool(School school) => Create(school);
        public async Task<IEnumerable<School>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        public void DeleteSchool(School school) => Delete(school);

    }
}
