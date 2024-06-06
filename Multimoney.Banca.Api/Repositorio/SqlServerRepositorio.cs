using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Multimoney.Banca.Api.Repositorio
{
    public class SqlServerRepositorio : ISqlServerRepositorio
    {
        private string _cadenaConexion = null!;

        public SqlServerRepositorio(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public async Task<(IEnumerable<T>, string)> ObtenerResultadoProcedimiento<T>(string nombreProcedimiento, object? argumentos = null, bool utilizaSalida = false)
        {
            using var conexion = new SqlConnection(_cadenaConexion);
            var argumentosDinamicos = new DynamicParameters();
            argumentosDinamicos.AddDynamicParams(argumentos);

            if (utilizaSalida)
            {
                argumentosDinamicos.Add(Constantes.NOMBRE_SALIDA_ESTADO, "", DbType.String, direction: ParameterDirection.Output, 3);
            }

            var resultado = await conexion.QueryAsync<T>(nombreProcedimiento, argumentosDinamicos, commandType: CommandType.StoredProcedure);

            var estado = argumentosDinamicos.Get<string>(Constantes.NOMBRE_SALIDA_ESTADO);

            return (resultado, estado);
        }
    }
}
