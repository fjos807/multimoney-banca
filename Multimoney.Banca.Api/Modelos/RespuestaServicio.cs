namespace Multimoney.Banca.Api.Modelos
{
    public class RespuestaServicio
    {
        public bool OperacionExitosa { get; set; }
        public string? Error { get; set; }
        public string? Mensaje { get; set; }
        public object? Datos { get; set; }
    }
}
