namespace Multimoney.Banca.Api.Modelos
{
    public class TransferenciaEntreCuentas
    {
        public int TipoIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public int NumeroCuentaOrigen { get; set; }
        public int NumeroCuentaDestino { get; set; }
        public decimal Monto { get; set; }
    }
}
