using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saree.Admin
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["BreadCrum"] = "";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    DashBoardCount dashBoardCount = new DashBoardCount();
                    Session["DashBoardcategory"] = dashBoardCount.Count("Category");
                    Session["DashBoardProduct"] = dashBoardCount.Count("Product");
                    Session["DashBoardOrder"] = dashBoardCount.Count("Order");
                    Session["DashBoardDelivered"] = dashBoardCount.Count("Delivered");
                    Session["DashBoardPending"] = dashBoardCount.Count("Pending");
                    Session["DashBoardUsers"] = dashBoardCount.Count("Users");
                    Session["DashBoardSOLDAMOUNT"] = dashBoardCount.Count("SOLDAMOUNT");
                    Session["DashBoardContect"] = dashBoardCount.Count("Contect");
                }
            }
        }
    }
}