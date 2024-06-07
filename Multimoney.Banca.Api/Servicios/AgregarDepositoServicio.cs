using Multimoney.Banca.Api.Interfaces;
using Multimoney.Banca.Api.Modelos;
using Multimoney.Banca.Api.Repositorio;
using Newtonsoft.Json;

namespace Multimoney.Banca.Api.Servicios
{
    public class AgregarDepositoServicio : IAgregarDepositoServicio
    {
        private readonly ISqlServerRepositorio _repositorio;

        public AgregarDepositoServicio(ISqlServerRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<RespuestaServicio> IngresarNuevoDeposito(AgregarDepositoRetiroCuenta agregarDepositoCuenta)
        {
            try
            {
                var argumentos = new
                {
                    arg_tipo_identificacion = agregarDepositoCuenta.TipoIdentificacion,
                    arg_identificacion = agregarDepositoCuenta.Identificacion,
                    arg_id_cuenta = agregarDepositoCuenta.NumeroCuenta,
                    arg_tipo_movimiento = Constantes.NOMBRE_MOVIMIENTO_DEPOSITO,
                    arg_monto = agregarDepositoCuenta.Monto
                };

                var respuesta = await _repositorio.ObtenerResultadoProcedimiento<object>(Constantes.NOMBRE_SP_AGREGAR_MOVIMIENTO_CONTABLE, argumentos, true);
                string estadoRespuesta = respuesta.Item2;

                RespuestaServicio respuestaServicio = new RespuestaServicio();

                if (estadoRespuesta == "200")
                {
                    respuestaServicio.OperacionExitosa = true;
                    respuestaServicio.Mensaje = "Depósito agregado correctamente";
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
