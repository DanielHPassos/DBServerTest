using DBServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServerAPI.Services.Interfaces
{
    public interface IFinanceiroService
    {
        Response BuscarExtrato(long idCliente);
        Response RealizarTransferencia(Transferencia transferencia);
    }
}
