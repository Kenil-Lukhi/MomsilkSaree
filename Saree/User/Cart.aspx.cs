using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saree.Admin;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Saree.User
{
    public partial class Cart : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        decimal GrandTotal = 0;
        utils utils = new utils();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userid"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    GetCart();
                }
                //GetCart(); // after succsessfull payment it was remove
            }
        }

        private void GetCart()
        {

            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("CartCrud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rCartItem.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                rCartItem.FooterTemplate = null;
                rCartItem.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            rCartItem.DataBind();
            Session["cartCount"] = utils.CartCount(Convert.ToInt32(Session["userid"]));
        }
        protected void rCartItem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "remove")
            {
                con = new SqlConnection(Connection.GetConnection());
                cmd = new SqlCommand("CartCrud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
                cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
                cmd.CommandType = CommandType.StoredProcedure;
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
            if (e.CommandName == "UpdateCart")
            {
                bool IsCartUpdate = false;
                for (int item = 0; item < rCartItem.Items.Count; item++)
                {
                    if (rCartItem.Items[item].ItemType == ListItemType.Item || rCartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox Quentity = rCartItem.Items[item].FindControl("txtQuantity") as TextBox;
                        HiddenField _ProductID = rCartItem.Items[item].FindControl("hdnProductID") as HiddenField;
                        HiddenField _quentity = rCartItem.Items[item].FindControl("hdnQuentity") as HiddenField;
                        int quentityFromCart = Convert.ToInt32(Quentity.Text);
                        int ProductID = Convert.ToInt32(_ProductID.Value);
                        int quentityFromDB = Convert.ToInt32(_quentity.Value);
                        bool isTrue = false;
                        int UpdateQuentity = 1;
                        if (quentityFromCart > quentityFromDB)
                        {
                            UpdateQuentity = quentityFromCart;
                            isTrue = true;
                        }
                        else if (quentityFromCart < quentityFromDB)
                        {
                            UpdateQuentity = quentityFromCart;
                            isTrue = true;
                        }

                        if (isTrue)
                        {
                            IsCartUpdate = utils.UpdateCartQuentity(UpdateQuentity, ProductID, Convert.ToInt32(Session["userid"]));
                        }
                    }
                }
            }
            GetCart();
            if(e.CommandName == "checkout")
            {
                bool IsTrue = false;
                string PName = string.Empty;
                for (int item = 0; item < rCartItem.Items.Count; item++)
                {
                    if (rCartItem.Items[item].ItemType == ListItemType.Item || rCartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField _ProductID = rCartItem.Items[item].FindControl("hdnProductID") as HiddenField;
                        HiddenField _CartQuentity = rCartItem.Items[item].FindControl("hdnQuentity") as HiddenField;
                        HiddenField _ProductQuentity = rCartItem.Items[item].FindControl("hdnProductQuentity") as HiddenField;
                        Label ProductName = rCartItem.Items[item].FindControl("lblName") as Label;

                        int ProductID = Convert.ToInt32(_ProductID.Value);
                        int CartQuentity = Convert.ToInt32(_CartQuentity.Value);
                        int ProductQuentity = Convert.ToInt32(_ProductQuentity.Value);
                        if (ProductQuentity > CartQuentity && ProductQuentity > 2)
                        {
                            IsTrue = true;
                        }
                        else 
                        {
                            IsTrue = false;
                            PName = ProductName.Text.ToString();
                            break;
                        }
                    }
                }
                if(IsTrue)
                {
                    Response.Redirect("PaymnetMethod.aspx");
                }
                else
                {
                    lblMsg.Text = "Item <b>'" + PName +"' </b> is out of stock :( ";
                    lblMsg.Visible = true;
                    lblMsg.CssClass = "alert alert-warning";
                }
            }
        }

        protected void rCartItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label totalPrice = e.Item.FindControl("lblTotalPrice") as Label;
                Label productPrice = e.Item.FindControl("lblPrice") as Label;
                TextBox quantity = e.Item.FindControl("txtQuantity") as TextBox;
                decimal calTotalPrice = Convert.ToDecimal(productPrice.Text) * Convert.ToDecimal(quantity.Text);
                totalPrice.Text = calTotalPrice.ToString();
                GrandTotal += calTotalPrice;
            }
            Session["GrandTotalPrice"] = GrandTotal;
        }

        private sealed class CustomTemplate : ITemplate
        {

            private ListItemType ListItemType { get; set; }

            public CustomTemplate(ListItemType type)
            {
                ListItemType = type;
            }
            public void InstantiateIn(Control container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<tr><td colspan='5'><b>Your Cart is empty.</b><a href='Menu.aspx' class='badge badge-info ml-2'>Continue Shopping</a></td><tr></tbody></table>");
                    container.Controls.Add(footer);
                }
            }
        }

        private string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        //private void PaymentMethod()
        //{
        //    string secret = "Your Secreet Key";
        //    string data = "";
        //    string Merchantkey = "Your Merchant Key";
        //    SortedDictionary<string, string> formParams = new SortedDictionary<string, string>();
        //    formParams.Add("appId", Merchantkey);
        //    formParams.Add("orderId", txtOrder.Text);
        //    formParams.Add("orderAmount", txtAmount.Text);
        //    formParams.Add("customerName", txtName.Text);
        //    formParams.Add("customerPhone", txtMobileNumber.Text);
        //    formParams.Add("customerEmail", txtEmail.Text);
        //    formParams.Add("returnUrl", "return URL");
        //    foreach (var kvp in formParams)
        //    {
        //        data = data + kvp.Key + kvp.Value;
        //    }
        //    string signature = CreateToken(data, secret);
        //    Console.Write(signature);
        //    string outputHTML = "<html>";
        //    outputHTML += "<head>";
        //    outputHTML += "<title>Merchant Check Out Page</title>";
        //    outputHTML += "</head>";
        //    outputHTML += "<body>";
        //    outputHTML += "<center>Please do not refresh this page...</center>";  // you can put h1 tag here
        //    outputHTML += "<form id='redirectForm' method='post' action='https://www.gocashfree.com/checkout/post/submit'>";
        //    outputHTML += "<input type='hidden' name='appId' value='" + Merchantkey + "'/>";
        //    outputHTML += "<input type='hidden' name='orderId' value='" + txtOrder.Text + "'/>";
        //    outputHTML += "<input type='hidden' name='orderAmount' value='" + txtAmount.Text + "'/>";
        //    outputHTML += "<input type='hidden' name='customerName' value='" + txtName.Text + "'/>";
        //    outputHTML += "<input type='hidden' name='customerEmail' value='" + txtEmail.Text + "'/>";
        //    outputHTML += "<input type='hidden' name='customerPhone' value='" + txtMobileNumber.Text + "'/>";
        //    outputHTML += "<input type='hidden' name='returnUrl' value='return URL'/>";
        //    outputHTML += "<input type='hidden' name='signature' value='" + signature + "'/>";
        //    outputHTML += "<table border='1'>";
        //    outputHTML += "<tbody>";
        //    foreach (string keys in formParams.Keys)
        //    {
        //        outputHTML += "<input type='hidden' name='" + keys + "' value='" + formParams[keys] + "'>";
        //    }
        //    outputHTML += "</tbody>";
        //    outputHTML += "</table>";
        //    outputHTML += "<script type='text/javascript'>";
        //    outputHTML += "document.getElementById('redirectForm').submit();";
        //    outputHTML += "</script>";
        //    outputHTML += "</form>";
        //    outputHTML += "</body>";
        //    outputHTML += "</html>";
        //    Response.Write(outputHTML);
        //}
    }
}