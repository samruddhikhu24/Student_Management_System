using System;
using System.Web.UI;

namespace Attendence_System
{
    public partial class Admin : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Course_Click(object sender, EventArgs e)
        {
            Response.Redirect("Course.aspx");
        }

        protected void Subject_Click(object sender, EventArgs e)
        {
            Response.Redirect("Subject.aspx");
        }

        protected void Teacher_Click(object sender, EventArgs e)
        {
            Response.Redirect("Teacher.aspx");
        }

        protected void AssignSubject_Click(object sender, EventArgs e)
        {
            Response.Redirect("Assign_Subject.aspx");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}