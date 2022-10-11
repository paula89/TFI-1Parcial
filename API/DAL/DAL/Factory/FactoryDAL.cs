using System.Configuration;
using DAL.Repositories.SQL;

namespace DAL.Factory
{
    public class FactoryDAL
    {
        #region Singleton

        private readonly static FactoryDAL instance = new FactoryDAL();

        public static FactoryDAL Current
        {
            get
            {
                return instance;
            }
        }

        private FactoryDAL()
        {
            //Implement here the initialization code
        }
        #endregion
        /// <summary>
        /// Return the Document's repository 
        /// </summary>
        /// <returns></returns>
        public DocumentsRepositories GetDocumentRepository()
        {
            try
            {
                string nombreNamespaceClaseAccesoDatos = ConfigurationManager.AppSettings["AccesoDatosBusiness"] + ".DocumentsRepositories";
                object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoDatos));

                return instancia as DocumentsRepositories;
            }
            catch (Exception exc)
            {
                System.Console.WriteLine("exc :::" + exc.Message);
                return null;
            }
        }

    }
}