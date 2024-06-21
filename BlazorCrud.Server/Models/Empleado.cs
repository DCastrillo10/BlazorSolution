using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorCrud.Server.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        public string NombreCompleto { get; set; }

        public int Sueldo { get; set; }

        public DateTime FechaContrato { get; set; }

        //Foreigns Keys
        [Required]
        public int IdDepartamento { get; set; }

        [ForeignKey("IdDepartamento")]
        public Departamento Departamento { get; set; }
    }
}
