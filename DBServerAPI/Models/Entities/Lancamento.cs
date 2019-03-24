using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServerAPI.Models.Entities
{
    public class Lancamento
    {
        public string IdLancamento { get; set; }
        public long IdClienteOrigem { get; set; }
        public long IdClienteDestino { get; set; }

        public decimal Valor { get; set; }
        public DateTime DtHrInclusao { get; set; }
    }
}
