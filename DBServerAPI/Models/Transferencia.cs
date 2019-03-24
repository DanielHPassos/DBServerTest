using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServerAPI.Models
{
    public class Transferencia
    {
        public long IdClienteOrigem { get; set; }
        public long IdClienteDestino { get; set; }
        public decimal Valor { get; set; }
    }
}
