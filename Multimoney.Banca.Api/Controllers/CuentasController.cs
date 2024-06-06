using Microsoft.AspNetCore.Mvc;
using Multimoney.Banca.Api.Interfaces;
using Multimoney.Banca.Api.Modelos;

namespace Multimoney.Banca.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly IConsultarCuentaServicio _consultarCuentaServicio;

        public CuentasController(IConsultarCuentaServicio consultarCuentaServicio)
        {
            _consultarCuentaServicio = consultarCuentaServicio;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400, Type = typeof(object))]
        [ProducesResponseType(500, Type = typeof(object))]
        async public Task<IActionResult> ConsultaCuenta(ConsultaInformacionCuenta consultaInformacionCuenta)
        {
            IActionResult respuesta;
            try
            {
                var respuestaServicio = await _consultarCuentaServicio.ObtenerInformacionCuenta(consultaInformacionCuenta);
                if (respuestaServicio.OperacionExitosa) {
                    respuesta = Ok(respuestaServicio);
                } else
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
