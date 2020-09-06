using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Text;


namespace V1_1
{
    public partial class adm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var userm = manager.FindById(User.Identity.GetUserId());

            if (userm == null)
            {
                Response.Redirect("~/Account/Login");
                //Server.Transfer("~/Account/Login.aspx", true);
                return;
            }
            if (!IsPostBack)
            {
                Label1.Text = userm.UserName;
                Label2.Text = P.I.Get_name_otdel(userm.info2);
                LabelU.Text = userm.Id;

                if (P.I.to_int(userm.info10) > 100)
                {


                }
                else
                {
                    Response.Redirect("~/");
                    return;
                }


            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();
            try
            {                
                using (SqlV sqlv = new SqlV(TextBox1.Text))
                {
                    if (sqlv.reader.HasRows)
                    {
                        while (sqlv.reader.Read())
                        {
                            for (int i = 0; i < sqlv.reader.FieldCount; i++)
                            {
                                sBuilder.Append(sqlv.reader[i].ToString());
                                sBuilder.Append("|");
                            }

                            sBuilder.Append("\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sBuilder.Append(ex.ToString());
            }

            TextBox2.Text = sBuilder.ToString();
        }
    }
}