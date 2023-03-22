using Gerenciado_de_Tarefas.Hubs;
using Gerenciado_de_Tarefas.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<MonitorService>();
builder.Services.AddScoped<ProcessoService>();

builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy",
    builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

builder.Services.AddSignalR(o => { o.EnableDetailedErrors = true; });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MonitorHub>("/monitor");
});

app.Run();
