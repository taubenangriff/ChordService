using FastEndpoints;
using FastEndpoints.Swagger;
using ChordService.src.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddJsonConsole();

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddResponseCaching();
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureServices();

var app = builder.Build();

app.UseAuthorization();
app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi3(s => s.ConfigureDefaults());
}

app.UseOpenApi();
app.UseHttpsRedirection(); 
app.UseResponseCaching();

app.MapGet("/", () => "use chord/roman to get some fancy chord data or audio/roman to hear them!");

app.Run();
