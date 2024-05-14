using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DemoNew.Models
{
    public class AdminReports
    {

        public string ProjectName { get; set; }
        public string Status { get; set; }
        public string Module { get; set; }
        public string Subject { get; set; }
        public string EntryDate { get; set; }
        public string Remark { get; set; }
        public string DailyWorkID { get; set; }
        public string AddedBy { get; set; }
        public string Device { get; set; }
        public string EmployeeName { get; set; }
        public string ProjectID { get; set; }
        public string FK_EmployeeID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ImageWork { get; set; }
        public List<AdminReports> lstDailyWork { get; set; }



        public DataSet GetProjectNameList()
        {
            DataSet ds = Connection.ExecuteQuery("GetProjectNameList");
            return ds;
        }
        public DataSet GetDailyWorkReports()
        {
            SqlParameter[] para =
                {
                     new SqlParameter("@ProjectID",ProjectID),
                      new SqlParameter("@Device",Device),
                      new SqlParameter("@Status",Status),
                      new SqlParameter("@FromDate",FromDate),
                      new SqlParameter("@ToDate",ToDate)
               };
            DataSet ds = Connection.ExecuteQuery("GetDailyWorkList",para);
            return ds;
        }
    }
}