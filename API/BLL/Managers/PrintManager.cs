using BLL.Facade;
using DAL.Factory;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    /// <summary>
    /// this class manage the documents printed
    /// </summary>
    public class PrintManager
    {
        #region Singleton
        private readonly static PrintManager _instance = new PrintManager();
        private FactoryDAL instancia;

        public static PrintManager Current
        {
            get
            {
                return _instance;
            }
        }

        private PrintManager()
        {
            instancia = FactoryDAL.Current;
        }
        #endregion

        /// <summary>
        /// save the document printed
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public int SaveDocument(RequestToPrint document)
        {
            try
            {
                return instancia.GetDocumentRepository().Insert(document);
            } catch (Exception ex)
            {
                System.Console.WriteLine("exc :::" + ex.Message);
                //FacadeService.ManageException(new ExceptionDAL(ex.Message));
                throw;
            }
        }
        
        /// <summary>
        /// get the document printed
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Printed> GetDocumentPrinted()
        {
            IEnumerable<Printed> documents = new List<Printed>();
            try
            {
                string[] filters = {};
                documents = instancia.GetDocumentRepository().GetAll(filters);
                return documents.ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("exc :::" + ex.Message);
                //FacadeService.ManageException(new ExceptionDAL(ex.Message));
                throw;
            }

        }

    }
}
