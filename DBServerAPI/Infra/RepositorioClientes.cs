using DBServerAPI.Infra.Interfaces;
using DBServerAPI.Models;
using DBServerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServerAPI.Infra
{
    public class RepositorioClientes : IRepositorioClientes
    {
        private static List<Cliente> clientes = new List<Cliente>();
        public RepositorioClientes()
        {
            clientes.Add(new Cliente() { IdCliente = 1, Valor = 50000 });
            clientes.Add(new Cliente() { IdCliente = 2, Valor = 200 });
        }
        public bool ClienteExiste(long idCliente)
        {
            return clientes.Any(x => x.IdCliente == idCliente);
        }
        public decimal AtualizarSaldo(long idCliente, decimal saldo)
        {
            var indexCliente = clientes.FindIndex(x => x.IdCliente == idCliente);

            return clientes[indexCliente].Valor = saldo;
        }

        public decimal BuscarSaldo(long idCliente)
        {
            var indexCliente = clientes.FindIndex(x => x.IdCliente == idCliente);

            return clientes[indexCliente].Valor;
        }
    }
}
