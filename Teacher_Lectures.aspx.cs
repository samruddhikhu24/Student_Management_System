using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attendence_System
{
    public partial class Teacher_Lectures : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DrpCourse();
            }
        }

        private void DrpCourse()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Course", con);
                DropDownList1.DataSource = cmd.ExecuteReader();
                DropDownList1.DataTextField = "CourseName";
                DropDownList1.DataValueField = "CID";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "Select Course");
            }
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label1.Text = "";

            int courseID;
            if (!int.TryParse(DropDownList1.SelectedValue, out courseID))
            {
                Label1.Text = "Invalid Course ID";
                return;
            }

            Label1.Text = "CID=" + courseID +
                          " Year=" + DropDownList2.SelectedItem.Text +
                          " Sem=" + DropDownList5.SelectedItem.Text;

            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand(
                @"SELECT SID, SubjectName
          FROM Subject
          WHERE CID=@CID
          AND [Year]=@Year
          AND Sem=@Sem", con);

                cmd.Parameters.AddWithValue("@CID", courseID);
                cmd.Parameters.AddWithValue("@Year", DropDownList2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Sem", DropDownList5.SelectedItem.Text);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                DropDownList3.Items.Clear();

                if (dr.HasRows)
                {
                    DropDownList3.DataSource = dr;
                    DropDownList3.DataTextField = "SubjectName";
                    DropDownList3.DataValueField = "SID";
                    DropDownList3.DataBind();
                }

                DropDownList3.Items.Insert(0, "Select Subject");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM T_Lectures", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                Label1.Text = "Rows Found : " + dt.Rows.Count;
            }
        
            // Consolidated validation
            if (DropDownList1.SelectedValue == "Select Course" ||
                DropDownList2.SelectedValue == "Select Year" ||
                DropDownList5.SelectedValue == "Select Semester" ||
                DropDownList3.SelectedValue == "Select Subject" ||
                string.IsNullOrWhiteSpace(TextBox1.Text))
            {
                Label1.Text = "Please select all the fields.";
                Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string date = TextBox1.Text;
            string course = DropDownList1.SelectedItem.Text;
            string year = DropDownList2.SelectedItem.Text;
            string subject = DropDownList3.SelectedItem.Text;
            string sem = DropDownList5.SelectedItem.Text;

            if (Session["TeacherID"] != null)
            {
                string teacherID = Session["TeacherID"].ToString();

                using (SqlConnection con = new SqlConnection(str))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(@"
SELECT Date AS Lecture_Date,
       LTaken AS Lecture_Taken
FROM T_Lectures
WHERE Course=@Course
AND Year=@Year
AND Sem=@Sem
AND Subject=@Subject
AND CONVERT(date,[Date])=CONVERT(date,@Date)", con);

                    sda.SelectCommand.Parameters.AddWithValue("@Course", DropDownList1.SelectedItem.Text);
                    sda.SelectCommand.Parameters.AddWithValue("@Year", DropDownList2.SelectedItem.Text);
                    sda.SelectCommand.Parameters.AddWithValue("@Sem", DropDownList5.SelectedItem.Text);
                    sda.SelectCommand.Parameters.AddWithValue("@Subject", DropDownList3.SelectedItem.Text);
                    sda.SelectCommand.Parameters.AddWithValue("@Date", Convert.ToDateTime(TextBox1.Text));

                    DataSet ds = new DataSet();
                    sda.Fill(ds, "T_Lectures");

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }
    }
}
