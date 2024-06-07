using Multimoney.Banca.Api.Interfaces;
using Multimoney.Banca.Api.Servicios;
using Multimoney.Banca.Api.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Multimoney.Banca.Api
{
    public static class Dependencias
    {
        public static IServiceCollection AgregarDependencias(this IServiceCollection servicios, IConfiguration configuracion) {
            
            servicios.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var campoLlaveError = context.ModelState.LastOrDefault();
                    var campoNombreError = campoLlaveError.Key.Replace("$.", string.Empty);
                    var detalleError = new
                    {
                        error = $"Hubo un error validando el campo {campoLlaveError.Key}"
                    };

                    return new BadRequestObjectResult(detalleError);
                };
            });

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
