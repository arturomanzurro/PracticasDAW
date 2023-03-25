using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica01.Models;

namespace Practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carrerasController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;
        public carerrasController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        //Create a new career 
        [HttpPost]
        [Route("AddCareer")]
        public IActionResult addCareer([FromBody] carreras carerra)
        {
            try
            {
                _equiposContexto.carreras.Add(carerra);
                _equiposContexto.SaveChanges();
                return Ok(carerra);
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
                var listaCarrera = (from c in _equiposContexto.carreras
                                    join f in _equiposContexto.facultades on c.facultad_id equals f.facultad_id

                                    select new
                                    {
                                        c.carrera_id,
                                        c.nombre_carrera,
                                        c.facultad_id,
                                        f.nombre_facultad,
                                        c.estado
                                    }).ToList();

                if (listaCarrera.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listaCarrera);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

  
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult updatecarrera(int id, [FromBody] carreras modificarcarrera)
        {
           
            
                carreras? actcarrera = (from ca in _equiposContexto.carreras where ca.carrera_id == id select ca).FirstOrDefault();


                if (actcarrera == null) return NotFound();

                
                actcarrera.nombre_carrera = modificarcarrera.nombre_carrera;


                _equiposContexto.Entry(actcarrera).State = EntityState.Modified;
                _equiposContexto.SaveChanges();
                return Ok(actcarrera);
          
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminarcarrera(int id)
        {
            
                carreras? carreraeliminar = (from cel in _equiposContexto.carreras where cel.carrera_id == id select cel).FirstOrDefault();


                if (carreraeliminar == null) return NotFound();

                _equiposContexto.carreras.Attach(carreraeliminar);
                _equiposContexto.carreras.Remove(carreraeliminar);
                _equiposContexto.SaveChanges();
                return Ok(carreraeliminar);
           
        }
    }
}

