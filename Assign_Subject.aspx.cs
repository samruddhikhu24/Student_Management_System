using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attendence_System
{
    public partial class TeacherSubject : Page
    {
        string str = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownTeacherShow();
                DropDownCourseShow();
                ShowTeacherSubject();
            }
        }

        private void DropDownTeacherShow()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT TeacherID,TeacherName FROM Teacher", con);

                DropDownList1.DataSource = cmd.ExecuteReader();
                DropDownList1.DataTextField = "TeacherName";
                DropDownList1.DataValueField = "TeacherID";
                DropDownList1.DataBind();

                DropDownList1.Items.Insert(0, new ListItem("Select Teacher", "0"));
            }
        }

        private void DropDownCourseShow()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT CID,CourseName FROM Course", con);

                DropDownList2.DataSource = cmd.ExecuteReader();
                DropDownList2.DataTextField = "CourseName";
                DropDownList2.DataValueField = "CID";
                DropDownList2.DataBind();

                DropDownList2.Items.Insert(0, new ListItem("Select Course", "0"));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();

                if (HiddenField1.Value == "")
                {
                    SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO TeacherSubject
            (Teacher,Course,Year,Sem,Subject)

            VALUES

            (@Teacher,@Course,@Year,@Sem,@Subject)", con);

                    cmd.Parameters.AddWithValue("@Teacher", DropDownList1.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Course", DropDownList2.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Year", DropDownList3.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Sem", DropDownList5.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Subject", DropDownList4.SelectedItem.Text);

                    cmd.ExecuteNonQuery();

                    Label1.Text = "Saved Successfully";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(
                    @"UPDATE TeacherSubject SET

            Teacher=@Teacher,
            Course=@Course,
            Year=@Year,
            Sem=@Sem,
            Subject=@Subject

            WHERE ID=@ID", con);

                    cmd.Parameters.AddWithValue("@Teacher", DropDownList1.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Course", DropDownList2.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Year", DropDownList3.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Sem", DropDownList5.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Subject", DropDownList4.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@ID", HiddenField1.Value);

                    cmd.ExecuteNonQuery();

                    HiddenField1.Value = "";

                    Button1.Text = "Save";

                    Label1.Text = "Updated Successfully";
                }
            }

            ShowTeacherSubject();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList5.Items.Clear();

            DropDownList5.Items.Add(new ListItem("Select Semester", "0"));

            switch (DropDownList3.SelectedValue)
            {
                case "1":
                    DropDownList5.Items.Add(new ListItem("Sem I", "1"));
                    DropDownList5.Items.Add(new ListItem("Sem II", "2"));
                    break;

                case "2":
                    DropDownList5.Items.Add(new ListItem("Sem III", "3"));
                    DropDownList5.Items.Add(new ListItem("Sem IV", "4"));
                    break;

                case "3":
                    DropDownList5.Items.Add(new ListItem("Sem V", "5"));
                    DropDownList5.Items.Add(new ListItem("Sem VI", "6"));
                    break;
            }
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedValue == "0")
                return;

            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                @"SELECT SID,SubjectName
                  FROM Subject
                  WHERE CID=@CID
                  AND Year=@Year
                  AND Sem=@Sem", con);

                cmd.Parameters.AddWithValue("@CID", DropDownList2.SelectedValue);
                cmd.Parameters.AddWithValue("@Year", DropDownList3.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Sem", DropDownList5.SelectedItem.Text);

                DropDownList4.DataSource = cmd.ExecuteReader();
                DropDownList4.DataTextField = "SubjectName";
                DropDownList4.DataValueField = "SID";
                DropDownList4.DataBind();

                DropDownList4.Items.Insert(0, new ListItem("Select Subject", "0"));
                DropDownList4.Items.Insert(0, new ListItem(".NET ", "1"));

                DropDownList4.Items.Insert(0, new ListItem("java", "2"));

                DropDownList4.Items.Insert(0, new ListItem("javascript", "3"));

                DropDownList4.Items.Insert(0, new ListItem("python", "4"));

            }
        }

        private void ShowTeacherSubject()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT
            ID,
            Teacher,
            Course,
            Year,
            Sem,
            Subject
        FROM TeacherSubject", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SqlConnection con = new SqlConnection(str);
            con.Open();

            if (e.CommandName == "EditRecord")
            {
                HiddenField1.Value = e.CommandArgument.ToString();

                SqlCommand cmd = new SqlCommand(
                "SELECT * FROM TeacherSubject WHERE ID=@ID", con);

                cmd.Parameters.AddWithValue("@ID", HiddenField1.Value);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    DropDownList1.Items.FindByText(dr["Teacher"].ToString()).Selected = true;

                    DropDownList2.Items.FindByText(dr["Course"].ToString()).Selected = true;

                    DropDownList3.Items.FindByText(dr["Year"].ToString()).Selected = true;

                    DropDownList3_SelectedIndexChanged(null, null);

                    DropDownList5.Items.FindByText(dr["Sem"].ToString()).Selected = true;

                    DropDownList5_SelectedIndexChanged(null, null);

                    DropDownList4.Items.FindByText(dr["Subject"].ToString()).Selected = true;

                    Button1.Text = "Update";
                }

                dr.Close();
            }

            if (e.CommandName == "DeleteRecord")
            {
                SqlCommand cmd = new SqlCommand(
                "DELETE FROM TeacherSubject WHERE ID=@ID", con);

                cmd.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());

                cmd.ExecuteNonQuery();

                ShowTeacherSubject();

                Label1.Text = "Deleted Successfully";
            }

            con.Close();
        }
        private void Clear()
        {
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList5.SelectedIndex = 0;
            DropDownList4.SelectedIndex = 0;

            HiddenField1.Value = "";

            Button1.Text = "Save";
        }

    }
}