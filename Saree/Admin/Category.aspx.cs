using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saree.Admin
{
    public partial class Category : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["BreadCrum"] = "Category";
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    GetCategory();
                }
            }
            lblMsg.Visible = false;
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty,
                   ImagePath = string.Empty,
                   FileExtenshion = string.Empty;
            bool IsValidToexecuteExecute = false;
            int CategoryID = Convert.ToInt32(hdnid.Value);

            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("CategoryCrude", con);
            cmd.Parameters.AddWithValue("@Action", CategoryID == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@Categoryid", CategoryID);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);

            if (fuCategoryImage.HasFile)
            {
                if (utils.IsValidExtension(fuCategoryImage.FileName))
                {
                    string serverPath = Server.MapPath("~/Images/Category/");
                    Guid obj = Guid.NewGuid();
                    FileExtenshion = Path.GetExtension(fuCategoryImage.FileName);
                    ImagePath = "Images/Category/" + obj.ToString() + FileExtenshion;
                    fuCategoryImage.PostedFile.SaveAs(Path.Combine(serverPath, obj.ToString() + FileExtenshion));
                    cmd.Parameters.AddWithValue("@imageUrl", ImagePath);
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
                    actionName = CategoryID == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Category " + actionName + " succsessfully.";
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
                    clear();
                    GetCategory();
                }
            }
        }

        private void GetCategory()
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("CategoryCrude", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            RCategory.DataSource = dt;
            RCategory.DataBind();
        }

        private void clear()
        {
            txtName.Text = string.Empty;
            cbIsActive.Checked = false;
            hdnid.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imgCategory.Visible = false;
            imgCategory.ImageUrl = string.Empty;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void RCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            con = new SqlConnection(Connection.GetConnection());

            if (e.CommandName == "Edit")
            {
                try
                {
                    cmd = new SqlCommand("CategoryCrude", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GETBYID");
                    cmd.Parameters.AddWithValue("@Categoryid", e.CommandArgument);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    imgCategory.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["Image"].ToString()) ? "../Images/No_Image.png" : "../" + dt.Rows[0]["Image"].ToString();
                    imgCategory.Height = 200;
                    imgCategory.Width = 200;
                    hdnid.Value = dt.Rows[0]["CategoryID"].ToString();
                    btnAddOrUpdate.Text = "Update";
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
                    GetCategory();
                }
            }
            else if (e.CommandName == "Delete")
            {
                cmd = new SqlCommand("CategoryCrude", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@Categoryid", e.CommandArgument);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Category delete succsessfully.";
                    lblMsg.CssClass = "alert alert-success";
                }
                catch (Exception)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Something went's wrong to delete product.";
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                    GetCategory();
                }
            }
        }

        protected void RCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl = e.Item.FindControl("lblIsActive") as Label;
                if (lbl.Text == "True")
                {
                    lbl.Text = "Active";
                    lbl.CssClass = "badge badge-success";
                }
                else
                {
                    lbl.Text = "In-Active";
                    lbl.CssClass = "badge badge-danger";
                }
            }
        }
    }
}