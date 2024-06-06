using Multimoney.Banca.Api.Interfaces;
using Multimoney.Banca.Api.Modelos;
using Multimoney.Banca.Api.Repositorio;

namespace Multimoney.Banca.Api.Servicios
{
    public class TransferenciaCuentasServicio : ITransferenciaCuentasServicio
    {
        private readonly ISqlServerRepositorio _repositorio;

        public TransferenciaCuentasServicio(ISqlServerRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<RespuestaServicio> RealizarTransferenciaCuentas(TransferenciaEntreCuentas transferenciaEntreCuentas)
        {
            try
            {
                var argumentos = new
                {
                    arg_tipo_identificacion = transferenciaEntreCuentas.TipoIdentificacion,
                    arg_identificacion = transferenciaEntreCuentas.Identificacion,
                    arg_id_cuenta_origen = transferenciaEntreCuentas.NumeroCuentaOrigen,
                    arg_id_cuenta_destino = transferenciaEntreCuentas.NumeroCuentaDestino,
                    arg_monto = transferenciaEntreCuentas.Monto
                };

                var respuesta = await _repositorio.ObtenerResultadoProcedimiento<object>(Constantes.NOMBRE_SP_AGREGAR_TRANSFERENCIAS, argumentos, true);
                string estadoRespuesta = respuesta.Item2;

                RespuestaServicio respuestaServicio = new RespuestaServicio();

                if (estadoRespuesta == "200")
                {
                    respuestaServicio.OperacionExitosa = true;
                    respuestaServicio.Mensaje = "Transferencia realizada correctamente";
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
