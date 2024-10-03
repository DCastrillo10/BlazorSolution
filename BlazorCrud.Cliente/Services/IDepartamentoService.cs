using BlazorCrud.Shared;

namespace BlazorCrud.Cliente.Services
{
    public interface IDepartamentoService
    {
        Task<list<DepartamentoDTO>> Lista();
    }
}
