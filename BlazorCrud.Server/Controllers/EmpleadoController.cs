using BlazorCrud.Server.Data;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public EmpleadoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseAPI = new ResponseAPI<List<EmpleadoDTO>>();
            var listaEmpleadoDTO = new List<EmpleadoDTO>();

            try
            {
                foreach (var item in await _db.Empleados.Include(d=>d.IdDepartamento).ToListAsync())
                {
                    listaEmpleadoDTO.Add(new EmpleadoDTO
                    {
                        Id = item.Id,
                        NombreCompleto = item.NombreCompleto,
                        Sueldo = item.Sueldo,
                        FechaContrato = item.FechaContrato,
                        IdDepartamento = item.IdDepartamento,
                        Departamento = new DepartamentoDTO
                        {
                            Id=item.Departamento.Id,
                            Nombre=item.Departamento.Nombre
                        }
                    });
                }
                responseAPI.EsCorrecto = true;
                responseAPI.Valor = listaEmpleadoDTO;

            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseAPI = new ResponseAPI<EmpleadoDTO>();
            var EmpleadoDTO = new EmpleadoDTO();

            try
            {
                var dbEmpleado = await _db.Empleados.FirstOrDefaultAsync(x => x.Id == id);
                if(dbEmpleado != null)
                {
                    EmpleadoDTO.Id = dbEmpleado.Id;
                    EmpleadoDTO.NombreCompleto = dbEmpleado.NombreCompleto;
                    EmpleadoDTO.Sueldo = dbEmpleado.Sueldo;
                    EmpleadoDTO.FechaContrato = dbEmpleado.FechaContrato;
                    EmpleadoDTO.IdDepartamento = dbEmpleado.IdDepartamento;

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = EmpleadoDTO;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "No encontrado";
                }
                
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }
    }
}
