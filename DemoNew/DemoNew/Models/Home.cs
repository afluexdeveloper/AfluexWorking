using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DemoNew.Models
{
    public class Home
    {
        public string Password { get; set; }
        public string LoginID { get; set; }
        public string Code { get; set; }


        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginID),
                                new SqlParameter("@Password",Password)};
            DataSet ds = Connection.ExecuteQuery("Login", para);
            return ds;
        }

    }
}