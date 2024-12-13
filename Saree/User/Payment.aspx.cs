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
    public partial class Payment : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataReader dr1 = null;
        SqlDataAdapter sda;
        DataTable dt;
        string _name = string.Empty,
               _cartNO = string.Empty,
               _expiryDate = string.Empty,
               _cvv = string.Empty,
               _address = string.Empty,
               _patmentMode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userid"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

            }
        }

        protected void lbCardSubmit_Click(object sender, EventArgs e)
        {
            _name = txtName.Text.Trim();
            _cartNO = txtCardNo.Text.Trim();
            _cartNO = string.Format("************{0}", txtCardNo.Text.Trim().Substring(12,4));
            _cvv = txtCvv.Text.Trim();
            _address = txtAddress.Text.Trim();
            _patmentMode = "Cart";

            if (Session["userid"] != null)
            {
                OrderPayment(_name, _cartNO, _expiryDate, _cvv, _address, _patmentMode);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void lbCodSubmit_Click(object sender, EventArgs e)
        {
            _address = txtCODAddress.Text.Trim();
            _patmentMode = "COD";

            if (Session["userid"] != null)
            {
                OrderPayment(_name, _cartNO, _expiryDate, _cvv, _address, _patmentMode);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        void OrderPayment(string name, string cardNO, string expiryDate, string cvv, string address, string paymentMode)
        {
            int paymentID = 0,
            ProductID = 0,
            Quentity = 0;

            dt = new DataTable();

            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn("OrderNO", typeof(string)),
                new DataColumn("ProductID", typeof(int)),
                new DataColumn("Quentity", typeof(int)),
                new DataColumn("UserID", typeof(int)),
                new DataColumn("Status", typeof(string)),
                new DataColumn("PaymentID", typeof(int)),
                new DataColumn("OrderDate", typeof(DateTime))
            });

            con = new SqlConnection(Connection.GetConnection());
            con.Open();

            #region Sql Transaction
            cmd = new SqlCommand("SavePayment", con);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@CartNo", cardNO);
            cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
            cmd.Parameters.AddWithValue("@Cvv", cvv);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@PaymentMode", paymentMode);
            cmd.Parameters.AddWithValue("@ChallenCode", 024);
            cmd.Parameters.Add("@InsertedID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.ExecuteNonQuery();
                paymentID = (int)cmd.Parameters["@InsertedID"].Value;

                #region Get Cart Item
                cmd = new SqlCommand("CartCrud", con);
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
                cmd.CommandType = CommandType.StoredProcedure;


                List<Tuple<int, int>> cartItems = new List<Tuple<int, int>>();

                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ProductID = (int)dr["ProductID"];
                        Quentity = (int)dr["Quentity"];
                        cartItems.Add(new Tuple<int, int>(ProductID, Quentity));
                    }
                }
                foreach (var item in cartItems)
                {
                    ProductID = item.Item1;
                    Quentity = item.Item2;

                    // Update Product Quantity
                    UpdateQuentity(ProductID, Quentity, con);

                    // Delete Cart Item
                    DeleteCartItem(ProductID, con);

                    dt.Rows.Add(utils.GetUniqueID(), ProductID, Quentity, (int)Session["userid"], "Pending", paymentID, DateTime.Now);
                }
                #endregion Get Cart Item

                #region Order Details
                if (dt.Rows.Count > 0)
                {
                    cmd = new SqlCommand("SaveOrders", con);
                    cmd.Parameters.AddWithValue("@tblOrders", dt);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                #endregion Order Details

                lblMsg.Visible = true;
                lblMsg.Text = "Your item ordered successfully!";
                lblMsg.CssClass = "alert alert-success";
                Response.AddHeader("REFRESH", "1;URL=Invoice.aspx?id=" + paymentID);
            }
            catch (Exception ex)
            {
               
                
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
            #endregion Sql Transaction
        }

        void UpdateQuentity(int _productId, int _quentity, SqlConnection sqlConnection)
        {
            int dbQuentity;
            cmd = new SqlCommand("ProductCrude", sqlConnection);
            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@ProductID", _productId);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                dr1 = cmd.ExecuteReader();


                while (dr1.Read())
                {
                    dbQuentity = (int)dr1["Quentity"];
                    if (dbQuentity > _quentity && dbQuentity > 2)
                    {
                        dbQuentity = dbQuentity - _quentity;
                        cmd = new SqlCommand("ProductCrude", sqlConnection);
                        cmd.Parameters.AddWithValue("@Action", "QTUPDATE");
                        cmd.Parameters.AddWithValue("@Quentity", dbQuentity);
                        cmd.Parameters.AddWithValue("@ProductID", _productId);
                        cmd.CommandType = CommandType.StoredProcedure;

                    }

                }
                dr1.Close();
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error : " + ex.Message + "'); </script>");
            }
            finally
            {
                //dr1.Close();

                // Close the connection only if it was opened in this method
                sqlConnection.Close();
            }

        }

        void DeleteCartItem(int _productId, SqlConnection sqlConnection)
        {

            cmd = new SqlCommand("CartCrud", sqlConnection);
            cmd.Parameters.AddWithValue("@Action", "DELETE");
            cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
            cmd.Parameters.AddWithValue("@ProductID", _productId);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error : " + ex.Message + "'); </script>");
            }
        }
    }
}