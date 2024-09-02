using Minesweeper.Filters;
using Minesweeper.Repositories;
using Minesweeper.Services;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<SnakeCaseSchemaFilter>();
});


// ��� ��� �� �� ������ ��� � ���� ������, �� � ��� ������
// ���� ��������� ����������� �� ��� ����������
builder.Services.AddSingleton<IGameRepository, InMemoryGameRepository>();
builder.Services.AddSingleton<IGameService, GameService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // �� ������ ����� ������!!!!!!!!!!!!!!!!!!!
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();

