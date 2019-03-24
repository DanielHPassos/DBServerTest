using DBServerAPI.Infra.Interfaces;
using DBServerAPI.Models;
using DBServerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServerAPI.Infra
{
    public class RepositorioLancamentos : IRepositorioLancamentos
    {
        private static List<Lancamento> lancamentos = new List<Lancamento>();

        public List<Lancamento> BuscarLancamentos(long idCliente)
        {
            return lancamentos.Where(x => x.IdClienteOrigem == idCliente || x.IdClienteDestino == idCliente).ToList();
        }

        public bool InserirLancamento(Transferencia transferencia)
        {
            lancamentos.Add(new Lancamento { IdLancamento = Guid.NewGuid().ToString(), IdClienteOrigem = transferencia.IdClienteOrigem, IdClienteDestino = transferencia.IdClienteDestino, Valor = transferencia.Valor, DtHrInclusao = DateTime.Now });

            return true;
        }
    }
}
