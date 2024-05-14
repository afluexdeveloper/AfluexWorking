using AfluexHRMS.Filter;
using AfluexHRMS.Models;
using DemoNew.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoNew.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminDashboard()
        {
            Admin obj = new Admin();
            try
            {
                DataSet Ds = obj.BindDataForAdminDashboard();
                ViewBag.TotalProject = Ds.Tables[0].Rows[0]["TotalProject"].ToString();
                ViewBag.TotalWork = Ds.Tables[1].Rows[0]["TotalWork"].ToString();
                ViewBag.TotalOnProcessWork = Ds.Tables[2].Rows[0]["TotalOnProcessWork"].ToString();
                ViewBag.TotalDoneWork = Ds.Tables[3].Rows[0]["TotalDoneWork"].ToString();

                List<Admin> lst = new List<Admin>();
                if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow r in Ds.Tables[4].Rows)
                    {
                        Admin objM = new Admin();
                        objM.EmployeeName = r["EmployeeName"].ToString();
                        objM.DesignationDetails = r["DesignationDetails"].ToString();
                        objM.ProfilePic = r["ProfilePic"].ToString();
                        lst.Add(objM);
                    }
                    obj.lstGetEmployee = lst;
                }

                List<Admin> lstWorkTrend = new List<Admin>();
                if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow r in Ds.Tables[5].Rows)
                    {
                        Admin objM = new Admin();
                        objM.Subject = r["Subject"].ToString();
                        objM.Module = r["Module"].ToString();
                        objM.Status = r["Status"].ToString();
                        objM.WorkDate = r["WorkDate"].ToString();
                        objM.ProjectName = r["ProjectName"].ToString();
                        lstWorkTrend.Add(objM);
                    }
                    obj.lstGetWorkTrend = lstWorkTrend;
                }

                List<Admin> lstWorkTotal = new List<Admin>();
                if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[6].Rows.Count > 0)
                {
                    foreach (DataRow r in Ds.Tables[6].Rows)
                    {
                        Admin objM = new Admin();
                        objM.ProjectWiseName = r["ProjectName"].ToString();
                        objM.TotalProjectWork = r["TotalProjectWork"].ToString();
                        lstWorkTotal.Add(objM);
                    }
                    obj.lstTotalProjectWork = lstWorkTotal;
                }
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(obj);
        }
        public ActionResult EmployeeRegistration(Master model,string EmployeeID)
        {
            if (EmployeeID != null)
            {
                model.EmployeeID = EmployeeID;
                DataSet dsblock = model.DetailedEmployeeInfo();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {
                    
                    model.DepartmentID = dsblock.Tables[0].Rows[0]["FK_DepartmentID"].ToString();
                    #region GetDesignation
                    List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                    DataSet ds = model.DesignationList();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["Pk_DesignationID"].ToString() });

                        }
                    }
                    ViewBag.ddlDesignation = ddlDesignation;
                    #endregion
                    model.DesignationID = dsblock.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.FatherName = dsblock.Tables[0].Rows[0]["FatherName"].ToString();

                    #region Gender
                    List<SelectListItem> Gender = Common.GenderList();
                    ViewBag.Gender = Gender;
                    #endregion Gender

                    model.Gender = dsblock.Tables[0].Rows[0]["Gender"].ToString();
                    model.PhoneNo = dsblock.Tables[0].Rows[0]["PhoneNo"].ToString();
                    model.Address = dsblock.Tables[0].Rows[0]["Address"].ToString();
                    model.PAN = dsblock.Tables[0].Rows[0]["PAN"].ToString();
                    model.EmployeeID = dsblock.Tables[0].Rows[0]["PK_EmployeeID"].ToString();
                    model.LoginID = dsblock.Tables[0].Rows[0]["LoginID"].ToString();
                    model.EmployeeName = dsblock.Tables[0].Rows[0]["EmployeeName"].ToString();
                    model.DepartmentName = dsblock.Tables[0].Rows[0]["DepartmentName"].ToString();
                    model.DesignationName = dsblock.Tables[0].Rows[0]["DesignationName"].ToString();
                    model.Gender = dsblock.Tables[0].Rows[0]["Gender"].ToString();
                    model.DateOfJoining = dsblock.Tables[0].Rows[0]["DateOfJoining"].ToString();
                    model.Contact = dsblock.Tables[0].Rows[0]["MobileNo"].ToString();
                    model.ProfilePic = dsblock.Tables[0].Rows[0]["ProfilePic"].ToString();
                    model.Email = dsblock.Tables[0].Rows[0]["Email"].ToString();
                    model.DOB = dsblock.Tables[0].Rows[0]["DOB"].ToString();
                }
            }

            else
            {
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                ViewBag.ddlDesignation = ddlDesignation;

                model.DateOfJoining = DateTime.Now.ToString("dd/MM/yyyy");

                #region Gender
                List<SelectListItem> Gender = Common.GenderList();
                ViewBag.Gender = Gender;
                #endregion Gender
            }
            
            #region ddlDepartment
            Master objDep = new Master();
            int count1 = 0;
            List<SelectListItem> ddlDepartment = new List<SelectListItem>();
            DataSet ds2 = objDep.DepartmentList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlDepartment.Add(new SelectListItem { Text = "Select Department", Value = "0" });
                    }
                    ddlDepartment.Add(new SelectListItem { Text = r["DepartmentName"].ToString(), Value = r["PK_DepartmentID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlDepartment = ddlDepartment;

            #endregion

            return View(model);
        }
        [HttpPost]
        [ActionName("EmployeeRegistration")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveEmployeeRegistration(HttpPostedFileBase postedFile, Master obj)
        {
            string FormName = "";
            string Controller = "";

            obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
            try
            {
                //obj.ProfilePic = "/ImageProfile/EmployeeProfile/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                //postedFile.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePic)));
                obj.AddedBy = "1";
                DataSet ds = obj.SaveEmployeeRegistration();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        Session["EmployeeName"] = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();

                        FormName = "ConfirmationRegistration";
                        Controller = "Admin";
                    }
                    else
                    {
                        TempData["Emp"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "EmployeeRegistration";
                        Controller = "Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Emp"] = ex.Message;
                FormName = "EmployeeRegistration";
                Controller = "Admin";
            }

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ConfirmationRegistration()
        {
            return View();
        }

        public ActionResult EmployeeDetails(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds1 = model.GetEmployeeDetails();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master objM = new Master();
                    objM.Password = r["Password"].ToString();
                    objM.EmployeeID = r["PK_EmployeeID"].ToString();
                    objM.LoginID = r["LoginID"].ToString();
                    objM.EmployeeName = r["EmployeeName"].ToString();
                    objM.DepartmentName = r["DepartmentName"].ToString();
                    objM.DesignationName = r["DesignationName"].ToString();
                    objM.DateOfJoining = r["DateofJoining"].ToString();
                    objM.Contact = r["MobileNo"].ToString();
                    lst.Add(objM);
                }
                model.lstEmployeeDetails = lst;
            }
            
            #region ddlDepartment
            Master objDep = new Master();
            int count1 = 0;
            List<SelectListItem> ddlDepartment = new List<SelectListItem>();
            DataSet ds2 = objDep.DepartmentList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlDepartment.Add(new SelectListItem { Text = "Select Department", Value = "0" });
                    }
                    ddlDepartment.Add(new SelectListItem { Text = r["DepartmentName"].ToString(), Value = r["PK_DepartmentID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlDepartment = ddlDepartment;

            #endregion

            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
            ViewBag.ddlDesignation = ddlDesignation;
            return View(model);
        }

        [HttpPost]
        [ActionName("EditEmployeeBasicInfo")]
        [OnAction(ButtonName = "Search")]
        public ActionResult EmployeeReportBy(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds1 = model.GetEmployeeDetails();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master objM = new Master();
                    objM.Password = r["Password"].ToString();
                    objM.EmployeeID = r["PK_EmployeeID"].ToString();
                    objM.LoginID = r["LoginID"].ToString();
                    objM.EmployeeName = r["EmployeeName"].ToString();
                    objM.DepartmentName = r["DepartmentName"].ToString();
                    objM.DesignationName = r["DesignationName"].ToString();
                    objM.DateOfJoining = r["DateofJoining"].ToString();
                    objM.Contact = r["MobileNo"].ToString();
                    lst.Add(objM);
                }
                model.lstEmployeeDetails = lst;
            }

            #region ddlDepartment
            Master objDep = new Master();
            int count1 = 0;
            List<SelectListItem> ddlDepartment = new List<SelectListItem>();
            DataSet ds2 = objDep.DepartmentList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlDepartment.Add(new SelectListItem { Text = "Select Department", Value = "0" });
                    }
                    ddlDepartment.Add(new SelectListItem { Text = r["DepartmentName"].ToString(), Value = r["PK_DepartmentID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlDepartment = ddlDepartment;

            #endregion

            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
            ViewBag.ddlDesignation = ddlDesignation;
            return View(model);
        }
    }
}