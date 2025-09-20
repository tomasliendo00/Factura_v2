using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EFWebAPI.Data.Repositories;
using EFWebAPI.Data.Models;

namespace EFWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private IFacturaRepository _repo;

        public FacturaController(IFacturaRepository repo)   // Inyección de dependencia del repositorio
        {
            _repo = repo;
        }

        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                return Ok(_repo.GetAll());   // Retorna todas las facturas
            }
            catch (Exception ex)    // Captura cualquier error
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { mensaje = "Error al intentar obtener las facturas.", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            try
            {
                var fact = _repo.GetByID(id);   // Busca la factura por ID
                if (fact == null)               // Si no se encontró...
                    return NotFound(new { mensaje = $"El Nro. de factura {id} no fue encontrado."});    // ...retorna 404
                return Ok(fact);    // Retorna la factura encontrada
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Error al intentar obtener la factura.", detalle = ex.Message });
            }
        }

        [HttpPost]

        public IActionResult Post([FromBody] Factura value)
        {
            try
            {
                if (value == null)
                    return BadRequest(new { mensaje = "La información de la factura es incorrecta." });
                _repo.Create(value);
                return CreatedAtAction(nameof(Get), new { id = value.NroFactura }, value);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Error al intentar crear la factura.", detalle = ex.Message });
            }
        }

        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] Factura value)
        {
            try
            {
                if (value == null)
                    return BadRequest(new { mensaje = "La información de la factura es incorrecta." });

                var fact = _repo.GetByID(id);
                if(fact == null)
                    return NotFound(new { mensaje = $"El Nro. de factura {id} no fue encontrado." });

                fact.Fecha = value.Fecha;
                fact.IdFormaPago = value.IdFormaPago;
                fact.IdCliente = value.IdCliente;
                //fact.DetalleFacturas = value.DetalleFacturas;

                _repo.Update(fact);
                return Ok(fact);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Error al intentar actualizar la factura.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            try
            {
                if(_repo.GetByID(id) == null)
                    return NotFound(new { mensaje = $"El Nro. de factura {id} no fue encontrado." });

                _repo.Delete(id);
                return Ok(new { mensaje = $"El Nro. de factura {id} fue eliminado." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Error al intentar eliminar la factura.", detalle = ex.Message });
            }
        }
    }
}
