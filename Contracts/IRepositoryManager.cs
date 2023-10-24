namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        ISchoolRepository School { get; }
        IClassroomRepository Classroom { get; }
        Task SaveAsync();
    }
}
