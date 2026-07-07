using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attendence_System
{
    public partial class Mark_Attendance : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label1.Text = "";

                DrpCourse();

                Label4.Text = "Today's Date : " +
                              DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            }
        }

        private void DrpCourse()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT CID, CourseName FROM Course", con);

                DropDownList1.DataSource = cmd.ExecuteReader();
                DropDownList1.DataTextField = "CourseName";
                DropDownList1.DataValueField = "CID";
                DropDownList1.DataBind();

                DropDownList1.Items.Insert(0, new ListItem("Select Course", "0"));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "0" ||
                DropDownList2.SelectedIndex == 0 ||
                DropDownList5.SelectedIndex == 0 ||
                DropDownList3.SelectedIndex == 0 ||
                DropDownList4.SelectedIndex == 0)
            {
                Label1.Text = "Please select all the fields.";
                Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(str))
            {
                SqlDataAdapter sda = new SqlDataAdapter(
                @"SELECT SID,
         Roll,
         SName
FROM Student
WHERE Course=@Course
AND Year=@Year
AND Sem=@Sem", con);

                sda.SelectCommand.Parameters.AddWithValue("@Course", DropDownList1.SelectedItem.Text);
                sda.SelectCommand.Parameters.AddWithValue("@Year", DropDownList2.SelectedItem.Text);
                sda.SelectCommand.Parameters.AddWithValue("@Sem", DropDownList5.SelectedItem.Text);

                DataTable dt = new DataTable();
                sda.Fill(dt);
                                GridView1.DataSource = dt;
                GridView1.DataBind();

                if (dt.Rows.Count == 0)
                {
                    Label1.Text = "No students found.";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    Label1.Text = "";
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                int Stud_ID = Convert.ToInt32(row.Cells[1].Text);
                int Roll_No = Convert.ToInt32(row.Cells[2].Text);
                string Stud_Name = row.Cells[3].Text;

                RadioButton rb1 = (RadioButton)row.FindControl("RadioButton1");

                int status = rb1.Checked ? 1 : 0;

                string course = DropDownList1.SelectedItem.Text;
                string year = DropDownList2.SelectedItem.Text;
                string subject = DropDownList3.SelectedItem.Text;
                string sem = DropDownList5.SelectedItem.Text;

                int Total_Lect = Convert.ToInt32(DropDownList4.SelectedItem.Text);
                int TLecture = Total_Lect * status;

                DateTime date = DateTime.Now;

                using (SqlConnection con = new SqlConnection(str))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO StudentAttendance
            (STID, StudentName, Course, Year, Subject, Roll, Status, Date, Lecture, Sem)
            VALUES
            (@STID,@StudentName,@Course,@Year,@Subject,@Roll,@Status,@Date,@Lecture,@Sem)", con);

                    cmd.Parameters.AddWithValue("@STID", Stud_ID);
                    cmd.Parameters.AddWithValue("@StudentName", Stud_Name);
                    cmd.Parameters.AddWithValue("@Course", course);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Subject", subject);
                    cmd.Parameters.AddWithValue("@Roll", Roll_No);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Lecture", TLecture);
                    cmd.Parameters.AddWithValue("@Sem", sem);

                    cmd.ExecuteNonQuery();
                }
            }

            Label3.Text = "Attendance Saved Successfully";
            Label3.ForeColor = System.Drawing.Color.Green;

            if (Session["TeacherID"] != null)
            {
                using (SqlConnection con = new SqlConnection(str))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO T_Lectures
            (TID,TName,Date,LTaken,Course,Year,Subject,Sem)
            VALUES
            (@TID,@TName,@Date,@LTaken,@Course,@Year,@Subject,@Sem)", con);

                    cmd.Parameters.AddWithValue("@TID", Session["TeacherID"]);
                    cmd.Parameters.AddWithValue("@TName", Session["TeacherName"]);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@LTaken", Convert.ToInt32(DropDownList4.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Course", DropDownList1.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Year", DropDownList2.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Subject", DropDownList3.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Sem", DropDownList5.SelectedItem.Text);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check Course
            if (DropDownList1.SelectedIndex == 0)
            {
                Label1.Text = "Please select Course.";
                Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Check Year
            if (DropDownList2.SelectedIndex == 0)
            {
                Label1.Text = "Please select Year.";
                Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Check Semester
            if (DropDownList5.SelectedIndex == 0)
            {
                Label1.Text = "Please select Semester.";
                Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int courseID;

            if (!int.TryParse(DropDownList1.SelectedValue, out courseID))
            {
                Label1.Text = "Invalid Course ID.";
                Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string year = DropDownList2.SelectedItem.Text;
            string sem = DropDownList5.SelectedItem.Text;

            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    @"SELECT SID, SubjectName
              FROM Subject
              WHERE CID = @CID
              AND Year = @Year
              AND Sem = @Sem", con);

                cmd.Parameters.AddWithValue("@CID", courseID);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Sem", sem);

                DropDownList3.DataSource = cmd.ExecuteReader();
                DropDownList3.DataTextField = "SubjectName";
                DropDownList3.DataValueField = "SID";
                DropDownList3.DataBind();

                DropDownList3.Items.Clear();

                DropDownList3.Items.Add(new ListItem("Select Subject", "0"));
                DropDownList3.Items.Add(new ListItem(".NET", "1"));
                DropDownList3.Items.Add(new ListItem("Python", "2"));
                DropDownList3.Items.Add(new ListItem("JavaScript", "3"));



            }

            Label1.Text = "";
        }
    }
    }