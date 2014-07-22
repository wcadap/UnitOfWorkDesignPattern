using Unit_Of_Work.DataModel;
using Unit_Of_Work.Interface;

namespace Unit_Of_Work.EFPayroll
{
    public class SQLUnitOfWork : IUnitOfWork
    {
        internal EmployeeContext Context;
        internal SQLRepository<Department> _Departments;
        internal SQLRepository<Employee> _Employees;
        internal SQLRepository<Payroll> _Payroll;

        public SQLUnitOfWork(EmployeeContext _Context)
        {
            Context = _Context;
        }
        public IGenericRepository<Payroll> EmployeePayroll
        {
            get
            {
                if (this._Payroll == null)
                {
                    this._Payroll = new SQLRepository<Payroll>(Context);
                }
                return this._Payroll;
            }
        }

        public IGenericRepository<Department> Departments
        {
            get {
                if (this._Departments == null)
                {
                    this._Departments = new SQLRepository<Department>(Context);
                }
                return this._Departments;
            }
        }

        public IGenericRepository<Employee> Employees
        {
            get
            {
                if (this._Employees == null)
                {
                    this._Employees = new SQLRepository<Employee>(Context);
                }
                return this._Employees;
            }
        }

        public void ComputePayroll()
        {
            decimal NetPay = 0;
            decimal Salary = 0;
            decimal StateTax = 0;
            decimal StateInsurance = 0;
            double StateTaxPercent = .30;
            double StateInsurancePercent = .05;

            foreach (var employee in this.Employees.Query())
            {
                Salary = employee.Salary.Value;
                
                StateTax = Salary * (decimal)StateTaxPercent;
                StateInsurance = Salary * (decimal)StateInsurancePercent;
                NetPay = Salary - (StateTax + StateInsurance);

                employee.Payrolls.Add(new Payroll {StateTax = StateTax, StateInsurance = StateInsurance, NetPay = NetPay });
            }
        }

        public void Commit()
        {
            Context.SaveChanges();
        }
    }
}
