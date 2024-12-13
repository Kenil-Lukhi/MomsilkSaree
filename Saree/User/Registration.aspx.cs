using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Saree.Admin;
using Saree.Common;

namespace Saree.User
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    GetUser();
                }
                else if (Session["userid"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        private void clear()
        {
            txtName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPostCode.Text = string.Empty;
        }
        private void GetUser()
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("UserSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECTFORPROFILE");
            cmd.Parameters.AddWithValue("@UserID", Request.QueryString["id"]);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count == 1 )
            {
                string pass = CommonHelper.Decrypt(dt.Rows[0]["Password"].ToString());
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                txtMobileNo.Text = dt.Rows[0]["Mobile"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtPostCode.Text = dt.Rows[0]["PostCode"].ToString();
                string password =  CommonHelper.Decrypt(dt.Rows[0]["Password"].ToString());
                string val = txtPassword.Text;
                txtPassword.Text = password;
                val = txtPassword.Text;
                imgUser.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageURL"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["ImageURL"].ToString();
                imgUser.Height = 200;
                imgUser.Width = 200;
            }
            lblHeadingMsg.Text = "<h2>Edit Profile</h2>";
            btnRegister.Text = "Update";
            lblAlredyUser.Text = "";
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty,
                   ImagePath = string.Empty,
                   FileExtenshion = string.Empty;

            string pass = CommonHelper.Encrypt(txtPassword.Text.Trim());
            int Code = 0;
            int userid = Convert.ToInt32(Request.QueryString["id"]);
            bool IsValidToexecuteExecute = false;

            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("UserInsertUpdateDelete", con);
            cmd.Parameters.AddWithValue("@Action", userid == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@FullName", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@PostCode", txtPostCode.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", pass);
            cmd.Parameters.Add("@Code", SqlDbType.Int).Direction = ParameterDirection.Output;
            if (fuUserImage.HasFile)
            {
                if (utils.IsValidExtension(fuUserImage.FileName))
                {
                    string serverPath = Server.MapPath("~/Images/User/");
                    Guid obj = Guid.NewGuid();
                    FileExtenshion = Path.GetExtension(fuUserImage.FileName);
                    ImagePath = "Images/User/" + obj.ToString() + FileExtenshion;
                    fuUserImage.PostedFile.SaveAs(Path.Combine(serverPath, obj.ToString() + FileExtenshion));
                    cmd.Parameters.AddWithValue("@ImgURL", ImagePath);
                    IsValidToexecuteExecute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Please select .jpg, .png, .jpeg image";
                    lblMsg.CssClass = "alert alert-danger";
                    IsValidToexecuteExecute = false;
                }
            }
            else
            {
                IsValidToexecuteExecute = true;
            }
            if (IsValidToexecuteExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Code = (int)cmd.Parameters["@Code"].Value;
                    actionName = userid == 0 ? "Registration is successful ! <b> <a href='Login.aspx'> Click here</a> </b> to do login"
                                             : "Details updated successful ! <b> <a href='Profile.aspx'> Can check here</a> </b> to do login";
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b> " + txtUserName.Text.Trim() + "</b>" + actionName;
                    lblMsg.CssClass = "alert alert-success";
                    if (userid != 0)
                    {
                        Response.AddHeader("REFRESH", "1;URL=Profile.aspx");
                    }
                    if (Code == 1)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b>" + txtUserName.Text.Trim() + "</b> Username is alredy exist, try again ..!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
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
                    clear();
                }
            }
        }
    }
}