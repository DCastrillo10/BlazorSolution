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

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(EmpleadoDTO empleado)
        {
            var responseAPI = new ResponseAPI<int>();
            
            try
            {
                var dbEmpleado = new Empleado
                {
                    NombreCompleto = empleado.NombreCompleto,
                    Sueldo = empleado.Sueldo,
                    FechaContrato = empleado.FechaContrato,
                    IdDepartamento = empleado.IdDepartamento,
                };

                _db.Empleados.Add(dbEmpleado);
                await _db.SaveChangesAsync();

                if(dbEmpleado.Id != 0)
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbEmpleado.Id;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "No guardado";
                }

            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(EmpleadoDTO empleado, int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbEmpleado = await _db.Empleados.FirstOrDefaultAsync(e => e.Id == id);
                if (dbEmpleado != null)
                {
                    dbEmpleado.NombreCompleto = empleado.NombreCompleto;
                    dbEmpleado.Sueldo = empleado.Sueldo;
                    dbEmpleado.FechaContrato = empleado.FechaContrato;
                    dbEmpleado.IdDepartamento = empleado.IdDepartamento;

                    _db.Empleados.Update(dbEmpleado);
                    await _db.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbEmpleado.Id;

                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Empleado no encontrado";
                }

            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbEmpleado = await _db.Empleados.FirstOrDefaultAsync(e => e.Id == id);
                if (dbEmpleado != null)
                {
                    _db.Empleados.Remove(dbEmpleado);
                    await _db.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Empleado no encontrado";
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
