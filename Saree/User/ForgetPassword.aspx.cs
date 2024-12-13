using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Saree.Common;

namespace Saree.User
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ResetPassword_Click(object sender, EventArgs e)
        {
            int code = 0;
            string Message = string.Empty;
            con = new SqlConnection(Connection.GetConnection());
            try
            {
                string password = CommonHelper.Encrypt(txtPassword.Text.Trim());
                cmd = new SqlCommand("ResetPasswor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.Add("@Code", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                if (con.State != ConnectionState.Open) con.Open();
                cmd.ExecuteNonQuery();
                code = (int)cmd.Parameters["@Code"].Value;
                Message = (string)cmd.Parameters["@Message"].Value;
                if (con.State == ConnectionState.Open) con.Close();

            }
            catch(Exception ex)
            {
                code = 2;
                Message = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
                clear();
                lblMsg.Text = Message; 
                lblMsg.Visible = true;
                lblMsg.CssClass = "alert alert-danger";
            }
            if(code == 0)
            {
                Response.Redirect("Login.aspx");
            }    

        }

        private void clear()
        {
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConPassword.Text = string.Empty;
        }
    }
}