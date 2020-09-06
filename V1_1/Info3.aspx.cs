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
    public partial class Info3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var userm = manager.FindById(User.Identity.GetUserId());

            if (userm == null)
            {
                Response.Redirect("~/Account/Login");
                return;
            }
            if (!IsPostBack)
            {
                Label1.Text = userm.UserName;
                Label2.Text = P.I.Get_name_otdel(userm.info2);
                LabelU.Text = userm.Id;
                Calendar1.SelectedDate = DateTime.Now;
                Calendar2.SelectedDate = DateTime.Now;

                if (P.I.to_int(userm.info10) > 10)
                {

                    Panel1.Visible = false;
                    Panel2.Visible = true;

                }
                else
                {
                    return;
                }


            }

        }
       
        void html4()
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=info4.html");

            Response.ContentType = "application/octet-stream";

            StringBuilder sBuilder = new System.Text.StringBuilder();


            sBuilder.Append("<html " +
            " lang = \"ru\" ><head><meta http - equiv = \"Content-Type\" content = \"text/html; charset=utf-8\" /><meta charset = \"utf-8\" /><meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />");
            sBuilder.Append("<title>Сбор данных о работе отделений СЗГУ</title>");
            sBuilder.Append("<body> ");

            sBuilder.Append("<style>.textmode{}</style><div>");

            sBuilder.Append("<big><big>Сбор данных о работе отделений СЗГУ</big></big>");
            sBuilder.Append("<BR/>");
            sBuilder.Append("<big>Период с "+Calendar1.SelectedDate.ToLongDateString()+ " по "+ Calendar2.SelectedDate.ToLongDateString() +" </big>");
            sBuilder.Append("<BR/>");
            sBuilder.Append("<BR/>");

            sBuilder.Append("<table cellspacing=\"0\" rules =\"all\" border = \"1\" id = \"MainContent_GridView1\" style=\"border-collapse:collapse;\" >");

            //sBuilder.Append("<tr style=\"background-color:White;\" >");
            ////  sBuilder.Append("<th>Отдел</th><th>ФИО</th><th>Дата</th><th>Учебный курс</th><th>Вебинар</th>");
            //sBuilder.Append("<th>Отдел</th><th>ФИО</th><th>Учебный курс</th><th>Вебинар</th>");
            //sBuilder.Append(" </tr>");


           
            string dt_str = " dt_usr BETWEEN "+P.I.to_sql_dt(Calendar1.SelectedDate,false) +" and "+ P.I.to_sql_dt(Calendar2.SelectedDate, true)+" ";

            try
            {
                using (SqlV sqlv = new SqlV("select count(distinct usr) as cnt FROM Info2 where (isnull(p5,0)='0' or isnull(p7,0)='1') and " + dt_str))
                {
                    if (sqlv.reader.HasRows)
                    {
                        if (sqlv.reader.Read())
                        {
                            sBuilder.Append("<tr style=\"background-color:White;\" >");
                            sBuilder.Append("<th>Количество работников, привлеченных к работе в офисе в отчетный период</th><th  style=\"width: 100px\">" + sqlv.reader["cnt"].ToString() + "</th>");
                            sBuilder.Append(" </tr>");
                        }
                    }
                }
                using (SqlV sqlv = new SqlV("select count(*) as cnt FROM Info2 where (isnull(p5,0)='0' or isnull(p7,0)='1') and " + dt_str))
                {
                    if (sqlv.reader.HasRows)
                    {
                        if (sqlv.reader.Read())
                        {
                            sBuilder.Append("<tr style=\"background-color:White;\" >");
                            sBuilder.Append("<th>Количество человеко-дней (выходов) работников в офис в отчетный период</th><th  style=\"width: 100px\">" + sqlv.reader["cnt"].ToString() + "</th>");
                            sBuilder.Append(" </tr>");
                        }
                    }
                }
                using (SqlV sqlv = new SqlV("select count(distinct usr) as cnt FROM Info2 where isnull(p5,0)='1' and isnull(p7,0)='0' and isnull(p6,0)='1' and " + dt_str))
                {
                    if (sqlv.reader.HasRows)
                    {
                        if (sqlv.reader.Read())
                        {
                            sBuilder.Append("<tr style=\"background-color:White;\" >");
                            sBuilder.Append("<th>Количество работников, фактически привлекаемых к работе дистанционно за отчетный период</th><th  style=\"width: 100px\">" + sqlv.reader["cnt"].ToString() + "</th>");
                            sBuilder.Append(" </tr>");
                        }
                    }
                }
                using (SqlV sqlv = new SqlV("select  count(distinct usr) as cnt FROM Info1 where diag='Болен'  and " + dt_str))
                {
                    if (sqlv.reader.HasRows)
                    {
                        if (sqlv.reader.Read())
                        {
                            sBuilder.Append("<tr style=\"background-color:White;\" >");
                            sBuilder.Append("<th>Количество временно нетрудоспособных работников:</th><th  style=\"width: 100px\">" + sqlv.reader["cnt"].ToString() + "</th>");
                            sBuilder.Append(" </tr>");
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                sBuilder.Append(ex.ToString());
            }



            sBuilder.Append("</table></div>");
            sBuilder.Append("</body> </html>");


            Response.Output.Write(sBuilder.ToString());
            Response.Flush();
            Response.End();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            html4();

        }
    }
}