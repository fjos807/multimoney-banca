namespace Multimoney.Banca.Api.Modelos
{
    public class AgregarDepositoCuenta
    {
        public int TipoIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public int NumeroCuenta { get; set; }
        public decimal Monto { get; set; }
    }
}
