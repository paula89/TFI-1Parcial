using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BadResponse
    {
        public int DocumentId { get; set; }
        public string Message { get; } = "Documento no encontrado";

    }
}
