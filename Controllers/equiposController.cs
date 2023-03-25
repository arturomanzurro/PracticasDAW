using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica01.Models;
using Microsoft.EntityFrameworkCore;

namespace Practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {

        private readonly equiposContext _equiposContexto;

        public equiposController (equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;

        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<equipos> listadoEquipo = (from e in _equiposContexto.equipos join m in _equiposContexto.marcas on e.marca_id equals
                                           m.id_marcas select new
                                           {
                                               e.id_equipos,

                                           }
                                           

            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipo);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id) 
        {
            equipos? equipo = (from e in _equiposContexto.equipos where e.id_equipos == id select e).FirstOrDefault();

            if(equipo == null) 
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro) 
        {
            equipos? equipos = (from e in _equiposContexto.equipos where e.descripcion.Contains(filtro) select e).FirstOrDefault();

            if (equipos == null) 
            {
                return NotFound();
            }

            return Ok(equipos);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {
            try 
            {
                _equiposContexto.equipos.Add(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
             
        }


        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar) 
        {
            equipos? equipoActual = (from e in _equiposContexto.equipos
                                     where e.id_equipos == id select e).FirstOrDefault();

            if (equipoActual == null) {return NotFound();}

            equipoActual.nombre = equipoModificar.nombre;
            equipoActual.descripcion = equipoModificar.descripcion;
            equipoActual.marca_id = equipoModificar?.marca_id;
            equipoActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
            equipoActual.anio_compra = equipoModificar.anio_compra;
            equipoActual.costo = equipoActual.costo;

            _equiposContexto.Entry(equipoActual).State= EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(equipoModificar);
        }
        [HttpDelete]
        [Route ("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id) 
        {
            equipos? equipo = (from e in _equiposContexto.equipos where e.id_equipos== id select e).FirstOrDefault();

            if (equipo == null) { return NotFound(); }

            _equiposContexto.equipos.Attach(equipo);
            _equiposContexto.equipos.Remove(equipo);
            _equiposContexto.SaveChanges();

            return Ok(equipo);
        }
        

    }

    
}
