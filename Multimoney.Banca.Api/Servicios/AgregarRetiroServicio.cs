using Multimoney.Banca.Api.Interfaces;
using Multimoney.Banca.Api.Modelos;
using Multimoney.Banca.Api.Repositorio;

namespace Multimoney.Banca.Api.Servicios
{
    public class AgregarRetiroServicio : IAgregarRetiroServicio
    {
        private readonly ISqlServerRepositorio _repositorio;

        public AgregarRetiroServicio(ISqlServerRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<RespuestaServicio> IngresarNuevoRetiro(AgregarDepositoRetiroCuenta agregarRetiroCuenta)
        {
            try
            {
                var argumentos = new
                {
                    arg_tipo_identificacion = agregarRetiroCuenta.TipoIdentificacion,
                    arg_identificacion = agregarRetiroCuenta.Identificacion,
                    arg_id_cuenta = agregarRetiroCuenta.NumeroCuenta,
                    arg_tipo_movimiento = Constantes.NOMBRE_MOVIMIENTO_RETIRO,
                    arg_monto = agregarRetiroCuenta.Monto
                };

                var respuesta = await _repositorio.ObtenerResultadoProcedimiento<object>(Constantes.NOMBRE_SP_AGREGAR_MOVIMIENTO_CONTABLE, argumentos, true);
                string estadoRespuesta = respuesta.Item2;

                RespuestaServicio respuestaServicio = new RespuestaServicio();

                if (estadoRespuesta == "200")
                {
                    respuestaServicio.OperacionExitosa = true;
                    respuestaServicio.Mensaje = "Retiro agregado correctamente";
                }
                else
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
    }
}
