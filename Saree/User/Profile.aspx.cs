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
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
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
                    GetUSerDetails();
                    GetPurchesHistry();
                }
                lblMsg.Visible = false;
            }
        }

        private void GetUSerDetails()
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("UserSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECTFORPROFILE");
            cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rUserProfile.DataSource = dt;
            rUserProfile.DataBind();
            if (dt.Rows.Count == 1)
            {
                Session["PRname"] = dt.Rows[0]["Name"].ToString();
                Session["PRMobile"] = dt.Rows[0]["Mobile"].ToString();
                Session["PREmail"] = dt.Rows[0]["Email"].ToString();
                Session["PRAddress"] = dt.Rows[0]["Address"].ToString();
                Session["imageUrl"] = dt.Rows[0]["ImageURL"].ToString();
                Session["PRcreatedDate"] = dt.Rows[0]["CreatedDate"].ToString();
            }
        }

        private void GetPurchesHistry()
        {
            int sr = 0;
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("Invoice", con);
            cmd.Parameters.AddWithValue("@Action", "ODRHISTORY");
            cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            dt.Columns.Add("SrNo", typeof(Int32));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    sr++;
                    row["SrNo"] = sr;
                }
            }
            if (dt.Rows.Count == 0)
            {
                rPurchesHistory.FooterTemplate = null;
                rPurchesHistory.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            rPurchesHistory.DataSource = dt;
            rPurchesHistory.DataBind();
        }

        protected void rPurchesHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                double grandTotal = 0;
                HiddenField PaymentID = e.Item.FindControl("hdnPayment") as HiddenField;
                Repeater repOrder = e.Item.FindControl("rOrder") as Repeater;

                con = new SqlConnection(Connection.GetConnection());
                cmd = new SqlCommand("Invoice", con);
                cmd.Parameters.AddWithValue("@Action", "OrdersForProfile");
                cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
                cmd.Parameters.AddWithValue("@PaymentID", Convert.ToInt32(PaymentID.Value));
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        grandTotal += Convert.ToDouble(row["TotalPrice"]);
                    }
                }

                DataRow dr = dt.NewRow();
                dr["TotalPrice"] = grandTotal;

                dt.Rows.Add(dr);
                repOrder.DataSource = dt;
                repOrder.DataBind();
            }
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
                    var footer = new LiteralControl("<tr><td><b>hurry up! Why not order saree.</b><a href='Menu.aspx' class='badge badge-info ml-2'>Click to order</a></td><tr></tbody></table>");
                    container.Controls.Add(footer);
                }
            }
        }

        protected async void rOrder_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if(e.CommandName == "CencelOrder")
            {
                    Transtatus transtatus = new Transtatus();
                if(e.CommandArgument != null && (string)e.CommandArgument != "")
                {
                    int OrderDetailsID = Convert.ToInt32(e.CommandArgument);

                    SqlConnection con = new SqlConnection(Connection.GetConnection());
                    try
                    {
                        SqlCommand cmd = new SqlCommand("DeleteOrder", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderDetailsID",OrderDetailsID);
                        cmd.Parameters.Add("@Code", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar,200).Direction = ParameterDirection.Output;

                        if (con.State != ConnectionState.Open) await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        transtatus.Message = (string)cmd.Parameters["@Message"].Value;
                        transtatus.Code = (int)cmd.Parameters["@Code"].Value;
                        if (con.State == ConnectionState.Open) con.Close();
                    }
                    catch (Exception ex)
                    {
                        transtatus.Code = 2;
                        transtatus.Message = "Something went wrong!";
                        if (con.State == ConnectionState.Open) con.Close();
                    }
                    if (con.State == ConnectionState.Open) con.Close();
                }
                if(transtatus.Code == 0)
                {
                    GetUSerDetails();
                    GetPurchesHistry();
                    lblMsg.CssClass = "alert alert-success";
                    lblMsg.Text = transtatus.Message;
                    lblMsg.Visible = true;
                }    
                else
                {
                    lblMsg.CssClass = "alert alert-danger";
                    lblMsg.Text = transtatus.Message;
                    lblMsg.Visible = true;
                }
            }
        }
    }
}