using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica01.Models;

namespace Practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estados_equipoController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;
        public estados_equipoController(equiposContext equiposContext)
        {
            _equiposContexto = equiposContext;
        }

        
        [HttpPost]
        [Route("Addestado")]
        public IActionResult Addestado([FromBody] estados_equipo estadoequip)
        {
            
            
                _equiposContexto.estados_equipos.Add(estadoequip);
                _equiposContexto.SaveChanges();
                return Ok(estadoequip);
            
            
        }


 
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            
                List<estados_equipo> estadoequipo = (from esteq in _equiposContexto.estados_equipos select esteq).ToList();

                if (estadoequipo.Count == 0)
                {
                    return NotFound();
                }
                return Ok(estadoequipo);

            
           
        }

 
        [HttpPut]
        [Route("updateestado/{id}")]
        public IActionResult updateestado(int id, [FromBody] estados_equipo Moestadoequipo)
        {
            
            
                estados_equipo? estados = (from eseq in _equiposContexto.estados_equipos where eseq.id_estados_equipo == id select eseq).FirstOrDefault();

                if (estados == null) return NotFound();

                estados.descripcion = Moestadoequipo.descripcion;
                estados.estado = Moestadoequipo.estado;

                _equiposContexto.Entry(estados).State = EntityState.Modified;
                _equiposContexto.SaveChanges();
                return Ok(estados);

            
           
        }

        
        [HttpDelete]
        [Route("deleteestado/{id}")]
        public IActionResult deleteestado(int id)
        {
            
                estados_equipo? estados = (from eseq in _equiposContexto.estados_equipos where eseq.id_estados_equipo == id select eseq).FirstOrDefault();

                if (estados == null) return NotFound();

                _equiposContexto.estados_equipos.Attach(estados);
                _equiposContexto.estados_equipos.Remove(estados);
                _equiposContexto.SaveChanges();
                return Ok(estados);
            
           
        }

    }
}

