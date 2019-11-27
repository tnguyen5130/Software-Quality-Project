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
        /// A string to stor admin Id
        public string adminEmployeeID { get; set; }

        /// \brief This method Admin 
        /// \details <b>Details</b>
        /// This method will get admin ID
        /// \return  void
        public Admin() { }
    }
}
