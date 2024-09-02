using Minesweeper.Filters;
using Minesweeper.Repositories;
using Minesweeper.Services;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ��� ����������� ��������� ������� (���������� ����������� �������������)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://minesweeper-test.studiotg.ru")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// ���������� ����������� ������������ ��������� ��������
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// ���������� ����� snake case
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<SnakeCaseSchemaFilter>();
});


// ��� ��� �� �� ������ ��� � ���� ������, �� � ��� ������
// ���� ��������� ����������� �� ��� ����������
builder.Services.AddSingleton<IGameRepository, InMemoryGameRepository>();

// ����� ��� ���������� ������ ���������� GameService, ��� ��� � ��� ��� ����������� �������
builder.Services.AddSingleton<IGameService, GameService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// ����������
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();

