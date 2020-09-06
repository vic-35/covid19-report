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
    public partial class Info1 : System.Web.UI.Page
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

           if(!IsPostBack)
            {
                DropDownList2.Items.Add("Нет");
                DropDownList2.Items.Add("Да");

                DropDownList3.Items.Add("Здоровы");
                DropDownList3.Items.Add("Имеются заболевшие ОРВИ");


                Label1.Text = userm.UserName;
                Label2.Text = P.I.Get_name_otdel(userm.info2);
                LabelU.Text = userm.Id;
                Calendar1.SelectedDate = DateTime.Now;
            }
            else
            {
                if (DropDownList1.SelectedIndex == 2) CheckBox1.Visible = true;
                else
                {
                    CheckBox1.Visible = false;
                }

            }




        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var userm = manager.FindById(User.Identity.GetUserId());

                if(DropDownList1.SelectedIndex == 0)
                {
                    ErrorMessage.Text = "Введите оценку состояния !";
                    return;
                }
                int dt_cur = P.I.to_int(DateTime.Now.ToString("yyyyMMdd"));
                int dt_otch = P.I.to_int(Calendar1.SelectedDate.ToString("yyyyMMdd"));

                if (userm.info3 != "1")
                {
                    if (dt_cur > dt_otch)
                    {
                        ErrorMessage.Text = "Отчеты за прошедшую дату изменять запрещено !";
                        return;
                    }
                    if (dt_cur == dt_otch)
                    {
                        if (DateTime.Now.Hour > 16)
                        {
                            ErrorMessage.Text = "Отчеты за текущую дату разрешено вводить до 17:00 !";
                            return;
                        }
                    }
                }


                using (SqlV sqlv = new SqlV())
                {
                    string sql= "delete from info1 where id_usr='"+ userm.Id+"' and  dt_usr = "+ P.I.to_sql_dt(Calendar1.SelectedDate, false) ;

                    sqlv.ExecScalarSql(sql);

                    sql = " INSERT INTO[dbo].[Info1] ([id_otdel],[id_usr],[usr],[diag],[msg],[dt],[dt_usr],diag1,p1) VALUES " +
            " ( '" + userm.info2 + "','" + userm.Id + "','" + userm.UserName + "','" + DropDownList1.SelectedValue + "','" +DropDownList2.SelectedValue + "'," + P.I.to_sql_dt(DateTime.Now) + "," +
            P.I.to_sql_dt(Calendar1.SelectedDate, false)+","+ P.I.bool_int_str(CheckBox1.Checked) + ",'"+DropDownList3.SelectedIndex.ToString() + "' )";

                    sqlv.ExecScalarSql(sql);

                }
                Panel1.Visible = false;
                Panel2.Visible = true;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.ToString();
            }


        }
    }
}