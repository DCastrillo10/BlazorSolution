using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class EmpleadoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser maximo de {1} caracteres.")]
        public string NombreCompleto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int Sueldo { get; set; }

        public DateTime FechaContrato { get; set; }

        [Required]
        [Range(1,int.MaxValue, ErrorMessage ="El campo {0} es requerido.")]
        public int IdDepartamento { get; set; }

        public DepartamentoDTO Departamento { get; set; }

    }
}
