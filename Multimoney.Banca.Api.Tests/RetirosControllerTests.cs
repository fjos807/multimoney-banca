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
    public class RetirosControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpCliente;

        public RetirosControllerTests(WebApplicationFactory<Program> app)
        {
            _httpCliente = app.CreateClient();
        }

        [Fact]
        private async void RealizarRetiroExitoso()
        {
            var cuerpoPrueba = new
            {
                tipoIdentificacion = 1,
                identificacion = "100010001",
                numeroCuenta = 1,
                monto = 3500
            };
            var stringContenido = new StringContent(JsonConvert.SerializeObject(cuerpoPrueba), Encoding.UTF8, "application/json");

            var respuesta = await _httpCliente.PostAsync("/api/v1/cuentas/retiros", stringContenido);

            respuesta.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        private async void RealizarRetiroFallido()
        {
            var cuerpoPrueba = new
            {
                tipoIdentificacion = 1,
                identificacion = "hola",
                numeroCuenta = 1,
                monto = 3500
            };
            var stringContenido = new StringContent(JsonConvert.SerializeObject(cuerpoPrueba), Encoding.UTF8, "application/json");

            var respuesta = await _httpCliente.PostAsync("/api/v1/cuentas/retiros", stringContenido);

            respuesta.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
