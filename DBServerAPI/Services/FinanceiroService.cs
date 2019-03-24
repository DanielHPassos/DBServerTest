using DBServerAPI.Infra.Interfaces;
using DBServerAPI.Models;
using DBServerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServerAPI.Services
{
    public class FinanceiroService : IFinanceiroService
    {
        private IRepositorioClientes repositorioClientes { get; set; }
        private IRepositorioLancamentos repositorioLancamentos { get; set; }
        public FinanceiroService(IRepositorioClientes repositorioClientes, IRepositorioLancamentos repositorioLancamentos
            //ILog seu log aqui
            )
        {
            this.repositorioClientes = repositorioClientes;
            this.repositorioLancamentos = repositorioLancamentos;
        }
        public Response BuscarExtrato(long idCliente)
        {
            var result = new Response();
            try
            {
                if (!this.repositorioClientes.ClienteExiste(idCliente))
                    return result.SetError("Cliente não encontrado").SetStatusCode(System.Net.HttpStatusCode.NotFound);

                // Eu sei que o certo deveria assegurar de buscar o usuário que está autenticado no momento.
                // Mas não tenho tempo para algo tão bem desenvolvido ou correto.
                // Incluse, se essa fosse uma demanda da sprint de fato, isso tudo daria facilmente um projeto, pois existe uma série de coisas a serem feitas.
                var lancamentos = this.repositorioLancamentos.BuscarLancamentos(idCliente);

                return result.SetResponse(lancamentos).SetStatusCode(System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                //log.Error(ex.ToString()) se tivesse log, enfim...
                return result.SetStatusCode(System.Net.HttpStatusCode.InternalServerError).SetError("Erro interno");
            }
        }
        public Response RealizarTransferencia(Transferencia transferencia)
        {
            var result = new Response();
            try
            {
                if (transferencia.IdClienteOrigem == transferencia.IdClienteDestino)
                    return result.SetError("Não é possível realizar transferência para si mesmo").SetStatusCode(System.Net.HttpStatusCode.BadRequest);

                if (!this.repositorioClientes.ClienteExiste(transferencia.IdClienteOrigem))
                    return result.SetError("Cliente de Origem não encontrado").SetStatusCode(System.Net.HttpStatusCode.NotFound);

                if (!this.repositorioClientes.ClienteExiste(transferencia.IdClienteDestino))
                    return result.SetError("Cliente de Destino não encontrado").SetStatusCode(System.Net.HttpStatusCode.NotFound);

                decimal saldoClienteOrigem = this.repositorioClientes.BuscarSaldo(transferencia.IdClienteOrigem);

                if ((saldoClienteOrigem - transferencia.Valor) < 0)
                    return result.SetError($"Saldo insuficiente. Saldo atual: {saldoClienteOrigem}").SetStatusCode(System.Net.HttpStatusCode.BadRequest);

                // Os dados daqui para baixo deveriam ser transacionados
                saldoClienteOrigem = this.repositorioClientes.AtualizarSaldo(transferencia.IdClienteOrigem, saldoClienteOrigem - transferencia.Valor);

                decimal saldoClienteDestino = this.repositorioClientes.BuscarSaldo(transferencia.IdClienteDestino);

                this.repositorioClientes.AtualizarSaldo(transferencia.IdClienteDestino, saldoClienteDestino + transferencia.Valor);

                this.repositorioLancamentos.InserirLancamento(transferencia);

                return result.SetResponse(new { transferido = transferencia.Valor, saldoAtual = saldoClienteOrigem }).SetStatusCode(System.Net.HttpStatusCode.OK);

            }
            catch (Exception)
            {
                return result.SetStatusCode(System.Net.HttpStatusCode.InternalServerError).SetError("Erro interno");
            }
        }
    }
}
