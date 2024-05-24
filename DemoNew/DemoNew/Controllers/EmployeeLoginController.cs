using AfluexHRMS;
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
    public class EmployeeLoginController : Controller
    {
        // GET: EmployeeLogin
        public ActionResult EmployeeDashBoard()
        {
            Employee obj = new Employee();
            try
            {
                obj.FK_EmployeeID= Session["PK_EmployeeID"].ToString();
                DataSet Ds = obj.BindDataForEmployeeDashboard();
                ViewBag.TotalWork = Ds.Tables[0].Rows[0]["TotalWork"].ToString();
                ViewBag.TotalOnProcessWork = Ds.Tables[1].Rows[0]["TotalOnProcessWork"].ToString();
                ViewBag.TotalDoneWork = Ds.Tables[2].Rows[0]["TotalDoneWork"].ToString();

               
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(obj);
        }
        public ActionResult DailyWork(string DailyWorkID)
        {
            Employee model = new Employee();
            #region ddlProject
            Employee obj = new Employee();
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
            if(DailyWorkID != null)
            {
                model.DailyWorkID = Crypto.Decrypt(DailyWorkID);
                DataSet dswork = model.GetDailyWorkReports();
                if (dswork != null && dswork.Tables.Count > 0 && dswork.Tables[0].Rows.Count > 0)
                {
                    model.DailyWorkID = Crypto.Encrypt(dswork.Tables[0].Rows[0]["PK_DailyWorkID"].ToString());
                    model.ProjectID = dswork.Tables[0].Rows[0]["FK_ProjectID"].ToString();
                    model.ProjectName = dswork.Tables[0].Rows[0]["ProjectName"].ToString();
                    model.Device = dswork.Tables[0].Rows[0]["Device"].ToString();
                    model.EntryDate = dswork.Tables[0].Rows[0]["EntryDate"].ToString();
                    model.Module = dswork.Tables[0].Rows[0]["Module"].ToString();
                    model.Subject = dswork.Tables[0].Rows[0]["Subject"].ToString();
                    model.Remark = dswork.Tables[0].Rows[0]["Remark"].ToString();
                    model.Status = dswork.Tables[0].Rows[0]["Status"].ToString();
                    model.FK_EmployeeID = dswork.Tables[0].Rows[0]["FK_EmployeeID"].ToString();
                    model.EmployeeName = dswork.Tables[0].Rows[0]["EmployeeName"].ToString();
                }
            }
            #region Device
            List<SelectListItem> SelectDevice = Common.Device();
            ViewBag.SelectDevice = SelectDevice;
            #endregion Device
            #region Status
            List<SelectListItem> WorkStatus = Common.WorkStatus();
            ViewBag.WorkStatus = WorkStatus;
            #endregion Status
            return View(model);
        }
        [HttpPost]
        [ActionName("DailyWork")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveProjectDailyWork(Employee obj, HttpPostedFileBase ImageWork)
        {
            try
            {
                if (ImageWork != null)
                {
                    obj.ImageWork = "/ImagesFile/ProjectWork/" + Guid.NewGuid() + Path.GetExtension(ImageWork.FileName);
                    ImageWork.SaveAs(Path.Combine(Server.MapPath(obj.ImageWork)));
                }
                obj.EntryDate = string.IsNullOrEmpty(obj.EntryDate) ? null : Common.ConvertToSystemDate(obj.EntryDate, "dd/MM/yyyy");
                obj.AddedBy = Session["PK_EmployeeID"].ToString();
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
            return RedirectToAction("DailyWork", "EmployeeLogin");
        }


        [HttpPost]
        [ActionName("DailyWork")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateProjectDailyWork(Employee obj, HttpPostedFileBase ImageWork)
        {
            try
            {
                if (ImageWork != null)
                {
                    obj.ImageWork = "/ImagesFile/ProjectWork/" + Guid.NewGuid() + Path.GetExtension(ImageWork.FileName);
                    ImageWork.SaveAs(Path.Combine(Server.MapPath(obj.ImageWork)));
                }
                obj.DailyWorkID = Crypto.Decrypt(obj.DailyWorkID);
                obj.EntryDate = string.IsNullOrEmpty(obj.EntryDate) ? null : Common.ConvertToSystemDate(obj.EntryDate, "dd/MM/yyyy");
                obj.UpdatedBy = Session["PK_EmployeeID"].ToString();
                DataSet ds = obj.UpdateProjectDailyWork();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateYourWork"] = "Your work has been updated successfully !!";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrUpdateYourWork"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrUpdateYourWork"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrUpdateYourWork"] = ex.Message;
            }
            return RedirectToAction("DailyWork", "EmployeeLogin");
        }
        public ActionResult EmployeeProfile()
        {
            return View();
        }
    }
}