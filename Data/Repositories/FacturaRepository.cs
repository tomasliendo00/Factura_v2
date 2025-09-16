using EFWebAPI.Data.Models;
using System.Data;

namespace EFWebAPI.Data.Repositories
{
    public class FacturaRepository : IRepository
    {
        public bool Actualizar(Factura oFactura)
        {
            throw new NotImplementedException();
        }

        public bool Borrar(int nro)
        {
            throw new NotImplementedException();
        }

        public bool Crear(Factura oFactura)
        {
            throw new NotImplementedException();
        }

        public List<Factura> ObtenerFacturaPorFiltros(DateOnly desde, DateOnly hasta, int idCliente)
        {
            throw new NotImplementedException();
        }

        public Factura ObtenerFacturaPorNro(int nro)
        {
            throw new NotImplementedException();
        }

        public List<Factura> ObtenerFacturas()
        {
            throw new NotImplementedException();
        }

        public int ObtenerProximoNro()
        {
            throw new NotImplementedException();
        }

        public DataTable ObtenerReporte(DateOnly desde, DateOnly hasta)
        {
            throw new NotImplementedException();
        }
    }
}
