using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saree.User;

namespace Saree.Admin
{
    public partial class Contects : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["BreadCrum"] = "Contect users";
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    GetContect();
                }
            }
        }

        private void GetContect()
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("ContectSP", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rContect.DataSource = dt;
            rContect.DataBind();
        }

        protected void rContect_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                con = new SqlConnection(Connection.GetConnection());
                cmd = new SqlCommand("ContectSP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ContectID", e.CommandArgument);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Record delete succsessfully.";
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
                    GetContect();
                }
            }
        }
    }
}