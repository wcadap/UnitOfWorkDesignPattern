using Unit_Of_Work.DataModel;

namespace Unit_Of_Work.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<Payroll> EmployeePayroll { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Employee> Employees { get; }
        void ComputePayroll();
        void Commit();
    }
}
