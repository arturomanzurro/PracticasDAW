using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica01.Models;

namespace Practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public usuariosController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        //Create method
        [HttpPost]
        [Route("Addusuario")]
        public IActionResult addusuario([FromBody] usuarios usuario)
        {
            try
            {
                _equiposContexto.usuarios.Add(usuario);
                _equiposContexto.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet]
        [Route("GetAll")]
        public IActionResult get()
        {
            try
            {
                var listausuario = (from us in _equiposContexto.usuarios
                                join ca in _equiposContexto.carreras on us.carrera_id equals ca.carrera_id

                                select new
                                {
                                    us.usuario_id,
                                    us.nombre,
                                    us.documento,
                                    us.tipo,
                                    us.carnet,
                                    us.carrera_id,
                                    ca.nombre_carrera,
                                    us.estado
                                }).ToList();

                
                if (listausuario.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listausuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut]
        [Route("userUpdate/{id}")]
        public IActionResult actualizarusuario(int id, [FromBody] usuarios usuarioModificar)
        {
            
            
                
                usuarios? actualizaruser = (from u in _equiposContexto.usuarios where u.usuario_id == id select u).FirstOrDefault();


                if (actualizaruser == null) return NotFound();

                actualizaruser.nombre = usuarioModificar.nombre;
                actualizaruser.documento = usuarioModificar.documento;
                actualizaruser.tipo = usuarioModificar.tipo;
                actualizaruser.carnet = usuarioModificar.carnet;

                _equiposContexto.Entry(actualizaruser).State = EntityState.Modified;
                _equiposContexto.SaveChanges();
                return Ok(actualizaruser);
          

        }
        [HttpDelete]
        [Route("eliminarusuario/{id}")]
        public IActionResult eliminar(int id)
        {
           
                
                usuarios? eliminaruser = (from u in _equiposContexto.usuarios where u.usuario_id == id select u).FirstOrDefault();


                if (eliminaruser == null) return NotFound();

                _equiposContexto.usuarios.Attach(eliminaruser);
                _equiposContexto.usuarios.Remove(eliminaruser);
                _equiposContexto.SaveChanges();
                return Ok(eliminaruser);
            
           
        }

    }
}
