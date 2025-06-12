using AgendaPro.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os padr�o do MVC ao container.
builder.Services.AddControllersWithViews();

// --- CONFIGURA��O DO HTTP CLIENT ---
// Isto regista o nosso IAgendaProApiClient.
builder.Services.AddHttpClient<IAgendaProApiClient, AgendaProApiClient>(client =>
{
    // Define o endere�o base da nossa API.
    // Altere a porta se a sua API estiver a rodar numa porta diferente!
    client.BaseAddress = new Uri("http://localhost:5299");
});

var app = builder.Build();

// Configura o pipeline de requisi��es HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
