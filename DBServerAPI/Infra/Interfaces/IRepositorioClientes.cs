using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServerAPI.Infra.Interfaces
{
    public interface IRepositorioClientes
    {
        bool ClienteExiste(long idCliente);
        decimal BuscarSaldo(long idCliente);
        decimal AtualizarSaldo(long idCliente, decimal saldo);
    }
}
