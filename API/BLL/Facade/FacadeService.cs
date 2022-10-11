using BLL.Managers;
using Domain;

namespace BLL.Facade
{
    /// <summary>
    /// class that use the Facade pattern 
    /// </summary>
    public static class FacadeService
    {
        /// <summary>
        /// Manage the exceptions to create the Bitacora or messages user
        /// </summary>
        /// <param name="ex"></param>
        /*public static void ManageException(Exception ex)
        {
            ExceptionManager.Current.Handle(ex);
        }
        /// <summary>
        /// Write the Bitacora with the traces
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public static void Write(string message, EventLevel e)
        {
            TraceManager.Current.Write(message, e);
        }*/
        /// <summary>
        /// save the Document printed
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static int Save(RequestToPrint document)
        {
            return PrintManager.Current.SaveDocument(document);
        }

    }
}