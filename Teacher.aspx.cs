using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attendence_System
{
    public partial class Teacher : Page
    {
        string str = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridShow();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();

                string fullname = TextBox2.Text.Trim() + " " + TextBox4.Text.Trim();

                if (string.IsNullOrEmpty(HiddenField1.Value))
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Teacher(TeacherName,USERNAME,PASSWORD) VALUES(@Name,@User,@Pass)", con);

                    cmd.Parameters.AddWithValue("@Name", fullname);
                    cmd.Parameters.AddWithValue("@User", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Pass", TextBox1.Text.Trim());

                    cmd.ExecuteNonQuery();

                    Label1.ForeColor = System.Drawing.Color.Green;
                    Label1.Text = "Teacher Added Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(
                        @"UPDATE Teacher
                  SET TeacherName=@Name,
                      USERNAME=@User,
                      PASSWORD=@Pass
                  WHERE TeacherID=@ID", con);

                    cmd.Parameters.AddWithValue("@ID", HiddenField1.Value);
                    cmd.Parameters.AddWithValue("@Name", fullname);
                    cmd.Parameters.AddWithValue("@User", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Pass", TextBox1.Text.Trim());

                    cmd.ExecuteNonQuery();

                    Label1.ForeColor = System.Drawing.Color.Green;
                    Label1.Text = "Teacher Updated Successfully.";

                    HiddenField1.Value = "";
                    Button1.Text = "Add Teacher";
                }

                Clear();

                GridShow();
            }
        }

        private void GridShow()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT TeacherID, TeacherName, USERNAME FROM Teacher", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        private void GridShowTeacher()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT TeacherName, USERNAME FROM Teacher", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewTeacher.DataSource = dt;
                GridViewTeacher.DataBind();
            }
        }

        private void GridShowCourse()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT CourseID, CourseName FROM Course", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewCourse.DataSource = dt;
                GridViewCourse.DataBind();
            }
        }

        private void GridShowSubject()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT SubjectID, SubjectName FROM Subject", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewSubject.DataSource = dt;
                GridViewSubject.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditTeacher")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(GridView1.DataKeys[index].Value);

                using (SqlConnection con = new SqlConnection(str))
                {
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM Teacher WHERE TeacherID=@ID", con);

                    da.SelectCommand.Parameters.AddWithValue("@ID", id);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        HiddenField1.Value = id.ToString();

                        string[] name = dt.Rows[0]["TeacherName"].ToString().Split(' ');

                        TextBox2.Text = name[0];

                        if (name.Length > 1)
                            TextBox4.Text = name[1];

                        TextBox3.Text = dt.Rows[0]["USERNAME"].ToString();
                        TextBox1.Attributes["value"] = dt.Rows[0]["PASSWORD"].ToString();

                        Button1.Text = "Update";
                    }
                }
            }

            if (e.CommandName == "DeleteTeacher")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(GridView1.DataKeys[index].Value);

                using (SqlConnection con = new SqlConnection(str))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Teacher WHERE TeacherID=@ID", con);

                    cmd.Parameters.AddWithValue("@ID", id);

                    cmd.ExecuteNonQuery();

                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "Teacher Deleted Successfully.";

                    GridShow();
                }
            }
        }

        private void Clear()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";

            HiddenField1.Value = "";

            Button1.Text = "Add Teacher";
        }
    }
}
