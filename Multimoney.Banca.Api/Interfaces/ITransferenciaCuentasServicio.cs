using Multimoney.Banca.Api.Modelos;

namespace Multimoney.Banca.Api.Interfaces
{
    public interface ITransferenciaCuentasServicio
    {
        Task<RespuestaServicio> RealizarTransferenciaCuentas(TransferenciaEntreCuentas transferenciaEntreCuentas);
    }
}
