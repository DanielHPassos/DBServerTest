using AutoFixture;
using DBServerAPI.Infra.Interfaces;
using DBServerAPI.Models.Entities;
using DBServerAPI.Services;
using NSubstitute;
using System;
using System.Linq;
using System.Net;
using Xunit;

namespace DBServerTests
{
    public class FinanceiroServiceTest
    {
        private FinanceiroService financeiroService;
        private IRepositorioClientes repositorioClientes;
        private IRepositorioLancamentos repositorioLancamentos;
        private Fixture fixture;

        public FinanceiroServiceTest()
        {
            this.repositorioClientes = Substitute.For<IRepositorioClientes>();
            this.repositorioLancamentos = Substitute.For<IRepositorioLancamentos>();

            this.financeiroService = new FinanceiroService(this.repositorioClientes, this.repositorioLancamentos);
            this.fixture = new Fixture();
        }
        // Fiz somente um teste para economizar tempo
        [Fact]
        public void AoBuscarExtrato_QuandoClienteExistir_EntaoRetornaSucesso()
        {
            var lancamentos = this.fixture.CreateMany<Lancamento>(2).ToList();

            this.repositorioClientes.ClienteExiste(Arg.Any<long>()).Returns(true);
            this.repositorioLancamentos.BuscarLancamentos(Arg.Any<long>()).Returns(lancamentos);

            var result = this.financeiroService.BuscarExtrato(1);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(lancamentos, result.Result);
        }
    }
}
