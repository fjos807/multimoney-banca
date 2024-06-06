namespace Multimoney.Banca.Api.Modelos
{
    public class Cuenta
    {
        public int IdCuenta { get; set; }
        public string NumeroIBAN { get; set; }
        public string Moneda { get; set; }
        public decimal SaldoDisponible { get; set; }
        public decimal SaldoBloqueado { get; set; }
    }
}
