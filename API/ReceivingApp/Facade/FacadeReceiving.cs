using Domain;
using ReceivingApp.Managers;

namespace ReceivingApp.Facade
{
    public static class FacadeReceiving
    {
        /// <summary>
        /// simulate send the document to the final printer
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static ResponseOK? Print(RequestToPrint document)
        {
            return PrinterManager.Current.Print(document);
        }
    }
}