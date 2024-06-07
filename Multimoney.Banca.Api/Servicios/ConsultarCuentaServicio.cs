using Multimoney.Banca.Api.Interfaces;
using Multimoney.Banca.Api.Modelos;
using Multimoney.Banca.Api.Repositorio;
using Newtonsoft.Json;

namespace Multimoney.Banca.Api.Servicios
{
    public class ConsultarCuentaServicio : IConsultarCuentaServicio
    {
        private readonly ISqlServerRepositorio _repositorio;

        public ConsultarCuentaServicio(ISqlServerRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<RespuestaServicio> ObtenerInformacionCuenta(ConsultaInformacionCuenta consultaInformacionCuenta)
        {
            try
            {
                var argumentos = new
                {
                    arg_tipo_identificacion = consultaInformacionCuenta.TipoIdentificacion,
                    arg_identificacion = consultaInformacionCuenta.Identificacion,
                    arg_id_cuenta = consultaInformacionCuenta.NumeroCuenta
                };

                var respuesta = await _repositorio.ObtenerResultadoProcedimiento<RespuestaConsultaCuenta>(Constantes.NOMBRE_SP_OBTENER_CUENTA_COMPLETO, argumentos, true);
                var registroCuenta = respuesta.Item1;
                string estadoRespuesta = respuesta.Item2;

                RespuestaServicio respuestaServicio = new RespuestaServicio();

                if (estadoRespuesta == "200")
                {
                    var informacionCuentaCompleta = new
                    {
                        cuenta = JsonConvert.DeserializeObject<Cuenta>(registroCuenta.First().Cuenta),
                        movimientos = JsonConvert.DeserializeObject<List<MovimientoCuenta>>(registroCuenta.First().Movimientos ?? "[]"),
                        intereses = JsonConvert.DeserializeObject<List<InteresCuenta>>(registroCuenta.First().Intereses ?? "[]"),
                    };

                    respuestaServicio.OperacionExitosa = true;
                    respuestaServicio.Datos = informacionCuentaCompleta;
                    respuestaServicio.Mensaje = "Cuenta consultada correctamente";
                } else
                {
                    respuestaServicio.OperacionExitosa = false;
                    respuestaServicio.Error = Constantes.ERRORES_CONTROLADOS[estadoRespuesta] ?? "Error desconocido";
                }

                return respuestaServicio;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ArgumentException(ex.ToString());
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
    }
}
