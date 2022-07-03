using System;

namespace advtech.Finance.Accounta
{
    public partial class rentservices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
    }
}