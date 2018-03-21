using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assincrono
{
    public partial class Async : System.Web.UI.Page
    {
        //SqlConnection conexao;
        //SqlCommand command;
        //SqlDataReader sqlDataReader;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected async void btnInsert_Click(object sender, EventArgs e)
        #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            CarregarDadosAssincrono();
            //conexao = ConnectAndOpen("192.168.254.193", "teste", "sa", "fxm@sterb1");
            //SqlConnection conexaoSQLServer = new SqlConnection();
            //string stringConexaoSQLServer = String.Format("DATA SOURCE={0}; INITIAL CATALOG={1}; USER ID={2}; Password={3}; Pooling=false;", "192.168.254.193", "teste", "sa", "fxm@sterb1");
            //string sql_User = "SELECT t3.UserID from [dbo].[Users] t3 WHERE t3.User_Nome = '" + 1 + @"'";

            //conexaoSQLServer = new SqlConnection(stringConexaoSQLServer);
            //await conexaoSQLServer.OpenAsync();

            //command = new SqlCommand(sql_User, conexao);
            //using (SqlDataReader rdr = await command.ExecuteReaderAsync())
            //{
            //    while (await rdr.ReadAsync())
            //    {
            //        txtDados.AppendText("\nId: ");
            //        txtDados.AppendText(await rdr.GetFieldValueAsync<int>(0) + "\t\t" + await rdr.GetFieldValueAsync<string>(1));
            //        txtDados.AppendText("\n");
            //    }
            //}

            //sqlDataReader.Close();
            //conexao.Close();

            // await AccessTheWebAsync();
        }

        //async Task AccessTheWebAsync()
        //{
        //    // You need to add a reference to System.Net.Http to declare client.
        //    HttpClient client = new HttpClient();

        //    // GetStringAsync returns a Task<string>. That means that when you await the
        //    // task you'll get a string (urlContents).
        //    Task<HttpResponseMessage> getStringTask = client.GetAsync("http://msdn.microsoft.com"); //GetStringAsync("http://msdn.microsoft.com");

        //    // You can do work here that doesn't rely on the string from GetStringAsync

        //    // The await operator suspends AccessTheWebAsync.
        //    //  - AccessTheWebAsync can't continue until getStringTask is complete.
        //    //  - Meanwhile, control returns to the caller of AccessTheWebAsync.
        //    //  - Control resumes here when getStringTask is complete. 
        //    //  - The await operator then retrieves the string result from getStringTask.
        //    HttpResponseMessage urlContents = await getStringTask;

        //    DoIndependentWork();

        //    // The return statement specifies an integer result.
        //    // Any methods that are awaiting AccessTheWebAsync retrieve the length value.
        //    //resultsTextBox.Text +=
        //    //    String.Format("\r\nLength of the downloaded string: {0}.\r\n", urlContents.ToString().Length);
        //}

        void DoIndependentWork()
        {
            resultsTextBox.InnerText += "Working . . . . . . .";
        }

        private void CarregaDados()
        {
            // SqlConnection conn = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=Cadastro;Integrated Security=True");
            // SqlConnection conn = new SqlConnection(string.Format("DATA SOURCE={0}; INITIAL CATALOG={1}; USER ID={2}; Password={3}; Pooling=false;", "fx-erp-cloud-WebService.database.windows.net", "FxERPcloud__eSocial", "edson.amaro@fluxus.com.br@fx-erp-cloud-webservice", "Fx-WebService"));
            using (SqlConnection conn = new SqlConnection(string.Format("DATA SOURCE={0}; INITIAL CATALOG={1}; USER ID={2}; Password={3}; Pooling=false;", "192.168.254.193", "HCM_CLOUD", "sa", "fxm@sterb1")))
            {
                // connection.Open();  //O Pool A é criado.
                conn.StateChange += new StateChangeEventHandler(OnStateChange);
                try
                {
                    conn.Open();
                    string sql = @"
                                DECLARE @i int = 0
                                IF OBJECT_ID('TEMPTeste') IS NOT NULL DROP TABLE TEMPTeste
                                    CREATE TABLE TEMPTeste(
	                                    batch_ID INT,
	                                    batch_sendXML [xml]
                                    );
                                WHILE @i < 1500
                                BEGIN
	                                INSERT INTO TEMPTeste
		                                SELECT batch_ID, batch_sendXML FROM eSocial_batches
	                                SET @i = @i + 1
                                END";
                    SqlCommand cmdVerifcPost = new SqlCommand(sql, conn);
                    cmdVerifcPost.CommandTimeout = 0;

                    cmdVerifcPost.ExecuteNonQuery();

                    string sql1New = @"SELECT batch_ID FROM TEMPTeste -- WHERE batch_ID BETWEEN 1 AND 1005";

                    SqlCommand cmd = new SqlCommand(sql1New, conn);
                    cmd.CommandTimeout = 0;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            resultsTextBox.InnerHtml += (((!rdr.IsDBNull(0)) ? rdr.GetFieldValue<int>(0) : 0)).ToString() + Environment.NewLine;// + "\t" + ((!rdr.IsDBNull(1)) ? rdr["batch_sendXML"].ToString() : ""));
                    }
                }
                catch (SqlException ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Detalhes Exception", "alert('" + ex.Message.Replace("'", "") + "');", true);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private async void CarregarDadosAssincrono()
        {
            //using (SqlConnection conn = new SqlConnection(string.Format("DATA SOURCE={0}; INITIAL CATALOG={1}; USER ID={2}; Password={3}; Pooling=true;", "192.168.254.193", "HCM_CLOUD", "sa", "fxm@sterb1")))
            using (SqlConnection conn = new SqlConnection(string.Format("DATA SOURCE={0}; INITIAL CATALOG={1}; USER ID={2}; Password={3}; Pooling=true;", "mychurch.database.windows.net", "mychurch", "mychurch", "fxm@sterb1")))
            {
                // connection.Open();  //O Pool A é criado.
                conn.StateChange += new StateChangeEventHandler(OnStateChange);
                try
                {
                    await conn.OpenAsync();
                    string sql = @"
                                DECLARE @i int = 0
                                IF OBJECT_ID('TEMPTeste') IS NOT NULL DROP TABLE TEMPTeste
                                    CREATE TABLE TEMPTeste(
	                                    batch_ID INT,
	                                    batch_sendXML [xml]
                                    );
                                WHILE @i < 750
                                BEGIN
	                                INSERT INTO TEMPTeste
		                                SELECT batch_ID, batch_sendXML FROM eSocial_batches
	                                SET @i = @i + 1
                                END";
                    SqlCommand cmdVerifcPost = new SqlCommand(sql, conn);
                    cmdVerifcPost.CommandTimeout = 0;

                    await cmdVerifcPost.ExecuteNonQueryAsync();

                    string sql1New = @"SELECT batch_ID FROM TEMPTeste -- WHERE batch_ID BETWEEN 1 AND 1005";

                    SqlCommand cmd = new SqlCommand(sql1New, conn);
                    cmd.CommandTimeout = 0;

                    using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                            resultsTextBox.InnerHtml += (((!rdr.IsDBNull(0)) ? await rdr.GetFieldValueAsync<int>(0) : 0)).ToString() + Environment.NewLine;// + "\t" + ((!rdr.IsDBNull(1)) ? rdr["batch_sendXML"].ToString() : ""));
                    }

                    //conn = new SqlConnection(string.Format("DATA SOURCE={0}; INITIAL CATALOG={1}; USER ID={2}; Password={3}; Pooling=false;", "192.168.254.193", "HCM_CLOUD", "sa", "fxm@sterb1"));
                    ////SqlConnection conn = new SqlConnection(string.Format("DATA SOURCE={0}; INITIAL CATALOG={1}; USER ID={2}; Password={3}; Pooling=false;", "fx-erp-cloud-WebService.database.windows.net", "FxERPcloud__eSocial", "edson.amaro@fluxus.com.br@fx-erp-cloud-webservice", "Fx-WebService"));
                    //await conn.OpenAsync();

                    //string sql1 = @"SELECT batch_ID, batch_sendXML FROM #tempTeste -- WHERE Collaborator_Name IS NOT NULL AND Collaborator_ID BETWEEN 436 AND 450";
                    //string sql1 = @"SELECT batch_ID FROM TEMPTeste -- WHERE batch_ID BETWEEN 1 AND 1005";
                    //try
                    //{
                    //    SqlCommand cmd = new SqlCommand(sql1, conn);
                    //    int k = 0;
                    //    resultsTextBox.InnerText = "";

                    //    using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                    //    {
                    //        while (await rdr.ReadAsync())
                    //        {
                    //            k = k + 1;

                    //            //resultsTextBox.InnerText += (Environment.NewLine + "\r\nId: ");
                    //            resultsTextBox.InnerText += (((!rdr.IsDBNull(0)) ? await rdr.GetFieldValueAsync<int>(0) : 0));// + "\t" + ((!rdr.IsDBNull(1)) ? rdr["batch_sendXML"].ToString() : ""));
                    //            resultsTextBox.InnerText += (Environment.NewLine);
                    //            //DoIndependentWork();
                    //        }
                    //    }
                    //}
                    //catch (SqlException ex)
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "Detalhes Exception", "alert('" + ex.Message.Replace("'", "") + "');", true);
                    //}
                    //finally
                    //{
                    //    conn.Close();
                    //}
                }
                catch (SqlException ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Detalhes Exception", "alert('" + ex.Message.Replace("'", "") + "');", true);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        static SqlConnection ConnectAndOpen(string localhost, string banco, string usuario, string senha)
        {
            SqlConnection conexaoSQLServer = new SqlConnection();
            string stringConexaoSQLServer = String.Format("DATA SOURCE={0}; INITIAL CATALOG={1}; USER ID={2}; Password={3}; Pooling=false;", localhost, banco, usuario, senha);

            try
            {
                conexaoSQLServer = new SqlConnection(stringConexaoSQLServer);
                conexaoSQLServer.Open();
            }
            catch (Exception Ex)
            {

                throw;
            }

            return conexaoSQLServer;
        }

        public static string ConnectBD()
        {
            string connectionString = null;
            //1-definição das informações para montar a string de conexão
            string Server = "192.168.254.193";
            string Username = "sa";
            string Password = "fxm@sterb1";
            string Database = "IntraNet";

            //2-montagem da string de conexão
            connectionString = "Data Source=" + Server + ";";
            connectionString += "User ID=" + Username + ";";
            connectionString += "Password=" + Password + ";";
            connectionString += "Initial Catalog=" + Database;

            return connectionString;
        }

        protected void btnInsertSync_Click(object sender, EventArgs e)
        {
            CarregaDados();
        }

        protected static void OnStateChange(object sender, StateChangeEventArgs args)
        {
            Console.WriteLine(
              "The current Connection state has changed from {0} to {1}.",
                args.OriginalState, args.CurrentState);
        }

    }
}