using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceivingApp.PrinterQueue
{
    public class Printer
    {
        #region Singleton

        private readonly static Printer instance = new Printer();

        public static Printer Current
        {
            get
            {
                return instance;
            }
        }

        private Printer()
        {
        }
        #endregion

        /// <summary>
        /// created a boolean response
        /// </summary>
        /// <returns></returns>
        public Boolean DocumentPrinted()
        {
            return new Random().Next(2) == 1;
        }
    }
}
