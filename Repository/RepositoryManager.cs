using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager: IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private ISchoolRepository _schoolRepository;
        private IClassroomRepository _classroomRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            { 
            if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }
        public ISchoolRepository School
        {
            get
            {
                if(_schoolRepository == null)
                    _schoolRepository= new SchoolRepository(_repositoryContext); 
                return _schoolRepository;
            }
        }
        public IClassroomRepository Classroom
        {
            get
            {
                if(_classroomRepository == null)
                    _classroomRepository= new ClassroomRepository(_repositoryContext);
                return _classroomRepository;
            }
        }
        public void Save() => _repositoryContext.SaveChanges();
    }
}
