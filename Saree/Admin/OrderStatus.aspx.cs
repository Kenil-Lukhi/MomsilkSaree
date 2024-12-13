using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Saree.Admin
{
    public partial class OrderStatus : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["BreadCrum"] = "Order Status";
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    GetOrderStatus();
                }
            }
            lblMsg.Visible = false;
            pOrderUpdateStatus.Visible = false;

        }

        protected void rOrderStatus_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "Edit")
            {
                con = new SqlConnection(Connection.GetConnection());
                try
                {
                    cmd = new SqlCommand("Invoice", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "STATUSBYID");
                    cmd.Parameters.AddWithValue("@OrderDetailsID", e.CommandArgument);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    hdnid.Value = dt.Rows[0]["OrderDetailsID"].ToString();
                    ddlOrderStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    pOrderUpdateStatus.Visible = true;
                    LinkButton btn = e.Item.FindControl("lnkEdit") as LinkButton;
                    btn.CssClass = "badge badge-warning";
                }
                catch (Exception)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Something went's wrong";
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                    GetOrderStatus();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int OrderDetailsID = Convert.ToInt32(hdnid.Value);

            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("Invoice", con);
            cmd.Parameters.AddWithValue("@Action", "UPDTSTATUS");
            cmd.Parameters.AddWithValue("@Status", ddlOrderStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@OrderDetailsID", OrderDetailsID);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                lblMsg.Visible = true;
                lblMsg.Text = "Order status updated succsessfully.";
                lblMsg.CssClass = "alert alert-success";
            }
            catch (Exception)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Something went's wrong";
                lblMsg.CssClass = "alert alert-danger";
            }
            finally
            {
                con.Close();
                GetOrderStatus();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pOrderUpdateStatus.Visible = false;
        }

        private void GetOrderStatus()
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("Invoice", con);
            cmd.Parameters.AddWithValue("@Action", "GETSTATUS");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rOrderStatus.DataSource = dt;
            rOrderStatus.DataBind();
        }
    }
}