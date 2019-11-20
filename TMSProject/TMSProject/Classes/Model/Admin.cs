using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;

namespace TMSProject.Classes.Model
{
    public class Admin
    {
        
        public Admin() { }

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
    }
}
