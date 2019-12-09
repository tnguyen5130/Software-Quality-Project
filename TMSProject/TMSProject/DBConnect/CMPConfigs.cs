using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSProject.DBConnect
{
    class CMPConfigs
    {
        public static string dbServer = "159.89.117.198";
        public static string dbPort = "3306";
        public static string dbUID = "DevOSHT";
        public static string dbPassword = "Snodgr4ss!";
        public static string dbDatabase = "cmp";


        public CMPConfigs()
        {
        }

        public string DbServer
        {
            get { return dbServer; }

            set { dbServer = value; }
        }

        public string DbPort
        {
            get { return dbPort; }

            set { dbPort = value; }
        }

        public string DbUID
        {
            get { return dbUID; }

            set { dbUID = value; }
        }

        public string DbPassword
        {
            get { return dbPassword; }

            set { dbPassword = value; }
        }

        public string DbDatabase
        {
            get { return dbDatabase; }

            set { dbDatabase = value; }
        }

        public List<String> CallConfiguration()
        {
            List<String> cmpDetails = new List<String>();
            cmpDetails.Add(dbServer);
            cmpDetails.Add(dbPort);
            cmpDetails.Add(dbUID);
            cmpDetails.Add(dbPassword);
            cmpDetails.Add(dbDatabase);
            return cmpDetails;
        }
    }
}
