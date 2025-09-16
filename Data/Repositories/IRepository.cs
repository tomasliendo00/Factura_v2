using EFWebAPI.Data.Models;
using System.Data;

namespace EFWebAPI.Data.Repositories



{
    public interface IRepository
    {
        List<Factura> ObtenerFacturas();

        int ObtenerProximoNro();

        bool Crear(Factura oFactura);

        bool Actualizar(Factura oFactura);

        bool Borrar(int nro);

        List<Factura> ObtenerFacturaPorFiltros(DateOnly desde, DateOnly hasta, int idCliente); // Cliente cliente

        Factura ObtenerFacturaPorNro(int nro);

        DataTable ObtenerReporte(DateOnly desde, DateOnly hasta);
    }
}
