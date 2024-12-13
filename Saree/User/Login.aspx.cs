using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saree.Common;

namespace Saree.User
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] != null)
            {
                Response.Redirect("Default.aspx");
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            string pass = CommonHelper.Encrypt(txtPassword.Text.Trim());
            string pass2 = CommonHelper.Decrypt("CwmVvwjQv3id048yj50AXA==");
            if (txtUsername.Text.Trim() == "Admin" && pass == "CwmVvwjQv3id048yj50AXA==")
            {
                Session["admin"] = txtUsername.Text.Trim();
                Response.Redirect("../Admin/Dashboard.aspx");
            }
            else
            {
                con = new SqlConnection(Connection.GetConnection());
                cmd = new SqlCommand("UserSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECTFORLOGIN");
                cmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", pass);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    Session["username"] = txtUsername.Text.Trim();
                    Session["userid"] = dt.Rows[0]["UserID"];
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Invalid Credential..";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
        }

        private void SavePasswordInCookie(string username, string password)
        {
            HttpCookie cookie = new HttpCookie("UserLogin");
            cookie.Values["Username"] = username;
            cookie.Values["Password"] = password;
            cookie.Expires = DateTime.Now.AddDays(30); // Set the cookie to expire in 30 days
            Response.Cookies.Add(cookie);
        }
    }
}