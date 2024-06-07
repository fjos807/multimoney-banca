namespace Multimoney.Banca.Api.Modelos
{
    public class RespuestaConsultaCuenta
    {
        public string Cuenta { get; set; }
        public string Movimientos { get; set; }
        public string Intereses { get; set; }
    }
}
