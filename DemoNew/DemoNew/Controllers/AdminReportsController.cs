using AfluexHRMS.Filter;
using AfluexHRMS.Models;
using DemoNew.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoNew.Controllers
{
    public class AdminReportsController : Controller
    {
        // GET: AdminReports
        public ActionResult ProjectDailyWorkReports()
        {
            #region ddlProject
            AdminReports obj = new AdminReports();
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
            return View(obj);
        }
        [HttpPost]
        [ActionName("ProjectDailyWorkReports")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetDailyWorkReports(AdminReports model )
        {


            List<AdminReports> lst = new List<AdminReports>();
            model.ProjectID = model.ProjectID == "0" ? null : model.ProjectID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.GetDailyWorkReports();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.DailyWorkID = r["PK_DailyWorkID"].ToString();
                    obj.ProjectID = r["FK_ProjectID"].ToString();
                    obj.ProjectName = r["ProjectName"].ToString();
                    obj.Device = r["Device"].ToString();
                    obj.EntryDate = r["EntryDate"].ToString();
                    obj.Module = r["Module"].ToString();
                    obj.ImageWork = r["ImageWork"].ToString();
                    obj.Subject = r["Subject"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.FK_EmployeeID = r["FK_EmployeeID"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    lst.Add(obj);
                }
                model.lstDailyWork = lst;
            }
            #region ddlProject
            int count = 0;
            List<SelectListItem> ddlProject = new List<SelectListItem>();
            DataSet ds = model.GetProjectNameList();
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

            return View(model);
        }
    }
}