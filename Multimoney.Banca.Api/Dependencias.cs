using Multimoney.Banca.Api.Interfaces;
using Multimoney.Banca.Api.Servicios;
using Multimoney.Banca.Api.Repositorio;

namespace Multimoney.Banca.Api
{
    public static class Dependencias
    {
        public static IServiceCollection AgregarDependencias(this IServiceCollection servicios, IConfiguration configuracion) {
            
            var cadenaConexion = configuracion.GetConnectionString(Constantes.NOMBRE_CADENA_CONEXION);
            if (!string.IsNullOrWhiteSpace(cadenaConexion)) {
                servicios.AddSingleton<ISqlServerRepositorio>((fabrica) =>
                {
                    return new SqlServerRepositorio(cadenaConexion);
                });
            }

            servicios.AddTransient<IConsultarCuentaServicio, ConsultarCuentaServicio>();
            servicios.AddTransient<IAgregarDepositoServicio, AgregarDepositoServicio>();
            servicios.AddTransient<IAgregarRetiroServicio, AgregarRetiroServicio>();
            servicios.AddTransient<ITransferenciaCuentasServicio, TransferenciaCuentasServicio>();

            return servicios;
        }
    }
}
