namespace Multimoney.Banca.Api.Modelos
{
    public class InteresCuenta
    {
        public string? Fecha { get; set; }
        public decimal PorcentajeInteres { get; set; }
        public decimal MontoInteres { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoSiguiente { get; set; }
    }
}
