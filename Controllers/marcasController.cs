using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica01.Models;

namespace Practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {

        private readonly equiposContext _equiposContexto;
        public marcasController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }


        [HttpPost]
        [Route("agregarmarca")]
        public IActionResult agregarmarca([FromBody] marcas marcas)
        {
            try
            {
                _equiposContexto.marcas.Add(marcas);
                _equiposContexto.SaveChanges();
                return Ok(marcas);
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
            
            
                List<marcas> listadomarca = (from lm in _equiposContexto.marcas select lm).ToList();

                if (listadomarca.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadomarca);
            
            
        }

        
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult actualizar(int id, [FromBody] marcas modificarmarcas)
        {
            try
            {

                marcas? marcas = (from m in _equiposContexto.marcas where m.id_marcas == id select m).FirstOrDefault();


                if (marcas == null) return NotFound();


                marcas.nombre_marca = modificarmarcas.nombre_marca;
                marcas.estados = modificarmarcas.estados;

                _equiposContexto.Entry(marcas).State = EntityState.Modified;
                _equiposContexto.SaveChanges();
                return Ok(marcas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminar(int id)
        {
            

                marcas? marcas = (from m in _equiposContexto.marcas where m.id_marcas == id select m).FirstOrDefault();


                if (marcas == null) return NotFound();

                _equiposContexto.marcas.Attach(marcas);
                _equiposContexto.marcas.Remove(marcas);
                _equiposContexto.SaveChanges();
                return Ok(marcas);
            
            

        }
    }
}
