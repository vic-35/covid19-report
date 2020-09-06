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
    public partial class Contact : Page
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

                if (P.I.to_int(userm.info10) > 10)
                {
                    Calendar1.SelectedDate = DateTime.Now;
                    Panel1.Visible = false;
                    Panel2.Visible = true;

                    if (P.I.to_int(userm.info10) < 50)
                    {
                        DropDownList1.SelectedValue = userm.info2;
                        DropDownList1.Enabled = false;
                        if(P.I.to_int(userm.info10) > 20)
                        {
                            Panel4.Visible = true;
                        }
                    }
                    else
                    {
                        Panel3.Visible = true;
                        Panel4.Visible = true;
                    }



                }
                else
                {
                    return;
                }


            }


        }

        int num_col_prn1 = 1;
        string prn1_b(string otdel)
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();

            try
            {

                sBuilder.Append(P.I.Get_name_otdel(otdel));
                sBuilder.Append("\r\n");
                using (SqlV sqlv = new SqlV("select usr,diag,msg,CONVERT ( char,dt_usr, 103) as dt_usr,diag1,p1 FROM Info1  WHERE id_otdel = " +
                     otdel + " and dt_usr = " + P.I.to_sql_dt(Calendar1.SelectedDate, false) + " order by usr"))
                {

                    if (sqlv.reader.HasRows)
                    {
                        while (sqlv.reader.Read())
                        {
                            sBuilder.Append((num_col_prn1++).ToString());
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["usr"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["diag"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["diag1"])));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["dt_usr"].ToString().Replace(";", "").Replace("/", "."));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["msg"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p1"].ToString().Replace(";", ""))));
                            sBuilder.Append("\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sBuilder.Append(ex.ToString());
            }

            return sBuilder.ToString();
        }
        void prn1(bool windows, string otdel)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=info1.csv");

            if (windows) Response.ContentEncoding = Encoding.GetEncoding("windows-1251");

            Response.ContentType = "application/octet-stream";

            StringBuilder sBuilder = new System.Text.StringBuilder();

            sBuilder.Append("Ежедневная анкета сотрудника о здоровье");
            sBuilder.Append("\r\n");
            sBuilder.Append("\r\n");

            sBuilder.Append("Отдел;ФИО;Состояние;Не ОРВИ;Дата;Имел контакт с зараженным;Состояние здоровья членов семьи - наличие заболевших ОРВИ");
            sBuilder.Append("\r\n");

            num_col_prn1 = 1;

            if (otdel == "all")
            {
                try
                {
                    using (SqlV sqlv = new SqlV("select id,name FROM spr_otdel"))
                    {
                        if (sqlv.reader.HasRows)
                        {
                            while (sqlv.reader.Read())
                            {
                                sBuilder.Append(prn1_b(sqlv.reader["id"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    sBuilder.Append(ex.ToString());
                }
            }
            else
            {
                sBuilder.Append(prn1_b(otdel));
            }




            if (windows)
            {
                Encoding utf8 = Encoding.UTF8;
                Encoding win1251 = Encoding.GetEncoding("windows-1251");
                byte[] utf8Bytes = utf8.GetBytes(sBuilder.ToString());
                byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
                Response.Output.Write(win1251.GetString(win1251Bytes), win1251);
            }
            else
            {
                Response.Output.Write(sBuilder.ToString());
            }

            Response.Flush();
            Response.End();

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            prn1(true, DropDownList1.SelectedValue);

        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            prn2(true, DropDownList1.SelectedValue);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            html1(true,DropDownList1.SelectedValue);
        }        
        
        protected void Button9_Click(object sender, EventArgs e)
        {
            html1(false, DropDownList1.SelectedValue);
        }


        protected void Button11_Click(object sender, EventArgs e)
        {
           
            html3(false, DropDownList1.SelectedValue);
        }


        protected void Button7_Click(object sender, EventArgs e)
        {
            prn1(true, "all");
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
            html1(true,"all");
        }
        protected void Button10_Click(object sender, EventArgs e)
        {

            html1(false, "all");

        }

        int num_col_prn2 = 1;
        string prn2_b(string otdel)
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();
            try
            {
                sBuilder.Append(P.I.Get_name_otdel(otdel));
                sBuilder.Append("\r\n");


                using (SqlV sqlv = new SqlV("select usr,msg,CONVERT ( char,dt_usr, 103) as dt_usr,p1,p2,p3_1,p3_2,p3_3,p3_4,p3_5,p3_6,p3_7,p3_8,p3_9,p3_10,p3_1_txt,p3_2_txt,p3_3_txt,p3_4_txt,p3_5_txt,p3_6_txt,p3_7_txt,p3_8_txt,p3_9_txt,p3_10_txt,p4,p5,p6,p7 FROM Info2  WHERE id_otdel = " + otdel + " and dt_usr = " + P.I.to_sql_dt(Calendar1.SelectedDate, false) + " order by usr"))
                {

                    if (sqlv.reader.HasRows)
                    {
                        while (sqlv.reader.Read())
                        {
                            sBuilder.Append((num_col_prn2++).ToString());
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["usr"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["dt_usr"].ToString().Replace(";", "").Replace("/", "."));
                            sBuilder.Append(";");

                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p5"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p7"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p6"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            //sBuilder.Append(sqlv.reader["p1"].ToString().Replace(";", ""));
                            //sBuilder.Append(";");
                            //sBuilder.Append(sqlv.reader["p2"].ToString().Replace(";", ""));
                            //sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p3_1"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["p3_1_txt"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p3_2"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["p3_2_txt"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p3_3"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["p3_3_txt"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p3_4"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["p3_4_txt"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p3_5"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["p3_5_txt"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p3_6"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["p3_6_txt"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p3_7"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["p3_7_txt"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p3_8"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["p3_8_txt"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p3_9"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["p3_9_txt"].ToString().Replace(";", ""));
                            sBuilder.Append(";");
                            sBuilder.Append(P.I.yesno(P.I.to_int(sqlv.reader["p3_10"].ToString().Replace(";", ""))));
                            sBuilder.Append(";");
                            sBuilder.Append(sqlv.reader["p3_10_txt"].ToString().Replace(";", ""));
                            sBuilder.Append(";");



                            //sBuilder.Append(sqlv.reader["p4"].ToString().Replace(";", ""));
                            //sBuilder.Append(";");


                            sBuilder.Append(sqlv.reader["msg"].ToString().Replace(";", ""));
                            sBuilder.Append("\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sBuilder.Append(ex.ToString());
            }
            return sBuilder.ToString();
        }
        void prn2(bool windows, string otdel)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=info2.csv");

            if (windows) Response.ContentEncoding = Encoding.GetEncoding("windows-1251");
            Response.ContentType = "application/octet-stream";

            StringBuilder sBuilder = new System.Text.StringBuilder();

            sBuilder.Append("Ежедневная анкета сотрудника об удаленной работе");
            sBuilder.Append("\r\n");
            sBuilder.Append("Отдел;ФИО;Дата;Удаленно;Привлекались к работе в офисе;Выполняли задачи удаленной работы;" +
                "Прошел(прошла) дистанционный курс;;Принял(а) участие в вебинаре;;Изучил(а) нормативные акты по профессии;;Проводил(а)сбор и анализ данных;;" +
                "Мониторинг лучших практик;;Работы по заданию руководства;;Работал в удаленной команде по направлению;;Организовывал работу удаленных групп/команд;;" +
                "Выполнение задач в рамках ф-й подразделений;;Мероприятия относящиеся к саморазвитию;;Дополнительно");
            sBuilder.Append("\r\n");
            //sBuilder.Append("Отделение Вологда");
            //sBuilder.Append("\r\n");

            num_col_prn2 = 1;

            if (otdel == "all")
            {
                try
                {
                    using (SqlV sqlv = new SqlV("select id,name FROM spr_otdel"))
                    {
                        if (sqlv.reader.HasRows)
                        {
                            while (sqlv.reader.Read())
                            {
                                sBuilder.Append(prn2_b(sqlv.reader["id"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    sBuilder.Append(ex.ToString());
                }
            }
            else
            {
                sBuilder.Append(prn2_b(otdel));
            }



            if (windows)
            {
                Encoding utf8 = Encoding.UTF8;
                Encoding win1251 = Encoding.GetEncoding("windows-1251");
                byte[] utf8Bytes = utf8.GetBytes(sBuilder.ToString());
                byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
                Response.Output.Write(win1251.GetString(win1251Bytes), win1251);

            }
            else
            {
                Response.Output.Write(sBuilder.ToString());
            }




            Response.Flush();
            Response.End();

        }



        protected void Button4_Click(object sender, EventArgs e)
        {
            html2(true, DropDownList1.SelectedValue);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            prn2(true, "all");
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            html2(true, "all");
        }
         protected void Button13_Click(object sender, EventArgs e)
        {
            html2(false, "all");
        }     
        protected void Button12_Click(object sender, EventArgs e)
        {
            html2(false, DropDownList1.SelectedValue);
        }

        string html1_b(string otdel)
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();

            try
            {

                sBuilder.Append("<tr><td class=\"textmode\" colspan=\"7\">" + P.I.Get_name_otdel(otdel) + " </td></tr>");

                using (SqlV sqlv = new SqlV("select usr,diag,msg,CONVERT ( char,dt_usr, 103) as dt_usr,diag1,p1 FROM Info1  WHERE id_otdel = " +
                     otdel + " and dt_usr = " + P.I.to_sql_dt(Calendar1.SelectedDate, false) + " order by usr"))
                {

                    if (sqlv.reader.HasRows)
                    {
                        while (sqlv.reader.Read())
                        {
                            sBuilder.Append("<tr>");
                            sBuilder.Append("<td class=\"textmode\">"+    (num_col_prn1++).ToString() +"</td>");
                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["usr"].ToString().Replace(";", "") + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["diag"].ToString().Replace(";", "") + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["diag1"])) + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["dt_usr"].ToString().Replace(";", "").Replace("/", ".") + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["msg"].ToString().Replace(";", "") + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["p1"].ToString().Replace(";", ""))) + "</td>");
                            
                            
                            sBuilder.Append(" </tr>");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sBuilder.Append(ex.ToString());
            }

            return sBuilder.ToString();
        }
        void html1(bool xls,string otdel)
        {

            Response.Clear();
            Response.Buffer = true;

            if (xls) Response.AddHeader("content-disposition", "attachment;filename=info1.xls");
            else Response.AddHeader("content-disposition", "attachment;filename=info1.html");

            Response.ContentType = "application/octet-stream";

            StringBuilder sBuilder = new System.Text.StringBuilder();


            sBuilder.Append("<html " +
            " lang = \"ru\" ><head><meta http - equiv = \"Content-Type\" content = \"text/html; charset=utf-8\" /><meta charset = \"utf-8\" /><meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />");
            sBuilder.Append("<title>Ежедневная анкета сотрудника о здоровье</title>");
            sBuilder.Append("<body> ");

            sBuilder.Append("<style>.textmode{}</style><div>");

            sBuilder.Append("<big><big>Ежедневная анкета сотрудника о здоровье</big></big>");
            sBuilder.Append("<BR/>");


            sBuilder.Append("<table cellspacing=\"0\" rules =\"all\" border = \"1\" id = \"MainContent_GridView1\" style=\"border-collapse:collapse;\" >");

            sBuilder.Append("<tr style=\"background-color:White;\" >");
            sBuilder.Append("<th>Отдел</th><th>ФИО</th><th>Состояние</th><th>Не ОРВИ</th><th>Дата</th><th>Имел контакт с зараженным</th><th>Состояние здоровья членов семьи - наличие заболевших ОРВИ</th>");
            sBuilder.Append(" </tr>");

            num_col_prn1 = 1;

            if (otdel == "all")
            {
                try
                {
                    using (SqlV sqlv = new SqlV("select id,name FROM spr_otdel"))
                    {
                        if (sqlv.reader.HasRows)
                        {
                            while (sqlv.reader.Read())
                            {
                                sBuilder.Append(html1_b(sqlv.reader["id"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    sBuilder.Append("\r\n"+ex.ToString());
                }
            }
            else
            {
                sBuilder.Append(html1_b(otdel));
            }

           sBuilder.Append("</table></div>");
           sBuilder.Append("</body> </html>");


            Response.Output.Write(sBuilder.ToString());
            Response.Flush();
            Response.End();

        }


        string html2_b(string otdel)
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();

            try
            {

                sBuilder.Append("<tr><td class=\"textmode\" colspan=\"27\">" + P.I.Get_name_otdel(otdel) + " </td></tr>");
                using (SqlV sqlv = new SqlV("select usr,msg,CONVERT ( char,dt_usr, 103) as dt_usr,p1,p2,p3_1,p3_2,p3_3,p3_4,p3_5,p3_6,p3_7,p3_8,p3_9,p3_10,p3_1_txt,p3_2_txt,p3_3_txt,p3_4_txt,p3_5_txt,p3_6_txt,p3_7_txt,p3_8_txt,p3_9_txt,p3_10_txt,p4,p5,p6,p7 FROM Info2  WHERE id_otdel = " + otdel + " and dt_usr = " + P.I.to_sql_dt(Calendar1.SelectedDate, false) + " order by usr"))
                {

                    if (sqlv.reader.HasRows)
                    {
                        while (sqlv.reader.Read())
                        {

                            sBuilder.Append("<tr>");
                            sBuilder.Append("<td class=\"textmode\">" + (num_col_prn2++).ToString() + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["usr"].ToString().Replace(";", "") + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["dt_usr"].ToString().Replace(";", "").Replace("/", ".") + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["p5"].ToString().Replace(";", ""))) + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["p7"].ToString().Replace(";", ""))) + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["p6"].ToString().Replace(";", ""))) + "</td>");

                            {
                                int p = P.I.to_int(sqlv.reader["p3_1"].ToString().Replace(";", ""));
                                string txt =       sqlv.reader["p3_1_txt"].ToString().Replace(";", "");
                                if (txt.Length == 0)
                                {
                                    sBuilder.Append("<td class=\"textmode\" colspan=\"2\" >" + P.I.yesno(p) + "</td>");
                                }
                                else
                                {
                                    sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                                    sBuilder.Append("<td style=\"background-color:LightGreen;\"  class=\"textmode\"  >" + txt + "</td>");
                                }
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_2"].ToString().Replace(";", ""));
                                string txt =       sqlv.reader["p3_2_txt"].ToString().Replace(";", "");
                                if (txt.Length == 0)
                                {
                                    sBuilder.Append("<td class=\"textmode\" colspan=\"2\" >" + P.I.yesno(p) + "</td>");
                                }
                                else
                                {
                                    sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                                    sBuilder.Append("<td style=\"background-color:LightGreen;\" class=\"textmode\">" + txt + "</td>");
                                }
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_3"].ToString().Replace(";", ""));
                                string txt =       sqlv.reader["p3_3_txt"].ToString().Replace(";", "");
                                if (txt.Length == 0)
                                {
                                    sBuilder.Append("<td class=\"textmode\" colspan=\"2\" >" + P.I.yesno(p) + "</td>");
                                }
                                else
                                {
                                    sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                                    sBuilder.Append("<td style=\"background-color:LightGreen;\"  class=\"textmode\">" + txt + "</td>");
                                }
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_4"].ToString().Replace(";", ""));
                                string txt =       sqlv.reader["p3_4_txt"].ToString().Replace(";", "");
                                if (txt.Length == 0)
                                {
                                    sBuilder.Append("<td class=\"textmode\" colspan=\"2\" >" + P.I.yesno(p) + "</td>");
                                }
                                else
                                {
                                    sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                                    sBuilder.Append("<td style=\"background-color:LightGreen;\"  class=\"textmode\">" + txt + "</td>");
                                }
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_5"].ToString().Replace(";", ""));
                                string txt =       sqlv.reader["p3_5_txt"].ToString().Replace(";", "");
                                if (txt.Length == 0)
                                {
                                    sBuilder.Append("<td class=\"textmode\" colspan=\"2\" >" + P.I.yesno(p) + "</td>");
                                }
                                else
                                {
                                    sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                                    sBuilder.Append("<td style=\"background-color:LightGreen;\"  class=\"textmode\">" + txt + "</td>");
                                }
                            }
                            
                            {
                                int p = P.I.to_int(sqlv.reader["p3_6"].ToString().Replace(";", ""));
                                string txt =       sqlv.reader["p3_6_txt"].ToString().Replace(";", "");
                                if (txt.Length == 0)
                                {
                                    sBuilder.Append("<td class=\"textmode\" colspan=\"2\" >" + P.I.yesno(p) + "</td>");
                                }
                                else
                                {
                                    sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                                    sBuilder.Append("<td style=\"background-color:LightGreen;\"  class=\"textmode\">" + txt + "</td>");
                                }
                            }
                            
                            {
                                int p = P.I.to_int(sqlv.reader["p3_7"].ToString().Replace(";", ""));
                                string txt =       sqlv.reader["p3_7_txt"].ToString().Replace(";", "");
                                if (txt.Length == 0)
                                {
                                    sBuilder.Append("<td class=\"textmode\" colspan=\"2\" >" + P.I.yesno(p) + "</td>");
                                }
                                else
                                {
                                    sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                                    sBuilder.Append("<td style=\"background-color:LightGreen;\"  class=\"textmode\">" + txt + "</td>");
                                }
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_8"].ToString().Replace(";", ""));
                                string txt =       sqlv.reader["p3_8_txt"].ToString().Replace(";", "");
                                if (txt.Length == 0)
                                {
                                    sBuilder.Append("<td class=\"textmode\" colspan=\"2\" >" + P.I.yesno(p) + "</td>");
                                }
                                else
                                {
                                    sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                                    sBuilder.Append("<td style=\"background-color:LightGreen;\"  class=\"textmode\">" + txt + "</td>");
                                }
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_9"].ToString().Replace(";", ""));
                                string txt =       sqlv.reader["p3_9_txt"].ToString().Replace(";", "");
                                if (txt.Length == 0)
                                {
                                    sBuilder.Append("<td class=\"textmode\" colspan=\"2\" >" + P.I.yesno(p) + "</td>");
                                }
                                else
                                {
                                    sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                                    sBuilder.Append("<td style=\"background-color:LightGreen;\"  class=\"textmode\">" + txt + "</td>");
                                }
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_10"].ToString().Replace(";", ""));
                                string txt =       sqlv.reader["p3_10_txt"].ToString().Replace(";", "");
                                if (txt.Length == 0)
                                {
                                    sBuilder.Append("<td class=\"textmode\" colspan=\"2\" >" + P.I.yesno(p) + "</td>");
                                }
                                else
                                {
                                    sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                                    sBuilder.Append("<td style=\"background-color:LightGreen;\"  class=\"textmode\">" + txt + "</td>");
                                }
                            }

                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["msg"].ToString().Replace(";", "") + "</td>");


                            sBuilder.Append(" </tr>");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sBuilder.Append(ex.ToString());
            }

            return sBuilder.ToString();
        }
        void html2(bool xls, string otdel)
        {

            Response.Clear();
            Response.Buffer = true;

            if (xls) Response.AddHeader("content-disposition", "attachment;filename=info2.xls");
            else Response.AddHeader("content-disposition", "attachment;filename=info2.html");

            Response.ContentType = "application/octet-stream";

            StringBuilder sBuilder = new System.Text.StringBuilder();


            sBuilder.Append("<html " +
            " lang = \"ru\" ><head><meta http - equiv = \"Content-Type\" content = \"text/html; charset=utf-8\" /><meta charset = \"utf-8\" /><meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />");
            sBuilder.Append("<title>Ежедневная анкета сотрудника об удаленной работе</title>");
            sBuilder.Append("<body> ");

            sBuilder.Append("<style>.textmode{}</style><div>");

            sBuilder.Append("<big><big>Ежедневная анкета сотрудника об удаленной работе</big></big>");
            sBuilder.Append("<BR/>");

            sBuilder.Append("<table cellspacing=\"0\" rules =\"all\" border = \"1\" id = \"MainContent_GridView1\" style=\"border-collapse:collapse;\" >");

            sBuilder.Append("<tr style=\"background-color:White;\" >");
            sBuilder.Append("<th>Отдел</th><th>ФИО</th><th>Дата</th><th>Удаленно</th><th>Привлекались к работе в офисе</th><th  colspan=\"1\">Выполняли задачи удаленной работы</th><th colspan=\"2\">Прошел(прошла) дистанционный курс</th>" +
                "<th  colspan=\"2\">Принял(а) участие в вебинаре</th><th colspan=\"2\">Изучил(а) нормативные акты по профессии</th><th colspan=\"2\">Проводил(а)сбор и анализ данных</th>" +
                "<th colspan=\"2\">Мониторинг лучших практик</th><th colspan=\"2\">Работы по заданию руководства</th><th colspan=\"2\">Работал в удаленной команде по направлению</th>" +
                "<th colspan=\"2\">Организовывал работу удаленных групп/команд</th><th colspan=\"2\">Выполнение задач в рамках ф-й подразделений</th><th colspan=\"2\">Мероприятия относящиеся к саморазвитию</th><th>Дополнительно</th>");
            sBuilder.Append(" </tr>");



            num_col_prn2 = 1;

            if (otdel == "all")
            {
                try
                {
                    using (SqlV sqlv = new SqlV("select id,name FROM spr_otdel"))
                    {
                        if (sqlv.reader.HasRows)
                        {
                            while (sqlv.reader.Read())
                            {
                                sBuilder.Append(html2_b(sqlv.reader["id"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    sBuilder.Append(ex.ToString());
                }
            }
            else
            {
                sBuilder.Append(html2_b(otdel));
            }


            sBuilder.Append("</table></div>");
            sBuilder.Append("</body> </html>");


            Response.Output.Write(sBuilder.ToString());
            Response.Flush();
            Response.End();

        }

        string html3_b_1(int p,string txt,string txt1)
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();

            if (p > 0)
            {
                sBuilder.Append("<tr>");
                sBuilder.Append("<td class=\"textmode\">" + "</td>");
                sBuilder.Append("<td class=\"textmode\">" + "</td>");
                sBuilder.Append("<td  colspan=\"5\" class=\"textmode\"  >" + txt1 + "</td>");
                sBuilder.Append("</tr>");

                sBuilder.Append("<tr>");
                sBuilder.Append("<td class=\"textmode\">" + "</td>");
                sBuilder.Append("<td class=\"textmode\">" + "</td>");
                //sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(p) + "</td>");
                sBuilder.Append("<td style=\"background-color:LightGreen;\" colspan=\"5\" class=\"textmode\"  >" + txt + "</td>");
                sBuilder.Append("</tr>");
            }
            return sBuilder.ToString();
        }
        string html3_b(string otdel)
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();

            try
            {

                sBuilder.Append("<tr><td class=\"textmode\" colspan=\"7\">" + P.I.Get_name_otdel(otdel) + " </td></tr>");
                using (SqlV sqlv = new SqlV("select usr,msg,CONVERT ( char,dt_usr, 103) as dt_usr,p1,p2,p3_1,p3_2,p3_3,p3_4,p3_5,p3_6,p3_7,p3_8,p3_9,p3_10,p3_1_txt,p3_2_txt,p3_3_txt,p3_4_txt,p3_5_txt,p3_6_txt,p3_7_txt,p3_8_txt,p3_9_txt,p3_10_txt,p4,p5,p6,p7 FROM Info2  WHERE id_otdel = " + otdel + " and dt_usr = " + P.I.to_sql_dt(Calendar1.SelectedDate, false) + " order by usr"))
                {
                    if (sqlv.reader.HasRows)
                    {
                        while (sqlv.reader.Read())
                        {

                            sBuilder.Append("<tr>");
                            sBuilder.Append("<td class=\"textmode\">" + (num_col_prn2++).ToString() + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["usr"].ToString().Replace(";", "") + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["dt_usr"].ToString().Replace(";", "").Replace("/", ".") + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["p5"].ToString().Replace(";", ""))) + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["p7"].ToString().Replace(";", ""))) + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["p6"].ToString().Replace(";", ""))) + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["msg"].ToString().Replace(";", "") + "</td>");
                            sBuilder.Append(" </tr>");





                            {

                                int p = P.I.to_int(sqlv.reader["p3_1"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_1_txt"].ToString().Replace(";", "");

                                sBuilder.Append(html3_b_1(p, txt, "Прошел(прошла) дистанционный курс"));

                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_2"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_2_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html3_b_1(p, txt, "Принял(а) участие в вебинаре"));
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_3"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_3_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html3_b_1(p, txt, "Изучил(а) нормативные акты по профессии"));
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_4"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_4_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html3_b_1(p, txt, "Проводил(а)сбор и анализ данных"));
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_5"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_5_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html3_b_1(p, txt, "Мониторинг лучших практик"));
                            }

                            {
                                int p = P.I.to_int(sqlv.reader["p3_6"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_6_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html3_b_1(p, txt, "Работы по заданию руководства"));
                            }

                            {

                                int p = P.I.to_int(sqlv.reader["p3_7"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_7_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html3_b_1(p, txt, "Работал в удаленной команде по направлению"));
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_8"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_8_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html3_b_1(p, txt, "Организовывал работу удаленных групп/команд"));
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_9"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_9_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html3_b_1(p, txt, "Выполнение задач в рамках ф-й подразделений"));
                            }
                            {

                                int p = P.I.to_int(sqlv.reader["p3_10"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_10_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html3_b_1(p, txt, "Мероприятия относящиеся к саморазвитию"));
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sBuilder.Append(ex.ToString());
            }

            return sBuilder.ToString();
        }
        void html3(bool xls, string otdel)
        {

            Response.Clear();
            Response.Buffer = true;

            if (xls) Response.AddHeader("content-disposition", "attachment;filename=info3.xls");
            else Response.AddHeader("content-disposition", "attachment;filename=info3.html");

            Response.ContentType = "application/octet-stream";

            StringBuilder sBuilder = new System.Text.StringBuilder();


            sBuilder.Append("<html " +
            " lang = \"ru\" ><head><meta http - equiv = \"Content-Type\" content = \"text/html; charset=utf-8\" /><meta charset = \"utf-8\" /><meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />");
            sBuilder.Append("<title>Ежедневная анкета сотрудника об удаленной работе</title>");
            sBuilder.Append("<body> ");

            sBuilder.Append("<style>.textmode{}</style><div>");

            sBuilder.Append("<big><big>Ежедневная анкета сотрудника об удаленной работе</big></big>");
            sBuilder.Append("<BR/>");

            sBuilder.Append("<table cellspacing=\"0\" rules =\"all\" border = \"1\" id = \"MainContent_GridView1\" style=\"border-collapse:collapse;\" >");

            sBuilder.Append("<tr style=\"background-color:White;\" >");
            sBuilder.Append("<th>Отдел</th><th>ФИО</th><th>Дата</th><th>Удаленно</th></th><th>Выход в офис</th><th  colspan=\"1\">Задачи у.р.</th><th>Дополнительно</th>");
            sBuilder.Append(" </tr>");



            num_col_prn2 = 1;

            if (otdel == "all")
            {
                try
                {
                    using (SqlV sqlv = new SqlV("select id,name FROM spr_otdel"))
                    {
                        if (sqlv.reader.HasRows)
                        {
                            while (sqlv.reader.Read())
                            {
                                sBuilder.Append(html3_b(sqlv.reader["id"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    sBuilder.Append(ex.ToString());
                }
            }
            else
            {
                sBuilder.Append(html3_b(otdel));
            }


            sBuilder.Append("</table></div>");
            sBuilder.Append("</body> </html>");


            Response.Output.Write(sBuilder.ToString());
            Response.Flush();
            Response.End();

        }

        string html4_b(string otdel)
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();

            try
            {
                sBuilder.Append("<tr><td class=\"textmode\" colspan=\"4\">" + P.I.Get_name_otdel(otdel) + " </td></tr>");

                using (SqlV sqlv1 = new SqlV("select distinct id_usr,usr  FROM Info2  WHERE id_otdel = '" + otdel + "' order by usr "))
                {
                    if (sqlv1.reader.HasRows)
                    {
                        while (sqlv1.reader.Read())
                        {

                            sBuilder.Append("<tr>");
                            sBuilder.Append("<td class=\"textmode\"></td>");
                            sBuilder.Append("<td class=\"textmode\" style=\"background-color:GhostWhite;\" colspan=\"3\" >" + sqlv1.reader["usr"].ToString() + "</td>");
                            sBuilder.Append(" </tr>");

                            using (SqlV sqlv = new SqlV("select CONVERT ( char,dt_usr, 103) as dt_usr1,p3_1,p3_2,p3_1_txt,p3_2_txt FROM Info2  WHERE id_otdel= '"+ otdel + "'  and id_usr = '" + sqlv1.reader["id_usr"].ToString() + "' and ( p3_1 = '1' OR p3_2 = '1')  order by dt_usr"))
                            {
                                if (sqlv.reader.HasRows)
                                {
                                    while (sqlv.reader.Read())
                                    {

                                        sBuilder.Append("<tr>");
                                        //sBuilder.Append("<td class=\"textmode\">" + (num_col_prn2++).ToString() + "</td>");
                                        sBuilder.Append("<td class=\"textmode\"></td>");
                                        sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["dt_usr1"].ToString().Replace(";", "").Replace("/", ".") + "</td>");

                                        
                                        {

                                            int p = P.I.to_int(sqlv.reader["p3_1"].ToString().Replace(";", ""));
                                            string txt = sqlv.reader["p3_1_txt"].ToString().Replace(";", "");

                                                sBuilder.Append("<td class=\"textmode\">" + txt + "</td>");



                                        }
                                        {
                                            int p = P.I.to_int(sqlv.reader["p3_2"].ToString().Replace(";", ""));
                                            string txt = sqlv.reader["p3_2_txt"].ToString().Replace(";", "");

                                                sBuilder.Append("<td class=\"textmode\">" + txt + "</td>");

                                        }








                                        sBuilder.Append(" </tr>");

                                        //                {

                                        //                    int p = P.I.to_int(sqlv.reader["p3_1"].ToString().Replace(";", ""));
                                        //                    string txt = dt_usr.Replace(";", "");

                                        //                    sBuilder.Append(html3_b_1(p, txt, "Прошел(прошла) дистанционный курс"));

                                        //                }
                                        //                {
                                        //                    int p = P.I.to_int(sqlv.reader["p3_2"].ToString().Replace(";", ""));
                                        //                    string txt = sqlv.reader["p3_2_txt"].ToString().Replace(";", "");
                                        //                    sBuilder.Append(html3_b_1(p, txt, "Принял(а) участие в вебинаре"));
                                        //                }
                                        //                {
                                        //                    int p = P.I.to_int(sqlv.reader["p3_3"].ToString().Replace(";", ""));
                                        //                    string txt = sqlv.reader["p3_3_txt"].ToString().Replace(";", "");
                                        //                    sBuilder.Append(html3_b_1(p, txt, "Изучил(а) нормативные акты по профессии"));
                                        //                }
                                        //                {
                                        //                    int p = P.I.to_int(sqlv.reader["p3_4"].ToString().Replace(";", ""));
                                        //                    string txt = sqlv.reader["p3_4_txt"].ToString().Replace(";", "");
                                        //                    sBuilder.Append(html3_b_1(p, txt, "Проводил(а)сбор и анализ данных"));
                                        //                }
                                        //                {
                                        //                    int p = P.I.to_int(sqlv.reader["p3_5"].ToString().Replace(";", ""));
                                        //                    string txt = sqlv.reader["p3_5_txt"].ToString().Replace(";", "");
                                        //                    sBuilder.Append(html3_b_1(p, txt, "Мониторинг лучших практик"));
                                        //                }

                                        //                {
                                        //                    int p = P.I.to_int(sqlv.reader["p3_6"].ToString().Replace(";", ""));
                                        //                    string txt = sqlv.reader["p3_6_txt"].ToString().Replace(";", "");
                                        //                    sBuilder.Append(html3_b_1(p, txt, "Работы по заданию руководства"));
                                        //                }

                                        //                {

                                        //                    int p = P.I.to_int(sqlv.reader["p3_7"].ToString().Replace(";", ""));
                                        //                    string txt = sqlv.reader["p3_7_txt"].ToString().Replace(";", "");
                                        //                    sBuilder.Append(html3_b_1(p, txt, "Работал в удаленной команде по направлению"));
                                        //                }
                                        //                {
                                        //                    int p = P.I.to_int(sqlv.reader["p3_8"].ToString().Replace(";", ""));
                                        //                    string txt = sqlv.reader["p3_8_txt"].ToString().Replace(";", "");
                                        //                    sBuilder.Append(html3_b_1(p, txt, "Организовывал работу удаленных групп/команд"));
                                        //                }
                                        //                {
                                        //                    int p = P.I.to_int(sqlv.reader["p3_9"].ToString().Replace(";", ""));
                                        //                    string txt = sqlv.reader["p3_9_txt"].ToString().Replace(";", "");
                                        //                    sBuilder.Append(html3_b_1(p, txt, "Выполнение задач в рамках ф-й подразделений"));
                                        //                }
                                        //                {

                                        //                    int p = P.I.to_int(sqlv.reader["p3_10"].ToString().Replace(";", ""));
                                        //                    string txt = sqlv.reader["p3_10_txt"].ToString().Replace(";", "");
                                        //                    sBuilder.Append(html3_b_1(p, txt, "Мероприятия относящиеся к саморазвитию"));
                                        //                }

                                        //            }
                                        //        }
                                    }
                                }


                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                sBuilder.Append(ex.ToString());
            }

            return sBuilder.ToString();
        }

        void html4(string otdel)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=info4.html");

            Response.ContentType = "application/octet-stream";

            StringBuilder sBuilder = new System.Text.StringBuilder();


            sBuilder.Append("<html " +
            " lang = \"ru\" ><head><meta http - equiv = \"Content-Type\" content = \"text/html; charset=utf-8\" /><meta charset = \"utf-8\" /><meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />");
            sBuilder.Append("<title>Учебный курсы и вебинары сотрудников</title>");
            sBuilder.Append("<body> ");

            sBuilder.Append("<style>.textmode{}</style><div>");

            sBuilder.Append("<big><big>Учебный курсы и вебинары сотрудников</big></big>");
            sBuilder.Append("<BR/>");

            sBuilder.Append("<table cellspacing=\"0\" rules =\"all\" border = \"1\" id = \"MainContent_GridView1\" style=\"border-collapse:collapse;\" >");

            sBuilder.Append("<tr style=\"background-color:White;\" >");
          //  sBuilder.Append("<th>Отдел</th><th>ФИО</th><th>Дата</th><th>Учебный курс</th><th>Вебинар</th>");
            sBuilder.Append("<th>Отдел</th><th>ФИО</th><th>Учебный курс</th><th>Вебинар</th>");
            sBuilder.Append(" </tr>");


            num_col_prn2 = 1;

            if (otdel == "all")
            {
                try
                {
                    using (SqlV sqlv = new SqlV("select id,name FROM spr_otdel"))
                    {
                        if (sqlv.reader.HasRows)
                        {
                            while (sqlv.reader.Read())
                            {
                                sBuilder.Append(html4_b(sqlv.reader["id"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    sBuilder.Append(ex.ToString());
                }
            }
            else
            {
                sBuilder.Append(html4_b(otdel));
            }


            sBuilder.Append("</table></div>");
            sBuilder.Append("</body> </html>");


            Response.Output.Write(sBuilder.ToString());
            Response.Flush();
            Response.End();

        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            html4(DropDownList1.SelectedValue);
        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            html4("all");
        }

        protected void Button3_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/info3");
        }

        protected void Button4_Click1(object sender, EventArgs e)
        {
            html5(DropDownList1.SelectedValue);

        }
        string html5_b_1(int p, string txt, string txt1)
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();

            if (p > 0)
            {

                sBuilder.Append(txt1 + ": ");
                sBuilder.Append( txt );
                sBuilder.Append("<BR/>");

            }
            return sBuilder.ToString();
        }
        string html5_b(string otdel)
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();

            try
            {
                using (SqlV sqlv = new SqlV("select usr,msg,CONVERT ( char,dt_usr, 103) as dt_usr,p1,p2,p3_1,p3_2,p3_3,p3_4,p3_5,p3_6,p3_7,p3_8,p3_9,p3_10,p3_1_txt,p3_2_txt,p3_3_txt,p3_4_txt,p3_5_txt,p3_6_txt,p3_7_txt,p3_8_txt,p3_9_txt,p3_10_txt,p4,p5,p6,p7 FROM Info2  WHERE id_otdel = " + otdel + " and dt_usr = " + P.I.to_sql_dt(Calendar1.SelectedDate, false) + " and ( p5='1' or p7='1' ) order by usr"))
                {
                    if (sqlv.reader.HasRows)
                    {
                        while (sqlv.reader.Read())
                        {


                            sBuilder.Append("<tr>");
                            sBuilder.Append("<td class=\"textmode\">" + (num_col_prn2++).ToString() + "</td>");
                            sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["usr"].ToString().Replace(";", "") + "</td>");
                            //sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["dt_usr"].ToString().Replace(";", "").Replace("/", ".") + "</td>");
                            //sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["p5"].ToString().Replace(";", ""))) + "</td>");
                            //sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["p7"].ToString().Replace(";", ""))) + "</td>");
                            //sBuilder.Append("<td class=\"textmode\">" + P.I.yesno(P.I.to_int(sqlv.reader["p6"].ToString().Replace(";", ""))) + "</td>");
                            //sBuilder.Append("<td class=\"textmode\">" + sqlv.reader["msg"].ToString().Replace(";", "") + "</td>");






                            sBuilder.Append("<td>");
                            //{

                            //    int p = P.I.to_int(sqlv.reader["p6"].ToString().Replace(";", ""));
                            //    string txt = P.I.yesno(p);
                            //    sBuilder.Append(html5_b_1(p, txt, "Выполнял задачи удаленной работы"));

                            //}

                            {

                                int p = P.I.to_int(sqlv.reader["p3_1"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_1_txt"].ToString().Replace(";", "");

                                sBuilder.Append(html5_b_1(p, txt, "Прошел(прошла) дистанционный курс"));

                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_2"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_2_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html5_b_1(p, txt, "Принял(а) участие в вебинаре"));
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_3"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_3_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html5_b_1(p, txt, "Изучил(а) нормативные акты по профессии"));
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_4"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_4_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html5_b_1(p, txt, "Проводил(а)сбор и анализ данных"));
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_5"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_5_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html5_b_1(p, txt, "Мониторинг лучших практик"));
                            }

                            {
                                int p = P.I.to_int(sqlv.reader["p3_6"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_6_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html5_b_1(p, txt, "Работы по заданию руководства"));
                            }

                            {

                                int p = P.I.to_int(sqlv.reader["p3_7"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_7_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html5_b_1(p, txt, "Работал в удаленной команде по направлению"));
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_8"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_8_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html5_b_1(p, txt, "Организовывал работу удаленных групп/команд"));
                            }
                            {
                                int p = P.I.to_int(sqlv.reader["p3_9"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_9_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html5_b_1(p, txt, "Выполнение задач в рамках ф-й подразделений"));
                            }
                            {

                                int p = P.I.to_int(sqlv.reader["p3_10"].ToString().Replace(";", ""));
                                string txt = sqlv.reader["p3_10_txt"].ToString().Replace(";", "");
                                sBuilder.Append(html5_b_1(p, txt, "Мероприятия относящиеся к саморазвитию"));
                            }
                            {

                                string txt = sqlv.reader["msg"].ToString().Replace(";", "").Trim();
                                int p = txt.Length;
                                sBuilder.Append(html5_b_1(p, txt, "Дополнительно"));
                            }
                            sBuilder.Append(" </td>"); 
                            sBuilder.Append("</tr>");


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sBuilder.Append(ex.ToString());
            }

            return sBuilder.ToString();
        }
        void html5(string otdel)
        {

            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment;filename=info3.html");

            Response.ContentType = "application/octet-stream";

            StringBuilder sBuilder = new System.Text.StringBuilder();


            sBuilder.Append("<html " +
            " lang = \"ru\" ><head><meta http - equiv = \"Content-Type\" content = \"text/html; charset=utf-8\" /><meta charset = \"utf-8\" /><meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />");
            sBuilder.Append("<title>Отчет о дистанционной работе</title>");
            sBuilder.Append("<body> ");

            sBuilder.Append("<style>.textmode{}</style><div>");

            sBuilder.Append("<big><big>Отчет о дистанционной работе "+P.I.Get_name_otdel(otdel) +" за " + Calendar1.SelectedDate.ToShortDateString() + "</big></big>");
            sBuilder.Append("<BR/>");
            sBuilder.Append("<BR/>");


            sBuilder.Append("<table cellspacing=\"0\" rules =\"all\" border = \"1\" id = \"MainContent_GridView1\" style=\"border-collapse:collapse;\" >");

            sBuilder.Append("<tr style=\"background-color:White;\" >");
            sBuilder.Append("<th>№</th><th>ФИО</th><th>Итоги выполнения задач, обозначенных руководителем/саморазвитие</th>");
            sBuilder.Append(" </tr>");



            num_col_prn2 = 1;

            //if (otdel == "all")
            //{
            //    try
            //    {
            //        using (SqlV sqlv = new SqlV("select id,name FROM spr_otdel"))
            //        {
            //            if (sqlv.reader.HasRows)
            //            {
            //                while (sqlv.reader.Read())
            //                {
            //                    sBuilder.Append(html5_b(sqlv.reader["id"].ToString()));
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        sBuilder.Append(ex.ToString());
            //    }
            //}
            //else
            {
                sBuilder.Append(html5_b(otdel));
            }


            sBuilder.Append("</table></div>");
            sBuilder.Append("</body> </html>");


            Response.Output.Write(sBuilder.ToString());
            Response.Flush();
            Response.End();

        }


    }


}