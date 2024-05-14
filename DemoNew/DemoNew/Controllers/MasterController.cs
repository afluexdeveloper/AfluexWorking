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
    public class MasterController : Controller
    {
        // GET: Master
        public ActionResult ProjectMaster(Master model)
        {
            List<Master> lst = new List<Master>();
            //model.EmployeeCode = Session["LoginID"].ToString();
            DataSet ds1 = model.GetProjectNameList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master objM = new Master();
                    objM.ProjectID = r["PK_ProjectID"].ToString();
                    objM.ProjectName = r["ProjectName"].ToString();
                    objM.ProjectLogo = r["ProjectLogo"].ToString();
                    lst.Add(objM);
                }
                model.lstProject = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ProjectMaster")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveProjectName(Master obj, HttpPostedFileBase imgLogo)
        {
            try
            {
                if (imgLogo != null)
                {
                    obj.imgLogo = "/ImagesFile/ProjectLogo/" + Guid.NewGuid() + Path.GetExtension(imgLogo.FileName);
                    imgLogo.SaveAs(Path.Combine(Server.MapPath(obj.imgLogo)));
                }
                obj.EntryDate = string.IsNullOrEmpty(obj.EntryDate) ? null : Common.ConvertToSystemDate(obj.EntryDate, "dd/MM/yyyy");
                obj.AddedBy = "1";
                DataSet ds = obj.SaveProjectName();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SaveProject"] = "Project name saved successfully !!";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrProject"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrProject"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrSaveProject"] = ex.Message;
            }
            return RedirectToAction("ProjectMaster", "Master");
        }

        public ActionResult ProjectDailyWork()
        {
            #region ddlProject
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlProject = new List<SelectListItem>();
            DataSet ds = obj.GetProjectNameList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProject.Add(new SelectListItem { Text = "Select Project", Value = "0" });
                    }
                    ddlProject.Add(new SelectListItem { Text = r["ProjectName"].ToString(), Value = r["PK_ProjectID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlProject = ddlProject;

            #endregion

            #region Device
            List<SelectListItem> SelectDevice = Common.Device();
            ViewBag.SelectDevice = SelectDevice;
            #endregion Device
            #region Status
            List<SelectListItem> WorkStatus = Common.WorkStatus();
            ViewBag.WorkStatus = WorkStatus;
            #endregion Status
            return View();
        }
        [HttpPost]
        [ActionName("ProjectDailyWork")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveProjectDailyWork(Master obj)
        {
            try
            {
                obj.EntryDate = string.IsNullOrEmpty(obj.EntryDate) ? null : Common.ConvertToSystemDate(obj.EntryDate, "dd/MM/yyyy");
                obj.AddedBy = "1";
                DataSet ds = obj.SaveProjectDailyWork();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SaveYourWork"] = "Your work has been saved successfully !!";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrYourWork"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrYourWork"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrYourWork"] = ex.Message;
            }
            return RedirectToAction("ProjectDailyWork", "Master");
        }
        #region Department

        public ActionResult DepartmentMaster(string Pk_DepartmentId)
        {
            Master model = new Master();
            if (Pk_DepartmentId != null)
            {
                model.Pk_DepartmentId = Pk_DepartmentId;
                DataSet ds = model.DepartmentDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.Pk_DepartmentId = ds.Tables[0].Rows[0]["PK_DepartmentID"].ToString();
                    model.DepartmentName = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                }
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("DepartmentMaster")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveDepartment(Master obj)
        {
            try
            {
                obj.AddedBy ="1";
                DataSet ds = obj.AddDepartment();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Departmentmsg"] = "Department saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDepartmentmsg"] = ex.Message;
            }
            return RedirectToAction("DepartmentMaster", "Master");
        }

        public ActionResult DepartmentList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.DepartmentDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_DepartmentId = r["PK_DepartmentID"].ToString();
                    obj.DepartmentName = r["DepartmentName"].ToString();

                    lst.Add(obj);
                }
                model.listDepartment = lst;
            }
            return View(model);
        }



        [HttpPost]
        [ActionName("DepartmentMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateDepartmentMaster(Master obj)
        {
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateDepartment();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Departmentmsg"] = "Department Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDepartmentmsg"] = ex.Message;
            }
            return RedirectToAction("DepartmentList", "Master");
        }

        public ActionResult DeleteDepartment(Master obj)
        {
            try
            {
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteDepartment();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Departmentmsg"] = "Department Deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDepartmentmsg"] = ex.Message;
            }
            return RedirectToAction("DepartmentList", "Master");
        }

        #endregion

        #region Designation

        public ActionResult DesignationMaster(string Pk_DesignationID)
        {
            Master model = new Master();

            #region ddlDepartment
            try
            {
                Master obj = new Master();
                int count = 0;
                List<SelectListItem> ddlDepartment = new List<SelectListItem>();
                DataSet ds1 = obj.DepartmentDetails();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlDepartment.Add(new SelectListItem { Text = "Select Department", Value = "0" });
                        }
                        ddlDepartment.Add(new SelectListItem { Text = r["DepartmentName"].ToString(), Value = r["PK_DepartmentID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlDepartment = ddlDepartment;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion


            if (Pk_DesignationID != null)
            {
                model.Pk_DesignationID = Pk_DesignationID;

                DataSet ds = model.DesignationDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.Pk_DesignationID = ds.Tables[0].Rows[0]["Pk_DesignationID"].ToString();
                    model.Fk_DepartmentId = ds.Tables[0].Rows[0]["FK_DepartmentID"].ToString();
                    model.DesignationName = ds.Tables[0].Rows[0]["DesignationName"].ToString();

                }
            }
            return View(model);


        }



        [HttpPost]
        [ActionName("DesignationMaster")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveDesignation(Master obj)
        {
            try
            {
                obj.AddedBy = "1";
                DataSet ds = obj.InsertDesignation();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Designationmsg"] = "Designation saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDesignationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDesignationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDesignationmsg"] = ex.Message;
            }
            return RedirectToAction("DesignationMaster", "Master");
        }


        public ActionResult DesignationList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.DesignationDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_DesignationID = r["Pk_DesignationID"].ToString();
                    obj.DepartmentName = r["DepartmentName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();

                    lst.Add(obj);
                }
                model.listDesignation = lst;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("DesignationMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateDesignation(Master obj)
        {
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateDesignation();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Designationmsg"] = "Designation Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDesignationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDesignationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDesignationmsg"] = ex.Message;
            }
            return RedirectToAction("DesignationList", "Master");
        }


        public ActionResult DeleteDesignation(Master obj)
        {
            try
            {
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.DeleteDesignation();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Designationmsg"] = "Designation Deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDesignationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDesignationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDesignationmsg"] = ex.Message;
            }
            return RedirectToAction("DesignationList", "Master");
        }


        #endregion

        public ActionResult GetDesignation(string DepartmentID)
        {
            try
            {
                Master model = new Master();
                model.DepartmentID = DepartmentID;

                #region GetDesignation
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                DataSet dsDes = model.DesignationList();

                if (dsDes != null && dsDes.Tables.Count > 0)
                {
                    model.Result = "yes";
                    foreach (DataRow r in dsDes.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["Pk_DesignationID"].ToString() });
                    }
                }
                model.ddlDesignation = ddlDesignation;
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}