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
    public partial class Info2 : System.Web.UI.Page
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

            if(!IsPostBack)
            {

                DropDownList1.Items.Add("Нет, работал только дистанционно");
                DropDownList1.Items.Add("Да");

                DropDownList2.Items.Add("Нет");
                DropDownList2.Items.Add("1");
                DropDownList2.Items.Add("2");
                DropDownList2.Items.Add("3");

                DropDownList3.Items.Add("Не Трудоспособен");
                DropDownList3.Items.Add("Трудоспособен");

                DropDownList4.Items.Add("Да");
                DropDownList4.Items.Add("Нет");

                DropDownList5.Items.Add("Нет");
                DropDownList5.Items.Add("Да");


                Calendar1.SelectedDate = DateTime.Now;
                LabelU.Text = userm.Id;
                Label1.Text = userm.UserName;
                Label2.Text = P.I.Get_name_otdel(userm.info2);

            }
            else
            {
                CheckBox1_txt.Visible = CheckBox1.Checked; if (!CheckBox1_txt.Visible) CheckBox1_txt.Text = "";
                CheckBox2_txt.Visible = CheckBox2.Checked; if (!CheckBox2_txt.Visible) CheckBox2_txt.Text = "";
                CheckBox3_txt.Visible = CheckBox3.Checked; if (!CheckBox3_txt.Visible) CheckBox3_txt.Text = "";
                CheckBox4_txt.Visible = CheckBox4.Checked; if (!CheckBox4_txt.Visible) CheckBox4_txt.Text = "";
                CheckBox5_txt.Visible = CheckBox5.Checked; if (!CheckBox5_txt.Visible) CheckBox5_txt.Text = "";
                CheckBox6_txt.Visible = CheckBox6.Checked; if (!CheckBox6_txt.Visible) CheckBox6_txt.Text = "";
                CheckBox7_txt.Visible = CheckBox7.Checked; if (!CheckBox7_txt.Visible) CheckBox7_txt.Text = "";
                CheckBox8_txt.Visible = CheckBox8.Checked; if (!CheckBox8_txt.Visible) CheckBox8_txt.Text = "";
                CheckBox9_txt.Visible = CheckBox9.Checked; if (!CheckBox9_txt.Visible) CheckBox9_txt.Text = "";
                CheckBox10_txt.Visible = CheckBox10.Checked; if (!CheckBox10_txt.Visible) CheckBox10_txt.Text = "";
            }

            if (DropDownList4.SelectedIndex == 1) Panel4.Visible = true;
            else Panel4.Visible = false;

            if (DropDownList5.SelectedIndex == 1) Panel3.Visible = true;
            else Panel3.Visible = false;



        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            try
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var userm = manager.FindById(User.Identity.GetUserId());


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
                    string sql = "delete from info2 where id_usr='" + userm.Id + "' and  dt_usr = " + P.I.to_sql_dt(Calendar1.SelectedDate, false);

                    sqlv.ExecScalarSql(sql);


                    sql = " INSERT INTO[dbo].[Info2] ([id_otdel],[id_usr],[usr],[msg],[dt],[dt_usr],p1,p2,p3_1,p3_2,p3_3,p3_4,p3_5,p3_6,p3_7,p3_8,p3_9,p3_10,p3_1_txt,p3_2_txt,p3_3_txt,p3_4_txt,p3_5_txt,p3_6_txt,p3_7_txt,p3_8_txt,p3_9_txt,p3_10_txt,p4,p5,p6,p7) VALUES " +
            " ( '" + userm.info2 + "','" + userm.Id + "','" + userm.UserName + "','" + P.I.sqldecor(Msg.Text) + "'," + P.I.to_sql_dt(DateTime.Now) + "," + P.I.to_sql_dt(Calendar1.SelectedDate, false) + 
            ","+DropDownList1.SelectedIndex.ToString()+ "," + DropDownList2.SelectedIndex.ToString()+ ","+
             P.I.bool_int_str(CheckBox1.Checked) + "," +
             P.I.bool_int_str(CheckBox2.Checked) + "," +
             P.I.bool_int_str(CheckBox3.Checked) + "," +
             P.I.bool_int_str(CheckBox4.Checked) + "," +
             P.I.bool_int_str(CheckBox5.Checked) + "," +
             P.I.bool_int_str(CheckBox6.Checked) + "," +
             P.I.bool_int_str(CheckBox7.Checked) + "," +
             P.I.bool_int_str(CheckBox8.Checked) + "," +
             P.I.bool_int_str(CheckBox9.Checked) + "," +
             P.I.bool_int_str(CheckBox10.Checked) + "," +
             "'" + P.I.sqldecor(CheckBox1_txt.Text) + "'," +
             "'" + P.I.sqldecor(CheckBox2_txt.Text) + "'," +
             "'" + P.I.sqldecor(CheckBox3_txt.Text) + "'," +
             "'" + P.I.sqldecor(CheckBox4_txt.Text) + "'," +
             "'" + P.I.sqldecor(CheckBox5_txt.Text) + "'," +
             "'" + P.I.sqldecor(CheckBox6_txt.Text) + "'," +
             "'" + P.I.sqldecor(CheckBox7_txt.Text) + "'," +
             "'" + P.I.sqldecor(CheckBox8_txt.Text) + "'," +
             "'" + P.I.sqldecor(CheckBox9_txt.Text) + "'," +
             "'" + P.I.sqldecor(CheckBox10_txt.Text) + "'," +
             DropDownList3.SelectedIndex.ToString()+","+
             DropDownList4.SelectedIndex.ToString() + "," +
             DropDownList5.SelectedIndex.ToString() + "," +
            DropDownList1.SelectedIndex.ToString()
             + " )";

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
        int num_col_prn2 = 0;
        string html2_b()
        {
            StringBuilder sBuilder = new System.Text.StringBuilder();

            try
            {

                sBuilder.Append("<tr><td class=\"textmode\" colspan=\"27\">" + Label2.Text + " </td></tr>");
                using (SqlV sqlv = new SqlV("select usr,msg,CONVERT ( char,dt_usr, 103) as dt_usr,p1,p2,p3_1,p3_2,p3_3,p3_4,p3_5,p3_6,p3_7,p3_8,p3_9,p3_10,p3_1_txt,p3_2_txt,p3_3_txt,p3_4_txt,p3_5_txt,p3_6_txt,p3_7_txt,p3_8_txt,p3_9_txt,p3_10_txt,p4,p5,p6,p7 FROM Info2  WHERE id_usr = '" + LabelU.Text + "' order by dt_usr desc "))
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
                                string txt = sqlv.reader["p3_1_txt"].ToString().Replace(";", "");
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
                                string txt = sqlv.reader["p3_2_txt"].ToString().Replace(";", "");
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
                                string txt = sqlv.reader["p3_3_txt"].ToString().Replace(";", "");
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
                                string txt = sqlv.reader["p3_4_txt"].ToString().Replace(";", "");
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
                                string txt = sqlv.reader["p3_5_txt"].ToString().Replace(";", "");
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
                                string txt = sqlv.reader["p3_6_txt"].ToString().Replace(";", "");
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
                                string txt = sqlv.reader["p3_7_txt"].ToString().Replace(";", "");
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
                                string txt = sqlv.reader["p3_8_txt"].ToString().Replace(";", "");
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
                                string txt = sqlv.reader["p3_9_txt"].ToString().Replace(";", "");
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
                                string txt = sqlv.reader["p3_10_txt"].ToString().Replace(";", "");
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
        void html2()
        {

            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment;filename=info2.html");

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


           sBuilder.Append(html2_b());



            sBuilder.Append("</table></div>");
            sBuilder.Append("</body> </html>");


            Response.Output.Write(sBuilder.ToString());
            Response.Flush();
            Response.End();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            html2();
        }
    }
}