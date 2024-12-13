using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saree.User
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetCategory();
                GetProduct();
            }
            
            Control sliderUserControl = (Control)Page.LoadControl("SliderUserControl.ascx");
            pnSliderUC.Controls.Add(sliderUserControl);
        }
        private void GetCategory()
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("CategoryCrude", con);
            cmd.Parameters.AddWithValue("@Action", "ActiveCategory");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rCategory.DataSource = dt;
            rCategory.DataBind();

            //rCategory2.DataSource = dt;
            //rCategory2.DataBind();
        }
        private void GetProduct()
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("ProductCrude", con);
            cmd.Parameters.AddWithValue("@Action", "ActiveProductForDFLT");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rProduct.DataSource = dt;
            rProduct.DataBind();
        }

        protected void rProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (Session["userid"] != null)
            {
                bool isCartItemUpdated = false;
                int check = isItemExistsInCart(Convert.ToInt32(e.CommandArgument));
                if (check == 0)
                {
                    con = new SqlConnection(Connection.GetConnection());
                    cmd = new SqlCommand("CartCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@Quentity", 1);
                    cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error " + ex.Message + "'); </script>");
                    }
                    finally
                    {
                        con.Close();

                    }
                }
                else
                {
                    utils utils = new utils();
                    isCartItemUpdated = utils.UpdateCartQuentity(check + 1, Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["userid"]));
                }
                Response.AddHeader("REFRESH", "1;URL=Cart.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        public int isItemExistsInCart(int productid)
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("CartCrud", con);
            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@ProductID", productid);
            cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            int Quentity = 0;
            if (dt.Rows.Count > 0)
            {
                Quentity = Convert.ToInt32(dt.Rows[0]["Quentity"]);
            }
            return Quentity;
        }
    }
}