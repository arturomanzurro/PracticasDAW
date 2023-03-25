using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica01.Models;

namespace Practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class reservasController : ControllerBase
    {
        
        private readonly equiposContext _equiposContexto;
        public reservasController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

       
        [HttpPost]
        [Route("addreserva")]
        public IActionResult agregarreserva([FromBody] reservas reservas)
        {
            try
            {
                _equiposContexto.reservas.Add(reservas);
                _equiposContexto.SaveChanges();
                return Ok(reservas);
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
              var listadoreserva = (from re in _equiposContexto.reservas
                                    join eq in _equiposContexto.equipos on re.equipo_id equals eq.id_equipos
                                    join user in _equiposContexto.usuarios on re.usuario_id equals user.usuario_id
                                    join er in _equiposContexto.estados_reserva on re.reserva_id equals er.estado_res_id
                                    select new
                                    {
                                        re.reserva_id,
                                        re.equipo_id,
                                        eq.nombre,
                                        eq.descripcion,
                                        eq.costo,
                                        re.usuario_id,
                                        userName = user.nombre,
                                        user.documento,
                                        user.carnet,
                                        re.fecha_salida,
                                        re.fecha_retorno,
                                        re.tiempo_reserva,
                                        re.estado_reserva_id,
                                        estadoReserva = er.estado,
                                        re.estado
                                    }).ToList();

                if (listadoreserva.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoreserva);
            
          
        }

        
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult actualizarreservas(int id, [FromBody] reservas modificarreservas)
        {
            
               
                reservas? reserva = (from re in _equiposContexto.reservas where re.reserva_id == id select re).FirstOrDefault();


                if (reserva == null) return NotFound();

               
                reserva.fecha_salida = modificarreservas.fecha_salida;
                reserva.hora_salida = modificarreservas.hora_salida;
                reserva.tiempo_reserva = modificarreservas.tiempo_reserva;
                reserva.fecha_retorno = modificarreservas.fecha_retorno;
                reserva.hora_retorno = modificarreservas.hora_retorno;

                _equiposContexto.Entry(reserva).State = EntityState.Modified;
                _equiposContexto.SaveChanges();
                return Ok(reserva);
            
            

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminar(int id)
        {
            
            
                
                reservas? reservaeliminar = (from rd in _equiposContexto.reservas where rd.reserva_id == id select rd).FirstOrDefault();


                if (reservaeliminar == null) return NotFound();

                _equiposContexto.reservas.Attach(reservaeliminar);
                _equiposContexto.reservas.Remove(reservaeliminar);
                _equiposContexto.SaveChanges();
                return Ok(reservaeliminar);
            
            
        }
    }
}

