using DBServerAPI.Models;
using DBServerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServerAPI.Infra.Interfaces
{
    public interface IRepositorioLancamentos
    {
        List<Lancamento> BuscarLancamentos(long idCliente);
        bool InserirLancamento(Transferencia transferencia);
    }
}
