using AgendaPro.Aplicacao.Dtos;
using AgendaPro.MVC.Services;
using Microsoft.AspNetCore.Mvc;

public class SalasController : Controller
{
    private readonly IAgendaProApiClient _apiClient;

    public SalasController(IAgendaProApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> Index()
    {
        // Chama o nosso cliente para buscar os dados da API
        var salas = await _apiClient.ListarSalasAsync();
        // Passa a lista de salas para a View
        return View(salas);
    }

    public IActionResult Cadastrar()
    {
        return View("Cadastrar");
    }

    // --- PASSO 2.2: PROCESSAR O FORMULÁRIO (POST) ---
    // Este método é chamado quando o utilizador clica em "Criar" no formulário.
    [HttpPost]
    [ValidateAntiForgeryToken] // Medida de segurança contra ataques
    public async Task<IActionResult> Cadastrar([Bind("Nome,Capacidade")] SalaDto sala)  
    {
        if (ModelState.IsValid) // Verifica se os dados do formulário são válidos
        {
            await _apiClient.CriarSalaAsync(sala);
            // Se for bem-sucedido, redireciona para a lista de salas
            return RedirectToAction(nameof(Index));
        }
        // Se houver um erro de validação, mostra o formulário novamente com as mensagens de erro
        return View("Cadastrar", sala);
    }

    // GET: /Salas/Excluir/
    // Mostra a página de confirmação de exclusão
    public async Task<IActionResult> Excluir(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var sala = await _apiClient.GetSalaByIdAsync(id.Value);
        if (sala is null)
        {
            return NotFound();
        }

        // Passa os dados da sala para a view Excluir.cshtml
        return View(sala);
    }

    // POST: /Salas/Excluir/5
    // Executa a exclusão quando o utilizador confirma no formulário
    [HttpPost, ActionName("Excluir")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExcluirConfirmado(Guid id)
    {
        try
        {
            await _apiClient.ExcluirSalaAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (HttpRequestException ex)
        {
            // Apanhamos o erro da chamada à API
            // Adicionamos a mensagem de erro ao ModelState para ser mostrada na View.
            ModelState.AddModelError(string.Empty, "Não foi possível excluir. Verifique se a sala tem reservas associadas.");

            // Precisamos de buscar os dados da sala novamente para poder mostrar a página de confirmação com o erro.
            var sala = await _apiClient.GetSalaByIdAsync(id);
            return View(sala); // Volta para a página de exclusão, agora com a mensagem.
        }
    }

    public async Task<IActionResult> Editar(Guid id)
    {
        var sala = await _apiClient.GetSalaByIdAsync(id);
        if (sala == null)
        {
            return NotFound();
        }
        return View(sala);
    }

    // POST: /Salas/Editar/5
    // Processa os dados do formulário de edição.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Guid id, [Bind("Id,Nome,Capacidade")] SalaDto sala)
    {
        if (id != sala.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _apiClient.EditarSalaAsync(id, sala);
            return RedirectToAction(nameof(Index));
        }
        return View(sala);
    }
}
