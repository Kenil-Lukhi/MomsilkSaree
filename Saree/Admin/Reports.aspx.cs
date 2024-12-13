using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saree.Admin
{
    public partial class Reports : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["BreadCrum"] = "Selling Report";
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
            }
        }
        private void GetReportData(DateTime FromDate, DateTime ToDate)
        {
            double GrandTotal = 0;
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("SellingReport", con);
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@ToDate", ToDate);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    GrandTotal += Convert.ToDouble(dr["TotalPrice"]);
                }
                lblTotal.Text = "Sold Cost : ₹" + GrandTotal.ToString();
                lblTotal.CssClass = "badge badge-primary";
            }

            rReport.DataSource = dt;
            rReport.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text);

            if (ToDate > DateTime.Now)
            {
                Response.Write("<script>alert('ToDate cannot be greater than current date!'); </script>");
            }
            else if (FromDate > ToDate)
            {
                Response.Write("<script>alert('FromDate cannot be greater than ToDate date!'); </script>");
            }
            else
            {
                GetReportData(FromDate, ToDate);
            }
        }
    }
}