using AgendaPro.Infraestrutura;
using AgendaPro.Aplicacao.Servicos;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// --- INJE��O DE DEPEND�NCIA ---

// 1. Registar os servi�os da camada de Infraestrutura (DbContext, Repositories)
builder.Services.AddInfraestruturaServices(builder.Configuration);

// 2. Registar os servi�os do MediatR. ESTA � A CORRE��O PRINCIPAL.
//    Esta linha diz � aplica��o para procurar todos os 'Commands' e 'Handlers'
//    no projeto 'AgendaPro.Aplicacao'.
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CriarReservaComando).Assembly);
});

// 3. Adicionar servi�os padr�o do ASP.NET Core
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- CONFIGURA��O DO PIPELINE HTTP ---

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