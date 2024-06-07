using Microsoft.AspNetCore.Mvc;
using Multimoney.Banca.Api.Modelos;

namespace Multimoney.Banca.Api.Controllers
{
    [ApiController]
    [Route("estado-servicio")]
    public class EstadoServicioController : ControllerBase
    {
        private readonly IConfiguration _config;

        public EstadoServicioController(IConfiguration configuracion)
        {
            _config = configuracion;
        }

        [HttpGet()]
        [ProducesResponseType(200)]
        public IActionResult ObtenerEstadoServicio()
        {
            var informacion = new EstadoServicioClase
            {
                NombreServicio = _config[Constantes.NOMBRE_SERVICIO] ?? "Sin nombre configurado",
                VersionServicio = _config[Constantes.VERSION_SERVICIO] ?? "Sin versión configurada",
                EstadoServicio = Constantes.ESTADO_SERVICIO
            };

            IActionResult respuesta = Ok(informacion);
            return respuesta;
        }
    }
}
