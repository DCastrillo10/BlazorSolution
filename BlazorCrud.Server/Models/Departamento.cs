using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Server.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        
        public string Nombre { get; set; }
    }
}
