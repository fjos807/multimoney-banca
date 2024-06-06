using Multimoney.Banca.Api.Modelos;

namespace Multimoney.Banca.Api.Interfaces
{
    public interface IAgregarDepositoServicio
    {
        Task<RespuestaServicio> IngresarNuevoDeposito(AgregarDepositoRetiroCuenta agregarDepositoCuenta);
    }
}
