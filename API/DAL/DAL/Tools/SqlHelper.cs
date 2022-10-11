using Domain;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAL.Tools
{
    /// <summary>
    /// this class has the connections to the database
    /// </summary>
    internal static class SqlHelper
    {
        static string conString;
        static SqlHelper()
        {
            conString = ConfigurationManager.ConnectionStrings["MainConString"].ConnectionString;

        }
        /// <summary>
        /// return the connection 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static SqlConnection getConnection()
        {
            SqlConnection conn;
            try
            {

                conn = new SqlConnection(conString);

                return conn;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// insert a new Document
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Int32 ExecuteNonQuery(String commandText,
            CommandType commandType, RequestToPrint parameters)
        {
            using (var trxScope = new TransactionScope())
            {
                SqlConnection conn = getConnection();
                try
                {
                    conn.Open();
                    var comm = new SqlCommand(commandText);
                    comm.Connection = conn;

                    comm.Parameters.AddWithValue("Name", parameters.Document);
                    comm.Parameters.AddWithValue("Printed", DateTime.Now);
                    comm.Parameters.AddWithValue("Saved", DateTime.Now);
                    var resultado = comm.ExecuteNonQuery();
                    trxScope.Complete();

                    return resultado;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
        /// <summary>
        /// get the Document by id
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = getConnection();
            try
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    return reader;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }
        }
        
    }
}
