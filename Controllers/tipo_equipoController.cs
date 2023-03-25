using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica01.Models;

namespace Practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipo_equipoController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;
        public tipo_equipoController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        
        [HttpPost]
        [Route("Addtipoequipo")]
        public IActionResult agregartipoequipo([FromBody] tipo_equipo nuevotipo_equipo)
        {
            try
            {
                _equiposContexto.Add(nuevotipo_equipo);
                _equiposContexto.SaveChanges();
                return Ok(nuevotipo_equipo);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {
                List<tipo_equipo> listatipoEqu = (from tp in _equiposContexto.tipo_equipo select tp).ToList();

                if (listatipoEqu.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listatipoEqu);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult actualizartipoequipo(int id, [FromBody] tipo_equipo modificartipo_equipo)
        {
            
              
                tipo_equipo? tipodeequipo = (from m in _equiposContexto.tipo_equipo where m.id_tipo_equipo == id select m).FirstOrDefault();


                if (tipodeequipo == null) return NotFound();

                
                tipodeequipo.descripcion = modificartipo_equipo.descripcion;
                tipodeequipo.estado = modificartipo_equipo.estado;

                _equiposContexto.Entry(tipodeequipo).State = EntityState.Modified;
                _equiposContexto.SaveChanges();
                return Ok(tipodeequipo);
            
            

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminar(int id)
        {
            
                
                tipo_equipo? tipodeequipo = (from m in _equiposContexto.tipo_equipo where m.id_tipo_equipo == id select m).FirstOrDefault();


                if (tipodeequipo == null) return NotFound();

                _equiposContexto.tipo_equipo.Attach(tipodeequipo);
                _equiposContexto.tipo_equipo.Remove(tipodeequipo);
                _equiposContexto.SaveChanges();
                return Ok(tipodeequipo);
            
           
        }
    }
}

