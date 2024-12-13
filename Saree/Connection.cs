using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Saree
{
    public class Connection
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }
    }
    public class utils
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public static bool IsValidExtension(string FileName)
        {
            bool isValid = false;
            string[] FileExtension = { ".jpg", ".png", ".jpeg" };
            for (int i = 0; i < FileExtension.Length; i++)
            {
                if (FileName.Contains(FileExtension[i]))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }


        public static string GetImage(object url)
        {
            string ImageUrl = string.Empty;
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                ImageUrl = "../Images/No_Image.png";
            }
            else
            {
                ImageUrl = string.Format("../{0}", url);
            }
            return ImageUrl;
        }

        public bool UpdateCartQuentity(int Quentity, int ProductID, int UserID)
        {
            bool IsUpdate = false;

            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("CartCrud", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "UPDATE");
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@Quentity", Quentity);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                IsUpdate = true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Error " + ex.Message + "'); </script>");
            }

            return IsUpdate;
        }

        public int CartCount(int userid)
        {
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("CartCrud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt.Rows.Count;
        }

        public static string GetUniqueID()
        {
            Guid guid = Guid.NewGuid();
            string UniqeID = guid.ToString();
            return UniqeID;
        }
    }

    public class DashBoardCount
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;

        public int Count(string name)
        {
            int count = 0;
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("DashBoard", con);
            cmd.Parameters.AddWithValue("@Action", name);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    if (sdr[0] == DBNull.Value)
                    {
                        count = 0;
                    }
                    else
                    {
                        count = Convert.ToInt32(sdr[0]);
                    }
                }
                sdr.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return count;
        }
    }
}
