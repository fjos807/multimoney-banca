using System.ComponentModel.DataAnnotations;

namespace Multimoney.Banca.Api.Modelos
{
    public class ConsultaInformacionCuenta
    {
        [Range(1, int.MaxValue)]
        public int TipoIdentificacion { get; set; }

        public string Identificacion { get; set; }

        [Range(1, int.MaxValue)]
        public int NumeroCuenta { get; set; }
    }
}
