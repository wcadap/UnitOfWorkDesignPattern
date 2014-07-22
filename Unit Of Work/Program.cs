using System;
using Unit_Of_Work.DataModel;
using Unit_Of_Work.EFPayroll;
using Unit_Of_Work.Interface;
using System.Collections.Generic;

namespace Unit_Of_Work
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfWork UnitOfWork = new SQLUnitOfWork(new EmployeeContext());

            IList<Employee> _Employees = new List<Employee>();

            _Employees.Add(new Employee { EmployeeName = "Jim Lee", Salary = 6000 });
            _Employees.Add(new Employee { EmployeeName = "Jannet Smith", Salary = 7000 });
            _Employees.Add(new Employee { EmployeeName = "Leonor Jordan", Salary = 8000 });

            UnitOfWork.Departments.Add(new Department
            {
                DepartmentName = "R&D",
                Employees = _Employees
            });
            UnitOfWork.Commit();

            UnitOfWork.ComputePayroll();
            UnitOfWork.Commit();

            //Let Display the computed salaries to test if it works. 
            //You can refactor this and have it as method of IUnitWork.
            //This is for demo purpose only.

            var _Department = "";
            var _EmployeeName = "";
            decimal _Salary = 0;
            decimal _StateTax = 0;
            decimal _StateInsurance = 0;
            decimal _NetPay = 0;

            Console.WriteLine("=======================================================================");
            foreach (var department in UnitOfWork.Departments.Query())
            {
                
                _Department = department.DepartmentName;
                foreach (var employee in department.Employees)
                {
                    _EmployeeName = employee.EmployeeName;
                    _Salary = (decimal)employee.Salary;

                    
                    foreach (var payroll in employee.Payrolls)
                    {
                        _StateTax = (decimal)payroll.StateTax;
                        _StateInsurance = (decimal)payroll.StateInsurance;
                        _NetPay = (decimal)payroll.NetPay;
                        Console.WriteLine(String.Format("{0} | {1} | {2:C2} | {3:C2} | {4 :C2} | {5:C2}", _Department, _EmployeeName, _Salary, _StateTax, _StateInsurance, _NetPay));
                        
                    }
                }
            }
            Console.WriteLine("=======================================================================");
        }
    }
}
