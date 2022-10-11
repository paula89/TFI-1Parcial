using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RequestToPrint
    {
        public String Document { get; set; } // public byte[] document
        public int Priority { get; set; }
    }
}
