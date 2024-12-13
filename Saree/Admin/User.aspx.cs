using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saree.Admin
{
    public partial class User : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["BreadCrum"] = "User";
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    GetUser();
                }
            }
        }
        private void GetUser()
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("UserSelect", con);
            cmd.Parameters.AddWithValue("@Action", "SELECTFORADMIN");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            RUser.DataSource = dt;
            RUser.DataBind();
        }

        protected void RUser_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                con = new SqlConnection(Connection.GetConnection());
                cmd = new SqlCommand("UserInsertUpdateDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@UserID", e.CommandArgument);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "User delete succsessfully.";
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
                    GetUser();
                }
            }
        }
    }
}