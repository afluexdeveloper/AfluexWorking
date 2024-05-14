using DemoNew.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoNew.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }

        public ActionResult LoginAction(Home obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Employee"))
                        {
                            Session["LoginID"] = ds.Tables[0].Rows[0]["LoginID"].ToString();
                            Session["PK_EmployeeID"] = ds.Tables[0].Rows[0]["PK_EmployeeID"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                            Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                            FormName = "EmployeeDashboard";
                            Controller = "EmployeeLogin";
                        }
                        else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                        {
                            if (ds.Tables[0].Rows[0]["UserTypeName"].ToString() == "Admin")
                            {
                                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                                Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                                Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                                Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                                Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                                Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                                FormName = "AdminDashBoard";
                                Controller = "Admin";
                            }
                            else
                            {
                                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                                Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                                Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                                Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                                Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                                Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                                FormName = "AdminDashBoard";
                                Controller = "Admin";
                            }
                        }
                        else
                        {
                            TempData["Login"] = "Oops! Something went wrong, please try again later";
                            FormName = "Login";
                            Controller = "Home";
                        }
                    }
                    else
                    {
                        TempData["Login"] = "Oops! Something went wrong, please try again later";
                        FormName = "Login";
                        Controller = "Home";
                    }
                }

                else
                {
                    TempData["Login"] = "Oops! Invalid LoginID or Password, Please try again later";
                    FormName = "Login";
                    Controller = "Home";
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = "Oops! Something went wrong, please try again later";
                FormName = "Login";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }
    }
}