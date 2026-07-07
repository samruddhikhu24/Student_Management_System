using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Web.UI.WebControls;

namespace Attendence_System
{
    public partial class Subject : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DrpDwnCourse();
                ShowStudent();
                    DropDownList1.Items.Clear();
                    DropDownList1.Items.Add(new ListItem("Select Semester", "0"));
                    DropDownList1.Items.Add(new ListItem("Sem I", "1"));
                    DropDownList1.Items.Add(new ListItem("Sem II", "2"));
                    DropDownList1.Items.Add(new ListItem("Sem III", "3"));
                    DropDownList1.Items.Add(new ListItem("Sem IV", "4"));
                    DropDownList1.Items.Add(new ListItem("Sem V", "5"));
                    DropDownList1.Items.Add(new ListItem("Sem VI", "6"));
                }

            }

        

        private void DrpDwnCourse()
        {
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Course", con);
            DropDownList2.DataSource = cmd.ExecuteReader();
            DropDownList2.DataTextField = "CourseName";
            DropDownList2.DataValueField = "CID";
            DropDownList2.DataBind();
            con.Close();
            DropDownList2.Items.Insert(0, "Select Course");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(str);

            con.Open();

            string fullname = TextBox1.Text;


            if (HiddenField1.Value == "")
            {
                SqlCommand cmd = new SqlCommand(
                @"insert into Student
        (SName,Roll,Email,Phone,Course,Year,Sem)

        values

        (@Name,@Roll,@Email,@Phone,@Course,@Year,@Sem)", con);

                cmd.Parameters.AddWithValue("@Name", fullname);
                cmd.Parameters.AddWithValue("@Roll", TextBox4.Text);
                cmd.Parameters.AddWithValue("@Email", TextBox2.Text);
                cmd.Parameters.AddWithValue("@Phone", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Course", DropDownList2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Year", DropDownList3.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Sem", DropDownList1.SelectedItem.Text);

                cmd.ExecuteNonQuery();

                Label1.Text = "Student Added Successfully";
            }
            else
            {
                SqlCommand cmd = new SqlCommand(
                @"update Student set

        SName=@Name,
        Roll=@Roll,
        Email=@Email,
        Phone=@Phone,
        Course=@Course,
        Year=@Year,
        Sem=@Sem

        where SID=@SID", con);

                cmd.Parameters.AddWithValue("@SID", HiddenField1.Value);
                cmd.Parameters.AddWithValue("@Name", fullname);
                cmd.Parameters.AddWithValue("@Roll", TextBox4.Text);
                cmd.Parameters.AddWithValue("@Email", TextBox2.Text);
                cmd.Parameters.AddWithValue("@Phone", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Course", DropDownList2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Year", DropDownList3.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Sem", DropDownList1.SelectedItem.Text);

                cmd.ExecuteNonQuery();

                HiddenField1.Value = "";

                Button1.Text = "Save";

                Label1.Text = "Student Updated Successfully";
            }

            con.Close();

            Clear();

            ShowStudent();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(DropDownList3.SelectedValue) > 0)
            {
                DataTable statedt = new DataTable();
                statedt.Columns.Add("SemId", typeof(int));
                statedt.Columns.Add("SemName");
                if (DropDownList3.SelectedValue == "1")
                {
                    statedt.Rows.Add(0, "Select Semester");
                    statedt.Rows.Add(1, "Sem I");
                    statedt.Rows.Add(2, "Sem II");
                }
                if (DropDownList3.SelectedValue == "2")
                {
                    statedt.Rows.Add(0, "Select Semester");
                    statedt.Rows.Add(3, "Sem III");
                    statedt.Rows.Add(4, "Sem IV");
                }
                if (DropDownList3.SelectedValue == "3")
                {
                    statedt.Rows.Add(0, "Select Semester");
                    statedt.Rows.Add(5, "Sem V");
                    statedt.Rows.Add(6, "Sem VI");
                }

                DropDownList1.DataSource = statedt;
                DropDownList1.DataTextField = "SemName";
                DropDownList1.DataValueField = "SemId";
                DropDownList1.DataBind();
                if (DropDownList3.SelectedValue == "0")
                {
                    Label1.Text = "Please select proper Semester field.";
                }
            }
        }
        private void ShowStudent()
        {
            SqlConnection con = new SqlConnection(str);

            SqlDataAdapter da = new SqlDataAdapter(
            "select * from Student", con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            GridView1.DataSource = dt;

            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = GridView1.Rows[index];

            int SID = Convert.ToInt32(GridView1.DataKeys[index].Value);

            if (e.CommandName == "EditRecord")
            {
                SqlConnection con = new SqlConnection(str);

                SqlDataAdapter da = new SqlDataAdapter(
                "select * from Student where SID=" + SID, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    HiddenField1.Value = SID.ToString();

                    string[] name = dt.Rows[0]["SName"].ToString().Split(' ');

                    TextBox1.Text = name[0];

                    if (name.Length > 1)
                        TextBox5.Text = name[1];

                    TextBox4.Text = dt.Rows[0]["Roll"].ToString();

                    TextBox2.Text = dt.Rows[0]["Email"].ToString();

                    TextBox3.Text = dt.Rows[0]["Phone"].ToString();

                    DropDownList2.SelectedValue =
                        DropDownList2.Items.FindByText(dt.Rows[0]["Course"].ToString()).Value;

                    DropDownList3.SelectedValue =
                        DropDownList3.Items.FindByText(dt.Rows[0]["Year"].ToString()).Value;

                    DropDownList3_SelectedIndexChanged(null, null);

                    DropDownList1.SelectedValue =
                        DropDownList1.Items.FindByText(dt.Rows[0]["Sem"].ToString()).Value;

                    Button1.Text = "Update";
                }
            }

            if (e.CommandName == "DeleteRecord")
            {
                SqlConnection con = new SqlConnection(str);

                con.Open();

                SqlCommand cmd = new SqlCommand(
                "delete from Student where SID=@SID", con);

                cmd.Parameters.AddWithValue("@SID", SID);

                cmd.ExecuteNonQuery();

                con.Close();

                ShowStudent();

                Label1.Text = "Student Deleted Successfully";
            }
        }
        private void Clear()
        {
            TextBox1.Text = "";

            TextBox5.Text = "";

            TextBox4.Text = "";

            TextBox2.Text = "";

            TextBox3.Text = "";

            DropDownList2.SelectedIndex = 0;

            DropDownList3.SelectedIndex = 0;

            DropDownList1.Items.Clear();

            DropDownList1.Items.Add("Select Semester");
        }

    }
}

