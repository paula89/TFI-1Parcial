using Domain;
using ReceivingApp.PrinterQueue;

namespace ReceivingApp.Managers
{
    public class PrinterManager
    {
        #region Singleton
        private readonly static PrinterManager _instance = new PrinterManager();
        private Printer instancia;

        public static PrinterManager Current
        {
            get
            {
                return _instance;
            }
        }

        private PrinterManager()
        {
            instancia = Printer.Current;
        }
        #endregion

        /// <summary>
        /// send to the printer queue
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseOK? Print(RequestToPrint request)
        {
            if (request.Priority > 0 && request.Priority < 11 ) {
                var random = Printer.Current.DocumentPrinted();
                return random ? CreateResponseOk(request) :  null;                
            }            
            return null;
        }

        /// <summary>
        /// created a correct response
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private ResponseOK CreateResponseOk(RequestToPrint request)
        {
            return new ResponseOK() {
                DocumentName = request.Document,
                PrintDate = DateTime.Now,
            };    
        }

    }
}
