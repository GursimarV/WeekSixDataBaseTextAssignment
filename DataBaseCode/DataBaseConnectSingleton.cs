using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekSixDataBaseTextAssignment.DataBaseCode
{
    //Made a Singleton class to make one instance of this class for the connection
    public sealed class DataBaseConnectSingleton
    {
        private DataBaseConnectSingleton()
        {

        }

        //Help from: https://www.tutorialsteacher.com/csharp/singleton
        private static DataBaseConnectSingleton instance = null;

        public static DataBaseConnectSingleton Instance() 
        {
            if (instance == null)
            {
                instance = new DataBaseConnectSingleton();
            }
            return instance;
        }
        
        //Learned from Week 6 Powerpoint presentation
        public string PrepareDBConnect()
        {
            //This is used to make the connection into string
            SqlConnectionStringBuilder connectBuild = new SqlConnectionStringBuilder();
            connectBuild["server"] = @"(localdb)\MSSQLLocalDB";
            connectBuild["Trusted_Connection"] = true;
            connectBuild["Integrated Security"] = "SSPI";
            connectBuild["Initial Catalog"] = "PROG260FA23";

            return connectBuild.ToString();
        }

    }
}
