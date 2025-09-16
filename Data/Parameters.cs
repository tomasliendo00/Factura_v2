namespace EFWebAPI.Data
{
    public class Parameters
    {
        public string Clave { get; set; }
        public object Valor { get; set; }

        public Parameters(string clave, object valor)
        {
            Clave = clave;
            Valor = valor;
        }
    }
}
