using Multimoney.Banca.Api.Modelos;

namespace Multimoney.Banca.Api.Interfaces
{
    public interface IAgregarRetiroServicio
    {
        Task<RespuestaServicio> IngresarNuevoRetiro(AgregarDepositoRetiroCuenta agregarRetiroCuenta);
    }
}
