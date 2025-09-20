using EFWebAPI.Data.Models;
using System.Data;

namespace EFWebAPI.Data.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private FacturaDBContext _context;

        public FacturaRepository(FacturaDBContext context)      // Inyección de dependencia del contexto
        {
            _context = context;
        }

        public bool Create(Factura factura)
        {
            _context.Facturas.Add(factura);
            return _context.SaveChanges() > 0;  // Devuelve true si se guardó al menos un cambio
        }

        public bool Delete(int id)
        {
            var fact = GetByID(id);     // Busca la factura por ID
            if (fact != null)           // Si se encontró la factura...
            {
                _context.Facturas.Remove(fact);     // ...la elimina
                return _context.SaveChanges() > 0;  // Devuelve true
            }
            return false;   // Devuelve falso si no se encontró la factura
        }

        public List<Factura> GetAll()
        {
            return _context.Facturas.ToList(); // Retorna todas las facturas como una lista
        }

        public Factura? GetByID(int id)     // Busca una factura por su ID, puede no existir
        {
            return _context.Facturas.Find(id);      // Retorna la factura o null si no se encuentra
        }

        public bool Update(Factura factura)
        {
            _context.Facturas.Update(factura);
            return _context.SaveChanges() > 0;  // Devuelve true si se guardó al menos un cambio
        }
    }
}
