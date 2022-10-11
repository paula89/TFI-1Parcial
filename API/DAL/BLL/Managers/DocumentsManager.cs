using AutogService.BLL.Facade;
using DAL.Factory;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutogService.BLL.Managers
{
    /// <summary>
    /// this class manage the documents printed
    /// </summary>
    public class DocumentsManager
    {
        #region Singleton
        private readonly static DocumentsManager _instance = new DocumentsManager();
        private FactoryDAL instancia;

        public static DocumentsManager Current
        {
            get
            {
                return _instance;
            }
        }

        private DocumentsManager()
        {
            instancia = FactoryDAL.Current;
        }
        #endregion

        /// <summary>
        /// save the document printed
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public ResponseOK? SaveDocument(RequestToPrint document)
        {
            try
            {
                int total = instancia.GetDocumentRepository().Insert(document);
                if (total == 1)
                {
                    ResponseOK response = new()
                    {
                        DocumentName = document.Document,
                        PrintDate = DateTime.Now,
                    };
                    return response;
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("exc :::" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// get the document printed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Printed> GetAllDocumentPrinted()
        {
            IEnumerable<Printed> documents = new List<Printed>();
            try
            {
                documents = instancia.GetDocumentRepository().GetAll();
                return documents;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("exc :::" + ex.Message);
                return documents;
            }

        }

        /// <summary>
        /// get the document printed by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Object GetDocumentPrinted(int id)
        {
            try
            {
                IEnumerable<Printed> documents = instancia.GetDocumentRepository().GetAll(id);
                if (documents.Count() > 0)
                {
                    ResponseOK response = new()
                    {
                        DocumentName = documents.First().DocumentName,
                        PrintDate = documents.First().PrintDate
                    };
                    return response;
                }
                return new BadResponse()
                {
                    DocumentId = id
                };


            }
            catch (Exception ex)
            {
                System.Console.WriteLine("exc :::" + ex.Message);
                throw;
            }

        }

    }
}
