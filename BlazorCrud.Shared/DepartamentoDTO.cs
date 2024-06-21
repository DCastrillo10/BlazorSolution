using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class DepartamentoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(60, ErrorMessage = "El campo {0} debe ser maximo de {1} caracteres.")]
        public string Nombre { get; set; }
    }
}
