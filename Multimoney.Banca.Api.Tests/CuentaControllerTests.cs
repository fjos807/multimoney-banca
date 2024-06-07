using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace Multimoney.Banca.Api.Tests
{
    public class CuentaControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpCliente;

        public CuentaControllerTests(WebApplicationFactory<Program> app)
        {
            _httpCliente = app.CreateClient();
        }

        [Fact]
        private async void ConsultarCuentaExitoso()
        {
            var cuerpoPrueba = new
            {
                tipoIdentificacion = 1,
                identificacion = "100010001",
                numeroCuenta = 1
            };
            var stringContenido = new StringContent(JsonConvert.SerializeObject(cuerpoPrueba), Encoding.UTF8, "application/json");

            var respuesta = await _httpCliente.PostAsync("/api/v1/cuentas", stringContenido);

            respuesta.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        private async void ConsultarCuentaFallido()
        {
            var cuerpoPrueba = new
            {
                tipoIdentificacion = 1,
                identificacion = "hola",
                numeroCuenta = 1
            };
            var stringContenido = new StringContent(JsonConvert.SerializeObject(cuerpoPrueba), Encoding.UTF8, "application/json");

            var respuesta = await _httpCliente.PostAsync("/api/v1/cuentas", stringContenido);

            respuesta.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
