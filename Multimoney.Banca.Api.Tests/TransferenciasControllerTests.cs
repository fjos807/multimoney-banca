using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Multimoney.Banca.Api.Tests
{
    public class TransferenciasControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpCliente;

        public TransferenciasControllerTests(WebApplicationFactory<Program> app)
        {
            _httpCliente = app.CreateClient();
        }

        [Fact]
        private async void RealizarTransferenciaExitosa()
        {
            var cuerpoPrueba = new
            {
                tipoIdentificacion = 1,
                identificacion = "100010001",
                numeroCuentaOrigen = 1,
                numeroCuentaDestino = 2,
                monto = 3000
            };
            var stringContenido = new StringContent(JsonConvert.SerializeObject(cuerpoPrueba), Encoding.UTF8, "application/json");

            var respuesta = await _httpCliente.PostAsync("/api/v1/cuentas/transferencias", stringContenido);

            respuesta.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        private async void RealizarTransferenciaFallida()
        {
            var cuerpoPrueba = new
            {
                tipoIdentificacion = 1,
                identificacion = "hola",
                numeroCuentaOrigen = 1,
                numeroCuentaDestino = 2,
                monto = 3000
            };
            var stringContenido = new StringContent(JsonConvert.SerializeObject(cuerpoPrueba), Encoding.UTF8, "application/json");

            var respuesta = await _httpCliente.PostAsync("/api/v1/cuentas/transferencias", stringContenido);

            respuesta.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
