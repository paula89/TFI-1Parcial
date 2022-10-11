using AutogService.BLL.Managers;
using Domain;

namespace AutogService.BLL.Facade
{
    /// <summary>
    /// class that use the Facade pattern 
    /// </summary>
    public static class FacadeService
    {
        /// <summary>
        /// save the Document printed
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static ResponseOK? Save(RequestToPrint document)
        {
            return DocumentsManager.Current.SaveDocument(document);
        }

        /// <summary>
        /// get a Document printed by id
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Object Read(int document)
        {
            return DocumentsManager.Current.GetDocumentPrinted(document);
        }

        /// <summary>
        /// get all Documents printed
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Printed> ReadAll()
        {
            return DocumentsManager.Current.GetAllDocumentPrinted();
        }
    }
}