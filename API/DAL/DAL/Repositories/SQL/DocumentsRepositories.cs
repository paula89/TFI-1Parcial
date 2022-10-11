using DAL.Contracts;
using DAL.Tools;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.SQL
{
    /// <summary>
    /// this class implements the interface IGenericRepository to create the differents transactions to the Documents
    /// </summary>
    public class DocumentsRepositories : IGenericRepository<Printed>
    {

        /// <summary>
        /// statements region
        /// </summary>
        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[DocumentsPrinted] (Name, Printed, Saved) VALUES (@Name, @Printed, @Saved)";
        }

        private string SelectAByIdStatement
        {
            get => "SELECT Name, Printed, Saved FROM [dbo].[DocumentsPrinted] WHERE Id = @Id";
        }

        private string SelectAllStatement
        {
            get => "SELECT Id, Name, Printed, Saved FROM [dbo].[DocumentsPrinted] ORDER BY Saved DESC";
        }
        #endregion

        /// <summary>
        /// get the Documents by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Printed> GetAll(int id)
        {
            try
            {
                List<Printed> documents = new List<Printed>();
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@Id", id));

                using (var dr = SqlHelper.ExecuteReader(SelectAByIdStatement, System.Data.CommandType.Text, parametros.ToArray()))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        Printed document = new Printed();
                        //document.Id = values[0].ToString();
                        document.DocumentName = values[0].ToString();
                        document.SaveDate = (DateTime)values[1];
                        document.PrintDate = (DateTime)values[2];
                        documents.Add(document);
                    }
                }

                return documents;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("exc :::" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get all the Documents
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Printed> GetAll()
        {
            try
            {
                List<Printed> documents = new List<Printed>();
                List<SqlParameter> parametros = new List<SqlParameter>();

                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, System.Data.CommandType.Text, parametros.ToArray()))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        Printed document = new Printed();
                        document.Id = (int)values[0];
                        document.DocumentName = values[1].ToString();
                        document.SaveDate = (DateTime)values[2];
                        document.PrintDate = (DateTime)values[3];
                        documents.Add(document);
                    }
                }

                return documents;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("exc :::" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// insert a new document 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public int Insert(RequestToPrint document)
        {
            try
            {
                int inserted = SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text, document);

                return inserted;
            } catch (Exception ex)
            {
                System.Console.WriteLine("exc :::" + ex.Message);
                return 0;
            }
        }

        public int Insert(Printed o)
        {
            throw new NotImplementedException();
        }
    }
}
