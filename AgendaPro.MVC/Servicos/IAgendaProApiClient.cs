using AgendaPro.Aplicacao.Dtos;


namespace AgendaPro.MVC.Services;

public interface IAgendaProApiClient
{
    // SALA
    Task<IEnumerable<SalaDto>> ListarSalasAsync();  
    Task CriarSalaAsync(SalaDto sala); 
    Task<SalaDto> GetSalaByIdAsync(Guid id);
    Task ExcluirSalaAsync(Guid id); 
    Task EditarSalaAsync(Guid id, SalaDto sala);

    // USUÁRIO
    Task<IEnumerable<UsuarioDto>> ListarUsuariosAsync();
    Task<UsuarioDto> GetUsuarioByIdAsync(Guid id);
    Task CriarUsuarioAsync(UsuarioDto usuario); 
    Task EditarUsuarioAsync(Guid id, UsuarioDto usuario);
    Task ExcluirUsuarioAsync(Guid id);


}