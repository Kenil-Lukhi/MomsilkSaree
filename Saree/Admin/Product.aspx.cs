using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Linq;

namespace Saree.Admin
{
    public partial class Product : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["BreadCrum"] = "Product";
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    GetProduct();
                }
                lblMsg.Visible = false;
            }
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty,
                   ImagePath = string.Empty,
                   FileExtenshion = string.Empty;
            bool IsValidToexecuteExecute = false;
            int ProductID = Convert.ToInt32(hdnid.Value);

            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("ProductCrude", con);
            cmd.Parameters.AddWithValue("@Action", ProductID == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@Name", productNameTxt.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txtProductDiscrtoption.Text.Trim());
            cmd.Parameters.AddWithValue("@LongDescription", txtLongDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@Price", txtProductPrice.Text.Trim());
            cmd.Parameters.AddWithValue("@Quentity", txtProductQunatity.Text.Trim());
            cmd.Parameters.AddWithValue("@CategoryID", ddlCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);

            if (fuProductImage.HasFile)
            {
                if (utils.IsValidExtension(fuProductImage.FileName))
                {
                    string serverPath = Server.MapPath("~/Images/Product/");
                    Guid obj = Guid.NewGuid();
                    FileExtenshion = Path.GetExtension(fuProductImage.FileName);
                    ImagePath = "Images/Product/" + obj.ToString() + FileExtenshion;
                    fuProductImage.PostedFile.SaveAs(Path.Combine(serverPath, obj.ToString() + FileExtenshion));
                    cmd.Parameters.AddWithValue("@ImageUrl", ImagePath);
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
                    actionName = ProductID == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product " + actionName + " succsessfully.";
                    lblMsg.CssClass = "alert alert-success";
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Something went's wrong";
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                    clear();
                    GetProduct();
                }
            }
        }
        private void GetProduct()
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("ProductCrude", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            RProcuct.DataSource = dt;
            RProcuct.DataBind();
        }
        private void clear()
        {
            productNameTxt.Text = string.Empty;
            txtProductDiscrtoption.Text = string.Empty;
            txtProductPrice.Text = string.Empty;
            txtProductQunatity.Text = string.Empty;
            txtLongDescription.Text = string.Empty;
            ddlCategory.ClearSelection();
            cbIsActive.Checked = false;
            hdnid.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imgProduct.Visible = false;
            imgProduct.ImageUrl = string.Empty;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void RProcuct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            con = new SqlConnection(Connection.GetConnection());

            if (e.CommandName == "Edit")
            {
                try
                {
                    cmd = new SqlCommand("ProductCrude", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GETBYID");
                    cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    productNameTxt.Text = dt.Rows[0]["Name"].ToString();
                    txtProductPrice.Text = dt.Rows[0]["Price"].ToString();
                    txtProductQunatity.Text = dt.Rows[0]["Quentity"].ToString();
                    txtProductDiscrtoption.Text = dt.Rows[0]["Description"].ToString();
                    txtLongDescription.Text = dt.Rows[0]["LongDescription"].ToString();
                    ddlCategory.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
                    cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    imgProduct.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["Image"].ToString()) ? "../Images/No_Image.png" : "../" + dt.Rows[0]["Image"].ToString();
                    imgProduct.Height = 200;
                    imgProduct.Width = 200;
                    hdnid.Value = dt.Rows[0]["ProductID"].ToString();
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
                    GetProduct();
                }
            }
            else if (e.CommandName == "Delete")
            {
                cmd = new SqlCommand("ProductCrude", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
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
                    lblMsg.Text = "Something went's wrong";
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                    GetProduct();
                }
            }
        }


        protected void RProcuct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblIsActive = e.Item.FindControl("lblIsActive") as Label;
                Label lblQuentity = e.Item.FindControl("lblQuentity") as Label;
                if (lblIsActive.Text == "True")
                {
                    lblIsActive.Text = "Active";
                    lblIsActive.CssClass = "badge badge-success";
                }
                else
                {
                    lblIsActive.Text = "In-Active";
                    lblIsActive.CssClass = "badge badge-danger";
                }
                if (Convert.ToInt32(lblQuentity.Text) <= 5)
                {
                    lblQuentity.CssClass = "badge badge-danger";
                    lblQuentity.ToolTip = "Item about to be 'out of stock'";
                }
            }
        }
    }
}