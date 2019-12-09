//* FILE			: Employee.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Project Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : Employee for the login page




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    /// \class Employee
    /// \brief This class contains the Employee Info
    /// \author : <i>Yonchul Choi <i>
    public class Employee
    {
        /// <summary>
        /// A string to get employeeID 
        /// A string to get firstName
        /// A string to get lastName
        /// A string to get employeeType
        /// </summary>
        public string employeeID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string employeeType { get; set; }
        
        public Employee() { }

        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save employee Info
        /// \return  void
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

        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will Delete employee Info
        /// \return  void
        public void Delete()
        {
            new EmployeeBizDAO().DeleteEmployee(this);
        }

        /// \brief This method GetById
        /// \details <b>Details</b>
        /// This method will get element by ID
        /// \return  void
        public Employee GetById(string employeeID)
        {
            var employees = new EmployeeBizDAO().GetEmployees(employeeID);
            return employees[0];
        }

        /// \brief This method GetEmployees
        /// \details <b>Details</b>
        /// This method will get employee info
        /// \return  void
        public List<Employee> GetEmployees(string pattern)
        {
            var employeeList = new EmployeeBizDAO().GetEmployees(pattern);
            return employeeList;
        }
    }
}
