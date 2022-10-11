using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ResponseOK
    {
        public string DocumentName { get; set; }    
        public DateTime PrintDate { get; set; }
        public string Answer
        { get; } = "OK";
    }
}
