using AgendaPro.Aplicacao.Dtos;
using AgendaPro.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AgendaPro.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IAgendaProApiClient _apiClient;

        public UsuariosController(IAgendaProApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        // GET: /Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _apiClient.ListarUsuariosAsync();
            return View(usuarios);
        }

        // GET: /Usuarios/Cadastrar
        public IActionResult Cadastrar()
        {
            return View();
        }

        // POST: /Usuarios/Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar([Bind("Nome,Email")] UsuarioDto usuario)
        {
            if (ModelState.IsValid)
            {
                // Lembre-se de adicionar o método CriarUsuarioAsync ao seu ApiClient
                // await _apiClient.CriarUsuarioAsync(usuario); 
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: /Usuarios/Edit/5
        public async Task<IActionResult> Editar(Guid id)
        {
            var usuario = await _apiClient.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: /Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, [Bind("Id,Nome,Email")] UsuarioDto usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _apiClient.EditarUsuarioAsync(id, usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: /Usuarios/Excluir/5
        public async Task<IActionResult> Excluir(Guid id)
        {
            var usuario = await _apiClient.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

    }
}