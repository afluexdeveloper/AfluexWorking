using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoNew.Models
{
    public class Master
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
        public string Pk_DepartmentId { get; set; }
        public string Pk_DesignationID { get; set; }
        public List<SelectListItem> ddlDesignation { get; set; }

        #region Department
        public DataSet AddDepartment()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@DepartmentName",DepartmentName),
                                new SqlParameter("@AddedBy",AddedBy),

                            };
            DataSet ds = Connection.ExecuteQuery("SaveDepartment", para);
            return ds;

        }

        public DataSet DepartmentDetails()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Pk_DepartmentId",Pk_DepartmentId),

                            };
            DataSet ds = Connection.ExecuteQuery("GetDepartmentList", para);

            return ds;

        }

        public DataSet UpdateDepartment()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@DepartmentName",DepartmentName),
                                new SqlParameter("@Pk_DepartmentId",Pk_DepartmentId),
                                 new SqlParameter("@UpdatedBy",UpdatedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("UpdateDepartment", para);
            return ds;

        }
        public DataSet DeleteDepartment()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Pk_DepartmentId",Pk_DepartmentId),
                                new SqlParameter("@DeletedBy",DeletedBy),

                            };
            DataSet ds = Connection.ExecuteQuery("DeleteDepartment", para);
            return ds;

        }
        #endregion

        #region Designation
        public DataSet InsertDesignation()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@FK_DepartmentID",DepartmentID),
                                new SqlParameter("@DesignationName",DesignationName),
                                 new SqlParameter("@AddedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("SaveDesignation", para);
            return ds;

        }
        public DataSet DesignationDetails()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Pk_DesignationID",Pk_DesignationID),
                            };
            DataSet ds = Connection.ExecuteQuery("GetDesignationList", para);
            return ds;

        }


        public DataSet UpdateDesignation()
        {
            SqlParameter[] para = {
                                new SqlParameter("@Pk_DesignationId",Pk_DesignationID),
                                new SqlParameter("@FK_DepartmentID",Fk_DepartmentId),
                                 new SqlParameter("@DesignationName",DesignationName),
                                 new SqlParameter("@UpdatedBy",UpdatedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdateDesignation", para);
            return ds;

        }

        public DataSet DeleteDesignation()
        {
            SqlParameter[] para = {
                                new SqlParameter("@Pk_DesignationId",Pk_DesignationID),
                                new SqlParameter("@DeletedBy",DeletedBy),
                            };
            DataSet ds = Connection.ExecuteQuery("DeleteDesignation", para);
            return ds;

        }
        #endregion

        public DataSet SaveProjectName()
        {
            SqlParameter[] para = 
                {
                     new SqlParameter("@ProjectName",ProjectName),
                      new SqlParameter("@ProjectLogo",imgLogo),
                      new SqlParameter("@AddedBy",AddedBy),
               };
            DataSet ds = Connection.ExecuteQuery("SaveProjectName", para);
            return ds;
        }
        public DataSet GetProjectNameList()
        {
            DataSet ds = Connection.ExecuteQuery("GetProjectNameList");
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
                      new SqlParameter("@Status",Status),
                      new SqlParameter("@Remarks",Remark),
                      new SqlParameter("@AddedBy",AddedBy),
               };
            DataSet ds = Connection.ExecuteQuery("SaveDailyWork", para);
            return ds;
        }
        public DataSet DetailedEmployeeInfo()
        {
            SqlParameter[] para ={
                     new SqlParameter("@EmployeeID",EmployeeID),
                            };
            DataSet ds = Connection.ExecuteQuery("DetailedEmployeeInformation", para);
            return ds;
        }
        public DataSet SaveEmployeeRegistration()
        {
            SqlParameter[] para ={
                                  new SqlParameter("@FK_DepartmentID",DepartmentID),
                                  new SqlParameter("@FK_DesignationID",DesignationID),
                                  new SqlParameter("@EmployeeName",EmployeeName),
                                  new SqlParameter("@FatherName",FatherName),
                                  new SqlParameter("@DOB",DOB),
                                  new SqlParameter("@Gender",Gender),
                                  new SqlParameter("@MobileNo",Contact),
                                  new SqlParameter("@PhoneNo",PhoneNo),
                                  new SqlParameter("@Address",Address),
                                  new SqlParameter("@Email",Email),
                                  new SqlParameter("@DateOfJoining",DateOfJoining),
                                  new SqlParameter("@PAN",PAN),
                                  new SqlParameter("@ProfilePic",ProfilePic),
                                  new SqlParameter("@AddedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("SaveEmployeeDetails", para);
            return ds;
        }
        public DataSet GetEmployeeDetails()
        {
            SqlParameter[] para ={
             new SqlParameter("@EmployeeName", EmployeeName),
             new SqlParameter("@LoginID", LoginID),
               new SqlParameter("@DepartmentID", DepartmentID),
             new SqlParameter("@DesignationID", DesignationID),
                            };
            DataSet ds = Connection.ExecuteQuery("GetEmployeeDetails", para);
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

    }
}