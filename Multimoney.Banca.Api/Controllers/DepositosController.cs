using Microsoft.AspNetCore.Mvc;
using Multimoney.Banca.Api.Interfaces;
using Multimoney.Banca.Api.Modelos;

namespace Multimoney.Banca.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cuentas/depositos")]
    public class DepositosController : ControllerBase
    {
        private readonly IAgregarDepositoServicio _agregarDepositoServicio;

        public DepositosController(IAgregarDepositoServicio agregarDepositoServicio)
        {
            _agregarDepositoServicio = agregarDepositoServicio;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400, Type = typeof(object))]
        [ProducesResponseType(500, Type = typeof(object))]
        async public Task<IActionResult> IngresarDeposito(AgregarDepositoCuenta agregarDepositoCuenta)
        {
            IActionResult respuesta;
            try
            {
                var respuestaServicio = await _agregarDepositoServicio.IngresarNuevoDeposito(agregarDepositoCuenta);
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
