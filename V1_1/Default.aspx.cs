using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace V1_1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var userm = manager.FindById(User.Identity.GetUserId());
            if (!IsPostBack)
            {
                if (userm != null)
                {
                    Label1.Text = userm.UserName;
                    Label2.Text = P.I.Get_name_otdel(userm.info2);
                    Panel2.Visible = false;
                    Panel1.Visible = true;

                }
                else
                {
                    Panel1.Visible = false;
                    Panel2.Visible = true;

                }
            }


        }

    }
}