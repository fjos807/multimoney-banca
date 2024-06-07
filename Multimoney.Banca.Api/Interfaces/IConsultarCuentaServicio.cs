using Multimoney.Banca.Api.Modelos;

namespace Multimoney.Banca.Api.Interfaces
{
    public interface IConsultarCuentaServicio
    {
        Task<RespuestaServicio> ObtenerInformacionCuenta(ConsultaInformacionCuenta consultaInformacionCuenta);
    }
}
