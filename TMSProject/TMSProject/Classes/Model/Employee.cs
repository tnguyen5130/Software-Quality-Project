using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    public class Employee
    {
        public string employeeID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string employeeType { get; set; }
        
        public Employee() { }

        public void Save()
        {
            if (employeeID != "")
            {
                new EmployeeBizDAO().UpdateEmployee(this);
            }
            else
            {
                new EmployeeBizDAO().InsertEmployee(this);
            }
        }

        public void Delete()
        {
            new EmployeeBizDAO().DeleteEmployee(this);
        }

        public Employee GetById(string employeeID)
        {
            var employees = new EmployeeBizDAO().GetEmployees(employeeID);
            return employees[0];
        }

        public List<Employee> GetEmployees(string pattern)
        {
            var employeeList = new EmployeeBizDAO().GetEmployees(pattern);
            return employeeList;
        }
    }
}
