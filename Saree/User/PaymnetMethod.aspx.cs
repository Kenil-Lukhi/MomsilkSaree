using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Saree.Common;
using Razorpay.Api;
using System.Threading.Tasks;

namespace Saree.User
{
    public partial class PaymnetMethod : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataReader dr1 = null;
        SqlDataAdapter sda;
        DataTable dt;

        int paymentID2 = 0,
            OrderDetailsID = 0,
            ProductID = 0,
            Quentity = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["BreadCrum"] = "Category";
            if (!IsPostBack)
            {
                if (Session["userid"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            lblMsg.Visible = false;
        }

        protected async void lbCardSubmit_Click(object sender, EventArgs e)
        {
            PaymentDetailsReq req = new PaymentDetailsReq();
            Transtatus transtatus = new Transtatus();

            if (COD.Checked)
            {
                req.PaymentMethod = "Cash on delevery";
                req.Status = "Pending";
            }
            else if (Online.Checked)
            {
                req.PaymentMethod = "Online payment method";
                req.Status = "Approve";
            }
            req.CustomerName = txtName.Text;
            req.Address = txtAdress.Text;
            req.City = txtCity.Text;
            req.State = txtState.Text;
            req.Country = txtCountry.Text;
            req.Postcode = txtPostalCode.Text;
            req.MobileNo = txtMobileNo.Text;
            req.Email = txtEmail.Text;
            req.Amount = Convert.ToInt32(Session["grandTotalPrice"]);
            req.UserID = Convert.ToInt32(Session["userid"]);

            int paymentID = 0;
            (transtatus, paymentID) = await SavePaymentDetails(req);

            (transtatus, OrderDetailsID) = await OrderPlacement(paymentID);
        }

        private async Task<Tuple<Transtatus, int>> SavePaymentDetails(PaymentDetailsReq req)
        {
            Transtatus transtatus = new Transtatus();
            int paymentID = 0;
            con = new SqlConnection(Connection.GetConnection());
            try
            {
                cmd = new SqlCommand("InsertPayment", con);
                cmd.Parameters.AddWithValue("@UserID", req.UserID);
                cmd.Parameters.AddWithValue("@PaymentMethod", req.PaymentMethod);
                cmd.Parameters.AddWithValue("@Amount", req.Amount);
                cmd.Parameters.AddWithValue("@CustomerName", req.CustomerName);
                cmd.Parameters.AddWithValue("@Email", req.Email);
                cmd.Parameters.AddWithValue("@Phone", req.MobileNo);
                cmd.Parameters.AddWithValue("@Address", req.Address);
                cmd.Parameters.AddWithValue("@City", req.City);
                cmd.Parameters.AddWithValue("@State", req.State);
                cmd.Parameters.AddWithValue("@Country", req.Country);
                cmd.Parameters.AddWithValue("@PINCode", req.Postcode);
                cmd.Parameters.AddWithValue("@status", req.Status);
                cmd.Parameters.AddWithValue("@PaymentResJson", req.ApymentJason);
                cmd.Parameters.Add("@Message", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Code", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@PaymentID", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open) await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                transtatus.Message = (string)cmd.Parameters["@Message"].Value;
                transtatus.Code = (int)cmd.Parameters["@Code"].Value;
                paymentID = (int)cmd.Parameters["@PaymentID"].Value;
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                if (con.State == ConnectionState.Open) con.Close();
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return new Tuple<Transtatus, int>(transtatus, paymentID);
        }

        private async Task<Tuple<Transtatus, int>> OrderPlacement(int PaymentID)
        {
            Transtatus transtatus = new Transtatus();
            int OrderID = 0;

            try
            {
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

                cmd = new SqlCommand("CartCrud", con);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    if (con.State != ConnectionState.Open) await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();


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

                         await UpdateQuentity(ProductID, Quentity);

                        await DeleteCartItem(ProductID);

                        dt.Rows.Add(utils.GetUniqueID(), ProductID, Quentity, (int)Session["userid"], "Pending", PaymentID, DateTime.Now);
                    }
                    cmd.Dispose();

                    if (dt.Rows.Count > 0)
                    {
                        cmd = new SqlCommand("SaveOrders", con);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@tblOrders", dt);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State != ConnectionState.Open) await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        cmd.Dispose();
                    }

                    lblMsg.Visible = true;
                    lblMsg.Text = "Your item ordered successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    Response.AddHeader("REFRESH", "1;URL=Invoice.aspx?id=" + PaymentID);

                    if (con.State == ConnectionState.Open) con.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    if (con.State == ConnectionState.Open) con.Close();
                    cmd.Dispose();
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
            }
            catch (Exception ex)
            {
                transtatus.Code = 2;
                transtatus.Message = ex.ToString();
            }

            return new Tuple<Transtatus, int>(transtatus, OrderID);
        }


        private async Task UpdateQuentity(int ProductID,  int Quentity)
        {
            int dbQuentity = 0;
            con = new SqlConnection(Connection.GetConnection());
            try
            {
                if (con.State != ConnectionState.Open) await con.OpenAsync();
                cmd = new SqlCommand("ProductCrude", con);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.CommandType = CommandType.StoredProcedure;

                dr1 = cmd.ExecuteReader();
                while (dr1.Read())
                {
                    dbQuentity = (int)dr1["Quentity"];                    
                }
                dr1.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error : " + ex.Message + "'); </script>");
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            try
            {
                if (dbQuentity > Quentity && dbQuentity > 2)
                {
                    dbQuentity = dbQuentity - Quentity;
                    cmd = new SqlCommand("ProductCrude", con);
                    cmd.Parameters.AddWithValue("@Action", "QTUPDATE");
                    cmd.Parameters.AddWithValue("@Quentity", dbQuentity);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != ConnectionState.Open) await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error : " + ex.Message + "'); </script>");
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private async Task DeleteCartItem(int _productId)
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("CartCrud", con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Action", "DELETE");
            cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
            cmd.Parameters.AddWithValue("@ProductID", _productId);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (con.State != ConnectionState.Open) con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error : " + ex.Message + "'); </script>");
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
    }

    public class PaymentDetailsReq
    {
        public int UserID { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ApymentJason { get; set; }
        public string Status { get; set; }
    }
    public class PaymentMethodRes
    {
        public string PaymentMenthod { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
    }
    public class CustomerDetails
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

}