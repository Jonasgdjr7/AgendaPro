using AgendaPro.Infraestrutura;
using AgendaPro.Aplicacao.Servicos;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// --- INJEÇÃO DE DEPENDÊNCIA ---

// 1. Registar os serviços da camada de Infraestrutura (DbContext, Repositories)
builder.Services.AddInfraestruturaServices(builder.Configuration);

// 2. Registar os serviços do MediatR. ESTA É A CORREÇÃO PRINCIPAL.
//    Esta linha diz à aplicação para procurar todos os 'Commands' e 'Handlers'
//    no projeto 'AgendaPro.Aplicacao'.
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CriarReservaComando).Assembly);
});

// 3. Adicionar serviços padrão do ASP.NET Core
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- CONFIGURAÇÃO DO PIPELINE HTTP ---

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();