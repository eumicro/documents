using document.file.repository.api.contracts;
using document.repository.api.client;
using document.repository.api.contracts;
using document.services.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDocumentService();
builder.Services.AddControllers();
builder.Services.AddSingleton<IDocumentRepository, DocumentRepositoryClient>((o) => new DocumentRepositoryClient("http://localhost:5171"));
builder.Services.AddSingleton<IFileRepository, FileRepositoryClient>((o) => new FileRepositoryClient("http://localhost:5041"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
