using Microsoft.AspNetCore.Mvc;
using Multimoney.Banca.Api.Interfaces;
using Multimoney.Banca.Api.Modelos;
using Multimoney.Banca.Api.Servicios;

namespace Multimoney.Banca.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cuentas/retiros")]
    public class RetirosController : ControllerBase
    {
        private readonly IAgregarRetiroServicio _agregarRetiroServicio;

        public RetirosController(IAgregarRetiroServicio agregarRetiroServicio)
        {
            _agregarRetiroServicio = agregarRetiroServicio;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400, Type = typeof(object))]
        [ProducesResponseType(500, Type = typeof(object))]
        async public Task<IActionResult> IngresarDeposito(AgregarDepositoRetiroCuenta agregarRetiroCuenta)
        {
            IActionResult respuesta;
            try
            {
                var respuestaServicio = await _agregarRetiroServicio.IngresarNuevoRetiro(agregarRetiroCuenta);
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
