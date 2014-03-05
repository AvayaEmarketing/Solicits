using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;


public partial class traductor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public class Datos 
    {
        public string solicit_id { get; set; }
        public string S_key_name {get;set;}
        public string nombre {get;set;}
        public string S_original_language {get;set;}
        public string S_translate_language {get;set;}
		public string S_register_date {get;set;}
        public string S_desired_date {get;set;}
        public string T_Fecha_Estimada { get; set; }
        public string S_solicit_priority { get; set; }
        
    }

    public class Calendar {
        public string id { get; set; }
        public string url { get; set; }
        public string clase { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }

    [WebMethod]
    public static string getCountries()
    {
        string result;
        SqlDataReader datos;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        string strSQL = "SELECT idCountry,Country from C_Country order by Country";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        try
        {
            con.Open();
            datos = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(datos);
            result = DataTableToJSON(dt);
            //result = new JavaScriptSerializer().Serialize(dt);
            con.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            result = "";
        }
        finally
        {
            con.Close();
        }
        return result;

    }

    public static string DataTableToJSON(DataTable table)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(list);
    }

    //Metodo del Calendario
    [WebMethod(EnableSession = true)]
    public static string getDatosCalendarioDetail()
    {
        var sessionUsuario = HttpContext.Current.Session;
        string responsable2 = sessionUsuario["id"].ToString();
        string result = "";
        SqlDataReader datos;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        //string strSQL = "SELECT CONVERT(VARCHAR(20),solicit_id) AS id, S_document_name AS title, CONVERT(VARCHAR(20),(cast(Datediff(s, '1970-01-01', S_desired_date2) AS bigint)*1000)) AS start, CONVERT(VARCHAR(20),(cast(Datediff(s, '1970-01-01', T_Fecha_Estimada2) AS bigint)*1000)) AS 'end' from Translate_Solicits where estado=3 and S_visible='YES'";
        string strSQL = "SELECT solicit_id AS url, S_solicit_priority AS 'clase', CONVERT(VARCHAR(20),solicit_id) AS id, S_key_name AS title, CONVERT(VARCHAR(20),(cast(Datediff(s, '1970-01-01', S_register_date2) AS bigint)*1000)) AS start, CONVERT(VARCHAR(20),(cast(Datediff(s, '1970-01-01', T_Fecha_Estimada2) AS bigint)*1000)) AS 'end' from Translate_Solicits where S_visible='YES' and T_Fecha_Estimada2 IS NOT NULL and responsable=" + responsable2;
        SqlCommand cmd = new SqlCommand(strSQL, con);
        try
        {
            con.Open();
            datos = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(datos);
            result = DataTableToJSON(dt);

            ////Agregar una nueva columna para el tipo de evento 
            //DataColumn colClass = dt.Columns.Add("class", typeof(string));
    //        foreach (DataRow row in dt.Rows)
    //        {
    //            string valor;
    //            valor = row["class"].ToString();
    //            if (valor.Equals("Low")) { row[1] = "event-success"; }
    //            if (valor.Equals("Medium")) { row[1] = "event-important"; }
    //            if (valor.Equals("High")) { row[1] = "event-warning"; }
    //            string id = row["url"].ToString();
    //            row[0] = "trad_req_detail.aspx?id=" + id;
    //        }

    //        result = DataTableToJSON(dt);
    //        //result = new JavaScriptSerializer().Serialize(dt);
    //        con.Close();

    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //        result = "";
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //    return result;

            //}result = DataTableToJSON(dt);
            //result = new JavaScriptSerializer().Serialize(dt);
            con.Close();

        }
        catch (Exception ex)
        {
            result = "fail";
            Console.WriteLine(ex.Message);
            
        }
        finally
        {
            con.Close();
        }
        return result;

    }

    [WebMethod]
    public static string Convertir()
    {
        string resultado = "error0";
        try
        {
            string path = HttpContext.Current.Server.MapPath("~");
            resultado = toExcel(path);
            HttpContext.Current.Response.SetCookie(new HttpCookie("fileDownload", "true") { Path = "/" });
        }
        catch (Exception e)
        {
            resultado = e.ToString();
        }
        return resultado;

    }

    [WebMethod]
    public static string getDatosReg()
    {
        string result = "";
        if (validaSession() == "fail")
        {
            result = "fail";
        }
        else
        {

            string resultado = getDatosRequest();
            var serializer = new JavaScriptSerializer();
            List<Datos> values = serializer.Deserialize<List<Datos>>(resultado);

            string jsonarmado = "[";
            foreach (var root in values)
            {
                jsonarmado += "{\"S_key_name\": \"" + root.S_key_name + "\", \"nombre\": \"" + root.nombre + "\",\"S_original_language\": \"" + root.S_original_language + "\",\"S_translate_language\": \"" + root.S_translate_language + "\",\"S_register_date\": \"" + root.S_register_date + "\",\"S_desired_date\": \"" + root.S_desired_date + "\",\"T_Fecha_Estimada\": \"" + root.T_Fecha_Estimada + "\",\"S_solicit_priority\": \"" + root.S_solicit_priority + "\",\"Edit\": \"<a href=\'#\' onClick=\'detailsTrad(" + root.solicit_id + ")\'><img title=\'Details\' src=\'images/edit.png\'/></a>\"},";
            }

            jsonarmado = jsonarmado.Substring(0, jsonarmado.Length - 1);
            if (jsonarmado == "")
            {
                result = "[]";
            }
            else
            {
                result = jsonarmado + "]";
            }
        }
        return result;
    }

  

    [WebMethod]
    public static string getEvents()
    {
        string result = "";
        
        if (validaSession() == "fail")
        {
            result = "fail";
        }
        else
        {
            string clase = "";
            string resultado = getDatosCalendarioDetail();
            var serializer = new JavaScriptSerializer();
            List<Calendar> values = serializer.Deserialize<List<Calendar>>(resultado);

            string jsonarmado = "[";
            if (values != null)
            {
                foreach (var root in values)
                {
                    if (root.clase.Equals("Low")) { clase = "event-success"; }
                    if (root.clase.Equals("Medium")) { clase = "event-important"; }
                    if (root.clase.Equals("High")) { clase = "event-warning"; }
                    jsonarmado += "{\"id\": \"" + root.id + "\",\"url\": \"trad_req_detail.aspx?id=" + root.url + "\", \"class\": \"" + clase + "\",\"title\": \"" + root.title + "\",\"start\": \"" + root.start + "\",\"end\": \"" + root.end + "\"},";
                }
            }

            jsonarmado = jsonarmado.Substring(0, jsonarmado.Length - 1);
            if (jsonarmado == "")
            {
                result = "[]";
            }
            else
            {
                result = jsonarmado + "]";
            }
        }
        return result;
    }

    [WebMethod(EnableSession = true)]
    public static string getDatosRequest()
    {
        string result;
        SqlDataReader datos;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        var sessionUsuario = HttpContext.Current.Session;
        string responsable2 = sessionUsuario["id"].ToString();
        int responsable = Convert.ToInt32(responsable2);
        //string strSQL = "select sol.solicit_id, sol.S_key_name, sta.nombre, sol.S_original_language, sol.S_translate_language, sol.S_register_date, sol.S_desired_date, sol.S_solicit_priority from Translate_Solicits sol, Translate_State sta  where sol.responsable = @responsable and sol.estado = sta.id and S_register_date2 = (select max(S_register_date2) as fecha_registro from Translate_Solicits where responsable = @responsable)";
        string strSQL = "select sol.solicit_id, sol.S_key_name, sta.nombre, sol.S_original_language, sol.S_translate_language, sol.S_register_date, sol.S_desired_date,sol.T_Fecha_Estimada, sol.S_solicit_priority from Translate_Solicits sol, Translate_State sta  where sol.responsable = @responsable and sol.estado = sta.id and S_visible = 'YES'";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        cmd.Parameters.Add("@responsable", SqlDbType.Int);
        cmd.Parameters["@responsable"].Value = responsable;

        try
        {
            con.Open();
            datos = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(datos);
            result = DataTableToJSON(dt);
            //result = JsonConvert.SerializeObject(dt);
            con.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            result = "";
        }
        finally
        {
            con.Close();
        }
        return result;

    }

    public static int getTraductor(string original_language, string translate_language) {
        int traductor = 0;

        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        string stmt = "select id from UserData where idioma1 = @original and idioma2 = @translate";

        SqlCommand cmd2 = new SqlCommand(stmt, con);

        cmd2.Parameters.Add("@original", SqlDbType.VarChar, 50);
        cmd2.Parameters.Add("@translate", SqlDbType.VarChar, 50);

        cmd2.Parameters["@original"].Value = original_language;
        cmd2.Parameters["@translate"].Value = translate_language;

        try
        {
            con.Open();
            traductor = (Int32)cmd2.ExecuteScalar();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            con.Close();
        }

        return traductor;
    }

    [WebMethod(EnableSession = true)]
    public static string putData(string translation_name,int document_type, string original_language, string translate_language, string desired_date, string prioridad, string priority_comment, string observations, string document_name)
    {
        string result = "";
		string resultado = "";
        DateTime datt = DateTime.Now;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();

        string strSQL = "SELECT CURRENT_TIMESTAMP AS registerDate";
        SqlCommand cmd = new SqlCommand(strSQL, con);
        try
        {
            con.Open();
            datt = (DateTime)cmd.ExecuteScalar();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            con.Close();
        }

        
        var sessionUsuario = HttpContext.Current.Session;
        int traductor = 0;
        string idioma = "";
        string solicitante2 = sessionUsuario["id"].ToString();
        int solicitante = Convert.ToInt32(solicitante2);
        int state = 1;  //estado = Requerida
        int i = 0;
        int j = 0;
        string[] words = translate_language.Split(',');
        Dictionary<Int32, string> traductor_idioma = new Dictionary<Int32, string>();
        foreach (string word in words)
        {
            //Console.WriteLine(word);
            traductor = getTraductor(original_language, word);
            traductor_idioma.Add(traductor, word);
            j = j + 1;
        }
        
        int[] arr4 = new int[j]; 
        

        foreach (var datos in traductor_idioma)
        {
            traductor = datos.Key;
            idioma = datos.Value;
            arr4[i] = guardarDatos(translation_name,solicitante, traductor, state, document_type, original_language, idioma, prioridad, priority_comment, observations, datt, desired_date,document_name);
            i = i + 1;
        }

        for (int k = 0; k < arr4.Length; k++)
        {
            if (arr4[k] != -1)
            {
                result = result + arr4[k].ToString() + ',';
            }
        }



        //  FALTA EL ENVIO DE LOS EMAILS A LOS TRADUCTORES

        //if (result == "ok")
        //{
        //    string nombrec = inputName + " " + inputLast;
        //    resultado = sendMails(nombrec, inputCompany, inputEmail);
        //}
        return result;
    }

    public static int guardarDatos(string translation_name,int solicitante, int traductor,  int state, int document_type, string original_language, string translate_language, string prioridad,string priority_comment,string observations,DateTime datt,string desired_date, string document_name) {
        int result;
       
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["calawebConnectionString"].ToString();
        
        string stmt = "INSERT INTO Translate_Solicits (solicit_id,S_Key_name, solicitante_id, responsable, estado, S_document_type,S_original_language,S_translate_language,S_solicit_priority,S_priority_comment,S_observations,S_register_date,S_register_date2,S_desired_date,S_desired_date2,S_document_name) OUTPUT INSERTED.solicit_id VALUES (@solicit_id,@translation_name,@solicitante, @traductor, @state, @document_type, @original_language, @translate_language, @prioridad, @priority_comment, @comments, @register_date, @register_date2, @desired_date, @desired_date2, @document_name)";

        SqlCommand cmd2 = new SqlCommand(stmt, con);
        cmd2.Parameters.Add("@solicit_id", SqlDbType.Int);
        cmd2.Parameters.Add("@translation_name", SqlDbType.VarChar, 200);
        cmd2.Parameters.Add("@solicitante", SqlDbType.Int);
        cmd2.Parameters.Add("@traductor", SqlDbType.Int);
        cmd2.Parameters.Add("@state", SqlDbType.Int);
        cmd2.Parameters.Add("@document_type", SqlDbType.Int);
        cmd2.Parameters.Add("@original_language", SqlDbType.VarChar, 50);
        cmd2.Parameters.Add("@translate_language", SqlDbType.VarChar, 50);
        cmd2.Parameters.Add("@prioridad", SqlDbType.VarChar, 10);
        cmd2.Parameters.Add("@priority_comment", SqlDbType.VarChar, 500);
        cmd2.Parameters.Add("@comments", SqlDbType.VarChar, 500);
        cmd2.Parameters.Add("@register_date", SqlDbType.VarChar, 60);
        cmd2.Parameters.Add("@register_date2", SqlDbType.DateTime);
        cmd2.Parameters.Add("@desired_date", SqlDbType.VarChar, 60);
        cmd2.Parameters.Add("@desired_date2", SqlDbType.DateTime);
        cmd2.Parameters.Add("@document_name", SqlDbType.VarChar,150);

        cmd2.Parameters["@solicit_id"].Value = 0;
        cmd2.Parameters["@translation_name"].Value = translation_name;
        cmd2.Parameters["@solicitante"].Value = solicitante;
        cmd2.Parameters["@traductor"].Value = traductor;
        cmd2.Parameters["@state"].Value = state;
        cmd2.Parameters["@document_type"].Value = document_type;
        cmd2.Parameters["@original_language"].Value = original_language;
        cmd2.Parameters["@translate_language"].Value = translate_language;
        cmd2.Parameters["@prioridad"].Value = prioridad;
        cmd2.Parameters["@priority_comment"].Value = priority_comment;
        cmd2.Parameters["@comments"].Value = observations;
        cmd2.Parameters["@register_date"].Value = datt;
        cmd2.Parameters["@register_date2"].Value = datt;
        cmd2.Parameters["@desired_date"].Value = desired_date;
        cmd2.Parameters["@desired_date2"].Value = desired_date;
        cmd2.Parameters["@document_name"].Value = document_name;

        try
        {
            con.Open();
            result = (Int32)cmd2.ExecuteScalar();
            con.Close();
            
        }
        catch (Exception ex)
        {
            result = -1;
            Console.WriteLine(ex.Message);
        }
        finally
        {
            con.Close();
        }
        return result;
    }
	
	    public static string sendMails(string nombre, string observacion, string correo)
        {
            string result = "";
            string title = "Avaya ATF"; 
            try
            {
                //string contenido = getContenidoMail(nombre, observacion);
                string plantilla = getPlantilla();
                string rta_mail = SendMail(correo, "e-marketing@avaya.com", title, plantilla);

                result = "ok";
            }
            catch (Exception ex)
            {
                result = "false" + ex;
            }
            return result;
        }

        public static string SendMail(string to, string from, string subject, string contenido)
        {
            string respuesta = "";

            MailAddress sendfrom = new MailAddress(from);
            MailAddress sendto = new MailAddress(to);
            MailMessage message = new MailMessage();

            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            string body = HttpUtility.HtmlDecode(contenido);
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            message.AlternateViews.Add(alternate);

            message.From = new MailAddress(from);
            message.To.Add(to);
            message.Subject = subject;

            SmtpClient client = new SmtpClient("localhost");

            try
            {
                client.Send(message);
                respuesta = "ok";

            }
            catch (SmtpException e)
            {
                respuesta = "fail";
                throw new SmtpException(e.Message);

            }
            return respuesta;
        }

        public static string getContenidoMail(string nombre, string observacion)
        {
            string plantilla = getPlantilla();

            Dictionary<string, string> dataIndex = new Dictionary<string, string>();
            dataIndex.Add("{nombre}", nombre);
            dataIndex.Add("{evento}", observacion);
            
            string buscar = "";
            string reemplazar = "";
            string index = "";
            //Recorrer la plantilla del index para reemplazar el contenido
            foreach (var datos in dataIndex)
            {
                buscar = datos.Key;
                reemplazar = datos.Value;
                index = plantilla.Replace(buscar, reemplazar);
                plantilla = index;
            }

            return plantilla;
        }

        public static string getPlantilla()
        {
            string fullPath = HttpContext.Current.Server.MapPath("~");

			string html = "";
            //html = File.ReadAllText(fullPath + "sf_mail_conf_register1_html.html");
            html = File.ReadAllText(fullPath + "\\usa\\events\\ATF-2014_test\\ATF-ty-reg.html");
			return html;
        }


        public static string getContenido()
        {
            string result = "";
            string resultado = getDatosRequest();
            string tabla = "";
            var serializer = new JavaScriptSerializer();

            List<Datos> values = serializer.Deserialize<List<Datos>>(resultado);

            foreach (var root in values)
            {

                tabla += "<tr>";
                tabla += "<td>" + root.S_key_name + "</td>";
                tabla += "<td>" + root.nombre + "</td>";
                tabla += "<td>" + root.S_original_language + "</td>";
                tabla += "<td>" + root.S_translate_language + "</td>";
                tabla += "<td>" + root.S_register_date + "</td>";
                tabla += "<td>" + root.S_desired_date + "</td>";
                tabla += "<td>" + root.T_Fecha_Estimada + "</td>";
                tabla += "<td>" + root.S_solicit_priority + "</td>";
                tabla += "</tr>";
            }



            result = tabla;
            return result;
        }

        public static string toExcel(string path1)
        {
            string respuesta = "";
            string path = path1 + "\\ExcelFiles\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string nombre = DateTime.Now.ToString("yyyyMMddhhmmss") + "ExcelFiles.xls";
            string fullPath = path1 + "\\ExcelFiles\\" + nombre;
            string contenido = getContenido();
            string data = "<tr><th width=\"10%\">Document Name</th><th width=\"10%\">State</th><th width=\"10%\">Original Language</th><th width=\"10%\">Translation Language</th><th width=\"10%\">Register Date</th><th width=\"10%\">Desired Date</th><th width=\"10%\">Estimated Date</th><th width=\"10%\">Priority</th></tr>";
            contenido = data + contenido;
            contenido = "<table border = '1' style=" + '"' + "font-family: Verdana,Arial,sans-serif; font-size: 12px;" + '"' + ">" + contenido + "</table></body></html>";

            try
            {
                FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);

                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(contenido);
                }
            }
            catch
            {
                respuesta = "fail";
            }
            finally
            {
                respuesta = nombre;
            }
            return respuesta;
        }

        [WebMethod(EnableSession = true)]
        public static string validaSession()
        {
            string result = "";
            var sessionUsuario = HttpContext.Current.Session;
            if (sessionUsuario["id"] == null)
            {
                result = "fail";
            }
            else
            {
                result = sessionUsuario["id"].ToString();
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        public static string cerrarSession()
        {
            var sessionUsuario = HttpContext.Current.Session;
            var resultado = "";
            try
            {
                sessionUsuario.Clear();
                sessionUsuario.Abandon();
                resultado = "ok";
            }
            catch (Exception)
            {
                resultado = "fail";
            }
            return resultado;
        }


}

