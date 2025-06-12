using AgendaPro.Aplicacao.Dtos;
using AgendaPro.MVC.Services;
using System.Text.Json;

public class AgendaProApiClient : IAgendaProApiClient
{
    private readonly HttpClient _httpClient;

    public AgendaProApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    #region Sala
    public async Task<IEnumerable<SalaDto>> ListarSalasAsync()
    {
        // Faz uma chamada GET para o endpoint /api/salas da nossa API
        var response = await _httpClient.GetAsync("/api/salas");
        response.EnsureSuccessStatusCode(); // Lança uma exceção se a resposta não for 2xx

        // Lê o corpo da resposta e deserializa o JSON para uma lista de SalaDto
        return await response.Content.ReadFromJsonAsync<IEnumerable<SalaDto>>();
    }

    public async Task CriarSalaAsync(SalaDto sala)
    {
        // Envia uma requisição POST para /api/salas com os dados da sala no corpo
        var response = await _httpClient.PostAsJsonAsync("/api/salas", sala);
        response.EnsureSuccessStatusCode(); // Garante que a resposta foi 2xx (sucesso)
    }
    
    public async Task<SalaDto> GetSalaByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<SalaDto>($"/api/salas/RetonarSala/{id}");
    }

    public async Task ExcluirSalaAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"/api/salas/DeletarSala/{id}");
        response.EnsureSuccessStatusCode();
    }
    public async Task EditarSalaAsync(Guid id, SalaDto sala)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/salas/{id}", sala);
        response.EnsureSuccessStatusCode();
    }
    #endregion

    #region Usuario
    public async Task<IEnumerable<UsuarioDto>> ListarUsuariosAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<UsuarioDto>>("/api/usuarios");

    public async Task<UsuarioDto> GetUsuarioByIdAsync(Guid id) => await _httpClient.GetFromJsonAsync<UsuarioDto>($"/api/usuarios/RetornarUsuario/{id}");

    public async Task CriarUsuarioAsync(UsuarioDto usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/usuarios", usuario);
        response.EnsureSuccessStatusCode();
    }
    public async Task ExcluirUsuarioAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"/api/usuarios/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task EditarUsuarioAsync(Guid id, UsuarioDto usuario)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/usuarios/{id}", usuario);
        response.EnsureSuccessStatusCode();
    }

    #endregion

}