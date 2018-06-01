using System;
using System.Web;

namespace PostCommInspectionReport
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = HttpContext.Current.User.Identity.Name;
        }
    }
}