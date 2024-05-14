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
        public ActionResult DailyWork()
        {
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
    }
}