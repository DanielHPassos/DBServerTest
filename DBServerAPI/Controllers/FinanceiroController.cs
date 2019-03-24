using DBServerAPI.Models;
using DBServerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServerAPI.Controllers
{
    [Route("api/financeiro")]
    [ApiController]
    public class FinanceiroController : ControllerBase
    {
        private IFinanceiroService financeiroService { get; set; }

        public FinanceiroController(IFinanceiroService financeiroService)
        {
            this.financeiroService = financeiroService;
        }
        [Route("extrato/{idCliente}")]
        [HttpGet]
        public IActionResult BuscarExtrato(long idCliente)
        {
            var response = this.financeiroService.BuscarExtrato(idCliente);

            return StatusCode((int)response.StatusCode, response);
        }
        [Route("transferencia/")]
        [HttpPost]
        public IActionResult TransferirSaldo([FromBody] Transferencia transferencia)
        {
            var response = this.financeiroService.RealizarTransferencia(transferencia);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
