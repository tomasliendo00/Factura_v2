using EFWebAPI.Data.Models;
using System.Data;

namespace EFWebAPI.Data.Repositories

{
    public interface IFacturaRepository
    {
        List<Factura> GetAll();

        Factura? GetByID(int id);

        bool Create(Factura factura);

        bool Update(Factura factura);

        bool Delete(int id);
    }
}
