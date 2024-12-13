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

namespace Saree.User
{
    public partial class Details : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected int ProductID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);

            if (ProductID != 0)
            {
                GetUSerDetails();
            }

        }

        private void GetUSerDetails()
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("ProductCrude", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                Session["ProductID"] = dt.Rows[0]["ProductID"].ToString();
                Session["ProductName"] = dt.Rows[0]["Name"].ToString();
                Session["ProductDescription"] = dt.Rows[0]["Description"].ToString();
                Session["ProductLongDescription"] = dt.Rows[0]["LongDescription"].ToString();
                Session["ProductPrice"] = dt.Rows[0]["Price"].ToString();
                Session["ProductImage"] = dt.Rows[0]["Image"].ToString();
            }
        }

        protected void lbCheckout_Click(object sender, EventArgs e)
        {
            if (Session["userid"] != null)
            {
                bool isCartItemUpdated = false;
                int check = isItemExistsInCart(Convert.ToInt32(Session["ProductID"]));
                if (check == 0)
                {
                    con = new SqlConnection(Connection.GetConnection());
                    cmd = new SqlCommand("CartCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@ProductID", Session["ProductID"]);
                    cmd.Parameters.AddWithValue("@Quentity", txtQuantity.Text.Trim());
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
                    isCartItemUpdated = utils.UpdateCartQuentity(check + 1, Convert.ToInt32(Session["ProductID"]), Convert.ToInt32(Session["userid"]));
                }
                lblMsg.Text = "Item added successfully in your cart !";
                lblMsg.Visible = true;
                lblMsg.CssClass = "alert alert-success";
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



