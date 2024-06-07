using Microsoft.AspNetCore.Mvc;
using Multimoney.Banca.Api.Interfaces;
using Multimoney.Banca.Api.Modelos;
using Multimoney.Banca.Api.Servicios;

namespace Multimoney.Banca.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cuentas/transferencias")]
    public class TransferenciasController : ControllerBase
    {
        private readonly ITransferenciaCuentasServicio _transferenciaCuentasServicio;

        public TransferenciasController(ITransferenciaCuentasServicio transferenciaCuentasServicio)
        {
            _transferenciaCuentasServicio = transferenciaCuentasServicio;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400, Type = typeof(object))]
        [ProducesResponseType(500, Type = typeof(object))]
        async public Task<IActionResult> NuevaTransferenciaCuentas(TransferenciaEntreCuentas transferenciaEntreCuentas)
        {
            IActionResult respuesta;
            try
            {
                var respuestaServicio = await _transferenciaCuentasServicio.RealizarTransferenciaCuentas(transferenciaEntreCuentas);
                if (respuestaServicio.OperacionExitosa)
                {
                    respuesta = Ok(respuestaServicio);
                }
                else
                {
                    respuesta = StatusCode(StatusCodes.Status400BadRequest, respuestaServicio);
                }
            }
            catch (ArgumentException ex)
            {
                respuesta = StatusCode(StatusCodes.Status400BadRequest, new { mensaje = $"Ocurrió un error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Ocurrió un error: {ex.Message}" });
            }

            return respuesta;
        }
    }
}
