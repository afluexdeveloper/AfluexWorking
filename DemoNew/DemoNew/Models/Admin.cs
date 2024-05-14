using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DemoNew.Models
{
    public class Admin
    {

        public string DesignationDetails { get; set; }
        public List<Admin> lstGetEmployee { get; set; }
        public List<Admin> lstGetWorkTrend { get; set; }
        public List<Admin> lstTotalProjectWork { get; set; }
        public string EmployeeName { get; set; }
        public string ProfilePic { get; set; }
        public string Subject { get; set; }
        public string Module { get; set; }
        public string Status { get; set; }
        public string WorkDate { get; set; }
        public string ProjectName { get; set; }
        public string TotalProjectWork { get; set; }
        public string ProjectWiseName { get; set; }

        public DataSet BindDataForAdminDashboard()
        {
            DataSet ds = Connection.ExecuteQuery("GetDashBoardForAdmin");
            return ds;
        }
        
    }
}