//* FILE			: Admin.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : TripBizDAO for the biiling infomation



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;


/// \namespace TMSProject.Classes.Controller
/// \brief The purpose of this namespace is to create a handle billing menu option. 
/// \details This namespace has 13 classes <b>BillingBizDAO</b>, <b>BizCommon</b>, <b>BizDAO</b>, <b>CarrierBizDAO</b>, <b>CityBizDAO</b>, <b>CMPBizDAO</b>
/// <b>ContractBizDAO</b>, <b>CustomerBizDAO</b>,<b>EmployeeBizDAO</b>, <b>InvoiceBizDAO</b>, <b>OrderBizDAO</b>, <b>PlanInfoBizDAO</b>, <b>TripBizDAO</b>
/// \author : <i>Youchul Choi<i>
namespace TMSProject.Classes.Model
{
    /// \class Admin
    /// \brief This class contains the admin info
    /// \author : <i>Yonchul Choi <i>
    public class Admin
    {
        public string adminEmployeeID { get; set; }
        public string adminPassword { get; set; }
        public string employeeType { get; set; }
        public string command { get; set; }
       
        public void Save()
        {
            bool flag = false;

            if (command == "UPDATE")
            {
                flag = new AdminBizDAO().UpdateAdmin(this);
            }
            else if (command == "INSERT")
            {
                flag = new AdminBizDAO().InsertAdmin(this);
            }
        }

        public void Delete()
        {
            new AdminBizDAO().DeleteAdmin(this);
        }

        public Admin GetById(string adminEmployeeID, string adminPassword)
        {
            var admins = new AdminBizDAO().GetAdmins(adminEmployeeID, adminPassword);
            return admins[0];
        }

        public List<Admin> GetAdmins(string adminEmployeeID, string adminPassword)
        {
            var adminList = new AdminBizDAO().GetAdmins(adminEmployeeID, adminPassword);
            return adminList;
        }

        /// \brief This method Admin 
        /// \details <b>Details</b>
        /// This method will get admin ID
        /// \return  void
        public Admin() { }
    }
}
