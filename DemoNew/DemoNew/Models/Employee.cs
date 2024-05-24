using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoNew.Models
{
    public class Employee
    {

        public string ProjectName { get; set; }
        public string Status { get; set; }
        public string Module { get; set; }
        public string Subject { get; set; }
        public string EntryDate { get; set; }
        public string Remark { get; set; }
        public string imgLogo { get; set; }
        public string postedFile { get; set; }
        public string AddedBy { get; set; }
        public string Device { get; set; }
        public string Password { get; set; }
        public string ImageWork { get; set; }


        public List<Master> lstProject { get; set; }
        public List<Master> lstEmployeeDetails { get; set; }
        public List<Master> listDepartment { get; set; }
        public List<Master> listDesignation { get; set; }
        public string ProjectLogo { get; set; }
        public string ProjectID { get; set; }
        public string DepartmentID { get; set; }
        public string DesignationID { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string PAN { get; set; }
        public string EmployeeID { get; set; }
        public string LoginID { get; set; }
        public string EmployeeName { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string DateOfJoining { get; set; }
        public string Contact { get; set; }
        public string ProfilePic { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Result { get; set; }
        public string Fk_DepartmentId { get; set; }
        public string DeletedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Pk_DepartmentId { get; set; }
        public string Pk_DesignationID { get; set; }
        public string FK_EmployeeID { get; set; }
        public string DailyWorkID { get; set; }
        public List<SelectListItem> ddlDesignation { get; set; }
        public List<Employee> lstDailyWork { get; set; }


        public DataSet GetProjectNameList()
        {
            DataSet ds = Connection.ExecuteQuery("GetProjectNameList");
            return ds;
        }
        public DataSet BindDataForEmployeeDashboard()
        {
            SqlParameter[] para =
                {
                     new SqlParameter("@FK_EmployeeID",FK_EmployeeID),
               };
            DataSet ds = Connection.ExecuteQuery("GetDashBoardForEmployee",para);
            return ds;
        }
        public DataSet SaveProjectDailyWork()
        {
            SqlParameter[] para =
                {
                     new SqlParameter("@ProjectID",ProjectID),
                      new SqlParameter("@Device",Device),
                       new SqlParameter("@EntryDate",EntryDate),
                      new SqlParameter("@Subject",Subject),
                       new SqlParameter("@Module",Module),
                       new SqlParameter("@ImageWork",ImageWork),
                      new SqlParameter("@Status",Status),
                      new SqlParameter("@Remarks",Remark),
                      new SqlParameter("@AddedBy",AddedBy),
               };
            DataSet ds = Connection.ExecuteQuery("SaveDailyWork", para);
            return ds;
        }
        public DataSet UpdateProjectDailyWork()
        {
            SqlParameter[] para =
                {
                new SqlParameter("@DailyWorkID",DailyWorkID),
                     new SqlParameter("@ProjectID",ProjectID),
                      new SqlParameter("@Device",Device),
                       new SqlParameter("@EntryDate",EntryDate),
                      new SqlParameter("@Subject",Subject),
                       new SqlParameter("@Module",Module),
                       new SqlParameter("@ImageWork",ImageWork),
                      new SqlParameter("@Status",Status),
                      new SqlParameter("@Remarks",Remark),
                      new SqlParameter("@UpdatedBy",UpdatedBy),
               };
            DataSet ds = Connection.ExecuteQuery("UpdateDailyWork", para);
            return ds;
        }
        public DataSet DesignationList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_DepartmentID", DepartmentID) };
            DataSet ds = Connection.ExecuteQuery("GetDesignationList", para);
            return ds;
        }
        public DataSet DeleteEmployee()
        {
            SqlParameter[] para = {
                     new SqlParameter("@EmployeeID",EmployeeID),
                     new SqlParameter("@DeletedBy",AddedBy),
                            };
            DataSet ds = Connection.ExecuteQuery("DeleteEmployee", para);
            return ds;
        }
        public DataSet DepartmentList()
        {
            DataSet ds = Connection.ExecuteQuery("GetDepartmentList");
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
                      new SqlParameter("@ToDate",ToDate),
                      new SqlParameter("@FK_EmployeeID",FK_EmployeeID),
                       new SqlParameter("@DailyWorkID",DailyWorkID),
               };
            DataSet ds = Connection.ExecuteQuery("GetDailyWorkList", para);
            return ds;
        }
    }
}