using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Attendence_System
{
    public partial class Login : Page
    {
        string str = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        protected void Button1_Click(object sender, EventArgs e)
        {
            // ==========================
            // Admin Login
            // ==========================
            if (TextBox1.Text.Trim() == "Admin" &&
                TextBox2.Text.Trim() == "123")
            {
                Session["Admin"] = "Admin";
                Response.Redirect("Admin_Home.aspx");
                return;
            }

            // ==========================
            // Teacher Login
            // ==========================
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Teacher WHERE USERNAME=@USERNAME AND PASSWORD=@PASSWORD", con);

                cmd.Parameters.AddWithValue("@USERNAME", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@PASSWORD", TextBox2.Text.Trim());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Label1.Text = "Login Success";

                    Session["TeacherID"] = dr["TeacherID"].ToString();
                    Session["TeacherName"] = dr["TeacherName"].ToString();

                    Response.Redirect("Teacher_Home.aspx");
                }
                else
                {
                    Label1.Text = "Login Failed";
                }
            }
        }
    }
}