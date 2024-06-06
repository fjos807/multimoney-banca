namespace Multimoney.Banca.Api
{
    public static class Constantes
    {
        #region Informacion del servicio

        public static string NOMBRE_SERVICIO = "NombreServicio";
        public static string VERSION_SERVICIO = "VersionServicio";
        public static string ESTADO_SERVICIO = "Ok";

        #endregion

        #region Base de datos

        public static string NOMBRE_CADENA_CONEXION = "ConexionBaseDatosBanca";
        public static string NOMBRE_SALIDA_ESTADO = "arg_estado";

        #endregion

        #region Procedimientos almacenados

        public static string NOMBRE_SP_OBTENER_CUENTA_COMPLETO = "PROC_CUENTAS_OBTENER_COMPLETO";
        

        #endregion

        #region Errores controlados

        public static Dictionary<string, string> ERRORES_CONTROLADOS = new Dictionary<string, string>()
        {
            {"CNE", "La cuenta ingresada no existe en el sistema" }
        };
        
        #endregion
    }
}
