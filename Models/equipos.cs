using System.ComponentModel.DataAnnotations;
namespace Practica01.Models
{
    public class equipos
    {
        [Key]
        public int id_equipos { get; set; }
        public String nombre { get; set; }

        public String descripcion { get; set; }

        public int? tipo_equipo_id { get; set; }

        public int? marca_id { get; set; }
        public String modelo { get; set; }
        public int? anio_compra { get; set; }
        public decimal costo { get; set; }
        public int? vida_util { get; set; }
        public int? estado_equipo_id { get; set; }
        public String  estado { get; set; }


    }
}
