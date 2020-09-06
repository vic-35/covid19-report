
// Аксенов -----------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net.Sockets;
using System.Collections;
using System.Globalization;
using System.Xml;




/// <summary>
/// Summary description for Util_leadbelly
/// </summary>
public class RdArray
{
    public RdArray( SqlDataReader reader)
    {
        header =new string[reader.FieldCount];
        value = new string[reader.FieldCount];
        for (int i = 0; i < reader.FieldCount; i++)
        {
            header[i] = reader.GetName(i);
            value[i] = reader[i].ToString();
        }
        
    }
    public string this[string name]
    {
        get
        {
            for (int i = 0; i < header.GetLength(0); i++)
            {
                if (header[i].ToLower() == name.ToLower())
                {
                    return value[i];
                }
            }
            throw new SystemException("Error RdArray, not found field- " + name);
        }
    }
    public string[] header;
    public string[] value;

}

public class SqlV : IDisposable
{
    public SqlConnection new_connection = null;
    public SqlDataReader reader = null;

    //string connect_string = "";//ConfigurationManager.ConnectionStrings["rabisConnectionString"].ConnectionString;

    public SqlV()
    {
        string connect_string = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        new_connection = new SqlConnection(connect_string);
        new_connection.Open();
    }
    public SqlV(string conn_name,bool use_connect)
    {
        string connect_string = ConfigurationManager.ConnectionStrings[conn_name].ConnectionString;
        new_connection = new SqlConnection(connect_string);
        new_connection.Open();
    }
    
    public SqlV(string sql)
    {
        string connect_string = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        new_connection = new SqlConnection(connect_string);
        new_connection.Open();
        GetSqlReader(sql);
    }

    public SqlV(string sql, string conn_name, bool use_connect)
    {
        string connect_string = ConfigurationManager.ConnectionStrings[conn_name].ConnectionString;
        new_connection = new SqlConnection(connect_string);
        new_connection.Open();
        GetSqlReader(sql);
    }

    ~SqlV()
    {

    }

    public void Dispose()
    {
        if (reader != null)
        {
            reader.Dispose(); reader = null;
        }
        if (new_connection != null)
        {
            new_connection.Dispose(); new_connection = null;
        }
    }


    public SqlDataReader GetSqlReader(string selectstring)
    {
        SqlCommand select_command = new SqlCommand(selectstring, new_connection);
        if (reader != null)
        {
            if (!reader.IsClosed) reader.Close();
        }
        reader = select_command.ExecuteReader();
        return reader;
    }
    public object ExecScalarSql(string selectstring)
    {
        SqlCommand select_command = new SqlCommand(selectstring, new_connection);
        object r1 = select_command.ExecuteScalar();
        select_command.Dispose();
        return r1;
    }
    public void ExecSql(string selectstring)
    {
        SqlCommand select_command = new SqlCommand(selectstring, new_connection);
        select_command.ExecuteNonQuery();
        select_command.Dispose();
    }


}

public class Util_l
{
	public Util_l()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //static public bool find_control(Control contr, string id,ref object ret_obj)
    //{
    //    foreach (Control child in contr.Controls)
    //    {
    //        if (child.ID == id) { ret_obj = child; return true; }
    //    }
    //    return false;
    //}

 

    static public string Sl_get(System.Web.UI.Page p, string name)
    {
        //string d = p.Page.Request.Path + name;
        string ret = (string)HttpContext.Current.Session[p.Page.Request.Path + name];
        return ret;
    }
    static public string S_get(string name)
    {
        //string d = p.Page.Request.Path + name;
        string ret = (string)HttpContext.Current.Session[name];
        return ret;
    }

    static public bool Set_Ini(string name, string value)
    {
        try
        {
            using (SqlV sqlv = new SqlV())
            {
                object ii = null;
                try
                {
                    using (SqlV sqlv1 = new SqlV())
                    {
                        ii = sqlv1.ExecScalarSql("select TOP 1 value from Table_parametrs where parametr = '" + name + "'");
                    }

                }
                catch (Exception ex)
                {
                    string s = ex.ToString();
                }

                if (ii == null)
                {
                    sqlv.ExecScalarSql("INSERT INTO Table_parametrs ([parametr], [value]) VALUES ( '" + name + "','" + value + "')");
                    return true;
                }
                else
                {
                    sqlv.ExecScalarSql("UPDATE Table_parametrs SET [value] = '" + value + "' WHERE [parametr] = '" + name + "'");
                }
            }
        }
        catch (Exception ex)
        {
            string s = ex.ToString();
        }
        return false;
    }


    static public string Ini(string name)
    {
        try
        {
           using (SqlV sqlv = new SqlV())
           {
                 return sqlv.ExecScalarSql("select TOP 1 value from Table_parametrs where parametr = '" + name + "'").ToString();
           }
            
        }
        catch(Exception ex)
        {
            string s = ex.ToString();
        }
        return "";
    }
    
    static public string repl_break(object str)
    {
       return  str.ToString().Replace("\n", "<BR>");
    }
    //static public void Error(HttpResponse Response, string str)
    //{
    //    Response.ClearContent();
    //    Response.ClearHeaders();

    //    Response.Write("<HTML> <title>Ошибка !!</title> <body> <b><br>Ошибка !!<br><br>" + str + "</b><br></body></html>");
    //    Response.End();
    //}
    static public bool is_empty(object str)
    {
        if (str.ToString().Length == 0) return false;

        return true;
    }
 
    static public string red_find(object str, object find)
    {
        String ret = str.ToString();
        
        ArrayList f_txt = to_arr(find.ToString());
        foreach (string s in f_txt)
        {
            //<FONT color=#ff0000>%s</FONT>
           StringBuilder buf= new StringBuilder();
           int start_pos = 0;
           int find_pos;
           while ((find_pos = ret.IndexOf(s, start_pos, StringComparison.OrdinalIgnoreCase)) != -1)
           {
               buf.Append(ret, start_pos, find_pos - start_pos);
               buf.Append("<FONT color=#ff0000><B>");
               buf.Append(ret, find_pos, s.Length);
               buf.Append("</B></FONT>");
               start_pos = find_pos + s.Length;
           }
           buf.Append(ret, start_pos,ret.Length - start_pos);

           ret = buf.ToString();
        }
        return ret.Replace("\n", "<BR>");
    }
    static public ArrayList to_arr(string str_f)
    {
        ArrayList f_txt = new ArrayList();
        using (TextReader t = new StringReader(str_f.ToLower().Replace(" ", "\x0A\x0d")))
        {
            String Line;
            while ((Line = t.ReadLine()) != null)
            {
                Line = Line.Trim();
                if (Line.Length > 0)
                {
                    f_txt.Add(Line);
                }
            }
        }
        return f_txt;
    }
    
 

    static public int find_in_arr(ref ArrayList ar, string str)
    {
        str=str.Trim();
        for(int i=0;i < ar.Count;i++)
        {
            string s=ar[i].ToString();
            if (String.Compare(s,0, str,0,s.Length, true) == 0)
            {
                return i;
            }
        }
        return -1;
    }
    static public string nvl(object obj, string is_nvl)
    {
        if (obj == null) return is_nvl;
        return obj.ToString();
    }
    static public string nvl(object obj)
    {
        return nvl(obj, "");
    }
    static public string to_sql_dt(string str)
    {

        try
        {

            IFormatProvider culture = new CultureInfo("ru-RU", true);

            DateTime dt = DateTime.Parse(str, culture);

            return " convert(datetime,'" + dt.ToString("dd.MM.yyyy") + "',104) ";

        }

        catch (Exception ex)
        {

            throw new SystemException("Ошибка преобразования даты - " + str + " " + ex.ToString());

        }

    }
    static public string db_to_int(string str)
    {

        try
        {

           return (Int64.Parse(str)).ToString();

        }

        catch (Exception ex)
        {

            return "-1";

        }

    }

    static public string rt_to_2(string str)
    {

        try
        {

           decimal i = Decimal.Parse(str);

           decimal i1 = i / 100;

           return i1.ToString("N2");


        }

        catch (Exception ex)
        {

            return "";

        }

    }

    static public Int32 db_to_int32(string str)
    {

        try
        {

            return Int32.Parse(str);

        }

        catch (Exception ex)
        {

            return -1;

        }

    }
    static public string q_to_intstr(string str)
    {
        try
        {
            StringBuilder s = new StringBuilder();

            foreach (char c in str)
            {
               if( c >= 0x30 && c <= 0x39)
                s.Append(c);
            }
            return s.ToString();
        }
        catch (Exception ex)
        {
        }
        return "";
    }

    static public string db_to_str(string str)
    {
        try
        {
            str = str.Replace("'", "''");
            return str;
        }
        catch (Exception ex)
        {
        }
        return "";
    }

    static public string substr(string s, int start, int len)
    {
        try
        {
            if (s.Length == 0) return "";
            if (start > s.Length) return "";
            if ((len + start) > s.Length) len = s.Length - start;
            return s.Substring(start, len) ;
        }
        catch (Exception ex)
        {
        }
        return "";
    }


}

public class P
{
    private static P _instance = new P();

    public static string brk = "\x0D\x0A";

    public long version = 0;

    public bool use_arc_db = false;



    public string Get_name_otdel(string id_podr)
    {

        try
        {
            using (SqlV sqlv = new SqlV("select TOP 1 name FROM spr_otdel WHERE id = '" + Util_l.db_to_str(id_podr)+"'" ))
            {

                if (sqlv.reader.HasRows)
                {
                    if (sqlv.reader.Read())
                    {
                        return  sqlv.reader["name"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
        return "";
    }
    public string PadE(string str, string empty)
    {
        if (str.Length >= empty.Length) return str;

        string tmp = str + empty;
        return SubStr(tmp, 0, empty.Length);
    }

    public string Right(string str, int len)
    {
        if (str.Length == 0) return "";

        int len1 = str.Length - len;
        if (len1 <= 0) len1 = 0;

        return SubStr(str, len1);
    }

    public string SubStr(string str, int start)
    {
        return SubStr(str, start, str.Length);
    }

    public string SubStr(string str, int start, int len)
    {
        if (str.Length > 0)
        {
            if (start < str.Length)
            {
                if (start + len < str.Length)
                    return str.Substring(start, len);
                else
                    return str.Substring(start);
            }
        }
        return "";
    }

    public string bool_int_str(object str)
    {
        if (str == null) return "0";

        string tmp = str.ToString();
        if (tmp == "True" || tmp == "1") return "1";

        return "0";
    }
    public string yesno(int b)
    {
        return yesno(b, false);
    }
    public string yesno(int b,bool revers)
    {
        if (b > 0) return revers?"Нет":"Да" ;
        return revers ? "Да" : "Нет";
    }
    public bool is_bool(object str)
    {
        if (str == null) return false;

        string tmp = str.ToString();
        if (tmp == "True" || tmp == "1") return true;

        return false;
    }
    public string S_str(string str, int len)
    {

        if (str.Length <= len || len < 4)
        {
            return str;
        }

        return str.Substring(0, len - 3) + "...";
    }
    public string sqldecor(string str)
    {
        string s = str.Replace("'", "''");
        string s1 = s.Replace("\\", "\\\\");
        return s1;
    }
    public string io_decor(object str)
    {
        string s = str.ToString();
        s = s.Replace("\\", "_");
        s = s.Replace(":", "_");

        return s;
    }

 
    public string date_to_strshort(object t)
    {
        if (t == null) return "";
        if (t == DBNull.Value) return "";

        return ((DateTime)t).ToShortDateString();

    }



    public DateTime to_datetime(object str)
    {
        DateTime dt = DateTime.Parse("01/01/1970");
        try
        {
            dt = (DateTime)str;
        }
        catch (Exception ex)
        {
        }
        return dt;
    }

    public Decimal to_dec_d(Double d)
    {
        return to_dec(d.ToString());
    }


    public long to_long(object str)
    {
        if( str == null) return 0;

        string tmp_str = str.ToString();

        if (tmp_str == "") return 0;

        try
        {
            long d = long.Parse(tmp_str, NumberStyles.Integer);
            return d;

        }
        catch (Exception ex)
        {
        }


        return -1;
    }
    public int to_int(object str)
    {
        if (str == null) return 0;

        string tmp_str = str.ToString();

        if (tmp_str == "") return 0;

        try
        {
            int d = int.Parse(tmp_str, NumberStyles.Integer);
            return d;

        }
        catch (Exception ex)
        {
        }


        return -1;
    }

    public Decimal to_dec(object str)
    {
        string tmp_str = str.ToString();

        if (tmp_str == "") return 0;

        try
        {
            Decimal d = Decimal.Parse(tmp_str, NumberStyles.Currency);
            return d;

        }
        catch (Exception ex)
        {
        }

        try
        {
            Decimal d = Decimal.Parse(tmp_str, NumberStyles.Number);
            return d;

        }
        catch (Exception ex)
        {
        }


        return -1;
    }


    public string to_sql_dt(DateTime dt, bool to_end)
    {
        if (to_end)
            return " CONVERT(DATETIME, '" + dt.ToString("yyyy.MM.dd") + " 23:59:59',101 ) ";
        else
            return " CONVERT(DATETIME, '" + dt.ToString("yyyy.MM.dd") + " 00:00:00',101 ) ";
    }
    public string to_sql_dt(DateTime dt)
    {
        return " CONVERT(DATETIME, '" + dt.ToString("yyyy.MM.dd HH:mm:ss") + "',101 ) ";
    }





    public decimal dec_okr(decimal d)
    {
        decimal tmp = d * 100;
        decimal ost = tmp - Decimal.ToInt64(tmp);
        if (ost > 0)
        {
            tmp = (1 + (tmp - ost)) / 100;
            return tmp;
        }
        return d;
    }

    public decimal dec_okr_to_int2(decimal d)
    {
        decimal d_i = dec_okr_to_int(d);
        long i_i = Decimal.ToInt64(d_i);

        if (d_i != 2 * (i_i / 2)) return d_i + 1;

        return d_i;
    }

    public decimal dec_okr_to_int(decimal d)
    {
        decimal tmp = d;
        decimal ost = tmp - Decimal.ToInt64(tmp);
        if (ost > 0)
        {
            tmp = Decimal.ToInt64(d) + 1;
            return tmp;
        }
        return d;
    }

    



    public static P I
    {
        get
        {
            return _instance;
        }
    }
 

}




