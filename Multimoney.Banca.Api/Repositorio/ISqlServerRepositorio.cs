namespace Multimoney.Banca.Api.Repositorio
{
    public interface ISqlServerRepositorio
    {
        Task<(IEnumerable<T>, string)> ObtenerResultadoProcedimiento<T>(string nombreProcedimiento, object? argumentos = null, bool utilizaSalida = false);
    }
}
